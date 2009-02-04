<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageList.aspx.cs" Inherits="Wis.Website.Web.Backend.Article.PageList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/Website.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Ajax.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Pager.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Page.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>

</head>
<body onload="WebsiteObj.Page.LoadData(1)">
    <form id="form1" runat="server">
    <div class="right" id="divright">
        <div class="position">
            内容管理 » <span class="red" runat="server" id="daohang">单页面</span>
        </div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="10" height="30">
                    &nbsp;
                </td>
                <td width="100" align="left">
                    <div class="command" onclick="WebsiteObj.Page.PageAdd()">
                        添加单页面</div>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div id="BetPage">
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
