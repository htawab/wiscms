//------------------------------------------------------------------------------
// <copyright file="Validator.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Wis.Toolkit
{
    /// <summary>
    /// У���ࡣ
    /// </summary>
    public sealed class Validator
    {
        private Validator() { }

        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ�������
        /// </summary>
        /// <param name="value">���жϵ�ֵ</param>
        /// <returns></returns>
        [Obsolete("�ѹ��ڣ���ʹ��IsInt���")]
        public static bool IsInteger(string value)
        {
            if (value == null)
                return false;

            try
            {
                //int parsedValue = 
                Int32.Parse(value, CultureInfo.CurrentCulture);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ���Guid
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static bool IsGuid(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            string pattern = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$"; // Replace("{########-####-####-####-############}", "#", "[0-9,A-F]")
            return Regex.IsMatch(value, pattern);

        }


        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ���Guid
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static bool IsEmail(string g)
        {
            if (string.IsNullOrEmpty(g)) return false;
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            //string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            //    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            //    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return Regex.IsMatch(g, pattern);
        }


        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ�������
        /// </summary>
        /// <param name="value">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDateTime(string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }


        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ��� 8 λ�޷���������
        /// </summary>
        /// <param name="value">���жϵ�ֵ</param>
        /// <returns></returns>
        public static bool IsByte(string value)
        {
            byte outByte;
            return byte.TryParse(value, out outByte);
        }


        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ�������
        /// </summary>
        /// <param name="value">���жϵ�ֵ</param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            int outInt;
            return int.TryParse(value, out outInt);
        }

        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ���double��ֵ��
        /// </summary>
        /// <param name="value">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDouble(string value)
        {
            double result;
            return double.TryParse(value, out result);
        }

        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ���Decimal��ֵ��
        /// </summary>
        /// <param name="value">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDecimal(string value)
        {
            Decimal result;
            return Decimal.TryParse(value, out result);
        }
        /// <summary>
        /// ��֤�����ַ����Ƿ��ǺϷ���Boolean��ֵ��
        /// </summary>
        /// <param name="value">�����ַ���</param>
        /// <returns></returns>
        public static bool IsBoolean(string value)
        {
            Boolean result;
            return Boolean.TryParse(value, out result);
        }
    }
}