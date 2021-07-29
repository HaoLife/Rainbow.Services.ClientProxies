using Microsoft.Extensions.DependencyInjection;
using Rainbow.Services.ClientProxies.Abstractions;
using Rainbow.Services.ClientProxies.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public static class HttpProxyDescriptorExtensions
    {

        public static IServiceProxyBuilder AddHttpProxy<T>(this IServiceProxyBuilder builder, string service)
        {
            builder.Services.AddSingleton<IProxyDescriptor>(ProxyDescriptor.Create<T>(service, ClientProxyDefaults.ProviderName));

            return builder;
        }

    }
}
