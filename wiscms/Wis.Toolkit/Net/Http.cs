//------------------------------------------------------------------------------
// <copyright file="Http.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.IO;

namespace Wis.Toolkit.Net
{
    public class Http
    {
        public static string DownloadUri(string url)
        {
            // 打开网络连接
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);

                // 向服务器请求，获得服务器回应数据流
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("GBK"));
                string r = sr.ReadToEnd();
                sr.Close();
                response.Close();

                return r;
            }
            catch (System.Exception ex)
            {
                Logging.Debug(string.Format("下载 {0} 失败，发生异常信息：{1}", url, ex.ToString()));
                return string.Empty;
            }
        }
    }
}
