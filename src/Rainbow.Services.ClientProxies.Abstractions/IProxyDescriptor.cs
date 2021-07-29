using System;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IProxyDescriptor
    {
        string ServiceName { get; }
        Type ProxyType { get; }
        string ProviderType { get; }
    }
}
