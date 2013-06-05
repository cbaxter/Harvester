using System;
using Harvester.Core.Messaging;
using Harvester.Core.Messaging.Parsers;
using Harvester.Core.Messaging.Sources.DbWin;
using Harvester.Core.Processes;
using Moq;
using Xunit;

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

namespace Harvester.Core.Tests.Messaging.Parsers.UsingDefaultParser
{
    public class WhenParsingMessage
    {
        private readonly Mock<IRetrieveProcesses> processRetriever = new Mock<IRetrieveProcesses>();
        private readonly IParseMessages messageParser;

        public WhenParsingMessage()
        {
            messageParser = new DefaultParser(processRetriever.Object, new FakeExtendedProperties());
        }

        [Fact]
        public void AlwaysTraceLevel()
        {
            Assert.Equal(SystemEventLevel.Trace, messageParser.Parse(CreateMessage()).Level);
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
        public void SourceFromMessage()
        {
            var message = CreateMessage();

            Assert.Equal(message.Source, messageParser.Parse(message).Source);
        }

        [Fact]
        public void MessageTextFromMessage()
        {
            var message = CreateMessage();

            Assert.Equal(message.Message, messageParser.Parse(message).Message);
        }

        [Fact]
        public void RawMessageFromMessage()
        {
            var message = CreateMessage();

            Assert.Equal(message.Message, messageParser.Parse(message).RawMessage.Value);
        }

        [Fact]
        public void ThreadIsEmpty()
        {
            Assert.Equal(String.Empty, messageParser.Parse(CreateMessage()).Thread);
        }

        [Fact]
        public void UsernameIsEmpty()
        {
            Assert.Equal(String.Empty, messageParser.Parse(CreateMessage()).Username);
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

            return new OutputDebugString("xUnit Source", 123, "xUnit Test Message");
        }
    }
}
