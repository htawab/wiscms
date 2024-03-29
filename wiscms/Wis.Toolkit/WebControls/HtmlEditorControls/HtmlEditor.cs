//------------------------------------------------------------------------------
// <copyright file="HtmlEditor.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

[assembly: TagPrefix("HtmlEditorControls", "WebControls")]

[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Css.style.css", "text/css")]

//[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Scripts.Designer.js", "text/javascript")]
//[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Scripts.DesignerView.js", "text/javascript")]
//[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Scripts.Preload.js", "text/javascript")]
//[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Scripts.Toolbars.js", "text/javascript")]
//image/gif
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Bold.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.BulletedList.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Clear.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Copy.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.CreateLink.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Cut.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.DecreasesRegional.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Design.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.FontForeColor.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Help.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.IncreaseRegional.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Indent.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.InsertPhoto.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.InsertFile.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.InsertVideo.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.InsertFlash.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.InsertHorizontalRule.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.InsertSymbol.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.InsertTable.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Italic.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.JustifyCenter.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.JustifyFull.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.JustifyLeft.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.JustifyRight.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_bottom.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_left.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_left1.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_leftbottom.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_lefttop.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_right.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_right1.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_rightbottom.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_righttop.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Menu_border_top.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Mouseoff.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Mouseon.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.NumberedList.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Outdent.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Paste.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.PreFormatted.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Preview.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Printpage.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Redo.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.RemoveFormat.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Sel.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Source.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Strikethrough.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.StripWordFormatting.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Subscript.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Superscript.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Text.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.TextHighlightColor.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Underline.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Undo.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Images.Unlink.gif", "image/gif")]
//[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Designers.Designer.html", "text/html")]
//[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Designers.DesignerToolbars.html", "text/html")]
//[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.HtmlEditorControls.Designers.DesignerView.html", "text/html")]

// XHTML references http://www.sharkui.com/reference/htmltags/index.php
// Symbols references http://www.w3schools.com/tags/ref_entities.asp

namespace Wis.Toolkit.WebControls.HtmlEditorControls
{
    /// <summary>
    /// A WYSIWYG rich-_Text editor for ASP.NET, which replace any TextBox with an Word-like editor.
    /// </summary>
    [ToolboxData("<{0}:HtmlEditor runat=server></{0}:HtmlEditor>")]
    [ValidationPropertyAttribute("Text")]
    [DefaultProperty("Text")]
    [Designer(typeof(HtmlEditorControlDesigner))]
    [DescriptionAttribute("a word-like editor")]
    public class HtmlEditor:Control, INamingContainer, IPostBackDataHandler, IPostBackEventHandler
    {
        /// <summary>
        /// Dialog's path.
        /// </summary>
        public string DialogsPath
        {
            get
            {
                return (ViewState["DialogsPath"] == null) ? (this.ApplicationPath + "HtmlEditor/Dialogs/") : ViewState["DialogsPath"].ToString();
            }
            set { ViewState["DialogsPath"] = value; }
        }

        private string ApplicationPath
        {
            get
            {
                string applicationPath = Page.Request.ApplicationPath;
                if (!applicationPath.EndsWith("/")) applicationPath += "/";
                return applicationPath;
            }
        }


        /// <summary>
        /// Contains the Html Content for the editor.
        /// </summary>
        [CategoryAttribute("输出")]
        [DescriptionAttribute("获取或设置 Html Editor 控件的内容。")]
        public string Text
        {
            get
            {
                string text = ((ViewState["Text"] == null) ? "" : ViewState["Text"].ToString());

                //// 主机地址
                //string serverName = Page.Request.ServerVariables["HTTP_HOST"];
                //// 绝对地址
                //string script = Page.Request.ServerVariables["SCRIPT_NAME"];

                //// 将当前页面地址清除
                //string queryString = Page.Request.QueryString.ToString();
                //text = text.Replace("href=\"http://" + serverName + script + "?" + queryString, "href=\"");
                //text = text.Replace("href=http://" + serverName + script + "?" + queryString, "href=");

                //queryString = queryString.Replace("&", "&amp;");
                //text = text.Replace("href=\"http://" + serverName + script + "?" + queryString, "href=\"");
                //text = text.Replace("href=http://" + serverName + script + "?" + queryString, "href=");

                // 移除编辑器内容中超链接地址、图片地址等地址中的主机地址
                // 如果不移除主机地址，编辑器默认将使用形如：http://www.htmleditor.cn/images/editor.gif 的绝对地址。
                //text = Regex.Replace(text, "HREF=http://" + serverName, "HREF=", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "HREF=\"http://" + serverName, "HREF=\"", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "src=http://" + serverName, "src=", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "src=\"http://" + serverName, "src=\"", RegexOptions.IgnoreCase);

                // 编码特殊符号
                //text = Regex.Replace(text, "¡", "&#161;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¢", "&#162;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "£", "&#163;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¤", "&#164;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¥", "&#165;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¦", "&#166;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "§", "&#167;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¨", "&#168;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "©", "&#169;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ª", "&#170;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "«", "&#171;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¬", "&#172;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "®", "&#174;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¯", "&#175;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "°", "&#176;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "±", "&#177;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "²", "&#178;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "³", "&#179;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "´", "&#180;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "µ", "&#181;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¶", "&#182;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "·", "&#183;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¸", "&#184;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¹", "&#185;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "º", "&#186;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "»", "&#187;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¼", "&#188;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "½", "&#189;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¾", "&#190;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "¿", "&#191;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "À", "&#192;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Á", "&#193;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Â", "&#194;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ã", "&#195;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ä", "&#196;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Å", "&#197;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Æ", "&#198;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ç", "&#199;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "È", "&#200;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "É", "&#201;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ê", "&#202;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ë", "&#203;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ì", "&#204;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Í", "&#205;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Î", "&#206;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ï", "&#207;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ð", "&#208;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ñ", "&#209;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ò", "&#210;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ó", "&#211;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ô", "&#212;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Õ", "&#213;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ö", "&#214;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "×", "&#215;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ø", "&#216;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ù", "&#217;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ú", "&#218;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Û", "&#219;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ü", "&#220;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ý", "&#221;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Þ", "&#222;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ß", "&#223;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "à", "&#224;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "á", "&#225;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "â", "&#226;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ã", "&#227;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ä", "&#228;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "å", "&#229;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "æ", "&#230;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ç", "&#231;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "è", "&#232;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "é", "&#233;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ê", "&#234;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ë", "&#235;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ì", "&#236;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "í", "&#237;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "î", "&#238;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ï", "&#239;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ð", "&#240;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ñ", "&#241;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ò", "&#242;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ó", "&#243;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ô", "&#244;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "õ", "&#245;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ö", "&#246;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "÷", "&#247;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ø", "&#248;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ù", "&#249;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ú", "&#250;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "û", "&#251;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ü", "&#252;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ý", "&#253;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "þ", "&#254;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ÿ", "&#255;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "•", "&#8226;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "…", "&#8230;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "™", "&#8482;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ø", "&#216;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "˜", "&#732;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "·", "&#183;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Œ", "&#338;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "œ", "&#339;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Š", "&#352;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "š", "&#353;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "Ÿ", "&#376;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "ˆ", "&#710;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "˜", "&#732;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "–", "&#8211;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "—", "&#8212;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "‘", "&#8216;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "’", "&#8217;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "‚", "&#8218;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "“", "&#8220;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "”", "&#8221;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "„", "&#8222;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "†", "&#8224;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "‡", "&#8225;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "‰", "&#8240;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "‹", "&#8249;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "›", "&#8250;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "€", "&#8364;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "\"", "&#34;", RegexOptions.IgnoreCase);
                //text = Regex.Replace(text, "'", "&#39;", RegexOptions.IgnoreCase);

                return text;
            }
            set
            {
                ViewState["Text"] = value;
            }
        }


        // TODO:move the function to Text.
        public string Remove(string html)
        {
            string serverName = Page.Request.ServerVariables["HTTP_HOST"];

            string pattern = string.Format("href=http://{0}", serverName);
            html = Regex.Replace(html, pattern, "href=", RegexOptions.IgnoreCase);

            pattern = string.Format("href=\"http://{0}", serverName);
            html = Regex.Replace(html, pattern, "href=\"", RegexOptions.IgnoreCase);

            pattern = string.Format("src=http://{0}", serverName);
            html = Regex.Replace(html, pattern, "src=", RegexOptions.IgnoreCase);

            pattern = string.Format("src=\"http://{0}", serverName);
            html = Regex.Replace(html, pattern, "src=\"", RegexOptions.IgnoreCase);

            return html;
        }


        /// <summary>
        /// If ReadOnly is true, Html editor is not editable.
        /// </summary>
        [CategoryAttribute("行为")]
        [DescriptionAttribute("获取或设置一个值，该值指示编辑器中的内容是否允许编辑。")]
        public bool ReadOnly
        {
            get
            {
                object viewState = this.ViewState["ReadOnly"];
                return (viewState == null) ? false : (bool)viewState;
            }
            set
            {
                ViewState["ReadOnly"] = value;
            }
        }


        /// <summary>
        /// 获取或设置编辑器的起始视图。可以为：PlainTextView（纯文本视图），SourceView（源代码视图），DesignView（设计视图），PreviewView（预览视图）。
        /// </summary>
        [CategoryAttribute("行为")]
        [DescriptionAttribute("获取或设置编辑器的起始视图。可以为：PlainTextView（纯文本视图），SourceView（源代码视图），DesignView（设计视图），PreviewView（预览视图）。")]
        public Panels StartDesignerView
        {
            get
            {
                object viewState = this.ViewState["StartDesignerView"];
                return (viewState == null) ? Panels.Design : (Panels)viewState;
            }
            set
            {
                ViewState["StartDesignerView"] = value;
            }
        }


        /// <summary>
        /// 获取或设置 纯文本视图 使用的 CSS 样式文件。
        /// </summary>
        [
        CategoryAttribute("外部的"),
        DescriptionAttribute("获取或设置 纯文本视图 使用的 CSS 样式文件。")
        ]
        public string PlainTextViewCss
        {
            get
            {
                object viewState = this.ViewState["PlainTextViewCss"];
                return (viewState == null) ? "" : (string)viewState;
            }
            set
            {
                ViewState["PlainTextViewCss"] = value;
            }
        }


        /// <summary>
        /// 获取或设置 源代码视图 使用的 CSS 样式文件。
        /// </summary>
        [
        CategoryAttribute("外部的"),
        DescriptionAttribute("获取或设置 源代码视图 使用的 CSS 样式文件。")
        ]
        public string SourceViewCss
        {
            get
            {
                object viewState = this.ViewState["SourceViewCss"];
                return (viewState == null) ? "" : (string)viewState;
            }
            set
            {
                ViewState["SourceViewCss"] = value;
            }
        }


        /// <summary>
        /// 获取或设置 设计视图 使用的 CSS 样式文件。
        /// </summary>
        [
        CategoryAttribute("外部的"),
        DescriptionAttribute("获取或设置 设计视图 使用的 CSS 样式文件。")
        ]
        public string DesignViewCss
        {
            get
            {
                object viewState = this.ViewState["DesignViewCss"];
                return (viewState == null) ? "" : (string)viewState;
            }
            set
            {
                ViewState["DesignViewCss"] = value;
            }
        }


        /// <summary>
        /// 获取或设置 预览视图 使用的 CSS 样式文件。
        /// </summary>
        [
        CategoryAttribute("外部的"),
        DescriptionAttribute("获取或设置 预览视图 使用的 CSS 样式文件。")
        ]
        public string PreviewViewCss
        {
            get
            {
                object viewState = this.ViewState["PreviewViewCss"];
                return (viewState == null) ? "" : (string)viewState;
            }
            set
            {
                ViewState["PreviewViewCss"] = value;
            }
        }


        /// <summary>
        /// The back color of the entire editor area.
        /// </summary>
        [
        CategoryAttribute("外观"),
        DescriptionAttribute("获取或设置 HtmlEditor 控件的背景色。")
        ]
        public System.Drawing.Color BackgroundColor
        {
            get
            {
                object viewState = this.ViewState["BackgroundColor"];
                return (viewState == null) ? System.Drawing.ColorTranslator.FromHtml("#9EBEF5") : (System.Drawing.Color)viewState;
            }
            set
            {
                ViewState["BackgroundColor"] = value;
            }
        }

        private Unit mWidth = Unit.Pixel(680);
        /// <summary>
        /// The width of the editor.
        /// </summary>
        public Unit Width
        {
            get { return mWidth; }
            set { mWidth = value; }
        }

        private Unit mHeight = Unit.Pixel(250);
        /// <summary>
        /// The height of the editor.
        /// </summary>
        public Unit Height
        {
            get { return mHeight; }
            set { mHeight = value; }
        }

        /// <summary>
        /// 获取或设置编辑器样式。
        /// </summary>
        [
        CategoryAttribute("外观"),
        DescriptionAttribute("获取或设置编辑器样式。")
        ]
        public Theme Theme
        {
            get
            {
                object viewState = this.ViewState["Theme"];
                return (viewState == null) ? Theme.Word2007 : (Theme)viewState;
            }
            set
            {
                ViewState["Theme"] = value;
            }
        }


        /// <summary>
        /// Switch mode of editor.
        /// </summary>
        [
        CategoryAttribute("外观"),
        DescriptionAttribute("获取或设置编辑器视图切换模式。")
        ]
        public NotificationMode SwitchMode
        {
            get
            {
                object viewState = this.ViewState["SwitchMode"];
                return (viewState == null) ? NotificationMode.Verbose : (NotificationMode)viewState;
            }
            set
            {
                ViewState["SwitchMode"] = value;
            }
        }


        /// <summary>
        /// 实现<see cref="IPostBackEventHandler"/> 接口，使 <see cref="HtmlEditor"/> 控件能够处理将窗体发送到服务器时引发的事件。
        /// </summary>
        /// <param _Name="eventArgument"></param>
        public virtual void RaisePostBackEvent(string eventArgument)
        {
            //switch (eventArgument) { }
        }


        /// <summary>
        /// 
        /// </summary>
        public void RaisePostDataChangedEvent()
        {
            // nothing happens for _Text changed
        }


        /// <summary>
        /// 引发 System.Web.UI.Control.PreRender 事件。
        /// </summary>
        /// <param _Name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnPreRender(EventArgs e)
        {
            Page.RegisterRequiresPostBack(this);

            string version = "<!--Copyright (c) Wis.Toolkit Corporation.  All rights reserved. -->\r\n";
            if (!Page.ClientScript.IsClientScriptBlockRegistered(HtmlEditor_ClientScript_Copyright_Begin))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), HtmlEditor_ClientScript_Copyright_Begin, version);
            }                

            // start register client script
            if (!Page.ClientScript.IsClientScriptBlockRegistered(HtmlEditor_ClientScript_BeginRegister))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), HtmlEditor_ClientScript_BeginRegister, @"<!-- HtmlEditor ClientScript Begin -->");
            }

            string browser = Page.Request.Browser.Browser.ToUpper();
            int browserMajorVersion = Page.Request.Browser.MajorVersion;

            if ((browser.IndexOf("IE") > -1 && browserMajorVersion >= 5))
            {
                PreRenderHtmlEditor();// if the client supports html editor
            }
            //else
            //{
                //PreRenderTextArea();
            //}

            // end register client script
            if (!Page.ClientScript.IsClientScriptBlockRegistered(HtmlEditor_ClientScript_EndRegister))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), HtmlEditor_ClientScript_EndRegister, @"<!-- HtmlEditor ClientScript End -->");
            }

            //版权申明
            if (!Page.ClientScript.IsClientScriptBlockRegistered(HtmlEditor_ClientScript_Copyright_End))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), HtmlEditor_ClientScript_Copyright_End, "\r\n" + version);

            base.OnPreRender(e);
        }
        
        private const string HtmlEditor_ClientScript_Copyright_Begin = "HtmlEditor_ClientScript_Copyright_Begin";
        private const string HtmlEditor_ClientScript_Copyright_End = "HtmlEditor_ClientScript_Copyright_End";
        private const string HtmlEditor_ClientScript_BeginRegister = "HtmlEditor_ClientScript_BeginRegister";
        private const string HtmlEditor_ClientScript_EndRegister = "HtmlEditor_ClientScript_EndRegister";
        private const string HtmlEditor_ClientScript_Preload = "HtmlEditor_ClientScript_Preload";
        private const string HtmlEditor_ClientScript_Toolbars = "HtmlEditor_ClientScript_Toolbars";
        private const string HtmlEditor_ClientScript_Designer = "HtmlEditor_ClientScript_Designer";
        private const string HtmlEditor_ClientScript_DesignerView = "HtmlEditor_ClientScript_DesignerView";
        private const string HtmlEditor_Style_Css = "HtmlEditor_Style_Css";

        private void PreRenderHtmlEditor()
        {
            // 预装载图片
            // 1 Register preload images client script block
            RegisterClientScriptBlock(HtmlEditor_ClientScript_Preload, this.GetType().Namespace.ToString() + ".Scripts.Preload.js");

            // 2 Register html editor style.
            RegisterClientStyleBlock(HtmlEditor_Style_Css, this.GetType().Namespace.ToString() + ".Css.Style.css");

            // 3 register toolbars script block 
            RegisterClientScriptBlock(HtmlEditor_ClientScript_Toolbars, this.GetType().Namespace.ToString() + ".Scripts.Toolbars.js");

            // 4 register designer script block
            RegisterClientScriptBlock(HtmlEditor_ClientScript_Designer, this.GetType().Namespace.ToString() + ".Scripts.Designer.js");

            // 5 register designer script block
            RegisterClientScriptBlock(HtmlEditor_ClientScript_DesignerView, this.GetType().Namespace.ToString() + ".Scripts.DesignerView.js");

            // 提交表单前，保存Html
            // 2 fix for IE 5 which doesn't not have iframe.onblur
            // TODO:能不能在javascript中控制OnSubmit?
            this.Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), base.ClientID + "_OnSubmit", "return HtmlEditor_Save(" + base.ClientID + @"_Designer,'" + base.ClientID + @"_Hidden', " + base.ClientID + @"_CurrentDesignerView);");
        }

        private void RegisterClientScriptBlock(string key, string typeName)
        {
            string scriptBlock;
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
 
            // TODO:线程安全
            System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(typeName);
            
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, encoding))
            {
                scriptBlock = reader.ReadToEnd();
            }

            scriptBlock = "\n<script language='JavaScript' type='text/javascript'>\n" + scriptBlock + "\n</script>\n";
            if (!Page.ClientScript.IsClientScriptBlockRegistered(key))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), key, scriptBlock);
            }
        }

        private void RegisterClientStyleBlock(string key, string typeName)
        {
            string styleBlock;
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            
            // TODO:线程安全
            System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(typeName);
            
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, encoding))
            {
                styleBlock = reader.ReadToEnd();
            }

            styleBlock = "\n\n<style type='text/css'>\n" + styleBlock + "\n</style>\n";
            if (!Page.ClientScript.IsClientScriptBlockRegistered(key))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), key, styleBlock);
            }
        }


        /// <summary>
        /// 将服务器控件内容发送到提供的 System.Web.UI.HtmlTextWriter 对象，此对象编写将在客户端呈现的内容。
        /// </summary>
        /// <param name="writer">接收服务器控件内容的 System.Web.UI.HtmlTextWriter 对象。</param>
        protected override void Render(HtmlTextWriter writer)
        {
            // writer a container
            writer.Write(string.Format("<table border='0' cellpadding='0' cellspacing='0' id='{0}Container' style='border:1px solid #9AB1D1; display:block; background-color:#D6E2F2'><tr><td>", this.ClientID));

            string browser = Page.Request.Browser.Browser.ToUpper();
            int browserMajorVersion = Page.Request.Browser.MajorVersion;
            if ((browser.IndexOf("IE") > -1 && browserMajorVersion >= 5))
            {
                // if the client supports html editor
                RenderHtmlEditor(writer);
            }
            else
            {
                RenderTextArea(writer);
            }

            // close writer.
            writer.Write("</td></tr></table>");
        }


        private void RenderHtmlEditor(HtmlTextWriter writer)
        {
            string theme;
            switch (this.Theme)
            {
                case Theme.Mini:
                    theme = "Mini";
                    break;
                case Theme.Word2007:
                    theme = "Word2007";
                    break;
                default:
                    theme = "Word2007";
                    break;
            }
            theme = "Word2007";

            // 1 Render toolbars
            WriteHtml(writer, this.GetType().Namespace.ToString() + ".Themes." + theme + ".DesignerToolbars.html");

            // 2 Render designer
            WriteHtml(writer, this.GetType().Namespace.ToString() + ".Themes." + theme + ".Designer.html");

            // 3 Render designer view
            WriteHtml(writer, this.GetType().Namespace.ToString() + ".Themes." + theme + ".DesignerView.html");
        }

        private void WriteHtml(HtmlTextWriter writer, string typeName)
        {
            string html;
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;

            // TODO:lock
            System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream(typeName);
            if (stream == null) return;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, encoding))
            {
                html = reader.ReadToEnd();
            }

            // 取出所有的图片 src="design.gif" 
            string pattern = "<img(?<Attributes1>[\\s\\S]*?)src=(\"{1}|'{1}|)(?<picture>[^\\[^>]*?(gif|jpg|jpeg|bmp|bmp))(\"{1}|'{1}|)(?<Attributes2>[\\s\\S]*?)>";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = r.Matches(html);
            foreach (Match match in matches)
            {
                string src = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.WebControls.HtmlEditorControls.Images." + match.Groups["picture"].Value);
                html = Regex.Replace(html, match.Groups["picture"].Value, src);
            }

            // background="menu_border_right.gif"
            pattern = "background=(\"{1}|'{1}|)(?<picture>[^\\[^>]*?(gif|jpg|jpeg|bmp|bmp))(\"{1}|'{1}|)";
            r = new Regex(pattern, RegexOptions.IgnoreCase);
            matches = r.Matches(html);
            foreach (Match match in matches)
            {
                string src = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Wis.Toolkit.WebControls.HtmlEditorControls.Images." + match.Groups["picture"].Value);
                html = Regex.Replace(html, match.Groups["picture"].Value, src);
            }

            // $ApplicationPath$ replace by Application Path.
            html = Regex.Replace(html, "\\$ApplicationPath\\$", ApplicationPath);

            // $ClientID$ replace by HtmlEditor.ClientID.
            html = Regex.Replace(html, "\\$ClientID\\$", this.ClientID);

            // $DialogsPath$ replace by HtmlEditor.DialogsPath.
            html = Regex.Replace(html, "\\$DialogsPath\\$", this.DialogsPath);

            // TODO:postback
            // scriptBlock = Regex.Replace(scriptBlock, "<POSTBACK>(?<command>[^<]+)</POSTBACK>", Page.ClientScript.GetPostBackEventReference(this, "${command}"));
            html = Regex.Replace(html, "\\$Text\\$", this.Text.Replace("\"", "&quot;"));
            html = Regex.Replace(html, "\\$ReadOnly\\$", this.ReadOnly.ToString().ToLower());

            html = Regex.Replace(html, "\\$Width\\$", string.Format("{0}", this.Width));
            
            html = Regex.Replace(html, "\\$Height\\$", string.Format("{0}", this.Height));
            //html = Regex.Replace(html, "\\$BackgroundColor\\$", this.BackgroundColor.ToString()); // ? 需要转换为 Html 颜色值
            html = Regex.Replace(html, "\\$StartDesignerView\\$", Convert.ToInt32(this.StartDesignerView).ToString()); // ? 需要转换为 Html 颜色值

            html = Regex.Replace(html, "\\$PlainTextViewCss\\$", this.PlainTextViewCss);
            html = Regex.Replace(html, "\\$SourceViewCss\\$", this.SourceViewCss);
            html = Regex.Replace(html, "\\$DesignViewCss\\$", this.DesignViewCss);
            html = Regex.Replace(html, "\\$PreviewViewCss\\$", this.PreviewViewCss);
            
            writer.Write(html);
        }


        private void RenderTextArea(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<textarea id=\"" + this.ClientID + "\" name=\"" + this.ClientID + "\"");
            if (this.Width.Type == UnitType.Pixel)
            {
                sb.Append(" COLS=" + (Int32.Parse(this.Width.Value.ToString()) / 10).ToString());
            }
            else
            {
                sb.Append(" COLS=40"); // TODO: throw the default _Value to default config.
            }
            if (this.Height.Type == UnitType.Pixel)
            {
                sb.Append(" ROWS=" + (Int32.Parse(this.Height.Value.ToString()) / 20).ToString());
            }
            else
            {
                sb.Append(" ROWS=7"); // TODO: throw the default _Value to default config.
            }
            sb.Append(">" + this.Text + "</TEXTAREA>");

            writer.Write(sb.ToString());
        }


        /// <summary>
        /// 将访问资源保存到本地，支持断点续传。
        /// </summary>
        /// <param name="path">文件在本地的存储路径，比如D:\4f087e08.htm。</param>
        /// <param name="url">文件的访问网址</param>
        /// <returns></returns>
        public static void SaveAs(string path, System.Uri uri)
        {
            byte[] bytes = new byte[513];
            System.IO.Stream stream = null;
            int count = 0;
            long offset = 0;
            System.IO.FileStream fileStream = null;
            System.Net.HttpWebRequest httpWebRequest = null;

            if (System.IO.File.Exists(path))
            {
                fileStream = System.IO.File.OpenWrite(path);
                offset = fileStream.Length;
                fileStream.Seek(offset, System.IO.SeekOrigin.Current);
            }
            else
            {
                fileStream = new System.IO.FileStream(path, System.IO.FileMode.Create);
                offset = 0;
            }

            httpWebRequest = ((System.Net.HttpWebRequest)(System.Net.HttpWebRequest.Create(uri)));
            if (offset > 0)
            {
                httpWebRequest.AddRange(System.Convert.ToInt32(offset));
            }

            stream = httpWebRequest.GetResponse().GetResponseStream();
            count = stream.Read(bytes, 0, 512);
            while (count > 0)
            {
                fileStream.Write(bytes, 0, count);
                count = stream.Read(bytes, 0, 512);
            }
            fileStream.Close();
            stream.Close();
        }

        #region IPostBackDataHandler Members

        public virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            if (postCollection == null) return false;

            string PresentValue = this.Text;
            string PostedValue = postCollection[base.ClientID + "_Hidden"];

            if (!PresentValue.Equals(PostedValue))
            {
                this.Text = PostedValue;
                //Page.RegisterRequiresRaiseEvent(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}