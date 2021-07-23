using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IServiceEndpointProvider
    {
        bool TryGetEndpoint(string serviceName, out IServiceEndpoint endpoint);

    }
}
