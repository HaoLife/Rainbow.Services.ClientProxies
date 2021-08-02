using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Rainbow.Services.ClientProxies.Http.Text;
using Microsoft.Extensions.Options;
using Rainbow.Services.ClientProxies.Http.Attributes;
using Rainbow.Services.ClientProxies.Http.Metadatas;
using Rainbow.Services.ClientProxies.Http.Covenants;
using System.Threading.Tasks;

namespace Rainbow.Services.ClientProxies.Http
{
    public class ProxyActionDescriptorFinder : IProxyActionDescriptorFinder
    {
        public ProxyActionDescriptorFinder(IOptions<HttpServiceProxyOptions> options, IEnumerable<INameCovenant> nameCovenants)
        {
            this.options = options;
            this.nameCovenants = nameCovenants;
        }

        private Dictionary<MethodInfo, ProxyActionDescriptor> _cache = new Dictionary<MethodInfo, ProxyActionDescriptor>();
        private readonly IOptions<HttpServiceProxyOptions> options;
        private readonly IEnumerable<INameCovenant> nameCovenants;

        public ProxyActionDescriptor Find(IProxyDescriptor descriptor, MethodInfo methodInfo)
        {
            if (!_cache.TryGetValue(methodInfo, out ProxyActionDescriptor result))
            {
                result = this.Create(descriptor, methodInfo);
                _cache.Add(methodInfo, result);
            }
            return result;
        }

        public ProxyActionDescriptor Create(IProxyDescriptor descriptor, MethodInfo methodInfo)
        {
            var proxyRouteTemplate = descriptor.Metadatas.OfType<RouteTemplateMetadata>().FirstOrDefault()?.RouteTemplate ?? "";
            var restful = descriptor.Metadatas.OfType<RestfulMetadata>().FirstOrDefault()?.IsRestful;

            var methodAttr = methodInfo.GetCustomAttribute<HttpMethodAttribute>();

            //methodInfo.Name
            var methodHttpMethod = ClientProxyDefaults.HttpMethodPrefixs
                .Where(a => methodInfo.Name.StartsWith(a, StringComparison.OrdinalIgnoreCase))
                .First();

            var httpMethod = methodAttr?.HttpMethod ?? methodHttpMethod ?? ClientProxyDefaults.DefaultHttpMethod;

            restful = restful ?? this.options.Value.Resuful;

            proxyRouteTemplate = !string.IsNullOrEmpty(proxyRouteTemplate) ? proxyRouteTemplate :
                (restful.Value ? ClientProxyDefaults.DefaultRestfulProxyRouteTemplate : ClientProxyDefaults.DefaultProxyRouteTemplate);

            var actionRouteTemplate = !string.IsNullOrEmpty(methodAttr?.Template) ? methodAttr?.Template :
                (restful.Value ? ClientProxyDefaults.DefaultRestfulActionRouteTemplate : ClientProxyDefaults.DefaultActionRouteTemplate);

            proxyRouteTemplate = proxyRouteTemplate.Last() == '/' ? proxyRouteTemplate : $"{proxyRouteTemplate}/";

            var routeTemplate = $"{proxyRouteTemplate}{actionRouteTemplate}";

            var actionTokens = new FormatStringTokenizer()
                .Tokenize(routeTemplate);


            var optionals = actionTokens.Where(a => a.Optional)
                .Select(a => a.Text)
                .ToList();
            var methodParams = methodInfo.GetParameters();

            var routeParams = methodInfo.GetParameters().Select(a => a.Name)
                .Intersect(optionals)
                .ToList();

            //删除可选参数
            actionTokens = actionTokens
                .Where(a => !a.Optional || routeParams.Contains(a.Text))
                .ToList();
            var outherParams = methodParams
                .Where(a => !routeParams.Contains(a.Name))
                .ToList();

            var routeParameters = string.Compare(httpMethod, ClientProxyDefaults.GetMethod, true) == 0
                ? outherParams.ToDictionary(a => a.Name, a => a.ParameterType)
                : new Dictionary<string, Type>();

            var bodyParameters = string.Compare(httpMethod, ClientProxyDefaults.GetMethod, true) == -1
                ? outherParams.ToDictionary(a => a.Name, a => a.ParameterType)
                : new Dictionary<string, Type>();

            var isTask = methodInfo.ReturnType.IsGenericType
                && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
            var outType = isTask ? methodInfo.ReturnType.GetGenericArguments().FirstOrDefault() :
                methodInfo.ReturnType;


            var result = new ProxyActionDescriptor()
            {
                ProxyDescriptor = descriptor,
                MethodInfo = methodInfo,
                HttpMethod = httpMethod,
                RouteTemplate = routeTemplate,
                InputFormat = this.options.Value.InputFormat,
                RouteTokens = actionTokens,
                RouteParameters = routeParameters,
                BodyParameters = bodyParameters,
                IsTask = isTask,
                OutType = outType,
            };

            foreach (var handler in nameCovenants)
            {
                handler.Handle(result);
            }

            return result;
        }

    }
}
