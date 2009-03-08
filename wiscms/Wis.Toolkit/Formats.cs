//------------------------------------------------------------------------------
// <copyright file="Formats.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Wis.Toolkit
{
    public sealed class Formats
    {
        private Formats() { }

        /// <summary>
        /// 格式化数值为指定的小数位数。
        /// </summary>
        /// <param name="numberDecimal">数值</param>
        /// <param name="numberDecimalDigits">在数值中使用的小数位数。</param>
        /// <returns></returns>
        public static decimal FormatNumber(decimal numberDecimal, int numberDecimalDigits)
        {
            System.Globalization.NumberFormatInfo numberFormatInfo = new System.Globalization.NumberFormatInfo();
            numberFormatInfo.NumberDecimalDigits = numberDecimalDigits;
            return Convert.ToDecimal(numberDecimal, numberFormatInfo);
        }

        /// <summary>
        /// 使用指定的格式格式化 o 的值。
        /// </summary>
        /// <param name="o">待格式化的对象</param>
        /// <param name="format">格式</param>
        /// <returns>返回被格式化的值</returns>
        public static string Format(object o, string format)
        {
            if (o is IFormattable)
                return ((IFormattable)o).ToString(format, null);
            else if (o is DBNull)
                return string.Empty;
            else
                return o.ToString();
        }


        /// <summary>
        /// 使用指定的格式格式化 o 的值。
        /// </summary>
        /// <param name="o">待格式化的对象</param>
        /// <returns>返回被格式化的值</returns>
        public static string Format(object o)
        {
            if (o is DBNull)
                return string.Empty; // &nbsp;
            else
                return o.ToString();
        }


        /// <summary>
        /// 将 Html 格式化为纯文本。
        /// </summary>
        /// <param name="html">HTML文本</param>
        /// <returns>返回纯文本</returns>
        public static string FormatHtmlToText(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            html = System.Web.HttpUtility.HtmlDecode(html);
            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", "").Replace("&nbsp;", " ");
        }

        /// <summary>
        /// 截断字符串 
        /// </summary>
        /// <param name="text">待截断的字符串。</param>
        /// <param name="length">被截断的长度</param>
        /// <returns>返回截断后的字符。</returns>
        public static string TruncateString(object o, int length)
        {
            if (o is DBNull) return string.Empty;
            string text = o.ToString();
            if (string.IsNullOrEmpty(text)) return string.Empty;
            text = text.Trim(); // 去掉前后空格
            if (text.Length <= length) return text;

            int charsLength = 0;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            char[] chars = text.ToCharArray();
            for (int index = 0; index < chars.Length; index++)
            {
                sb.Append(chars[index]);
                int asc = chars[index];
                if (asc < 0 || asc > 127)
                    charsLength += 2;
                else
                    charsLength++;

                if (charsLength == length * 2 || (charsLength + 1) == length * 2)
                {
                    if ((index + 1) == (chars.Length-1)) // 如果只剩下一个汉字或一个字母，就不用追加…
                        sb.Append(chars[index + 1]);
                    else
                        sb.Append("…");

                    break;
                }
            }
            return sb.ToString();
        }
    }
}
