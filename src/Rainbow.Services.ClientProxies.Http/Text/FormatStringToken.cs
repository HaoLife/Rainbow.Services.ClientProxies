using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rainbow.Services.ClientProxies.Http.Text
{
    public class FormatStringToken
    {
        public string Text { get; private set; }

        public FormatStringTokenType Type { get; private set; }
        public bool Optional { get; set; }

        public FormatStringToken(string text, FormatStringTokenType type, bool optional)
        {
            Text = text;
            Type = type;
            Optional = optional;
        }
    }
}
