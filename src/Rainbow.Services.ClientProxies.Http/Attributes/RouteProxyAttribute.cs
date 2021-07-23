using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class RouteProxyAttribute : Attribute
    {
        public RouteProxyAttribute(string template)
        {
            this.Template = template;
        }
        public string Template { get; set; }

    }
}
