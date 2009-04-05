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

namespace Wis.Website.Web.Backend
{
    public partial class CategoryUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string requestCategoryGuid = Request.QueryString["CategoryGuid"];
            if (string.IsNullOrEmpty(requestCategoryGuid) || !Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
            {
                Warning.InnerHtml = "不正确的分类编号，请<a href='ArticleSelectCategory.aspx'>返回</a>选择分类";
                return;
            }

            Wis.Website.DataManager.CategoryManager categoryManager = new Wis.Website.DataManager.CategoryManager();
            Guid categoryGuid = new Guid(requestCategoryGuid);
            Wis.Website.DataManager.Category category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                Warning.InnerHtml = "未读取到分类信息，请<a href='ArticleSelectCategory.aspx'>返回</a>选择分类";
                return;
            }

            TextBoxCategoryName.Text = category.CategoryName;
            TextBoxCategoryRank.Text = category.Rank.ToString();
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // 修改分类信息
            string requestCategoryGuid = Request.QueryString["CategoryGuid"];
            if (string.IsNullOrEmpty(requestCategoryGuid) || !Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
            {
                Warning.InnerHtml = "不正确的分类编号，请<a href='ArticleSelectCategory.aspx'>返回</a>选择分类";
                return;
            }

            Wis.Website.DataManager.CategoryManager categoryManager = new Wis.Website.DataManager.CategoryManager();
            Guid categoryGuid = new Guid(requestCategoryGuid);
            Wis.Website.DataManager.Category category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                Warning.InnerHtml = "未读取到分类信息，请<a href='ArticleSelectCategory.aspx'>返回</a>选择分类";
                return;
            }

            int rank = 0;
            if (int.TryParse(TextBoxCategoryRank.Text.Trim(), out rank) == false)
            {
                return;
            }

            category.CategoryName = TextBoxCategoryName.Text;
            category.Rank = rank;
            categoryManager.Update(category);
        }
    }
}
