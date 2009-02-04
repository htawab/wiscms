<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateLabelList.aspx.cs" Inherits="Wis.Website.Web.Backend.SystemManage.TemplateLabelList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/Website.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/Ajax.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/Pager.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/System.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>
</head>
<body onload="WebsiteObj.System.LoadData(1)">
    <form id="form1" runat="server">
    <div class="right" id="divright">
        <div class="position">
            标签管理 » <span class="red" runat="server" id="daohang"></span>
        </div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="10">
                    &nbsp;
                </td>
                <td width="46%">
                    <div class="command" onclick="WebsiteObj.System.TemplateLabelAddNew()">添加标签</div>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div id="BetTag">
            <center><img src="../../images/ajaxing.gif" align="absmiddle" border="0" />数据装载中，请稍候...</center>
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
