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
        private SortedDictionary<Type, IProxyDescriptor> _cache = new SortedDictionary<Type, IProxyDescriptor>();

        public ProxyDescriptorFinder(IEnumerable<IProxyDescriptor> proxyDescriptors)
        {
            this.Initialize(proxyDescriptors);
        }

        private void Initialize(IEnumerable<IProxyDescriptor> proxyDescriptors)
        {

            foreach (var item in proxyDescriptors)
            {
                if (_cache.ContainsKey(item.ProxyType))
                {
                    _cache[item.ProxyType] = item;
                    continue;
                }

                _cache.Add(item.ProxyType, item);
            }


        }

        public IProxyDescriptor Find(Type proxy)
        {
            return _cache.TryGetValue(proxy, out IProxyDescriptor descriptor) ? descriptor : null;
        }

        public bool TryFind(Type proxy, out IProxyDescriptor descriptor)
        {
            return _cache.TryGetValue(proxy, out descriptor);
        }
    }
}
