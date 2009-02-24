<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThumbnailUpload.aspx.cs" Inherits="Wis.Website.Web.Backend.Article.ThumbnailUpload" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传文件</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
<style>
body {background:#d6e7f7;}
</style>

</head>

<body>
    <form id="form1" runat="server">
		<FileUploads:DJUploadController ID="DJUploadController1" runat="server" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/" /><asp:FileUpload ID="ThumbnailImage" runat="server"  /> <asp:ImageButton ID="ImageButtonSubmit" runat="server" ImageUrl="../images/createthumb.png" />
    </form>
</body>
</html>
