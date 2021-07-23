using System;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IProxyDescription
    {
        string ServiceName { get; }
        Type ProxyType { get; }
    }
}
