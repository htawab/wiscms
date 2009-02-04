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
    public partial class PageAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);

            Guid pageGuid = Guid.NewGuid();

            if (!Wis.Toolkit.Validator.IsInt(CategoryId.Value))
                CategoryId.Value = "null";
            string commandText = string.Format("select Count(PageId) from Page where Title =N'{0}'", title.Value);
            dataProvider.Open();
            int o = (int)dataProvider.ExecuteScalar(commandText);
            if (o > 0)
            {
                ViewState["javescript"] = string.Format("alert('页面标题不能重复!');");
                dataProvider.Close();
                return;
            }
            try
            {
                commandText = string.Format(@"insert into Page
(PageGuid,CategoryId,MetaKeywords,MetaDesc,Title,ContentHtml,TemplatePath,ReleasePath) values 
('{0}',{1},'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}')", pageGuid, CategoryId.Value, MetaKeywords.Value.Replace("'", "\""), MetaDesc.Value.Replace("'", "\""),
                                   this.title.Value.Replace("'", "\""), ContentHtml.Value.Replace("'", "\""), TemplatePath.Value, ReleasePath.Value);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                //生成静态页面
               //
            }
            catch
            {
                ViewState["javescript"] = string.Format("alert('添加失败!');");
                if (!dataProvider.IsClosed) dataProvider.Close();
                return;
            }
            Wis.Website.Logger.LoggerInsert(Guid.NewGuid(), "添加页面 " + this.title.Value.Replace("'", "\""), pageGuid, this.ContentHtml.Value.Replace("'", "\""));
            Response.Redirect("PageList.aspx");
            return;

        }
    }
}

