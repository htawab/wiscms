<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoEdit.aspx.cs" Inherits="Wis.Website.Web.Backend.dialog.VideoEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../JavaScript/Prototype.js" language="javascript" type="text/javascript"></script>

    <script src="../../JavaScript/Public.js" language="javascript" type="text/javascript"></script>

    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <link href="../../sysImages/default/css/css.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <style type="text/css">
        .style1
        {
            width: auto;
            height: 25px;
            line-height: 24px; *;line-height:26px;margin:3px;text-align:left;text-indent:6px;background:#fffurl(../images/bar.gif)repeat-x;border:1pxsolid#bddef7;overflow:hidden;font-weight:bold;}</style>

    <script language="javascript">

        function loadvideo() {
            var gvalur = document.getElementById("PathVideo").value;
            var widthid = document.getElementById("widthid").value;
            var heightid = document.getElementById("heightid").value;
            var AutoStart = document.getElementById("AutoStart").value;
            var textalign = document.getElementById("textalign").value;
            if (widthid == "" || widthid == null)
                widthid = "300";
            if (heightid == "" || heightid == null)
                heightid = "200";
            if (AutoStart == "" || AutoStart == null)
                AutoStart = "0";
            var content = "";
            if (gvalur != null && gvalur != "") {
                if (gvalur.indexOf(".") > -1) {
                    gvalur = gvalur.toLowerCase();
                    var fileExstarpostion = gvalur.lastIndexOf(".");
                    var fileExName = gvalur.substring(fileExstarpostion, (gvalur.length))

                    switch (fileExName.toLowerCase()) {
                        case ".jpg": case ".gif": case ".bmp": case ".ico": case ".png": case ".jpeg": case ".tif": case ".rar": case ".doc": case ".zip": case ".txt":
                            alert("错误的视频文件"); return false;
                            break;
                        case ".swf":
                            content = "<p align=\"" + textalign + "\"><object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"  width=\"" + widthid + "\"  height =\"" + heightid + "\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\">";
                            content += "<param name=\"movie\" value=\"" + gvalur + "\" />"
                            content += "<param name=\"quality\" value=\"high\" />"
                            content += "<embed src=\"" + gvalur + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\"></embed>"
                            content += "</object></p>"
                            break;
                        case ".flv":
                            content = "<p align=\"" + textalign + "\"><object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"   width=\"" + widthid + "\"  height =\"" + heightid + "\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" width=\"500\" height=\"400\">" +
                            "<param name=\"movie\" value=\" \">" +
                            "<param name=\"quality\" value=\"high\">" +
                            "<param name=\"allowFullScreen\" value=\"true\" />" +
                            "<param name=\"FlashVars\" value=\"vcastr_file=" + gvalur + "\" />" +
                            "<embed src=\" \" FlashVars=\"vcastr_file=" + gvalur + "\" allowFullScreen=\"true\"  quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"500\" height=\"400\"></embed>" +
                            "</object></p>";
                            break;
                        //                        case ".avi":     
                        //                            content='<embed width="' + widthid + '"  height ="' + heightid + '" border="0" showdisplay="1" showcontrols="1" autostart="' + AutoStart + '" '+     
                        //                            ' autorewind="0" playcount="0"moviewindowheight="' + heightid + '" moviewindowwidth="' + widthid + '" filename="/' + gvalur + '" src="' + gvalur + '">' +     
                        //                            '</embed>'     
                        //                            break;     
                        case ".wmv":
                            content = "<p align=\"" + textalign + "\"><object id=\"NSPlay\"  width=\"" + widthid + "\"  height =\"" + heightid + "\"  classid=\"CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,5,715\" standby=\"Loading Microsoft Windows Media Player components...\" type=\"application/x-oleobject\" hspace=\"5\">\r\n" +
                            "<param name=\"AutoRewind\" value=1>\r\n" +
                            "<param name=\"FileName\" value=\"" + gvalur + "\">\r\n" +
                            "<param name=\"ShowControls\" value=\"1\">\r\n" +
                            "<param name=\"ShowPositionControls\" value=\"0\">\r\n" +
                            "<param name=\"ShowAudioControls\" value=\"1\">\r\n" +
                            "<param name=\"ShowTracker\" value=\"-1\">\r\n" +
                            "<param name=\"ShowDisplay\" value=\"0\">\r\n" +
                            "<param name=\"ShowStatusBar\" value=\"-1\">\r\n" +
                            "<param name=\"ShowGotoBar\" value=\"0\">\r\n" +
                            "<param name=\"ShowCaptioning\" value=\"0\">\r\n" +
                            "<param name=\"AutoStart\" value=\"" + AutoStart + "\">\r\n" +
                            "<param name=AudioStream value=-1>\r\n" +
                            "<param name=WindowlessVideo value=-1>\r\n" +
                            "<param name=EnablePositionControls value=-1>\r\n" +
                            "<param name=EnableFullScreenControls value=-1>\r\n" +
                            "<param name=EnableTracker value=-1>\r\n" +
                            "<param name=\"Volume\" value=\"-2500\">\r\n" +
                            "<param name=\"AnimationAtStart\" value=\"0\">\r\n" +
                            "<param name=\"TransparentAtStart\" value=\"0\">\r\n" +
                            "<param name=\"AllowChangeDisplaySize\" value=\"0\">\r\n" +
                            "<param name=\"AllowScan\" value=\"0\">\r\n" +
                            "<param name=\"EnableContextMenu\" value=\"-1\">\r\n" +
                            "<param name=\"ClickToPlay\" value=\"0\">\r\n" +
                            "</object></p>";
                            break;
                        case ".mpg":
                            content = "<p align=\"" + textalign + "\"><object classid=\"clsid:05589FA1-C356-11CE-BF01-00AA0055595A\"  id=\"ActiveMovie1\" width=\"" + widthid + "\"  height =\"" + heightid + "\"  >\r\n" +
                            "<param name=\"Appearance\" value=\"0\">\r\n" +
                            "<param name=\"AutoStart\" value=\"" + AutoStart + "\">\r\n" +
                            "<param name=\"AllowChangeDisplayMode\" value=\"-1\">\r\n" +
                            "<param name=\"AllowHideDisplay\" value=\"0\">\r\n" +
                            "<param name=\"AllowHideControls\" value=\"-1\">\r\n" +
                            "<param name=\"AutoRewind\" value=\"-1\">\r\n" +
                            "<param name=\"Balance\" value=\"0\">\r\n" +
                            "<param name=\"CurrentPosition\" value=\"0\">\r\n" +
                            "<param name=\"DisplayBackColor\" value=\"0\">\r\n" +
                            "<param name=\"DisplayForeColor\" value=\"16777215\">\r\n" +
                            "<param name=\"DisplayMode\" value=\"0\">\r\n" +
                            "<param name=\"Enabled\" value=\"-1\">\r\n" +
                            "<param name=\"EnableContextMenu\" value=\"-1\">\r\n" +
                            "<param name=\"EnablePositionControls\" value=\"-1\">\r\n" +
                            "<param name=\"EnableSelectionControls\" value=\"0\">\r\n" +
                            "<param name=\"EnableTracker\" value=\"-1\">\r\n" +
                            "<param name=\"Filename\" value=\"" + gvalur + "\" valuetype=\"ref\">\r\n" +
                            "<param name=\"FullScreenMode\" value=\"0\">\r\n" +
                            "<param name=\"MovieWindowSize\" value=\"0\">\r\n" +
                            "<param name=\"PlayCount\" value=\"1\">\r\n" +
                            "<param name=\"Rate\" value=\"1\">\r\n" +
                            "<param name=\"SelectionStart\" value=\"-1\">\r\n" +
                            "<param name=\"SelectionEnd\" value=\"-1\">\r\n" +
                            "<param name=\"ShowControls\" value=\"-1\">\r\n" +
                            "<param name=\"ShowDisplay\" value=\"-1\">\r\n" +
                            "<param name=\"ShowPositionControls\" value=\"0\">\r\n" +
                            "<param name=\"ShowTracker\" value=\"-1\">\r\n" +
                            "<param name=\"Volume\" value=\"-480\">\r\n" +
                            "</object></p>";
                            break;
                        case ".wma":
                            content = '<p align="' + textalign + '"><object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95"  width="' + widthid + '"  height ="' + heightid + '" id="MediaPlayer1" > \r\n' +
                            '<param name="Filename" value="' + gvalur + '">\r\n' +
                            '<param name="PlayCount" value="1"> \r\n' +
                            '<param name="AutoStart" value="' + AutoStart + '">\r\n' +
                            '<param name="ClickToPlay" value="1">\r\n' +
                            '<param name="DisplaySize" value="0">\r\n' +
                            '<param name="EnableFullScreenControls" value="1">\r\n' +
                            '<param name="ShowAudioControls" value="-1">\r\n' +
                            '<param name="EnableContextMenu" value="-1">\r\n' +
                            '<param name="ShowDisplay" value="1">\r\n' +
                            '</object></p>'
                            break;
                        case ".rmvb":
                            //                            content = '<p align="' + textalign + '"><object classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" name="player"   width="' + widthid + '"  height ="' + heightid + '" id="player">\r\n' +
                            //                           '<param name="_ExtentX" value="11298"> \r\n' +
                            //                           '<param name="_ExtentY" value="7938"> \r\n' + 
                            //                            '<param name="AUTOSTART" value="' + AutoStart + '"> \r\n' +
                            //                            '<param name="SHUFFLE" value="0"> \r\n' +
                            //                            '<param name="PREFETCH" value="0"> \r\n' +
                            //                            '<param name="NOLABELS" value="-1"> \r\n' +
                            //                            '<param name="SRC" value="' + gvalur + '"> \r\n' +
                            //                            '<param name="CONTROLS" value="ImageWindow,StatusBar,ControlPanel"> \r\n' +
                            //                            '<param name="CONSOLE" value="clip1"> \r\n' +
                            //                            '<param name="LOOP" value="true"> \r\n' +
                            //                            '<param name="NUMLOOP" value="0"> \r\n' +
                            //                            '<param name="CENTER" value="0"> \r\n' +
                            //                            '<param name="MAINTAINASPECT" value="0"> \r\n' +
                            //                            '<param name="BACKGROUNDCOLOR" value="#000000"> \r\n' +
                            //                            '</object></p> '
                            //                            
                            content = '<p align="' + textalign + '"><object id="vid" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width=500 height=300> \r\n' +
                            '<param name="_ExtentX" value="11298"> \r\n' +
                            '<param name="_ExtentY" value="7938"> \r\n' +
                            '<param name="AUTOSTART" value="-1"> \r\n' +
                            '<param name="SHUFFLE" value="0"> \r\n' +
                            '<param name="PREFETCH" value="0"> \r\n' +
                            '<param name="NOLABELS" value="-1"> \r\n' +
                            '<param name="SRC" value="' + gvalur + '";> \r\n' +
                            '<param name="CONTROLS" value="Imagewindow"> \r\n' +
                            '<param name="CONSOLE" value="clip1"> \r\n' +
                            '<param name="LOOP" value="0"> \r\n' +
                            '<param name="NUMLOOP" value="0"> \r\n' +
                            '<param name="CENTER" value="0"> \r\n' +
                            '<param name="MAINTAINASPECT" value="0"> \r\n' +
                            '<param name="BACKGROUNDCOLOR" value="#000000"> \r\n' +
                            '</object>\r\n' +
                            '<object id="vid2" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width=500 height=30> \r\n' +
                            '<param name="_ExtentX" value="11298"> \r\n' +
                            '<param name="_ExtentY" value="794"> \r\n' +
                            '<param name="AUTOSTART" value="-1"> \r\n' +
                            '<param name="SHUFFLE" value="0"> \r\n' +
                            '<param name="PREFETCH" value="0"> \r\n' +
                            '<param name="NOLABELS" value="-1"> \r\n' +
                            '<param name="SRC" value="' + gvalur + '";> \r\n' +
                            '<param name="CONTROLS" value="ControlPanel"> \r\n' +
                            '<param name="CONSOLE" value="clip1"> \r\n' +
                            '<param name="LOOP" value="0"> \r\n' +
                            '<param name="NUMLOOP" value="0"> \r\n' +
                            '<param name="CENTER" value="0"> \r\n' +
                            '<param name="MAINTAINASPECT" value="0"> \r\n' +
                            '<param name="BACKGROUNDCOLOR" value="#000000"> \r\n' +
                             '</object></p> '
                            break;
                        case ".rm":
                            content = '<p align="' + textalign + '"><object CLASSID="clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA" id="vid"  width="' + widthid + '"  height ="' + heightid + '">\r\n' +
                            '<param name="_ExtentX" value="22304">\r\n' +
                            '<param  name="_ExtentY" value="14288">\r\n' +
                            '<param name="AUTOSTART" value="' + AutoStart + '"> \r\n' +
                            '<param name="SHUFFLE" value="0"> \r\n' +
                            '<param name="PREFETCH" value="0"> \r\n' +
                            '<param name="NOLABELS" value="-1"> \r\n' +
                            '<param name="SRC" value="' + gvalur + '"> \r\n' +
                            '<param name="CONTROLS" value="ImageWindow,StatusBar,ControlPanel"> \r\n' +
                            '<param name="CONSOLE" value="clip1"> \r\n' +
                            '<param name="LOOP" value="true"> \r\n' +
                            '<param name="NUMLOOP" value="0"> \r\n' +
                            '<param name="CENTER" value="0"> \r\n' +
                            '<param name="MAINTAINASPECT" value="0"> \r\n' +
                            '<param name="BACKGROUNDCOLOR" value="#000000"> \r\n' +
                            '</object></p>'
                            break;
                        default:
                            content = "<p align=\"" + textalign + "\"><OBJECT ID=video1 CLASSID=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\"  width=\"" + widthid + "\"  height =\"" + heightid + "\">\r\n" +
                            "<param name=\"_ExtentX\" value=\"9313\">\r\n" +
                            "<param name=\"_ExtentY\" value=\"7620\">\r\n" +
                            "<param name=\"AUTOSTART\" value=\"" + AutoStart + "\">\r\n" +
                            "<param name=\"SHUFFLE\" value=\"-1\">\r\n" +
                            "<param name=\"PREFETCH\" value=\"-1\">\r\n" +
                            "<param name=\"NOLABELS\" value=\"-1\">\r\n" +
                            "<param name=\"SRC\" value=\"" + gvalur + "\">\r\n" +
                            "<param name=\"CONTROLS\" value=\"ImageWindow,StatusBar,ControlPanel\">\r\n" +
                            "<param name=\"CONSOLE\" value=\"Clip1\">\r\n" +
                            "<param name=\"LOOP\" value=\"-1\">\r\n" +
                            "<param name=\"NUMLOOP\" value=\"-1\">\r\n" +
                            "<param name=\"CENTER\" value=\"-1\">\r\n" +
                            "<param name=\"MAINTAINASPECT\" value=\"0\">\r\n" +
                            "<param name=\"BACKGROUNDCOLOR\" value=\"#000000\">\r\n" +
                            "</OBJECT></p>";
                            break;
                    }
                }
            }
            if (parseInt(heightid) > 300)
                document.getElementById("view").style.height = heightid
            else
                document.getElementById("view").style.height = "313"
            return content;
        }
        function ReturnValue() {
            var content = loadvideo();
            if (content != "") {
                var view = document.getElementById("view");
                view.innerHTML = "";
                parent.ReturnFunVdieo(content);
            }
            else
                alert('没有视频文件!或者不支持此视频文件格式');

        }
        function Cancle() {
            var view = document.getElementById("view");
            view.innerHTML = ""; ;
            parent.ReturnFunVdieo("");
        }
        function videoview() {
            var content = loadvideo();
            if (content != "") {
                var view = document.getElementById("view");
                view.innerHTML = "";
                view.innerHTML = content;
            }
        }
   
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
        <div class="style1">
            插入编辑视频</div>
        <div>
            <label>
                &nbsp;&nbsp;视频地址：</label><input type="text" runat="server" onblur="videoview();"
                    size="50" id="PathVideo" name="PathVideo" /><img src="../../images/folder.gif" alt="选择已有视频"
                        border="0" style="cursor: pointer;" onclick="selectFile('video',document.form1.PathVideo,350,500);document.form1.PathVideo.focus();" />
            <span onclick="selectFile('UploadVideo',document.form1.PathVideo,165,500);document.form1.PathVideo.focus();"
                style="cursor: hand; color: Red;">上传新视频</span>
            <br />
            <label class="video">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;宽度：</label><input type="text" value="300" runat="server"
                    size="5" id="widthid" name="TabloidPathVideo" />
            <label class="video">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;高度：</label><input type="text" value="200" runat="server"
                    size="5" id="heightid" name="TabloidPathVideo" />
            <label class="video">
                自动播放：</label><select id="AutoStart" runat="server"><option value="-1" selected>是</option>
                    <option value="0">否</option>
                </select>
            <label class="video">
                对齐方式：</label><select id="textalign" runat="server"><option value="center" selected>居中</option>
                    <option value="left">左对齐</option>
                    <option value="right">右对齐</option>
                </select>
            <input type="button" onclick="ReturnValue()" value="确定" />
            <input type="button" onclick="videoview()" value="预览" />
            <input type="button" onclick="Cancle()" value="取消" />
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td valign="top" align="center">
                        <div id="view" runat="server" class="viewvdieo" style="height: 313px; text-align: center;
                            vertical-align: middle;">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
