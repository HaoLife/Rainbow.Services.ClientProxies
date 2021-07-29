using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Rainbow.Services.ClientProxies.Abstractions;
using Rainbow.Services.ClientProxies.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Rainbow.Services.ClientProxies.Exceptions;
using Rainbow.Services.ClientProxies.Extensions;

namespace Rainbow.Services.ClientProxies
{
    public static class ProxyDescriptorAttributesExtensions
    {
        public static IServiceProxyBuilder AddAutoProxy(this IServiceProxyBuilder builder)
        {
            var dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            IEnumerable<Assembly> assemblys = dependencyContext.RuntimeLibraries
                .SelectMany(p => p.GetDefaultAssemblyNames(dependencyContext))
                .Select(Assembly.Load);

            var proxyTypes = assemblys.SelectMany(a => a.GetTypes())
                .Where(a => a.GetCustomAttributes<RemoteServiceAttribute>().Any())
                .ToList();

            var list = new List<IProxyDescriptor>();
            foreach (var item in proxyTypes)
            {
                var attr = item.GetCustomAttribute<RemoteServiceAttribute>();

                builder.AddProxy(attr.ServiceName, item, attr.ProviderType);
            }



            return builder;
        }
    }
}
