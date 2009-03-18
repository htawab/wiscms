<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogInfoUpdate.aspx.cs" Inherits="Wis.Website.Web.Backend.BlogInfoUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改博客信息</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:#d6e7f7">
    <form id="BlogInfoUpdateForm" runat="server">
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
    <div><label>博客名称：</label><input type="text" onfocus="cb(this)" onblur="cb2(this)" /><span>4-12位（英文或数字）</span></div>
    <div><label>博客描述：</label><textarea cols="28" rows="3" onfocus="cb(this)" onblur="cb2(this)"></textarea><span>4-12位（英文或数字）</span></div>
    
    
    <div class="borBot openBlog"><label>选择皮肤：</label>
    <dl>
     <dd onclick="chooseBlogSkin(this)"><a href="#"><img src="images/blog1.jpg" width="68" height="68" alt="纯净素雅" /></a><input type="radio" name="blogSkin" />纯净素雅</dd>
     <dd onclick="chooseBlogSkin(this)"><a href="#"><img src="images/blog1.jpg" width="68" height="68" alt="蓝色经典" /></a><input type="radio" name="blogSkin" />蓝色经典</dd>
     <dd onclick="chooseBlogSkin(this)"><a href="#"><img src="images/blog1.jpg" width="68" height="68" alt="蓝色经典" /></a><input type="radio" name="blogSkin" />蓝色经典</dd>
     <dd onclick="chooseBlogSkin(this)"><a href="#"><img src="images/blog1.jpg" width="68" height="68" alt="蓝色经典" /></a><input type="radio" name="blogSkin" />蓝色经典</dd>
    </dl>
    <script>
     function chooseBlogSkin(oSkin) {
     oSkin.getElementsByTagName("input")[0].checked=true;
     }
    </script>
    
    <div class="clear"></div>
    </div>
    <div class="borBot blgInfo">
     <ul>
      <li>文章数：<a>345</a></li><li>浏览量：<a>345</a></li><li>评论数：<a>345</a></li><li>投票数：<a>345</a></li>
      <li>创建日期：<a>2008-12-23</a></li><li>最后更新日期：<a>2008-12-23</a></li>
     </ul>
     <div class="clear"></div>
    </div>
    
  
   </div>
<script>
//改变背景色
function cb(iobj) {
var iDiv = iobj.parentNode;
iDiv.style.background="#f5f5ff";
}
function cb2(iobj) {
var iDiv = iobj.parentNode;
iDiv.style.background="#fafafa";
}
</script> 

   
  </div>
    
 

<div class="add_button"><a href="#"><img src="images/StepDone.gif" /></a></div>
    
    
    </form>
</body>
</html>
