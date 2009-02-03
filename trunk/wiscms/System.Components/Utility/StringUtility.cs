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
		/// �ض��ַ������� 
		/// </summary>
		/// <param name="text">���ضϵ��ַ���</param>
		/// <param name="length">���ضϵĳ���</param>
		/// <returns>���ؽضϺ���ַ���</returns>
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
        
        /// <summary>
        /// ��ʽ�������̿ںʹ�С�̿�
        /// ���ָ���ʱ������
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Obsolete("�����Wis.Toolkit.Formats.FormatHandicap")]
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
                //������ȥ��
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
        /// ��ʽ�����ʡ�
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Obsolete("�����Wis.Toolkit.Formats.FormatOdds")]
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
