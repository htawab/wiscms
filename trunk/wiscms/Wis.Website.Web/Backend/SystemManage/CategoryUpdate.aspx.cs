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


namespace Wis.Website.Web.Backend.SystemManage
{
    public partial class CategoryUpdate : System.Web.UI.Page
    {
        Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
        string categoryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            categoryId = Request["CategoryId"];
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");
                if (!Wis.Toolkit.Validator.IsInt(categoryId))
                {
                    Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                    return;
                }
                getdate();
            }
        }
        protected void getdate()
        {
            string commandtext = string.Format("select * from Category where CategoryId ={0}", categoryId);
            dataProvider.Open();
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandtext).Tables[0];
             System.Data.DataRow drow = dt.Rows[0];
             string o = "";
             if (drow["ParentGuid"].ToString() != Guid.Empty.ToString())
             {
                 commandtext = string.Format("select CategoryName from Category where ParentGuid = '{0}'", drow["ParentGuid"].ToString());
                  o = dataProvider.ExecuteScalar(commandtext).ToString();
             }
                 dataProvider.Close();
            if (dt.Rows.Count < 1)
            {
                Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                return;
            }
           
            this.ParentGuid.Value = drow["ParentGuid"].ToString();
            this.CategoryName.Value = drow["CategoryName"].ToString();
            this.TemplatePath.Value = drow["TemplatePath"].ToString();
            this.ReleasePath.Value = drow["ReleasePath"].ToString();
            this.ParentName.Value = o;
            //this.NewsTemplatePath.Value = drow["NewsTemplatePath"].ToString();
            //this.NewsReleasePath.Value = drow["NewsReleasePath"].ToString();
            this.Rank.Value = drow["Rank"].ToString();


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string commandText = string.Format("select Count(CategoryId) from Category where CategoryName =N'{0}' and CategoryId <> {1} and ParentGuid='{2}'", CategoryName.Value.Replace("'", "\""), categoryId,ParentGuid.Value);
            dataProvider.Open();
            if (string.IsNullOrEmpty(ParentName.Value))
                ParentGuid.Value = Guid.Empty.ToString();
            int o = (int)dataProvider.ExecuteScalar(commandText);
            if (o > 0)
            {
                ViewState["javescript"] = string.Format("alert('栏目标题不能重复!');");
                dataProvider.Close();
                return;
            }
            try
            {
                commandText = string.Format(@"update  Category set CategoryName=N'{1}',TemplatePath=N'{2}',ReleasePath=N'{3}',Rank={4},ParentGuid ='{5}' where CategoryId ={0} ", categoryId,
                                   this.CategoryName.Value.Replace("'", "\""), TemplatePath.Value, ReleasePath.Value,  Rank.Value,ParentGuid.Value);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                Response.Redirect("CategoryList.aspx");
                return;
            }
            catch
            {
                ViewState["javescript"] = string.Format("alert('添加失败!');");
                if (!dataProvider.IsClosed) dataProvider.Close();
                return;
            }
        }
    }
}
