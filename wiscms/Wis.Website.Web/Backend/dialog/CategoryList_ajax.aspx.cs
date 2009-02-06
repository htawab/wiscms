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

namespace Wis.Website.Web.Backend.dialog
{
    public partial class CategoryList_ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.CacheControl = "no-cache";
                Response.Expires = 0;
                newsList.InnerHtml = newsstr();
            }
        }
        string newsstr()
        {
            string ParentId = Request.QueryString["ParentId"];
            string liststr = string.Empty;
            if (string.IsNullOrEmpty(ParentId))
                ParentId = "0";
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            //Guid parentGuid;
            //if (!Wis.Toolkit.Validator.IsGuid(ParentId))
            //    ParentId = Guid.Empty.ToString();
            string commandText;
            if (ParentId=="0")
                commandText = string.Format("select * from Category where ParentGuid = '{0}' ", Guid.Empty);
            else
               commandText= string.Format("select * from Category where ParentGuid in (select CategoryGuid from Category where CategoryId ={0}) ", ParentId);
            System.Data.DataTable dt  = dataProvider.ExecuteDataset(commandText).Tables[0];
            foreach(System.Data.DataRow drow in dt.Rows)
            {
                commandText = string.Format("select count(CategoryId) from Category where ParentGuid = '{0}'", drow["CategoryGuid"].ToString());
                int o = (int)dataProvider.ExecuteScalar(commandText);
                if (o > 0 )
                {
                    liststr += "<div><img src=\"../../sysImages/normal/b.gif\" alt=\"点击展开子栏目\"  border=\"0\" class=\"LableItem\" onClick=\"javascript:SwitchImg(this,'" + drow["CategoryId"] + "');\" />&nbsp;<span id=\"" + drow["CategoryGuid"] + "\" class=\"LableItem\" ondblclick=\"ReturnValue();\" onClick=\"SelectLable(this);sFiles('" + drow["CategoryGuid"] + "','" + drow["CategoryName"] + "');\">" + drow["CategoryName"] + "</span><div id=\"Parent" + drow["CategoryId"] + "\" class=\"SubItem\" HasSub=\"True\" style=\"height:100%;display:none;\"></div></div>";
                }
                else
                {
                    liststr += "<div><img src=\"../../sysImages/normal/s.gif\" alt=\"没有子栏目\"  border=\"0\" class=\"LableItem\" />&nbsp;<span id=\"" + drow["CategoryGuid"] + "\" class=\"LableItem\" ondblclick=\"ReturnValue();\" onClick=\"SelectLable(this);sFiles('" + drow["CategoryGuid"] + "','" + drow["CategoryName"] + "');\">" + drow["CategoryName"] + "</span></div>";
                }
            }
            dataProvider.Close();
            if (liststr != string.Empty)
                liststr = "Succee|||" + ParentId + "|||" + liststr;
            else
                liststr = "Fail|||" + ParentId + "|||";
            return liststr;
        }
    }
}
