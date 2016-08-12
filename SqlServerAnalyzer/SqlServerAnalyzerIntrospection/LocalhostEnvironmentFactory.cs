using SqlServerAnalyzerCommon.Configuration;
using SqlServerAnalyzerCommon.Interfaces;

namespace SqlServerAnalyzerIntrospection
{
    public class LocalhostEnvironmentFactory : IEnvironmentCreator<LocalhostConfig>
    {
        [Bootstrap]
        public static void RegisterFactory()
        {
            SqlAnalyzation.RegisterEnvironmentCreator(new LocalhostEnvironmentFactory());
        }

        public ISqlEnvironement CreateEnvironement(LocalhostConfig config)
        {
            return new LocalhostSqlEnvironment(config);
        }
    }
}
