using Rainbow.Services.ClientProxies.Abstractions;
using Rainbow.Services.ClientProxies.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rainbow.Services.ClientProxies.Http.Covenants;

namespace Microsoft.Extensions.Configuration
{
    public static class ServiceProxyBuilderExtensions
    {
        public static IServiceProxyBuilder AddHttp(this IServiceProxyBuilder builder)
        {
            return builder.AddHttp((c) => { });
        }

        public static IServiceProxyBuilder AddHttp(this IServiceProxyBuilder builder, Action<HttpServiceProxyOptions> configure)
        {
            builder.Services.AddSingleton<INameCovenant, ProxyNameCovenant>();
            builder.Services.AddSingleton<INameCovenant, ActionNameCovenant>();

            builder.Services.TryAddSingleton<IProxyActionDescriptorFinder, ProxyActionDescriptorFinder>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IServiceProxyProvider, HttpServiceProxyProvider>());

            builder.Services.Configure(configure);
            return builder;
        }
    }
}
