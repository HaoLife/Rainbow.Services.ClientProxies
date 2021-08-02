using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Services.ClientProxies.Http.Covenants
{
    public class ActionNameCovenant : INameCovenant
    {
        public void Handle(ProxyActionDescriptor descriptor)
        {
            var method = descriptor.MethodInfo;
            var action = method.Name;

            if (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)
                && action.EndsWith("Async"))
            {
                action = action.Substring(0, action.Length - 5);
            }

            descriptor.RouteValues.Add(ClientProxyDefaults.ActionName, action);
        }
    }
}
