﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingList.aspx.cs" Inherits="Wis.Website.Web.Templates.MeetingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>教育动态</title>

    <script src="../../JavaScript/Website.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Ajax.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Pager.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Meeting.js" language="javascript" type="text/javascript"></script>

    <link href="../../css/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../../javascript/indexTab.js"></script>

</head>
<body onload="WebsiteObj.Meeting.LoadData(1)">
    <div class="header">
        <div class="headerBox">
            <img src="../../images/logo.gif" width="173px" height="53px" />
            <div class="loginBox">
                用户名：
                <input type="text" size="12" />
                密码：
                <input type="text" size="12" />
                <input type="button" class="btn" />
            </div>
            <ul>
                <li class="left"></li>
                <li><a href="$ReleaseDirectory$/Default.htm" title="首页">首页</a></li><li class="sep">|</li>
                <li><a href="jydtList.html" >教育动态</a></li><li class="sep">|</li>
                <li><a href="#" >教学研究</a></li><li class="sep">|</li>
                <li><a href="#" >会议资源</a></li><li class="sep">|</li>
                <li><a href="#" >专题信息</a></li><li class="sep">|</li>
                <li><a href="#" >东师资源</a></li><li class="sep">|</li>
                <li><a href="#" >铁东资源</a></li><li class="sep">|</li>
                <li><a href="#" >铁东教育</a></li><li class="sep">|</li>
                <li><a href="#" >名师风采</a></li><li class="sep">|</li>
                <li><a href="#" >教育博客</a></li>
                <li class="right"></li>
            </ul>
        </div>
    </div>
    <div class="main">
        <div class="left listtab">
            <div class="position">
                铁东教育 &gt; 会议信息</div>
            <div class="clear">
            </div>
            <div id="BetMeeting">
                <center>
                    <img src="../../images/ajaxing.gif" align="absmiddle" border="0" />
            </div>
            <div class="page" id="contentpane">
                <div id="DataHolder">
                </div>
                <div id="PagerHolder">
                </div>
            </div>
        </div>
        <div class="right">
            <div class="sysLink mgnBot">
                <a href="#" title="信息化设备管理">信息化设备管理</a><a href="#" title="信息化教学备案">信息化教学备案</a><a
                    href="#" title="人事管理系统">人事管理系统</a><a href="#" title="学籍管理系统">学籍管理系统</a><a href="#"
                        title="统计报表">统计报表</a><a href="#" title="部门职能">部门职能</a><a href="#" title="会议预订系统">会议预订系统</a><a
                            href="#" title="电子期刊管理">电子期刊管理</a><a href="#" title="自助建站管理">自助建站管理</a></div>
            <div class="right tdjy">
                <img src="../../images/tdjyPic.gif" width="65" height="75" alt="电子杂志" />
                <div class="line1">
                    <div>
                        《<span class="red">铁东教育</span>》第<span>13</span> 期</div>
                    <select>
                        <option>铁东教育第13期</option>
                    </select></div>
                <div class="line2">
                    <div class="lineIn">
                        <a href="#" title="#">第12期 2008.12</a><br />
                        <a href="#" title="#">第12期 2008.12</a></div>
                    <input type="button" class="readBtn" /></div>
            </div>
            <div class="clear  mgnBot">
            </div>
            <div class="areasch outbox height5">
                <div class="cap mgnBot">
                    学区查询</div>
                <select name="">
                    <option>办事处名称1</option>
                </select><span class="red">（填写说明1）</span><br />
                <select name="">
                    <option>办事处名称1</option>
                </select><span class="red">（填写说明1）</span><br />
                <select name="">
                    <option>办事处名称1</option>
                </select><span class="red">（填写说明1）</span><br />
                <img src="../../images/areaschbtn.gif" width="56" height="21" class="btn" />
            </div>
            <div class="softdown outbox height6">
                <div class="cap">
                    软件下载</div>
                <ul>
                    <li><a href="#" title="#">腾讯球球</a></li>
                    <li><a href="#" title="#">腾讯球球</a></li>
                    <li><a href="#" title="#">腾讯球球</a></li>
                    <li><a href="#" title="#">腾讯</a></li>
                    <li><a href="#" title="#">腾讯</a></li>
                    <li><a href="#" title="#">腾讯</a></li>
                    <li><a href="#" title="#">腾讯</a></li>
                    <li><a href="#" title="#">腾讯球球</a></li>
                    <li><a href="#" title="#">腾讯球球</a></li>
                    <li><a href="#" title="#">腾讯球球</a></li>
                    <li><a href="#" title="#">腾讯</a></li>
                    <li><a href="#" title="#">腾讯</a></li>
                    <li><a href="#" title="#">腾讯</a></li>
                    <li><a href="#" title="#">腾讯</a></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="link outbox">
        <div class="cap">
            学校链接</div>
        <a href="#" title="#">
            <img src="../../images/link.gif" /></a> <a href="#" title="#">
                <img src="../../images/link.gif" /></a> <a href="#" title="#">
                    <img src="../../images/link.gif" /></a> <a href="#" title="#">
                        <img src="../../images/link.gif" /></a> <a href="#" title="#">
                            <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                    <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                        <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                            <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                                <img src="../../images/link.gif" /></a>
        <a href="#" title="#">
            <img src="../../images/link.gif" /></a> <a href="#" title="#">
                <img src="../../images/link.gif" /></a> <a href="#" title="#">
                    <img src="../../images/link.gif" /></a> <a href="#" title="#">
                        <img src="../../images/link.gif" /></a> <a href="#" title="#">
                            <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                    <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                        <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                            <img src="../../images/link.gif" /></a> <a href="#" title="#">
                                                <img src="../../images/link.gif" /></a>
        <div class="clear">
        </div>
    </div>
    <div class="bottom">
        <a href="#" title="#">设置首页</a>－<a href="#" title="#">加入收藏</a>－<a href="#" title="#">版权声明</a>－<a
            href="#" title="#">联系方式</a>－<a href="#" title="#">网站地图</a><br />
        鞍山市铁东区教育局版权所有<br />
        辽ICP备0000号
    </div>
</body>
</html>
