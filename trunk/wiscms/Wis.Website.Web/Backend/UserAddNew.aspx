<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAddNew.aspx.cs" Inherits="Wis.Website.Web.Backend.UserAddNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加新用户</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
</head>
<body  style="background:#d6e7f7">
    <form runat="server" id="UserAddNewForm">
    
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
    <div><label>用户帐号：</label><asp:TextBox ID="TextBoxName" runat="server" Width="158"></asp:TextBox><span>4-12位（英文或数字）</span><br /><label></label><input type="button" value="检测帐号" /></div>
    <div><label>登录密码：</label><asp:TextBox ID="TextBoxPassword" runat="server" Width="158"></asp:TextBox><span>4-12位（英文或数字）</span></div>
    <div><label>确认登录密码：</label><asp:TextBox ID="TextBoxPassword2" runat="server" Width="158"></asp:TextBox><span>4-12位（英文或数字）</span></div>
    <div class="borBot"><span class="moreInfo" onclick="showMore(this)" title="显示/隐藏更多信息">更多信息</span></div>
   </div>
   
   <div class="userEdit" id="userEdit2" style="display:none;">
    <div><label>呢称：</label><asp:TextBox ID="TextBoxNickname" runat="server" Width="158"></asp:TextBox></div>
    <div><label>姓名：</label><asp:TextBox ID="TextBoxCompellation" runat="server" Width="158"></asp:TextBox><label style="width:160px;">性别：</label><select><option>男</option><option>女</option></select></div>
    <div><label>出生日期：</label><select onfocus="cb(this)" onblur="cb2(this)"><option>1980年</option></select>  <select onfocus="cb(this)" onblur="cb2(this)"><option>5月</option></select> <select onfocus="cb(this)" onblur="cb2(this)"><option>30日</option></select><label style="width:152px">民族：</label><asp:TextBox ID="TextBoxNation" runat="server" Width="158"></asp:TextBox></div>
    <div><label>毕业学校：</label><asp:TextBox ID="TextBoxGraduateSchool" runat="server" Width="158"></asp:TextBox><label style="width:160px;">专业：</label><asp:TextBox ID="TextBoxMajor" runat="server" Width="158"></asp:TextBox></div>
    
    
    <div><label>手机：</label><asp:TextBox ID="TextBoxMobilePhone" runat="server" Width="158"></asp:TextBox></div>
    <div><label>传呼机：</label><asp:TextBox ID="TextBoxBeepPager" runat="server" Width="158"></asp:TextBox><label style="width:160px;">小灵通：</label><asp:TextBox ID="TextBoxPhs" runat="server" Width="158"></asp:TextBox></div>
    <div><label>QQ：</label><asp:TextBox ID="TextBoxQQ" runat="server" Width="158"></asp:TextBox><label style="width:160px;">ICQ：</label><asp:TextBox ID="TextBoxICQ" runat="server" Width="158"></asp:TextBox></div>
    <div><label>MSN：</label><asp:TextBox ID="TextBoxMSN" runat="server" Width="158"></asp:TextBox><label style="width:160px;">YahooMessenger：</label><asp:TextBox ID="TextBoxYahooMessenger" runat="server" Width="158"></asp:TextBox></div>
    <div><label>Skype：</label><asp:TextBox ID="TextBoxSkype" runat="server" Width="158"></asp:TextBox><label style="width:160px;">Popo：</label><asp:TextBox ID="TextBoxPopo" runat="server" Width="158"></asp:TextBox></div>
    
    
    <div><label>Email：</label><asp:TextBox ID="TextBoxEmail" runat="server" Width="238"></asp:TextBox></div>
    <div><label>个人主页：</label><asp:TextBox ID="TextBoxHomepage" runat="server" Width="238"></asp:TextBox></div>
    
    <div><label>博客：</label><asp:TextBox ID="TextBoxBlog" runat="server" Width="238"></asp:TextBox></div>
    <div><label>家庭住址：</label><asp:TextBox ID="TextBoxHomeAddress" runat="server" Width="388"></asp:TextBox></div>
    <div><label>邮政编码：</label><asp:TextBox ID="TextBoxHomePostalcode" runat="server"></asp:TextBox></div>
    <div><label>住宅电话1：</label><asp:TextBox ID="TextBoxHomePhone1" runat="server" Width="158"></asp:TextBox><label style="width:160px;">住宅电话2：</label><asp:TextBox ID="TextBoxHomePhone2" runat="server" Width="158"></asp:TextBox></div>
    <div><label>单位名称：</label><asp:TextBox ID="TextBoxOffice" runat="server" Width="238"></asp:TextBox></div>
    <div><label>部门：</label><asp:TextBox ID="TextBoxDepartment" runat="server" Width="158"></asp:TextBox><label style="width:160px;">职位：</label><asp:TextBox ID="TextBoxOfficePosition" runat="server" Width="158"></asp:TextBox></div>
 
    <div><label>办公邮件：</label><asp:TextBox ID="TextBoxOfficeMail" runat="server" Width="238"></asp:TextBox></div>
    <div><label>办公电话1：</label><asp:TextBox ID="TextBoxOfficePhone1" runat="server" Width="158"></asp:TextBox><label style="width:160px;">办公电话2：</label><asp:TextBox ID="TextBoxOfficePhone2" runat="server" Width="158"></asp:TextBox></div>
    <div><label>传真：</label><asp:TextBox ID="TextBoxOfficeFax" runat="server" Width="158"></asp:TextBox></div>
    <div><label>单位网站：</label><asp:TextBox ID="TextBox1" runat="server" Width="248"></asp:TextBox></div>
    <div><label>办公地址：</label><asp:TextBox ID="TextBoxOfficeAddress" runat="server" Width="388"></asp:TextBox></div>
    <div><label>邮政编码：</label><asp:TextBox ID="TextBoxOfficePostalcode" runat="server"></asp:TextBox></div>
    <div class="borBot"><label>附注：</label><asp:TextBox ID="TextBoxAnnotations" runat="server" Width="448" Rows="6" TextMode="MultiLine"></asp:TextBox></div>


   </div>

<script>
//显示，隐藏更多信息
var moreInfo = document.getElementById("userEdit2");
function showMore(moreSpan) {
if (moreSpan.className == "moreInfo") {
moreSpan.className = "moreInfo2";
moreInfo.style.display="block";
}
else {
moreSpan.className = "moreInfo";
moreInfo.style.display="none";
}

}

</script>
   
  </div>
    
 

<div class="add_button"><a href="#"><img src="images/StepDone.gif" /></a></div>
    
    
    </form>
</body>
</html>