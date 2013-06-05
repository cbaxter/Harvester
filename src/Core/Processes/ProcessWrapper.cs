using System;
using System.Diagnostics;

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

namespace Harvester.Core.Processes
{
    internal class ProcessWrapper : IProcess
    {
        private readonly String processName;
        private readonly Int32 processId;
        private DateTime? exitTime;
        private Process process;

        public Int32 Id { get { return processId; } }
        public String Name { get { return processName; } }
        public DateTime? ExitTime { get { return exitTime; } }
        public Boolean HasExited { get { return exitTime.HasValue; } }

        public ProcessWrapper(Process process)
        {
            Verify.NotNull(process, "process");

            this.process = process;
            this.processId = process.Id;
            this.processName = process.ProcessName;
            this.process.Exited += OnProcessExited;
            this.process.EnableRaisingEvents = true;
        }

        private void OnProcessExited(Object sender, EventArgs e)
        {
            exitTime = DateTime.Now;

            process.Exited -= OnProcessExited;
            process.Dispose();
            process = null;
        }
    }
}
