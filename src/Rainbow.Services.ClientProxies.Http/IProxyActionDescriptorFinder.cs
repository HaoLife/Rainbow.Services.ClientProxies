using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http
{
    public interface IProxyActionDescriptorFinder
    {
        ProxyActionDescriptor Find(IProxyDescriptor description, MethodInfo methodInfo);
    }
}
