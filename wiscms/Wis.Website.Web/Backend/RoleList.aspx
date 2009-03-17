<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="Wis.Website.Web.Backend.RoleLise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>角色管理</title>
<link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.RoleList ul li.a {width:112px;}
.RoleList ul li.b {width:290px;}
.RoleList ul li.c {width:100px;}
.RoleList ul li.d {width:100px;}
</style>
</head>
<body>
    <form id="RoleListForm" runat="server">
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>
    
    
    <div class="listBox RoleList">
    <div class="rightBox_in"><label>角色名称：</label><input type="text" /><input type="button" class="button" value="搜索" /><a href="roleAdd.htm">新增角色</a><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterRoleList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                  <li class="a">角色名称</li>
                  <li class="b">角色描述</li>
                  <li class="d">创建日期</li>
                  <li class="e Operate">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                   <li class="a">角色名称</li>
                   <li class="b">角色描述</li>
                   <li class="d">创建日期</li>
                   <li class="e Operate"><a href="#">修改</a><a href="#">删除</a><a href="####" onclick='picUpLoadBox.init("指派权限");'>指派权限</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                   <li class="a">角色名称</li>
                   <li class="b">角色描述</li>
                   <li class="d">创建日期</li>
                   <li class="e Operate"><a href="#">修改</a><a href="#">删除</a><a href="####" onclick='picUpLoadBox.init("指派权限");'>指派权限</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>