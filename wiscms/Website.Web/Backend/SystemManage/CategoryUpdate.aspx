<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryUpdate.aspx.cs" Inherits="Wis.Website.Web.Backend.SystemManage.CategoryUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="right" id="divright">
        <div class="position">
            栏目管理 » <span class="red">栏目修改</span></div>
        <div class="addBox">
            <label class="long">
                栏目名称：</label><input type="text" id="CategoryName" runat="server" maxlength="20" /><br />
            <label class="long">
                父栏目：</label><input type="text" id="ParentGuid" name="ParentGuid" style="display: none;" runat="server" maxlength="20" /><input type="text" id="ParentName"  runat="server" maxlength="20" /><img src="../../images/folder.gif" alt="选择已有标签"
                            border="0" style="cursor: pointer;" onclick="selectFile('CategoryList',new Array(document.form1.ParentGuid,document.form1.ParentName),250,500);document.form1.ParentName.focus();" /><br />
            <label class="long">
                栏目模板路径：</label><input type="text" ContentEditable="false" runat="server" id="TemplatePath" size="60" name="TemplatePath" /><img
                    src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor: pointer;"
                    onclick="selectFile('templet',document.form1.TemplatePath,250,500);document.form1.TemplatePath.focus();" />
            <br />
            <label class="long">
                栏目保存路径：</label><input type="text" runat="server" id="ReleasePath" size="60" name="ReleasePath" /><img
                    src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor: pointer;"
                    onclick="selectFile('ReleasePath',document.form1.ReleasePath,250,500);document.form1.ReleasePath.focus();" />
            <br />
            <label class="long">
                新闻模板路径：</label><input type="text" ContentEditable="false" runat="server" id="NewsTemplatePath" size="60" name="NewsTemplatePath" /><img
                    src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor: pointer;"
                    onclick="selectFile('templet',document.form1.NewsTemplatePath,250,500);document.form1.NewsTemplatePath.focus();" />
            <br />
            <label class="long">
                新闻保存路径：</label><input type="text" runat="server" id="NewsReleasePath" size="60" name="NewsReleasePath" /><img
                    src="../../sysImages/folder/s.gif" alt="" border="0" style="cursor: pointer;"
                    onclick="selectFile('ReleasePath',document.form1.NewsReleasePath,250,500);document.form1.NewsReleasePath.focus();" />
            <br />
            <label class="long">
                等级：</label><input type="text" value="0" id="Rank" onkeypress="if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57))))return false;"
                    runat="server" maxlength="20" /><br />
            <label style="height: 20px;">
            </label>
            <br />
            <asp:Button ID="Button1" runat="server" Text="" CssClass="saveBtn" OnClick="Button1_Click" /><br />
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
<%=ViewState["javescript"] %>
    function checkNews() {
        if (document.getElementById("CategoryName").value == "") {
            alert("栏目不能为空！");
            return false; 
        }
        if (document.getElementById("Rank").value == "") {
            alert("等级不能为空！");
            return false; 
        }
        if (document.getElementById("TemplatePath").value == "") {
            alert("模板路径不能为空！");
            return false;
        }
        if (document.getElementById("ReleasePath").value == "") {
            alert("栏目保存路径不能为空！");
            return false;
        }
        return true;
    }
     var windowheight = document.body.clientHeight;
    if (windowheight < 480)
        document.getElementById("divright").style.height = "480px";
    else
        document.getElementById("divright").style.height = "100%";
</script>
