using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Harvester.Core.Messaging.Parsers;

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
    internal class MessageProcessor : IProcessMessages
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly BlockingCollection<IMessage> messageQueue;
        private readonly IEnumerable<IParseMessages> parsers;
        private readonly Object syncLock = new Object();
        private readonly IRenderEvents renderer;
        private readonly Thread processor;

        public MessageProcessor(IRenderEvents eventRenderer, IList<IParseMessages> messageParsers)
        {
            Verify.NotNull(eventRenderer, "eventRenderer");
            Verify.NotNull(messageParsers, "messageParsers");

            renderer = eventRenderer;
            parsers = messageParsers;
            messageQueue = new BlockingCollection<IMessage>();
            cancellationTokenSource = new CancellationTokenSource();
            processor = new Thread(ProcessAllMessages) { IsBackground = true, Name = "Processor", };
            processor.Start();
        }

        public void Dispose()
        {
            lock (syncLock)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                    return;

                cancellationTokenSource.Cancel();
                processor.Join();

                cancellationTokenSource.Dispose();
                messageQueue.Dispose();
            }
        }

        private void ProcessAllMessages()
        {
            try
            {
                foreach (var message in messageQueue.GetConsumingEnumerable(cancellationTokenSource.Token))
                {
                    var parser = parsers.FirstOrDefault(p => p.CanParseMessage(message.Message));

                    renderer.Render(parser != null ? parser.Parse(message) : SystemEvent.Create(message));
                }
            }
            catch (OperationCanceledException)
            { }
        }

        public void Process(IMessage message)
        {
            lock (syncLock)
            {
                if (message == null || cancellationTokenSource.IsCancellationRequested)
                    return;

                messageQueue.Add(message);
            }
        }
    }
}
