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

namespace Harvester.Core.Messaging.Sources.DbWin
{
    public sealed class OutputDebugString : IMessage
    {
        public const Int32 NullByteSize = 1;
        public const Int32 BufferSize = 4096;
        public const Int32 PreambleSize = sizeof(Int32);
        public const Int32 MaxMessageSize = BufferSize - PreambleSize - NullByteSize;
        private static readonly Byte[] Empty = new Byte[0];

        public DateTime Timestamp { get; private set; }
        public Int32 ProcessId { get; private set; }
        public String Message { get; private set; }

        public OutputDebugString(Byte[] buffer)
        {
            Timestamp = DateTime.Now;
            Message = GetMessage(buffer ?? Empty);
            ProcessId = GetProcessId(buffer ?? Empty);
        }

        public OutputDebugString(Int32 processId, String message)
        {
            Timestamp = DateTime.Now;
            Message = message ?? String.Empty;
            ProcessId = processId;
        }

        private static Int32 GetProcessId(Byte[] buffer)
        {
            return buffer.Length < PreambleSize ? 0 : BitConverter.ToInt32(buffer, 0);
        }

        private static String GetMessage(Byte[] buffer)
        {
            Int32 index = PreambleSize;

            while (index < buffer.Length && buffer[index] != 0)
                index++;

            return index > PreambleSize ? Encoding.UTF8.GetString(buffer, PreambleSize, index - PreambleSize) : String.Empty;
        }
    }
}
