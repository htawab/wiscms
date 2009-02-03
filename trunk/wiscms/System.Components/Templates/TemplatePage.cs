//------------------------------------------------------------------------------
// <copyright file="TemplatePage.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates
{
	/// <summary>
	/// TemplatePage ��ժҪ˵����
	/// </summary>
	public class TemplatePage : System.Web.UI.Page
	{
		private Templates.TemplateManager templateManager;
		/// <summary>
		/// 
		/// </summary>
		public Templates.TemplateManager TemplateManager
		{
			get
			{
                if (templateManager == null)
                {
                    templateManager = new TemplateManager();

                    templateManager.SetVariable("Server", this.Server);
                    templateManager.SetVariable("Session", this.Session);
                    templateManager.SetVariable("Application", this.Application);
                    templateManager.SetVariable("Request", this.Request);
                    templateManager.SetVariable("Response", this.Response);
                    templateManager.SetVariable("User", this.User);
                }

				 return templateManager;
			}
			set { templateManager = value;}
		}

		protected void ProcessTemplate(System.Web.UI.HtmlTextWriter writer, string templateData)
		{
			TemplateManager.TemplateData = templateData; // ָ��ģ������
			writer.Write(TemplateManager.Process()); // ����ģ��
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			using (System.IO.TextWriter tempWriter = new System.IO.StringWriter())
			{
				base.Render(new System.Web.UI.HtmlTextWriter(tempWriter));
				ProcessTemplate(writer, tempWriter.ToString());
			}
		}
	}
}
