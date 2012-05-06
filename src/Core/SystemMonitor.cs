using System;
using System.Collections.Generic;
using System.Security.Principal;
using Harvester.Core.Configuration;
using Harvester.Core.Messaging;
using Harvester.Core.Messaging.Parsers;
using Harvester.Core.Messaging.Sources.DbWin;
using Harvester.Core.Messaging.Sources.NamedPipe;
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

namespace Harvester.Core
{
    public class SystemMonitor : IDisposable
    {
        private readonly IList<MessageListener> messageListeners;
        private readonly IProcessMessages messageProcessor;

        public SystemMonitor(IRenderEvents eventRenderer)
        {
            messageProcessor = new MessageProcessor(eventRenderer, Settings.GetParsers(new ProcessRetriever()));
            messageListeners = new List<MessageListener>();

            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent() ?? WindowsIdentity.GetAnonymous());
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                messageListeners.Add(new OutputDebugStringListener(messageProcessor, @"Global\DBWIN", @"DBWinMutex"));

            messageListeners.Add(new OutputDebugStringListener(messageProcessor, @"Local\DBWIN", @"DBWinMutex"));
            messageListeners.Add(new PipeMessageListener(messageProcessor, @"\\.\pipe\Harvester", @"HarvestDBWinMutex"));
        }

        public void Dispose()
        {
            messageProcessor.Dispose();

            foreach (var messageListener in messageListeners)
                messageListener.Dispose();
        }
    }
}
