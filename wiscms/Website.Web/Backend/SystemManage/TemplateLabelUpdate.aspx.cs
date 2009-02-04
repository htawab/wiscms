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
    public partial class TemplateLabelUpdate : System.Web.UI.Page
    {
        Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
        string tagId;
        protected void Page_Load(object sender, EventArgs e)
        {
            tagId = Request["TagId"];
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");
                if (!Wis.Toolkit.Validator.IsInt(tagId))
                {
                    Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                    return;
                }
                getdate();
            }
        }
        protected void getdate()
        {
            string commandtext = string.Format("select * from TemplateLabel where TemplateLabelId ={0}", tagId);
            dataProvider.Open();
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandtext).Tables[0];
            dataProvider.Close();
            if (dt.Rows.Count < 1)
            {
                Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                return;
            }
            System.Data.DataRow drow = dt.Rows[0];
            this.TagName.Value = drow["TemplateLabelName"].ToString();
            this.Description.Value = drow["Description"].ToString();
            this.ContentHtml.Value = drow["TemplateLabelValue"].ToString();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            string commandText = string.Format("select Count(TemplateLabelId) from TemplateLabel where TemplateLabelName =N'{0}' and TemplateLabelId <> {1}", TagName.Value.Replace("'", "\""), tagId);
            dataProvider.Open();
            int o = (int)dataProvider.ExecuteScalar(commandText);
            if (o > 0)
            {
                ViewState["javescript"] = string.Format("alert('标签标题不能重复!');");
                dataProvider.Close();
                return;
            }
            try
            {
                commandText = string.Format(@"update  TemplateLabel set TemplateLabelName=N'{1}',Description=N'{2}',TemplateLabelValue=N'{3}' where TemplateLabelId ={0} ", tagId,
                                   this.TagName.Value.Replace("'", "\""), Description.Value.Replace("'", "\""), ContentHtml.Value.Replace("'", "\""));
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                //生成静态页面

                //
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
