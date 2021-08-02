using System;
using System.Collections.Generic;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IProxyDescriptor
    {
        string ServiceName { get; }
        Type ProxyType { get; }
        string ProviderType { get; }
        List<IProxyDescriptorMetadata> Metadatas { get; }
    }
}
