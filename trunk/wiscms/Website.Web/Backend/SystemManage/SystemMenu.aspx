<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemMenu.aspx.cs" Inherits="Wis.Website.Web.Backend.SystemManage.SystemMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        var menuid = "menuid4";
    function selectmenu(obj) {
        if (document.getElementById(menuid))
            document.getElementById(menuid).className = "";
        document.getElementById(obj).className = "selected";
        menuid = obj;
    }
    </script>

</head>
<body style="background: #E6F5FF;">
    <form id="menu">
    <div class="leftbar" style="height: 480px;">
        <ul>
                    <li><a href="TemplateLabelList.aspx" class="selected" id="menuid1" target="main" title="标签管理"
                onclick="selectmenu('menuid1')">标签管理</a></li>
            <li><a href="TemplatesList.aspx" target="main" id="menuid2" title="模板管理" onclick="selectmenu('menuid2')">
                模板管理</a></li>
                <li><a href="CategoryList.aspx" target="main" id="menuid3" title="栏目管理" onclick="selectmenu('menuid3')">
                栏目管理</a></li>
        </ul>
    </div>
    </form>
</body>
</html>
