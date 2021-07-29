using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Rainbow.Services.ClientProxies.Http.Text;
using Microsoft.Extensions.Options;
using Rainbow.Services.ClientProxies.Http.Attributes;

namespace Rainbow.Services.ClientProxies.Http
{
    public class ProxyActionDescriptorFinder : IProxyActionDescriptorFinder
    {
        public ProxyActionDescriptorFinder(IOptions<HttpServiceProxyOptions> options)
        {
            this.options = options;
        }

        private SortedDictionary<MethodInfo, ProxyActionDescriptor> _cache = new SortedDictionary<MethodInfo, ProxyActionDescriptor>();
        private readonly IOptions<HttpServiceProxyOptions> options;

        public ProxyActionDescriptor Find(IProxyDescriptor descriptor, MethodInfo methodInfo)
        {
            if (!_cache.TryGetValue(methodInfo, out ProxyActionDescriptor result))
            {
                var methodAttr = methodInfo.GetCustomAttribute<HttpMethodAttribute>();
                var routeAttr = methodInfo.GetCustomAttribute<RouteProxyAttribute>();

                //methodInfo.Name
                var methodHttpMethod = ClientProxyDefaults.HttpMethodPrefixs
                    .Where(a => methodInfo.Name.ToUpper().StartsWith(a))
                    .FirstOrDefault();
                var httpMethod = methodAttr == null ? methodHttpMethod : methodAttr.HttpMethod;

                var restful = !string.IsNullOrEmpty(methodHttpMethod) && this.options.Value.Resuful;

                var defaultApiRouteTemplate = ClientProxyDefaults.DefaultRestfulProxyRouteTemplate;
                var defaultActionRouteTemplate = ClientProxyDefaults.DefaultActionRouteTemplate;
                var apiRouteTemplate = routeAttr == null ? defaultApiRouteTemplate : routeAttr.Template;
                var actionRouteTemplate = methodAttr == null ? defaultActionRouteTemplate : methodAttr.Template;


                var actionTokens = new FormatStringTokenizer()
                    .Tokenize(actionRouteTemplate)
                    .Where(a => a.Type == FormatStringTokenType.DynamicValue)
                    .ToList();

                var optionals = actionTokens.Where(a => a.Text.EndsWith("?"))
                    .Select(a => a.Text.Substring(0, a.Text.Length - 1))
                    .ToList();

                var parms = methodInfo.GetParameters().Select(a => a.Name)
                    .Intersect(optionals)
                    .ToList();

                //parms.Where(a => !a.Text.EndsWith("?"))
                //    .ToList();

                result = new ProxyActionDescriptor()
                {
                    MethodInfo = methodInfo,
                    ProxyType = descriptor.ProxyType,
                    ServiceName = descriptor.ServiceName,
                    //ProxyDescription = description,
                    HttpMethod = methodHttpMethod,
                    Restful = restful,
                    ApiRouteTemplate = apiRouteTemplate,
                    ActionRouteTemplate = actionRouteTemplate,
                    InputFormat = this.options.Value.InputFormat,
                };
            }
            return result;
        }

    }
}
