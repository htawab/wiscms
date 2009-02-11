<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertImage.aspx.cs" Inherits="Wis.Website.Web.Admin.InsertImage" %>

<%@ Register Src="../Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <base target="_self" />
    <title>Insert Image</title>
    <style type="text/css">
        body {
	        font-size: 12px;
	        background-color: Window;
	        margin-left: 0px;
	        margin-top: 0px;
	        margin-right: 0px;
	        margin-bottom: 0px;
        }
        TD {
            FONT-SIZE: 12px; 
            COLOR: #333;
        }
        
        .text {
	        font-family: "宋体";
	        font-size: 12px;
	        color: #000000;
        }
        .title {
	        font-family: Arial, Helvetica, sans-serif;
	        font-size: 11px;
	        color: #FFFFFF;
	        font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">
        
        var img;
        var designer    = dialogArguments; // 设计器
        var range       = designer.document.selection.createRange();
        var rangeType   = designer.document.selection.type;
        
        /// <summary>
        /// 去空格，left,right,all可选
        /// </summary>
        function BaseTrim(str){
            var temp;
	        var leftIndex = 0;
	        var rightIndex = str.length;
	        if (BaseTrim.arguments.length==2)
	            act = BaseTrim.arguments[1].toLowerCase()
	        else
	            act = "all"
              
            for(var index=0; index<str.length; index++){
	  	        temp = str.substring(leftIndex, leftIndex+1)
		        temp = str.substring(rightIndex, rightIndex-1)
                if ((act=="all" || act=="left") && temp == " "){
			        leftIndex++
                }
                if ((act=="all" || act=="right") && temp == " "){
			        rightIndex--
                }
              }
	          return str.slice(leftIndex, rightIndex)
        }

        /// <summary>
        /// 转为数字型，并无前导0，不能转则返回""
        /// </summary>
        function ConvertToInt(str){
	        str = BaseTrim(str);
	        if (str!=""){
		        var temp = parseFloat(str);
		        if (isNaN(temp)){
			        str = "";
		        }else{
			        str = temp;
		        }
	        }
	        return str;
        }
        
        
        /// <summary>
        /// 根据指定的id获得当前控件对象。
        /// </summary>
        function getElementById(id) {
	        if (document.getElementById){
		        return document.getElementById(id);
	        }else if (document.all) {
		        return document.all[id];
	        }else if (document.layers) {
		        return document.layers[id];
	        }else {
	            return null;
	        }
        }

        /// <summary>
        /// 设置待插入图片的属性。
        /// </summary>
        function setProperties(src, alt) // , border, borderColor, vspace, hspace, filter, align, width, height
        {
   	        if(getElementById('src')) getElementById('src').value         = src;
	        if(getElementById('alt')) getElementById('alt').value         = alt;
	        if(getElementById('border')) getElementById('border').value   = 0;
	        if(getElementById('align')) getElementById('align').value   = 'absmiddle';
        }

        /// <summary>
        /// 插入图片。
        /// </summary>
        function InsertImage(){
	        // 数字型输入的有效性
	        if(getElementById('border')) getElementById('border').value = ConvertToInt(getElementById('border').value);
	        if(getElementById('width')) getElementById('width').value = ConvertToInt(getElementById('width').value);
	        if(getElementById('height')) getElementById('height').value = ConvertToInt(getElementById('height').value);
        	
	        // insert image
	        var html = '<img src="' + getElementById('src').value + '"';
	        if (getElementById('border') && getElementById('border').value != ''){
		        html = html + ' border="' + getElementById('border').value + '"';
	        }
	        if (getElementById('alt') && getElementById('alt').value != ''){
		        html = html + ' alt="' + getElementById('alt').value + '"';
	        }
	        if (getElementById('align') && getElementById('align').value != ''){
		        html = html + ' align="' + getElementById('align').value + '"';
	        }
	        if (getElementById('width') && getElementById('width').value != ''){
		        html = html + ' width="' + getElementById('width').value + '"';
	        }
	        if (getElementById('height') && getElementById('height').value != ''){
		        html = html + ' height="' + getElementById('height').value + '"';
	        }
	        html = html + '>';
    	    
    	    // paste html
    	    designer.focus();
	        var sel = designer.document.selection.createRange();
	        sel.pasteHTML(html);
		        
		    window.returnValue = null;
	        window.close();	        	        
        }

        // 修改图片属性
        if (rangeType == "Control") {
	        if (range.item(0).tagName == "IMG"){
		        img = range.item(0);
		        getElementById('src').value         = img.getAttribute("src", 2);
		        getElementById('alt').value         = img.alt;
		        getElementById('border').value      = img.border;
		        for (var index=0; index < getElementById('align').length; index++){
		            if (getElementById('align').options[index].value == img.align){
			            getElementById('align').selectedIndex = index;
			            break;
		            }
	            }
		        getElementById('width').value       = img.width;
		        getElementById('height').value      = img.height;
	        }
        }

        /// <summary>
        /// 只允许输入数字。
        /// </summary>
        function IsInteger(){
          return ((event.keyCode >= 48) && (event.keyCode <= 57));
        }

		</script>      
</head>

<body bgColor="menu">
<table width="99%" border="0" cellpadding="0" cellspacing="0" align="center">
	<tr>
    	<td>
<form id="form1" runat="server" enctype="multipart/form-data">
<fieldset>
	<legend>Upload</legend>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
    	<td>
    		<table width="100%" border="0" cellspacing="0" cellpadding="0">
            	<tr>
                	<td width="20" height="25" align="right"></td>
                    <td align="center">
                    	<table border="0" cellspacing="0" cellpadding="0">
                        	<tr>
                            	<td><input class="text" name="BrowseFile" type="file" size="78" id="BrowseFile" runat="server"/></td>
								<td width="5"></td>
								<td>
                                    <asp:Button ID="btnUpload" runat="server" CssClass="text" OnClick="BtnUpload_Click"
                                        Text="Upload" /></td>
                            </tr>
                         </table>
                     </td>
                </tr>
            </table>
        </td>
    </tr>
</table>	
</fieldset>
<table border="0"  cellspacing="0"  cellpadding="0"  width="100%">
	<tr><td height="5"></td></tr>
</table>
<fieldset>
	<legend>Image List</legend>
	<table border="0" cellspacing="0" cellpadding="0" width="100%">
	<tr><td height="5"></td></tr>
</table>
<table height="30" width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#ECE9D8" style="border-top:1px solid #b7b7af; border-bottom:1px solid #b7b7af; border-left:1px solid #b7b7af; border-right:1px solid #b7b7af">
  <tr>
    <td align="center">
        <table border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td align="right">Create By:</td>
              <td><input name="createdby" type="text" class="text" id="createdby" size="15" maxlength="8" value='<%=Request["createdby"] %>' /></td>
              <td align="right">Upload Time:</td>
              <td><input name="startdate" type="text" class="text" id="startdate" size="15" maxlength="10" value='<%=Request["startdate"] %>' /></td>
              <td>TO</td>
              <td><input name="enddate" type="text" class="text" id="enddate" size="15" maxlength="10" value='<%=Request["enddate"] %>' /></td>
              <td>Keyword</td>
              <td><input name="keywords" type="text" class="text" id="keywords" size="20" maxlength="50" value='<%=Request["keywords"] %>' /></td>
              <td><input name="Search" type="submit" class="text" value="Search" id="Search" onserverclick="Search_ServerClick" runat="server" /></td>
            </tr>
        </table>
    </td>
  </tr>
</table>
<table border="0" cellspacing="0" cellpadding="0" width="100%">
	<tr><td height="5"></td></tr>
</table>
<table width="99%" border="0" cellpadding="0" cellspacing="0" align="center">
	<tr>
    	<td>
            <div style="width:100%;height:300px;overflow:scroll;overflow-x : hidden;position:relative;background-color: #ECE9D8">
                <table border="0"  cellspacing="0"  cellpadding="0"  width="100%">
	                <tr>
	                    <td>
                            <asp:Repeater ID="ImageList" runat="server">
                                <ItemTemplate>
                                    <img style="cursor:hand" alt="<%# DataBinder.Eval(Container,"DataItem.Description") %>" onclick="setProperties('<%# Msra.BackendLogic.FilesHandle.GetNonDownloadFileFullPath((Guid)DataBinder.Eval(Container,"DataItem.FileGuid")) %>','<%# DataBinder.Eval(Container,"DataItem.Description") %>')" src='<%# Msra.BackendLogic.FilesHandle.GetNonDownloadFileFullPath((Guid)DataBinder.Eval(Container,"DataItem.FileGuid")) %>' width="90px" height="60px"/>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>  
    	</td>
    </tr>
</table>
    <uc1:Pager ID="Pager1" runat="server" PageSize="45" />
</fieldset>
<table border="0" cellspacing="0" cellpadding="0" width="100%">
	<tr><td height="5"></td></tr>
</table>               
<fieldset>
	<legend>Attributes</legend>
<table border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td colspan="15" height="5"></td>
              </tr>
    <tr>
        <td width="7">
        </td>
        <td style="width: 58px">src:</td>
        <td width="5">
        </td>
        <td align="left" colspan="12"><input name="src" type="text" class="text " id="src" size="78" maxlength="50" /></td>
    </tr>
    <tr>
        <td width="7">
        </td>
        <td style="width: 58px">
        </td>
        <td width="5">
        </td>
        <td align="left" colspan="12">
        </td>
    </tr>
              <tr>
                <td width="7"></td>
                <td style="width: 58px">alt:</td>
                <td width="5"></td>
                <td colspan="12" align="left"><label for="alt"><input name="alt" id="alt" type="text" class="text" size="78" maxlength="50" /></label></td>
              </tr>
              <tr>
                <td width="7"></td>
                <td nowrap="nowrap" style="width: 58px">border:</td>
                <td width="5"></td>
                <td><input name="border" type="text" class="text" id="border" size="10" maxlength="1" /></td>
                <td width="20"></td>
                <td nowrap="nowrap" align="right">align:</td>
                <td width="5"></td>
                <td><select name="align" size="1" class="text" id="align" style="WIDTH:72px">
                    <option value="" selected="selected">默认</option>
                    <option value='left'>居左</option>
                    <option value='right'>居右</option>
                    <option value='top'>顶部</option>
                    <option value='middle'>中部</option>
                    <option value='bottom'>底部</option>
                    <option value='absmiddle'>绝对居中</option>
                    <option value='absbottom'>绝对底部</option>
                    <option value='baseline'>基线</option>
                    <option value='texttop'>文本顶部</option>
                </select></td>
                <td></td>
                <td>width:</td>
                <td width="5">&nbsp;</td>
                <td width="5"><input name="width" type="text" class="text" id="width" onkeypress="event.returnValue=IsInteger();" size="10" maxlength="4" /></td>
                <td>&nbsp;</td>
                <td>height:</td>
                <td><input name="height" type="text" class="text" id="height" onkeypress="event.returnValue=IsInteger();" size="10" maxlength="4" /></td>
              </tr>
              </table>
</fieldset>
<table border="0"  cellspacing="0"  cellpadding="0"  width="100%">
	<tr><td height="5"></td></tr>
</table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="background-color:Window">
        <tr>
          <td align="center" valign="top" bgcolor="#ECE9D8" style="border-top:1px solid #b7b7af; border-bottom:1px solid #b7b7af; border-left:1px solid #b7b7af; border-right:1px solid #b7b7af">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td align="center"><input name="insert" type="button" class="text" id="insert" onclick="InsertImage()" value=' Insert  ' />
                  &nbsp;
                  <input name="button32" type="button" class="text " onclick="window.close();" value=' Cancel ' /></td>
              </tr>
              <tr>
                <td align="center">&nbsp;</td>
              </tr>
            </table></td>
        </tr>
      </table>
</form></td>
    </tr>
</table>
</body>
</html>