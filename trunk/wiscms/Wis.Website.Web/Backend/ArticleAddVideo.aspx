<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleAddVideo.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleAddVideo" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>录入视频信息 - 内容管理 - 常智内容管理系统</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <script src="Article/wis.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function Check() {
            $("Loading").style.display = "";
            return true;
        }
    </script>
</head>
<body style="background: #d6e7f7">
    <form id="form1" runat="server">
    <div>
        <div class="position">当前位置：<asp:HyperLink ID="HyperLinkCategory" runat="server"></asp:HyperLink> » <a href="ArticleUpdate.aspx?ArticleGuid=<%=Request["ArticleGuid"] %>">录入内容</a> » 录入视频信息</div>
        <div class="add_step">
            <ul>
                <li>第一步：选择分类</li>
                <li>第二步：录入内容</li>
                <li class="current_step">第三步：录入视频信息</li>
                <li>第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main">
            <div>
                <label>视频文件：</label>
                <FileUploads:DJFileUpload ID="VideoFile" runat="server" AllowedFileExtensions=".asx,.flv,.wmv9,.rm,.rmvb,.asf,.mpg,.wmv,.3gp,.mp4,.mov,.avi" />
                <FileUploads:DJUploadController ID="DJUploadController1" runat="server" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/"  />
                <FileUploads:DJAccessibleProgressBar ID="DJAccessibleProgrssBar1" runat="server" />
            </div>
            <div>
                <label>星级：</label>
                <select id="Star" name="Star">
                    <option></option>
                    <option value="5">★★★★★</option>
                    <option value="4">★★★★</option>
                    <option value="3">★★★</option>
                    <option value="2">★★</option>
                    <option value="1">★</option>
                </select>
            </div>
        </div>
        <div id="Warning" runat="server"></div>
        <div id="Loading" style="display: none;"><img src='images/loading.gif' align='absmiddle' alt="上传中" /> 上传中...</div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" OnClientClick="javascript:return Check();" />
        </div>
    </div>
    </form>
</body>
</html>
