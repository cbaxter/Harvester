using System;
using System.Threading;

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

namespace Harvester.Core.Messaging.Sources.NamedPipe
{
    internal class PipeMessageListener : MessageListener
    {
        private readonly MessageBuffer messageBuffer;
        private readonly Mutex mutex;

        public PipeMessageListener(IProcessMessages messageProcessor, IConfigureListeners configuration)
            : this(GetSource(configuration), messageProcessor, new NamedPipeServerBuffer(GetSource(configuration), GetIdentity(configuration)), GetMutex(configuration))
        { }

        private PipeMessageListener(String listenerName, IProcessMessages messageProcessor, MessageBuffer messageBuffer, Mutex mutex)
            : base(listenerName, messageProcessor, new PipeMessageReader(messageBuffer))
        {
            this.mutex = mutex;
            this.messageBuffer = messageBuffer;
        }

        private static String GetSource(IConfigureListeners configuration)
        {
            Verify.NotNull(configuration, "configuration");

            return configuration.Binding;
        }

        private static String GetIdentity(IConfigureListeners configuration)
        {
            Verify.NotNull(configuration, "configuration");
            Verify.True(configuration.HasExtendedProperty("identity"), "configuration", "Listener mising configuration attribute 'identity'.\r\nName: " + configuration.Binding);

            return configuration.GetExtendedProperty("identity");
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
