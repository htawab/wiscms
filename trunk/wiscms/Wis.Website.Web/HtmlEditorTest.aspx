﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtmlEditorTest.aspx.cs" Inherits="Wis.Website.Web.HtmlEditorTest" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="Wis" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.HtmlEditorControls" tagprefix="HtmlEditorControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style>
#Pager 
{
	color:#222;
	font-family:宋体;
	font-size:12px;
	}
#Pager span 
{
	border:1px solid #ddd;
	padding:3px 6px;
	margin:0 2px;
	}
#Pager a 	
{
	border:1px solid #ddd;
	text-decoration:none;
	padding:3px 6px;
	margin:0 2px;
	color:#886db4;
	}
#Pager a:hover	
{
	border:1px solid #999;
	text-decoration:none;
	padding:3px 6px;
	margin:0 2px;
	color:#fff;
	background:#c48c4b;
	}

#Pager span.currentPager
{
	border:1px solid #ddd;
	text-decoration:none;
	padding:3px 6px;
	margin:0 2px;
	background:#fafafa;
	}
	
    </style>
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