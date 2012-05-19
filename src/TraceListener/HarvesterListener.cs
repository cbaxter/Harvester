using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Xml;
using Harvester.Core;
using Harvester.Core.Messaging.Sources;
using Harvester.Core.Messaging.Sources.DbWin;
using Harvester.Core.Messaging.Sources.NamedPipe;

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

namespace Harvester.Integration.Diagnostics
{
    public sealed class HarvesterListener : TraceListener
    {
        private static readonly IList<Object> NoData = new Object[0];
        private const String NoCategory = "";
        private const String NoMessage = "";
        private const String NoLogger = "";

        private readonly IWriteMessages messageWriter;
        private readonly MessageBuffer messageBuffer;
        private readonly String machineName;
        private readonly String domain;

        public override bool IsThreadSafe { get { return true; } }

        public HarvesterListener(String initalizeData)
        {
            initalizeData = initalizeData ?? String.Empty;

            machineName = Environment.MachineName;
            domain = AppDomain.CurrentDomain.FriendlyName;

            var parsedInitializeData = ParseInitializationData(initalizeData);
            var binding = parsedInitializeData.ContainsKey("Binding") ? parsedInitializeData["Binding"] : @"\\.\pipe\Harvester";
            var mutexName = parsedInitializeData.ContainsKey("Mutex Name") ? parsedInitializeData["Mutex Name"] : "HarvesterMutex";
            var bufferType = parsedInitializeData.ContainsKey("Buffer Type") ? parsedInitializeData["Buffer Type"] : "NamedPipeBuffer";

            switch (bufferType)
            {
                case "SharedMemoryBuffer":
                    messageBuffer = new SharedMemoryBuffer(binding, OutputDebugString.BufferSize);
                    messageWriter = new OutputDebugStringWriter(mutexName, messageBuffer);
                    break;
                case "NamedPipeBuffer":
                    messageBuffer = new NamedPipeClientBuffer(".", binding);
                    messageWriter = new PipeMessageWriter(mutexName, messageBuffer);
                    break;
                default:
                    throw new NotSupportedException(String.Format(Localization.BufferTypeNotSupported, bufferType));
            }
        }

        private static IDictionary<String, String> ParseInitializationData(String initializeData)
        {
            if (String.IsNullOrWhiteSpace(initializeData))
                return new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);

            return initializeData.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(item => item.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                                 .Where(item => item.Length == 2)
                                 .ToDictionary(item => (item[0] ?? String.Empty).Trim(), item => (item[1] ?? String.Empty).Trim(), StringComparer.OrdinalIgnoreCase);
        }

        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);

            if (!disposing)
                return;

            messageBuffer.Dispose();
        }

        public override void Fail(String message)
        {
            Fail(message, NoCategory);
        }

        public override void Fail(String message, String detailMessage)
        {
            WriteEvent(DateTime.Now, TraceEventType.Critical, NoLogger, message + Environment.NewLine + detailMessage, NoData);
        }

        public override void TraceData(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id, Object data)
        {
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
                WriteEvent(DateTime.Now, TraceEventType.Verbose, NoCategory, NoMessage, new[] { data });
        }

        public override void TraceData(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id, params Object[] data)
        {
            if (data != null && (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data)))
                WriteEvent(DateTime.Now, TraceEventType.Verbose, NoCategory, NoMessage, data);
        }

        public override void TraceTransfer(TraceEventCache eventCache, String source, Int32 id, String message, Guid relatedActivityId)
        {
            TraceEvent(eventCache, source, TraceEventType.Transfer, id, message + ", relatedActivityId=" + relatedActivityId);
        }

        public override void TraceEvent(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id)
        {
            TraceEvent(eventCache, source, eventType, id, NoMessage, NoData);
        }

        public override void TraceEvent(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id, String message)
        {
            TraceEvent(eventCache, source, eventType, id, message, NoData);
        }

        // ReSharper disable MethodOverloadWithOptionalParameter
        public override void TraceEvent(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id, String format, params Object[] args)
        {
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
                WriteEvent(eventCache.DateTime, eventType, source, String.Format(format, args), null);
        }
        // ReSharper restore MethodOverloadWithOptionalParameter

        public override void Write(Object value)
        {
            if (value != null && (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, null, null, value, null)))
                WriteEvent(DateTime.Now, TraceEventType.Verbose, NoLogger, value.ToString(), NoData);
        }

        public override void Write(Object value, String category)
        {
            if (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, category, null, value, null))
                WriteEvent(DateTime.Now, TraceEventType.Verbose, category ?? NoCategory, value == null ? NoMessage : value.ToString(), null);
        }

        public override void Write(String message)
        {
            WriteEvent(DateTime.Now, TraceEventType.Verbose, NoCategory, message, NoData);
        }

        public override void Write(String message, String category)
        {
            if (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, message + Environment.NewLine, null, null, null))
                WriteEvent(DateTime.Now, TraceEventType.Verbose, category ?? NoCategory, message, NoData);
        }

        protected override void WriteIndent()
        {
            /* Do Nothing */
        }

        public override void WriteLine(Object value)
        {
            if (value != null && (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, null, null, value, null)))
                WriteEvent(DateTime.Now, TraceEventType.Verbose, NoCategory, value + Environment.NewLine, NoData);
        }

        public override void WriteLine(Object value, String category)
        {
            if (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, category, null, value, null))
                WriteEvent(DateTime.Now, TraceEventType.Verbose, category ?? NoCategory, value == null ? NoMessage : value + Environment.NewLine, NoData);
        }

        public override void WriteLine(String message)
        {
            Write(message + Environment.NewLine);
        }

        public override void WriteLine(String message, String category)
        {
            if (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, message + Environment.NewLine, null, null, null))
                WriteEvent(DateTime.Now, TraceEventType.Verbose, category ?? NoCategory, message, NoData);
        }

        private void WriteEvent(DateTime timestamp, TraceEventType level, String logger, String message, IList<Object> data)
        {
            var xml = new StringBuilder();

            using (var stringWriter = new StringWriter(xml))
            using (var xmlWriter = new XmlTextWriter(stringWriter))
            {
                xmlWriter.WriteStartElement("log4net:event");
                xmlWriter.WriteAttributeString("logger", logger);
                xmlWriter.WriteAttributeString("timestamp", timestamp.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzzz"));
                xmlWriter.WriteAttributeString("level", GetLevelFromType(level));
                xmlWriter.WriteAttributeString("thread", GetCurrentThread());
                xmlWriter.WriteAttributeString("domain", domain);
                xmlWriter.WriteAttributeString("username", GetCurrentUsername());

                xmlWriter.WriteStartElement("log4net:message");
                xmlWriter.WriteString(message);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("log4net:properties");

                xmlWriter.WriteStartElement("log4net:data");
                xmlWriter.WriteAttributeString("name", "log4net:HostName");
                xmlWriter.WriteAttributeString("value", machineName);
                xmlWriter.WriteEndElement();

                if (data != null)
                {
                    for (var i = 0; i < data.Count; i++)
                    {
                        var item = i + 1;

                        xmlWriter.WriteStartElement("log4net:data");
                        xmlWriter.WriteAttributeString("name", "log4net:DataItem #" + item);
                        xmlWriter.WriteAttributeString("value", data[i] == null ? String.Empty : data[i].ToString());
                        xmlWriter.WriteEndElement();
                    }
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }

            messageWriter.Write(xml.ToString());
        }

        private static String GetLevelFromType(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Critical: return "FATAL";
                case TraceEventType.Error: return "ERROR";
                case TraceEventType.Warning: return "WARN";
                case TraceEventType.Information: return "INFO";
                case TraceEventType.Verbose: return "DEBUG";
                default: return "TRACE";
            }
        }

        private static String GetCurrentUsername()
        {
            var principal = Thread.CurrentPrincipal;
            var username = principal != null && !String.IsNullOrWhiteSpace(principal.Identity.Name) ? Thread.CurrentPrincipal.Identity.Name : String.Empty;

            return String.IsNullOrWhiteSpace(username) ? (WindowsIdentity.GetCurrent() ?? WindowsIdentity.GetAnonymous()).Name : username;
        }

        private static String GetCurrentThread()
        {
            return Thread.CurrentThread.Name ?? Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture);
        }
    }
}
