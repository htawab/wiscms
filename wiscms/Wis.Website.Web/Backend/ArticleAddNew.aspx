<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleAddNew.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleAddNew" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.HtmlEditorControls" TagPrefix="HtmlEditorControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>录入内容 - 内容管理 - 常智内容管理系统</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <script src="images/HtmlEditor/Dialogs/InsertPhotos/InsertPhoto.js" language="javascript" type="text/javascript"></script>
    <link href="images/HtmlEditor/Dialogs/InsertPhotos/InsertPhoto.css" rel="stylesheet" type="text/css" />
    <script src="Article/wis.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function CheckArticle() {
            if ($("title").value == "") {
                alert("标题不能为空！");
                return false;
            }
            $("Loading").style.display = "";
            return true;
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
<body style="background: #d6e7f7"><form id="form1" runat="server">
    <div>
        <div id="position">当前位置：<asp:HyperLink ID="HyperLinkCategory" runat="server"></asp:HyperLink> » 录入内容</div>
        <div class="add_step">
            <ul>
                <li>第一步：选择分类</li>
                <li class="current_step">第二步：录入内容</li>
                <li>第三步：录入更多内容</li>
                <li>第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main">
            <div id="divTitle" class="box">
                <label class="articleLabel">标 题：</label>
                <asp:TextBox ID="Title" runat="server" CssClass="title"></asp:TextBox>
                <input name="TitleColor" runat="server" style="display: none;" id="TitleColor" type="text" size="10" />
                <img border="0" src="images/rect.gif" width="18" style="background-color: #FFFFFF; cursor: hand;" id="SelectTitleColor" onclick='SelectColor("TitleColor","SelectTitleColor")' align="absmiddle">
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
            <div id="divSummary" style="display: none;" class="box">
                <label class="articleLabel">摘 要：</label>
                <textarea name="Summary" runat="server" id="Summary" rows="4" cols="70"></textarea>
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
            <div id="divContentHtml">
                <label class="articleLabel">内 容：</label>
                <HtmlEditorControls:HtmlEditor ID="ContentHtml" runat="server" DialogsPath="../images/HtmlEditor/"></HtmlEditorControls:HtmlEditor>
            </div>
            <div id="divAuthor" class="box">
                <label class="articleLabel">作 者：</label>
                <input id="Author" type="text" size="30" runat="server" name="Author" value="" />
                <br />
            </div>
            <div id="divOriginal" class="box">
                <label class="articleLabel">来 源：</label>
                <input id="Original" type="text" runat="server" size="30" name="Original" value="" />
                <br />
            </div>
            <div id="divTag">
                <label class="articleLabel" title="不同于一般的目录结构的分类方法，以较少的代价细化分类">主 题：</label>
                <input id="Tags" type="text" size="30" name="Tags" value="" /> 主题用空格隔开
                <br />
            </div>
        </div>
        <div id="Warning" runat="server"></div>
        <div id="Loading" style="display: none;"><img src='images/loading.gif' align='absmiddle' /> 上传中...</div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" OnClientClick="javascript:return CheckArticle();" />
        </div>
    </div></form>
</body>
</html>
