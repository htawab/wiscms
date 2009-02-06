using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Wis.Toolkit.IO
{
    public class FileOperate
    {
        /// <summary>
        /// 修改文件名(文件夹)名称
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="oldname">原始名称</param>
        /// <param name="newname">新名称</param>
        /// <param name="type">0为文件夹,1为文件</param>
        /// <returns>成功返回1</returns>
        public static int EidtName(string path, string oldname, string newname, int type)
        {
            int result = 0;
            if (type == 0)
            {
                if (System.IO.Directory.Exists(path + "\\" + oldname))
                {
                    try
                    {
                        System.IO.Directory.Move(path + "\\" + oldname, path + "\\" + newname.Replace(".", ""));
                    }
                    catch
                    {
                        return result;
                    }
                    result = 1;
                }
                else
                {
                    return result;
                }
            }
            else
            {
                if (System.IO.File.Exists(path + "\\" + oldname))
                {
                    try
                    {
                        System.IO.File.Move(path + "\\" + oldname, path + "\\" + newname);
                    }
                    catch
                    {
                        return result;
                    }
                    result = 1;
                }
                else
                {
                    return result;
                }
            }
            return result;
        }



        /// <summary>
        /// 删除文件或文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="filename">名称</param>
        /// <param name="type">0代表文件夹,1代表文件</param>
        /// <returns>返回值</returns>
        public static int Del(string path, string filename, int type)
        {
            int result = 0;
            if (type == 0)
            {
                if (System.IO.Directory.Exists(path + "\\" + filename))
                {
                    try
                    {
                        System.IO.Directory.Delete(path, true);
                    }
                    catch
                    {
                        return result;
                    }
                    result = 1;
                }
                else
                {
                    return result;
                }
            }
            else
            {
                if (System.IO.File.Exists(path + "\\" + filename))
                {
                    try
                    {
                        System.IO.File.Delete(path + "\\" + filename);
                    }
                    catch
                    {
                        return result;
                    }
                    result = 1;
                }
                else
                {
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 添加文件夹

        /// </summary>
        /// <param name="path">当前路径</param>
        /// <param name="filename">文件夹名称</param>
        /// <returns></returns>
        public static int AddDir(string path, string filename)
        {
            int result = 0;
            if (System.IO.Directory.Exists(path + "\\" + filename))
            {
                return result;
            }
            else
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path + "\\" + filename.Replace(".", ""));
                }
                catch
                {
                    return result;
                }
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 获取当前目录的父目录
        /// </summary>
        /// <param name="path">当前目录</param>
        /// <param name="temppath">当前的模板目录</param>
        /// <returns></returns>

        public static string PathPre(string path, string temppath)
        {
            if (path != null)
            {
                int i, j;
                i = path.LastIndexOf(temppath);
                j = path.Length - i;
                path = path.Substring(i, j);
            }
            else
            {
                path = temppath;
            }
            return path;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fileContent">文件内容</param>
        /// <returns></returns>

        public static int saveFile(string path, string fileContent)
        {
            int result = 0;
            if (System.IO.File.Exists(path))
            {
                try
                {
                    StreamWriter Fso = new StreamWriter(path);
                    Fso.WriteLine(fileContent);
                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
                result = 1;
            }
            else
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// 显示文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>

        public static string showFileContet(string path)
        {
            string str_content = "";
            if (System.IO.File.Exists(path))
            {
                try
                {
                    StreamReader Fso = new StreamReader(path);
                    str_content = Fso.ReadToEnd();
                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
            }
            else
            {
                throw new Exception("找不到相应的文件!");
            }
            return str_content;
        }
    }
}
