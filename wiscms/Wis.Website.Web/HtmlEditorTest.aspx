<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtmlEditorTest.aspx.cs" Inherits="Wis.Website.Web.HtmlEditorTest" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="Wis" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.HtmlEditorControls" tagprefix="HtmlEditorControls" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="PageHeader" runat="server">
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
   
    <cc1:MiniPager ID="MiniPager1" runat="server" >
    </cc1:MiniPager>
   
   <br />
   Video Play
   <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="360" height="270">
<param name="movie" value="Play.swf">
<param name="quality" value="high">
<param name="allowFullScreen" value="true" />
<param name="FlashVars" value="vcastr_file=happy_feet.flv&LogoUrl=Logo.swf" /> 
<embed src="Play.swf" allowFullScreen="true" FlashVars="vcastr_file=vcastr_file=thewitcher12.flv&LogoUrl=Logo.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="360" height="270"></embed>
</object><br />
<table border="0" cellpadding="0" cellspacing="0">
<tr>
<th>参数名称</th>
<th>参数说明</th>
<th>默认值</th>
<td>vcastr_file</td><td>方法2传递影片flv文件地址参数，多个使用|分开</td><td>空</td> 
<td>vcastr_title  影片标题参数，多个使用|分开，与方法2配合使用</td><td>空</td>
<td>vcastr_xml    方法3 传递影片flv文件地址参数，样板参考 (根目录下的vcastr.xml 文件)</td><td></td>
<td>IsAutoPlay    影片自动播放参数：0表示不自动播放，1表示自动播放</td><td>0</td>
<td>IsContinue    影片连续播放参数：0表示不连续播放，1表示连续循环播</td><td>1</td>
<td>IsRandom      影片随机播放参数：0表示不随机播放，1表示随机播放</td><td>0</td>
<td>DefaultVolume 默认音量参数 ：0-100 的数值，设置影片开始默认音量大小</td><td>100</td>
<td>BarPosition   控制栏位置参数 ：0表示在影片上浮动显示，1表示在影片下方显示</td><td>0</td>
<td>IsShowBar     控制栏显示参数 ：0表示不显示；1表示一直显示；2表示鼠标悬停时显示；3表示开始不显示，鼠标悬停后显示</td><td>2</td>
<td>BarColor      播放控制栏颜色，颜色都以0x开始16进制数字表示</td><td>0x000033</td>
<td>BarTransparent 播放控制栏透明度</td><td>60</td>
<td>GlowColor     按键图标颜色，颜色都以0x开始16进制数字表示</td><td>0x66ff00</td>
<td>IconColor     鼠标悬停时光晕颜色，颜色都以0x开始16进制数字表示</td><td>0xFFFFFF</td>
<td>TextColor     播放器文字颜色，颜色都以0x开始16进制数字表示</td><td>0xFFFFFF</td>
<td>LogoText      可以添加自己网站名称等信息(英文)</td><td>空</td>
<td>LogoUrl       可以从外部读取logo图片,注意自己调整logo大小,支持图片格式和swf格式</td><td>空</td>
<td>EndSwf        影片播放结束后,从外部读取swf文件，可以添加相关影片信息，影片分享等信息，需自己制作</td><td>空</td>
<td>BeginSwf      影片开始播放之前,从外部读取swf文件，可以添加广告，或者网站信息，需自己制作</td><td>空</td>
<td>IsShowTime    是否显示时间 : 0表示不显示时间，1表示显示时间</td><td>1</td>
<td>BufferTime    影片缓冲时间，单位（秒）</td><td>2</td>
</tr>
</table>
                                                                                                                   
<br />

		<object type="application/x-shockwave-flash" data="vcastr3.swf" width="650" height="500" id="vcastr3">
			<param name="movie" value="http://localhost:3419/vcastr3.swf"/> 
			<param name="allowFullScreen" value="true" />
			<param name="FlashVars" value="xml=
				<vcastr>
					<channel>
						<item>
							<source>happy_feet.flv</source>
							<duration></duration>
							<title></title>
						</item>
					</channel>
					<config>
					</config>
					<plugIns>
						<logoPlugIn>
							<url>logoPlugIn.swf</url>
							<logoText>www.everwis.com</logoText>
							<logoTextAlpha>0.75</logoTextAlpha>
							<logoTextFontSize>30</logoTextFontSize>
							<logoTextLink>http://www.everwis.com</logoTextLink>
							<logoTextColor>0xffffff</logoTextColor>
							<textMargin>20 20 auto auto</textMargin>
						</logoPlugIn>
					</plugIns>
				</vcastr>"/>
		</object>
		
		<br />
		<FileUploads:DJUploadController ID="DJUploadController1" runat="server" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/" />
		<FileUploads:DJFileUpload ID="DJFileUpload1" runat="server" AllowedFileExtensions=".png,.jpg,.gif" />
		<FileUploads:DJAccessibleProgressBar ID="DJAccessibleProgrssBar1" runat="server" />
		<asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="upButtonNormal" ajaxcall="none" Visible="false" />
    <%=DJFileUpload1.ClientID%>
    </form>
    
    <P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <SPAN lang="EN-US" 
            style="font-size: 14pt; line-height: 250%; font-family: 仿宋_GB2312; mso-bidi-font-size: 12.0pt"><p></p></SPAN></P>
</body>
</html>
