<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="Wis.Website.Web.Backend.CategoryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:#d6e7f7">
<form id="form1" runat="server">
<div>
 <div id="Position">当前位置：添加新闻</div>
 
 <div class="CateGoryList">
  <ul class="top">
   <li class="li1">编号</li><li class="li2">类别名称</li><li class="li3">所属目录</li><li class="li4">操作</li>
  </ul>
  <ul>
   <li class="li1">编号</li><li class="li2">类别名称</li><li class="li3">所属目录</li><li class="li4"><a href="#">修改</a><a href="#">删除</a></li>
  </ul>
  <div class="clear"></div>
 </div>
    
    
</div>
</form>
</body>
</html>
