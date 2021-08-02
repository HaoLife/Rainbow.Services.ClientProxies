using Microsoft.Extensions.DependencyInjection;
using Rainbow.Services.ClientProxies.Abstractions;
using Rainbow.Services.ClientProxies.Http;
using Rainbow.Services.ClientProxies.Http.Metadatas;
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


        public static IServiceProxyBuilder AddHttpProxy<T>(this IServiceProxyBuilder builder, string service, bool? restful = null, string routeTemplate = "")
        {
            var descriptor = ProxyDescriptor.Create<T>(service, ClientProxyDefaults.ProviderName);
            descriptor.Metadatas.Add(new RestfulMetadata(restful));
            descriptor.Metadatas.Add(new RouteTemplateMetadata(routeTemplate));
            builder.Services.AddSingleton<IProxyDescriptor>(descriptor);

            return builder;
        }

    }
}
