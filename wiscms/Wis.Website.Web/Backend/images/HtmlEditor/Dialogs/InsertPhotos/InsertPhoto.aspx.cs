using System;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Wis.Toolkit.WebControls.FileUploads;

namespace Wis.Website.Web.Backend.images.HtmlEditor.Dialogs.InsertPhotos
{
    public partial class InsertPhoto : System.Web.UI.Page
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Page.IsPostBack && DJUploadController1.Status != null)
            {
                StringBuilder sb = new StringBuilder();

                if (DJUploadController1.Status.LengthExceeded)
                {
                    sb.Append("<p style='color:#ff0000'>上传文件超出最大允许尺寸</p>");
                }

                if (!UploadManager.Instance.ModuleInstalled)
                {
                    sb.Append("<p style='color:#ff0000'>上传组件未处理上传事件</p>");
                }

                sb.Append("<div class='up_results'>");
                sb.Append("<h3>消息：</h3>");
                sb.Append("<ul>");

                foreach (UploadedFile f in DJUploadController1.Status.UploadedFiles)
                {
                    sb.Append("<li>");
                    sb.Append(f.FileName);
                    sb.Append("</li>");
                }

                sb.Append("</ul>");

                //sb.Append("<h3>错误消息：</h3>");
                sb.Append("<ul>");

                foreach (UploadedFile f in DJUploadController1.Status.ErrorFiles)
                {
                    sb.Append("<li>");

                    sb.Append(f.FileName);

                    if (f.Identifier != null)
                    {
                        sb.Append(" ID = ");
                        sb.Append(f.Identifier.ToString());
                    }

                    if (f.Exception != null)
                    {
                        sb.Append(" 异常： ");
                        sb.Append(f.Exception.Message);
                    }

                    sb.Append("</li>");
                }

                sb.Append("</ul>");

                sb.Append("</div>");

                ltResults.Text = sb.ToString();
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the default processor
            FileSystemProcessor fs = new FileSystemProcessor();
            fs.OutputPath = Server.MapPath("~/uploadsdefault");
            DJUploadController1.DefaultFileProcessor = fs;

            // Change the file processor and set it's properties.
            FieldTestProcessor fsd = new FieldTestProcessor();
            fsd.OutputPath = Server.MapPath("~/uploads");
            DJFileUpload1.FileProcessor = fsd;
        }
    }
}
