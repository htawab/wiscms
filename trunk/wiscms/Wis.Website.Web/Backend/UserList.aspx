<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Wis.Website.Web.Backend.UserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户管理</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.UserListBox ul li.a {width:135px;}
.UserListBox ul li.b {width:90px;}
.UserListBox ul li.c {width:90px;}
.UserListBox ul li.d {width:120px;}
.UserListBox ul li.e {}
.UserListBox ul li span.online {padding-left:12px; display:inline-block; background:url(../images/online_icon.gif) left center no-repeat; *background:url(../images/online_icon.gif) left 3px no-repeat;}
.UserListBox ul li span.offline {padding-left:12px; display:inline-block; background:url(../images/offline_icon.gif) left center no-repeat; *background:url(../images/offline_icon.gif) left 3px no-repeat;}
</style>
</head>
<body style="background:#d6e7f7">
    <form id="UserListForm" runat="server">
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>
    
    
    <div class="listBox UserListBox">
    <div class="rightBox_in"><label>用户名称：</label><input type="text" /><input type="button" value="搜索" class="button" /><a href="userAdd.htm">新增用户</a><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterUserList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                    <li class="a">用户名称</li>
                    <li class="b">用户状态</li>
                    <li class="c">是否在线</li>
                    <li class="d">创建日期</li>
                    <li class="e Operate">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                    <li class="a">3344bage521</li>
                    <li class="b">己激活</li>
                    <li class="c"><span class="online">在线</span></li>
                    <li class="d">2008-8-26 15:55</li>
                    <li class="e Operate"><a href="#">指派角色</a><a href="#">分配用户组</a><a href="#">停用</a><a href="Psw.htm">修改密码</a><a href="userAdd.htm">修改信息</a><a href="#">删除</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                    <li class="a">3344bage521</li>
                    <li class="b">己激活</li>
                    <li class="c"><span class="online">在线</span></li>
                    <li class="d">2008-8-26 15:55</li>
                    <li class="e Operate"><a href="#">指派角色</a><a href="#">分配用户组</a><a href="#">停用</a><a href="Psw.htm">修改密码</a><a href="userAdd.htm">修改信息</a><a href="#">删除</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>
