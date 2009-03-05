<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleRelease.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleRelease" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: #d6e7f7"><form id="form1" runat="server">
    <div>
        <div class="position">
            当前位置：添加新闻</div>
        <div class="add_step">
            <ul>
                <li>第一步：选择分类</li><li>第二步：基本信息</li>
                <li>第三步：扩展信息</li><li class="current_step">第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main add_step4">
            信息录入完成，以下相关页面将被重新生成
            <ul class="top">
                <li class="step4_li1">页面标题</li><li class="step4_li2">模板路径</li><li class="step4_li3">
                    发布路径</li><li class="step4_li4">
                        <input type="checkbox" /></li>
            </ul>
            <ul>
                <li class="step4_li1">博客（Web2.0）新时代里程碑 </li>
                <li class="step4_li2">templete/web/web/aa/htm</li><li class="step4_li3">website/web/web/aa/htm</li><li
                    class="step4_li4">
                    <input type="checkbox" /></li>
            </ul>
            <ul>
                <li class="step4_li1">博客（Web2.0）新时代里程碑 </li>
                <li class="step4_li2">templete/web/web/aa/htm</li><li class="step4_li3">website/web/web/aa/htm</li><li
                    class="step4_li4">
                    <input type="checkbox" /></li>
            </ul>
            <ul>
                <li class="step4_li1">博客（Web2.0）新时代里程碑 </li>
                <li class="step4_li2">templete/web/web/aa/htm</li><li class="step4_li3">website/web/web/aa/htm</li><li
                    class="step4_li4">
                    <input type="checkbox" /></li>
            </ul>
            <ul>
                <li class="step4_li1">博客（Web2.0）新时代里程碑 </li>
                <li class="step4_li2">templete/web/web/aa/htm</li><li class="step4_li3">website/web/web/aa/htm</li><li
                    class="step4_li4">
                    <input type="checkbox" /></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <div id="Warning" runat="server"></div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" />
        </div>
    </div></form>
</body>
</html>