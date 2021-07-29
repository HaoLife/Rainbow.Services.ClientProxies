using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public static class RemoteServiceConfigureExtensions
    {
        public static IServiceProxyBuilder AddServiceConfigure(this IServiceProxyBuilder builder, IConfiguration configuration)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<ServiceEndpointOptions>>(
                new ServiceEndpointConfigureOptions(configuration)));

            return builder;
        }

    }
}
