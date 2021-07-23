using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IProxyDescriptorFinder
    {
        IProxyDescription Find(Type proxy);
        bool TryFind(Type proxy, out IProxyDescription descriptor);
    }
}
