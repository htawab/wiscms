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

namespace Wis.Website.Web.Backend.dialog
{
    public partial class UploadFile : System.Web.UI.Page
    {
        string fileType;
        protected void Page_Load(object sender, EventArgs e)
        {
            fileType = Request["FileType"];
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string localSavedir = "/files";
            string dimmdir = "";
            string allowExtensions = "";
            string images = "jpg|gif|bmp|jpeg";
            string file = "png|doc|docx|xls|xlsx|ppt|pdf|htm|html|rar|zip|asp|txt|exe|iso|msi";
            string video = "swf|avi|mpg|mpeg|mpe|m1v|m2v|mpv2|mp2v|dat|ts|tp|tpr|pva|pss|mp4|m4v|m4p|m4b|3gp|3gpp|3g2|3gp2|ogg|mov|qt|amr|rm|ram|rmvb|rpm|mp3|wma";
            switch (fileType)
            {
                case "UploadImage":
                    dimmdir = "/images";
                    allowExtensions = images;
                    break;
                case "UploadFile":
                    dimmdir = "/file";
                    allowExtensions = file;

                    break;
                case "UploadVideo":
                    dimmdir = "/video";
                    allowExtensions = video;
                    break;
                default:
                    dimmdir = "/orther";
                    break;
            }

            string filePath = FileUpload1.FileName;
            if (!string.IsNullOrEmpty(filePath))
            {
                string extension = System.IO.Path.GetExtension(filePath);
                string fileName = System.IO.Path.GetFileName(filePath);
                if (!Wis.Toolkit.IO.File.IsInAllowExtensions(extension, allowExtensions))
                {
                    ViewState["javescript"] = string.Format("alert('不允许上传扩展名为{0}的文件{1}！');", extension, fileName);
                    return;
                }
                string directory = System.DateTime.Now.Year.ToString()+"-" + System.DateTime.Now.Month.ToString() ;
                string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + localSavedir + dimmdir + "/" + directory;
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                string randnum = Wis.Toolkit.Drawings.TextToImage.CreateRandChars(8);
                path += "/" + randnum + "-" + fileName;
                try
                {
                    FileUpload1.SaveAs(path);
                    FileUpload1.Dispose();
                    path = localSavedir + dimmdir + "/" + directory + "/" + randnum + "-" + fileName;
                    ViewState["javescript"] = "ReturnValue('" + path + "');closefDiv();";
                    return;
                }
                catch
                {
                    ViewState["javescript"] = string.Format("alert('上传失败！');");
                    return;
                }
            }
        }
    }
}
