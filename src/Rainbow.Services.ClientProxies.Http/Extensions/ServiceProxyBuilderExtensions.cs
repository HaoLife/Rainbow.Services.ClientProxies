using Rainbow.Services.ClientProxies.Abstractions;
using Rainbow.Services.ClientProxies.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.Configuration
{
    public static class ServiceProxyBuilderExtensions
    {
        public static IServiceProxyBuilder AddHttpProxy(this IServiceProxyBuilder builder)
        {
            return builder.AddHttpProxy((c) => { });
        }

        public static IServiceProxyBuilder AddHttpProxy(this IServiceProxyBuilder builder, Action<HttpServiceProxyOptions> configure)
        {
            builder.Services.TryAddSingleton<IApiActionDescriptorFinder, ApiActionDescriptorFinder>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IServiceProxyProvider, HttpServiceProxyProvider>());

            builder.Services.Configure(configure);
            return builder;
        }
    }
}
