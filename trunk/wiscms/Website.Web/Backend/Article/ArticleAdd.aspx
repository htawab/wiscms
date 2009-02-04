<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ArticleAdd.aspx.cs"
    Inherits="Wis.Website.Web.Backend.Article.ArticleAdd" %>

<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.HtmlEditorControls"
    TagPrefix="cc1" %>
<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="Wis" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>
    <script src="../../JavaScript/Website.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" src="../../editor/fckeditor.js"></script>

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
            <label>栏&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目：</label><Wis:DropdownMenu ID="Category" runat="server" ImagePath="../images/DropdownMenu/" /><br />
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
            <%--            <label  style="height:300px;">
                内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：</label>
                 <script type="text/javascript" language="JavaScript">
                     window.onload = function() {
                         var sBasePath = "../../editor/"
                         var oFCKeditor = new FCKeditor('ContentHtml');
                         oFCKeditor.BasePath = sBasePath;

                         oFCKeditor.Width = '91%';
                         oFCKeditor.Height = '350';
                         oFCKeditor.ReplaceTextarea();
                     }
                        </script>
                        <textarea name="ContentHtml" rows="1" cols="1" style="display: none" id="ContentHtml"
                            runat="server"></textarea>--%>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="height: 300px; width: 60px;">
                        <label style="vertical-align: top;">
                            内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：
                            <br />
                            <span id="Div2" onclick="selectFile('videoEdit',document.form1.Summary,400,650);"
                                style="cursor: hand; color: Red;">插入/编辑视频</span>
                        </label>
                    </td>
                    <td valign="top">

                        <script type="text/javascript" language="JavaScript">
                            window.onload = function() {
                                var sBasePath = "../../editor/"
                                var oFCKeditor = new FCKeditor('ContentHtml');
                                oFCKeditor.BasePath = sBasePath;

                                oFCKeditor.Width = '98%';
                                oFCKeditor.Height = '300';
                                oFCKeditor.ReplaceTextarea();
                            }
                        </script>

                        <%--<cc1:HtmlEditor ID="ContentHtml" runat="server">
        </cc1:HtmlEditor>--%>
                        <textarea name="ContentHtml" rows="1" cols="1" style="display: none" id="ContentHtml"
                            runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px;">
                        <label>
                            附件列表：</label>
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
            <label>
                作&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;者：</label><input id="Author" type="text"
                    size="30" runat="server" name="Author" />
            <label>
                来&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;源：</label>
            <input id="Original" type="text" runat="server" size="30" name="Original" /><br />
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
