<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertPhoto.aspx.cs" Inherits="Wis.Website.Web.Backend.images.HtmlEditor.Dialogs.InsertPhotos.InsertPhoto" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="PageHeader" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上传图片</title>
</head>
<style>
    </style>
<link href="picUpLoad.css" rel="stylesheet" media="all" type="text/css" />

<script language="javascript" type="text/javascript"> function tabChange(boxId) { var Box1 = document.getElementById("Box1"); var Box2 = document.getElementById("Box2"); var tabIn = document.getElementById("tab").getElementsByTagName("a");   if (boxId==1) { Box1.style.display="block"; Box2.style.display="none"; tabIn[0].className ="tabNoBorder"; tabIn[1].className =""; } if (boxId==2) { Box1.style.display="none"; Box2.style.display="block"; tabIn[0].className =""; tabIn[1].className ="tabNoBorder"; }  }  function picShow(obj) { var picSrc = obj.src; var showBox = document.getElementById("showBox"); if (picSrc != "") { showBox.src=obj.src;  if (showBox.width>160) { showBox.style.width=160+"px"; } else { showBox.style.width=""; } } }  </script>

<body><form id="MainForm" runat="server">
    <div class="widowContent">
        <ul class="tab" id="tab">
            <li><a href="javascript:tabChange(1)" class="tabNoBorder" onfocus="this.blur();">从图片列表中选择</a></li>
            <li><a href="javascript:tabChange(2)" class="" onfocus="this.blur();">从电脑上传</a></li>
        </ul>
        <div id="Box1">
            <div id="picChoose">
                <div id="SelectBox">
                    <select>
                        <option>图片目录</option>
                        <option>图片目录</option>
                        <option>图片目录</option>
                    </select>
                </div>
                <div id="allPics">
                    <a href="####" title="#">
                        <img src="aa.png" onclick="picShow(this)" /></a><a href="####" title="#"><img src="aa.png"
                            onclick="picShow(this)" /></a><a href="####" title="#"><img src="aa.png" onclick="picShow(this)" /></a><a
                                href="####" title="#"><img src="aa.png" onclick="picShow(this)" /></a><a href="####"
                                    title="#"><img src="aa.png" onclick="picShow(this)" /></a><a href="####" title="#"><img
                                        src="aa.png" onclick="picShow(this)" /></a><a href="####" title="#"><img src="aa.png"
                                            onclick="picShow(this)" /></a>
                </div>
            </div>
            <div id="picShow">
                <div id="imgBox">
                    <img src="noimg.gif" id="showBox" /></div>
                <div class="imgInputA">
                    说明文字：<input type="text" />
                </div>
                <div class="imgInputB">
                    边框粗细：<input type="text" />
                    边框颜色：<input type="text" />
                </div>
                <div class="imgInputB">
                    特殊效果：<select><option>特殊效果</option><option>特殊效果</option><option>特殊效果</option></select>
                
                    对齐方式：<select><option>对齐方式</option><option>对齐方式</option><option>对齐方式</option></select>
                </div>
                <div class="imgInputB">
                    图片宽度：<input type="text" />
                
                    图片高度：<input type="text" />
                </div>
                <div class="imgInputB">
                    上下间距：<input type="text" />
                
                    左右间距：<input type="text" />
                </div>

            
            </div>
            <div class="clear">
            </div>
        </div>
        <div id="Box2" style="display:none">
            <div class="picupLoad">
                <FileUploads:DJUploadController ID="DJUploadController1" runat="server" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/" />
                <FileUploads:DJAccessibleProgressBar ID="DJAccessibleProgrssBar1" runat="server" />
                <FileUploads:DJFileUpload ID="DJFileUpload1" runat="server" ShowAddButton="true" ShowUploadButton="true" AllowedFileExtensions=".pdf,.xls,.doc,.zip,.rar,.iso,.png,.jpg,.gif,.ppt" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/" />
                <br />
                <asp:Literal runat="server" ID="ltResults"></asp:Literal>
            </div>
        </div>
    </div></form>
</body>
</html>
