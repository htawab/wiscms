<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Wis.Website.Web.Backend.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="css/css.css" rel="stylesheet" type="text/css" />
<script language=javascript>
    var menuid = "menuid1";
    function selectmenu(obj) {
        if (document.getElementById(menuid))
            document.getElementById(menuid).className = "";
        document.getElementById(obj).className = "nav_on";
        menuid = obj;
    }
</script>
</head>

<body>
<div class="header">
 <img src="images/Logo.gif" alt="logo" />
 <div class="header_right">
  <ul>
   <li class="left"></li>
   <li class="nav1"><a href="#">退出登录</a></li>
   <li class="nav2"><a href="#">修改密码</a></li>
   <li class="nav3"><a href="#">修改资料</a></li>
   <li class="right"></li>
   <div style="clear:both"></div>
  </ul>
 </div>
</div>
<div class="menu">
  <ul>
   <li class="nav_on" id ="menuid1"><a href="#" class="menu1" onclick="selectmenu('menuid1')">首页</a></li>
   <li id="menuid3"><a href="Article/ArticleMenu.aspx" onclick="selectmenu('menuid3')" target="menu" class="menu2">内容管理</a></li>
   <li id="menuid4"><a href="#" onclick="selectmenu('menuid4')" class="menu3">用户管理</a></li>
   <li id="menuid5"><a href="SystemManage/SystemMenu.aspx"  target="menu"  onclick="selectmenu('menuid5')" class="menu3">系统配置</a></li>
   <li id="menuid6"><a href="#" onclick="selectmenu('menuid6')" class="menu3">日志管理</a></li>
  </ul>
</div>
<div class="main">
        <table cellpadding="0"  cellspacing="0" width="100%">
            <tr>
                <td width="172px" valign="top"  >
                    <iframe frameborder="0" width="172px"  src="Article/ArticleMenu.aspx" id="menu"  name="menu"   style="height: expression(menu.document.body.scrollHeight);" scrolling="no"></iframe>
                </td>
                <td    valign="top">
                    <iframe frameborder="0" width="100%"  src="Index.aspx"  name="main"   style="height: expression(main.document.body.scrollHeight);"  scrolling="no"></iframe>
                </td>
            </tr>
        </table> <div class="clear"></div>
</div>
<div class="bottom">Copyright©2002-2008 EVERWIS Inc. All Rights Reserved <a href="http://www.everwis.com" title="北京东方常智科技有限公司" target="_blank">北京东方常智科技有限公司</a>版权所有 【本系统适用于1024*768及以上分辨率】</div>
</body>
</html>
