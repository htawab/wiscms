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




        private Wis.Website.DataManager.Category category = null;
        private Wis.Website.DataManager.CategoryManager categoryManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {

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

            

            // TODO:需要事务处理，如果生成页面失败，那新增新闻也失败

        }
    }
}