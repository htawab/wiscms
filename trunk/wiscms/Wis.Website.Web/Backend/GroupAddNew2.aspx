<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupAddNew1.aspx.cs" Inherits="Wis.Website.Web.Backend.GroupAddNew1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加用户组 step2</title>
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
    
     <ul class="top">
      <li><input type="checkbox" /><span>角色名称</span>角色描述</li>
     </ul>
     <ul>
      <li><input type="checkbox" /><span>角色名称</span>角色描述</li>
      <li><input type="checkbox" /><span>角色名称</span>角色描述</li>
      <li><input type="checkbox" /><span>角色名称</span>角色描述</li>
      <li><input type="checkbox" /><span>角色名称</span>角色描述</li>
      <li><input type="checkbox" /><span>角色名称</span>角色描述</li>
      <li><input type="checkbox" /><span>角色名称</span>角色描述</li>
     </ul>
   </div>
 
   
  </div>
    
  <div class="add_button"><a href="GroupAddNew3.aspx"><img src="images/nextStep.gif" /></a></div>
    
    
    </form>
</body>
</html>
