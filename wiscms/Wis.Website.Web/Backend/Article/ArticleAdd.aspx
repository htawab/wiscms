<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ArticleAdd.aspx.cs"
    Inherits="Wis.Website.Web.Backend.Article.ArticleAdd" %>

<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.HtmlEditorControls" TagPrefix="HtmlEditorControls" %>
<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="Wis" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/Website.js" language="javascript" type="text/javascript"></script>
    <link href="../images/MessageBox/MessageBox.css" rel="stylesheet" type="text/css" />
    <script src="../images/MessageBox/MessageBox.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" src="../../editor/fckeditor.js"></script>
<script language="javascript" type="text/javascript">
<!--
MessageBox.init("84611745", "84611745");
//-->
</script>
    <script type="text/javascript">
    <%=ViewState["javescript"] %>
    function checkNews() {
        if (document.getElementById("title").value == "") {
            alert("标题不能为空！");
            return false;
        }
        if (document.getElementById("CategoryId").value == "") {
            alert("栏目不能为空！");
            return false;
        }
        return true;
    }
    var randNum = 2;
    function Url_add() {
        var tempstr = '<div id="fujian' + randNum + '">&nbsp;名称：<input name="URLName" type="text" style="width:100px;" value="" class="form" id="URLName" />&nbsp;附件地址：<input name="FileUrl" type="text" style="width:250px;"  value="" class="form" id="FileUrl' + randNum + '" />&nbsp;<img src="../../Images/folder.gif" alt="选择已有附件" border="0" style="cursor:pointer;" onclick="selectFile(\'file\',document.form1.FileUrl' + randNum + ',280,500);document.form1.FileUrl' + randNum + '.focus();" />&nbsp;<span onclick="selectFile(\'UploadFile\',document.form1.FileUrl'+randNum +',165,500);document.form1.FileUrl'+randNum +'.focus();" style="cursor:hand;color:Red;">上传新附件</span>&nbsp;排序：<input name="Rank" type="text" onKeyPress="if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57))))return false;" id="Rank" value="0" style="width:50px;"  class="form" />&nbsp<a href="#" onclick="URL_delete(' + randNum + ')" class="list_link">删除</a></div>';
        window.document.getElementById("temp").innerHTML += tempstr;
        randNum = randNum + 1;
    }
    function URL_delete(randNum) {
        var divname = "fujian" + randNum;
        window.document.getElementById(divname).innerHTML = "";
        window.document.getElementById(divname).style.display = "none";
    }
    function slectArticletype()
    {
       var  articleType = document.getElementsByName("ArticleType") 
       if(articleType[0].checked)
       {
         document.getElementById("divTabloidPath").style.display = "none";
         document.getElementById("divTabloidPathVideo").style.display = "none";
       } 
       else if(articleType[1].checked)
       {
        document.getElementById("divTabloidPath").style.display = "block";
        document.getElementById("divTabloidPathVideo").style.display = "none";
       }
       else if(articleType[2].checked)
       {
         document.getElementById("divTabloidPathVideo").style.display = "block";
         document.getElementById("divTabloidPath").style.display = "none";
     }
    }
    </script>
    <script type="text/javascript" language="javascript">
    function S(id, win) {
        try {
            return ( win || window ).document.getElementById(id);
        }
        catch( e ) {
            return null;
        }
    }
    
    function Show(a, o, t, f) {
        a = (typeof(a) == "string" ? S(a) : a);
        o = (typeof(o) == "string" ? S(o) : o);
        f = (typeof(f) == "string" ? S(f) : f);
        
        if (o) o.style.display = ((o.style.display == "none") ? "" : "none");
        a.innerHTML = ((o.style.display == "") ? "删除" : "添加") + t;
        if((o.style.display == ""))f.focus();
    }

    function Switch(o, bShow) {
        o = (typeof(o) == "string" ? S(o) : o);
        if (o) o.style.display= (bShow ? "" : "none");
    }

    function CheckFile() {
        var c = S( 'filecell' ).childNodes.length > 1 || ( S( 'exist_file' ) && S( 'exist_file' ).childNodes.length > 0 )
	    var s = [ [ true, false ],[ false, true ] ][ c > 0 ? 1 : 0 ];
	    Switch( 'sAddAtt1', s[ 0 ] );
	    Switch( 'sAddAtt2', s[ 1 ] );
	    Switch( 'Files', s[ 1 ] );
    }

    function DelFile( Name ) {
        var FileCell = S('filecell');
        var FileObj  = S( Name );
        FileCell.removeChild( FileObj.parentNode );
        CheckFile();
    }

    var AttachID = 1;
    function AddFile() {
	    document.getElementById("Uploader" + (AttachID-1)).click();
	    CheckFile();
    }

    function AddFileCell() {
        var FileCell = S('filecell');

        var Name = "Uploader" + AttachID;
        AttachID++;
        
        if(S(Name)) return;

        var template = "<input class='ico_att' style='margin: 0px 3px 2px 0px' type='button' value='' />&nbsp;";
        template += "<input class='file upload' id='" + Name + "' type='file' onchange='AfterAddFile(\"" + Name + "\")' name='" + Name + "' value='' />&nbsp;";
        template += "<span id='S" + Name + "'></span>&nbsp;";
        template += "<span>&nbsp;&nbsp;</span><a onclick=\"DelFile(\'" + Name + "\')\">删除</a>";
        
        var Div       = document.createElement("div");
        Div.className = "attsep upload";
        Div.id        = "D" + Name;
        Div.innerHTML = template;

        Switch( Div, true);
        FileCell.appendChild( Div );
    }

    function AfterAddFile( id ) {
        var filename = S( id ).value;
        var pos      = filename.lastIndexOf("\\");
        S( "D" + id ).className = "attsep";
        S( "S" + id ).innerText = ( pos == -1 ? filename : filename.substr( pos + 1 ) );
        Switch( "D" + id, true);
        
        AddFileCell();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
        <div class="position">内容管理 » <span class="red" runat="server" id="daohang"></span><span runat="server" id="jiantou"></span><span class="red">添加新闻</span></div>
        <div class="schBox">
            <label>新闻类型：</label><input runat="server" id="ArticleType0" type="radio" onclick="slectArticletype();" name="ArticleType" value="0" checked />普通
            <input runat="server" id="ArticleType1" type="radio" onclick="slectArticletype();" name="ArticleType" value="1" />图片
            <input runat="server" id="ArticleType2" type="radio" onclick="slectArticletype();" name="ArticleType" value="2" />视频
            <input runat="server" id="ArticleType3" type="radio" onclick="slectArticletype();" name="ArticleType" value="3" />软件
            <br />
            <div style="margin-left: 82px; padding-top:5px; height: 25px">
                <a id="aSubTitle" href="javascript:Show('aSubTitle', 'divSubTitle', '副标题', 'SubTitle');">添加副标题</a>&nbsp;|&nbsp;
                <a id="aSummary" href="javascript:Show('aSummary', 'divSummary', '摘要', 'Summary');">添加摘要</a>&nbsp;|&nbsp;
                <a id="aMeta" title="什么是Meta信息：meta标签是内嵌在你网页中的特殊html标签，包含着你有关于你网页的一些隐藏信息。Meat标签的作用是向搜索引擎解释你的网页是有关哪方面信息的。" href="javascript:Show('aMeta', 'divMeta', 'Meta信息', 'MetaKeywords');">添加Meta信息</a>
                <A id="AttachFrame" title="添加小于 50M 的文件作为附件" style="MARGIN-RIGHT: 10px" onfocus="this.blur()" onclick="AddFile();" sizelimit="50"><INPUT class="ico_att" type="button" align="" value=""/><SPAN id="sAddAtt1">添加附件</SPAN><SPAN id="sAddAtt2" style="DISPLAY: none">继续添加</SPAN></A>
            </div>
            <div style="margin-right: 20px; height: 30px;" id="divTitle">
            <label for="title">标&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;题：</label><input type="text" runat="server"
                    id="title" class="keyText" name="title" /><input name="TitleColor" runat="server"
                        style="display: none;" id="TitleColor" type="text" size="10" /><img border="0" src="../../images/rect.gif"
                            width="18" style="background-color: #FFFFFF; cursor: hand;" id="SelectTitleColor"
                            onclick='SelectColor("TitleColor","SelectTitleColor")' align="absmiddle">
            </div>
            <div style="margin-right: 20px; height: 33px; display:none" id="divSubTitle">
            <label for="SubTitle">副 标 题：</label><input id="SubTitle" runat="server" class="keyText" type="text" name="SubTitle" />
            </div>
            <label>作&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;者：</label><input id="Author" type="text" size="30" runat="server" name="Author" />
            <label>来&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;源：</label><input id="Original" type="text" runat="server" size="30" name="Original" />
            <br />
            <label style="float:left; padding-top:8px;">栏&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目：</label><div style="float:left; margin-bottom:3px;"><Wis:DropdownMenu ID="Category" runat="server" ImagePath="../images/DropdownMenu/" /></div><br />
            <div runat="server" id="divTabloidPath" style="display: none;">
               cript:hiddDivPic();" size="50" id="ImagePath" name="ImagePath" /><img
                            src="../../images/folder.gif" alt="选择已有图片" border="0" style="cursor: pointer;"
                            onclick="selectFile('pic',document.form1.ImagePath,350,500);document.form1.ImagePath.focus();" />
                <span onclick="selectFile('UploadImage',document.form1.ImagePath,165,500);document.form1.ImagePath.focus();"
                    style="cursor: hand; color: Red;">上传新图片</span>
                <img src="../images/createthumb.png" border="0" style="cursor: pointer;" onclick="selectFile('cutimg',document.form1.ImagePath,500,800);document.form1.ImagePath.focus();" />
                <br />
            </div>
            <div runat="server" id="divTabloidPathVideo" style="display: none;">
                <label>视频地址：</label><input type="text" runat="server" size="50" id="TabloidPathVideo" name="TabloidPathVideo" /><img
                        src="../../images/folder.gif" alt="选择已有视频" border="0" style="cursor: pointer;"
                        onclick="selectFile('video',document.form1.TabloidPathVideo,350,500);document.form1.TabloidPathVideo.focus();" />
                <span onclick="selectFile('UploadVideo',document.form1.TabloidPathVideo,165,500);document.form1.TabloidPathVideo.focus();"
                    style="cursor: hand; color: Red;">上传新视频</span>
                <br />
            </div>
            <div style="margin-right: 20px; height: 33px; display:none" id="divMeta">
            <label>meta关键字：</label><textarea name="MetaKeywords" runat="server" id="MetaKeywords" rows="4"
                    cols="70"></textarea>
            <br />
            <label>meta描述：</label><textarea name="MetaDesc" runat="server" id="MetaDesc" rows="4" cols="70"></textarea>
            </div>
            <div style="margin-right: 20px; height: 33px; display:none" id="divSummary">
            <label>
                摘&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;要：</label><textarea name="Summary"
                    runat="server" id="Summary" rows="4" cols="70"></textarea>
            </div>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="height: 300px; width: 60px;">
                        <label style="vertical-align: top;">
                            内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：
                            <br />
                            <span id="Div2" onclick="selectFile('videoEdit',document.form1.Summary,400,650);" style="cursor: hand; color: Red;">插入/编辑视频</span>
                        </label>
                    </td>
                    <td valign="top"><HtmlEditorControls:HtmlEditor ID="ContentHtml" runat="server" DialogsPath="../images/HtmlEditor/"></HtmlEditorControls:HtmlEditor></td>
                </tr>
                <tr>
                    <td style="width: 60px;">
                        <label>附件列表：</label>
                    </td>
                    <td>
                        <div id="temp">
                            <div id="fujian1" style="margin-bottom: 1px;">
                                &nbsp;名称：<input name="URLName" type="text" style="width: 100px;" value="" class="form"
                                    id="URLName" />&nbsp;附件地址：<input name="FileUrl" type="text" style="width: 250px;"
                                        value="" class="form" id="FileUrl1" />&nbsp;<img src="../../Images/folder.gif" alt="选择已有附件"
                                            border="0" style="cursor: pointer;" onclick="selectFile('file',document.form1.FileUrl1,280,500);document.form1.FileUrl1.focus();" />&nbsp;<span
                                                onclick="selectFile('UploadFile',document.form1.FileUrl1,165,500);document.form1.FileUrl1.focus();"
                                                style="cursor: hand; color: Red;">上传新附件</span>&nbsp;排序：<input name="Rank" onkeypress="if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57))))return false;"
                                                    type="text" id="Rank" value="0" style="width: 50px;" class="form" />&nbsp;<font color="red"><a
                                                        href="javascript:Url_add()" class="list_link"><strong>添加附件</strong></a></font></div>
                        </div>
                    </td>
                </tr>
            </table>
            <div id="Files" class="bd_upload content_title attbg" style="DISPLAY: none; padding-right: 8px; padding-left: 6px; padding-bottom: 4px; width: 99%; padding-top: 8px; font-family: Tahoma" valign="top">
                <label>附件列表：</label><div id="exist_file"></div>
                <div id="filecell" style="margin-bottom: 4px">
                    <div class="attsep" id="DUploader0" style="display: none">
                        <input class="ico_att" style="margin: 0px 3px 2px 0px" type="button" value="" />
                        <input class="file upload" id="Uploader0" type="file" onchange="AfterAddFile('Uploader0')" name="Uploader0" value="" />
                        <span id="SUploader0"></span>&nbsp;<span>&nbsp;&nbsp;</span><a onclick="DelFile('Uploader0')">删除</a>
                    </div>                   
                </div>
                <div id="BigAttach"></div>
            </div>
            <style>
.attbg {	BACKGROUND-COLOR: #e0ecf9} 
.bd_upload {	BORDER-RIGHT: #4e86c4 1px solid;	BORDER-TOP: #4e86c4 1px solid;	BORDER-LEFT: #4e86c4 1px solid;	BORDER-BOTTOM: #4e86c4 1px solid}
.content_title {PADDING-RIGHT: 0px;PADDING-LEFT: 12px;PADDING-BOTTOM: 5px;PADDING-TOP: 0px}
.ico_att {	BORDER-RIGHT: medium none;	PADDING-RIGHT: 0px;	BORDER-TOP: medium none;	PADDING-LEFT: 0px;	BACKGROUND: url(../images/compose.gif) no-repeat 0px -53px;	PADDING-BOTTOM: 0px;	BORDER-LEFT: medium none;	WIDTH: 12px;	PADDING-TOP: 0px;	BORDER-BOTTOM: medium none;	HEIGHT: 13px}
.file {	MARGIN: 4px auto 2px;	FONT: bold 12px "lucida Grande",Verdana}
.attsep { MARGIN-BOTTOM: 3px}
.upload {DISPLAY: none}        
            </style>
            <br />
            <label>
                模&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;板：</label><asp:DropDownList ID="TemplatePaths"
                    runat="server" DataTextField="Title" DataValueField="TemplatePath">
                </asp:DropDownList>
            <br />
            <label>
                保存路径：</label><input contenteditable="false" type="text" runat="server" id="ReleasePath"
                    size="50" name="ReleasePath" /><img src="../../sysImages/folder/s.gif" alt="" border="0"
                        style="cursor: pointer;" onclick="selectFile('ReleasePath',document.form1.ReleasePath,250,500);document.form1.ReleasePath.focus();" />
            <br />
            <asp:Button ID="btnOK" runat="server" Text="" CssClass="saveBtn" OnClick="btnOK_Click" />
        </div>
    </div>
    </form>
</body>
</html>
