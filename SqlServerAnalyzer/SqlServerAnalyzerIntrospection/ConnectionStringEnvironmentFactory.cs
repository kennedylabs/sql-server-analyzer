using SqlServerAnalyzerCommon.Configuration;
using SqlServerAnalyzerCommon.Interfaces;

namespace SqlServerAnalyzerIntrospection
{
    public class ConnectionStringEnvironmentFactory : IEnvironmentCreator<ConnectionStringConfig>
    {
        [Bootstrap]
        public static void RegisterFactory()
        {
            SqlAnalyzation.RegisterEnvironmentCreator(new ConnectionStringEnvironmentFactory());
        }

        public ISqlEnvironement CreateEnvironement(ConnectionStringConfig config)
        {
            return new ConnectionStringSqlEnvironment(config);
        }
    }
}
