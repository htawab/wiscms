<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleUpdate.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleUpdate" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.HtmlEditorControls" TagPrefix="HtmlEditorControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>内容修改 - 内容管理 - 常智内容管理系统</title>
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
            a = (typeof(a) == "string" ? $(a) : a);
            o = (typeof(o) == "string" ? $(o) : o);
            f = (typeof(f) == "string" ? $(f) : f);
            
            if (o) o.style.display = ((o.style.display == "none") ? "" : "none");
            a.innerHTML = ((o.style.display == "") ? "删除" : "添加") + t;
            if((o.style.display == ""))f.focus();
        }
    </script>
</head>
<body style="background: #d6e7f7">
    <form id="form1" runat="server">
    <div id="Position">当前位置：<asp:HyperLink ID="HyperLinkCategory" runat="server"></asp:HyperLink> » 修改内容</div>
    <div class="add_step">
      <ul>
        <li class="current_step">第一步：修改基本内容</li>
        <li>第二步：修改更多内容</li>
        <li>第三步：发布静态页</li>
      </ul>
    </div>
    
    <div class="add_main">
      <div id="divTitle" class="box">
        <label class="articleLabel">标 题：</label>
        <asp:TextBox ID="TextBoxTitle" runat="server" CssClass="title"></asp:TextBox>
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
         <asp:TextBox ID="TextBoxSubTitle" runat="server" CssClass="title"></asp:TextBox>
      </div>
      <div id="divSummary" style="display: none;" class="box">
         <label class="articleLabel">摘 要：</label>
         <asp:TextBox ID="TextBoxSummary" runat="server" TextMode="MultiLine" Width="420" Height="60"></asp:TextBox>
         <br />
      </div>
      
      <div id="divMeta" style="display: none;" class="box">
          <div class="box">
            <label class="articleLabel">meta关键字：</label>
            <asp:TextBox ID="TextBoxMetaKeywords" runat="server" TextMode="MultiLine" Width="420" Height="60"></asp:TextBox>
          </div>
          <div class="box">
            <label class="articleLabel">meta描 述：</label>
            <asp:TextBox ID="TextBoxMetaDesc" runat="server" TextMode="MultiLine" Width="420" Height="60"></asp:TextBox>
          </div>
      </div>
      
      <div id="divContentHtml">
          <label class="articleLabel">内 容：</label>
          <HtmlEditorControls:HtmlEditor ID="ContentHtml" runat="server" DialogsPath="../images/HtmlEditor/"></HtmlEditorControls:HtmlEditor>
      </div>
      
      <div id="divAuthor" class="box">
        <label class="articleLabel">作 者：</label>
        <asp:TextBox ID="TextBoxAuthor" runat="server"></asp:TextBox>
        <br />
      </div>
      
      <div id="divOriginal" class="box">
        <label class="articleLabel">来 源：</label>
        <asp:TextBox ID="TextBoxOriginal" runat="server"></asp:TextBox>
        <br />
      </div>
      
      <div id="divTag">
        <label class="articleLabel" title="不同于一般的目录结构的分类方法，以较少的代价细化分类">主 题：</label>
        <asp:TextBox ID="TextBoxTags" runat="server"></asp:TextBox>主题用空格隔开
        <br />
      </div>
    
    
    
    </div>
    
    
    <div id="Warning" runat="server"></div>
    <div id="Loading" style="display: none;"><img src='images/loading.gif' align='absmiddle' /> 上传中...</div>
    <div class="add_button">
      <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" OnClientClick="javascript:return CheckArticle();" />
    </div>
    </form>
</body>
</html>
