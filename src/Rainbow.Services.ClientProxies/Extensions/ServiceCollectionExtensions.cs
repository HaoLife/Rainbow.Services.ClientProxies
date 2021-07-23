using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Rainbow.Services.ClientProxies;
using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddClientProxies(this IServiceCollection services, IConfiguration configuration, Action<IServiceProxyBuilder> configure = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.TryAddSingleton<IServiceEndpointProvider, ServiceEndpointProvider>();
            services.TryAddSingleton<IProxyDescriptorFinder, ProxyDescriptorFinder>();
            services.TryAddSingleton<IServiceProxyFactory, ServiceProxyFactory>();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<ServiceEndpointOptions>>(
                new ServiceEndpointConfigureOptions(configuration)));

            var builder = new ServiceProxyBuilder(services);
            configure?.Invoke(builder);

            return services;
        }

    }
}
