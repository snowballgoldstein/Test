using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp.Abstract;
using TestApp.Concrete;

namespace TestApp.Concrete
{
    public class CleanerService : ICleaneable
    {

        #region Public Methods

        /// <summary>
        /// Cleans html string.
        /// </summary>
        /// <param name="htmlText">Html to clean.</param>
        /// <param name="whiteList">List of allowed tags.</param>
        /// <param name="blackList">List of unallowed tags.</param>
        /// <returns>Cleaned html string.</returns>
        public string CleanHtml(string htmlText, IList<string> whiteList, IList<string> blackList) 
        {
            if (String.IsNullOrEmpty(htmlText))
                return htmlText;

            //First we searching for tags from blackList
            foreach (string s in blackList)
            {
                string t = '<' + s;

                while (htmlText.Contains(t))
                {
                    //Get tag
                    int startIndex = htmlText.IndexOf(t);
                    string subString = htmlText.Substring(startIndex);
                    int subStrLength = subString.Substring(0, subString.IndexOf('>')).Length;

                    string tagSubString = htmlText.Substring(htmlText.LastIndexOf(t), subStrLength);

                    //Check whether it's single tag and deleteing if not
                    if (tagSubString[tagSubString.Length - 1] != '/')
                    {
                        int doubleTagLength = (subString.Substring(0, subString.LastIndexOf("</" + s + ">"))).Length + ("</" + s + ">").Length;
                        string doubleTagString = htmlText.Substring(htmlText.IndexOf(t), doubleTagLength);
                        htmlText = htmlText.Remove(startIndex, doubleTagString.Length);
                    }

                    //Deleteing if single
                    else
                    {
                        htmlText = htmlText.Remove(startIndex, tagSubString.Length + 1);
                    }

                }

                
            }

            //Extract all tags from string that doesn't contain unallowed tags
            List<string> tagList = new List<string>();
            tagList = ExtractFromString(htmlText, "<", ">");//useExtension ? ExtractFromString(htmlText, "<", ">") : htmlText.SplitTags("<", ">");

            for (int i = 0; i < tagList.Count; i++)
            {

                string tagName = String.Empty;
                char[] arr = (tagList[i].TrimEnd(new char[] { '/', ' ' }).TrimStart(new char[] { '/', ' ' })).ToCharArray();

                //Searching tag name
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j] != ' ')
                    {
                        tagName += arr[j];
                    }
                    else
                    {
                        break;
                    }
                }

                //If tag not exist in whiteList - delete it
                if (!whiteList.Contains(tagName))
                {
                    htmlText = htmlText.Replace("<" + tagList[i] + ">", String.Empty);
                }
            }

            return htmlText;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Split string on substrings.
        /// </summary>
        /// <param name="text">String to split.</c>.</param>
        /// <param name="startSymbol">Start substring.</param>
        /// <param name="endSymbol">End substring.</param>
        /// <returns>List of substrings.</returns>
        private static List<string> ExtractFromString(string text, string startSymbol, string endSymbol)
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

        #endregion
    }
}//fff