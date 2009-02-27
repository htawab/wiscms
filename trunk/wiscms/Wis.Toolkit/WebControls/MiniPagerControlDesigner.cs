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
    /// �����ҳ�ؼ����������
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
<STYLE> #MiniPager1 {color:#222;font-family:����;font-size:12px; line-height:22px;text-align:right;} #MiniPager1 A {border:1px solid #ddd;text-decoration:none;	padding:0 6px;color:#886db4; display:inline-block; height:22px; background:#fafafa;margin-left:2px;} #MiniPager1 a.currentPager {border:1px solid #fcc;	text-decoration:none;background:#f0f0f0; color:#f00;  cursor:default;} </STYLE>
<DIV id='MiniPager1'><a>��1000����¼&nbsp;��10ҳ/��50ҳ</a><A href='#'>��ҳ</A><A href='#'>��һҳ</A><A title='��1ҳ' href='#'>1</A><A title='��2ҳ' href='#'>2</A><A title='��3ҳ' href='#'>3</A><A title='��4ҳ' href='#'>4</A><A title='��5ҳ' href='#'>5</A><A title='��6ҳ' href='#'>6</A><A title='��7ҳ' href='#'>7</A><A title='��8ҳ' href='#'>8</A><A title='��9ҳ' href='#'>9</A><a class='currentPager' title='��10ҳ'>10</a><A title='��11ҳ' href='#'>...</A><A href='#'>��һҳ</A><A href='#'>βҳ</A></DIV>
";
		}
	}
}