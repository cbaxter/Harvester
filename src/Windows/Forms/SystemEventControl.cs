using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Harvester.Core;
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

namespace Harvester.Forms
{
    public partial class SystemEventControl : UserControl
    {
        public SystemEventControl()
        {
            InitializeComponent();

            SetFont(SystemEventProperties.Default.Font);
        }

        public void SetFont(Font font)
        {
            tabControl.Font = font;

            // Message tab
            messageText.Font = font;

            // Details tab
            messageIdHeader.Font = font;
            messageId.Font = font;
            levelHeader.Font = font;
            level.Font = font;
            sourceHeader.Font = font;
            source.Font = font;
            timestampHeader.Font = font;
            timestamp.Font = font;
            processHeader.Font = font;
            process.Font = font;
            threadHeader.Font = font;
            thread.Font = font;
            usernameHeader.Font = font;
            username.Font = font;
            messageHeader.Font = font;
            message.Font = font;

            // Raw tab
            rawText.Font = font;
        }

        public void Clear()
        {
            // Message tab
            messageText.Text = String.Empty;

            // Details tab
            messageId.Text = String.Empty;
            level.Text = String.Empty;
            source.Text = String.Empty;
            timestamp.Text = String.Empty;
            process.Text = String.Empty;
            thread.Text = String.Empty;
            username.Text = String.Empty;
            message.Text = String.Empty;

            // Raw tab
            rawText.Text = String.Empty;
        }

        public void Bind(SystemEvent e)
        {
            Verify.NotNull(e, "e");

            // Message tab
            messageText.Text = e.Message;

            // Details tab
            messageId.Text = e.MessageId.ToString(CultureInfo.CurrentUICulture);
            level.Text = e.Level.ToString();
            source.Text = e.Source;
            timestamp.Text = e.Timestamp.ToString("yyyy-MM-dd HH:mm:ss,fff");
            process.Text = e.ProcessName + " (" + e.ProcessId + ")";
            thread.Text = e.Thread;
            username.Text = e.Username;
            message.Text = e.Message;

            // Raw tab
            rawText.Text = e.RawMessage.Value;
        }
    }
}
