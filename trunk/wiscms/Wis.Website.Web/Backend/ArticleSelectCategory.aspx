<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleSelectCategory.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleSelectCategory" %>
<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="DropdownMenus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>选择分类 - 内容管理 - 常智内容管理系统</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: #d6e7f7"><form id="form2" runat="server">
    <div>
        <div class="position">当前位置：选择分类</div>
        <div class="add_step">
            <ul>
                <li class="current_step">第一步：选择分类</li>
                <li>第二步：录入内容</li>
                <li>第三步：录入更多内容</li>
                <li>第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main">
            <div class="step1">
                <DropdownMenus:DropdownMenu ID="DropdownMenuCategory" runat="server" ImagePath="../images/DropdownMenu/" />
                <div class="clear"></div>
            </div>
        </div>
        <div id="Warning" runat="server"></div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" />
        </div>
    </div></form>
</body>
</html>