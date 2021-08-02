using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public class ProxyDescriptor : IProxyDescriptor
    {
        public ProxyDescriptor()
        {

        }
        public ProxyDescriptor(string serviceName, Type proxyType, string providerType)
        {
            this.ServiceName = serviceName;
            this.ProxyType = proxyType;
            this.ProviderType = providerType;
        }
        public string ServiceName { get; set; }

        public Type ProxyType { get; set; }

        public string ProviderType { get; set; }

        public List<IProxyDescriptorMetadata> Metadatas { get; set; } = new List<IProxyDescriptorMetadata>();

        public static ProxyDescriptor Create<T>(string service, string providerType)
        {
            return new ProxyDescriptor(service, typeof(T), providerType);
        }

        public static ProxyDescriptor Create(string service, Type type, string providerType)
        {
            return new ProxyDescriptor(service, type, providerType);
        }

    }
}
