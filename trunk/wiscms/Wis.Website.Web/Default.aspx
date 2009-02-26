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
    </div>
    <div class="bottom">Copyright©2002-2008 EVERWIS Inc. All Rights Reserved <a href="http://www.everwis.com" target="_blank">北京东方常智科技有限公司</a>版权所有 【本系统适用于1024*768及以上分辨率】</div>
</body>
</html>