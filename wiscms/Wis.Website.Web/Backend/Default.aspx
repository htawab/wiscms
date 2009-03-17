<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Wis.Website.Web.Backend.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="css/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">

iframe {min-height:500px; height:100%;}
</style>
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
  <ul id="topMenu">
   <li class="nav_on"  onclick="thisOn(this);"><a href="leftMenu1.htm" target="menu" class="menu1">我的博客</a></li>
   <li onclick="thisOn(this);" id="menuid3"><a href="leftMenu2.htm" target="menu" class="menu2">内容管理</a></li>
   <li onclick="thisOn(this);" id="menuid4"><a href="leftMenu3.htm" target="menu" class="menu3">全局管理</a></li>
   <li onclick="thisOn(this);" id="Li1"><a href="leftMenu3.htm" target="menu" class="menu3">会议预订</a></li>
   <li onclick="thisOn(this);" id="Li2"><a href="leftMenu3.htm" target="menu" class="menu3">学籍管理</a></li>
   <li onclick="thisOn(this);" id="Li3"><a href="leftMenu3.htm" target="menu" class="menu3">人事管理</a></li>
   <li onclick="thisOn(this);" id="Li4"><a href="leftMenu3.htm" target="menu" class="menu3">教学备案</a></li>
   <li onclick="thisOn(this);" id="Li5"><a href="leftMenu3.htm" target="menu" class="menu3">设备管理</a></li>
   <li onclick="thisOn(this);" id="Li6"><a href="leftMenu3.htm" target="menu" class="menu3">自助建站</a></li>
   <li onclick="thisOn(this);" id="Li7"><a href="leftMenu3.htm" target="menu" class="menu3">电子期刊</a></li>
   
   
  </ul>
</div>
<script>

function thisOn (iObj) {
var aaa = document.getElementById("topMenu").getElementsByTagName("li");
for (var i=0; i<aaa.length; i++) {
aaa[i].className="";
}

iObj.className="nav_on";
}
</script>
<div class="main">
        <table cellpadding="0"  cellspacing="0" width="100%">
            <tr>
                <td width="172px" valign="top"  >
                    <iframe frameborder="0" width="172px"  src="leftMenu1.htm" id="menu"  name="menu"   style="height: expression(menu.document.body.scrollHeight); height:660px;"  scrolling="no"></iframe>
                </td>
                <td valign="top" style=" border:1px solid #9fb5d2; background: #d6e7f7">
                    <iframe frameborder="0" width="100%"  src="openBlog.htm"  name="main"  style=" border:1px solid #9fb5d2; border:0; height: expression(main.document.body.scrollHeight); width:99%;" id="oIframe" scrolling="no"></iframe>
                </td>
            </tr>
        </table><div class="clear"></div>
</div>
<div class="bottom">Copyright©2002-2008 EVERWIS Inc. All Rights Reserved <a href="http://www.everwis.com" title="北京东方常智科技有限公司" target="_blank">北京东方常智科技有限公司</a>版权所有 【本系统适用于1024*768及以上分辨率】</div>
</body>
</html>
