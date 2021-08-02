using Rainbow.Services.ClientProxies.Abstractions;
using Rainbow.Services.ClientProxies.Http.Text;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http
{
    public class ProxyActionDescriptor
    {
        public IProxyDescriptor ProxyDescriptor { get; set; }
        public MethodInfo MethodInfo { get; set; }

        public string HttpMethod { get; set; }

        public string RouteTemplate { get; set; }

        public List<FormatStringToken> RouteTokens { get; set; }
        public Dictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, Type> RouteParameters { get; set; } = new Dictionary<string, Type>();
        public Dictionary<string, Type> BodyParameters { get; set; } = new Dictionary<string, Type>();

        public string InputFormat { get; set; }
        public string OutFormat { get; set; }

        public Type OutType { get; set; }
        public bool IsTask { get; set; }
    }
}
