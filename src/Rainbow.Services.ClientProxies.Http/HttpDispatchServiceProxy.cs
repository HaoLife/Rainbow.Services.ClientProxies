using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Net.Http.Headers;

namespace Rainbow.Services.ClientProxies.Http
{
    public class HttpDispatchServiceProxy : DispatchProxy
    {

        private static List<string> _methods = new List<string> { "GET", "POST", "DELETE", "PUT" };

        private HttpServiceProxyProvider _provider;
        private IProxyDescription _description;
        private IServiceEndpoint _serviceEndpoint;

        internal static TService CreateProxy<TService>(HttpServiceProxyProvider provider, IProxyDescription descriptor, IServiceEndpoint endpoint)
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

            //apiActionDescription.RouteParameters


            var httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(apiActionDescription.ApiRouteTemplate);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //requestMessage.Content = new System.Net.Http.StringContent(
            //    System.Text.Json.JsonSerializer.Serialize(new {  }));

            httpClient.SendAsync(requestMessage);

            return null;
        }


    }
}
