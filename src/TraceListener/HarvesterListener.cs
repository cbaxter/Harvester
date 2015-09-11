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

namespace Harvester.Integration.Diagnostics
{
    /// <summary>
    /// Harvester integration <see cref="TraceListener"/>.
    /// </summary>
    public sealed class HarvesterListener : TraceListener
    {
        private readonly String domain = AppDomain.CurrentDomain.FriendlyName;
        private readonly String machineName = Environment.MachineName;
        private readonly IWriteMessages messageWriter;
        private readonly MessageBuffer messageBuffer;
        private readonly Boolean captureIdentity;

        /// <summary>
        /// Gets a value indicating whether the trace listener is thread safe.
        /// </summary>
        public override bool IsThreadSafe { get { return true; } }

        /// <summary>
        /// Initializes a new instance of <see cref="HarvesterListener"/>.
        /// </summary>
        /// <param name="initializeData">The message buffer initialization string (i.e., Binding=\\.\pipe\Harvester; Buffer Type=NamedPipeBuffer; Mutex Name=Global\HarvesterMutex;)</param>
        public HarvesterListener(String initializeData)
        {
            var parsedInitializeData = ParseInitializationData(initializeData ?? String.Empty);
            var binding = parsedInitializeData.ContainsKey("Binding") ? parsedInitializeData["Binding"] : @"\\.\pipe\Harvester";
            var mutexName = parsedInitializeData.ContainsKey("Mutex Name") ? parsedInitializeData["Mutex Name"] : "Global\HarvesterMutex";
            var bufferType = parsedInitializeData.ContainsKey("Buffer Type") ? parsedInitializeData["Buffer Type"] : "NamedPipeBuffer";

            captureIdentity = !parsedInitializeData.ContainsKey("Capture Identity") || Boolean.Parse(parsedInitializeData["Capture Identity"]);
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

        /// <summary>
        /// Parse the initialization data string to determine the underlying message buffer/write implementations.
        /// </summary>
        /// <param name="initializeData">The message buffer initialization string (i.e., Binding=\\.\pipe\Harvester; Buffer Type=NamedPipeBuffer; Mutex Name=Global\HarvesterMutex;)</param>
        private static IDictionary<String, String> ParseInitializationData(String initializeData)
        {
            if (String.IsNullOrWhiteSpace(initializeData))
                return new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);

            return initializeData.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(item => item.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                                 .Where(item => item.Length == 2)
                                 .ToDictionary(item => (item[0] ?? String.Empty).Trim(), item => (item[1] ?? String.Empty).Trim(), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.TraceListener"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);

            if (!disposing)
                return;

            messageBuffer.Dispose();
        }

        /// <summary>
        /// Emits an error message to the listener you create when you implement the <see cref="TraceListener"/> class.
        /// </summary>
        /// <param name="message">A message to emit.</param>
        public override void Fail(String message)
        {
            Fail(message, String.Empty);
        }

        /// <summary>
        /// Emits an error message and a detailed error message to the listener you create when you implement the <see cref="TraceListener"/> class.
        /// </summary>
        /// <param name="message">A message to emit. </param><param name="detailMessage">A detailed message to emit.</param>
        public override void Fail(String message, String detailMessage)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Fail: ");
            stringBuilder.Append(message);

            if (!String.IsNullOrWhiteSpace(detailMessage))
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(detailMessage);
            }

            WriteEvent(DateTime.Now, TraceEventType.Critical, String.Empty, stringBuilder.ToString());
        }

        /// <summary>
        /// Writes trace information, a data object and event information to the listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">The trace data to emit.</param>
        public override void TraceData(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id, Object data)
        {
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
                WriteEvent(eventCache.DateTime, eventType, source, data == null ? String.Empty : data.ToString());
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        /// <summary>
        /// Writes trace information, an array of data objects and event information to the listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">An array of objects to emit as data.</param>
        public override void TraceData(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id, params Object[] data)
        {
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                var stringBuilder = new StringBuilder();

                if (data != null)
                {
                    for (var i = 0; i < data.Length; ++i)
                    {
                        if (i != 0)
                            stringBuilder.Append(", ");

                        if (data[i] != null)
                            stringBuilder.Append(data[i]);
                    }
                }

                WriteEvent(eventCache.DateTime, eventType, source, stringBuilder.ToString());
            }
        }

        /// <summary>
        /// Writes trace information, a message, a related activity identity and event information to the listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">A message to write.</param>
        /// <param name="relatedActivityId">A <see cref="Guid"/>  object identifying a related activity.</param>
        public override void TraceTransfer(TraceEventCache eventCache, String source, Int32 id, String message, Guid relatedActivityId)
        {
            TraceEvent(eventCache, source, TraceEventType.Transfer, id, message + ", relatedActivityId=" + relatedActivityId);
        }

        /// <summary>
        /// Writes trace and event information to the listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        public override void TraceEvent(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id)
        {
            TraceEvent(eventCache, source, eventType, id, String.Empty);
        }

        /// <summary>
        /// Writes trace information, a message, and event information to the listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">A message to write.</param>
        public override void TraceEvent(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id, String message)
        {
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
                WriteEvent(eventCache.DateTime, eventType, source, message);
        }

        /// <summary>
        /// Writes trace information, a formatted array of objects and event information to the listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the <see cref="TraceEventType"/> values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args"/> array.</param>
        /// <param name="args">An object array containing zero or more objects to format.</param>
        public override void TraceEvent(TraceEventCache eventCache, String source, TraceEventType eventType, Int32 id, String format, params Object[] args)
        {
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
                WriteEvent(eventCache.DateTime, eventType, source, args == null ? format : String.Format(format, args));
            // ReSharper enable ConditionIsAlwaysTrueOrFalse
        }

        /// <summary>
        /// Writes the value of the object's <see cref="M:System.Object.ToString"/> method to the listener you create when you implement the <see cref="TraceListener"/> class.
        /// </summary>
        /// <param name="value">An <see cref="Object"/> whose fully qualified class name you want to write.</param>
        public override void Write(Object value)
        {
            if (value != null && (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, null, null, value, null)))
                WriteEvent(DateTime.Now, null, String.Empty, value.ToString());
        }

        /// <summary>
        /// Writes a category name and the value of the object's <see cref="Object.ToString"/> method to the listener you create when you implement the <see cref="TraceListener"/> class.
        /// </summary>
        /// <param name="value">An <see cref="Object"/> whose fully qualified class name you want to write. </param>
        /// <param name="category">A category name used to organize the output. </param>
        public override void Write(Object value, String category)
        {
            if (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, category, null, value, null))
                WriteEvent(DateTime.Now, null, category, value == null ? String.Empty : value.ToString());
        }

        /// <summary>
        /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(String message)
        {
            WriteEvent(DateTime.Now, null, String.Empty, message);
        }

        /// <summary>
        /// Writes a category name and a message to the listener you create when you implement the <see cref="TraceListener"/> class.
        /// </summary>
        /// <param name="message">A message to write. </param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void Write(String message, String category)
        {
            if (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, message + Environment.NewLine, null, null, null))
                WriteEvent(DateTime.Now, null, category, message);
        }

        /// <summary>
        /// No action.
        /// </summary>
        protected override void WriteIndent()
        {
            /* Do Nothing */
        }

        /// <summary>
        /// Writes the value of the object's <see cref="Object.ToString"/> method to the listener you create when you implement the <see cref="TraceListener"/> class, followed by a line terminator.
        /// </summary>
        /// <param name="value">An <see cref="Object"/> whose fully qualified class name you want to write.</param>
        public override void WriteLine(Object value)
        {
            if (value != null && (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, null, null, value, null)))
                WriteEvent(DateTime.Now, null, String.Empty, value + Environment.NewLine);
        }

        /// <summary>
        /// Writes a category name and the value of the object's <see cref="Object.ToString"/> method to the listener you create when you implement the <see cref="TraceListener"/> class, followed by a line terminator.
        /// </summary>
        /// <param name="value">An <see cref="T:System.Object"/> whose fully qualified class name you want to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(Object value, String category)
        {
            if (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, category, null, value, null))
                WriteEvent(DateTime.Now, null, category, value == null ? String.Empty : value + Environment.NewLine);
        }

        /// <summary>
        /// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(String message)
        {
            Write(message + Environment.NewLine);
        }

        /// <summary>
        /// Writes a category name and a message to the listener you create when you implement the <see cref="TraceListener"/> class, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write. </param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(String message, String category)
        {
            if (Filter == null || Filter.ShouldTrace(null, String.Empty, TraceEventType.Verbose, 0, message + Environment.NewLine, null, null, null))
                WriteEvent(DateTime.Now, null, category, message);
        }

        /// <summary>
        /// Write out the log4net XML message based on the trace event data provided.
        /// </summary>
        /// <param name="timestamp">The date and time when the trace event occurred.</param>
        /// <param name="level">The trace event level.</param>
        /// <param name="logger">The trace source name or category.</param>
        /// <param name="message">The message to log.</param>
        private void WriteEvent(DateTime timestamp, TraceEventType? level, String logger, String message)
        {
            var xml = new StringBuilder();

            using (var stringWriter = new StringWriter(xml))
            using (var xmlWriter = new XmlTextWriter(stringWriter))
            {
                xmlWriter.WriteStartElement("log4net:event");

                // Write out core trace event attributes.
                xmlWriter.WriteAttributeString("logger", logger);
                xmlWriter.WriteAttributeString("timestamp", timestamp.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzzz"));
                xmlWriter.WriteAttributeString("level", GetLevelFromType(level));
                xmlWriter.WriteAttributeString("thread", GetCurrentThread());
                xmlWriter.WriteAttributeString("domain", domain);

                // Only capture username if required (slow).
                if (captureIdentity)
                    xmlWriter.WriteAttributeString("username", GetCurrentUsername());

                // Write child elements.
                WriteMessage(xmlWriter, message);
                WriteProperties(xmlWriter, machineName);

                // Close the trace event element.
                xmlWriter.WriteEndElement();
            }

            messageWriter.Write(xml.ToString());
        }

        /// <summary>
        /// Write out the trace event log message.
        /// </summary>
        /// <param name="xmlWriter">The underlying XML writer.</param>
        /// <param name="message">The message to write.</param>
        private static void WriteMessage(XmlWriter xmlWriter, String message)
        {
            if (String.IsNullOrWhiteSpace(message)) return;

            xmlWriter.WriteStartElement("log4net:message");
            xmlWriter.WriteString(message);
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Write out the trace event extended properties.
        /// </summary>
        /// <param name="xmlWriter">The underlying XML writer.</param>
        /// <param name="machineName">The machine name.</param>
        private static void WriteProperties(XmlWriter xmlWriter, String machineName)
        {
            // Write out trace event extended properties.
            xmlWriter.WriteStartElement("log4net:properties");

            // Write out the machine name.
            if (!String.IsNullOrWhiteSpace(machineName))
                WriteProperty(xmlWriter, "log4net:HostName", machineName);

            // Include the correlation manager's activity ID if available.
            var activityId = Trace.CorrelationManager.ActivityId;
            if (activityId != Guid.Empty)
                WriteProperty(xmlWriter, "trace:activityId", activityId);

            // Include the correlation manager's logical stack trace frames if available
            var logicalOperationStack = Trace.CorrelationManager.LogicalOperationStack;
            if (logicalOperationStack.Count == 1)
            {
                WriteProperty(xmlWriter, "trace:logicalOperationStack", logicalOperationStack.Peek());
            }
            else if (logicalOperationStack.Count > 1)
            {
                var frames = logicalOperationStack.ToArray();
                var flattened = new StringBuilder(frames[0].ToString());

                for (var i = 1; i < frames.Length; i++)
                {
                    flattened.Append(", ");
                    flattened.Append(frames[i]);
                }

                WriteProperty(xmlWriter, "trace:logicalOperationStack", flattened);
            }

            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Write out the propert name/value pair.
        /// </summary>
        /// <param name="xmlWriter">The underlying XML writer.</param>
        /// <param name="name">The data element name.</param>
        /// <param name="value">The data element value.</param>
        private static void WriteProperty(XmlWriter xmlWriter, String name, Object value)
        {
            xmlWriter.WriteStartElement("log4net:data");
            xmlWriter.WriteAttributeString("name", name);
            xmlWriter.WriteAttributeString("value", value == null ? String.Empty : value.ToString());
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Get the <see cref="String"/> logging level based on the <see cref="TraceEventType"/>.
        /// </summary>
        /// <param name="eventType">The trace event type to capture.</param>
        private static String GetLevelFromType(TraceEventType? eventType)
        {
            if (!eventType.HasValue)
                return "TRACE";

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

        /// <summary>
        /// Get the current username from the current thread principal.
        /// </summary>
        private static String GetCurrentUsername()
        {
            var principal = Thread.CurrentPrincipal;
            var username = principal == null ? String.Empty : principal.Identity.Name;

            return String.IsNullOrWhiteSpace(username) ? (WindowsIdentity.GetCurrent() ?? WindowsIdentity.GetAnonymous()).Name : username;
        }

        /// <summary>
        /// Get the current thread name or managed thread id.
        /// </summary>
        private static String GetCurrentThread()
        {
            return Thread.CurrentThread.Name ?? Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture);
        }
    }
}
