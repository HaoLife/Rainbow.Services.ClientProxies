using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class GetProxyAttribute : HttpMethodAttribute
    {
        public GetProxyAttribute()
            : base(System.Net.Http.HttpMethod.Get.Method, "")
        {

        }
        public GetProxyAttribute(string template)
            : base(System.Net.Http.HttpMethod.Get.Method, template)
        {

        }
    }
}
