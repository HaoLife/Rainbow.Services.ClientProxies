using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public class ServiceEndpointConfigureOptions : IConfigureOptions<ServiceEndpointOptions>
    {
        private readonly IConfiguration configuration;

        public ServiceEndpointConfigureOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void Configure(ServiceEndpointOptions options)
        {
            options.Endpoints.Clear();
            var childs = this.configuration.GetChildren();

            foreach (var item in childs)
            {
                IServiceEndpoint endpoint
                     = new ServiceEndpoint(item.Key, item.Value);
                options.Endpoints.Add(endpoint.Name, endpoint);
            }

        }
    }
}
