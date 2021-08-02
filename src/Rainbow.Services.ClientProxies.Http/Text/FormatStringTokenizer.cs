using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Services.ClientProxies.Http.Text
{
    internal class FormatStringTokenizer
    {
        public List<FormatStringToken> Tokenize(string format, bool includeBracketsForDynamicValues = false)
        {
            var tokens = new List<FormatStringToken>();

            var currentText = new StringBuilder();
            var inDynamicValue = false;
            var optional = false;

            for (var i = 0; i < format.Length; i++)
            {
                var c = format[i];
                switch (c)
                {
                    case '{':
                        if (inDynamicValue)
                        {
                            throw new FormatException("Incorrect syntax at char " + i + "! format string can not contain nested dynamic value expression!");
                        }

                        inDynamicValue = true;

                        if (currentText.Length > 0)
                        {
                            tokens.Add(new FormatStringToken(currentText.ToString(), FormatStringTokenType.ConstantText, false));
                            currentText.Clear();
                        }

                        break;
                    case '}':
                        if (!inDynamicValue)
                        {
                            throw new FormatException("Incorrect syntax at char " + i + "! These is no opening brackets for the closing bracket }.");
                        }

                        inDynamicValue = false;
                        if (currentText.Length <= 0)
                        {
                            throw new FormatException("Incorrect syntax at char " + i + "! Brackets does not containt any chars.");
                        }

                        var dynamicValue = currentText.ToString();
                        if (includeBracketsForDynamicValues)
                        {
                            dynamicValue = "{" + dynamicValue + "}";
                        }

                        tokens.Add(new FormatStringToken(dynamicValue, FormatStringTokenType.DynamicValue, optional));
                        currentText.Clear();
                        optional = false;

                        break;
                    case '?':
                        optional = true;
                        break;
                    default:
                        currentText.Append(c);
                        break;
                }
            }

            if (inDynamicValue)
            {
                throw new FormatException("There is no closing } char for an opened { char.");
            }

            if (currentText.Length > 0)
            {
                tokens.Add(new FormatStringToken(currentText.ToString(), FormatStringTokenType.ConstantText, false));
            }

            return tokens;
        }
    }
}
