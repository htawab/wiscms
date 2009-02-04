using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace Wis.Website.Web.Admin
{
    public partial class InsertImage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Pager1.PageIndexChange += new EventHandler(Pager1_PageIndexChange);
            //if (!Page.IsPostBack)
            //    BindPhotos();
        }

        void Pager1_PageIndexChange(object sender, EventArgs e)
        {
            //BindPhotos();
        }

        // Common keywords
        public const string Keywords = "keywords";
        public const string StartDate = "startdate";
        public const string EndDate = "enddate";
        public const string PageIndex = "pageindex";
        public const string CreatedBy = "createdby";
        public const char Splitter = ' '; // use a space as seperator        
        public static string NormalizeSplitters(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            string newStr = ReplaceCharacters(
                                str,
                                "?:/|\\*'\"<>，.。：;；~`!",
                                true,
                                Splitter).Trim();
            return newStr;
        }

        public static string ReplaceCharacters(
    string srcStr,
    string charsToRemove,
    bool isReplacerValid,
    char replacer)
        {
            if (srcStr == null || srcStr.Length == 0)
                return string.Empty;
            if (charsToRemove == null || charsToRemove.Length == 0)
                return srcStr;

            StringBuilder sb = new StringBuilder();
            foreach (char s in srcStr)
            {
                bool foundMatch = false;
                foreach (char r in charsToRemove)
                {
                    if (s == r)
                    {
                        foundMatch = true;
                        break;
                    }
                }

                if (foundMatch && isReplacerValid)
                    sb.Append(replacer);
                else
                    sb.Append(s);
            }
            return sb.ToString();
        }


        //从数据库读取
        //private void BindPhotos()
        //{
        //    string searchCondition = " MediaType=30";
            
        //    string createby = Request[CreatedBy];
        //    string startdate = Request[StartDate];
        //    string enddate = Request[EndDate];
        //    string keywords = Request[Keywords];
        //    if (string.IsNullOrEmpty(startdate) == false)
        //    {
        //        if (IsDateTime(startdate) == false)
        //        {
        //            Alert("Upload Time格式不正确，示例：" + System.DateTime.Now.ToString("yyyy/M/d"));
        //            return;
        //        }
        //    }
        //    if (string.IsNullOrEmpty(enddate) == false)
        //    {
        //        if (IsDateTime(enddate) == false)
        //        {
        //            Alert("Upload Time格式不正确，示例：" + System.DateTime.Now.ToString("yyyy/M/d"));
        //            return;
        //        }
        //    }

        //    // CreatedBy
        //    if (!string.IsNullOrEmpty(createby))
        //        searchCondition += string.Format(" and CONTAINS(CreatedBy, N'\"{0}\"')", createby); ;

        //    if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
        //    {
        //        searchCondition += string.Format(" and CreationDate BETWEEN '{0}' AND '{1}'", startdate, enddate);
        //    }
        //    if (!string.IsNullOrEmpty(startdate) && string.IsNullOrEmpty(enddate))
        //    {
        //        searchCondition += string.Format(" and CreationDate >= '{0}'", startdate);
        //    }
        //    if (string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
        //    {
        //        searchCondition += string.Format(" and CreationDate <= '{0}'", enddate);
        //    }

        //    if (!string.IsNullOrEmpty(keywords))
        //    {
        //        keywords = NormalizeSplitters(keywords);
        //        string[] split = keywords.Split(Splitter);

        //        StringBuilder sb = new StringBuilder();

        //        sb.Append(" and CONTAINS(*, N'");
        //        for (int index = 0; index < split.Length; index++)
        //        {
        //            if (index > 0)
        //                sb.Append(" AND");

        //            sb.Append(string.Format(null, "\"{0}\"", split[index].Trim()));
        //        }
        //        sb.Append("')");

        //        searchCondition += sb.ToString();
        //    }

        //    this.Pager1.RecordCount= BackendLogic.FilesManager.Count(searchCondition);

        //    DataTable dt = BackendLogic.FilesManager.Query(searchCondition, "CreationDate DESC", this.Pager1.PageSize , this.Pager1.CurrentPageIndex);
        //    // 过滤单引号
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        row["Description"] = row["Description"].ToString().Replace("'", "&#39;").Replace("\"", "&#34;").Replace("\\", "\\\\"); 
        //    }

        //    ImageList.DataSource = dt;
        //    ImageList.DataBind();
        //}


        /// <summary>
        /// 上传文件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(BrowseFile.PostedFile.FileName))
            //{
            //    Alert("请浏览文件");
            //    return;
            //}
            //string allowExtensions = Msra.BackendLogic.MediaTypesManager.Load(30).FileExtensions.Replace(";", "|");
            //string extension = System.IO.Path.GetExtension(BrowseFile.PostedFile.FileName).ToLower();
            //if(!Utils.StringUtility.IsAllowedFileExtension(extension, allowExtensions))
            //{
            //    Alert("上传失败，您只能上传扩展名为" + allowExtensions + "的文件");
            //}

     
            //string description = "";
            //Guid fileguid = FilesHandle.Upload(BrowseFile.PostedFile, 30, BrowseFile.PostedFile.FileName.Substring(BrowseFile.PostedFile.FileName.LastIndexOf("\\")+1), description, FileUploadType.Nondownload);
            //if (!fileguid.Equals(Guid.Empty))
            //{
            //    string filepath = FilesHandle.GetNonDownloadFileFullPath(fileguid);
            //    // alt, border, borderColor, vspace, hspace, filter, align, width, height

            //    description = description.Replace("'", "\\'"); // TODO: Test '
            //    string script = "<script language=\"javascript\">\n\twindow.onload = function selectImage() {\n\t\tsetProperties('" + filepath + "', '" + description + "');\n\t}\n</script>";
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", script);
            //    Alert("上传成功");
            //    BindPhotos();
            //}
            //else
            //{
            //    Alert("上传失败");
            //}
        }

        protected void Search_ServerClick(object sender, EventArgs e)
        {
            //BindPhotos();
        }

        public static bool IsDateTime(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            try
            {
                DateTime.Parse(str, System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static void Alert(string message)
        {
            const int maxMessageLen = 255;

            if (string.IsNullOrEmpty(message))
                message = "";

            if (message.Length > maxMessageLen)
            {
                message = message.Substring(0, maxMessageLen);
            }

            // Remove unwanted characters
            message = ReplaceCharacters(message, "\r\n", false, '\0');
            message = message.Replace("\"", "\\\"");

            HttpContext.Current.Response.Write("<script>\n");
            HttpContext.Current.Response.Write(" window.alert(\"" + message + "\");\n");
            HttpContext.Current.Response.Write("</script>\n");
        }
    }
}
