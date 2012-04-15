using System;
using System.Collections.Generic;

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

namespace Harvester.Core.Messaging.Sources.DbWin
{
    internal class OutputDebugStringReader : IReadMessages
    {
        private const Int32 MaxFragmentSize = OutputDebugString.MaxMessageSize * 16;
        private readonly Queue<OutputDebugString> queuedMessages = new Queue<OutputDebugString>();
        private readonly IMessageBuffer messageBuffer;
        private OutputDebugString partial;

        public IMessage Current { get; private set; }

        public OutputDebugStringReader(IMessageBuffer messageBuffer)
        {
            Verify.NotNull(messageBuffer, "messageBuffer");

            this.messageBuffer = messageBuffer;
        }

        public Boolean ReadNext()
        {
            WaitForMessages();

            Current = queuedMessages.Dequeue();

            return true;
        }

        private void WaitForMessages()
        {
            while (queuedMessages.Count == 0)
            {
                var data = messageBuffer.Read();
                var fragment = new OutputDebugString(data);

                if (NewMessageFragment(fragment))
                    FlushPartialMessage();

                if (String.IsNullOrEmpty(fragment.Message))
                    continue;

                AppendToPartialMessage(fragment);
                
                if (LastFragment(fragment) || ExceedsMaxMessageLength(partial))
                    FlushPartialMessage();
            }
        }

        private Boolean NewMessageFragment(OutputDebugString fragment)
        {
            return partial != null && (partial.ProcessId != fragment.ProcessId || String.IsNullOrEmpty(fragment.Message));
        }

        private void FlushPartialMessage()
        {
            queuedMessages.Enqueue(partial);
            partial = null;
        }

        private void AppendToPartialMessage(OutputDebugString fragment)
        {
            partial = partial == null ? fragment : new OutputDebugString(partial.ProcessId, partial.Message + fragment.Message);
        }

        private Boolean ExceedsMaxMessageLength(OutputDebugString fragment)
        {
            // HACK: As we are forced to rely on a FULL messageBuffer denoting a message fragment, guard against all messages being exactly MaxFragmentSize. 
            //       In the case of OutputDebugString we will cap at approximately 64K before we force flush the message regardless. 
            return fragment.Message.Length >= MaxFragmentSize;
        }

        private Boolean LastFragment(OutputDebugString fragment)
        {
            // HACK: Unfortunately, the good folks who wrote the implementation of OutputDebugString did not feel compelled to include a message length in the 
            //       serialized data. As such, let's assume that any message that exactly fills the messageBuffer is likely just a fragment provided the next message
            //       comes from the same process. This may be a lie, but it is far more likely that a message overflows the messageBuffer than exactly fills the 
            //       messageBuffer (worst case: formatting is lost - no data will be lost).
            return fragment.Message.Length < OutputDebugString.MaxMessageSize;
        }
    }
}
