using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System.Reflection;
using Rainbow.Services.ClientProxies.Attributes;

namespace Rainbow.Services.ClientProxies.Extensions
{
    public static class ProxyDescriptorExtensions
    {
        private static IProxyDescriptor CreateProxy(this IServiceProxyBuilder builder, string service, Type type, string providerType)
        {
            var descriptor = ProxyDescriptor.Create(service, type, providerType);
            builder.Services.AddSingleton<IProxyDescriptor>(descriptor);
            return descriptor;
        }

        public static IServiceProxyBuilder AddProxy<T>(this IServiceProxyBuilder builder, string service, string providerType)
        {
            return builder.AddProxy(service, typeof(T), providerType);
        }

        public static IServiceProxyBuilder AddProxy(this IServiceProxyBuilder builder, string service, Type type, string providerType)
        {
            builder.CreateProxy(service, type, providerType);
            return builder;
        }

        public static IServiceProxyBuilder AddProxy(this IServiceProxyBuilder builder, string service, Type type, string providerType, List<IProxyDescriptorMetadata> metadatas)
        {
            var descriptor = builder.CreateProxy(service, type, providerType);
            descriptor.Metadatas.AddRange(metadatas);
            return builder;
        }
    }
}
