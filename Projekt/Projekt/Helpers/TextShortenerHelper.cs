using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Projekt.Helpers
{
    public static class ShortenedTextHelper
    {
        public static string ShortenText(string pValue, int length = 20, string pDelim = "...")
        {
            string returnedString = string.Empty;
            if(!string.IsNullOrEmpty(pValue) && pValue.Length > length)
            {
                returnedString = string.Concat(pValue.Substring(0, length), pDelim);
            }
            else
            {
                returnedString = pValue;
            }

            return returnedString;
        }
    }
}
