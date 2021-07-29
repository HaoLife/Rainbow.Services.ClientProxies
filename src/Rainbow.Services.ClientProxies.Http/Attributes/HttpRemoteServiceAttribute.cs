using Rainbow.Services.ClientProxies.Attributes;
using Rainbow.Services.ClientProxies.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public class HttpRemoteServiceAttribute : RemoteServiceAttribute
    {
        public HttpRemoteServiceAttribute(string service, string routeTemplate = "")
            : base(service, ClientProxyDefaults.ProviderName)
        {
        }

    }
}
