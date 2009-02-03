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
        /// 格式化让球盘口和大小盘口，负数自动格式化为正数。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FormatHandicap(decimal d)
        {
            return FormatHandicap(d, true);
        }

        /// <summary>
        /// 格式化赔率。
        /// </summary>
        /// <param name="d">赔率</param>
        /// <param name="IsMinusRemoved">是否移除符号</param>
        /// <returns></returns>
        public static string FormatHandicap(decimal d, bool IsMinusRemoved)
        {
            if (d.Equals(decimal.MinValue) || d.Equals(decimal.MaxValue)) return string.Empty;
            if (d.Equals(0 - decimal.MinValue) || d.Equals(0 - decimal.MaxValue)) return string.Empty;
            if (d.ToString().IndexOf('.') < 0)
            {
                if (d < 0)
                    d = 0 - d;
                return d.ToString();
            }
            if (d == 0)
                return "0";
            else
            {
                string dFormated;
                if (d < 0 && IsMinusRemoved)
                {
                    dFormated = (0 - d).ToString();
                    if (dFormated.IndexOf('.') > 0)
                        return dFormated.TrimEnd('0').TrimEnd('.'); // 将负号去掉
                    else
                        return dFormated;
                }
                else
                {
                    dFormated = d.ToString();
                    if (dFormated.IndexOf('.') > 0)
                        return dFormated.TrimEnd('0').TrimEnd('.');
                    else
                        return dFormated;
                }
            }
        }


        /// <summary>
        /// 格式化赔率/让分/大小盘/比分。
        /// </summary>
        /// <param name="bet">赔率/让分/大小盘</param>
        /// <returns></returns>
        public static string FormatBet(string bet)
        {
            
            if (string.IsNullOrEmpty(bet)) return string.Empty;

            if (bet == "0")
                return bet;
            else
                return bet.TrimEnd('0').TrimEnd('.').Replace("-", "");// 去掉负号
        }


        /// <summary>
        /// 格式化赔率。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
