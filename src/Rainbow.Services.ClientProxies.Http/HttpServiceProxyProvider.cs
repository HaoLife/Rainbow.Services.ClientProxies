using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Linq;

namespace Rainbow.Services.ClientProxies.Http
{
    public class HttpServiceProxyProvider : IServiceProxyProvider
    {
        public HttpServiceProxyProvider(IApiActionDescriptorFinder apiActionDescriptorFinder)
        {
            Finder = apiActionDescriptorFinder;
        }

        public IApiActionDescriptorFinder Finder { get; set; }

        public bool CanHandle(string providerName)
        {
            return ClientProxyDefaults.ProviderNames.Contains(providerName.ToUpper());
        }

        public T Create<T>(IProxyDescription descriptor, IServiceEndpoint endpoint)
        {
            return HttpDispatchServiceProxy.CreateProxy<T>(this, descriptor, endpoint);
        }
    }
}
