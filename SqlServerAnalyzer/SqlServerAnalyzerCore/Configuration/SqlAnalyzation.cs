using SqlServerAnalyzerCore.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SqlServerAnalyzerCore.Configuration
{
    public static class SqlAnalyzation {

        private static ConcurrentDictionary<IEnvironmentConfig, ISqlEnvironement>
            _environments = new ConcurrentDictionary<IEnvironmentConfig, ISqlEnvironement>(
                new EnvironmentConfigComparer());

        public static ISqlEnvironement GetOrCreateEnvironment(
            IEnvironmentConfig config, bool forceCreate = false)
        {
            return forceCreate ? _environments.AddOrUpdate(
                config, CreateEnvironment, (c, e) => CreateEnvironment(c)) :
                _environments.GetOrAdd(config, CreateEnvironment);
        }

        public static IEnumerable<ISqlEnvironement> GetAllEnvironements()
        {
            return _environments.Values;
        }

        public static bool TryRemoveEnvironemnt<TEnvironmentConfig>(string configName)
            where TEnvironmentConfig : IEnvironmentConfig
        {
            var config = _environments.Keys.FirstOrDefault(
                c => c.GetType() == typeof(TEnvironmentConfig) && c.Name == configName);

            return TryRemoveEnvironemnt(config);
        }

        public static bool TryRemoveEnvironemnt(IEnvironmentConfig config)
        {
            ISqlEnvironement environment = null;

            if (config != null)
            {
                _environments.TryRemove(config, out environment);

                var disposable = environment as IDisposable;
                if (disposable != null) disposable.Dispose();
            }

            return environment != null;
        }

        public static void RemoveAllEnvironemnts()
        {
            foreach (var config in _environments.Keys)
                TryRemoveEnvironemnt(config);
        }

        private static ISqlEnvironement CreateEnvironment(IEnvironmentConfig config)
        {
            var environmentCreator = config as IEnvironmentCreator;
            return environmentCreator != null ? environmentCreator.CreateEnvironement() : null;
        }

        private class EnvironmentConfigComparer : EqualityComparer<IEnvironmentConfig>
        {
            public override int GetHashCode(IEnvironmentConfig obj)
            {
                return $"{obj.GetType()}{obj.Name}".GetHashCode();
            }

            public override bool Equals(IEnvironmentConfig x, IEnvironmentConfig y)
            {
                return GetHashCode(x) == GetHashCode(y);
            }
        }
    }
}
