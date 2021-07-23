using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Rainbow.Services.ClientProxies
{
    public class ServiceEndpointProvider : IServiceEndpointProvider
    {
        private readonly IOptionsMonitor<ServiceEndpointOptions> options;

        public ServiceEndpointProvider(IOptionsMonitor<ServiceEndpointOptions> options)
        {
            this.options = options;
        }

        public bool TryGetEndpoint(string serviceName, out IServiceEndpoint endpoint)
        {
            return options.CurrentValue.Endpoints.TryGetValue(serviceName, out endpoint);
        }

    }
}
