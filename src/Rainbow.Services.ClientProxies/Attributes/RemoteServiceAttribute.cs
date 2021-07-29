using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RemoteServiceAttribute : Attribute
    {
        public RemoteServiceAttribute(string serviceName, string providerType)
        {
            this.ServiceName = serviceName;
            this.ProviderType = providerType;
        }

        public string ServiceName { get; set; }
        public string ProviderType { get; set; }
    }
}
