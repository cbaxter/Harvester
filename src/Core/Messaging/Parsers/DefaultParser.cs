using System;
using System.Collections.Generic;
using Harvester.Core.Processes;

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

namespace Harvester.Core.Messaging.Parsers
{
    internal class DefaultParser : IParseMessages
    {
        private readonly IRetrieveProcesses processes;

        public DefaultParser(IRetrieveProcesses processRetriever, IDictionary<String, String> extendedProperties)
        {
            Verify.NotNull(processRetriever, "processRetriever");
            Verify.NotNull(extendedProperties, "extendedProperties");

            processes = processRetriever;
        }

        public bool CanParseMessage(String message)
        {
            return true;
        }

        public SystemEvent Parse(IMessage message)
        {
            Verify.NotNull(message, "message");

            var process = processes.GetProcessById(message.ProcessId);

            return new SystemEvent
                       {
                           Level = SystemEventLevel.Trace,
                           ProcessName = process.Name,
                           ProcessId = process.Id,
                           Timestamp = message.Timestamp,
                           Message = message.Message,
                           Source = message.Source,
                           RawMessage = new Lazy<String>(() => message.Message),
                           Thread = String.Empty,
                           Username = String.Empty,
                       };
        }
    }
}
