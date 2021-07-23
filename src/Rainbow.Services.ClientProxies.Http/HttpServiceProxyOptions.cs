using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http
{
    public class HttpServiceProxyOptions
    {
        public bool Resuful { get; set; } = false;
        public string InputFormat { get; set; } = "JSON";
    }
}
