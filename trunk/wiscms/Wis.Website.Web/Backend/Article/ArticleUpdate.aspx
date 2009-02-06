<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleUpdate.aspx.cs"
    Inherits="Wis.Website.Web.Backend.Article.ArticleUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Website.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Ajax.js" language="javascript" type="text/javascript"></script>

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
        if (document.getElementById("TemplatePath").value == "") {
            alert("模板路径不能为空！");
            return false;
        }
        return true;
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
    var randNum = 2;
    function Url_add() {
        var tempstr = '<div id="fujian' + randNum + '">&nbsp;名称：<input name="URLName" type="text" style="width:100px;" value="" class="form" id="URLName" />&nbsp;附件地址：<input name="FileUrl" type="text" style="width:250px;"  value="" class="form" id="FileUrl' + randNum + '" />&nbsp;<img src="../../Images/folder.gif" alt="选择已有附件" border="0" style="cursor:pointer;" onclick="selectFile(\'file\',document.form1.FileUrl' + randNum + ',280,500);document.form1.FileUrl' + randNum + '.focus();" />&nbsp;<span onclick="selectFile(\'UploadFile\',document.form1.FileUrl'+randNum +',165,500);document.form1.FileUrl'+randNum +'.focus();" style="cursor:hand;color:Red;">上传新附件</span>&nbsp;排序：<input name="FileOrderID" type="text" id="FileOrderID" onKeyPress="if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57))))return false;" value="0" style="width:50px;"  class="form" />&nbsp<a href="#" onclick="URL_delete(' + randNum + ')" class="list_link">删除</a></div>';
        window.document.getElementById("temp").innerHTML += tempstr;
        randNum = randNum + 1;
    }
    function URL_delete(randNum) {
        var divname = "fujian" + randNum;
        window.document.getElementById(divname).innerHTML = "";
        window.document.getElementById(divname).style.display = "none";
    }
    function filedelete(id)
    {
     if(!confirm("您确定要删除吗？")) return false;
      var WebsiteObj = new Object();
     WebsiteObj.Article = new Object();
     WebsiteObj.Article.Ajax = new AJAXRequest;
    if (!WebsiteObj.Article.Ajax) return;
      WebsiteObj.Article.Ajax.get('/AjaxRequests/Article.aspx?command=DleteFile&FileId=' + id,
	    function(req) {
     if(req.responseText=="1") // 定义总记录数(必要)
    {
       Website.Get("files"+id).innerHTML = "";
       Website.Get("files"+id).style.display = "none";
    }
    else
    {
             alert("删除失败！");
    }
    });
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
        <div class="position">
            内容管理 » <span class="red" runat="server" id="daohang"></span>» <span class="red">修改新闻</span></div>
        <div class="schBox">
            <label>
                新闻类型：</label><input type="radio" runat="server" id="ArticleType0" onclick="slectArticletype();"
                    name="ArticleType" value="0" />普通
            <input runat="server" id="ArticleType1" type="radio" onclick="slectArticletype();"
                name="ArticleType" value="1" />图片
            <input runat="server" id="ArticleType2" type="radio" onclick="slectArticletype();"
                name="ArticleType" value="2" />视频
            <br />
            <label for="title">
                标&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;题：</label><input type="text" runat="server"
                    id="title" class="keyText" name="title" /><input type="text" runat="server" id="ArticleGuid"
                        style="display: none;" class="keyText" name="ArticleGuid" /><input name="TitleColor"
                            runat="server" style="display: none;" id="TitleColor" type="text" size="10" /><img
                                border="0" src="../../images/rect.gif" width="18" style="background-color: <%=ViewState["TitleColor"] %>;
                                cursor: hand;" id="SelectTitleColor" onclick='SelectColor("TitleColor","SelectTitleColor")'
                                align="absmiddle"><br />
            <label for="SubTitle">
                副 标 题：</label><input id="SubTitle" runat="server" class="keyText" type="text" name="SubTitle" />
            <br />
            <label>
                栏 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目：</label><input id="CategoryId" runat="server"
                    type="text" name="CategoryId" style="display: none;" /><input id="CategoryGuid" runat="server"
                    type="text" name="CategoryGuid" style="display: none;" /><input id="CategoryName" readonly runat="server"
                        type="text" size="30" name="CategoryName" /><img src="../../images/folder.gif" alt="选择已有标签"
                            border="0" style="cursor: pointer;" onclick="selectFile('CategoryList',new Array(document.form1.CategoryGuid,document.form1.CategoryName),250,500);document.form1.CategoryName.focus();" />
            <br />
            <div runat="server" id="divTabloidPath" style="display: none;">
                <label>
                    图片地址：</label><input type="text" runat="server" onmouseover="javascript:ShowDivPic(this,document.form1.TabloidPath.value.toLowerCase().replace('{@dirfile}','files').replace('{@userdirfile}','userfiles'),'.jpg',1);"
                        onmouseout="javascript:hiddDivPic();" size="50" id="TabloidPath" name="TabloidPath" /><img
                            src="../../images/folder.gif" alt="选择已有图片" border="0" style="cursor: pointer;"
                            onclick="selectFile('pic',document.form1.TabloidPath,350,500);document.form1.TabloidPath.focus();" />
                <span onclick="selectFile('UploadImage',document.form1.TabloidPath,165,500);document.form1.TabloidPath.focus();"
                    style="cursor: hand; color: Red;">上传新图片</span> <img
                            src="../images/createthumb.png"  border="0" style="cursor: pointer;"
                            onclick="selectFile('cutimg',document.form1.ImagePath,500,800);document.form1.ImagePath.focus();" />
                <br />
            </div>
            <div runat="server" id="divTabloidPathVideo" style="display: none;">
                <label>
                    视频地址：</label><input type="text" runat="server" size="50" id="TabloidPathVideo" name="TabloidPathVideo" /><img
                        src="../../images/folder.gif" alt="选择已有视频" border="0" style="cursor: pointer;"
                        onclick="selectFile('video',document.form1.TabloidPathVideo,350,500);document.form1.TabloidPathVideo.focus();" />
                <span onclick="selectFile('UploadVideo',document.form1.TabloidPathVideo,165,500);document.form1.TabloidPathVideo.focus();"
                    style="cursor: hand; color: Red;">上传新视频</span>
                <br />
            </div>
            <label>
                meta关键字：</label><textarea name="MetaKeywords" runat="server" id="MetaKeywords" rows="4"
                    cols="70"></textarea>
            <br />
            <label>
                meta描述：</label><textarea name="MetaDesc" runat="server" id="MetaDesc" rows="4" cols="70"></textarea>
            <br />
            <label>
                摘&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;要：</label><textarea name="Summary"
                    runat="server" id="Summary" rows="4" cols="70"></textarea>
            <br />
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
                        <label>
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

                        <textarea name="ContentHtml" rows="1" cols="1" style="display: none" id="ContentHtml"
                            runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px;">
                        <label>
                            上传附件：</label>
                    </td>
                    <td>
                        <div id="temp">
                            <div id="fujian1" style="margin-bottom: 1px;">
                                &nbsp;名称：<input name="URLName" type="text" style="width: 100px;" value="" class="form"
                                    id="URLName" />&nbsp;附件地址：<input name="FileUrl" type="text" style="width: 250px;"
                                        value="" class="form" id="FileUrl1" />&nbsp;<img src="../../Images/folder.gif" alt="选择已有附件"
                                            border="0" style="cursor: pointer;" onclick="selectFile('file',document.form1.FileUrl1,280,500);document.form1.FileUrl1.focus();" />&nbsp;<span
                                                onclick="selectFile('UploadFile',document.form1.FileUrl1,165,500);document.form1.FileUrl1.focus();"
                                                style="cursor: hand; color: Red;">上传新附件</span>&nbsp;排序：<input name="FileOrderID"
                                                    onkeypress="if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57))))return false;"
                                                    type="text" id="FileOrderID" value="0" style="width: 50px;" class="form" />&nbsp;<font
                                                        color="red"><a href="javascript:Url_add()" class="list_link"><strong>添加附件</strong></a></font></div>
                        </div>
                    </td>
                </tr>
            </table>
            <div id="filetemp" runat="server">
            </div>
            <label>
                作&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;者：</label><input id="Author" type="text"
                    size="30" runat="server" name="Author" />
            <label>
                来&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;源：</label>
            <input id="Original" type="text" runat="server" size="30" name="Original" /><br />
            <label>
                模板路径：</label><input type="text" ContentEditable="false" runat="server" id="TemplatePath" size="50" name="TemplatePath" /><img
                    src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor: pointer;"
                    onclick="selectFile('templet',document.form1.TemplatePath,250,500);document.form1.TemplatePath.focus();" />
            <br />
            <label>
                保存路径：</label><input type="text" ContentEditable="false" runat="server" id="ReleasePath" size="50" name="ReleasePath" /><img
                    src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor: pointer;"
                    onclick="selectFile('ReleasePath',document.form1.ReleasePath,250,500);document.form1.ReleasePath.focus();" />
            <br />
            <asp:Button ID="Button1" runat="server" Text="" CssClass="saveBtn" OnClick="Button1_Click" />
        </div>
    </div>
    </form>
</body>
</html>
