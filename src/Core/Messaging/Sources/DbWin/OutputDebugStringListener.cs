using System;
using System.Threading;

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

namespace Harvester.Core.Messaging.Sources.DbWin
{
    internal class OutputDebugStringListener : MessageListener
    {
        private readonly MessageBuffer messageBuffer;
        private readonly Mutex mutex;

        public OutputDebugStringListener(IProcessMessages messageProcessor, IConfigureListeners configuration)
            : this(GetSource(configuration), messageProcessor, new SharedMemoryBuffer(GetSource(configuration), OutputDebugString.BufferSize), GetMutex(configuration))
        { }

        private OutputDebugStringListener(String source, IProcessMessages messageProcessor, MessageBuffer messageBuffer, Mutex mutex)
            : base(source, messageProcessor, new OutputDebugStringReader(messageBuffer))
        {
            this.mutex = mutex;
            this.messageBuffer = messageBuffer;
        }

        private static String GetSource( IConfigureListeners configuration)
        {
            Verify.NotNull(configuration, "configuration");

            return configuration.Binding;
        }

        private static Mutex GetMutex(IConfigureListeners configuration)
        {
            Verify.NotNull(configuration, "configuration");

            return new Mutex(false, configuration.Mutex);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                messageBuffer.Dispose();
                mutex.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
