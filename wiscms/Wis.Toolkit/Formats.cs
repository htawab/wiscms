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
        /// ��ʽ�������̿ںʹ�С�̿ڣ������Զ���ʽ��Ϊ������
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FormatHandicap(decimal d)
        {
            return FormatHandicap(d, true);
        }

        /// <summary>
        /// ��ʽ�����ʡ�
        /// </summary>
        /// <param name="d">����</param>
        /// <param name="IsMinusRemoved">�Ƿ��Ƴ�����</param>
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
                        return dFormated.TrimEnd('0').TrimEnd('.'); // ������ȥ��
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
        /// ��ʽ������/�÷�/��С��/�ȷ֡�
        /// </summary>
        /// <param name="bet">����/�÷�/��С��</param>
        /// <returns></returns>
        public static string FormatBet(string bet)
        {
            
            if (string.IsNullOrEmpty(bet)) return string.Empty;

            if (bet == "0")
                return bet;
            else
                return bet.TrimEnd('0').TrimEnd('.').Replace("-", "");// ȥ������
        }


        /// <summary>
        /// ��ʽ�����ʡ�
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
