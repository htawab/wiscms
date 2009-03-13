using System;
using System.Web;
using System.Configuration;
using System.IO;
using System.Text;

namespace Wis.Toolkit.HttpModules
{
    public class UserAgentLog : IHttpModule
    {

        #region IHttpModule 成员

        public void Dispose()
        {
            //
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(Context_BeginRequest);
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                HttpApplication context = (HttpApplication)sender;
                System.Collections.Specialized.NameValueCollection ServerVariables = HttpContext.Current.Request.ServerVariables;

                // 网页地址
                string absoluteUri = context.Request.Url.AbsoluteUri;
                if (!absoluteUri.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase) &&
                    !absoluteUri.EndsWith(".htm", StringComparison.OrdinalIgnoreCase) &&
                    !absoluteUri.EndsWith(".html", StringComparison.OrdinalIgnoreCase) &&
                    !absoluteUri.EndsWith(".asp", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                string referer = context.Request.ServerVariables["HTTP_REFERER"] == null ? string.Empty : context.Request.ServerVariables["HTTP_REFERER"].ToString();
                string useragent = ServerVariables["HTTP_USER_AGENT"].ToString();
                // ip地址
                string ipAddress = ServerVariables["REMOTE_ADDR"].ToString();

                string cmdText = string.Format("insert into UserAgentLog(AbsoluteUri, Referer, Useragent, IPAddress) values('{0}', '{1}', '{2}', '{3}')",
                    absoluteUri, referer, useragent, ipAddress);

                // 添加统计记录
                ConnectionStringsSection connectionStringsSection = ConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;
                System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection();
                sqlConnection.ConnectionString = connectionStringsSection.ConnectionStrings[1].ConnectionString;
                System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(cmdText, sqlConnection);
                sqlConnection.Open();
                int iExecuteNonQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).ToLower();
                int length = path.LastIndexOf("/bin/"); // 截取掉DLL的文件名，得到DLL当前的路径

                path = path.Substring(0, length).Replace("/", "\\") + "\\Logs\\";
                if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
                path += DateTime.Today.ToShortDateString() + ".log";

                using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
                {
                    //清除 回车符；将双引号修改为 \"
                    string s = ex.Message.Replace("\r", "").Replace("\n", "").Replace("\"", "\\\"");
                    sw.WriteLine(DateTime.Now + "   " + s);
                }
            }
        }

        #endregion
    }
}
