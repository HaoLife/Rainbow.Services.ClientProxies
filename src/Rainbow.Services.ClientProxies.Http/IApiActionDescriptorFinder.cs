using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http
{
    public interface IApiActionDescriptorFinder
    {
        ApiActionDescription Find(IProxyDescription description, MethodInfo methodInfo);
    }
}
