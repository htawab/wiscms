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
    /// ��Ϣ���ڡ�
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
        /// ��ʾ�� ASP.NET Web Ӧ�ó������������������� .aspx �ļ����ֳ�Ϊ Web ����ҳ����
        /// </summary>
        public System.Web.UI.Page Page
        {
            get { return _Page; }
            set { _Page = value; }
        }


        private string _Title;
        /// <summary>
        /// ���⡣
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }


        private string _Message;
        /// <summary>
        /// ��Ϣ��
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
        /// С�������Ͻǵ� Icon ͼƬ��
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
        /// ������Ϣ��
        /// </summary>
        public void Call()
        {
            // ��� Script
            string scriptBlock;
            System.IO.Stream scriptStream = this.GetType().Assembly.GetManifestResourceStream(ScriptTypeName);
            using (System.IO.StreamReader reader = new System.IO.StreamReader(scriptStream, System.Text.Encoding.UTF8))
            {
                scriptBlock = reader.ReadToEnd();
            }

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(ScriptKey))
            {
                scriptBlock = string.Format("\n<script language='JavaScript' type='text/javascript'>\n{0}\n</script>\n", scriptBlock);

                // ���� Script �����ӵ�ͼƬ��Դ \\$WindowIcon\\$
                scriptBlock = Regex.Replace(scriptBlock, "win.png", this.WindowIcon);
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), ScriptKey, scriptBlock);
            }

            // ��� CSS
            string styleBlock;
            // TODO:�̰߳�ȫ
            System.IO.Stream styleStream = this.GetType().Assembly.GetManifestResourceStream(StyleTypeName);
            using (System.IO.StreamReader reader = new System.IO.StreamReader(styleStream, System.Text.Encoding.UTF8))
            {
                styleBlock = reader.ReadToEnd();
            }

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(StyleKey))
            {
                styleBlock = "\n\n<style type='text/css'>\n" + styleBlock + "\n</style>\n";
                // ���� CSS �����ӵ�ͼƬ��Դ
                // LeftCorners.png
                styleBlock = styleBlock.Replace("left-corners.png", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.left-corners.png"));
                styleBlock = styleBlock.Replace("top-bottom.png", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.top-bottom.png"));
                styleBlock = styleBlock.Replace("tool-sprites.gif", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.tool-sprites.gif"));
                // right-corners.png
                styleBlock = styleBlock.Replace("right-corners.png", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.right-corners.png"));
                styleBlock = styleBlock.Replace("left-right.png", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.left-right.png"));

                // TODO:����MessageBoxType��ʾ������ͼƬ
                styleBlock = styleBlock.Replace("icon-info.gif", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.icon-info.gif"));

                styleBlock = styleBlock.Replace("btnEnter-normal.gif", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.btnEnter-normal.gif"));
                styleBlock = styleBlock.Replace("btnEnter-hover.gif", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.ClientScript.WebResources.btnEnter-hover.gif"));

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), StyleKey, styleBlock);
            }

            // ����������Ϣ
            if (!this.Page.ClientScript.IsStartupScriptRegistered(CallScriptKey))
            {
                scriptBlock = string.Format("\n<script language='JavaScript' type='text/javascript'><!--\nMessageBox.init('{0}', '{1}');\n//--></script>\n", this.Title, this.Message);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), CallScriptKey, scriptBlock);
            }
        }
    }

    /// <summary>
    /// ��Ϣ���ͣ�����Mail��Info��Error��Save��Advert��Wait��
    /// </summary>
    public enum MessageBoxType
    {
        /// <summary>
        /// �ʼ���
        /// </summary>
        Mail,
        /// <summary>
        /// ��Ϣ��
        /// </summary>
        Info,
        /// <summary>
        /// ����
        /// </summary>
        Error,
        /// <summary>
        /// ���档
        /// </summary>
        Save,
        /// <summary>
        /// ��ʾ��Ϣ��
        /// </summary>
        Advert,
        /// <summary>
        /// �ȴ���
        /// </summary>
        Wait
    }
}