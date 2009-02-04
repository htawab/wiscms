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
    public partial class LinkUpdate : System.Web.UI.Page
    {
        Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
        string linkId;

        protected void Page_Load(object sender, EventArgs e)
        {
            linkId = Request["LinkId"];
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");
                if (!Wis.Toolkit.Validator.IsInt(linkId))
                {
                    Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                    return;
                }
                getdate();
            }
        }
        protected void getdate()
        {
            string commandtext = string.Format("select * from Link where LinkId ={0}", linkId);
            dataProvider.Open();
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandtext).Tables[0];
            dataProvider.Close();
            if (dt.Rows.Count < 1)
            {
                Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                return;
            }
            System.Data.DataRow drow = dt.Rows[0];
            this.LinkName.Value = drow["LinkName"].ToString();
            this.LinkUrl.Value = drow["LinkUrl"].ToString();
            this.Logo.Value = drow["Logo"].ToString();
            this.Sequence.Value = drow["Sequence"].ToString();
            this.Remark.Value = drow["Remark"].ToString();
            this.LinkGuid.Value = drow["LinkGuid"].ToString();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Guid linkGuid = new Guid(LinkGuid.Value);
            string commandText = string.Format("select Count(LinkId) from Link where LinkName =N'{0}' and LinkId <> {1}", LinkName.Value.Replace("'", "\""), linkId);
            dataProvider.Open();
            int o = (int)dataProvider.ExecuteScalar(commandText);
            if (o > 0)
            {
                ViewState["javescript"] = string.Format("alert('名称不能重复!');");
                dataProvider.Close();
                return;
            }
            try
            {
                commandText = string.Format(@"update  Link set LinkName=N'{1}',LinkUrl=N'{2}',Logo=N'{3}',Sequence={4},Remark=N'{5}' where LinkId ={0} ", linkId,
                                   this.LinkName.Value.Replace("'", "\""), LinkUrl.Value, Logo.Value, Sequence.Value, Remark.Value);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
            }
            catch
            {
                ViewState["javescript"] = string.Format("alert('添加失败!');");
                if (!dataProvider.IsClosed) dataProvider.Close();
                return;
            }
            if (Website.WriteHtml.linkhtml())
            { }
            Wis.Website.Logger.LoggerInsert(Guid.NewGuid(), "修改学校连接 " + this.LinkName.Value.Replace("'", "\""), linkGuid, this.LinkUrl.Value.Replace("'", "\""));
            Response.Redirect("LinkList.aspx");
            return;
        }
    }
}
