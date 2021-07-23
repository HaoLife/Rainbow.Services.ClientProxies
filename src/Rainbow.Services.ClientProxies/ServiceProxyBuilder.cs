using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;

namespace Rainbow.Services.ClientProxies
{
    public class ServiceProxyBuilder : IServiceProxyBuilder
    {
        public IServiceCollection Services { get; }
        public ServiceProxyBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        //public IConfiguration ServiceEndpointConfiguration { get; }
        //public IList<IServiceProxySource> Sources { get; } = new List<IServiceProxySource>();

        //public IServiceProxyFactory Build(IServiceProvider services)
        //{
        //    var providers = new List<IServiceProxyProvider>();
        //    foreach (var source in Sources)
        //    {
        //        var provider = source.Build(services);
        //        providers.Add(provider);
        //    }
        //    return new ServiceProxyFactory(services, providers);
        //}
    }
}
