using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Abstract
{
    public interface ICleaneable
    {
        /// <summary>
        /// Cleans html string.
        /// </summary>
        /// <param name="htmlText">Html to clean.</param>
        /// <param name="whiteList">List of allowed tags.</param>
        /// <param name="blackList">List of unallowed tags.</param>
        /// <returns>Cleaned html string.</returns>
        string CleanHtml(string htmlText, IList<string> whiteList, IList<string> blackList);
        
    }
}
