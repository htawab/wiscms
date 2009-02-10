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

namespace Wis.Toolkit.WebControls.HtmlEditorControls
{
	/// <summary>
    /// Control designer of the Html editor for extending the design-mode behavior.
	/// </summary>
    public class HtmlEditorControlDesigner : System.Web.UI.Design.ControlDesigner
    {

		/// <summary>
		/// 
		/// </summary>
        public HtmlEditorControlDesigner() { }

        private HtmlEditor _HtmlEditor;

		/// <summary>
		/// 
		/// </summary>
		/// <param _Name="component"></param>
		public override void Initialize(IComponent component) {
            _HtmlEditor = (HtmlEditor)component;
			base.Initialize(component);
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public override string GetDesignTimeHtml() {
            string html = String.Format("<table width=\"{0}\" height=\"{1}\" bgcolor=\"#f7f7f7\" bordercolor=\"#c7c7c7\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\"><tr><td valign=\"middle\" align=\"center\">Microsoft HtmlEditor V1.0 - <b>{2}</b></td></tr></table>", _HtmlEditor.Width.ToString(), _HtmlEditor.Height.ToString(), _HtmlEditor.ID);
            return html;
		}
	}
}