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
    <asp:SiteMapPath ID="SiteMapPath1" runat="server" Font-Names="Verdana" Font-Size="0.8em"
    PathSeparator=" &gt; ">
    <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
    <CurrentNodeStyle ForeColor="#333333" />
    <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
    <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
   </asp:SiteMapPath>
    </form>
</body>
</html>
