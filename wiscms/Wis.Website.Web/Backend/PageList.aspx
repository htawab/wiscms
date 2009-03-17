<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageList.aspx.cs" Inherits="Wis.Website.Web.Backend.PageList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>单页管理</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.PageList ul li.a {width:135px;}
.PageList ul li.b {width:235px;}
.PageList ul li.c {width:120px;}
.PageList ul li.d {width:120px;}
.PageList ul li.d {width:100px;}
.PageList ul li.e {}
</style>
</head>
<body style="background:#d6e7f7">
    <form id="PageListForm" runat="server">
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>
    
    
    <div class="listBox PageList">
    <asp:Repeater ID="RepeaterUserList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                    <li class="a">单页名称</li>
                    <li class="b">单页描述</li>
                    <li class="c">发布路径</li>
                    <li class="d">模板路径</li>
                    <li class="e">创建日期</li>
                    <li class="f Operate">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                    <li class="a">联系我们</li>
                    <li class="b">联系方式介绍</li>
                    <li class="c">../../.htm</li>
                    <li class="d">../../.htm</li>
                    <li class="e">2008-8-26 15:55</li>
                    <li class="f Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                    <li class="a">联系我们</li>
                    <li class="b">联系方式介绍</li>
                    <li class="c">../../.htm</li>
                    <li class="d">../../.htm</li>
                    <li class="e">2008-8-26 15:55</li>
                    <li class="f Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>
