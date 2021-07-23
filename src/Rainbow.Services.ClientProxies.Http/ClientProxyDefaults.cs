using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http
{
    public class ClientProxyDefaults
    {
        public static readonly string[] ProviderNames = new string[] { "HTTP", "HTTPS" };

        public static readonly string[] HttpMethodPrefixs = new string[] { "GET", "POST", "DELETE", "PUT" };

        public static readonly string DefaultApiRouteTemplate = "{service}/{proxy}";
        public static readonly string DefaultActionRouteTemplate = "{method?}/{id?}";
    }
}
