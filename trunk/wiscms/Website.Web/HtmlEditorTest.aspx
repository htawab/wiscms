<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtmlEditorTest.aspx.cs" Inherits="Wis.Website.Web.HtmlEditorTest" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="Wis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
    body {margin:25px; font:11px Verdana,Arial; background:#eee;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    </div>
    <Wis:DropdownMenu ID="DropdownMenu1" runat="server" ImagePath="/Backend/images/DropdownMenu/" />
    <input id="Hidden1" type="hidden" />
    </form>
</body>
</html>
