using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RemoteProxyAttribute : Attribute
    {
        public RemoteProxyAttribute(string service)
        {
            this.Service = service;
        }

        public string Service { get; set; }
    }
}
