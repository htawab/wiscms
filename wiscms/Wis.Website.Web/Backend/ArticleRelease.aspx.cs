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
    public partial class ArticleRelease : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // 生成静态页面和关联页面
            //DataManager.ReleaseManager releaseManager = new DataManager.ReleaseManager();
            //releaseManager.ReleaseRelation(article);

            // 跳转
            // TODO:继续添加新闻，还是返回新闻列表页？可以在页面上放一个选项框，让用户选择
            //Response.Redirect("ArticleAdd.aspx?CategoryGuid=" + DropdownMenuCategory.Value);
        }
    }
}
