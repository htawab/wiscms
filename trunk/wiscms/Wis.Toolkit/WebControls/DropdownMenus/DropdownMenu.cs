//------------------------------------------------------------------------------
// <copyright file="DropdownMenu.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Drawing;
using System.ComponentModel;

[assembly: TagPrefix("DropdownMenu", "WebControls")]

namespace Wis.Toolkit.WebControls.DropdownMenus
{
    [ToolboxBitmap(typeof(DropdownMenu), "DropdownMenu.bmp")]
    [System.Web.UI.ToolboxData("<{0}:DropdownMenu runat=server></{0}:DropdownMenu>")]
    [System.Web.UI.ValidationProperty("Text")]
    [System.ComponentModel.DefaultProperty("Text")]
    [System.ComponentModel.Designer(typeof(Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuControlDesigner))]
    [System.ComponentModel.Description("Dropdown Menu")]
    public class DropdownMenu : System.Web.UI.WebControls.WebControl
    {
        public DropdownMenu()
        {
            _MenuItems = new List<DropdownMenuItem>();
        }


        /// <summary>
        /// 文本。
        /// </summary>
        public string Text
        {
            get { return ((string)(ViewState["Text"])); }
            set { ViewState["Text"] = value; }
        }

        /// <summary>
        /// 值。
        /// </summary>
        public string Value
        {
            get { return ((string)(ViewState["Value"])); }
            set { ViewState["Value"] = value; }
        }

        private List<DropdownMenuItem> _MenuItems;
        public List<DropdownMenuItem> MenuItems
        {
            get { return _MenuItems; }
            set { _MenuItems = value; }
        }


        /// <summary>
        /// 图片所在路径。
        /// </summary>
        public string ImagePath
        {
            get { return ((string)(ViewState["ImagePath"])); }
            set { ViewState["ImagePath"] = value; }
        }


        /// <summary>
        /// 引发 Load 事件
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="EventArgs"/> 对象</param>
        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.Text = System.Web.HttpContext.Current.Request[this.ClientID + "_Text"];
                this.Value = System.Web.HttpContext.Current.Request[this.ClientID + "_Value"];
            }
        }


        /// <summary>
        /// 引发 System.Web.UI.Control.PreRender 事件。
        /// </summary>
        /// <param _Name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnPreRender(EventArgs e)
        {
            string version = "<!--Copyright (C) Everwis Corporation.  All rights reserved. -->\r\n";
            base.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DropdownMenuCopyright", version);

            string scriptKey = "DropdownMenuScript";
            string scriptBlock = @"
<script type='text/javascript'>    
    var DropdownMenu = function(){
    
	    function Create(name){
	        this.Name = name;
	        this.IntervalTime = 15;
	        this.ListItems = [];
	        this.SubMenus = [];
	        this.DataItems = [];
	    }
	    
	    Create.prototype.init = function(className){
	        //document.execCommand('BackgroundImageCache', false, true);
	        
		    var dropdownMenuElement = document.getElementById(this.Name); // 整个DropdownMenu
		    var subMenus = dropdownMenuElement.getElementsByTagName('ul'); // UL 集合，SubMenu集合
		    
		    var index;
		    for(index = 0; index<subMenus.length; index++){
			    this.SubMenus[index] = subMenus[index];
			    var listItem = subMenus[index].parentNode;
			    if(listItem.tagName == 'LI'){
			        this.ListItems[index] = listItem;
			        listItem.onmouseover = new Function(this.Name + '.MouseOver(' + index + ')');
			        listItem.onmouseout = new Function(this.Name + '.MouseOut(' + index + ')');
			    }
		    }
		    
		    var dataItems = dropdownMenuElement.getElementsByTagName('a'); // a 对象集合，即数据项
		    for(index=0; index<dataItems.length; index++){
		        this.DataItems[index] = dataItems[index];
		        if(dataItems[index].className != 'menulink')
		        {
		            dataItems[index].onclick = new Function(this.Name + '.MouseClick(' + index + ')');
		        }
		    }
	    }
	    
	    Create.prototype.MouseClick = function(index){
		    var selectDataItem = document.getElementById(this.Name + '__DropdownMenu__');
		    selectDataItem.innerHTML = this.DataItems[index].innerHTML;
            document.getElementById(this.Name + '_Text').value = this.DataItems[index].innerHTML;
            document.getElementById(this.Name + '_Value').value = this.DataItems[index].getAttribute('value');
		    return true;
	    } 
	    
	    Create.prototype.MouseOver = function(index){
		    var subMenu = this.SubMenus[index];
		    subMenu.style.display = 'block';
		    subMenu.style.overflow = 'visible';
		}

	    Create.prototype.MouseOut = function(index){
		    var subMenu = this.SubMenus[index];
		    subMenu.style.display = 'none';
		    subMenu.style.overflow = 'hidden';
	    }

	    return{Create:Create}
    }();
</script>
";
            if (!base.Page.ClientScript.IsClientScriptBlockRegistered(scriptKey))
            {
                base.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptKey, scriptBlock);
            }

            // body {margin:25px; font:11px Verdana,Arial; background:#eee;}
            string styleKey = string.Format("DropdownMenuCss_{0}", base.ClientID);
            string styleBlock = string.Format(@"
            <style type='text/css'>
                ul.{0} {{list-style:none; margin:0; padding:0; width:169px; overflow:visible; line-height:23px;}}
                ul.{0} * {{margin:0; padding:0; cursor: pointer;}}
                ul.{0} a {{display:block; color:#000; text-decoration:none; height:22px;}}
                ul.{0} li {{background:url({1}header.gif); position:relative; float:left; margin-right:2px; overflow:visible;}}
                ul.{0} iframe {{position:absolute; width:169px; height:23px; top:0; left:-1px; z-index:-1; }}
                ul.{0} ul {{position:absolute; top:23px; left:0; width:169px; display:none; *opacity:0; list-style:none;}}
                ul.{0} ul li {{position:relative; border:1px solid #aaa; width:167px; border-top:none;  margin:0}}
                ul.{0} ul li a {{display:block; padding:0px 7px; height:22px; background-color:#d1d1d1}}
                ul.{0} ul li a:hover {{background-color:#c5c5c5}}
                ul.{0} ul ul {{left:168px; top:0px}}
                ul.{0} .menulink {{display:block; border:1px solid #aaa; padding:0px 15px 0px 7px; font-weight:bold; background:url({1}arrow3.gif) 152px 8px no-repeat; width:145px}}
                ul.{0} .menulink:hover, ul.menu .menuhover {{background:url({1}arrow2.gif) 152px 8px no-repeat;}}
                ul.{0} .sub {{background:#d1d1d1 url({1}arrow.gif) 147px 8px no-repeat}}
                ul.{0} .topline {{border-top:1px solid #aaa}}
            </style>
            ", base.ClientID, this.ImagePath);

            if (!base.Page.ClientScript.IsClientScriptBlockRegistered(styleKey))
            {
                base.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), styleKey, styleBlock);
            }
        }


        /// <summary>
        /// 将服务器控件内容发送到提供的 HtmlTextWriter 对象。
        /// </summary>
        /// <param name="writer">HtmlTextWriter 对象</param>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            // <ul class="Category" id="Category">
            writer.WriteBeginTag("ul");
            writer.WriteAttribute("id", base.ClientID);
            writer.WriteAttribute("class", base.ClientID);
            writer.Write(">");

            // <li>
            writer.WriteBeginTag("li");
            writer.Write(">");

            // <a href="#" class="menulink" id="Category__DropdownMenu__">Dropdown One</a>
            writer.WriteBeginTag("a");
            writer.WriteAttribute("href", "#");
            writer.WriteAttribute("class", "menulink");
            writer.WriteAttribute("id", string.Format("{0}__DropdownMenu__", base.ClientID));
            writer.Write(">");
            writer.Write(this.Text);
            writer.WriteEndTag("a");

            WriteMenuItem(writer, this.MenuItems);

            // </li>
            writer.WriteEndTag("li");
            // </ul>
            writer.WriteEndTag("ul");

            writer.Write("<input name=\"" + this.ClientID + "_Text\" type=\"hidden\" id=\"" + this.ClientID + "_Text\" value=\"" + this.Text + "\" />");
            writer.Write("<input name=\"" + this.ClientID + "_Value\" type=\"hidden\" id=\"" + this.ClientID + "_Value\" value=\"" + this.Value + "\" />");

            string scriptBlock = string.Format(@"
<script type='text/javascript'>
	var {0} = new DropdownMenu.Create('{0}');
	{0}.init();
</script>
", base.ClientID);
            writer.Write(scriptBlock);
        }

        private void WriteMenuItem(System.Web.UI.HtmlTextWriter writer, List<DropdownMenuItem> menuItems)
        {
            /*
             * <ul>
                <li><a href="#">Navigation Item 1</a></li>
                <li><a href="#" class="sub">Navigation Item 2</a>
                    <ul>
                        <li class="topline"><a href="#">Navigation Item 21</a></li>
                        <li><a href="#">Navigation Item 22</a></li>
                        <li><a href="#">Navigation Item 23</a></li>
                        <li><a href="#">Navigation Item 24</a></li>
                        <li><a href="#">Navigation Item 25</a></li>
                    </ul>
                </li>
             * </ul>
             */
            // <ul>
            int index = 0;
            writer.WriteBeginTag("ul");
            writer.Write(">");
            foreach (DropdownMenuItem menuItem in menuItems)
            {
                index++;

                // <li>
                writer.WriteBeginTag("li");
                writer.Write(">");


                writer.WriteBeginTag("a");
                writer.WriteAttribute("href", "#");

                // 有子项时，给sub样式，为第一项时，给topline样式
                if (menuItem.SubMenuItems.Count > 0 && index == 1)
                    writer.WriteAttribute("class", "sub topline");
                else if (menuItem.SubMenuItems.Count == 0 && index == 1)
                    writer.WriteAttribute("class", "topline");
                else if (menuItem.SubMenuItems.Count > 0 && index > 1)
                    writer.WriteAttribute("class", "sub");

                // <li class="topline"><a href="#">Navigation Item 21</a></li>
                if (!string.IsNullOrEmpty(menuItem.Value))
                    writer.WriteAttribute("Value", menuItem.Value);

                writer.Write(">");

                writer.Write(menuItem.Text);

                writer.WriteEndTag("a");

                //iframe
                writer.WriteBeginTag("iframe");
                writer.WriteAttribute("frameborder", "0");
                writer.WriteAttribute("scrolling", "no");
                writer.WriteAttribute("src", "a.html");
                writer.Write(">");//
                writer.WriteEndTag("iframe");
                // 递归生成ul
                if (menuItem.SubMenuItems.Count > 0)
                    WriteMenuItem(writer, menuItem.SubMenuItems);

                // </li>

                writer.WriteEndTag("li");
            }

            // </ul>
            writer.WriteEndTag("ul");
        }
    }
}
