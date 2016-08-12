using SqlServerAnalyzerCommon.Interfaces;

namespace SqlServerAnalyzerCommon.Configuration
{
    public interface IEnvironmentCreator<in TEnvironmentConfig>
        where TEnvironmentConfig : IEnvironmentConfig
    {
        ISqlEnvironement CreateEnvironement(TEnvironmentConfig config);
    }
}
