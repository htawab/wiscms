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

namespace Wis.Website.Web
{
    public partial class HtmlEditorTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem item;
            
            item = new Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem();
            item.Text = "1";

            DropdownMenu1.MenuItems.Add(item);
        }
    }
}
