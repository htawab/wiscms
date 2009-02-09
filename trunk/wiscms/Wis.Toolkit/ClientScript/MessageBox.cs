//------------------------------------------------------------------------------
// <copyright file="MessageBox.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Web.UI;
using System.Text.RegularExpressions;

[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.btnEnter-hover.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.btnEnter-normal.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.icon-info.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.left-corners.png", "image/png")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.left-right.png", "image/png")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.MessageBox.css", "text/css")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.MessageBox.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.right-corners.png", "image/png")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.tool-sprites.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.top-bottom.png", "image/png")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.ClientScript.WebResources.win.png", "image/png")]

namespace Wis.Toolkit.ClientScript
{
    /// <summary>
    /// 消息窗口。
    /// </summary>
    public class MessageBox
    {
        private const string CallScriptKey = "CallMessageBox";
        private const string ScriptKey = "MessageBoxScript";
        private const string ScriptTypeName = "Wis.Toolkit.ClientScript.WebResources.MessageBox.js";
        private const string StyleKey = "MessageBoxCss";
        private const string StyleTypeName = "Wis.Toolkit.ClientScript.WebResources.MessageBox.css";

        private System.Web.UI.Page _Page;
        /// <summary>
        /// 表示从 ASP.NET Web 应用程序的宿主服务器请求的 .aspx 文件（又称为 Web 窗体页）。
        /// </summary>
        public System.Web.UI.Page Page
        {
            get { return _Page; }
            set { _Page = value; }
        }


        private string _Title;
        /// <summary>
        /// 标题。
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }


        private string _Message;
        /// <summary>
        /// 消息。
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }


        public MessageBox(Page page, string title, string message)
        {
            if (page == null)
                throw new System.ArgumentNullException("page");
            if (title == null)
                throw new System.ArgumentNullException("title");
            if (message == null)
                throw new System.ArgumentNullException("message");

            this.Page = page;
            this.Title = title.Replace("'", "\\'");
            this.Message = message.Replace("'", "\\'");
        }

        private string _WindowIcon;
        /// <summary>
        /// 小窗口左上角的 Icon 图片。
        /// </summary>
        public string WindowIcon
        {
            get
            {
                if (string.IsNullOrEmpty(_WindowIcon))
                {
                    _WindowIcon = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.win.png");
                }
                return _WindowIcon;
            }
            set { _WindowIcon = value; }
        }

        /// <summary>
        /// 弹出消息。
        /// </summary>
        public void Call()
        {
            // 输出 Script
            string scriptBlock;
            System.IO.Stream scriptStream = this.GetType().Assembly.GetManifestResourceStream(ScriptTypeName);
            using (System.IO.StreamReader reader = new System.IO.StreamReader(scriptStream, System.Text.Encoding.UTF8))
            {
                scriptBlock = reader.ReadToEnd();
            }

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(ScriptKey))
            {
                scriptBlock = string.Format("\n<script language='JavaScript' type='text/javascript'>\n{0}\n</script>\n", scriptBlock);

                // 处理 Script 中连接的图片资源 \\$WindowIcon\\$
                scriptBlock = Regex.Replace(scriptBlock, "win.png", this.WindowIcon);
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), ScriptKey, scriptBlock);
            }

            // 输出 CSS
            string styleBlock;
            // TODO:线程安全
            System.IO.Stream styleStream = this.GetType().Assembly.GetManifestResourceStream(StyleTypeName);
            using (System.IO.StreamReader reader = new System.IO.StreamReader(styleStream, System.Text.Encoding.UTF8))
            {
                styleBlock = reader.ReadToEnd();
            }

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(StyleKey))
            {
                styleBlock = "\n\n<style type='text/css'>\n" + styleBlock + "\n</style>\n";
                // 处理 CSS 中连接的图片资源
                // LeftCorners.png
                styleBlock = styleBlock.Replace("left-corners.png", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.left-corners.png"));
                styleBlock = styleBlock.Replace("top-bottom.png", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.top-bottom.png"));
                styleBlock = styleBlock.Replace("tool-sprites.gif", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.tool-sprites.gif"));
                // right-corners.png
                styleBlock = styleBlock.Replace("right-corners.png", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.right-corners.png"));
                styleBlock = styleBlock.Replace("left-right.png", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.left-right.png"));

                // TODO:根据MessageBoxType显示其他的图片
                styleBlock = styleBlock.Replace("icon-info.gif", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.icon-info.gif"));

                styleBlock = styleBlock.Replace("btnEnter-normal.gif", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.btnEnter-normal.gif"));
                styleBlock = styleBlock.Replace("btnEnter-hover.gif", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.btnEnter-hover.gif"));

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), StyleKey, styleBlock);
            }

            // 输出标题和消息
            if (!this.Page.ClientScript.IsStartupScriptRegistered(CallScriptKey))
            {
                scriptBlock = string.Format("\n<script language='JavaScript' type='text/javascript'><!--\nMessageBox.init('{0}', '{1}');\n//--></script>\n", this.Title, this.Message);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), CallScriptKey, scriptBlock);
            }
        }
    }

    /// <summary>
    /// 消息类型，包括Mail、Info、Error、Save、Advert和Wait。
    /// </summary>
    public enum MessageBoxType
    {
        /// <summary>
        /// 邮件。
        /// </summary>
        Mail,
        /// <summary>
        /// 信息。
        /// </summary>
        Info,
        /// <summary>
        /// 错误。
        /// </summary>
        Error,
        /// <summary>
        /// 保存。
        /// </summary>
        Save,
        /// <summary>
        /// 提示信息。
        /// </summary>
        Advert,
        /// <summary>
        /// 等待。
        /// </summary>
        Wait
    }
}