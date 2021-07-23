using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IServiceProxySource
    {
        IServiceProxyProvider Build(IServiceProvider services);
    }
}
