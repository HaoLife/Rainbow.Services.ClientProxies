using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class HttpMethodAttribute : Attribute
    {
        public HttpMethodAttribute(string httpMethod, string template)
        {
            this.HttpMethod = httpMethod;
            this.Template = template;
        }
        public string HttpMethod { get; set; }
        public string Template { get; set; }
    }
}
