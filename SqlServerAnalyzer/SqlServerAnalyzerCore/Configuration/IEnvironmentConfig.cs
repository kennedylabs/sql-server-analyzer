using SqlServerAnalyzerCore.Interfaces;
using System.Collections.Generic;

namespace SqlServerAnalyzerCore.Configuration
{
    public interface IEnvironmentConfig
    {
        string Name { get; }
    }

    internal interface IEnvironmentCreator
    {
        ISqlEnvironement CreateEnvironement();
    }
}
