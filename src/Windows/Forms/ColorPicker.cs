using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
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
    internal partial class ColorPicker : FormBase
    {
        public ColorPicker()
        {
            InitializeComponent();

            primaryBackColorButton.Click += (sender, e) => HandleEvent(() => PickColor(primaryBackColorDisplay));
            primaryFontButton.Click += (sender, e) => HandleEvent(PickFont);

            fatalForeColorButton.Click += (sender, e) => HandleEvent(() => PickColor(fatalForeColorDisplay));
            fatalBackColorButton.Click += (sender, e) => HandleEvent(() => PickColor(fatalBackColorDisplay));

            errorForeColorButton.Click += (sender, e) => HandleEvent(() => PickColor(errorForeColorDisplay));
            errorBackColorButton.Click += (sender, e) => HandleEvent(() => PickColor(errorBackColorDisplay));

            warningForeColorButton.Click += (sender, e) => HandleEvent(() => PickColor(warningForeColorDisplay));
            warningBackColorButton.Click += (sender, e) => HandleEvent(() => PickColor(warningBackColorDisplay));

            informationForeColorButton.Click += (sender, e) => HandleEvent(() => PickColor(informationForeColorDisplay));
            informationBackColorButton.Click += (sender, e) => HandleEvent(() => PickColor(informationBackColorDisplay));

            debugForeColorButton.Click += (sender, e) => HandleEvent(() => PickColor(debugForeColorDisplay));
            debugBackColorButton.Click += (sender, e) => HandleEvent(() => PickColor(debugBackColorDisplay));

            traceForeColorButton.Click += (sender, e) => HandleEvent(() => PickColor(traceForeColorDisplay));
            traceBackColorButton.Click += (sender, e) => HandleEvent(() => PickColor(traceBackColorDisplay));

            restoreDefaults.Click += (sender, e) => HandleEvent(RestoreDefaultColors);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            HandleEvent(() =>
                          {
                              primaryBackColorDisplay.BackColor = SystemEventProperties.Default.PrimaryBackColor;
                              primaryFontDisplay.Font = SystemEventProperties.Default.Font;

                              fatalForeColorDisplay.BackColor = SystemEventProperties.Default.FatalForeColor;
                              fatalBackColorDisplay.BackColor = SystemEventProperties.Default.FatalBackColor;

                              errorForeColorDisplay.BackColor = SystemEventProperties.Default.ErrorForeColor;
                              errorBackColorDisplay.BackColor = SystemEventProperties.Default.ErrorBackColor;

                              warningForeColorDisplay.BackColor = SystemEventProperties.Default.WarningForeColor;
                              warningBackColorDisplay.BackColor = SystemEventProperties.Default.WarningBackColor;

                              informationForeColorDisplay.BackColor = SystemEventProperties.Default.InformationForeColor;
                              informationBackColorDisplay.BackColor = SystemEventProperties.Default.InformationBackColor;

                              debugForeColorDisplay.BackColor = SystemEventProperties.Default.DebugForeColor;
                              debugBackColorDisplay.BackColor = SystemEventProperties.Default.DebugBackColor;

                              traceForeColorDisplay.BackColor = SystemEventProperties.Default.TraceForeColor;
                              traceBackColorDisplay.BackColor = SystemEventProperties.Default.TraceBackColor;
                          });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            HandleEvent(() =>
                          {
                              if (DialogResult != DialogResult.OK)
                                  return;

                              SystemEventProperties.Default.PrimaryBackColor = primaryBackColorDisplay.BackColor;
                              SystemEventProperties.Default.Font = primaryFontDisplay.Font;

                              SystemEventProperties.Default.FatalForeColor = fatalForeColorDisplay.BackColor;
                              SystemEventProperties.Default.FatalBackColor = fatalBackColorDisplay.BackColor;

                              SystemEventProperties.Default.ErrorForeColor = errorForeColorDisplay.BackColor;
                              SystemEventProperties.Default.ErrorBackColor = errorBackColorDisplay.BackColor;

                              SystemEventProperties.Default.WarningForeColor = warningForeColorDisplay.BackColor;
                              SystemEventProperties.Default.WarningBackColor = warningBackColorDisplay.BackColor;

                              SystemEventProperties.Default.InformationForeColor = informationForeColorDisplay.BackColor;
                              SystemEventProperties.Default.InformationBackColor = informationBackColorDisplay.BackColor;

                              SystemEventProperties.Default.DebugForeColor = debugForeColorDisplay.BackColor;
                              SystemEventProperties.Default.DebugBackColor = debugBackColorDisplay.BackColor;

                              SystemEventProperties.Default.TraceForeColor = traceForeColorDisplay.BackColor;
                              SystemEventProperties.Default.TraceBackColor = traceBackColorDisplay.BackColor;

                              SystemEventProperties.Default.Save();
                          });
        }

        private void RestoreDefaultColors()
        {
            primaryBackColorDisplay.BackColor = Color.Black;
            primaryFontDisplay.Font = new Font("Courier New", 8.25F);

            fatalForeColorDisplay.BackColor = Color.White;
            fatalBackColorDisplay.BackColor = Color.Red;

            errorForeColorDisplay.BackColor = Color.Red;
            errorBackColorDisplay.BackColor = Color.Black;

            warningForeColorDisplay.BackColor = Color.Yellow;
            warningBackColorDisplay.BackColor = Color.Black;

            informationForeColorDisplay.BackColor = Color.White;
            informationBackColorDisplay.BackColor = Color.Black;

            debugForeColorDisplay.BackColor = Color.DarkGray;
            debugBackColorDisplay.BackColor = Color.Black;

            traceForeColorDisplay.BackColor = Color.DarkCyan;
            traceBackColorDisplay.BackColor = Color.Black;
        }

        private void PickColor(Control displayControl)
        {
            colorDialog.Color = displayControl.BackColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
                displayControl.BackColor = colorDialog.Color;
        }

        private void PickFont()
        {
            fontDialog.Font = primaryFontDisplay.Font;

            if (fontDialog.ShowDialog(this) == DialogResult.OK)
                primaryFontDisplay.Font = fontDialog.Font;
        }
    }
}
