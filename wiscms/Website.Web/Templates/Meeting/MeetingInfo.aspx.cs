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

using Wis.Website;
namespace Wis.Website.Web.Templates
{
    public partial class MeetingInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string eformsn = Request["eformsn"];
                if (Wis.Toolkit.Validator.IsInt(eformsn))
                {
                    Response.Write("<script language='javascript'>alert ('编号错误!');window.close();</script>");
                    return;
                }
                Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.MeetingConnectionString);
                dataProvider.Open();
                string commandText = string.Format("Select  * from wd_75 where eformsn = {0}", eformsn);
                System.Data.DataTable dt = dataProvider.ExecuteDataset(commandText).Tables[0];
                if (dt == null)
                {
                    Response.Write("<script language='javascript'>alert ('数据不存在!');window.close();</script>");
                    return;
                }
                if (dt.Rows[0]["txtTitle"] != null)
                    titleId.InnerHtml = dt.Rows[0]["txtTitle"].ToString();
                if (dt.Rows[0]["txtTitle"] != null)
                    titleId.InnerHtml = dt.Rows[0]["txtTitle"].ToString();
            }
        }
        public void getMettingInfo()
        { 
          
        }
    }
}
