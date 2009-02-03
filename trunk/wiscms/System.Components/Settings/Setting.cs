using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace Wis.Toolkit.Settings
{
    /// <summary>
    /// ���á�
    /// </summary>
    public class Setting
    {
        protected static string Path
        {
            get
            {
                // Get the DLL file Path
                string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).ToLower(CultureInfo.CurrentCulture);
                int length = path.LastIndexOf("/bin/"); // ��ȡ��DLL���ļ������õ�DLL��ǰ��·��
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
        /// �������á�
        /// </summary>
        public static void Save()
        {
            object thisLock = new object();
            lock (thisLock)
            {
                if (Current == null)
                {
                    throw new System.ArgumentNullException("��ȡ������Ϊ��");
                }

                // TODO:ȥ���ļ�ֻ������
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
