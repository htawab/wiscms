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
    public partial class TemplateLabelAddNew : System.Web.UI.Page
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

            Guid tagGuid = Guid.NewGuid();

            dataProvider.Open();
            string commandText = string.Format("select count(TemplateLabelId) from TemplateLabel where TemplateLabelName =N'{0}'", TagName.Value.Trim().Replace("'", "\""));

            int count = (int)dataProvider.ExecuteScalar(commandText);
            if (count > 0)
            {
                ViewState["javescript"] = string.Format("alert('标签名称重复!');");
                dataProvider.Close();
                return;
            }
            try
            {

                commandText = string.Format(@"insert into TemplateLabel
(TemplateLabelName,Description,TemplateLabelValue) values 
(N'{0}',N'{1}',N'{2}')", TagName.Value.Trim().Replace("'", "\""), Description.Value.Replace("'", "\""), ContentHtml.Value.Replace("'", "\""));

                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                //生成静态页面

                Response.Redirect("TemplateLabelList.aspx");
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
