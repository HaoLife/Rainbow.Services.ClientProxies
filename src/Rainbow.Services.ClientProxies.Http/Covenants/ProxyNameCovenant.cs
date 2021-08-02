using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http.Covenants
{
    public class ProxyNameCovenant : INameCovenant
    {
        public void Handle(ProxyActionDescriptor descriptor)
        {
            var type = descriptor.ProxyDescriptor.ProxyType;
            var proxy = type.Name;
            if (type.IsInterface && proxy.StartsWith("I"))
            {
                proxy = proxy.Substring(1);
            }

            if (proxy.EndsWith("Service"))
            {
                proxy = proxy.Substring(0, proxy.Length - 7);
            }
            descriptor.RouteValues.Add(ClientProxyDefaults.ProxyName, proxy);
        }
    }
}
