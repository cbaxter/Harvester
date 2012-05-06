using System;
using Harvester.Core.Messaging;
using Harvester.Core.Messaging.Sources.DbWin;
using Moq;
using Xunit;

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

namespace Harvester.Core.Tests.Messaging.Sources.DbWin.UsingOutputDebugStringListener
{
    public class WhenDisposingListener : IDisposable
    {
        private readonly Mock<IProcessMessages> messageProcessor = new Mock<IProcessMessages>();
        private readonly MessageListener messageListener;

        public WhenDisposingListener()
        {
            var guid = Guid.NewGuid();
            var configuration = new FakeListenerConfiguration
                                    {
                                        Name = String.Format(@"Local\HRVSTR_{0}", guid),
                                        Mutex = "Harvester: " + guid
                                    };

            messageListener = new OutputDebugStringListener(messageProcessor.Object, configuration);
            messageListener.Start();
        }

        public void Dispose()
        {
            messageListener.Dispose();
        }

        [Fact]
        public void CanSafelyCallDisposeMultipleTimes()
        {
            messageListener.Dispose();
            messageListener.Dispose();
        }
    }
}
