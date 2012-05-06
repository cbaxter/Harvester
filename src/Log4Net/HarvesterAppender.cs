using System;
using Harvester.Core;
using Harvester.Core.Messaging.Sources;
using Harvester.Core.Messaging.Sources.DbWin;
using Harvester.Core.Messaging.Sources.NamedPipe;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

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

namespace Harvester.Integration.Log4Net
{
    public sealed class HarvesterAppender : AppenderSkeleton
    {
        private IWriteMessages MessageWriter { get; set; }
        private IMessageBuffer MessageBuffer { get; set; }

        public String Binding { get; set; }
        public String Buffer { get; set; }
        public String Mutex { get; set; }

        public HarvesterAppender()
        {
            Layout = new XmlLayoutSchemaLog4j();
            Binding = @"\\.\pipe\Harvester";
            Buffer = @"NamedPipeBuffer";
            Mutex = @"HarvesterMutex";
        }

        public override void ActivateOptions()
        {
            base.ActivateOptions();

            switch (Buffer)
            {
                case "SharedMemoryBuffer":
                    SetupSharedMemoryBuffer(Binding, Mutex);
                    break;
                case "NamedPipeBuffer":
                    SetupNamedPipeBuffer(Binding, Mutex);
                    break;
                default:
                    throw new NotSupportedException(String.Format("Unknown buffer type specified '{0}'; supported buffers are 'SharedMemoryBuffer' and 'NamedPipeBuffer'.", Buffer));
            }
        }

        private void SetupSharedMemoryBuffer(String buffer, String mutex)
        {
            if (String.IsNullOrWhiteSpace(Binding) || !(Binding.StartsWith(@"Local\") || Binding.StartsWith(@"Global\")))
                throw new ArgumentException(Localization.InvalidSharedMemoryBinding, "buffer");

            if (String.IsNullOrWhiteSpace(Mutex))
                throw new ArgumentException(Localization.InvalidSharedMemoryMutex, "mutex");

            MessageBuffer = new SharedMemoryBuffer(buffer, OutputDebugString.BufferSize);
            MessageWriter = new OutputDebugStringWriter(mutex, MessageBuffer);
        }

        private void SetupNamedPipeBuffer(String buffer, String mutex)
        {
            if (String.IsNullOrWhiteSpace(Binding) || !Binding.StartsWith(@"\\.\pipe\"))
                throw new ArgumentException(Localization.InvalidNamedPipeBinding, "buffer");

            if (String.IsNullOrWhiteSpace(Mutex))
                throw new ArgumentException(Localization.InvalidNamedPipeMutex, "mutex");

            MessageBuffer = new NamedPipeClientBuffer(".", buffer);
            MessageWriter = new PipeMessageWriter(mutex, MessageBuffer);
        }

        protected override void OnClose()
        {
            base.OnClose();

            if (MessageBuffer == null)
                return;

            MessageBuffer.Dispose();
            MessageBuffer = null;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            MessageWriter.Write(RenderLoggingEvent(loggingEvent));
        }
    }
}
