using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Concrete
{
    public static class CustomExtensions
    {
        public static List<string> SplitTags(this String text, string startSymbol, string endSymbol)
        {
            List<string> Matched = new List<string>();
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            while (!exit)
            {
                indexStart = text.IndexOf(startSymbol);
                indexEnd = text.IndexOf(endSymbol);
                if (indexStart != -1 && indexEnd != -1)
                {
                    Matched.Add(text.Substring(indexStart + startSymbol.Length, indexEnd - indexStart - startSymbol.Length));
                    text = text.Substring(indexEnd + endSymbol.Length);
                }
                else
                    exit = true;
            }
            return Matched;
        }
    }
}