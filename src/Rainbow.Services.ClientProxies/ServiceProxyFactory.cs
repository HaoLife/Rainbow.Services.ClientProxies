using Rainbow.Services.ClientProxies.Abstractions;
using Rainbow.Services.ClientProxies.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Rainbow.Services.ClientProxies
{
    public class ServiceProxyFactory : IServiceProxyFactory
    {
        private readonly IServiceEndpointProvider serviceEndpointProvider;
        private readonly IProxyDescriptorFinder proxyDescriptorFinder;

        public IEnumerable<IServiceProxyProvider> ProxyProviders { get; }

        public ServiceProxyFactory(IEnumerable<IServiceProxyProvider> providers
            , IServiceEndpointProvider serviceEndpointProvider
            , IProxyDescriptorFinder proxyDescriptorFinder)
        {
            this.ProxyProviders = providers;
            this.serviceEndpointProvider = serviceEndpointProvider;
            this.proxyDescriptorFinder = proxyDescriptorFinder;
        }

        public T Create<T>()
        {
            if (!this.proxyDescriptorFinder.TryFind(typeof(T), out IProxyDescription descriptor))
            {
                throw new NotFoundException($"not found type {typeof(T).FullName} ");
            }

            if (!this.serviceEndpointProvider.TryGetEndpoint(descriptor.ServiceName, out IServiceEndpoint endpoint))
            {
                throw new NotFoundException($"not found service {descriptor.ServiceName}");
            }

            var provider = this.ProxyProviders.FirstOrDefault(a => a.CanHandle(endpoint.Protocol));

            if (provider == null)
            {
                throw new NotFoundException($"not found provider {typeof(T).FullName}, not support {endpoint.Protocol}");
            }

            return provider.Create<T>(descriptor, endpoint);
        }
    }
}
