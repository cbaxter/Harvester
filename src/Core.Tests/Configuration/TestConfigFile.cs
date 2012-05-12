using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

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

        public static TestConfigFile Open()
        {
            var caller = new StackFrame(1, false).GetMethod();
            var callerNamespace = caller.DeclaringType == null ? "Unknown" : caller.DeclaringType.Namespace ?? String.Empty;
            var manifestResourceName = String.Format("{0}.{1}.config", callerNamespace, caller.Name);

            return new TestConfigFile(manifestResourceName);
        }
    }
}
