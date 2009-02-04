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
    public partial class iframe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sh = Request.QueryString["heights"];
            select_iframe.InnerHtml = select_iframelist(sh);

        }
        string select_iframelist(string sh)
        {
            string liststr = "";
            string srcstr = "";// Request.ApplicationPath;
            string rq = Request.QueryString["FileType"];
            string arrrq = rq.Split('|')[0];
            switch (arrrq)
            {
                
                case "CategoryList":
                    srcstr += "/Backend/dialog/CategoryList.aspx";
                    break;
                case "pic":
                    srcstr += "/Backend/dialog/SelectFiles.aspx?FileType=pic";
                    break;
                case "file":
                    srcstr += "/Backend/dialog/SelectFiles.aspx?FileType=file";
                    break;
                case "video":
                    srcstr += "/Backend/dialog/SelectFiles.aspx?FileType=video";
                    break;
                case "templet":
                    srcstr += "/Backend/dialog/SelectFiles.aspx?FileType=templet";
                    break;
                case "UploadImage":
                    srcstr += "/Backend/dialog/UploadFile.aspx?FileType=UploadImage";
                    break;
                case "UploadFile":
                    srcstr += "/Backend/dialog/UploadFile.aspx?FileType=UploadFile";
                    break;
                case "UploadVideo":
                    srcstr += "/Backend/dialog/UploadFile.aspx?FileType=UploadVideo";
                    break;
                case "ReleasePath":
                    srcstr += "/Backend/dialog/SelectPath.aspx?Path=Web";
                    break;
                case "videoEdit":
                    srcstr += "/Backend/dialog/VideoEdit.aspx?Path=Web";
                    break;
                case "cutimg":
                    srcstr += "/Backend/dialog/Cutimg.aspx?ImagePath=" + Request.QueryString["ImagePath"] + "&heights=" + 480;
                    liststr += "<iframe src=\"" + srcstr + "\" frameborder=\"0\" id=\"select_main\" scrolling=\"no\" name=\"select_main\" width=\"100%\" height=\"" + sh + "px\" />";
                    return liststr;
                default:
                    break;
            }
            liststr += "<iframe src=\"" + srcstr + "\" frameborder=\"0\" id=\"select_main\" scrolling=\"yes\" name=\"select_main\" width=\"100%\" height=\"" + sh + "px\" />";
            return liststr;
        }
    }
}
