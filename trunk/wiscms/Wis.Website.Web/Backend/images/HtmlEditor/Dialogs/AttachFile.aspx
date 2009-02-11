<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachFile.aspx.cs" Inherits="Wis.Website.Web.Admin.AttachFile" %>

<%@ Register Src="../Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<HTML>
<HEAD runat="server">
    <TITLE>附件</TITLE>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <base target="_self" />
    <style type="text/css">
    <!--
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
        
        /* 块按钮 */
        .StandardButton {
	        BORDER-LEFT: #7f9db9 1px solid;
	        BORDER-TOP: #7f9db9 1px solid;
	        BORDER-RIGHT: #7f9db9 1px solid;
	        BORDER-BOTTOM: #7f9db9 1px solid;
	        PADDING-LEFT: 5px;
	        PADDING-TOP: 2px;
	        PADDING-RIGHT: 5px;
	        PADDING-BOTTOM: 1px;
	        TEXT-TRANSFORM: uppercase;
	        background-color:#ffffff;
	        cursor:hand;
	        float:left;
        }
    -->
    </style>
</HEAD>
<BODY bgColor="menu">
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
	<legend>File List</legend>
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
            <div style="width:100%;height:300px;overflow:auto;overflow-x : hidden;position:relative;background-color: #ECE9D8">
                        <asp:Repeater ID="ImageList" runat="server">
                            <ItemTemplate>
                                <table width="99%" border="0" cellpadding="0" cellspacing="0" align="center">
                                    <tr>
                                        <td height="30"><a title="<%# DataBinder.Eval(Container,"DataItem.Description") %>" onclick="setProperties('<%# Msra.BackendLogic.FilesHandle.GetNonDownloadFileFullPath((Guid)DataBinder.Eval(Container,"DataItem.FileGuid")) %>','<%# DataBinder.Eval(Container,"DataItem.SaveAsFileName") %>')" href='#'><%# DataBinder.Eval(Container,"DataItem.OriginalFileName") %></a></td>
                                        <td align="center" width="50" onclick="setProperties('<%# Msra.BackendLogic.FilesHandle.GetNonDownloadFileFullPath((Guid)DataBinder.Eval(Container,"DataItem.FileGuid")) %>','<%# DataBinder.Eval(Container,"DataItem.SaveAsFileName") %>')"><div class="StandardButton"><a href="#">select</a></div></td>
                                        <td width="10"></td>
                                        <td align="center" width="50"><div class="StandardButton"><a href="<%# Msra.BackendLogic.FilesHandle.GetNonDownloadFileFullPath((Guid)DataBinder.Eval(Container,"DataItem.FileGuid")) %>" target="_blank">open</a></div></td>
                                    </tr>
                                    <tr><td style="background-color:efefef" colspan="3"></td></tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                
            </div>
            
    	</td>
    </tr>
</table>
    <uc1:Pager ID="Pager1" runat="server" PageSize="50"  />
</fieldset>
<table border="0" cellspacing="0" cellpadding="0" width="100%">
	<tr><td height="5"></td></tr>
</table>               
<fieldset>
	<legend>Attributes</legend>
<table border="0" cellpadding="0" cellspacing="0" align="center">
              <tr>
                <td colspan="5" height="5"></td>
              </tr>
    <tr>
        <td width="7">
        </td>
        <td style="width: 58px">
            URL:</td>
        <td width="5">
        </td>
        <td align="left" colspan="2"><input name="url" type="text" class="text " id="url" size="78" maxlength="50" /></td>
    </tr>
              <tr>
                <td width="7"></td>
                <td style="width: 58px">DisplayText:</td>
                <td width="5"></td>
                <td colspan="2" align="left"><label for="alt"><input name="displaytext" id="displaytext" type="text" class="text" size="78" maxlength="50" /></label></td>
              </tr>
              <tr>
                <td width="7"></td>
                <td nowrap="nowrap" style="width: 58px">target:</td>
                <td width="5"></td>
                  <td>
                      <select name="target" size="1" class="text" id="target" style="WIDTH:72px">
                    <option value="_blank" selected="selected">默认</option>
                    <option value='_blank'>_blank</option>
                    <option value='_parent'>_parent</option>
                    <option value='_search'>_search</option>
                    <option value='_self'>_self</option>
                    <option value='_top'>_top</option>
                </select>
                  </td>
                  <td align="right"><input name="insertas" id="insertas" type="checkbox" /><label for="insertas">Insert As Object</label></td>
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
                <td align="center"><input name="insert" type="button" class="text" id="insert" onclick="AttachFile()" value=' Insert  ' />
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
    <script language="JavaScript" type="text/javascript">
        var designer    = dialogArguments; // 设计器
        var range       = designer.document.selection.createRange();
        var rangeType   = designer.document.selection.type;

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
        /// 设置待附加文件的属性。
        /// </summary>
        function setProperties(url, displaytext)
        {
   	        if(getElementById('url')) getElementById('url').value                   = url;
	        if(getElementById('displaytext')) getElementById('displaytext').value   = displaytext;
        }
        
        /// <summary>
        /// 附加文件。
        /// </summary>
        function AttachFile(){
            if(!getElementById('displaytext') || !getElementById('url') || !getElementById('target')) return;
            
	        var displaytext = getElementById('displaytext').value; // trim
	        if(displaytext == '') displaytext = getElementById('url').value;
	        
	        var target = getElementById('target').options[getElementById('target').selectedIndex].value;
	        var href = getElementById('url').value;
	        
	        var extension = href.substr(href.lastIndexOf(".") + 1);
	        extension = extension.toUpperCase();
	
	        // 获得扩展名
	        
	        var html;
	        
	        // default html.
	        html = '<a href="' + href + '" target="' + target + '">' + displaytext + '</a>';
	        
	        // insert as object
	        if(getElementById('insertas').checked)
	        {
	            switch(extension){
	                case "SWF": // swf file.
	                    html = "<OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000";
                    	
                	    var width = 100;
                	    var height = 100;
	                    if (width != "") html+=" width=" + width;
	                    if (height != "") html+=" height=" + height;
	                    html+="><PARAM NAME=movie VALUE='" + href + "'><PARAM NAME=quality VALUE=high><embed src='" + href + "' quality=high pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash'";
	                    if (width != "") html+=" width=" + width;
	                    if (height != "") html+=" height=" + height;
	                    html += ">" + href + "</embed></OBJECT>";
		                break;
		            case "GIF": // image
		            case "JPG":
		            case "BMP":
		            case "PNG":
	                    html = '<img src="' + href + '" border="0" alt="' + displaytext + '" align="absmiddle" />';
		                break;
		            case "AVI":
		            case "MPG":
		            case "MPEG":
		            case "ASF":
		                html = "<EMBED src='" + href + "' width=200 height=200 type=audio/x-pn-realaudio-plugin console='Clip1' controls='IMAGEWINDOW,ControlPanel,StatusBar' autostart='true'></EMBED>"
		                break;
		            case "MP3":
		            case "MID":
		            case "MIDI":
		            case "WAV":
		                html = "<EMBED SRC='" + href + "'>";
		                break;
		        }
	        }		       
		    // RA   RM
		    // TXT  CHM HLP DOC PDF MDB ZIP RAR EXE XLS PPT PPS
		    // ASP  JSP JS  PHP PHP3    ASPX
		    // HTM  HTML    SHTML     

    	    // paste html
    	    designer.focus();
	        var sel = designer.document.selection.createRange();
	        sel.pasteHTML(html);
		        
		    window.returnValue = null;
	        window.close();	        	        
        }

        if (rangeType == "Text") {
//		        getElementById('href').value         = link.getAttribute("href", 2);
//		        for (var index=0; index < getElementById('target').length; index++){
//		            if (getElementById('target').options[index].value == link.target){
//			            getElementById('target').selectedIndex = index;
//			            break;
//		            }
//	            }
	        getElementById('displaytext').value  = range.text;
        }
</script>
</body>
</html>