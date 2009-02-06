<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageUpdate.aspx.cs" Inherits="Wis.Website.Web.Backend.Article.PageUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            alert("页面标题不能为空！");
            return false;
        }
        if (document.getElementById("TemplatePath").value == "") {
            alert("模板路径不能为空！");
            return false;
        }
        if (document.getElementById("ReleasePath").value == "") {
            alert("保存路径以及文件名不能为空！");
            return false;
        }
        return true;
  }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
        <div class="position">
            栏目管理 » <span class="red" runat="server" id="daohang"></span><span runat="server"
                id="jiantou"></span><span class="red">添加单页面</span></div>
        <div class="schBox">
            <label for="title"><input  type=text id="pageGuid" runat=server name="pageGuid" style="display:none;"/>
                页面标题：</label><input type="text" runat="server" id="title" class="keyText" name="title" /><input
                    name="TitleColor" runat="server" style="display: none;" id="TitleColor" type="text"
                    size="10" /><br />
            <label>
                所属栏目：</label><input id="CategoryId" runat="server" type="text" name="CategoryId"
                    style="display: none;" /><input id="CategoryName" runat="server" type="text" size="30"
                        name="CategoryName" /><img src="../../images/folder.gif" alt="选择已有标签" border="0"
                            style="cursor: pointer;" onclick="selectFile('CategoryList',new Array(document.form1.CategoryId,document.form1.CategoryName),250,500);document.form1.CategoryName.focus();" /><span
                                class="red">无所属栏目可以为空</span>
            <br />
            <label for="MetaKeywords">
                meta关键字：</label><textarea name="MetaKeywords" runat="server" id="MetaKeywords" rows="4"
                    cols="70"></textarea>
            <br />
            <label>
                meta描述：</label><textarea name="MetaDesc" runat="server" id="MetaDesc" rows="4" cols="70"></textarea>
            <br />
            <label>
                模板路径：</label><input type="text" ContentEditable="false" runat="server" id="TemplatePath" size="50" name="TemplatePath" /><img
                    src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor: pointer;"
                    onclick="selectFile('templet',document.form1.TemplatePath,250,500);document.form1.TemplatePath.focus();" />
            <br />
            <label>
                路径文件名：</label><input id="ReleasePath" runat="server" type="text" size="30" name="ReleasePath" /><br />
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
                            <span id="Div2" onclick="selectFile('videoEdit',document.form1.MetaDesc,400,650);"
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
            </table>
            <asp:Button ID="Button1" runat="server" Text="" CssClass="saveBtn" OnClick="Button1_Click" />
        </div>
    </div>
    </form>
</body>
</html>
