using System;
using Harvester.Core.Messaging;

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

namespace Harvester.Core
{
    public sealed class SystemEvent
    {
        private static readonly Sequence MessageIdSequence = new Sequence();

        public SystemEventLevel Level { get; internal set; }
        public DateTime Timestamp { get; internal set; }
        public UInt32 MessageId { get; private set; }
        public Int32 ProcessId { get; internal set; }
        public String ProcessName { get; internal set; }
        public String Thread { get; internal set; }
        public String Username { get; internal set; }
        public String Source { get; internal set; }
        public String Message { get; internal set; }
        public Lazy<String> RawMessage { get; internal set; }

        public SystemEvent()
        {
            MessageId = MessageIdSequence.Next();
        }

        public static SystemEvent Create(IMessage message)
        {
            Verify.NotNull(message, "message");

            return new SystemEvent
                       {
                           Level = SystemEventLevel.Trace,
                           ProcessName = "Process #" + message.ProcessId,
                           ProcessId = message.ProcessId,
                           Timestamp = message.Timestamp,
                           Message = message.Message,
                           Source = message.Source,
                           RawMessage = new Lazy<String>(() => message.Message)
                       };
        }
    }
}
