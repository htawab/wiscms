//------------------------------------------------------------------------------
// <copyright file="Calendar.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wis.Toolkit.WebControls
{
    /// <summary>
    /// Calendar control is independent of backend.
    /// </summary>
    [ValidationProperty("Text")]
    public class Calendar : WebControl, INamingContainer
    {
        /// <summary>
        /// ���� Load �¼�
        /// </summary>
        /// <param name="e">�����¼����ݵ� <see cref="EventArgs"/> ����</param>
        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
                Text = System.Web.HttpContext.Current.Request[this.UniqueID + "_Calendar"];
        }

        /// <summary>
        /// ���� System.Web.UI.Control.PreRender �¼���
        /// </summary>
        /// <param _Name="e">�����¼����ݵ� System.EventArgs ����</param>
        protected override void OnPreRender(EventArgs e)
        {
            //����ű�û��ע����ע��ű�
            if (!Page.ClientScript.IsClientScriptBlockRegistered("CalendarJavaScript") && base.Enabled)
            {
                //��Ȩ����
                string version = "<!--Copyright (C) Microsoft Corporation.  All rights reserved. -->";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Begin:CalendarJavaScript", version + "\r\n");

                //�õ�Ƕ���js��Դ
                System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(this.GetType().ToString() + ".Images.Calendar.js");
                string script = (new System.IO.StreamReader(stream, Encoding.UTF8)).ReadToEnd();
                //�滻 $DisplayTime$
                string showTime = (this.DisplayTime) ? "1" : "0";
                script = script.Replace("$DisplayTime$", showTime);
                //�滻 $ImagesPath$
                script = script.Replace("$ImagesPath$", this.ImagesPath);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CalendarJavaScript", script);//�����ͻ��˽ű�

                //��Ȩ����
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "End:CalendarJavaScript", "\r\n" + version);
            }
        }

        /// <summary>
        /// ���������ؼ����ݷ��͵��ṩ�� HtmlTextWriter ����
        /// </summary>
        /// <param name="writer">HtmlTextWriter ����</param>
        protected override void Render(HtmlTextWriter writer)
        {
            string onlyread = "readonly";//ֻ��
            if (!this.ReadOnly) onlyread = "";

            writer.Write("<!-- ���ڿؼ���ʼ -->\r\n");
            writer.Write("<span id=\"" + this.UniqueID + "\" style=\"display:inline\">\r\n");
            writer.Write("<table cellspacing=0 cellpadding=0 border=0 style=\"border-width:0px;height:100%;width:130px;border-collapse:collapse;\">\r\n");
            writer.Write("	<tr>\r\n");
            writer.Write("		<td id=\"" + this.UniqueID + "_pageTable1\" align=left>\r\n");
            writer.Write("          <input name=\"" + this.UniqueID + "_Calendar\" type=\"text\" " + onlyread + " id=\"" + this.UniqueID + "_Calendar\" style=\"width:120px;\" value=\"" + this.Text + "\""); 
            
            //����ɫ
            if(!this.BackColor.Equals(System.Drawing.Color.Empty))
                writer.Write(" style=\"background-color:" + this.BackColor.Name + "\"");

            writer.Write("/>\r\n");
            writer.Write("      </td>\r\n");
            writer.Write("      <td align=right>\r\n");
            if (base.Enabled)
                writer.Write("          <img style=\"cursor: hand;\" src=\"" + this.ImagesPath + "Calendar.gif\" onclick=\"PopUpCalendar(this,'" + this.UniqueID + "_Calendar','" + this.DateTimeExpression.ToLower() + "')\">\r\n");
            else
                writer.Write("          <img style=\"cursor: hand;\" src=\"" + this.ImagesPath + "CalendarDisabled.gif\" >\r\n");
            writer.Write("      </td>\r\n");
            writer.Write("	</tr>\r\n");
            writer.Write("</table>\r\n");
            writer.Write("</span>\r\n");
            writer.Write("<!-- ���ڿؼ����� -->\r\n");
            base.Render(writer);
        }

        #region ����
        
        /// <summary>
        /// ͼƬ·��
        /// </summary>
        [Category("����"), Description("ͼƬ·��"), Browsable(true), NotifyParentProperty(true)]
        public string ImagesPath
        {
            get
            {
                if (ViewState["ImagesPath"] == null)
                    return "../images/Calendar/";

                return ViewState["ImagesPath"].ToString();
            }
            set
            {
                ViewState["ImagesPath"] = value;
            }
        }


        /// <summary>
        /// ���ڱ��ʽ
        /// </summary>
        [Category("����"), Description("���ڱ��ʽ"), Browsable(true), NotifyParentProperty(true)]
        protected string DateTimeExpression
        {
            get
            {
                object obj = ViewState["DateTimeExpression"];
                return (obj == null) ? "yyyy-MM-dd" : obj.ToString();
            }
            set
            {
                ViewState["DateTimeExpression"] = value;
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        [Category("����"), Description("����")]
        public string Text
        {
            get
            {
                return ((ViewState["Text"] == null) ? "" : ViewState["Text"].ToString());
            }
            set { ViewState["Text"] = value; }
        }

        public DateTime InputedDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                    return DateTime.Now;
                else
                    return DateTime.Parse(Text);
            }
            set
            {
                this.Text = value.ToString(this.DateTimeExpression);
            }
        }

        /// <summary>
        /// ��ʾʱ��
        /// </summary>
        [Category("����"), Description("��ʾʱ��")]
        public bool DisplayTime
        {
            get
            {
                object obj = ViewState["DisplayTime"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                ViewState["DisplayTime"] = value;
            }
        }


        private bool _ReadOnly;

        /// <summary>
        /// �����ʱ��ֻ��
        /// </summary>
        [Category("����"), Description("�����ʱ��ֻ��")]
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                _ReadOnly = value;
            }
        }
        #endregion
    }
}