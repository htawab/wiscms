<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryUpdate.aspx.cs"
    Inherits="Wis.Website.Web.Backend.CategoryUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改分类</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function CheckCategory() {
            if ($("TextBoxCategoryName").value == "") {
                alert("分类名称不能为空！");
                return false;
            }
            $("Loading").style.display = "";
            return true;
        }
    </script>
</head>
<body style="background: #d6e7f7">
    <form runat="server" id="BlogCategoryAddNewForm">
    <div id="Position">
        所在位置：
        <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
            <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
            <CurrentNodeStyle ForeColor="#333333" />
            <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
            <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
        </asp:SiteMapPath>
    </div>
    <div class="listBox">
        <div class="userEdit">
            <div>
                <label>
                    分类名称：</label><asp:TextBox ID="TextBoxCategoryName" runat="server"></asp:TextBox><span>分类名称</span></div>
            <div class="borBot">
                <label>
                    分类序号：</label><asp:TextBox ID="TextBoxCategoryRank" runat="server"></asp:TextBox><span>分类排充号（阿拉伯数字）</span></div>
        </div>
    </div>
    <div id="Warning" runat="server"></div>
    <div id="Loading" style="display: none;"><img src='images/loading.gif' align='absmiddle' /> 上传中...</div>
    <div class="add_button">
        <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/StepDone.gif" OnClick="ImageButtonNext_Click" OnClientClick="javascript:return CheckCategory();" />
    </div>
    </form>
</body>
</html>
