using SqlServerAnalyzerCommon.Interfaces;

namespace SqlServerAnalyzerCommon.Configuration
{
    public class LocalhostConfig : IEnvironmentConfig
    {
        public string Name { get; private set; }

        public LocalhostConfig() : this("localhost")
        {
        }

        public LocalhostConfig(string name)
        {
            Name = name;
        }
    }
}
