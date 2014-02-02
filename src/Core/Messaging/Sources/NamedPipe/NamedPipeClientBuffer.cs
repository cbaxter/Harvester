using System;
using System.IO.Pipes;

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
    public sealed class NamedPipeClientBuffer : MessageBuffer
    {
        private static readonly Byte[] Empty = new Byte[0];
        private readonly String serverName;
        private readonly String pipeName;
        private Int32 timeout;

        public NamedPipeClientBuffer()
            : this(@".", @"\\.\pipe\Harvester")
        { }

        public NamedPipeClientBuffer(String serverName, String pipeName)
            : base(pipeName)
        {
            Verify.NotWhitespace(pipeName, "pipeName");
            Verify.NotWhitespace(serverName, "serverName");
            Verify.True(pipeName.StartsWith(@"\\.\pipe\"), "pipeName", Localization.InvalidNamedPipeName);

            this.serverName = serverName;
            this.pipeName = pipeName;

            Timeout = TimeSpan.FromSeconds(10);
        }

        protected override void Dispose(Boolean disposing)
        { }

        protected override Byte[] ReadMessage()
        {
            throw new NotSupportedException();
        }

        protected override void WriteMessage(Byte[] message)
        {
            message = message ?? Empty;
            using (var pipeStream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.None))
            {
                pipeStream.Connect(timeout);
                pipeStream.Write(message, 0, message.Length);
            }
        }

        protected override void OnTimeoutChanged()
        {
            timeout = Convert.ToInt32(Timeout.TotalMilliseconds);
        }
    }
}
