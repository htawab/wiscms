<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Wis.Website.Web._Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>后台管理－常智内容管理系统</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        window.parent.location = "web"
        function logout() {
            window.parent.location = "Login.aspx";
        }
    </script>
</head>
<body>
    <div class="header">
        <img src="images/Logo.gif" alt="logo" />
        <div class="header_right">
            <ul>
                <li class="left"></li>
                  <li class="nav1"><a href="index.aspx">老版本</a></li>
                <li class="nav2"> <a href="#" onclick="logout();" >退出登录</a></li>
                <li class="nav2"><a href="../user/info/ChangePassword.aspx" target="main">修改密码</a></li>
<li class="nav3"><a href="../user/info/userinfo.aspx"target="main">修改资料</a></li>
                <li class="right"></li>
                <div style="clear: both">
                </div>
            </ul>
        </div>
    </div>
    <div class="menu">
        <ul>
            <li class="nav_on"><a href="#" class="menu1">中文网站</a></li>
            <li class="nav_on"><a href="#" class="menu1">英文网站</a></li>
            <li><a href="#" class="menu2">稿件管理</a></li>
            <li><a href="#" class="menu3">友情链接</a></li>
        </ul>
    </div>
    <div class="main">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td width="160px" valign="top" style="border: 1px solid #9fb5d2; background: #d6e7f7;
                    overflow: visible;">
                    <iframe frameborder="0" width="160px" height="655px" src="LeftMenu.aspx" scrolling="no"></iframe>
                </td>
                <td width="6px">
                </td>
                <td style="border: 1px solid #9fb5d2" valign="top">
<%--<p align="center"><object id="vid" height="300" width="500" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA">
<param value="10001" name="_ExtentX" />
<param value="6879" name="_ExtentY" />
<param value="-1" name="AUTOSTART" />
<param value="0" name="SHUFFLE" />
<param value="0" name="PREFETCH" />
<param value="-1" name="NOLABELS" />
<param value="http://localhost:3928/files/video/2008-12/日本超级模特激情做爱.rmvb" name="SRC" />
<param value="Imagewindow" name="CONTROLS" />
<param value="clip1" name="CONSOLE" />
<param value="0" name="LOOP" />
<param value="0" name="NUMLOOP" />
<param value="0" name="CENTER" />
<param value="0" name="MAINTAINASPECT" />
<param value="#000000" name="BACKGROUNDCOLOR" /></object><object id="vid2" height="50" width="500" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA">
<param value="10001" name="_ExtentX" />
<param value="1270" name="_ExtentY" />
<param value="-1" name="AUTOSTART" />
<param value="0" name="SHUFFLE" />
<param value="0" name="PREFETCH" />
<param value="-1" name="NOLABELS" />
<param value="http://localhost:3928/files/video/2008-12/日本超级模特激情做爱.rmvb" name="SRC" />
<param value="ControlPanel,StatusBar" name="CONTROLS" />
<param value="clip1" name="CONSOLE" />
<param value="0" name="LOOP" />
<param value="0" name="NUMLOOP" />
<param value="0" name="CENTER" />
<param value="0" name="MAINTAINASPECT" />
<param value="#000000" name="BACKGROUNDCOLOR" /></object></p> --%>       
<%--<object width="425" height="344"><param name="allowFullScreen" value="true"></param><param name="allowscriptaccess" value="always"></param><embed src="F:\p\henry\m\[Falcon] Big Dick Club 2.rmvb" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" width="425" height="344"></embed></object>--%>
<%--<table width="100%" height="" border="0" cellspacing="0" cellpadding="3" bgcolor=""> 
<tr bgcolor="" align="left"> 
<td colspan="1"> 
<div class="readf"><P align=center> 
<TABLE class=" FCK__ShowTableBorders" height=1 cellSpacing=0 cellPadding=0 width=492 border=0> 
<OBJECT id=player height=300 width=500 border=0 classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA name=player> 
<param name="_ExtentX" value="13229"> 
<param name="_ExtentY" value="7938"> 
<param name="AUTOSTART" value="0"> 
<param name="SHUFFLE" value="0"> 
<param name="PREFETCH" value="0"> 
<param name="NOLABELS" value="0"> 
<param name="SRC" value="E:/projects/EDUOA/Website/src/Wis.Website.Web/files/video/2008-12/日本超级模特激情做爱.rmvb"> 
<param name="CONTROLS" value="Imagewindow"> 
<param name="CONSOLE" value="clip*"> 
<param name="LOOP" value="0"> 
<param name="NUMLOOP" value="0"> 
<param name="CENTER" value="0"> 
<param name="MAINTAINASPECT" value="0"> 
<param name="BACKGROUNDCOLOR" value="#000000"></OBJECT> 
<TBODY> 
<TR> 
<TD width=65 height=1><INPUT style="BORDER-RIGHT: #a2a2a2 1px solid; BORDER-TOP: #ffffff 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #ffffff 1px solid; WIDTH: 85px; COLOR: #333333; BORDER-BOTTOM: #a2a2a2 1px solid; HEIGHT: 25px; BACKGROUND-COLOR: #eeeeee" onclick=document.player.SetFullScreen() type=button value=点击全屏播放 name=Submit></TD> 
<TD width=447 height=1> 
<OBJECT id=RP2 height=25 width=415 classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA> 
<param name="_ExtentX" value="10980"> 
<param name="_ExtentY" value="661"> 
<param name="AUTOSTART" value="-1"> 
<param name="SHUFFLE" value="0"> 
<param name="PREFETCH" value="0"> 
<param name="NOLABELS" value="-1"> 
<param name="SRC" value="E:/projects/EDUOA/Website/src/Wis.Website.Web/files/video/2008-12/日本超级模特激情做爱.rmvb"> 
<param name="CONTROLS" value="ControlPanel"> 
<param name="CONSOLE" value="clip*"> 
<param name="LOOP" value="0"> 
<param name="NUMLOOP" value="0"> 
<param name="CENTER" value="0"> 
<param name="MAINTAINASPECT" value="0"> 
<param name="BACKGROUNDCOLOR" value="#000000"></OBJECT></TD> 
</TR> 
</TBODY> 
</TABLE> 
</div> 
        </td>
            </tr>
        </table>--%>
    </div>
    <div class="bottom">Copyright©2002-2008 EVERWIS Inc. All Rights Reserved <a href="http://www.everwis.com" target="_blank">北京东方常智科技有限公司</a>版权所有 【本系统适用于1024*768及以上分辨率】</div>
</body>
</html>