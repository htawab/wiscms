using System;

namespace Wis.Toolkit.Utility
{
	/// <summary>
	/// StringUtility 的摘要说明。
	/// </summary>
	public class StringUtility
	{
		/// <summary>
		/// 截断字符。
		/// </summary>
		/// <param name="text">被截断的字符。</param>
		/// <param name="length">被截断的长度</param>
		/// <returns>返回截断后的字符。</returns>
		public static string TruncateString(string text, int length)
		{
			if (text.Length > length)
			{
				return text.Substring(0, length) + "…";
			}
			else
			{
				return text;
			}
		}


		/// <summary>
		/// 未部截断字符
		/// </summary>
		/// <param name="text">被截断的字符。</param>
		/// <param name="length">被截断的长度</param>
		/// <returns>返回截断后的字符。</returns>
		public static string LTruncateString(string text, int length)
		{
			if (text.Length > length)
			{
				return text.Substring(length, text.Length);
			}
			else
			{
				return text;
			}
		}
        /// <summary>
        /// Guid装换字符串
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string GuidToString(System.Guid guid)
        {
            return guid.ToString().Trim().ToUpper();
        }

        /// <summary>
        /// 格式化小数点后面的0
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FormatDecimal(decimal text)
        {
            if (text.ToString().IndexOf('.') < 0)
            {
                return text.ToString();
            }
            string temp = text.ToString().TrimEnd('0');
            //if (temp.Length < 3)
            //    temp = temp + "0";
            temp = temp.TrimEnd('.');
            return temp;
        }
    }
}
