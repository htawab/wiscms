using System;

namespace Wis.Toolkit.Utility
{
	/// <summary>
	/// StringUtility ��ժҪ˵����
	/// </summary>
	public class StringUtility
	{
		/// <summary>
		/// �ض��ַ���
		/// </summary>
		/// <param name="text">���ضϵ��ַ���</param>
		/// <param name="length">���ضϵĳ���</param>
		/// <returns>���ؽضϺ���ַ���</returns>
		public static string TruncateString(string text, int length)
		{
			if (text.Length > length)
			{
				return text.Substring(0, length) + "��";
			}
			else
			{
				return text;
			}
		}


		/// <summary>
		/// δ���ض��ַ�
		/// </summary>
		/// <param name="text">���ضϵ��ַ���</param>
		/// <param name="length">���ضϵĳ���</param>
		/// <returns>���ؽضϺ���ַ���</returns>
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
        /// Guidװ���ַ���
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string GuidToString(System.Guid guid)
        {
            return guid.ToString().Trim().ToUpper();
        }

        /// <summary>
        /// ��ʽ��С��������0
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
