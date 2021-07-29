using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http
{
    public class ProxyActionDescriptor
    {
        //{service}/{proxy}/{method}/{id?}
        public string ServiceName { get; set; }
        public string ProxyName { get; set; }
        public Type ProxyType { get; set; }
        public MethodInfo MethodInfo { get; set; }

        public string HttpMethod { get; set; }
        public string ApiRouteTemplate { get; set; }
        public string ActionRouteTemplate { get; set; }
        public bool Restful { get; set; }

        public List<string> RouteParameters { get; set; }
        public List<string> UrlParameters { get; set; }

        public string InputFormat { get; set; }
        public string OutFormat { get; set; }
    }
}
