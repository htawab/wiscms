<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtmlEditorTest.aspx.cs" Inherits="Wis.Website.Web.HtmlEditorTest" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="Wis" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.HtmlEditorControls" tagprefix="HtmlEditorControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
<style type="text/css">
#Pager 
{
	color:#222;
	font-family:宋体;
	font-size:12px;
	line-height:14px;
	height:26px;
	}  
#Pager span 
{
	border:1px solid #ddd;
	padding:2px 6px;
	margin:0 2px;
	float:left;
	}
#Pager span.noLink
{
	color:#b9b9b9;
	}
#Pager a 	
{
	border:1px solid #ddd;
	text-decoration:none;
	padding:2px 6px;
	margin:0 2px;
	color:#886db4;
	float:left;
	background:#fcfcfc;
	}
#Pager a:hover	
{
	border:1px solid #999;
	color:#fff;
	background:#c48c4b;
	}

#Pager span.currentPager
{
	border:1px solid #faa;
	background:#fff;
	color:#f00;
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
   
   <br />
   Video Play
   <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="360" height="270">
<param name="movie" value="play.swf">
<param name="quality" value="high">
<param name="allowFullScreen" value="true" />
<param name="FlashVars" value="vcastr_file=thewitcher12.flv&LogoUrl=Logo.swf" /> 
<embed src="Play.swf" allowFullScreen="true" FlashVars="vcastr_file=vcastr_file=thewitcher12.flv&LogoUrl=Logo.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="360" height="270"></embed>
</object>

<br />

    </form>
</body>
</html>
