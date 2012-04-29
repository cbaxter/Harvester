using System;
using System.Threading;
using Harvester.Core.Messaging.Sources;

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

namespace Harvester.Core.Messaging
{
    internal abstract class MessageListener : IDisposable
    {
        private readonly IProcessMessages messageProcessor;
        private readonly IReadMessages messageReader;
        private readonly Thread listener;
        private readonly String source;

        protected MessageListener(String source, IProcessMessages messageProcessor, IReadMessages messageReader)
        {
            Verify.NotNull(messageProcessor, "messageProcessor");
            Verify.NotNull(messageReader, "messageReader");
            Verify.NotWhitespace(source, "source");

            this.messageProcessor = messageProcessor;
            this.messageReader = messageReader;
            this.source = source;

            listener = new Thread(ReadAllMessages) { IsBackground = true, Priority = ThreadPriority.AboveNormal, Name = source, };
            listener.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!disposing)
                return;

            listener.Abort();
            listener.Join();
        }

        protected virtual void ReadAllMessages()
        {
            while (messageReader.ReadNext())
                messageProcessor.Process(source, messageReader.Current);
        }
    }
}
