<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogLinkList.aspx.cs" Inherits="Wis.Website.Web.Backend.BlogLinkList" %>
<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls" tagprefix="Wis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>博客链接管理</title>
<link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.blogLinkList li.li1 {width:190px;}
.blogLinkList li.li2 {width:340px;}
.blogLinkList li.li3 {width:120px;}
.blogLinkList li img {border:0;}
</style>
</head>
<body>
    <form id="BlogLinkListForm" runat="server">
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>
    
    
    <div class="listBox blogLinkList">
    <div class="rightBox_in"><label>链接名称：</label><input type="text" /><input type="image" style="height:auto;" src="images/schbtn.gif" /><a href="blogLinkAdd.htm">添加链接</a><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterBlogLinkList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                  <li class="li1">链接名称</li>
                  <li class="li2">链接描述</li>
                  <li class="li3">创建日期</li>
                  <li class="li4">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                   <li class="li1"><a href="#">中国教育网</a><a href="#"><img src="images/rss.gif" /></a></li>
                   <li class="li2">链接描述链接描述链接描述链接描述链接描述链接描述</li>
                   <li class="li3">2008-12-23 23:55</li>
                   <li class="li4 Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                   <li class="li1"><a href="#">中国教育网</a><a href="#"><img src="images/rss.gif" /></a></li>
                   <li class="li2">链接描述链接描述链接描述链接描述链接描述链接描述</li>
                   <li class="li3">2008-12-23 23:55</li>
                   <li class="li4 Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            <div class="page" style="width:800px;">
                <Wis:MiniPager ID="MiniPager1" runat="server"></Wis:MiniPager>
            </div>
    
    </div>
    </form>
</body>
</html>
