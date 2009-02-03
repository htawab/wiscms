

using System.Text;
using System.Runtime.InteropServices;

namespace Wis.Toolkit
{
    /// <summary>
    /// 繁简体转换。
    /// </summary>
    public sealed class TraditionalToSimplified
    {
        const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;
        [DllImport("kernel32.dll", EntryPoint = "LCMapStringA")]
        public static extern int LCMapString(int Locale, int dwMapFlags, byte[] lpSrcStr, int cchSrc, byte[] lpDestStr, int cchDest);

        /// <summary>
        /// 繁体转换简体
        /// </summary>
        /// <param name="str">简体</param>
        /// <param name="c" >转换类型</param>
        /// <returns>繁体</returns>
        public static string ToSimplified(string s)
        {
            Encoding gb2312 = Encoding.GetEncoding(936);
            byte[] source = gb2312.GetBytes(s);
            byte[] dest = new byte[source.Length];
            LCMapString(0x0804, LCMAP_SIMPLIFIED_CHINESE, source, -1, dest, source.Length);
            // 後的繁体转换，聯 -> 联，鴨 -> 鸭/拼音码放出来录入
            return gb2312.GetString(dest).Replace("後", "后").Replace("聯", "联").Replace("鴨", "鸭").Replace("盃", "杯");
        }


        /// <summary>
        /// 繁简体转换
        /// </summary>
        /// <param name="str">简体</param>
        /// <param name="c" >转换类型</param>
        /// <returns>繁体</returns>
        public static string ToTraditional(string s)
        {
            Encoding gb2312 = Encoding.GetEncoding(936);
            byte[] source = gb2312.GetBytes(s);
            byte[] dest = new byte[source.Length];
            LCMapString(0x0804, LCMAP_TRADITIONAL_CHINESE, source, -1, dest, source.Length);
            return gb2312.GetString(dest);
        }
    }
}