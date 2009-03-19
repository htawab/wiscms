<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleAddNew.aspx.cs" Inherits="Wis.Website.Web.Backend.RoleAddNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>角色修改</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body  style="background:#d6e7f7">
    <form runat="server" id="RoleAddNewForm">
    
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>
  
  <div class="listBox">
   <div class="userEdit">
    <div><label>角色名称：</label><asp:TextBox ID="TextBox4" onfocus="cb(this)" onblur="cb2(this)" runat="server"></asp:TextBox><span>4-12位（英文或数字）</span></div>
    <div><label>角色描述：</label><asp:TextBox ID="TextBox5" onfocus="cb(this)" onblur="cb2(this)" runat="server"></asp:TextBox><span>4-12位（英文或数字）</span></div>
  </div>
   
<script>
//改变背景色
function cb(iobj) {
var iDiv = iobj.parentNode;
iDiv.style.background="#f5f5ff";
}
function cb2(iobj) {
var iDiv = iobj.parentNode;
iDiv.style.background="#fafafa";
}
</script>

   
  </div>
    
<div class="add_button"><a href="#"><img src="images/StepDone.gif" /></a></div>
    
    
    </form>
</body>
</html>