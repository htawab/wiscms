using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using Wis.Toolkit.WebControls.FileUploads;

namespace Wis.Website.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // This sets up the default processor
            UploadManager.Instance.ProcessorType = typeof(DummyProcessor);
            UploadManager.Instance.ProcessorInit += new FileProcessorInitEventHandler(Processor_Init);
        }
        /// <summary>
        /// Initialises the file processor.
        /// </summary>
        /// <param name=\"sender\">Sender</param>
        /// <param name=\"args\">Arguments</param>
        void Processor_Init(object sender, FileProcessorInitEventArgs args)
        {
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Response.Redirect("/error.html");
            //Exception x = Server.GetLastError().GetBaseException();
            ////x.ToString();
            ////Exception x = Server.GetLastError().GetBaseException();
            //string errmsg = x.ToString();
            //Regex re = new Regex(@"文件(.*)不存在");
            //if (re.Match(errmsg).Success)
            //{
            //    //Wiscms.Web.UI.WebHint.ShowError("您所浏览的页面不存在", "/", true);
            //}
            //else
            //{
            //    //Wiscms.Web.UI.WebHint.ShowError(errmsg, "", false);
            //}
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}