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
    /// 实现 Rijndael 对称算法加密和解密功能。
    /// </summary>
    public class Rijndael
    {
        #region 私有常量

        /// <summary>
        /// 随机字符串，当密钥字符串长度不够时，用以补足长度
        /// </summary>
        private const string _RandomKey = "4&2$_0+9~7`-6!@|/5#[=3%^:;)8*(}1";

        #endregion 私有常量

        #region 私有变量

        /// <summary>
        /// Rijndael对称加密算法托管对象
        /// </summary>
        private RijndaelManaged _Rijndael = new RijndaelManaged();

        /// <summary>
        /// 密钥字符串
        /// </summary>
        private string _Key = "WisbetWisbet";

        #endregion 私有变量

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Key">密钥字符串</param>
        public Rijndael(string Key)
        {
            _Key = Key;

            // 有效密钥大小由对称算法的具体实现指定，并且在 LegalKeySizes 属性中列出。这个可不能瞎设
            _Rijndael.KeySize = 256;
            _Rijndael.BlockSize = 256;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Rijndael()
        {
            _Rijndael.KeySize = 256;
            _Rijndael.BlockSize = 256;
        }

        #endregion 构造函数

        #region 私有方法

        /// <summary>
        /// 生成密钥
        /// </summary>
        /// <returns>密钥数组</returns>
        private byte[] GetLegalKey()
        {
            string keyTmp = _Key;

            // 一个字符转成1个字节也就是8位，256/8=32，如果使用其他密钥长度，要修改成对应的值
            if (keyTmp.Length < 32)
            {
                keyTmp += _RandomKey.Substring(0, 32 - keyTmp.Length);
            }
            else if (keyTmp.Length > 32)
            {
                keyTmp = keyTmp.Substring(0, 32);
            }

            // 转换字符串到Byte数组
            // 此处建议使用ASCIIEncoding.ASCII而不要使用ASCIIEncoding.Default或者Encoding.GetEncoding( "utf-8" )
            // 来获取编码。否则的话，当密钥字串包含中文，所获得的数组长度可能会不符合密钥长度要求。当然你也可以做
            // 相应的处理来解决这个问题。
            return Encoding.UTF8.GetBytes(keyTmp);

        }

        #endregion 私有方法

        #region 公共方法

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Source">源字符串</param>
        /// <returns>加密字符串</returns>
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
        /// 解密
        /// </summary>
        /// <param name="Source">源加密字符串</param>
        /// <returns>解密字符串</returns>
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

        #endregion 公共方法
    }
}
