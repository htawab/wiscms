using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace Everwis.Templates
{
	/// <summary>
	/// TemplateFactory 的摘要说明。
	/// </summary>
	public class TemplateFactory : IHttpHandlerFactory,IRequiresSessionState
	{
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			// 获得模板名称
			//string templateName;
			string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
			if (!applicationPath.EndsWith("/")) applicationPath += "/";
			string rawUrl = context.Request.Path.Replace(applicationPath, "");
		
			//if(System.IO.File.Exists(context.Request.PhysicalPath))
			//{
				return PageParser.GetCompiledPageInstance(url, pathTranslated, context);
			//}
//			else
//			{
//				try
//				{
//					//string typeName = Settings.Setting.GetTypeName(rawUrl);
//					//return (IHttpHandler)Activator.CreateInstance(handlerType);
//				}
//				catch
//				{
//					//return new TemplatePageHandler();
//                    
//				}
//			}
		}

		void IHttpHandlerFactory.ReleaseHandler(IHttpHandler handler)
		{
			
		}
	}
}
