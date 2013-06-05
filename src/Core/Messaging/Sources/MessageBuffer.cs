using System;

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

namespace Harvester.Core.Messaging.Sources
{
    public abstract class MessageBuffer : IDisposable
    {
        private readonly String name;
        private TimeSpan timeout;

        public String Name { get { return name; } }
        public MessageBufferState State { get; private set; }
        public TimeSpan Timeout { get { return timeout; } set { timeout = value; OnTimeoutChanged(); } }

        protected virtual void OnTimeoutChanged()
        { }

        protected MessageBuffer(String name)
        {
            Verify.NotWhitespace(name, name);

            this.name = name;
        }

        public void Dispose()
        {
            State = MessageBufferState.Closed;

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(Boolean disposing);

        public virtual Byte[] Read()
        {
            EnsureConnected();

            try
            {
                return ReadMessage();
            }
            catch (Exception)
            {
                if (State != MessageBufferState.Closed)
                    State = MessageBufferState.Broken;

                throw;
            }
        }

        protected abstract Byte[] ReadMessage();

        public virtual void Write(Byte[] message)
        {
            EnsureConnected();

            try
            {
                WriteMessage(message);
            }
            catch (Exception)
            {
                if (State != MessageBufferState.Closed)
                    State = MessageBufferState.Broken;

                throw;
            }
        }

        protected abstract void WriteMessage(Byte[] message);

        private void EnsureConnected()
        {
            if (State == MessageBufferState.Broken)
                throw new InvalidOperationException(Localization.MessageBufferBroken);

            if (State == MessageBufferState.Closed)
                throw new ObjectDisposedException(GetType().Name, Localization.MessageBufferClosed);
        }
    }

}
