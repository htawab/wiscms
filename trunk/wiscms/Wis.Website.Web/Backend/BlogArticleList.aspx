<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogArticleList.aspx.cs" Inherits="Wis.Website.Web.Backend.BlogArticalList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>博客文章管理</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.blogArticleList li.a {width:345px;}
.blogArticleList li.b {width:98px;}
.blogArticleList li.c {width:70px;}
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
    
    
    <div class="listBox blogArticleList">
    <div class="rightBox_in"><label>博客标题：</label><input type="text" /><input type="image" style="height:auto;" src="images/schbtn.gif" /><a href="BlogArticleAddNew.aspx">发表博客</a><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterBlogArticalList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                    <li class="a">博客标题</li><li class="b">最后更新时间</li><li class="c">置顶</li><li class="d">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                    <li class="a"><a href="#" title="浏览数：35 评论数：23">品牌广告买主如何消除对图像广告…</a></li>
                    <li class="b">2009-12-23</li>
                    <li class="c Operate"><a href="#">置顶</a></li>
                    <li class="d Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                    <li class="a"><a href="#" title="浏览数：35 评论数：23">品牌广告买主如何消除对图像广告…</a></li>
                    <li class="b">2009-12-23</li>
                    <li class="c Operate"><a href="#">置顶</a></li>
                    <li class="d Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>
