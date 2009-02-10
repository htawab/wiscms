<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtmlEditorTest.aspx.cs" Inherits="Wis.Website.Web.HtmlEditorTest" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="Wis" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.HtmlEditorControls" tagprefix="HtmlEditorControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <HtmlEditorControls:HtmlEditor ID="ContentHtml" runat="server" DialogsPath="../images/HtmlEditor/"></HtmlEditorControls:HtmlEditor>
    </form>
</body>
</html>
