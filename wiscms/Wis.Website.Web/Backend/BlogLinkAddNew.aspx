<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogLinkAddNew.aspx.cs" Inherits="Wis.Website.Web.Backend.BlogLinkAddNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加链接</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body  style="background:#d6e7f7">
    <form runat="server" id="BlogLinkAddNewForm">
    
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
    <div><label>链接名称：</label><asp:TextBox ID="TextBoxBlogLinkName" runat="server" onfocus="cb(this)" onblur="cb2(this)"></asp:TextBox></div>
    <div><label>链接地址：</label><asp:TextBox ID="TextBoxBlogLinkUrl" runat="server" onfocus="cb(this)" onblur="cb2(this)"></asp:TextBox></div>
    <div><label>RSS地址：</label><asp:TextBox ID="TextBoxRssUrl" runat="server" onfocus="cb(this)" onblur="cb2(this)"></asp:TextBox></div>
    <div class="borBot"><label>链接描述：</label><asp:TextBox ID="TextBoxDescription" runat="server" onfocus="cb(this)" onblur="cb2(this)" TextMode="MultiLine" Rows="3"></asp:TextBox></div>
  
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