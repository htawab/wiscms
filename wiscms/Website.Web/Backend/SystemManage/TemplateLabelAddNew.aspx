<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateLabelAddNew.aspx.cs" Inherits="Wis.Website.Web.Backend.SystemManage.TemplateLabelAddNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../editor/fckeditor.js"></script>

    <script type="text/javascript">
    <%=ViewState["javescript"] %>
    function checkNews() {
        if (document.getElementById("TagName").value == "") {
            alert("标签名称不能为空！");
            return false;
        }

        return true;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="right" style="height:480px;">
        <div class="position">
            标签管理 » <span class="red" runat="server" id="daohang"></span><span runat="server"
                id="jiantou"></span><span class="red">新建标签</span></div>
        <div class="schBox">
            <label for="title">
                标签名称：</label><span class="red">$Tag_<input type="text" runat="server"
                    id="TagName"  name="TagName" />$</span><br />
            <label for="Description">
                标签描述：</label><input id="Description" runat="server" class="keyText" type="text" name="Description" />
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
                        <label style="vertical-align: top;">
                            内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：
                        </label>
                    </td>
                    <td valign="top">
                        

                        <textarea name="ContentHtml" rows="15" cols="80"  id="ContentHtml"
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
