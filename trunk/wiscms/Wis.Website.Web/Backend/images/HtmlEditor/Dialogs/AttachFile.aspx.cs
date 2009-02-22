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
using System.Text.RegularExpressions;

namespace Wis.Website.Web.Admin
{
    /// <summary>
    /// 支持大文件上传。
    /// </summary>
    public partial class AttachFile :System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Pager1.PageIndexChange += new EventHandler(Pager1_PageIndexChange);
            //if(!Page.IsPostBack)
            //    BindFiles();
        }

        void Pager1_PageIndexChange(object sender, EventArgs e)
        {
            //BindFiles();
        }

        // File search
        public const string CreatedBy = "createdby";
        // Common keywords
        public const string Keywords = "keywords";
        public const string StartDate = "startdate";
        public const string EndDate = "enddate";
        public const string PageIndex = "pageindex";
        public const char Splitter = ' '; // use a space as seperator 

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

        //private void BindFiles()
        //{
        //    string searchCondition = " 1=1 ";

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

        //    this.Pager1.RecordCount = BackendLogic.FilesManager.Count(searchCondition);
        //    DataTable dt = BackendLogic.FilesManager.Query(searchCondition, "CreationDate DESC", this.Pager1.PageSize ,this.Pager1.CurrentPageIndex);
        //    // 过滤单引号
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        row["Description"] = row["Description"].ToString().Replace("'", "&#39;").Replace("\"", "&#34;").Replace("\\", "\\\\");
        //        row["SaveAsFileName"] = row["SaveAsFileName"].ToString().Replace("'", "&#39;").Replace("\"", "&#34;").Replace("\\", "\\\\"); // TODO:Test
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
            //string allowExtensions = AllowExtensions;
            //string extension = System.IO.Path.GetExtension(BrowseFile.PostedFile.FileName).ToLower();
            //if (!IsAllowedFileExtension(extension, allowExtensions))
            //{
            //    Alert("上传失败，您只能上传扩展名为" + allowExtensions + "的文件");
            //    return;
            //}

            //// TODO:描述信息?
            //string description = "File upload by HtmlEditor";
            //Guid fileguid = FilesHandle.Upload(BrowseFile.PostedFile, BrowseFile.PostedFile.FileName.Substring(BrowseFile.PostedFile.FileName.LastIndexOf("\\")+1), description);
            //if (!fileguid.Equals(Guid.Empty))
            //{
            //    string filepath = FilesHandle.GetNonDownloadFileFullPath(fileguid);

            //    description = description.Replace("'", "\\'");
            //    string script = "<script language=\"javascript\">\n\twindow.onload = function selectFile() {\n\tableImages\tsetProperties('" + filepath + "', '" + filepath + "');\n\tableImages}\n</script>";
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", script);
            //    Alert("上传成功");

            //    BindFiles();
            //}
            //else
            //{
            //    Alert("上传失败");
            //}
        }

        protected void Search_ServerClick(object sender, EventArgs e)
        {
            //BindFiles();
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

        public static string ReplaceCharacters(string srcStr, string charsToRemove, 
            bool isReplacerValid, char replacer)
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

        /// <summary>
        /// determine to upload file who extension is allowed.
        /// </summary>
        /// <param name="extension">file extension</param>
        /// <param name="allowExtensions">allow extensions list, separate by ';', like "jpg;gif" </param>
        /// <returns></returns>
        public static bool IsAllowedFileExtension(string extension, string allowExtensions)
        {
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException("extension");

            if (extension[0] == '.') // Ingore the leading . if any
                extension = extension.Substring(1);

            return Regex.Match(extension, allowExtensions, RegexOptions.IgnoreCase).Success;
        }

        private static string allowExtensions;
        public static string AllowExtensions
        {
            get
            {

                if (null == allowExtensions)
                {
                    allowExtensions = "pdf;doc;docx;ppt;pptx;jpg;jpeg;gif;png;bmp;tga;tiff;psd;emf;svg;vrml;avi;mpg;mpeg;wmv;asf;wav;mp3;wma;msi;zip;rar";
                    //DataTable dt = DBConstants.Constants.MediaTypesConfig.ConfigTable;
                    //if (0 < dt.Rows.Count)
                    //{
                    //    for (int index = 0; index < dt.Rows.Count; index++)
                    //    {
                    //        DataRow dr = dt.Rows[index];
                    //        allowExtensions += dr[MediaTypes.FileExtensionsStr].ToString().Replace(";", "|");

                    //        if (index < (dt.Rows.Count - 1))
                    //            allowExtensions += "|";
                    //    }
                    //}
                }

                return allowExtensions;
            }

        }
    }
}
