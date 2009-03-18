<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogCategoryList.aspx.cs" Inherits="Wis.Website.Web.Backend.BlogCategoryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>博客分类管理</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body  style="background:#d6e7f7">
    <form runat="server" id="BlogCategoryListForm">
    
    <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
    </div>
    <div class="listBox blogList">
    <div class="rightBox_in"><label>博客标题：</label><input type="text" /><input type="image" style="height:auto;" src="images/schbtn.gif" /><a href="BlogCategoryAddNew.aspx">添加分类</a><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterBlogCategoryList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                    <li class="li1">博客分类名称</li><li class="li2">排序</li><li class="li4">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                    <li class="li1">博客分类名称</li>
                    <li class="li2"><a href="">↑</a> <a href="">↓</a></li>
                    <li class="li4 Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                    <li class="li1">博客分类名称</li>
                    <li class="li2"><a href="">↑</a> <a href="">↓</a></li>
                    <li class="li4 Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>
