using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Linq;

namespace Rainbow.Services.ClientProxies.Http
{
    public class HttpServiceProxyProvider : IServiceProxyProvider
    {
        public HttpServiceProxyProvider(IProxyActionDescriptorFinder apiActionDescriptorFinder)
        {
            Finder = apiActionDescriptorFinder;
        }

        public IProxyActionDescriptorFinder Finder { get; set; }

        public bool CanHandle(string providerName)
        {
            return string.Compare(ClientProxyDefaults.ProviderName, providerName, true) == 0;
        }

        public T Create<T>(IProxyDescriptor descriptor, IServiceEndpoint endpoint)
        {
            return HttpDispatchServiceProxy.CreateProxy<T>(this, descriptor, endpoint);
        }
    }
}
