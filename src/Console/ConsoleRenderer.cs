using System;
using System.Globalization;
using System.Text;
using Harvester.Core;
using Harvester.Core.Messaging;
using Harvester.Properties;

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

namespace Harvester
{
    internal class ConsoleRenderer : IRenderEvents
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        private Int32 MaxSourceLength { get; set; }
        private Int32 MaxProcessNameLength { get; set; }
        private Int32 MaxProcessIdLength { get; set; }
        private Int32 MaxThreadLength { get; set; }

        //TODO: Batch writes to Console every 100ms?
        //TODO: Move MessageColor.settings into common?
        public void Render(SystemEvent e)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(String.Format("{0:yyyy-MM-dd HH:mm:ss,fff}   ", e.Timestamp));

            SetConsoleBackgroundColor(e);
            SetConsoleForegroundColor(e);

            stringBuilder.Clear();
            stringBuilder.AppendFormat("[{0}]   ", GetProcessId(e));
            stringBuilder.AppendFormat("{0}   ", GetProcessName(e));
            stringBuilder.AppendFormat("[{0}]   ", GetThread(e));
            stringBuilder.AppendFormat("{0}   ", GetSource(e));
            stringBuilder.AppendFormat("[{0}]   ", GetLevel(e));
            stringBuilder.Append(e.Message);

            Console.WriteLine(stringBuilder.ToString());
        }

        private String GetProcessId(SystemEvent logMessage)
        {
            var value = logMessage.ProcessId.ToString(CultureInfo.InvariantCulture);

            MaxProcessIdLength = Math.Max(MaxProcessIdLength, value.Length);

            return value.PadLeft(MaxProcessIdLength, ' ');
        }

        private String GetProcessName(SystemEvent logMessage)
        {
            var value = logMessage.ProcessName ?? String.Empty;

            MaxProcessNameLength = Math.Max(MaxProcessNameLength, value.Length);

            return value.PadRight(MaxProcessNameLength, ' ');
        }

        private String GetThread(SystemEvent logMessage)
        {
            var value = logMessage.Thread ?? String.Empty;

            MaxThreadLength = Math.Max(MaxThreadLength, value.Length);

            return value.PadRight(MaxThreadLength, ' ');
        }

        private String GetSource(SystemEvent logMessage)
        {
            var value = logMessage.Source ?? String.Empty;

            MaxSourceLength = Math.Max(MaxSourceLength, value.Length);

            return value.PadRight(MaxSourceLength, ' ');
        }

        private static Char GetLevel(SystemEvent logMessage)
        {
            switch (logMessage.Level)
            {
                case SystemEventLevel.Fatal: return 'F';
                case SystemEventLevel.Error: return 'E';
                case SystemEventLevel.Warning: return 'W';
                case SystemEventLevel.Information: return 'I';
                case SystemEventLevel.Debug: return 'D';
                default: return 'T';
            }
        }

        private static void SetConsoleBackgroundColor(SystemEvent logMessage)
        {
            switch (logMessage.Level)
            {
                case SystemEventLevel.Fatal: Console.BackgroundColor = MessageColor.Default.FatalBackColor; break;
                case SystemEventLevel.Error: Console.BackgroundColor = MessageColor.Default.ErrorBackColor; break;
                case SystemEventLevel.Warning: Console.BackgroundColor = MessageColor.Default.WarningBackColor; break;
                case SystemEventLevel.Information: Console.BackgroundColor = MessageColor.Default.InformationBackColor; break;
                case SystemEventLevel.Debug: Console.BackgroundColor = MessageColor.Default.DebugBackColor; break;
                default: Console.BackgroundColor = MessageColor.Default.TraceBackColor; break;
            }
        }

        private static void SetConsoleForegroundColor(SystemEvent logMessage)
        {
            switch (logMessage.Level)
            {
                case SystemEventLevel.Fatal: Console.ForegroundColor = MessageColor.Default.FatalForeColor; break;
                case SystemEventLevel.Error: Console.ForegroundColor = MessageColor.Default.ErrorForeColor; break;
                case SystemEventLevel.Warning: Console.ForegroundColor = MessageColor.Default.WarningForeColor; break;
                case SystemEventLevel.Information: Console.ForegroundColor = MessageColor.Default.InformationForeColor; break;
                case SystemEventLevel.Debug: Console.ForegroundColor = MessageColor.Default.DebugForeColor; break;
                default: Console.ForegroundColor = MessageColor.Default.TraceForeColor; break;
            }
        }
    }
}
