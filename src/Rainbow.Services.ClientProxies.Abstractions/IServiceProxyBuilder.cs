using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IServiceProxyBuilder
    {
        //IList<IServiceProxySource> Sources { get; }
        //IServiceProxyFactory Build(IServiceProvider services);

        IServiceCollection Services { get; }
    }
}
