using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Resources;

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

namespace Harvester.Core.Tests.Configuration
{
    public class TestConfigFile : IDisposable
    {
        private readonly ExeConfigurationFileMap configurationMap;

        private TestConfigFile(String manifestResourceName)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestResourceName))
            {
                if (stream == null)
                    throw new MissingManifestResourceException(manifestResourceName);

                using (var reader = new StreamReader(stream))
                {
                    var filePath = Path.GetTempFileName();

                    File.WriteAllText(filePath, reader.ReadToEnd());

                    configurationMap = new ExeConfigurationFileMap { ExeConfigFilename = filePath };
                }
            }
        }

        public void Dispose()
        {
            File.Delete(configurationMap.ExeConfigFilename);
        }

        public T GetSection<T>()
            where T : ConfigurationSection
        {
            return ConfigurationManager.OpenMappedExeConfiguration(configurationMap, ConfigurationUserLevel.None)
                                       .GetSection(typeof(T).Name.Replace("Section", "").ToLowerInvariant()) as T;
        }

        public static TestConfigFile Open(MethodBase testMethod)
        {
            var callerNamespace = testMethod.DeclaringType == null ? "Unknown" : testMethod.DeclaringType.Namespace ?? String.Empty;
            var manifestResourceName = String.Format("{0}.{1}.config", callerNamespace, testMethod.Name);

            return new TestConfigFile(manifestResourceName);
        }
    }
}
