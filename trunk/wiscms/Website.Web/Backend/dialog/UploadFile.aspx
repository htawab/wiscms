<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="Wis.Website.Web.Backend.dialog.UploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>

    <link href="../../sysImages/default/css/css.css" rel="stylesheet" type="text/css" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
    function checkNews() {
        if (document.getElementById("FileUpload1").value == "") {
            alert("请选择需要上传到文件！");
            return false;
        }
        showfDiv(document.form1.FileUpload1,"login",300,200);
        return true;
    }
    function ReturnValue(obj) {
        var Str = obj;
        var Edit = '<% Response.Write(Request.QueryString["Edit"]);%>'
        if (Edit != null && Edit != "") {
            parent.insertHTMLEdit(Str);
        }
        else {
            parent.ReturnFun(Str);
        }
    }
         <%=ViewState["javescript"] %>
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
        <div class="schBox">
            <label for="title">
                文件上传</label>
            <asp:FileUpload ID="FileUpload1" CssClass="keyText" Width="300" runat="server" Height="22px" />
            <br />
            <label class="editor">
                &nbsp;&nbsp;&nbsp;&nbsp;文件保存规则</label><br />
            <label class="editor">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;图片：files/images/年-月/六位随机数-原文件名</label><br />
            <label class="editor">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;文件：files/file/年-月/六位随机数-原文件名</label><br />
            <label class="editor">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;视频flash：files/video/年-月/六位随机数-原文件名</label><br />
            <label class="editor">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><br />
            <asp:Button ID="Button1" runat="server" Text="" CssClass="saveBtn" OnClick="Button1_Click" />
        </div>
    </div>
    </form>
</body>
</html>
