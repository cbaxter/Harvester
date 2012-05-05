using System;
using System.Linq;
using System.Text;
using Harvester.Core.Messaging;
using Harvester.Core.Messaging.Sources.DbWin;
using Harvester.Core.Messaging.Sources.NamedPipe;
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

namespace Harvester.Core.Tests.Messaging.Sources.DbWin.UsingOutputDebugString
{
    public class WhenCreatingFromBuffer
    {
        private readonly IMessage message = new OutputDebugString("Source", BitConverter.GetBytes(123).Concat(Encoding.UTF8.GetBytes("My Message")).ToArray());
        private readonly DateTime now = DateTime.Now;

        [Fact]
        public void TimestampIsApproximatelyNow()
        {
            Assert.InRange(message.Timestamp, now.Subtract(TimeSpan.FromMilliseconds(10)), now.Add(TimeSpan.FromMilliseconds(10)));
        }

        [Fact]
        public void ProcessIdIsSet()
        {
            Assert.Equal(123, message.ProcessId);
        }

        [Fact]
        public void MessageIsSet()
        {
            Assert.Equal("My Message", message.Message);
        }

        [Fact]
        public void TolerateMissingNullTerminatingByte()
        {
            Assert.Equal("A", new PipeMessage("Source", new Byte[] { 123, 0, 0, 0, 65 }).Message);
        }

        [Theory, InlineData(null), InlineData(new Byte[0]), InlineData(new Byte[] { 123 }), InlineData(new Byte[] { 123, 0 }), InlineData(new Byte[] { 123, 0, 0 })]
        public void ProcessIdIsZeroIfLessThanFourBytesInBuffer(Byte[] buffer)
        {
            Assert.Equal(0, new PipeMessage("Source", buffer).ProcessId);
        }

        [Theory, InlineData(null), InlineData(new Byte[0]), InlineData(new Byte[] { 123, 0, 0, 0 }), InlineData(new Byte[] { 123, 0, 0, 0 })]
        public void MessageIsEmptyIfOnlyPreambleInBuffer(Byte[] buffer)
        {
            Assert.Equal(String.Empty, new PipeMessage("Source", buffer).Message);
        }

        [Theory,
        InlineData("M\0", new Byte[] { 123, 0, 0, 0, 77, 0 }),
        InlineData("My\0", new Byte[] { 123, 0, 0, 0, 77, 121, 0 }),
        InlineData("My\0 Message\0", new Byte[] { 123, 0, 0, 0, 77, 121, 0, 32, 77, 101, 115, 115, 97, 103, 101, 0 })]
        public void MessageAllowsForNullByteIfBufferHasMoreThanFourBytes(String expected, Byte[] buffer)
        {
            Assert.Equal(expected, new PipeMessage("Source", buffer).Message);
        }
    }
}
