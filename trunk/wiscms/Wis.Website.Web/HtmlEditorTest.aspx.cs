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
using System.Collections.Generic;
using Wis.Website.DataManager;

namespace Wis.Website.Web
{
    public partial class HtmlEditorTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem item;
            
            //item = new Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem();
            //item.Text = "1";

            //DropdownMenu1.MenuItems.Add(item);

            //Wis.Toolkit.ClientScript.MessageBox mb = new Wis.Toolkit.ClientScript.MessageBox(this.Page, "呵呵", "出现错误");
            //mb.Call();
            //((Wis.Toolkit.SiteMapDataProvider)SiteMap.Provider).Stack(category.CategoryName, string.Format("ArticleList.aspx?CategoryGuid={0}", category.CategoryGuid));
            List<KeyValuePair<string, Uri>> nodes = new List<KeyValuePair<string, Uri>>();
            nodes.Add(new KeyValuePair<string, Uri>("Dynamic Content", new Uri(Request.Url, "Default.aspx?id=")));
            nodes.Add(new KeyValuePair<string, Uri>(Request["id"], Request.Url));
            ((Wis.Toolkit.SiteMapDataProvider)SiteMap.Provider).Stack(nodes);

            ReleaseManager releaseManager = new ReleaseManager();
            Response.Write(releaseManager.ReleasePager(3, 100, 19));
        }
    }
}
