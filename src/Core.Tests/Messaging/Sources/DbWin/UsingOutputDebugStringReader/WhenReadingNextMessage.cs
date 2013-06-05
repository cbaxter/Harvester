using System;
using Harvester.Core.Messaging.Sources;
using Harvester.Core.Messaging.Sources.DbWin;
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

namespace Harvester.Core.Tests.Messaging.Sources.DbWin.UsingOutputDebugStringReader
{
    public class WhenReadingNextMessage
    {
        private readonly Mock<MessageBuffer> messageBuffer = new Mock<MessageBuffer>("BufferName");
        private readonly OutputDebugStringReader messageReader;

        public WhenReadingNextMessage()
        {
            messageReader = new OutputDebugStringReader(messageBuffer.Object);
        }

        [Fact]
        public void ReadSingleMessageIfOnlyFragment()
        {
            messageBuffer.Setup(mock => mock.Read()).Returns(new OutputDebugString("Source", 1, "Single Fragment").ToBytes());

            Assert.True(ReadNextMessage(1, "Single Fragment"));

            messageBuffer.Verify(mock => mock.Read(), Times.Once());
        }

        [Fact]
        public void ReadSingleMessageIfFragmentsFromSameProcess()
        {
            messageBuffer.Setup(mock => mock.Read()).Returns(new[]
                                                                 {
                                                                     new OutputDebugString("Source", 1, '^' + String.Empty.PadLeft(OutputDebugString.MaxMessageSize - 2) + '$').ToBytes(),
                                                                     new OutputDebugString("Source", 1, '^' + String.Empty.PadLeft(126) + '$').ToBytes()
                                                                 });

            Assert.True(ReadNextMessage(1, '^' + String.Empty.PadLeft(OutputDebugString.MaxMessageSize - 2) + '$' + '^' + String.Empty.PadLeft(126) + '$'));

            messageBuffer.Verify(mock => mock.Read(), Times.Exactly(2));
        }

        [Fact]
        public void ReadSingleMessageIfFragmentExactlyFillsBuffer()
        {
            messageBuffer.Setup(mock => mock.Read()).Returns(new[]
                                                                 {
                                                                     new OutputDebugString("Source", 1, '^' + String.Empty.PadLeft(OutputDebugString.MaxMessageSize - 2) + '$').ToBytes(),
                                                                     new OutputDebugString("Source", 1, String.Empty).ToBytes()
                                                                 });

            Assert.True(ReadNextMessage(1, '^' + String.Empty.PadLeft(OutputDebugString.MaxMessageSize - 2) + '$'));

            messageBuffer.Verify(mock => mock.Read(), Times.Exactly(2));
        }

        [Fact]
        public void ReadMultipleMessageIfFragmentsFromDifferentProcesses()
        {
            messageBuffer.Setup(mock => mock.Read()).Returns(new[]
                                                                 {
                                                                     new OutputDebugString("Source", 1, "First Fragment").ToBytes(),
                                                                     new OutputDebugString("Source", 2, "Second Fragment").ToBytes()
                                                                 });

            Assert.True(ReadNextMessage(1, "First Fragment"));
            Assert.True(ReadNextMessage(2, "Second Fragment"));

            messageBuffer.Verify(mock => mock.Read(), Times.Exactly(2));
        }

        [Fact]
        public void ReadMultipleMessageIfFragmentsExceedMaxMessageLength()
        {
            messageBuffer.Setup(mock => mock.Read()).Returns(new[]
                                                                 {
                                                                     new OutputDebugString("Source", 1, '^' + String.Empty.PadLeft(OutputDebugString.MaxMessageSize * 15 - 2) + '$').ToBytes(),
                                                                     new OutputDebugString("Source", 1, '^' + String.Empty.PadLeft(OutputDebugString.MaxMessageSize - 2) + '$').ToBytes(),
                                                                     new OutputDebugString("Source", 1, '^' + String.Empty.PadLeft(126) + '$').ToBytes()
                                                                 });

            Assert.True(ReadNextMessage(1, '^' + String.Empty.PadLeft(OutputDebugString.MaxMessageSize * 15 - 2) + '$' + '^' + String.Empty.PadLeft(OutputDebugString.MaxMessageSize - 2) + '$'));
            Assert.True(ReadNextMessage(1, '^' + String.Empty.PadLeft(126) + '$'));

            messageBuffer.Verify(mock => mock.Read(), Times.Exactly(3));
        }

        private Boolean ReadNextMessage(Int32 processId, String message)
        {
            var result = messageReader.ReadNext();
            var current = messageReader.Current;

            Assert.Equal(processId, current.ProcessId);
            Assert.Equal(message, current.Message);

            return result;
        }
    }
}
