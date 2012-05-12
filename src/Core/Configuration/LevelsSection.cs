using System;
using System.Configuration;

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

namespace Harvester.Core.Configuration
{
    public class LevelElement : ConfigurationElement
    {
        [ConfigurationProperty("backColor", IsRequired = true, DefaultValue = ConsoleColor.Black)]
        public ConsoleColor BackColor { get { return (ConsoleColor)base["backColor"]; } }

        [ConfigurationProperty("foreColor", IsRequired = true, DefaultValue = ConsoleColor.DarkGray)]
        public ConsoleColor ForeColor { get { return (ConsoleColor)base["foreColor"]; } }

        [ConfigurationProperty("aliases", IsRequired = true, DefaultValue = "")]
        public String Aliases { get { return (String)base["aliases"]; } }
    }

    public class LevelsSection : ConfigurationSection
    {
        [ConfigurationProperty("timestampColor", IsRequired = true)]
        public ConsoleColor TimestampColor { get { return (ConsoleColor)base["timestampColor"]; } }

        [ConfigurationProperty("fatal", IsRequired = true)]
        public LevelElement Fatal { get { return (LevelElement)base["fatal"]; } }

        [ConfigurationProperty("error", IsRequired = true)]
        public LevelElement Error { get { return (LevelElement)base["error"]; } }

        [ConfigurationProperty("warn", IsRequired = true)]
        public LevelElement Warning { get { return (LevelElement)base["warn"]; } }

        [ConfigurationProperty("info", IsRequired = true)]
        public LevelElement Information { get { return (LevelElement)base["info"]; } }

        [ConfigurationProperty("debug", IsRequired = true)]
        public LevelElement Debug { get { return (LevelElement)base["debug"]; } }

        [ConfigurationProperty("trace", IsRequired = true)]
        public LevelElement Trace { get { return (LevelElement)base["trace"]; } }
    }
}
