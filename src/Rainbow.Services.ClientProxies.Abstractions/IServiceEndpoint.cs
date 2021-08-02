using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Abstractions
{
    public interface IServiceEndpoint
    {
        Uri Uri { get; }
        string Name { get; }
        string Protocol { get; }
        string Host { get; }
        int Port { get; }
    }
}
