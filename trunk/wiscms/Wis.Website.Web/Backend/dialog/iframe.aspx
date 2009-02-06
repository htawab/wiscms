<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iframe.aspx.cs" Inherits="Wis.Website.Web.Backend.dialog.iframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <link href="../../sysImages/default/css/css.css" rel="stylesheet" type="text/css" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div onmousedown="drag(event,$('s_id'));" class="titile_bg" style="cursor: move;">
        <table style="width: 100%; background-color: #006699;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="height: 26px;">
                    <font color="white">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;窗口可拖动;双击此处关闭窗口;列表处双击选择文件</font>
                </td>
                <%--<td style="width:10px"><img align="right" src="../../sysImages/normal/close.gif" style="cursor:pointer;padding-right:2px;padding-bottom:2px;" title="关闭" onclick="closediv($('s_id'));" /></td>--%>
            </tr>
        </table>
    </div>

    <div id="select_iframe" runat="server">
    </div>
</body>
</html>
