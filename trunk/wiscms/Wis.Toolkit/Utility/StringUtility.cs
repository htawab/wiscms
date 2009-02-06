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
		/// 截断字符不带… 
		/// </summary>
		/// <param name="text">被截断的字符。</param>
		/// <param name="length">被截断的长度</param>
		/// <returns>返回截断后的字符。</returns>
		public static string NTruncateString(string text, int length)
		{
			if (text.Length > length)
			{
				return text.Substring(0, length);
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
        
        /// <summary>
        /// 格式化让球盘口和大小盘口
        /// 出现负数时有问题
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Obsolete("请改用Wis.Toolkit.Formats.FormatHandicap")]
        public static string FormatHandicap(decimal d)
        {
          
            if (d.Equals(decimal.MinValue)) return string.Empty;
            if (d.Equals(0 - decimal.MinValue)) return string.Empty;
            if (d.ToString().IndexOf('.') < 0)
            {
                return d.ToString();
            }
            if (d == 0)
            {
                return "0";
            }
            else
            {
                //将负号去掉
                if (d < 0)
                {
                    return d.ToString().TrimEnd('0').TrimEnd('.').Replace("-","");
                }
                else
                {
                    return d.ToString().TrimEnd('0').TrimEnd('.');

                }
                //return d.ToString().TrimEnd('0').TrimEnd('.');
            }
        }

        /// <summary>
        /// 格式化赔率。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Obsolete("请改用Wis.Toolkit.Formats.FormatOdds")]
        public static string FormatOdds(decimal d)
        {
            
            if (d.Equals(decimal.MinValue)) return string.Empty;
            if (d.Equals(0 - decimal.MinValue)) return string.Empty;

            if (d.ToString().IndexOf('.') < 0)
            {
                return d.ToString();
            }
            if (d == 0)
                return "0";
            else
                return d.ToString().TrimEnd('0').TrimEnd('.');
        }
    }
}
