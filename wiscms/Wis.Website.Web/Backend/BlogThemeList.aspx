<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogSkinList.aspx.cs" Inherits="Wis.Website.Web.Backend.BlogSkinList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>博客皮肤管理</title>
<link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.BlogSkinList ul li {height:78px; padding-top:4px; white-space:normal; line-height:18px;}
.BlogSkinList ul.top li {height:24px; line-height:24px;}
.BlogSkinList ul li img {border:solid 1px #aaa; background:#fff; padding:2px; }
.BlogSkinList li.a {width:85px;}
.BlogSkinList li.b {width:150px;}
.BlogSkinList li.c {width:110px;}
.BlogSkinList li.d {width:130px;}
.BlogSkinList li.e {width:205px;}
</style>
</head>
<body style="background:#d6e7f7">
    <form id="BlogSkinListForm" runat="server">
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>
    
    
    <div class="listBox BlogSkinList">
    <div class="rightBox_in"><label>博客名称：</label><input type="text" /><input type="button" value="搜索" class="button" /><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterBlogSkinList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                  <li class="a">皮肤预览</li>
                  <li class="b">皮肤名称</li>
                  <li class="c">创建人</li>
                  <li class="d">创建日期</li>
                  <li class="e">版权信息</li>
                  <li class="f">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                   <li class="a"><img src="images/blog1.jpg" /></li>
                   <li class="b">皮肤名称</li>
                   <li class="c">创建人</li>
                   <li class="d">2008-12-23 22:44</li>
                   <li class="e">版权信息版权信息版权信息版权信息</li>
                   <li class="f Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                    <li class="a"><img src="images/blog1.jpg" /></li>
                   <li class="b">皮肤名称</li>
                   <li class="c">创建人</li>
                   <li class="d">2008-12-23 22:44</li>
                   <li class="e">版权信息版权信息版权信息版权信息</li>
                   <li class="f Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>
