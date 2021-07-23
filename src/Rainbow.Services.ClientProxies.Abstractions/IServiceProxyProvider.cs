using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IServiceProxyProvider
    {
        bool CanHandle(string providerName);

        T Create<T>(IProxyDescription descriptor, IServiceEndpoint endpoint);
    }
}
