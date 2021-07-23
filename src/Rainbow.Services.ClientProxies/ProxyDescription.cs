using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public class ProxyDescription : IProxyDescription
    {
        public string ServiceName { get; set; }

        public Type ProxyType { get; set; }
    }
}
