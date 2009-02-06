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
    public partial class CategoryAdd : System.Web.UI.Page
    {
        Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");

                //dataProvider.Open();
                //string commandText = string.Format("select * from Category where ParentId is");
                //System.Data.IDataReader dr = dataProvider.ExecuteReader(commandText);
                //this.ParentId.Items.Add(new ListItem("  ", "0"));
                //while (dr.Read())
                //{ 
                //    this.ParentId.Items.Add(new ListItem(dr["CategoryName"].ToString(), dr["CategoryId"].ToString()));
                //}
                //ParentId.SelectedIndex = 0;
                //dr.Close();
                //dataProvider.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CategoryName.Value))
            {
                ViewState["javescript"] = string.Format("alert('名称不能为空!');");
                return;
            }
            if (string.IsNullOrEmpty(this.ParentGuid.Value))
                this.ParentGuid.Value = Guid.Empty.ToString();
            if (!Wis.Toolkit.Validator.IsGuid(this.ParentGuid.Value))
            {
                ViewState["javescript"] = string.Format("alert('父栏目编号不对!');");
                return;
            }
            if (!Wis.Toolkit.Validator.IsInt(this.Rank.Value))
            {
                ViewState["javescript"] = string.Format("alert('等级类型不对!');");
                return;
            }
            dataProvider.Open();
            string commandText = string.Format("select count(CategoryId) from Category where ParentGuid ='{0}' and CategoryName=N'{1}'", this.ParentGuid.Value, this.CategoryName.Value.Replace("'", "\""));
            int count = (int)dataProvider.ExecuteScalar(commandText);
            if (count > 0)
            {
                ViewState["javescript"] = string.Format("alert('此栏目已存在!');");
                dataProvider.Close();
                return;
            }
            commandText = string.Format("insert into Category(CategoryName,ParentGuid,Rank,TemplatePath,ReleasePath) values(N'{0}','{1}',{2},N'{3}',N'{4}')", this.CategoryName.Value.Replace("'", "\""), ParentGuid.Value, Rank.Value, this.TemplatePath.Value, ReleasePath.Value);
            dataProvider.ExecuteNonQuery(commandText);
            dataProvider.Close();
            Response.Redirect("CategoryList.aspx");
        }
    }
}
