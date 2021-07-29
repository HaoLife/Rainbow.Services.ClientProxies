using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IProxyDescriptorFinder
    {
        IProxyDescriptor Find(Type proxy);
        bool TryFind(Type proxy, out IProxyDescriptor descriptor);
    }
}
