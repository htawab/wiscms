using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace Wis.Toolkit.Settings
{
    /// <summary>
    /// 设置。
    /// </summary>
    public class Setting
    {
        protected static string Path
        {
            get
            {
                // Get the DLL file Path
                string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).ToLower(CultureInfo.CurrentCulture);
                int length = path.LastIndexOf("/bin/"); // 截取掉DLL的文件名，得到DLL当前的路径
                path = path.Substring(0, length).Replace("/", "\\") + "\\Setting.config";

                if (!System.IO.File.Exists(path))
                {
                    throw new System.IO.FileNotFoundException(path);
                }

                return path;
            }
        }

        private static Setting current;
        public static Setting Current
        {
            get
            {
                if (current == null)
                {
                    object thisLock = new object();
                    lock (thisLock)
                    {
                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Wis.Toolkit.Settings.Setting));
                        System.IO.FileStream fileStream = new System.IO.FileStream(Path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        object obj = serializer.Deserialize(fileStream);
                        fileStream.Close();

                        current = (Wis.Toolkit.Settings.Setting)obj;
                    }
                }

                return current;
            }
        }


        /// <summary>
        /// 保存配置。
        /// </summary>
        public static void Save()
        {
            object thisLock = new object();
            lock (thisLock)
            {
                if (Current == null)
                {
                    throw new System.ArgumentNullException("获取配置项为空");
                }

                // TODO:去掉文件只读属性
                Wis.Toolkit.XML.XMLSerializer.Serializer(Current, Path);
            }
        }


        private EntryCollection entries;
        public EntryCollection Entries
        {
            get 
            {
                if (entries == null) entries = new EntryCollection();

                return entries;
            }
            set { entries = value; }
        }

        private GenericPageCollection ignorePages;
        [System.Xml.Serialization.XmlArrayItem("IgnorePage")]
        public GenericPageCollection IgnorePages
        {
            get
            {
                if (ignorePages == null) ignorePages = new GenericPageCollection();

                return ignorePages;
            }
            set { ignorePages = value; }
        }

        private GenericPageCollection securePages;
        [System.Xml.Serialization.XmlArrayItem("SecurePage")]
        public GenericPageCollection SecurePages
        {
            get
            {
                if (securePages == null) securePages = new GenericPageCollection();

                return securePages;
            }
            set { securePages = value; }
        }
    }
}
