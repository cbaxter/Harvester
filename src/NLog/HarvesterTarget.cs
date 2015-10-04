﻿using System;
using Harvester.Core;
using Harvester.Core.Messaging.Sources;
using Harvester.Core.Messaging.Sources.DbWin;
using Harvester.Core.Messaging.Sources.NamedPipe;
using NLog;
using NLog.Layouts;
using NLog.Targets;

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

namespace Harvester.Integration.NLog
{
    [Target("Harvester")]
    public sealed class HarvesterTarget : Target
    {
        private IWriteMessages MessageWriter { get; set; }
        private MessageBuffer MessageBuffer { get; set; }
        private Layout Layout { get; set; }

        public String BufferType { get; set; }
        public String MutexName { get; set; }
        public String Binding { get; set; }

        public HarvesterTarget()
        {
            Layout = new Log4JXmlEventLayout();
            Binding = @"\\.\pipe\Harvester";
            BufferType = @"NamedPipeBuffer";
            MutexName = @"Global\HarvesterMutex";
        }

        protected override void InitializeTarget()
        {
            base.InitializeTarget();

            switch (BufferType)
            {
                case "SharedMemoryBuffer":
                    MessageBuffer = new SharedMemoryBuffer(Binding, OutputDebugString.BufferSize);
                    MessageWriter = new OutputDebugStringWriter(MutexName, MessageBuffer);
                    break;
                case "NamedPipeBuffer":
                    MessageBuffer = new NamedPipeClientBuffer(".", Binding);
                    MessageWriter = new PipeMessageWriter(MutexName, MessageBuffer);
                    break;
                default:
                    throw new NotSupportedException(String.Format(Localization.BufferTypeNotSupported, BufferType));
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing || MessageBuffer == null)
                return;

            MessageBuffer.Dispose();
            MessageBuffer = null;
        }

        protected override void Write(LogEventInfo logEvent)
        {
            MessageWriter.Write(Layout.Render(logEvent));
        }
    }
}
