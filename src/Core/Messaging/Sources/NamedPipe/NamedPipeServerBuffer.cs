using System;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;

/* Copyright (c) 2012 CBaxter
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
    internal class NamedPipeServerBuffer : IMessageBuffer
    {
        private readonly NamedPipeServerStream pipeStream;
        private readonly MemoryStream memoryStream;
        private readonly Byte[] buffer;

        public TimeSpan Timeout { get; set; }

        public NamedPipeServerBuffer()
            : this(@"\\.\pipe\Harvester", "Everyone")
        { }

        public NamedPipeServerBuffer(String pipeName, String identity)
        {
            Verify.NotWhitespace(pipeName, "pipeName");
            Verify.NotWhitespace(identity, "identity");

            this.pipeStream = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.None, 0, 0, GetPipeSecurity(identity));
            this.memoryStream = new MemoryStream();
            this.buffer = new Byte[8192];

            Timeout = TimeSpan.FromSeconds(10);
        }

        public void Dispose()
        {
            this.pipeStream.Dispose();
            this.memoryStream.Dispose();
        }

        private static PipeSecurity GetPipeSecurity(String identity)
        {
            var pipeSecurity = new PipeSecurity();

            pipeSecurity.AddAccessRule(new PipeAccessRule(identity, PipeAccessRights.ReadWrite, AccessControlType.Allow));

            return pipeSecurity;
        }

        public Byte[] Read()
        {
            this.pipeStream.WaitForConnection();
            try
            {
                var bytesRead = this.pipeStream.Read(this.buffer, 0, this.buffer.Length);

                return this.pipeStream.IsMessageComplete ? ReadBuffer(bytesRead) : ReadChunkedBuffer(bytesRead);
            }
            finally
            {
                this.pipeStream.Disconnect();
            }
        }

        private Byte[] ReadBuffer(Int32 bytesRead)
        {
            var result = new Byte[bytesRead];

            Buffer.BlockCopy(this.buffer, 0, result, 0, bytesRead);

            return result;
        }

        private Byte[] ReadChunkedBuffer(Int32 bytesRead)
        {
            this.memoryStream.SetLength(0);
            this.memoryStream.Write(this.buffer, 0, bytesRead);

            while (!this.pipeStream.IsMessageComplete)
            {
                bytesRead = this.pipeStream.Read(this.buffer, 0, this.buffer.Length);
                this.memoryStream.Write(this.buffer, 0, bytesRead);
            }

            return this.memoryStream.ToArray();
        }

        public void Write(Byte[] message)
        {
            throw new NotSupportedException();
        }
    }
}
