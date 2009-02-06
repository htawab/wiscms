<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleMenu.aspx.cs" Inherits="Wis.Website.Web.Backend.Article.ArticleMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Website.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Ajax.js" language="javascript" type="text/javascript"></script>

</head>
<%--onload=" CategoryLoad(0);"--%>
<body style="background: #E6F5FF;" onload="CategoryLoad(0);">
    <form id="form1">
    <div class="leftbar" id="divleftbar" name="divleftbar">
        <div id="Parent0" runat="server" class="border">
        </div>
        <%--<div id="ParentCategory" runat="server" class="border">
            <div class="Parent" id="Category1">
                <img src="../images/ico1.gif" style="cursor: hand;" width="11" height="11" alt="点击展开子栏目"
                    border="0" onclick="javascript:SwitchImg(this,'1');" />&nbsp;<a href="ArticleList.aspx?CategoryId=1"
                        onclick="javascript:Change('1');" target="main">教育动态</a></div>
            <div id="Parent1" style="height: 100%; display: none;">
                <%--<div class="Parent" id="Category2">&nbsp;&nbsp;&nbsp;&nbsp;
                    <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                        href="\"ArticleList.aspx?CategoryId=2" onclick="javascript:Change('2');" target="main">党群</a></div>--%>
          <%--  </div>--%>
          <%--  <div class="Parent" id="Category15">
                <img src="../images/ico1.gif" style="cursor: hand;" width="11" height="11" alt="点击展开子栏目"
                    border="0" onclick="javascript:SwitchImg(this,'15');" />&nbsp;<a href="ArticleList.aspx?CategoryId=15"
                        onclick="javascript:Change('15');" target="main">教学研究</a></div>
            <div id="Parent15" style="height: 100%; display: none;">
            </div>
            <div class="Parent" id="Category23">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=23" onclick="javascript:Change('23');" target="main">专题信息</a></div>
            <div class="Parent" id="Category24">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=24" onclick="javascript:Change('24');" target="main">名师风采</a></div>
            <div class="Parent" id="Category27">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=27" onclick="javascript:Change('27');" target="main">政策法规</a></div>
            <div class="Parent" id="Category28">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=28" onclick="javascript:Change('28');" target="main">铁东教育</a></div>
            <div class="Parent" id="Category29">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=29" onclick="javascript:Change('29');" target="main">图片头条</a></div>
            <div class="Parent" id="Category30">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=30" onclick="javascript:Change('30');" target="main">铁东资源</a></div>
            <div class="Parent" id="Category31">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=31" onclick="javascript:Change('31');" target="main">教学资源</a></div>
            <div class="Parent" id="Category32">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=32" onclick="javascript:Change('32');" target="main">空中课堂</a></div>
            <div class="Parent" id="Category33">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=33" onclick="javascript:Change('33');" target="main">视频资源</a></div>
            <div class="Parent" id="Category34">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=34" onclick="javascript:Change('34');" target="main">视频新闻</a></div>
            <div class="Parent" id="Category37">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="../SystemManage/LinkList.aspx?CategoryId=37" onclick="javascript:Change('37');" target="main">学校连接</a></div>--%>
<%--            <div class="Parent" id="Category23">
                <img src="../images/ico2.gif" width="11" height="11" alt="没有子栏目" border="0" />&nbsp;<a
                    href="ArticleList.aspx?CategoryId=23" onclick="javascript:Change('23');" target="main">软件下载</a></div>--%>
           
     <%--   </div>--%>
    </div>
    </form>
</body>
</html>

<script language="javascript">
    var WebsiteObj = new Object();
    WebsiteObj.Article = new Object();
    WebsiteObj.Article.Ajax = new AJAXRequest;
    function CategoryLoad(ParentId) {
        if (!WebsiteObj.Article.Ajax) return;
        WebsiteObj.Article.Ajax.get('/AjaxRequests/Article.aspx?command=CategoryList&ParentId=' + ParentId,
	    function(req) {
        Website.Get("Parent" + ParentId).innerHTML = req.responseText;
        });
    }

    function SwitchImgPage(ImgObj) {
        var ImgSrc = "", SubImgSrc;
        ImgSrc = ImgObj.src;
        SubImgSrc = ImgSrc.substr(ImgSrc.length - 8, 12);
        if (SubImgSrc == "ico1.gif") {
            ImgObj.src = ImgObj.src.replace(SubImgSrc, "ico2.gif");
            ImgObj.alt = "点击收起子栏目";
            Website.Get("Page").style.display = "block"
        } else if (SubImgSrc == "ico2.gif") {
            ImgObj.src = ImgObj.src.replace(SubImgSrc, "ico1.gif");
            ImgObj.alt = "点击展开子栏目";
            Website.Get("Page").style.display = "none"
        }

    }
    function SwitchImg(ImgObj, ParentId) {
        var ImgSrc = "", SubImgSrc;
        ImgSrc = ImgObj.src;
        SubImgSrc = ImgSrc.substr(ImgSrc.length - 8, 12);
        if (SubImgSrc == "ico1.gif") {
            ImgObj.src = ImgObj.src.replace(SubImgSrc, "ico2.gif");
            ImgObj.alt = "点击收起子栏目";
            SwitchSub(ParentId, true);
        }
        else if (SubImgSrc == "ico2.gif") {
            ImgObj.src = ImgObj.src.replace(SubImgSrc, "ico1.gif");
            ImgObj.alt = "点击展开子栏目";
            SwitchSub(ParentId, false);
        }

    }
    var categoryId;
    function Change(CategoryId) {
        if (Website.Get("Category" + categoryId))
            Website.Get("Category" + categoryId).className = "Parent";
        Website.Get("Category" + CategoryId).className = "selected";
        categoryId = CategoryId;
    }
    function SwitchSub(ParentId, ShowFlag) {
        if (ShowFlag) {
            Website.Get("Parent" + ParentId).style.display = "";
            if (Website.Get("Parent" + ParentId).innerHTML == "" || Website.Get("Parent" + ParentId).innerHTML == "栏目加载中...") {
                Website.Get("Parent" + ParentId).innerHTML = "栏目加载中...";
                CategoryLoad(ParentId);
            }
        } else {
        Website.Get("Parent" + ParentId).style.display = "none";
        }
    }
</script>

