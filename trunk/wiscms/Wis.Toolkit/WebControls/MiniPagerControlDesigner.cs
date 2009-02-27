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

namespace Wis.Toolkit.WebControls
{
	/// <summary>
    /// 迷你分页控件的设计器。
	/// </summary>
    public class MiniPagerControlDesigner : System.Web.UI.Design.ControlDesigner
    {

		/// <summary>
		/// 
		/// </summary>
        public MiniPagerControlDesigner() { }

        private MiniPager _MiniPager;

		/// <summary>
		/// 
		/// </summary>
		/// <param _Name="component"></param>
		public override void Initialize(IComponent component) {
            _MiniPager = (MiniPager)component;
			base.Initialize(component);
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public override string GetDesignTimeHtml() {
            return @"
<STYLE> #MiniPager1 {color:#222;font-family:宋体;font-size:12px; line-height:22px;text-align:right;} #MiniPager1 A {border:1px solid #ddd;text-decoration:none;	padding:0 6px;color:#886db4; display:inline-block; height:22px; background:#fafafa;margin-left:2px;} #MiniPager1 a.currentPager {border:1px solid #fcc;	text-decoration:none;background:#f0f0f0; color:#f00;  cursor:default;} </STYLE>
<DIV id='MiniPager1'><a>共1000条记录&nbsp;第10页/共50页</a><A href='#'>首页</A><A href='#'>上一页</A><A title='第1页' href='#'>1</A><A title='第2页' href='#'>2</A><A title='第3页' href='#'>3</A><A title='第4页' href='#'>4</A><A title='第5页' href='#'>5</A><A title='第6页' href='#'>6</A><A title='第7页' href='#'>7</A><A title='第8页' href='#'>8</A><A title='第9页' href='#'>9</A><a class='currentPager' title='第10页'>10</a><A title='下11页' href='#'>...</A><A href='#'>下一页</A><A href='#'>尾页</A></DIV>
";
		}
	}
}