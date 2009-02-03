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
        /// 引发 Load 事件
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="EventArgs"/> 对象</param>
        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
                Text = System.Web.HttpContext.Current.Request[this.UniqueID + "_Calendar"];
        }

        /// <summary>
        /// 引发 System.Web.UI.Control.PreRender 事件。
        /// </summary>
        /// <param _Name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnPreRender(EventArgs e)
        {
            //如果脚本没有注册则注册脚本
            if (!Page.ClientScript.IsClientScriptBlockRegistered("CalendarJavaScript") && base.Enabled)
            {
                //版权申明
                string version = "<!--Copyright (C) Microsoft Corporation.  All rights reserved. -->";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Begin:CalendarJavaScript", version + "\r\n");

                //得到嵌入的js资源
                System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(this.GetType().ToString() + ".Images.Calendar.js");
                string script = (new System.IO.StreamReader(stream, Encoding.UTF8)).ReadToEnd();
                //替换 $DisplayTime$
                string showTime = (this.DisplayTime) ? "1" : "0";
                script = script.Replace("$DisplayTime$", showTime);
                //替换 $ImagesPath$
                script = script.Replace("$ImagesPath$", this.ImagesPath);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CalendarJavaScript", script);//发出客户端脚本

                //版权申明
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "End:CalendarJavaScript", "\r\n" + version);
            }
        }

        /// <summary>
        /// 将服务器控件内容发送到提供的 HtmlTextWriter 对象。
        /// </summary>
        /// <param name="writer">HtmlTextWriter 对象</param>
        protected override void Render(HtmlTextWriter writer)
        {
            string onlyread = "readonly";//只读
            if (!this.ReadOnly) onlyread = "";

            writer.Write("<!-- 日期控件开始 -->\r\n");
            writer.Write("<span id=\"" + this.UniqueID + "\" style=\"display:inline\">\r\n");
            writer.Write("<table cellspacing=0 cellpadding=0 border=0 style=\"border-width:0px;height:100%;width:130px;border-collapse:collapse;\">\r\n");
            writer.Write("	<tr>\r\n");
            writer.Write("		<td id=\"" + this.UniqueID + "_pageTable1\" align=left>\r\n");
            writer.Write("          <input name=\"" + this.UniqueID + "_Calendar\" type=\"text\" " + onlyread + " id=\"" + this.UniqueID + "_Calendar\" style=\"width:120px;\" value=\"" + this.Text + "\""); 
            
            //背景色
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
            writer.Write("<!-- 日期控件结束 -->\r\n");
            base.Render(writer);
        }

        #region 属性
        
        /// <summary>
        /// 图片路径
        /// </summary>
        [Category("设置"), Description("图片路径"), Browsable(true), NotifyParentProperty(true)]
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
        /// 日期表达式
        /// </summary>
        [Category("设置"), Description("日期表达式"), Browsable(true), NotifyParentProperty(true)]
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
        /// 日期
        /// </summary>
        [Category("设置"), Description("日期")]
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
        /// 显示时间
        /// </summary>
        [Category("设置"), Description("显示时间")]
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
        /// 输入框时间只读
        /// </summary>
        [Category("设置"), Description("输入框时间只读")]
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