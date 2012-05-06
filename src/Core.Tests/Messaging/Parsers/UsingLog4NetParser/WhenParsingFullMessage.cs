using System;
using Harvester.Core.Messaging;
using Harvester.Core.Messaging.Parsers;
using Harvester.Core.Messaging.Sources.DbWin;
using Harvester.Core.Processes;
using Moq;
using Xunit;
using Xunit.Extensions;

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

namespace Harvester.Core.Tests.Messaging.Parsers.UsingLog4NetParser
{
    public class WhenParsingFullMessage
    {
        private readonly Mock<IRetrieveProcesses> processRetriever = new Mock<IRetrieveProcesses>();
        private readonly IParseMessages messageParser;

        public WhenParsingFullMessage()
        {
            messageParser = new Log4NetParser(processRetriever.Object, new FakeExtendedProperties());
        }

        [
        Theory,
        InlineData("Fatal", SystemEventLevel.Fatal),
        InlineData("Error", SystemEventLevel.Error),
        InlineData("Warn", SystemEventLevel.Warning),
        InlineData("Info", SystemEventLevel.Information),
        InlineData("Debug", SystemEventLevel.Debug),
        InlineData("Unknown", SystemEventLevel.Trace)
        ]
        public void TraceLevelFromMessage(String level, SystemEventLevel systemEventlevel)
        {
            processRetriever.Setup(mock => mock.GetProcessById(123)).Returns(new UnknownProcess(123));

            var message = new OutputDebugString("xUnit Source", 123, String.Format("<log4net:event level=\"{0}\"></log4net:event>", level));
            var e = messageParser.Parse(message);

            Assert.Equal(systemEventlevel, e.Level);
        }

        [Fact]
        public void ProcessIdFromMessage()
        {
            var message = CreateMessage();

            Assert.Equal(message.ProcessId, messageParser.Parse(message).ProcessId);
        }

        [Fact]
        public void TimestampFromMessage()
        {
            var message = CreateMessage();

            Assert.Equal(message.Timestamp, messageParser.Parse(message).Timestamp);
        }

        [Fact]
        public void SourceFromLoggerAttribute()
        {
            Assert.Equal("Test Logger", messageParser.Parse(CreateMessage()).Source);
        }

        [Fact]
        public void MessageFromMessageNode()
        {
            Assert.Equal("Test Message", messageParser.Parse(CreateMessage()).Message);
        }

        [Fact]
        public void RawMessageIsFormattedXml()
        {
            Assert.Equal(
                "<log4net:event level=\"error\" logger=\"Test Logger\" thread=\"Test Thread\" username=\"Test User\" xmlns:log4net=\"http://logging.apache.org/log4j/\">\r\n  <log4net:message>Test Message</log4net:message>\r\n</log4net:event>",
                messageParser.Parse(CreateMessage()).RawMessage.Value
            );
        }

        [Fact]
        public void ThreadFromThreadAttribute()
        {
            Assert.Equal("Test Thread", messageParser.Parse(CreateMessage()).Thread);
        }

        [Fact]
        public void UsernameFromUsernameAttribute()
        {
            Assert.Equal("Test User", messageParser.Parse(CreateMessage()).Username);
        }

        [Fact]
        public void ProcessNameFromProcess()
        {
            Assert.Equal("xUnit Process", messageParser.Parse(CreateMessage()).ProcessName);
        }

        [Fact]
        public void MessageIdIsSequenceValue()
        {
            Assert.NotEqual((UInt32)0, messageParser.Parse(CreateMessage()).MessageId);
        }

        private IMessage CreateMessage()
        {
            var process = new Mock<IProcess>();

            process.Setup(mock => mock.Id).Returns(123);
            process.Setup(mock => mock.Name).Returns("xUnit Process");
            processRetriever.Setup(mock => mock.GetProcessById(123)).Returns(process.Object);

            return new OutputDebugString("xUnit Source", 123, "<log4net:event level=\"error\" logger=\"Test Logger\" thread=\"Test Thread\" username=\"Test User\"><log4net:message>Test Message</log4net:message></log4net:event>");
        }
    }
}
