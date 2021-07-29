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
        public static IServiceProxyBuilder AddProxy<T>(this IServiceProxyBuilder builder, string service, string providerType)
        {
            return builder.AddProxy(service, typeof(T), providerType);
        }

        public static IServiceProxyBuilder AddProxy(this IServiceProxyBuilder builder, string service, Type type, string providerType)
        {
            builder.Services.AddSingleton<IProxyDescriptor>(ProxyDescriptor.Create(service, type, providerType));

            return builder;
        }

    }
}
