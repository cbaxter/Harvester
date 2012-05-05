using System;
using System.Text;

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

namespace Harvester.Core.Messaging.Sources.NamedPipe
{
    public sealed class PipeMessage : IMessage
    {
        public const Int32 PreambleSize = sizeof(Int32);
        private static readonly Byte[] Empty = new Byte[0];

        public DateTime Timestamp { get; private set; }
        public Int32 ProcessId { get; private set; }
        public String Message { get; private set; }
        public String Source { get; private set; }

        public PipeMessage(String source, Byte[] buffer)
            : this(source, GetProcessId(buffer ?? Empty), GetMessage(buffer ?? Empty))
        { }

        public PipeMessage(String source, Int32 processId, String message)
        {
            ProcessId = processId;
            Timestamp = DateTime.Now;
            Source = (source ?? String.Empty).Trim();
            Message = (message ?? String.Empty).Trim();
        }

        private static Int32 GetProcessId(Byte[] buffer)
        {
            return buffer.Length < PreambleSize ? 0 : BitConverter.ToInt32(buffer, 0);
        }

        private static String GetMessage(Byte[] buffer)
        {
            return buffer.Length >= PreambleSize ? Encoding.UTF8.GetString(buffer, PreambleSize, buffer.Length - PreambleSize).Trim() : String.Empty;
        }
    }
}
