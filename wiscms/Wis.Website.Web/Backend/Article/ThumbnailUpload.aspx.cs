using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Wis.Toolkit.WebControls.FileUploads;

namespace Wis.Website.Web.Backend.Article
{
    public partial class ThumbnailUpload : System.Web.UI.Page
    {
        private string OutputPath
        {
            get 
            {
                string outputPath = string.Format("~/Uploads/Thumbnail/{0}/", System.DateTime.Now.ToShortDateString());
                string physicalOutputPath = Server.MapPath(outputPath);
                if (!System.IO.Directory.Exists(physicalOutputPath)) System.IO.Directory.CreateDirectory(physicalOutputPath);
                return outputPath;
            }
        }

        private string OpenScriptKey = "OpenFull";
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Page.IsPostBack && DJUploadController1.Status != null)
            {
                string applicationPath = string.Format("{0}/{1}", 
                    Page.Request.Url.AbsoluteUri.Substring(0, Page.Request.Url.AbsoluteUri.IndexOf(Page.Request.Path)).TrimEnd('/'),
                    this.Page.Request.ApplicationPath.TrimStart('/')).TrimEnd('/');

                foreach (UploadedFile f in DJUploadController1.Status.UploadedFiles)
                {
                    // f.FileName "E:\\Tools\\visualxpath.zip"
                    string fileName = f.FileName;
                    int charIndex = fileName.LastIndexOf("\\");
                    if (charIndex > -1 && charIndex < fileName.Length)
                    {
                        // 上传的文件
                        fileName = fileName.Substring(charIndex + 1);
                        if (!this.Page.ClientScript.IsStartupScriptRegistered(OpenScriptKey))
                        {
                            // http://localhost:3419//Backend/Article/Thumbnail.aspx?thumbnailPath=~/Uploads/Thumbnail/2009-2-24/1708138d.jpg
                            string scriptBlock = string.Format("\n<script language='JavaScript' type='text/javascript'><!--\nparent.OpenFull('{0}/Backend/Article/Thumbnail.aspx?ImagePath={1}{2}');\n//--></script>\n", applicationPath, this.OutputPath.TrimStart('~'), fileName);
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), OpenScriptKey, scriptBlock);
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the default processor
            FileSystemProcessor fs = new FileSystemProcessor();
            fs.OutputPath = Server.MapPath(this.OutputPath);
            DJUploadController1.DefaultFileProcessor = fs;
        }
    }
}
