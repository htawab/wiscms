using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Wis.Toolkit
{
    public class Logging
    {
        /// <summary>
        /// 在调试状态下记录错误信息。
        /// </summary>
        /// <param name="o">错误信息，可以是Exception对象</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Debug(object o)
        {
#if !DEBUG
            if (o == null) return;

            string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).ToLower();
            int length = path.LastIndexOf("/bin/"); // 截取掉DLL的文件名，得到DLL当前的路径
            path = path.Substring(0, length).Replace("/", "\\") + "\\Logs\\";
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
            path += DateTime.Today.ToShortDateString() + ".log";

            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                string s;

                // 根据 o 的对象类型，记录更详细的资料。
                if (o.GetType().ToString() == "System.Exception")
                {
                    System.Exception e = o as System.Exception;
                    s = e.Message;
                }
                else
                {
                    s = o.ToString();
                }

                //清除 回车符；将双引号修改为 \"
                s = s.Replace("\r", "").Replace("\n", "").Replace("\"", "\\\"");

                sw.WriteLine(DateTime.Now + "   " + s);
            }
#endif
        }

    }
}
