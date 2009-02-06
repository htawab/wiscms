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


namespace Wis.Website.Web.Backend.Article
{
    public partial class ArticleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string categoryId = Request["CategoryId"];
                if (!string.IsNullOrEmpty(categoryId))
                {
                    this.ViewState["CategoryId"] = categoryId;
                    Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
                    dataProvider.Open();
                    string commandtext = string.Format("select CategoryName from Category where CategoryId ={0}", categoryId);
                    object o = dataProvider.ExecuteScalar(commandtext);
                    daohang.InnerText = o.ToString();
                    this.CategoryId.Value = categoryId;
                    this.CategoryName.Value = o.ToString();
                    dataProvider.Close();
                }
            }
        }
    }
}
