using Microsoft.Extensions.DependencyModel;
using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public class ProxyDescriptorFinder : IProxyDescriptorFinder
    {
        private SortedDictionary<Type, IProxyDescription> _cache = new SortedDictionary<Type, IProxyDescription>();


        public ProxyDescriptorFinder()
        {
            this.Load();
        }

        private void Load()
        {
            var dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            IEnumerable<Assembly> assemblys = dependencyContext.RuntimeLibraries
                .SelectMany(p => p.GetDefaultAssemblyNames(dependencyContext))
                .Select(Assembly.Load);

            var proxyTypes = assemblys.SelectMany(a => a.GetTypes())
                .Where(a => a.GetCustomAttributes<RemoteProxyAttribute>().Any())
                .ToList();

            var list = new List<IProxyDescription>();
            foreach (var item in proxyTypes)
            {
                var attr = item.GetCustomAttributes<RemoteProxyAttribute>()
                    .FirstOrDefault();
                var desc = new ProxyDescription()
                {
                    ProxyType = item,
                    ServiceName = attr.Service,
                };
                _cache.Add(desc.ProxyType, desc);
            }

        }

        public IProxyDescription Find(Type proxy)
        {
            return _cache.TryGetValue(proxy, out IProxyDescription descriptor) ? descriptor : null;
        }

        public bool TryFind(Type proxy, out IProxyDescription descriptor)
        {
            return _cache.TryGetValue(proxy, out descriptor);
        }
    }
}
