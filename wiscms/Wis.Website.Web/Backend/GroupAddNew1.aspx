<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupAddNew1.aspx.cs" Inherits="Wis.Website.Web.Backend.GroupAddNew1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加用户组 step1</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:#d6e7f7">
    <form id="GroupAddNew1Form" runat="server">
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
    <div><label>用户组名称：</label><asp:TextBox ID="TextBoxGroupName" runat="server" onfocus="cb(this)" onblur="cb2(this)" Width="240"></asp:TextBox><span>4-12位（英文或数字）</span></div>
    <div class="borBot"><label>用户组描述：</label><asp:TextBox ID="TextBoxGrounpDes" runat="server" onfocus="cb(this)" onblur="cb2(this)" TextMode="MultiLine" Rows="3" Width="280"></asp:TextBox><span>4-12位（英文或数字）</span></div>
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
    
  <div class="add_button"><a href="GroupAddNew2.aspx"><img src="images/nextStep.gif" /></a></div>
</form>
</body>
</html>
