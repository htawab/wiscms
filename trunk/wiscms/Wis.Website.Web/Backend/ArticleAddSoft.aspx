<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleAddSoft.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleAddSoft" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>录入软件信息 - 内容管理 - 常智内容管理系统</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function ToOperatingSystem(addTitle) {
        
	        var revisedTitle;
	        var currentTitle;
	        currentTitle = document.getElementById("OperatingSystem").value;
	       
	        var findSys = currentTitle.indexOf(addTitle);
	       
	        if (findSys == -1) {
	        document.getElementById("OperatingSystem").value=currentTitle+addTitle;
	        
	        }
	        else {
	        alert("己存在");
	        return
	        }
	        return; 
        }    
    </script>
</head>
<body style="background: #d6e7f7"><form id="form1" runat="server">
    <div>
        <div class="position">当前位置：<asp:HyperLink ID="HyperLinkCategory" runat="server"></asp:HyperLink> » <a href="ArticleUpdate.aspx?ArticleGuid=<%=Request["ArticleGuid"] %>">录入内容</a> » 录入软件信息</div>
        <div class="add_step">
            <ul>
                <li>第一步：选择分类</li>
                <li>第二步：录入内容</li>
                <li class="current_step">第三步：录入更多内容</li>
                <li>第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main">
            <div>
                <label>软件性质：</label>
                <INPUT size="10" name="SoftType" value="国产软件" /> <SELECT onchange="SoftType.value=this.value;" name="SelectSoftType"> <OPTION value="国产软件">选择软件类型</OPTION> <OPTION value="国产软件">国产软件</OPTION> <OPTION value="国外软件">国外软件</OPTION> <OPTION value="汉化补丁">汉化补丁</OPTION> <OPTION value="源码程序">源码程序</OPTION></SELECT>
            </div>
            <div><label>软件版本：</label><input type="text" size="18" id="Version" name="Version" /></div>
            <div>
                <label>软件语言：</label>
                <INPUT size="10" id="Languages" name="Languages" value="简体中文" /> <SELECT onchange="Languages.value=this.value;" name="SelectLanguages"> <OPTION value="简体中文">请选择语言</OPTION> <OPTION value="简体中文">简体中文</OPTION> <OPTION value="繁体中文">繁体中文</OPTION> <OPTION value="英文">英文</OPTION> <OPTION value="多国语言">多国语言</OPTION></SELECT>
            </div>
            <div><label>软件授权：</label><INPUT size="10" name="Copyright" value="共享版" /> <SELECT onchange="Copyright.value=this.value;" name="SelectCopyright"> <OPTION value="共享版">选择授权方式</OPTION> <OPTION value="共享版">共享版</OPTION> <OPTION value="免费版">免费版</OPTION> <OPTION value="自由版">自由版</OPTION> <OPTION value="试用版">试用版</OPTION> <OPTION value="演示版">演示版</OPTION> <OPTION value="商业版">商业版</OPTION></SELECT></div>
            <div><label>软件运行环境：</label><INPUT size="60" id="OperatingSystem" name="OperatingSystem" value="Win9X/2000/XP/2003/" /> <a href='javascript:document.getElementById("OperatingSystem").value="";void(0)'>清空</a></div>
            <div class="softText"><A href='javascript:ToOperatingSystem("Win9X/")'>Win9X</A>|<A href='javascript:ToOperatingSystem("ME/")'>ME</A>|<A href='javascript:ToOperatingSystem("NT/")'>NT</A>|<A href='javascript:ToOperatingSystem("2000/")'>2000</A>|<A href='javascript:ToOperatingSystem("XP/")'>XP</A>|<A href='javascript:ToOperatingSystem("2003/")'>2003</A>|<A href='javascript:ToOperatingSystem("Vista/")'>Vista</A>|<A href='javascript:ToOperatingSystem("Linux/")'>Linux</A>|<A href='javascript:ToOperatingSystem("Unix/")'>Unix</A>|<A href='javascript:ToOperatingSystem("Mac/")'>Mac</A></div>
            <div><label>软件星级：</label><SELECT name="Rank"><OPTION value="5">★★★★★</OPTION> <OPTION value="4">★★★★</OPTION> <OPTION value="3">★★★</OPTION> <OPTION value="2">★★</OPTION> <OPTION value="1">★</OPTION></SELECT></div>
            <div><label>演示地址：</label><input type="text" id="DemoUri" name="DemoUri" size="32" /></div>
            <div><label>注册地址：</label><input type="text" id="RegUri" name="RegUri" size="32" /></div>
            <div><label>解压密码：</label><input type="text" id="UnzipPassword" name="UnzipPassword" size="32" /></div>
            <div><label>插件情况：</label><SELECT name="Plugin"><OPTION value="1">无插件-无病毒</OPTION> <OPTION value="2">有插件-可选择</OPTION> <OPTION value="3">疑有毒-未确认</OPTION> <OPTION value="4">有插件-无选择</OPTION> <OPTION value="5">有病毒-已确认</OPTION> <OPTION value="6">尚未认证</OPTION> <OPTION value="7">未通过-暂停下载</OPTION></SELECT></div>
            <div>
                <label>软件预览图：</label>
                <FileUploads:DJUploadController ID="DJUploadController1" runat="server" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/"  />
                <input id='Preview' type='file' name='Preview' value='' />
            </div>
            <div>
                <label>软件图标：</label>
                <input id='Thumb' type='file' name='Thumb' value='' />
            </div>
            <div><label>更新时间：</label><INPUT id="SoftTime" size="25" name="SoftTime" value="2009-3-4 23:21:52" /></div>
            <div><label>所需积分：</label><INPUT size="10" name="Point" value="1" /></div>
        </div>
        <div id="Warning" runat="server"></div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" style="width:151px" />
        </div>
    </div></form>
</body>
</html>
