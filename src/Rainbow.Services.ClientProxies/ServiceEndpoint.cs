using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies
{
    public class ServiceEndpoint : IServiceEndpoint
    {
        public ServiceEndpoint(string name, string url, string auth = "defualt")
            : this(name, new Uri(url), auth)
        {

        }

        public ServiceEndpoint(string name, Uri uri, string auth = "defualt")
        {
            this.Uri = uri;
            this.Name = name;
            this.Protocol = uri.Scheme;
            this.Host = uri.Host;
            this.Auth = auth;
            this.Port = uri.Port;
        }
        public Uri Uri { get; set; }
        public string Name { get; set; }

        public string Protocol { get; set; }

        public string Host { get; set; }

        public string Auth { get; set; }

        public int Port { get; set; }
    }
}
