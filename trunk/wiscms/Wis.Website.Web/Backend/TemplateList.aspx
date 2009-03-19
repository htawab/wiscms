<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="Wis.Website.Web.Backend.TempleteList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>模板管理</title>
<link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body {background:#d6e7f7}
.Template ul li {height:78px; padding-top:4px; white-space:normal; line-height:18px;}
.Template ul.top li {height:24px; line-height:24px;}
.Template ul li img {border:solid 1px #aaa; background:#fff; padding:2px; }
.Template li.a {width:85px;}
.Template li.b {width:150px;}
.Template li.c {width:110px;}
.Template li.d {width:130px;}
.Template li.e {width:205px;}
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
    
    
    <div class="listBox Template">
    <div class="rightBox_in"><label>模板名称：</label><input type="text" /><input type="button" value="搜索" class="button" /><div class="clear"></div></div>
    <asp:Repeater ID="RepeaterTemplateList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                  <li class="a">模板预览</li>
                  <li class="b">模板名称</li>
                  <li class="c">创建人</li>
                  <li class="d">创建日期</li>
                  <li class="e">版权信息</li>
                  <li class="f">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                   <li class="a"><img src="images/blog1.jpg" /></li>
                   <li class="b">AAAAA</li>
                   <li class="c">AAAAAA</li>
                   <li class="d">2008-12-23 22:44</li>
                   <li class="e">AAAAAAAAAAAA</li>
                   <li class="f Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                    <li class="a"><img src="images/blog1.jpg" /></li>
                   <li class="b">AAAAAAAA</li>
                   <li class="c">AAAAAAA</li>
                   <li class="d">2008-12-23 22:44</li>
                   <li class="e">AAAAAAAAAAAA</li>
                   <li class="f Operate"><a href="#">修改</a><a href="#">删除</a></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            
            <div class="page" style="width:800px;"><a href="####" class="noLink">共 8 页 当前第 2 页</a> <a href="#">首页</a> <a href="#">上一页</a> <a href="">下一页</a> <a href="#">尾页</a></div>
    
    </div>
    </form>
</body>
</html>