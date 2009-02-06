<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="Wis.Website.Web.Backend.Article.ArticleList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/Website.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Ajax.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Pager.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Article.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>

</head>
<body onload="WebsiteObj.Article.CategoryId(<%=ViewState["CategoryId"]%>),WebsiteObj.Article.LoadData(1)">
    <form id="form1" runat="server">
    <div class="right" id="divright">
        <div class="position">
            内容管理 » <span class="red" runat="server" id="daohang"></span>
        </div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="10">
                    &nbsp;
                </td>
                <td width="46%">
                    <div class="command" onclick="WebsiteObj.Article.AllShow();">
                        全部显示</div>
                    <div class="command" onclick="WebsiteObj.Article.ArticleAdd()">
                        添加新闻</div>
                    <div class="command" onclick="WebsiteObj.Article.ArticleDelAll()">
                        删除新闻</div>
                    <div class="command">
                        生成静态页面</div>
                    <div class="command" id="CategoryHtml" onclick='WebsiteObj.Article.CategoryHtml()'>
                        生成栏目</div>
                </td>
                <td>
                    <div class="schBox">
                        <label>
                            关键字：</label><input type="text" size="10" id="keysword" name="keysword" />
                        <select id="type" runat="server">
                            <option value="Title">标题</option>
                            <option value="ContentHtml">内容</option>
                            <option value="Author">作者</option>
                        </select>
                        栏目：<input id="CategoryId" runat="server" type="text" name="CategoryId" style="display: none;" /><input
                            id="CategoryName" runat="server" readonly type="text" size="8" name="CategoryName" /><img
                                src="../../images/folder.gif" alt="选择已有标签" border="0" style="cursor: pointer;"
                                onclick="selectFile('CategoryList',new Array(document.form1.CategoryId,document.form1.CategoryName),250,500);document.form1.CategoryName.focus();" /><input
                                    type="button" onclick="WebsiteObj.Article.Seacrh()" value="" class="schbtn" title="搜索" />
                    </div>
                </td>
            </tr>
        </table>
        <div id="BetArticle">
            <center>
                <img src="../../images/ajaxing.gif" align="absmiddle" border="0" />
                数据装载中，请稍候...</center>
        </div>
        <div class="page" id="contentpane">
            <div id="DataHolder">
            </div>
            <div id="PagerHolder">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
