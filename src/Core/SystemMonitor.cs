using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Harvester.Core.Configuration;
using Harvester.Core.Messaging;
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
        private readonly Mutex mutex;

        private SystemMonitor(Mutex singleInstance)
        {
            Verify.NotNull(singleInstance, "singleInstance");

            messageListeners = new MessageListener[0];
            mutex = singleInstance;
        }

        private SystemMonitor(Mutex singleInstance, IRenderEvents eventRenderer)
        {
            Verify.NotNull(singleInstance, "singleInstance");
            Verify.NotNull(eventRenderer, "eventRenderer");

            messageProcessor = new MessageProcessor(eventRenderer, Settings.GetParsers(new ProcessRetriever()));
            messageListeners = Settings.GetListeners(messageProcessor);
            mutex = singleInstance;

            foreach (var messageListener in messageListeners)
                messageListener.Start();
        }

        public void Dispose()
        {
            foreach (var messageListener in messageListeners)
                messageListener.Dispose();

            if (messageProcessor != null)
                messageProcessor.Dispose();

            mutex.Dispose();
        }

        public static SystemMonitor CreateSingleInstance(IRenderEvents eventRenderer, out Boolean onlyInstance)
        {
            var singleInstance = new Mutex(true, "HarvesterSingleInstance", out onlyInstance);

            return onlyInstance ? new SystemMonitor(singleInstance, eventRenderer) : new SystemMonitor(singleInstance);
        }

        public static void ShowExistingInstance()
        {
            using (var currentProcess = Process.GetCurrentProcess())
            {
                var otherInstance = Process.GetProcesses()
                                           .Where(HasCoreAssemblyModuleLoaded)
                                           .FirstOrDefault(process => process.Id != currentProcess.Id);

                if (otherInstance == null)
                    return;

                NativeMethods.ActivateWindow(otherInstance.MainWindowHandle);
            }
        }

        private static Boolean HasCoreAssemblyModuleLoaded(Process process)
        {
            try
            {
                return process.Modules.Cast<ProcessModule>().Any(m => m.ModuleName == CoreAssembly.Reference.ManifestModule.Name);
            }
            catch (Win32Exception)
            {
                // An `Access Denied` exception may be thrown if the process requires elevated access to see module information.
                return false;
            }
        }
    }
}
