
namespace SqlServerAnalyzerCommon.Configuration
{
    public class ConnectionStringConfig : IEnvironmentConfig
    {
        public string Name { get; private set; }

        public string ServerInstance { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ConnectionStringConfig(string serverInstance) : this(serverInstance, serverInstance)
        {
        }

        public ConnectionStringConfig(string serverInstance, string userName, string password) :
            this(serverInstance, serverInstance, userName, password)
        {
        }

        public ConnectionStringConfig(string name, string serverInstance)
        {
            Name = name;
            ServerInstance = serverInstance;
        }

        public ConnectionStringConfig(
            string name, string serverInstance, string userName, string password)
        {
            Name = name;
            ServerInstance = serverInstance;
            UserName = userName;
            Password = password;
        }
    }
}
