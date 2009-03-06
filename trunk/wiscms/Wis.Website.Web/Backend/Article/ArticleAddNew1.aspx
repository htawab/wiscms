<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ArticleAddNew1.aspx.cs"
    Inherits="Wis.Website.Web.Backend.Article.ArticleAdd" %>

<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.HtmlEditorControls" TagPrefix="HtmlEditorControls" %>
<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls.DropdownMenus" tagprefix="DropdownMenus" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <link href="../images/MessageBox/MessageBox.css" rel="stylesheet" type="text/css" />
    <script src="../images/MessageBox/MessageBox.js" language="javascript" type="text/javascript"></script>
    <script src="wis.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript">
    <%=ViewState["javescript"] %>

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
    <div class="right" id="Right">
        <div class="position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
        </div>
        <div id="divCategory" class="box">
            <label class="articleLabel">分 类：</label>
            <div class="dropdownmenu"><DropdownMenus:DropdownMenu ID="DropdownMenuCategory" runat="server" 
                ImagePath="../images/DropdownMenu/" /></div>
        </div>
        <div id="divTitle" class="box"> 
            <label class="articleLabel">标 题：</label>
            <asp:TextBox ID="Title" runat="server" CssClass="title"></asp:TextBox>
            <input name="TitleColor" runat="server" style="display: none;" id="TitleColor" type="text" size="10" />
            <img border="0" src="../images/rect.gif" width="18" style="background-color: #FFFFFF; cursor: hand;" id="SelectTitleColor" onclick='SelectColor("TitleColor","SelectTitleColor")' align="absmiddle">
        </div>
        <div id="divArticleType" class="box">
            <label class="articleLabel">新闻类型：</label>
            <input runat="server" id="ArticleType0" type="radio" onclick="slectArticletype();" name="ArticleType" value="0" checked />普 通
            <input runat="server" id="ArticleType1" type="radio" onclick="slectArticletype();" name="ArticleType" value="1" />图 片
            <input runat="server" id="ArticleType2" type="radio" onclick="slectArticletype();" name="ArticleType" value="2" />视 频
            <input runat="server" id="ArticleType3" type="radio" onclick="slectArticletype();" name="ArticleType" value="3" />软 件
            <br />
        </div>
        <div id="divShortcut" class="box">
            <a id="aSubTitle" href="javascript:Show('aSubTitle', 'divSubTitle', '副标题', 'SubTitle');">添加副标题</a>&nbsp;|&nbsp;
            <a id="aSummary" href="javascript:Show('aSummary', 'divSummary', '摘要', 'Summary');">添加摘要</a>&nbsp;|&nbsp;
            <a id="aMeta" title="什么是Meta信息：meta标签是内嵌在你网页中的特殊html标签，包含着你有关于你网页的一些隐藏信息。Meat标签的作用是向搜索引擎解释你的网页是有关哪方面信息的。" href="javascript:Show('aMeta', 'divMeta', 'Meta信息', 'MetaKeywords');">添加Meta信息</a>
        </div>
        <div id="divSubTitle" style="display: none;" class="box">
            <label for="SubTitle" class="articleLabel">副 标 题：</label>
            <input id="SubTitle" runat="server" class="title" type="text" name="SubTitle" />
        </div>
        <div id="divTabloidPath" style="display: none;" class="box">
            <label class="articleLabel">缩 略 图：<label><span id="PreviewWidth">233</span>×<span id="PreviewHeight">22</span></label></label>
            <div class="slt">
                <div class="Preview" id="ImagePreview"></div>
                <div>
                    <asp:HiddenField ID="PointX" runat="server" />
                    <asp:HiddenField ID="PointY" runat="server" />
                    <asp:HiddenField ID="CropperWidth" runat="server" />
                    <asp:HiddenField ID="CropperHeight" runat="server" />
                    
                    <FileUploads:DJUploadController ID="DJUploadController1" runat="server" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/"  />
                    <input id='Photo' type='file' name='Photo' value='' style="display: none;" onchange="SelectImage();" />
                    <img src="../images/upLoadImg.gif" alt="选择图片" onclick='Photo_Load(event);' />
                </div>
            </div>
            <br />
        </div>
        <div id="divTabloidPathVideo" style="display: none;" class="box">
            <label class="articleLabel">视频地址：</label>
            <input type="text" runat="server" size="50" id="TabloidPathVideo" name="TabloidPathVideo" />
            <img src="../../images/folder.gif" alt="选择已有视频" border="0" style="cursor: pointer;" onclick="selectFile('video',document.form1.TabloidPathVideo,350,500);document.form1.TabloidPathVideo.focus();" />
            <span onclick="selectFile('UploadVideo',document.form1.TabloidPathVideo,165,500);document.form1.TabloidPathVideo.focus();" style="cursor: hand; color: Red;">上传新视频</span>
            <br />
        </div>
        <div id="divMeta" style="display: none;" class="box">
            <div class="box">
            <label class="articleLabel">meta关键字：</label>
            <textarea name="MetaKeywords" runat="server" id="MetaKeywords" rows="4" cols="70"></textarea>
            </div>
            <div class="box">
            <label class="articleLabel">meta描 述：</label>
            <textarea name="MetaDesc" runat="server" id="MetaDesc" rows="4" cols="70"></textarea>
            </div>
        </div>
        <div id="divSummary" style="display: none;" class="box">
            <label class="articleLabel">摘 要：</label>
            <textarea name="Summary" runat="server" id="Summary" rows="4" cols="70"></textarea>
            <br />
        </div>
   
        <div id="divContentHtml">
            <label class="articleLabel">内 容：</label>
            <HtmlEditorControls:HtmlEditor ID="ContentHtml" runat="server" DialogsPath="../images/HtmlEditor/"></HtmlEditorControls:HtmlEditor>
        </div>
        <div id="divAuthor" class="box">
            <label class="articleLabel">作 者：</label><input id="Author" type="text" size="30" runat="server" name="Author" value="" />
            <br />
        </div>
        <div id="divOriginal" class="box">
            <label class="articleLabel">来 源：</label><input id="Original" type="text" runat="server" size="30" name="Original" value="" />
            <br />
        </div>
        <div id="divTag">
            <label class="articleLabel" title="不同于一般的目录结构的分类方法，以较少的代价细化分类">主 题：</label><input id="Tags" type="text" runat="server" size="30" name="Tags" value="行业新闻" /> 
            主题用空格隔开<br />
        </div>
        <div id="divbtnOK" class="box">
            <asp:Button ID="btnOK" runat="server" Text="" CssClass="saveBtn" OnClick="btnOK_Click" />
        </div>
    </div>
    </form>
</body>
</html>