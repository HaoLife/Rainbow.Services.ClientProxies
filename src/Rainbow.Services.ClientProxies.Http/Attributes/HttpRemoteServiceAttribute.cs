using Rainbow.Services.ClientProxies.Attributes;
using Rainbow.Services.ClientProxies.Http;
using Rainbow.Services.ClientProxies.Http.Metadatas;
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
            this.Metadatas.Add(new RouteTemplateMetadata(routeTemplate));
        }

        public HttpRemoteServiceAttribute(string service, bool restful, string routeTemplate = "")
            : this(service, routeTemplate)
        {
            this.Metadatas.Add(new RestfulMetadata(restful));
        }
    }
}
