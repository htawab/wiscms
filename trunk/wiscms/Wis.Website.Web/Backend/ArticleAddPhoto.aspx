<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleAddPhoto.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleAddPhoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>tep1</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: #d6e7f7"><form id="form1" runat="server">
    <div>
        <div class="position">
            当前位置：添加新闻</div>
        <div class="add_step">
            <ul>
                <li>第一步：选择分类</li><li>第二步：基本信息</li>
                <li class="current_step">第三步：扩展信息</li><li>第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main add_step3">
            <div>
                <label>
                    缩略图预览：<br />
                    (188*64) &nbsp;&nbsp;</label><img src="../images/noimg2.gif" /></div>
            <div>
                <label>
                    制作缩略图：</label><span class="checked" id="span1" onclick="checkRadio(this,1)"><input
                        checked="checked" name="aaa" type="radio" value="1" />图片自动缩放</span> <span class="unCheck"
                            id="span2" onclick="checkRadio(this,2)">
                            <input name="aaa" type="radio" value="1" />在大图中手工截取</span></div>
            <div>
                <label>
                </label>
                <input type="image" src="../images/uploadimg2.gif" /></div>

            <script>
var span1 = document.getElementById("span1");
var span2 = document.getElementById("span2");
var radio1 = span1.getElementsByTagName("input")[0];
var radio2 = span2.getElementsByTagName("input")[0];
function checkRadio(iobj,icheck) {
if (icheck==1) {
span1.className = "checked"
span2.className = "unCheck"
radio1.checked = true;
radio2.checked = false;
}
else {
span1.className = "unCheck"
span2.className = "checked"
radio1.checked = false;
radio2.checked = true;
}
}
            </script>

        </div>
        <div id="Warning" runat="server"></div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" />
        </div>
    </div></form>
</body>
</html>
