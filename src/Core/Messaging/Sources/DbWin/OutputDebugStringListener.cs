using System;
using System.Security.AccessControl;
using System.Security.Principal;
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
    /// <summary>
    /// An output debug string (i.e., DBWin) message listener.
    /// </summary>
    internal class OutputDebugStringListener : MessageListener
    {
        private readonly MessageBuffer messageBuffer;
        private readonly Mutex mutex;

        /// <summary>
        /// Creates a new instance of <see cref="OutputDebugStringListener"/>.
        /// </summary>
        /// <param name="messageProcessor">The underlying message processor instance.</param>
        /// <param name="configuration">The named pipe message listener configuration.</param>
        public OutputDebugStringListener(IProcessMessages messageProcessor, IConfigureListeners configuration)
            : this(GetSource(configuration), messageProcessor, new SharedMemoryBuffer(GetSource(configuration), OutputDebugString.BufferSize), GetMutex(configuration))
        { }

        /// <summary>
        /// Creates a new instance of <see cref="OutputDebugStringListener"/>.
        /// </summary>
        /// <param name="source">The shared memory source name.</param>
        /// <param name="messageProcessor">The underlying message processor instance.</param>
        /// <param name="messageBuffer">The underlying message buffer instance.</param>
        /// <param name="mutex">The mutex used to indicate a listener is active.</param>
        private OutputDebugStringListener(String source, IProcessMessages messageProcessor, MessageBuffer messageBuffer, Mutex mutex)
            : base(source, messageProcessor, new OutputDebugStringReader(messageBuffer))
        {
            this.mutex = mutex;
            this.messageBuffer = messageBuffer;
        }

        /// <summary>
        /// Gets the named pipe binding configuration (i.e., source).
        /// </summary>
        /// <param name="configuration">The underlying listener configuration.</param>
        private static String GetSource(IConfigureListeners configuration)
        {
            Verify.NotNull(configuration, "configuration");

            return configuration.Binding;
        }

        /// <summary>
        /// Get or create the mutex instance used to singal that one or more listeners are active.
        /// </summary>
        /// <param name="configuration">The underlying listener configuration.</param>
        private static Mutex GetMutex(IConfigureListeners configuration)
        {
            Verify.NotNull(configuration, "configuration");

            return CreateMutex(configuration.Mutex);
        }

        /// <summary>
        /// Releases all resources used by the current instance of <see cref="OutputDebugStringListener"/>.
        /// </summary>
        /// <param name="disposing">Indicates whether the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).</param>
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
