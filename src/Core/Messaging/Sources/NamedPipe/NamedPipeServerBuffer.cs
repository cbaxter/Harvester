using System;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;

/* Copyright (c) 2012-2013 CBaxter
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
 * IN THE SOFTWARE. 
 */

namespace Harvester.Core.Messaging.Sources.NamedPipe
{
    internal sealed class NamedPipeServerBuffer : MessageBuffer
    {
        private readonly NamedPipeServerStream pipeStream;
        private readonly MemoryStream memoryStream;
        private readonly Byte[] buffer;

        public NamedPipeServerBuffer()
            : this(@"\\.\pipe\Harvester")
        { }

        public NamedPipeServerBuffer(String pipeName)
            : base(pipeName)
        {
            Verify.NotWhitespace(pipeName, "pipeName");
            Verify.True(pipeName.StartsWith(@"\\.\pipe\"), "pipeName", Localization.InvalidNamedPipeName);

            pipeStream = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 0, 0, GetPipeSecurity());
            memoryStream = new MemoryStream();
            buffer = new Byte[8192];

            Timeout = TimeSpan.FromSeconds(10);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!disposing)
                return;

            pipeStream.Dispose();
            memoryStream.Dispose();
        }

        private static PipeSecurity GetPipeSecurity()
        {
            var pipeSecurity = new PipeSecurity();

            pipeSecurity.AddAccessRule(new PipeAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), PipeAccessRights.ReadWrite, AccessControlType.Allow));

            return pipeSecurity;
        }

        protected override Byte[] ReadMessage()
        {
            pipeStream.WaitForConnection();
            try
            {
                var bytesRead = pipeStream.Read(buffer, 0, buffer.Length);

                return pipeStream.IsMessageComplete ? ReadBuffer(bytesRead) : ReadChunkedBuffer(bytesRead);
            }
            finally
            {
                pipeStream.Disconnect();
            }
        }

        private Byte[] ReadBuffer(Int32 bytesRead)
        {
            var result = new Byte[bytesRead];

            Buffer.BlockCopy(buffer, 0, result, 0, bytesRead);

            return result;
        }

        private Byte[] ReadChunkedBuffer(Int32 bytesRead)
        {
            memoryStream.SetLength(0);
            memoryStream.Write(buffer, 0, bytesRead);

            while (!pipeStream.IsMessageComplete)
            {
                bytesRead = pipeStream.Read(buffer, 0, buffer.Length);
                memoryStream.Write(buffer, 0, bytesRead);
            }

            return memoryStream.ToArray();
        }

        protected override void WriteMessage(Byte[] message)
        {
            throw new NotSupportedException();
        }
    }
}
