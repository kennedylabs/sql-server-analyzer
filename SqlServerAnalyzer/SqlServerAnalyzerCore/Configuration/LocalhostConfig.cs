using SqlServerAnalyzerCore.Interfaces;

namespace SqlServerAnalyzerCore.Configuration
{
    public class LocalhostConfig : IEnvironmentConfig, IEnvironmentCreator
    {
        public string Name { get; private set; }

        public LocalhostConfig() : this("localhost")
        {
        }

        public LocalhostConfig(string name)
        {
            Name = name;
        }

        ISqlEnvironement IEnvironmentCreator.CreateEnvironement()
        {
            return null;
        }
    }
}
