using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public class ServiceEndpointOptions
    {
        public SortedDictionary<string, IServiceEndpoint> Endpoints { get; } = new SortedDictionary<string, IServiceEndpoint>();

    }
}
