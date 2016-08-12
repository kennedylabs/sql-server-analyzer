using SqlServerAnalyzerCore.Interfaces;

namespace SqlServerAnalyzerCore.Configuration
{
    public class ConnectionStringConfig : IEnvironmentConfig, IEnvironmentCreator
    {
        public string Name { get; private set; }

        public string ConnectionString { get; private set; }

        public ConnectionStringConfig(string connectionString) :
            this(connectionString, connectionString)
        {
        }

        public ConnectionStringConfig(string name, string connectionString)
        {
            Name = name;
            ConnectionString = connectionString;
        }

        ISqlEnvironement IEnvironmentCreator.CreateEnvironement()
        {
            return null;
        }
    }
}
