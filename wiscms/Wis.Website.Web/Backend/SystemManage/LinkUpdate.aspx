<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkUpdate.aspx.cs" Inherits="Wis.Website.Web.Backend.SystemManage.LinkUpdate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>友情链接</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="right" id="divright">
        <div class="position">
            友情连接 » <span class="red">添加友情连接</span></div>
        <div class="schBox">
            <label>
                名称：</label><input  id="LinkGuid"  runat=server style="display:none;"/><input type="text" id="LinkName" runat="server" maxlength="20" /><br />
            <label>
                Logo图片：</label><input type="text" runat="server" onmouseover="javascript:ShowDivPic(this,document.form1.Logo.value.toLowerCase().replace('{@dirfile}','files').replace('{@userdirfile}','userfiles'),'.jpg',1);"
                    onmouseout="javascript:hiddDivPic();" size="50" id="Logo" name="Logo" /><img src="../../images/folder.gif"
                        alt="选择已有图片" border="0" style="cursor: pointer;" onclick="selectFile('pic',document.form1.Logo,350,500);document.form1.Logo.focus();" />
            <span onclick="selectFile('UploadImage',document.form1.Logo,165,500);document.form1.Logo.focus();"
                style="cursor: hand; color: Red;">上传新图片</span>
            <br />
            <label>
                连接地址：</label><input type="text" runat="server" id="LinkUrl" size="60" value="Http://" name="LinkUrl" />
            <br />
            <label>
                备注：</label><textarea name="Remark" runat="server" id="Remark" rows="4" cols="70"></textarea>
            <br />
            <label>
                排序：</label><input type="text" value="0" id="Sequence" onkeypress="if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57))))return false;"
                    runat="server" maxlength="20" /><br />
     <%--       <div id="temp">
                <div id="lanmu1">
                    <label>
                        栏目：</label><input id="CategoryId1" type="text" name="CategoryId" style="display: none;" /><input
                            id="CategoryName1" readonly type="text" size="30" name="CategoryName" /><img src="../../images/folder.gif"
                                alt="选择已有标签" border="0" style="cursor: pointer;" onclick="selectFile('CategoryList',new Array(document.form1.CategoryId1,document.form1.CategoryName1),250,500);document.form1.CategoryName1.focus();" />&nbsp;<font
                                    color="red"><a href="javascript:Categoryadd()" class="list_link"><strong>添加栏目</strong></a></font><input type=checkbox id="alllanmu" runat=server />所有栏目 <span style="color: Red;">  (一个友情连接可以放在多个栏目，单击添加栏目)</span>
                </div>
            </div>--%>
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
        if (document.getElementById("Sequence").value == "") {
            alert("等级不能为空！");
            return false; 
        }
        if (document.getElementById("LinkName").value == "") {
            alert("名称不能为空！");
            return false;
        }
         if (document.getElementById("Logo").value == "") {
            alert("Logo图片地址不能为空！");
            return false;
        }
        if (document.getElementById("LinkUrl").value == "") {
            alert("连接地址不能为空！");
            return false;
        }
        return true;
    }
     var windowheight = document.body.clientHeight;
    if (windowheight < 480)
        document.getElementById("divright").style.height = "480px";
    else
        document.getElementById("divright").style.height = "100%";
     
     var randNum = 2;
    function Categoryadd() {
         var tempstr = '<div id="lanmu'+randNum+'"><label>栏目：</label><input id="CategoryId'+ randNum +'"  type="text" name="CategoryId" style="display: none;" /><input id="CategoryName'+randNum+'" readonly  type="text" size="30" name="CategoryName" /><img src="../../images/folder.gif" alt="选择已有标签" border="0" style="cursor: pointer;" onclick="selectFile(\'CategoryList\',new Array(document.form1.CategoryId' + randNum + ',document.form1.CategoryName' + randNum + '),250,500);document.form1.CategoryName' + randNum + '.focus();" />&nbsp;<font color="red"><a href="javascript:URL_delete(' + randNum + ')" class="list_link"><strong>删除</strong></a></font></div>';
        window.document.getElementById("temp").innerHTML += tempstr;
        randNum = randNum + 1;
 }
   function URL_delete(randNum) {
        var divname = "lanmu" + randNum;
        window.document.getElementById(divname).innerHTML = "";
        window.document.getElementById(divname).style.display = "none";
    }
</script>

