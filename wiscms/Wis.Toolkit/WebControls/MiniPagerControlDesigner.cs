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
<STYLE> #MiniPager1 {	FONT-SIZE: 12px;	COLOR: #222;	FONT-FAMILY: ����} #MiniPager1 A {	BORDER-RIGHT: #ddd 1px solid;	PADDING-RIGHT: 6px;	BORDER-TOP: #ddd 1px solid;	PADDING-LEFT: 6px;	PADDING-BOTTOM: 3px;	MARGIN: 0px 2px;	BORDER-LEFT: #ddd 1px solid;	COLOR: #886db4;	PADDING-TOP: 3px;	BORDER-BOTTOM: #ddd 1px solid;	TEXT-DECORATION: none} #MiniPager1 SPAN {	BORDER-RIGHT: #ddd 1px solid;	PADDING-RIGHT: 6px;	BORDER-TOP: #ddd 1px solid;	PADDING-LEFT: 6px;	PADDING-BOTTOM: 3px;	MARGIN: 0px 2px;	BORDER-LEFT: #ddd 1px solid;	PADDING-TOP: 3px;	BORDER-BOTTOM: #ddd 1px solid} #MiniPager1 SPAN.currentPager {	BORDER-RIGHT: #ddd 1px solid;	PADDING-RIGHT: 6px;	BORDER-TOP: #ddd 1px solid;	PADDING-LEFT: 6px;	BACKGROUND: #fafafa;	PADDING-BOTTOM: 3px;	MARGIN: 0px 2px;	BORDER-LEFT: #ddd 1px solid;	PADDING-TOP: 3px;	BORDER-BOTTOM: #ddd 1px solid;	TEXT-DECORATION: none} </STYLE>
<DIV id='MiniPager1'><SPAN>��1000����¼&nbsp;��10ҳ/��50ҳ</SPAN><A href='#'>��ҳ</A><A href='#'>��һҳ</A><A title='��1ҳ' href='#'>1</A><A title='��2ҳ' href='#'>2</A><A title='��3ҳ' href='#'>3</A><A title='��4ҳ' href='#'>4</A><A title='��5ҳ' href='#'>5</A><A title='��6ҳ' href='#'>6</A><A title='��7ҳ' href='#'>7</A><A title='��8ҳ' href='#'>8</A><A title='��9ҳ' href='#'>9</A><SPAN class='currentPager' title='��10ҳ'>10</SPAN><A title='��11ҳ' href='#'>...</A><A href='#'>��һҳ</A><A href='#'>βҳ</A></DIV>
";
		}
	}
}