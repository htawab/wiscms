//------------------------------------------------------------------------------
// <copyright file="DropdownMenuControlDesigner.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.IO;

namespace Wis.Toolkit.WebControls.DropdownMenus
{
    /// <summary>
    /// DropdownMenu 控件的设计模式行为.
    /// </summary>
    public class DropdownMenuControlDesigner : System.Web.UI.Design.ControlDesigner
    {
        /// <summary>
		/// 
		/// </summary>
        public DropdownMenuControlDesigner() { }

        private DropdownMenu _DropdownMenu;

		/// <summary>
		/// 
		/// </summary>
		/// <param _Name="component"></param>
		public override void Initialize(IComponent component) {
            _DropdownMenu = (DropdownMenu)component;
			base.Initialize(component);
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public override string GetDesignTimeHtml() {
            string html = string.Format(@"
            <style type='text/css'>
                ul.{0} {{list-style:none; margin:0; padding:0; width:200px; overflow:visible; line-height:23px;}}
                ul.{0} * {{margin:0; padding:0; cursor: pointer;}}
                ul.{0} a {{display:block; color:#000; text-decoration:none; height:22px;}}
                ul.{0} li {{background:url({1}header.gif); position:relative; float:left; margin-right:2px; overflow:visible;}}
                ul.{0} iframe {{position:absolute; width:169px; height:23px; top:0; left:-1px; z-index:-1; }}
                ul.{0} ul {{position:absolute; top:23px; left:0; background:#d1d1d1;  display:none; *opacity:0; list-style:none;}}
                ul.{0} ul li {{position:relative; border:1px solid #aaa; width:167px; border-top:none;  margin:0}}
                ul.{0} ul li a {{display:block; padding:0px 7px; height:22px; background-color:#d1d1d1}}
                ul.{0} ul li a:hover {{background-color:#c5c5c5}}
                ul.{0} ul ul {{left:168px; top:0px}}
                ul.{0} .menulink {{display:block; border:1px solid #aaa; padding:0px 15px 0px 7px; font-weight:bold; background:url({1}arrow3.gif) 152px 8px no-repeat; width:145px}}
                ul.{0} .menulink:hover, ul.menu .menuhover {{background:url({1}arrow2.gif) 152px 8px no-repeat;}}
                ul.{0} .sub {{background:#d1d1d1 url({1}arrow.gif) 147px 8px no-repeat}}
                ul.{0} .topline {{border-top:1px solid #aaa}}
            </style>
            ", _DropdownMenu.ClientID, _DropdownMenu.ImagePath);

            html += String.Format("<UL class='{0}' id='{0}'><LI>ssss<A class='menulink' href='#'>{1}</A><iframe frameborder='0' scrolling='no' src='a.html'></iframe></LI></UL>", _DropdownMenu.ClientID, _DropdownMenu.Text);
            return html;
		}
    }
}
