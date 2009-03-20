<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogList.aspx.cs" Inherits="Wis.Website.Web.Backend.BlogList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>博客管理</title>
<link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.BlogList li.a {width:120px;}
.BlogList li.b {width:255px;}
.BlogList li.c {width:110px;}
.BlogList li.d {width:80px;}
.BlogList li.e {width:45px;}
</style>
</head>
<body style="background:#d6e7f7">
    <form id="BlogArticalListForm" runat="server">
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>

    
    <div class="listBox BlogList">
    <div class="rightBox_in"><label>博客名称：</label><input type="text" /><input type="image" style="height:auto;" src="images/schbtn.gif" /><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterBlogList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                    <li class="a">用户名称</li><li class="b">博客名称</li><li class="c">最后更新时间</li><li class="d">博客状态</li><li class="f">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                    <li class="a">用户名称</li>
                    <li class="b">博客名称</li>
                    <li class="c">2008-12-23 22:44</li>
                    <li class="d">己停用</li>
                    <li class="f Operate"><a href="#">激活</a><a href="BlogInfo.aspx">查看详细信息</a><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                    <li class="a">用户名称</li>
                    <li class="b">博客名称</li>
                    <li class="c">2008-12-23 22:44</li>
                    <li class="d">己停用</li>
                    <li class="f Operate"><a href="#">激活</a><a href="BlogInfo.aspx">查看详细信息</a><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>
