using System;
using System.Collections.Generic;
using System.Text;

namespace Wis.Website
{
    public class BackendPage : System.Web.UI.Page
    {
        private const string CallScriptKey = "CallMessageBox";
        private const string MessageBoxKey = "CallMessageBox";
        /// <summary>
        /// 输出标题和消息。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="message">消息。</param>
        public void MessageBox(string title, string message)
        {
            // 先导入外部资源
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(MessageBoxKey))
            {
                string applicationPath = this.Page.Request.ApplicationPath.TrimStart('/').TrimEnd('/');
                System.Text.StringBuilder sbScriptBlock = new System.Text.StringBuilder();
                sbScriptBlock.Append(string.Format("\n<link href='/{0}/Backend/images/MessageBox/MessageBox.css' rel='stylesheet' type='text/css' />\n", applicationPath));
                sbScriptBlock.Append(string.Format("\n<script src='/{0}/Backend/images/MessageBox/MessageBox.js' language='javascript' type='text/javascript'></script>\n", applicationPath));
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), MessageBoxKey, sbScriptBlock.ToString());
            }

            if (!this.Page.ClientScript.IsStartupScriptRegistered(CallScriptKey))
            {
                string scriptBlock = string.Format("\n<script language='JavaScript' type='text/javascript'><!--\nMessageBox.init('{0}', '{1}');\n//--></script>\n", title, message);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), CallScriptKey, scriptBlock);
            }
        }
    }
}
