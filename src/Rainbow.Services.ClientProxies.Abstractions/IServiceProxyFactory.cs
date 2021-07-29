using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IServiceProxyFactory
    {
        T Create<T>();
        T Create<T>(string endpoint);
    }
}
