using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http.Metadatas
{
    public class RouteTemplateMetadata : IProxyDescriptorMetadata
    {
        public RouteTemplateMetadata(string routeTemplate)
        {
            this.RouteTemplate = routeTemplate;
        }

        public string RouteTemplate { get; set; }
    }
}
