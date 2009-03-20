<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysConfig.aspx.cs" Inherits="Wis.Website.Web.Backend.SysConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统配置</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
<style>
.sysconfig {}
.sysconfig div label {float:left; width:105px;}
.sysconfig div.clear {clear:both; border:0; font-size:0; padding:0;}
.sysconfig dl {float:left; margin:0; padding:0; width:510px;}
.sysconfig dl dd {float:left; width:76px; margin-right:8px; overflow:hidden; margin-bottom:8px;}
.sysconfig dl dd a img {padding:1px; border:solid 3px #c0c0c0; background:#fff; width:68px; height:68px;}
.sysconfig dl dd a:hover img {padding:1px; border:solid 3px #ca8c34; background:#fff; width:68px; height:68px;}
</style>
</head>
<body style="background:#d6e7f7">
    <form id="SysConfigForm" runat="server">
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
   <div class="userEdit sysconfig">
    <div><label>允许上传的图片：</label><asp:TextBox ID="TextBox1" runat="server" onfocus="cb(this)" onblur="cb2(this)" Width="240"></asp:TextBox></div>
    <div><label>允许上传的视频：</label><asp:TextBox ID="TextBox2" runat="server" onfocus="cb(this)" onblur="cb2(this)" Width="240"></asp:TextBox></div>
    <div class="borBot"><label>相关文章抽取：</label><asp:TextBox ID="TextBox3" runat="server" onfocus="cb(this)" onblur="cb2(this)" Width="240"></asp:TextBox></div>
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
    
  <div class="add_button"><a href="GroupAddNew2.aspx"><img src="images/nextStep.gif" /></a></div>
    
    
    </form>
</body>
</html>
