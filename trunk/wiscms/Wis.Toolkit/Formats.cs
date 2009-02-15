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
        /// ��ʽ����ֵΪָ����С��λ����
        /// </summary>
        /// <param name="numberDecimal">��ֵ</param>
        /// <param name="numberDecimalDigits">����ֵ��ʹ�õ�С��λ����</param>
        /// <returns></returns>
        public static decimal FormatNumber(decimal numberDecimal, int numberDecimalDigits)
        {
            System.Globalization.NumberFormatInfo numberFormatInfo = new System.Globalization.NumberFormatInfo();
            numberFormatInfo.NumberDecimalDigits = numberDecimalDigits;
            return Convert.ToDecimal(numberDecimal, numberFormatInfo);
        }

        /// <summary>
        /// ʹ��ָ���ĸ�ʽ��ʽ�� o ��ֵ��
        /// </summary>
        /// <param name="o">����ʽ���Ķ���</param>
        /// <param name="format">��ʽ</param>
        /// <returns>���ر���ʽ����ֵ</returns>
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
        /// ʹ��ָ���ĸ�ʽ��ʽ�� o ��ֵ��
        /// </summary>
        /// <param name="o">����ʽ���Ķ���</param>
        /// <returns>���ر���ʽ����ֵ</returns>
        public static string Format(object o)
        {
            if (o is DBNull)
                return string.Empty; // &nbsp;
            else
                return o.ToString();
        }


        /// <summary>
        /// �� Html ��ʽ��Ϊ���ı���
        /// </summary>
        /// <param name="html">HTML�ı�</param>
        /// <returns>���ش��ı�</returns>
        public static string FormatHtmlToText(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            html = System.Web.HttpUtility.HtmlDecode(html);
            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", "").Replace("&nbsp;", " ");
        }

        /// <summary>
        /// �ض��ַ����������� 
        /// </summary>
        /// <param name="text">���ضϵ��ַ���</param>
        /// <param name="length">���ضϵĳ���</param>
        /// <returns>���ؽضϺ���ַ���</returns>
        public static string TruncateString(string text, int length)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            if (text.Length > length)
                return text.Substring(0, length) + "...";
            else
                return text;
        }
    }
}
