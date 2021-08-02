using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http
{
    public class HttpInvokeContext
    {
        public ProxyActionDescriptor ProxyActionDescriptor { get; set; }
        public List<InvokeParameter> InvokeParameters { get; set; }

    }
}
