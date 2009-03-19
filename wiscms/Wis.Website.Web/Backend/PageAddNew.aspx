<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageAddNew.aspx.cs" Inherits="Wis.Website.Web.Backend.PageAddNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>增加单页</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body  style="background:#d6e7f7">
    <form runat="server" id="PageAddNewForm">
    
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
   <div class="blogEdit">
    <div><label>单页标题：</label><asp:TextBox ID="TextBoxCategoryName" runat="server"></asp:TextBox></div>
    <div><label>Meta关键字：</label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
    <div><label>Meta描述：</label><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></div>
    <div><label style="float:left;">博客内容：</label><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></div>
    <div class="borBot"><label>发布：</label><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></div>  
   </div>


   
  </div>
    
<div class="add_button"><a href="#"><img src="images/StepDone.gif" /></a></div>
    
    
    </form>
</body>
</html>