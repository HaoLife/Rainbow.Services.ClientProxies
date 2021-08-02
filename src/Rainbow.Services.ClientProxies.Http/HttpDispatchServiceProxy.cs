using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Linq.Expressions;

namespace Rainbow.Services.ClientProxies.Http
{
    public class HttpDispatchServiceProxy : DispatchProxy
    {

        private static List<string> _methods = new List<string> { "GET", "POST", "DELETE", "PUT" };

        private HttpServiceProxyProvider _provider;
        private IProxyDescriptor _description;
        private IServiceEndpoint _serviceEndpoint;

        internal static TService CreateProxy<TService>(HttpServiceProxyProvider provider, IProxyDescriptor descriptor, IServiceEndpoint endpoint)
        {
            TService proxy = DispatchProxy.Create<TService, HttpDispatchServiceProxy>();
            if (proxy == null)
            {
                throw new Exception("创建代理服务异常");
            }

            var channelProxy = (HttpDispatchServiceProxy)(object)proxy;
            channelProxy._description = descriptor;
            channelProxy._provider = provider;
            channelProxy._serviceEndpoint = endpoint;

            return proxy;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            //通过方法获得接口的描述信息

            var apiActionDescription = this._provider.Finder.Find(_description, targetMethod);

            var parms = targetMethod.GetParameters()
                .Select((p, i) => new InvokeParameter(p.Name, args[i]))
                .ToList();

            var context = new HttpInvokeContext()
            {
                ProxyActionDescriptor = apiActionDescription,
                InvokeParameters = parms,
            };

            StringBuilder pathBuilder = new StringBuilder();
            foreach (var item in context.ProxyActionDescriptor.RouteTokens)
            {
                if (item.Type == Text.FormatStringTokenType.ConstantText)
                {
                    pathBuilder.Append(item.Text);
                    continue;
                }

                var defaultRouteValue = context.ProxyActionDescriptor.RouteValues.FirstOrDefault(a => a.Key == item.Text);
                if (!string.IsNullOrEmpty(defaultRouteValue.Key))
                {
                    pathBuilder.Append(defaultRouteValue.Value);
                    continue;
                }

                var routeValue = context.InvokeParameters.FirstOrDefault(a => a.Name == item.Text);
                if (routeValue == null)
                {
                    throw new Exception($"路由参数错误: {item.Text}");
                }

                pathBuilder.Append(routeValue.Value.ToString());
            }
            var url = $"{ _serviceEndpoint.Protocol}://{ _serviceEndpoint.Host}:{ _serviceEndpoint.Port}/{pathBuilder.ToString()}";

            var urlBuilder = new UriBuilder(url);

            if (context.ProxyActionDescriptor.RouteParameters.Any())
            {
                var kv = context.ProxyActionDescriptor.RouteParameters
                    .Select(a => new KeyValuePair<string, string>(a.Key, context.InvokeParameters.FirstOrDefault(b => b.Name == a.Key)?.Value?.ToString()))
                    .ToList();

                var content = new FormUrlEncodedContent(kv);
                urlBuilder.Query = content.ReadAsStringAsync().GetAwaiter().GetResult();

            }

            var httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = urlBuilder.Uri;
            //requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Method = new HttpMethod(context.ProxyActionDescriptor.HttpMethod);

            var responseMessage = httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();

            var contentString = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,                                   //格式化json字符串
                AllowTrailingCommas = true,                             //可以结尾有逗号
                                                                        //IgnoreNullValues = true,                              //可以有空值,转换json去除空值属性
                IgnoreReadOnlyProperties = true,                        //忽略只读属性
                PropertyNameCaseInsensitive = true,                     //忽略大小写
                                                                        //PropertyNamingPolicy = JsonNamingPolicy.CamelCase     //命名方式是默认还是CamelCase
            };

            var result = JsonSerializer.Deserialize(contentString, context.ProxyActionDescriptor.OutType, options);

            if (context.ProxyActionDescriptor.IsTask)
            {
                //暂不支持Task类型
                throw new Exception("暂不支持Task类型");
                //return Task.FromResult(result);
            }

            return result;
        }



        //public Task Create(object data, Type genericType)
        //{
        //    var method = typeof(Task).GetMethod(nameof(Task.FromResult));
        //    var p = Expression.Parameter(genericType);
        //    var call = Expression.Call(method, p);

        //    //var a = Expression.Lambda<Action<object, Type>>(call,);

        //    //Expression.Call()

        //    Expression.Parameter(genericType);

        //    var t = typeof(Type).MakeGenericType(genericType);

        //    return new Task<Type>(() => genericType);
        //}


    }
}
