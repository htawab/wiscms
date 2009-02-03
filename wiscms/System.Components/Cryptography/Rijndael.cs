//------------------------------------------------------------------------------
// <copyright file="Rijndael.cs" company="WisBet">
//     Copyright (C) WisBet Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Text;
using System.Security.Cryptography;

namespace Wis.Toolkit.Cryptography
{
    /// <summary>
    /// ʵ�� Rijndael �Գ��㷨���ܺͽ��ܹ��ܡ�
    /// </summary>
    public class Rijndael
    {
        #region ˽�г���

        /// <summary>
        /// ����ַ���������Կ�ַ������Ȳ���ʱ�����Բ��㳤��
        /// </summary>
        private const string _RandomKey = "4&2$_0+9~7`-6!@|/5#[=3%^:;)8*(}1";

        #endregion ˽�г���

        #region ˽�б���

        /// <summary>
        /// Rijndael�ԳƼ����㷨�йܶ���
        /// </summary>
        private RijndaelManaged _Rijndael = new RijndaelManaged();

        /// <summary>
        /// ��Կ�ַ���
        /// </summary>
        private string _Key = "WisbetWisbet";

        #endregion ˽�б���

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Key">��Կ�ַ���</param>
        public Rijndael(string Key)
        {
            _Key = Key;

            // ��Ч��Կ��С�ɶԳ��㷨�ľ���ʵ��ָ���������� LegalKeySizes �������г�������ɲ���Ϲ��
            _Rijndael.KeySize = 256;
            _Rijndael.BlockSize = 256;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public Rijndael()
        {
            _Rijndael.KeySize = 256;
            _Rijndael.BlockSize = 256;
        }

        #endregion ���캯��

        #region ˽�з���

        /// <summary>
        /// ������Կ
        /// </summary>
        /// <returns>��Կ����</returns>
        private byte[] GetLegalKey()
        {
            string keyTmp = _Key;

            // һ���ַ�ת��1���ֽ�Ҳ����8λ��256/8=32�����ʹ��������Կ���ȣ�Ҫ�޸ĳɶ�Ӧ��ֵ
            if (keyTmp.Length < 32)
            {
                keyTmp += _RandomKey.Substring(0, 32 - keyTmp.Length);
            }
            else if (keyTmp.Length > 32)
            {
                keyTmp = keyTmp.Substring(0, 32);
            }

            // ת���ַ�����Byte����
            // �˴�����ʹ��ASCIIEncoding.ASCII����Ҫʹ��ASCIIEncoding.Default����Encoding.GetEncoding( "utf-8" )
            // ����ȡ���롣����Ļ�������Կ�ִ��������ģ�����õ����鳤�ȿ��ܻ᲻������Կ����Ҫ�󡣵�Ȼ��Ҳ������
            // ��Ӧ�Ĵ��������������⡣
            return Encoding.UTF8.GetBytes(keyTmp);

        }

        #endregion ˽�з���

        #region ��������

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Source">Դ�ַ���</param>
        /// <returns>�����ַ���</returns>
        public string Encrypt(string Source)
        {
            byte[] bytIn = Encoding.UTF8.GetBytes(Source);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] bytKey = GetLegalKey();

            _Rijndael.Key = bytKey;
            _Rijndael.IV = bytKey;

            ICryptoTransform encrypto = _Rijndael.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            byte[] bytOut = ms.ToArray();

            cs.Clear();
            cs.Close();

            return System.Convert.ToBase64String(bytOut, 0, bytOut.Length);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Source">Դ�����ַ���</param>
        /// <returns>�����ַ���</returns>
        public string Decrypt(string Source)
        {
            byte[] bytIn = System.Convert.FromBase64String(Source);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn);

            byte[] bytKey = GetLegalKey();

            _Rijndael.Key = bytKey;
            _Rijndael.IV = bytKey;

            ICryptoTransform encrypto = _Rijndael.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

            byte[] bytOut = new byte[bytIn.Length];
            cs.Read(bytOut, 0, bytOut.Length);
            cs.Clear();
            cs.Close();

            return Encoding.UTF8.GetString(bytOut).TrimEnd(new char[] { '\0' });
        }

        #endregion ��������
    }
}
