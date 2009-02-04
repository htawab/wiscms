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
    public partial class LinkAdd : System.Web.UI.Page
    {
        Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            dataProvider.Open();
            string commandText = string.Format("select count(LinkId) from Link where LinkName=N'{0}'", this.LinkName.Value.Replace("'", "\""));
            int count = (int)dataProvider.ExecuteScalar(commandText);
            if (count > 0)
            {
                ViewState["javescript"] = string.Format("alert('此友情连接已存在!');");
                dataProvider.Close();
                return;
            }
            Guid linkguid = Guid.NewGuid();
           //dataProvider.BeginTransaction();
            try
            {
                commandText = string.Format("insert into Link(LinkGuid,LinkName,LinkUrl,Remark,Logo,Sequence) values(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',{5})", linkguid, this.LinkName.Value.Replace(",", ""), LinkUrl.Value.Replace(",", ""), this.Remark.Value.Replace(",", ""), this.Logo.Value.Replace(",", ""), Sequence.Value.Replace(",", ""));
                dataProvider.ExecuteNonQuery(commandText);
                //if (this.alllanmu.Checked)
                //{

                //    commandText = string.Format(@"select  CategoryGuid from Category where ParentId =0 ");
                //    System.Data.DataTable dt = dataProvider.ExecuteDataset(commandText).Tables[0];
                //    foreach (System.Data.DataRow drow in dt.Rows)
                //    {
                //        commandText = string.Format(@"insert into CategoryLink(LinkGuid,CategoryGuid) values ('{0}','{1}')", linkguid, drow["CategoryGuid"]);
                //        dataProvider.ExecuteNonQuery(commandText);
                //    }
                //}
                //else
                //{
                //    string categoryId = Request.Form["CategoryId"];
                //    if (!string.IsNullOrEmpty(categoryId))
                //    {
                //        string[] categoryIds = categoryId.Split(',');

                //        for (int i = 0; i < categoryIds.Length; i++)
                //        {
                //            if (Wis.Toolkit.Validator.IsInt(categoryIds[i]))
                //            {
                //                commandText = string.Format(@"insert into CategoryLink(LinkGuid,CategoryGuid) values ('{0}',(select CategoryGuid from Category where CategoryId ={1}))", linkguid, categoryIds[i]);
                //                dataProvider.ExecuteNonQuery(commandText);
                //            }
                //        }
                //    }
                //}
                //dataProvider.CommitTransaction();
                dataProvider.Close();
                ///
               
            }
            catch
            {
                ViewState["javescript"] = string.Format("alert('添加失败!');");
                //if (dataProvider.HasTransaction) dataProvider.RollbackTransaction();
                if (!dataProvider.IsClosed) dataProvider.Close();
                return;
            }
            if (Website.WriteHtml.linkhtml())
            { }
            Wis.Website.Logger.LoggerInsert(Guid.NewGuid(), "添加学校连接 " + this.LinkName.Value.Replace("'", "\""), linkguid, this.LinkUrl.Value.Replace("'", "\""));
            Response.Redirect("LinkList.aspx");
            return;
        }

    }
}
