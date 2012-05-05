using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
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

namespace Harvester.Core.Messaging.Parsers
{
    internal abstract class XmlMessageParser : IParseMessages
    {
        private readonly XmlNamespaceManager xmlNamespaceManager;
        private readonly XmlParserContext xmlParserContext;
        private readonly IRetrieveProcesses processes;

        protected XmlMessageParser(IRetrieveProcesses processRetriever, String namespacePrefix, String namespaceUri)
        {
            Verify.NotNull(processRetriever, "processRetriever");
            Verify.NotWhitespace(namespacePrefix, "namespacePrefix");
            Verify.NotWhitespace(namespaceUri, "namespaceUri");

            processes = processRetriever;
            xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
            xmlNamespaceManager.AddNamespace(namespacePrefix, namespaceUri);
            xmlParserContext = new XmlParserContext(null, xmlNamespaceManager, null, XmlSpace.None);
        }

        public abstract Boolean CanParseMessage(String message);

        public SystemEvent Parse(IMessage message)
        {
            Verify.NotNull(message, "message");

            var document = new XmlDocument();
            var process = processes.GetProcessById(message.ProcessId);

            using (var reader = new XmlTextReader(message.Message ?? String.Empty, XmlNodeType.Element, xmlParserContext))
                document.Load(reader);
            
            return new SystemEvent
                       {
                           Level = GetLevel(document),
                           ProcessName = process.Name,
                           ProcessId = process.Id,
                           Timestamp = message.Timestamp,
                           Thread = GetThread(document),
                           Username = GetUsername(document),
                           Source = GetSource(document),
                           Message = GetMessage(document).Trim(),
                           RawMessage = new Lazy<String>(() => FormatRawMessage(document)),
                       };
        }

        protected abstract SystemEventLevel GetLevel(XmlDocument document);

        protected abstract String GetSource(XmlDocument document);

        protected abstract String GetThread(XmlDocument document);

        protected abstract String GetUsername(XmlDocument document);

        protected abstract String GetMessage(XmlDocument document);

        protected String QuerySingleValue(XmlNode node, String xPath)
        {
            XmlNode result = node.SelectSingleNode(xPath, xmlNamespaceManager);

            return result == null ? String.Empty : result.Value ?? String.Empty;
        }

        protected String FormatRawMessage(XmlDocument document)
        {
            var sb = new StringBuilder();

            using (var stringWriter = new StringWriter(sb))
            using (var xmlTextWriter = new XmlTextWriter(stringWriter))
            {
                xmlTextWriter.Formatting = Formatting.Indented;

                document.WriteTo(xmlTextWriter);
            }

            return sb.ToString();
        }
    }
}
