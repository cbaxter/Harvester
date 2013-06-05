using System;
using System.IO;

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
    public sealed class PipeMessageReader : IReadMessages
    {
        private readonly MessageBuffer memoryBuffer;

        public IMessage Current { get; private set; }

        public PipeMessageReader(MessageBuffer memoryBuffer)
        {
            Verify.NotNull(memoryBuffer, "memoryBuffer");

            this.memoryBuffer = memoryBuffer;
        }

        public Boolean ReadNext()
        {
            try
            {
                Current = new PipeMessage(memoryBuffer.Name, memoryBuffer.Read());
                return true;
            }
            catch (Exception ex)
            {
                // An IOException or ObjectDisposedException is thrown from the NamedPipeServerStream.WaitForConnection
                // when disposed; thus if the message buffer state is closed we can ignore the exception and return false.
                if (memoryBuffer.State != MessageBufferState.Closed || !(ex is IOException || ex is ObjectDisposedException))
                    throw;

                Current = null;
                return false;
            }
        }
    }
}
