using System;

namespace Rainbow.Services.ClientProxies.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class HttpProxyAttribute : Attribute
    {
        public HttpProxyAttribute(string service, string providerName)
        {
            this.Service = service;
            this.ProviderName = providerName;
        }

        public string Service { get; set; }
        public string ProviderName { get; set; }
    }
}
