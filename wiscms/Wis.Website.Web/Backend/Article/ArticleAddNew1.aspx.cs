using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Wis.Toolkit;
using Wis.Website.DataManager;
using Wis.Toolkit.WebControls.FileUploads;

namespace Wis.Website.Web.Backend.Article
{ 
    public partial class ArticleAdd : System.Web.UI.Page
    {
        private const string CallScriptKey = "CallMessageBox";
        /// <summary>
        /// 输出标题和消息。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="message">消息。</param>
        public void MessageBox(string title, string message)
        {
            if (!this.Page.ClientScript.IsStartupScriptRegistered(CallScriptKey))
            {
                string scriptBlock = string.Format("\n<script language='JavaScript' type='text/javascript'><!--\nMessageBox.init('{0}', '{1}');\n//--></script>\n", title, message);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), CallScriptKey, scriptBlock);
            }
        }

        private Guid articleGuid = Guid.NewGuid();
        /// <summary>
        /// 文章编号。
        /// </summary>
        public Guid ArticleGuid
        {
            get { return articleGuid; }
        }

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
                        // 切割图片
                        string requestPointX = Request["PointX"];
                        string requestPointY = Request["PointY"];//Image
                        if (!string.IsNullOrEmpty(requestPointX) && !string.IsNullOrEmpty(requestPointY))
                        {
                            // 上传的文件
                            fileName = fileName.Substring(charIndex + 1);
                            
                            // TODO:文件格式判断

                            string srcFilename = string.Format("{1}{2}", this.OutputPath.TrimStart('~'), fileName);
                            string destFilename = "";

                            // PointX 和 PointY都不为空，则进行图片裁剪
                            int pointX;
                            int pointY;
                            if (int.TryParse(requestPointX, out pointX) == false || int.TryParse(requestPointY, out pointY) == false)
                            {
                                MessageBox("错误提示", "缩略图的PointX和PointY不为整数");
                                return;
                            }

                            // 裁剪图片
                            int cropperWidth = category.ImageWidth.Value;
                            int cropperHeight = category.ImageHeight.Value;
                            Wis.Toolkit.Drawings.ImageCropper.Crop(srcFilename, destFilename, pointX, pointY, cropperWidth, cropperHeight);

                            Wis.Website.DataManager.FileManager fileManager = new Wis.Website.DataManager.FileManager();
                            Wis.Website.DataManager.File file = new Wis.Website.DataManager.File();
                            file.CreatedBy = string.Empty; // TODO:填写当前登录用户的UserName
                            file.CreationDate = System.DateTime.Now;
                            file.Description = string.Empty; // TODO：如何提供描述？
                            file.FileGuid = Guid.NewGuid();
                            file.Hits = 0;
                            file.OriginalFileName = fileName;
                            file.Rank = 0;
                            file.SaveAsFileName = string.Format("{1}{2}", this.OutputPath.TrimStart('~'), fileName);
                            System.IO.FileInfo saveAsFileInfo = new FileInfo(Server.MapPath(file.SaveAsFileName));
                            file.Size = saveAsFileInfo.Length;
                            file.SubmissionGuid = this.ArticleGuid;
                            fileManager.AddNew(file);
                        }
                    }
                }
            }
        }

        private Wis.Website.DataManager.Category category = null;
        private Wis.Website.DataManager.CategoryManager categoryManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            FileSystemProcessor fs = new FileSystemProcessor();
            fs.OutputPath = Server.MapPath(this.OutputPath);
            DJUploadController1.DefaultFileProcessor = fs;

            string requestCategoryGuid = Request.QueryString["CategoryGuid"];
            if(categoryManager == null) categoryManager = new Wis.Website.DataManager.CategoryManager();
            DropdownMenuCategory.MenuItems = categoryManager.GetCategoryMenuItems();

            if (!Page.IsPostBack)
            {
                // 获取分类的信息
                if (Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
                {
                    Guid categoryGuid = new Guid(requestCategoryGuid);
                    category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
                    if (!string.IsNullOrEmpty(category.CategoryName))
                    {
                        DropdownMenuCategory.Text = category.CategoryName;
                        DropdownMenuCategory.Value = category.CategoryGuid.ToString();
                    }
                }

                // 提交表单前检测
                this.btnOK.Attributes.Add("onclick", "javascript:return CheckArticle();");
            }

            // 管理所在位置 MySiteMapPath
            List<KeyValuePair<string, Uri>> nodes = new List<KeyValuePair<string, Uri>>();
            if (category != null)
            {
                nodes.Add(new KeyValuePair<string, Uri>(category.CategoryName, new Uri(Request.Url, string.Format("ArticleList.aspx?CategoryGuid={0}", category.CategoryGuid))));
            }
            nodes.Add(new KeyValuePair<string, Uri>("新增内容", Request.Url));
            ((Wis.Toolkit.SiteMapDataProvider)SiteMap.Provider).Stack(nodes);
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            // TODO:
            Wis.Website.DataManager.Article article = new Wis.Website.DataManager.Article();
            // article.ArticleId 数据库自动生成

            // 内容类型 ArticleType
            // TODO:支持图片、视频和软件
            if (ArticleType0.Checked) article.ArticleType = Wis.Website.DataManager.ArticleType.Normal;
            if (ArticleType1.Checked) // 图片新闻
            {
                string requestPhoto = Request["Photo"];
                if (string.IsNullOrEmpty(requestPhoto))
                {
                    MessageBox("错误提示", "未读取到缩略图信息");
                    return;
                }
                //article.ImagePath = this.ImagePath.Value;
                article.ArticleType = Wis.Website.DataManager.ArticleType.Image;
            }
            if (ArticleType2.Checked)
            {
                article.ImagePath = this.TabloidPathVideo.Value;
                article.ArticleType = Wis.Website.DataManager.ArticleType.Video;
            }
            if (ArticleType3.Checked)
            {
                // TODO:article.ImagePath = this.TabloidPathVideo.Value;
                article.ArticleType = Wis.Website.DataManager.ArticleType.Soft;
            }

            // 
            
            // TODO:


            
            


            

            // TODO:需要事务处理，如果生成页面失败，那新增新闻也失败
            // 生成静态页面和关联页面
            DataManager.ReleaseManager releaseManager = new DataManager.ReleaseManager();
            releaseManager.ReleaseRelation(article);


            // 跳转
            Response.Redirect("ArticleAdd.aspx?CategoryGuid=" + DropdownMenuCategory.Value);
        }
    }
}