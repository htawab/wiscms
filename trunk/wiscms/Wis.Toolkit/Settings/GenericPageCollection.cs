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
        /// �����ļ��е��������������ǰ����� URL ����ƥ�䡣
        /// </summary>
        /// <param name="httpRequestUrl">��ǰ����� URL ����Ϣ��</param>
        /// <returns>��������ƥ���򷵻�True��ȫ����ƥ���򷵻�false��</returns>
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
