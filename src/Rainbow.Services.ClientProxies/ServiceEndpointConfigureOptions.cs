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
                IServiceEndpoint endpoint;
                if (item.Value == null)
                {
                    var url = item.GetSection("url").Value;
                    var auth = item.GetSection("auth")?.Value;
                    endpoint = new ServiceEndpoint(item.Key, url, auth);
                }
                else
                {
                    endpoint = new ServiceEndpoint(item.Key, item.Value);
                }

                options.Endpoints.Add(endpoint.Name, endpoint);

            }

        }
    }
}
