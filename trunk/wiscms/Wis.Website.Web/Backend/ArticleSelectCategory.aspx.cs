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
    public partial class ArticleSelectCategory : System.Web.UI.Page
    {
        Wis.Website.DataManager.CategoryManager categoryManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (categoryManager == null) categoryManager = new Wis.Website.DataManager.CategoryManager();
            DropdownMenuCategory.MenuItems = categoryManager.GetCategoryMenuItems();
            if (!Page.IsPostBack)
            {
                string requestCategoryGuid = Request.QueryString["CategoryGuid"];
                if (!string.IsNullOrEmpty(requestCategoryGuid) && Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
                {
                    Guid categoryGuid = new Guid(requestCategoryGuid);
                    Wis.Website.DataManager.Category category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
                    if (!string.IsNullOrEmpty(category.CategoryName))
                    {
                        DropdownMenuCategory.Text = category.CategoryName;
                        DropdownMenuCategory.Value = category.CategoryGuid.ToString();
                    }
                }
            }
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // 选择分类，进入下一步

            // 获取分类的信息
            if (string.IsNullOrEmpty(DropdownMenuCategory.Value) || !Wis.Toolkit.Validator.IsGuid(DropdownMenuCategory.Value))
            {
                Warning.InnerHtml = "请选择分类";
                return;
            }

            Guid categoryGuid = new Guid(DropdownMenuCategory.Value);
            Wis.Website.DataManager.Category category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
            if (string.IsNullOrEmpty(category.CategoryName)) // 没有读取到分类信息
            {
                Warning.InnerHtml = "未读取到分类信息，请选择分类";
                return;
            }

            // 下一步
            Response.Redirect(string.Format("ArticleAddNew.aspx?CategoryGuid={0}", DropdownMenuCategory.Value));
        }
    }
}
