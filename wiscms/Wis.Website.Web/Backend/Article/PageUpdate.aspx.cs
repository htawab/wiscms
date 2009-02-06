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
    public partial class PageUpdate : System.Web.UI.Page
    {
        Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
        string pageId;
        protected void Page_Load(object sender, EventArgs e)
        {
            pageId = Request["PageId"];
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");
                if (!Wis.Toolkit.Validator.IsInt(pageId))
                {
                    Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                    return;
                }
                getdate();
            }
        }

        protected void getdate()
        {
            string commandtext = string.Format("select * from page where pageId ={0}", pageId);
            dataProvider.Open();
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandtext).Tables[0];
            if (dt.Rows.Count < 1)
            {
                Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                return;
            }
            System.Data.DataRow drow = dt.Rows[0];
            if (Wis.Toolkit.Validator.IsInt(drow["CategoryId"].ToString()))
            {
                commandtext = string.Format("select CategoryName from Category where CategoryId ={0}", drow["CategoryId"].ToString());
                object o = dataProvider.ExecuteScalar(commandtext);
                this.CategoryName.Value = o.ToString();
            }
            dataProvider.Close();
            this.title.Value = drow["Title"].ToString();
            this.pageGuid.Value = drow["PageGuid"].ToString();
            this.CategoryId.Value = drow["CategoryId"].ToString();
            this.MetaKeywords.Value = drow["MetaKeywords"].ToString();
            this.ContentHtml.Value = drow["ContentHtml"].ToString();
            this.TemplatePath.Value = drow["TemplatePath"].ToString();
            this.ReleasePath.Value = drow["ReleasePath"].ToString();
            this.MetaDesc.Value = drow["MetaDesc"].ToString();
            daohang.InnerText = drow["Title"].ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(CategoryName.Value))
                CategoryId.Value = "null";
            Guid pageGuid =   new Guid(this.pageGuid.Value);
            string commandText = string.Format("select Count(PageId) from Page where Title =N'{0}' and PageId <> {1}", title.Value.Replace("'", "\""), pageId);
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
                commandText = string.Format(@"update  Page set CategoryId ={1},MetaKeywords=N'{2}',MetaDesc=N'{3}',Title=N'{4}',
ContentHtml =N'{5}',TemplatePath =N'{6}',ReleasePath =N'{7}' where PageId ={0} ", pageId, CategoryId.Value, MetaKeywords.Value.Replace("'", "\""), MetaDesc.Value.Replace("'", "\""),
                                   this.title.Value.Replace("'", "\""), ContentHtml.Value.Replace("'", "\""), TemplatePath.Value, ReleasePath.Value);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                //生成静态页面
                //
            }
            catch
            {
                ViewState["javescript"] = string.Format("alert('修改失败!');");
                if (!dataProvider.IsClosed) dataProvider.Close();
                return;
            }
            Wis.Website.Logger.LoggerInsert(Guid.NewGuid(), "修改页面 " + this.title.Value.Replace("'", "\""), pageGuid, this.ContentHtml.Value.Replace("'", "\""));
            Response.Redirect("PageList.aspx");
            return;

        }
 
    }
}
