using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Wis.Website.Web.Backend
{
    public partial class CategoryAddNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // 添加分类信息
            Wis.Website.DataManager.Category category = new Wis.Website.DataManager.Category();
            Wis.Website.DataManager.CategoryManager categoryManager = new Wis.Website.DataManager.CategoryManager();

            // 1 判断分类名称
            if (string.IsNullOrEmpty(TextBoxCategoryName.Text.Trim()))
            {
                return;
            }

            // 2 判断排序号
            int rank = 0;
            if (int.TryParse(TextBoxCategoryRank.Text.Trim(), out rank) == false)
            {
                return;
            }

            // 3 入库
            category.CategoryName = TextBoxCategoryName.Text;
            category.Rank = rank;
            categoryManager.AddNew(category);
        }
    }
}
