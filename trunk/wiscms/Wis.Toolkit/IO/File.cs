//----------------------------------------------------------------
//
// 版本所有 (C) 2004-2005 HeatBet
// 
//----------------------------------------------------------------

using System.IO;
using System.Drawing;
namespace Wis.Toolkit.IO
{
	/// <summary>
	/// File 的摘要说明。
	/// </summary>
	public class File
	{
        public static Bitmap ConvertbyteToImage(byte[] imageBuffer)
        {
            Bitmap result = null;
            using (MemoryStream memoryStream = new MemoryStream(imageBuffer, true))
            {
                memoryStream.Write(imageBuffer, 0, imageBuffer.Length);
                result = new Bitmap(memoryStream);
            }
            return result;
        } 

        // This method accepts two strings the represent two files to 
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the 
        // files are not the same.
        // 比较两个文件是否相同
        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
        }


		/// <summary>
		/// 读取指定文件的内容。
		/// </summary>
		/// <param name="path">文件路径。</param>
		/// <returns>返回指定文件的内容。</returns>
		public static string ReadFile(string path)
		{
			return ReadFile(path, System.Text.Encoding.Default);
		}
		
		
		/// <summary>
		/// 读取指定文件的内容。
		/// </summary>
		/// <param name="path">文件路径。</param>
		/// <param name="encoding">字符编码。</param>
		/// <returns>返回指定文件的内容。</returns>
		public static string ReadFile(string path, System.Text.Encoding encoding)
		{
			string content;
			
			using (System.IO.StreamReader reader = new System.IO.StreamReader(path, encoding))
			{
				content = reader.ReadToEnd();
			}

			return content; 
		}
		
		
	    /// <summary>
		/// 写文件
		/// </summary>
		/// <param name="path">文件路径</param>
		/// <param name="buffer">二进制流</param>
		/// <param name="isAppend">是否追加</param>
		/// <returns></returns>
		public static bool WriteFile(string path, byte[] buffer, bool isAppend)
		{
			System.IO.FileStream fs;

			try
			{
                if (buffer == null) return false;

				if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(path)) == false)
				{
					System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
				}

				if (isAppend)
				{
					fs = new System.IO.FileStream(path, System.IO.FileMode.Append, System.IO.FileAccess.ReadWrite);
				}
				else
				{
					fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
				}

				System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs, System.Text.Encoding.UTF8);
				bw.Write(buffer);
				bw.Close();

				return true;

			}
			catch (System.Exception ex)
			{
				Kernel.ExceptionAppender.Append(ex);
				return false;
			}
		}

		public static bool WriteFile(string path, string value, bool Append)
		{
			if (path == "" || path.Equals(string.Empty) || path == null)
			{
				return false;
			}

			try
			{
                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path, Append, System.Text.Encoding.UTF8);
				streamWriter.Write(value);
				streamWriter.WriteLine();
				streamWriter.Close();
			}
			catch (System.Exception ex)
			{
				Kernel.ExceptionAppender.Append(ex);
				return false;
			}

			return true;
		}

		public static bool WriteFile(string path, string value)
		{
			return WriteFile(path, value, true);
		}

        /// <summary>
        /// Returns the names of files in the specified directory that match the specified searchpatterns.
        /// </summary>
        /// <param name="path">the directory to search.</param>
        /// <param name="searchPatterns">the search strings to match against the names of files in the path deliminated by a ';'. For example:"*.gif;*.xl?;my*.txt"</param>
        /// <returns></returns>
        public static string[] GetFiles(string path, string searchPatterns)
        {
            //declare the return array
            string[] returnArray = new string[0];

            if (System.IO.Directory.Exists(path))
            {
                //loop throuht the givven searchpatterns
                foreach (string ext in searchPatterns.Split(';'))
                {

                    string[] tmpArray;
                    tmpArray = System.IO.Directory.GetFiles(path, ext);
                    if (tmpArray.Length > 0)
                    {
                        string[] newArray = new string[returnArray.Length + tmpArray.Length];
                        returnArray.CopyTo(newArray, 0);
                        tmpArray.CopyTo(newArray, returnArray.Length);
                        returnArray = newArray;
                    }
                }
            }
            return returnArray;
        }

        public static bool Download(string path, string filename)
        {
            //判断文件是否存在
            if (!System.IO.File.Exists(path)) return false;

        	System.Web.HttpContext context = System.Web.HttpContext.Current;//上下文
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(filename));

            //临时目录 + Guid + 扩展名
            // string tempPath = System.IO.Path.Combine( System.IO.Path.GetTempPath(), System.Guid.NewGuid() + System.IO.Path.GetExtension(path));
            
            // System.IO.File.Copy(path, tempPath);

            context.Response.WriteFile(path);
            return true;
        }
		
		
		/// <summary>
		/// 判断指定的扩展名是否在允许的扩展名列表中。
		/// </summary>
		/// <param name="extension">扩展名，比如.xls或xls。</param>
		/// <param name="allowExtensions">允许的扩展名列表，比如xls|doc|pdf|htm|gif|jpg|bmp|zip|rar|txt。</param>
		/// <returns>如果extension在allowExtensions中返回True，否则返回False。</returns>
		public static bool IsInAllowExtensions(string extension, string allowExtensions)
		{
			if (extension == null || extension == "") return false;

			extension = extension.ToLower().Trim();
			if(extension.StartsWith("."))
				extension = extension.Substring(1, extension.Length - 1);
			
			string[] arrExtensions = allowExtensions.Split('|');
			for (int index = 0; index < arrExtensions.Length; index++)
			{
				if (arrExtensions[index].ToLower().Trim() == extension)
					return true;
			}

			return false;
		}
	}
}