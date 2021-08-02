using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http.Metadatas
{
    public class RestfulMetadata : IProxyDescriptorMetadata
    {
        public bool? IsRestful { get; set; }
        public RestfulMetadata(bool? isRestful)
        {
            this.IsRestful = isRestful;
        }
    }
}
