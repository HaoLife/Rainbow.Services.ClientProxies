using System;
using System.Collections.Generic;
using System.Text;

namespace Rainbow.Services.ClientProxies.Http.Covenants
{
    public interface INameCovenant
    {
        void Handle(ProxyActionDescriptor descriptor);
    }
}
