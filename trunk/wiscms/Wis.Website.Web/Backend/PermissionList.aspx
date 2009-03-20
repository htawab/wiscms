<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionList.aspx.cs" Inherits="Wis.Website.Web.Backend.PermissionList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>权限列表</title>
<link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.PermissionList ul li.a {width:112px;}
.PermissionList ul li.b {width:490px;}
</style>
</head>
<body>
    <form id="PermissionListForm" runat="server">
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>
    
    
    <div class="listBox PermissionList">
    <div class="rightBox_in"><label>链接名称：</label><input type="text" /><input type="image" style="height:auto;" src="images/schbtn.gif" /><a href="blogLinkAdd.htm">添加链接</a><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterBlogLinkList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                  <li class="a">权限名称</li><li class="b">权限描述</li><li class="c">创建日期</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                   <li class="a">权限名称</li>
                   <li class="b">权限描述</li>
                   <li class="c">创建日期</li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                   <li class="a">权限名称</li>
                   <li class="b">权限描述</li>
                   <li class="c">创建日期</li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>