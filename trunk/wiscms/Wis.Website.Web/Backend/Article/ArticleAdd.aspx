<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ArticleAdd.aspx.cs"
    Inherits="Wis.Website.Web.Backend.Article.ArticleAdd" %>

<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.HtmlEditorControls" TagPrefix="HtmlEditorControls" %>
<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="DropdownMenus" %>
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
    <script type="text/javascript">
    <%=ViewState["javescript"] %>
    function checkNews() {
        if (document.getElementById("title").value == "") {
            alert("标题不能为空！");
            return false;
        }
//        if (document.getElementById("CategoryId").value == "") {
//            alert("栏目不能为空！");
//            return false;
//        }
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
    /*
    1、label中的标题不要加&nbsp;，可以固定长度右对齐；
    2、样式根据Id来设计，比如keyText应该是Title；
    3、尽量少写内嵌的样式，比如 <div id="divSummary"> 就不要写 <div id="divSummary" style="margin-right: 20px; height: 30px;">
    4、Html和CSS代码要简洁
    */
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
        <div class="position">
            <asp:SiteMapPath ID="SiteMapPath1" runat="server" PathSeparator="»"><RootNodeTemplate>内容管理</RootNodeTemplate><CurrentNodeTemplate>添加新闻</CurrentNodeTemplate></asp:SiteMapPath>
            内容管理 » <span class="red" runat="server" id="daohang"></span><span runat="server" id="jiantou"></span><span class="red">添加新闻</span>
        </div>
        <div id="divCategory">
            <label>分类：</label>
            <DropdownMenus:DropdownMenu ID="Category" runat="server" ImagePath="../images/DropdownMenu/" /><br />
        </div>
        <div id="divTitle"> 
            <label>标题：</label>
            <input type="text" runat="server" id="title" class="keyText" name="title" />
            <input name="TitleColor" runat="server" style="display: none;" id="TitleColor" type="text" size="10" />
            <img border="0" src="../../images/rect.gif" width="18" style="background-color: #FFFFFF; cursor: hand;" id="SelectTitleColor" onclick='SelectColor("TitleColor","SelectTitleColor")' align="absmiddle">
        </div>
        <div id="divAuthor">
            <label>作者：</label><input id="Author" type="text" size="30" runat="server" name="Author" />
            <label>来源：</label><input id="Original" type="text" runat="server" size="30" name="Original" />
            <br />
        </div>
        <div id="divArticleType">
            <label>新闻类型：</label>
            <input runat="server" id="ArticleType0" type="radio" onclick="slectArticletype();" name="ArticleType" value="0" checked />普通
            <input runat="server" id="ArticleType1" type="radio" onclick="slectArticletype();" name="ArticleType" value="1" />图片
            <input runat="server" id="ArticleType2" type="radio" onclick="slectArticletype();" name="ArticleType" value="2" />视频
            <input runat="server" id="ArticleType3" type="radio" onclick="slectArticletype();" name="ArticleType" value="3" />软件
            <br />
        </div>
        <div id="divShortcut">
            <a id="aSubTitle" href="javascript:Show('aSubTitle', 'divSubTitle', '副标题', 'SubTitle');">添加副标题</a>&nbsp;|&nbsp;
            <a id="aSummary" href="javascript:Show('aSummary', 'divSummary', '摘要', 'Summary');">添加摘要</a>&nbsp;|&nbsp;
            <a id="aMeta" title="什么是Meta信息：meta标签是内嵌在你网页中的特殊html标签，包含着你有关于你网页的一些隐藏信息。Meat标签的作用是向搜索引擎解释你的网页是有关哪方面信息的。" href="javascript:Show('aMeta', 'divMeta', 'Meta信息', 'MetaKeywords');">添加Meta信息</a>
        </div>
        <div id="divSubTitle" style="display: none;">
            <label for="SubTitle">副 标 题：</label>
            <input id="SubTitle" runat="server" class="keyText" type="text" name="SubTitle" />
        </div>
        <div id="divTabloidPath" style="display: none;">
            <label>图片地址：</label>
            <input type="text" runat="server" onmouseover="javascript:ShowDivPic(this,document.form1.ImagePath.value.toLowerCase().replace('{@dirfile}','files').replace('{@userdirfile}','userfiles'),'.jpg',1);" onmouseout="javascript:hiddDivPic();" size="50" id="ImagePath" name="ImagePath" />
            <img src="../../images/folder.gif" alt="选择已有图片" border="0" style="cursor: pointer;" onclick="selectFile('pic',document.form1.ImagePath,350,500);document.form1.ImagePath.focus();" />
            <span onclick="selectFile('UploadImage',document.form1.ImagePath,165,500);document.form1.ImagePath.focus();" style="cursor: hand; color: Red;">上传新图片</span>
            <img src="../images/createthumb.png" border="0" style="cursor: pointer;" onclick="selectFile('cutimg',document.form1.ImagePath,500,800);document.form1.ImagePath.focus();" />
            <br />
        </div>
        <div id="divTabloidPathVideo" style="display: none;">
            <label>视频地址：</label>
            <input type="text" runat="server" size="50" id="TabloidPathVideo" name="TabloidPathVideo" />
            <img src="../../images/folder.gif" alt="选择已有视频" border="0" style="cursor: pointer;" onclick="selectFile('video',document.form1.TabloidPathVideo,350,500);document.form1.TabloidPathVideo.focus();" />
            <span onclick="selectFile('UploadVideo',document.form1.TabloidPathVideo,165,500);document.form1.TabloidPathVideo.focus();" style="cursor: hand; color: Red;">上传新视频</span>
            <br />
        </div>
        <div id="divMetaKeywords" style="display: none;">
            <label>meta关键字：</label>
            <textarea name="MetaKeywords" runat="server" id="MetaKeywords" rows="4" cols="70"></textarea>
            <br />
        </div>
        <div id="divMetaDesc" style="display: none;">
            <label>meta描述：</label>
            <textarea name="MetaDesc" runat="server" id="MetaDesc" rows="4" cols="70"></textarea>
            <br />
        </div>
        <div id="divSummary">
            <label>摘要：</label>
            <textarea name="Summary" runat="server" id="Summary" rows="4" cols="70"></textarea>
            <br />
        </div>
        <div id="divContentHtml">
            <label>内&nbsp;&nbsp;容：</label>
            <HtmlEditorControls:HtmlEditor ID="ContentHtml" runat="server" DialogsPath="../images/HtmlEditor/"></HtmlEditorControls:HtmlEditor>
            <br />
        </div>
        <div id="divbtnOK">
            <asp:Button ID="btnOK" runat="server" Text="" CssClass="saveBtn" OnClick="btnOK_Click" />
        </div>
    </div>
    </form>
</body>
</html>
