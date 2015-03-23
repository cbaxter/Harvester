using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Harvester.Core.Messaging.Sources;

/* Copyright (c) 2012-2015 CBaxter
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
    /// <summary>
    /// A base message listener implementation.
    /// </summary>
    public abstract class MessageListener : IDisposable
    {
        private readonly IProcessMessages messageProcessor;
        private readonly IReadMessages messageReader;
        private readonly Thread listener;

        /// <summary>
        /// Initializes a new instance of <see cref="MessageListener"/>.
        /// </summary>
        /// <param name="source">The message listener name.</param>
        /// <param name="messageProcessor">The underlying message processor instance.</param>
        /// <param name="messageReader">The underlying message reader instance.</param>
        protected MessageListener(String source, IProcessMessages messageProcessor, IReadMessages messageReader)
        {
            Verify.NotNull(messageProcessor, "messageProcessor");
            Verify.NotNull(messageReader, "messageReader");
            Verify.NotWhitespace(source, "source");

            this.messageProcessor = messageProcessor;
            this.messageReader = messageReader;

            listener = new Thread(ReadAllMessages) { IsBackground = true, Priority = ThreadPriority.AboveNormal, Name = source, };
        }

        /// <summary>
        /// Start listening for messages.
        /// </summary>
        public void Start()
        {
            listener.Start();
        }

        /// <summary>
        /// Releases all resources used by the current instance of <see cref="MessageListener"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the current instance of <see cref="MessageListener"/>.
        /// </summary>
        /// <param name="disposing">Indicates whether the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).</param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (!disposing)
                return;

            listener.Join();
        }

        /// <summary>
        /// Blocks the current thread waiting for the next message to process.
        /// </summary>
        protected virtual void ReadAllMessages()
        {
            // ReadNext will block and always return true until the  
            // underlying message buffer is closed.
            while (messageReader.ReadNext())
                messageProcessor.Process(messageReader.Current);
        }

        /// <summary>
        /// Create the mutex instance used to singal that one or more listeners are active.
        /// </summary>
        /// <param name="mutexName">The shared mutex name.</param>
        protected static Mutex CreateMutex(String mutexName)
        {
            var securitySettings = new MutexSecurity();
            var createdNew = false;

            securitySettings.AddAccessRule(new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow));

            return new Mutex(false, mutexName, out createdNew, securitySettings);
        }
    }
}
