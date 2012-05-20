using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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
    internal class CheckedButtonRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            var button = e.Item as ToolStripButton;
            if (button != null && button.Checked)
            {
                var size = e.Item.Size;

                using (var borderPen = new Pen(SystemColors.ActiveCaption))
                    e.Graphics.DrawRectangle(borderPen, 0, 0, size.Width - 1, size.Height - 1);
                
                using (var fillBrush = new LinearGradientBrush(new Rectangle(Point.Empty, size), SystemColors.GradientInactiveCaption, SystemColors.GradientActiveCaption, 90F))
                    e.Graphics.FillRectangle(fillBrush, 1, 1, size.Width - 2, size.Height - 2);
            }
            else
            {
                base.OnRenderButtonBackground(e);
            }
        }
    }
}
