using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Wis.Toolkit.Settings
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class GenericPageCollection : List<GenericPage>
    {
        /// <summary>
        /// 配置文件中的所有配置项跟当前请求的 URL 进行匹配。
        /// </summary>
        /// <param name="httpRequestUrl">当前请求的 URL 的信息。</param>
        /// <returns>有配置项匹配则返回True，全部不匹配则返回false。</returns>
        public bool Match(System.Uri httpRequestUrl)
        {
            if (httpRequestUrl == null)
                throw new System.ArgumentNullException("httpRequestUrl");

            System.Text.RegularExpressions.Regex regex;

            for (int index = 0; index < this.Count; index++)
            {
                regex = new System.Text.RegularExpressions.Regex(this[index].Pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (regex.IsMatch(httpRequestUrl.AbsoluteUri)) return true;
            }

            return false;
        }
    }
}
