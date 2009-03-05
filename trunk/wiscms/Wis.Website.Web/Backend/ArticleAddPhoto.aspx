<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleAddPhoto.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleAddPhoto" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>录入图片信息 - 内容管理 - 常智内容管理系统</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #ImageCroppeImageCropperRightDown,#ImageCroppeImageCropperLeftDown,#ImageCroppeImageCropperLeftUp,#ImageCroppeImageCropperRightUp,#ImageCropperRight,#ImageCropperLeft,#ImageCropperUp,#ImageCropperDown{
	        position:absolute;
	        background:#FFF;
	        border: 1px solid #333;
	        width: 6px;
	        height: 6px;
	        z-index:500;
	        font-size:0;
	        opacity: 0.5;
	        filter:alpha(opacity=50);
        }

        #ImageCroppeImageCropperLeftDown,#ImageCroppeImageCropperRightUp{cursor:ne-resize;}
        #ImageCroppeImageCropperRightDown,#ImageCroppeImageCropperLeftUp{cursor:nw-resize;}
        #ImageCropperRight,#ImageCropperLeft{cursor:e-resize;}
        #ImageCropperUp,#ImageCropperDown{cursor:n-resize;}

        #ImageCroppeImageCropperLeftDown{left:0px;bottom:0px;}
        #ImageCroppeImageCropperRightUp{right:0px;top:0px;}
        #ImageCroppeImageCropperRightDown{right:0px;bottom:0px;background-color:#00F;}
        #ImageCroppeImageCropperLeftUp{left:0px;top:0px;}
        #ImageCropperRight{right:0px;top:50%;margin-top:-4px;}
        #ImageCropperLeft{left:0px;top:50%;margin-top:-4px;}
        #ImageCropperUp{top:0px;left:50%;margin-left:-4px;}
        #ImageCropperDown{bottom:0px;left:50%;margin-left:-4px;}

        #ImageCropperBackground{border:1px solid #666666; position:absolute;}
        #ImageCropperDrag {border:1px dashed #fff; width:<%=ThumbnailWidth %>; height:<%=ThumbnailHeight %>; top:10px; left:10px; cursor:move; }
        #ImagePreview {border:1px dashed #fff; width:<%=ThumbnailWidth %>; height:<%=ThumbnailHeight %>; overflow:hidden; position:relative;}
    </style>    
</head>
<body style="background: #d6e7f7"><form id="form1" runat="server">
    <div>
        <div class="position">当前位置：<asp:HyperLink ID="HyperLinkCategory" runat="server"></asp:HyperLink> » <a href="ArticleUpdate.aspx?ArticleGuid=<%=Request["ArticleGuid"] %>">录入内容</a> » 录入图片信息</div>
        <div class="add_step">
            <ul>
                <li>第一步：选择分类</li>
                <li>第二步：录入内容</li>
                <li class="current_step">第三步：录入更多内容</li>
                <li>第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main add_step3">
            <div>
                <label>缩略图预览：<br />(<%=ThumbnailWidth %>*<%=ThumbnailHeight %>) &nbsp;&nbsp;</label>
                <div class="Preview" id="ImagePreview"><img src="images/noimg2.gif" alt="缩略图预览" /></div>
            </div>
            <div>
                <label>制作缩略图：</label>
                <asp:HiddenField ID="PointX" runat="server" />
                <asp:HiddenField ID="PointY" runat="server" />
                <asp:HiddenField ID="CropperWidth" runat="server" />
                <asp:HiddenField ID="CropperHeight" runat="server" />
                <FileUploads:DJUploadController ID="DJUploadController1" runat="server" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/"  />
                <span id='PhotoFilename'></span><input id='Photo' type='file' name='Photo' value='' style="display: none;" onchange="SelectImage();" />
                <span class="checked" id="ThumbnailSpan1" onclick="Thumbnail_Load(event, 1);"><input name="Thumbnail" type="radio" value="Thumb" checked="checked" />按高度宽度最佳缩放</span> 
                <span class="unCheck" id="ThumbnailSpan2" onclick="Thumbnail_Load(event, 2);"><input name="Thumbnail" type="radio" value="Cropper" />在大图中手工裁剪</span></div>
                <div id="ImageCropperBackground" style="display:none">
                    <div id="ImageCropperDrag">
                      <div id="ImageCroppeImageCropperRightDown"></div>
                      <div id="ImageCroppeImageCropperLeftDown"></div>
                      <div id="ImageCroppeImageCropperRightUp"></div>
                      <div id="ImageCroppeImageCropperLeftUp"></div>
                      <div id="ImageCropperRight"></div>
                      <div id="ImageCropperLeft"></div>
                      <div id="ImageCropperUp"></div>
                      <div id="ImageCropperDown"></div>
                    </div>
                    <iframe style="position:absolute;top:0;left:0;width:100%;height:100%;filter:alpha(opacity=0);"></iframe>
                </div>
                <script type="text/javascript" language="javascript">

                    var span1 = $("ThumbnailSpan1");
                    var span2 = $("ThumbnailSpan2");
                    var radio1 = span1.getElementsByTagName("input")[0];
                    var radio2 = span2.getElementsByTagName("input")[0];
                    function Thumbnail_Load(e, index){
                        if (index == 1) {
                            span1.className = "checked"
                            span2.className = "unCheck"
                            radio1.checked = true;
                            radio2.checked = false;
                        }
                        else {
                            span1.className = "unCheck"
                            span2.className = "checked"
                            radio1.checked = false;
                            radio2.checked = true;
                        }
                        
                        e = e || window.event;
                        $("ImageCropperBackground").style.left = e.clientX + "px";
                        $("ImageCropperBackground").style.top = e.clientY + "px";
                        
                        $("Photo").click();
                        if(/msie/i.test(navigator.userAgent))
                        {   // IE浏览器
                            $("Photo").attachEvent("onpropertychange", SelectImage);
                        } 
                        else 
                        {   // 非ie浏览器，比如Firefox 
                            $("Photo").addEventListener("input", SelectImage, false); 
                        }
                    }

                    function SelectImage(){
                        var url = $("Photo").value;
                        var pos      = url.lastIndexOf("\\");
                        $("PhotoFilename").innerHtml = ( pos == -1 ? url : url.substr( pos + 1 ) );
                        
                        if ($("ImageCropperBackground").style.display == "none" && radio2.checked)
                        {
                            var ic = new ImageCropper("ImageCropperBackground", "ImageCropperDrag", url, {
                                Color: "#000",
                                Resize: false,
                                Right: "ImageCropperRight", Left: "ImageCropperLeft", Up:	"ImageCropperUp", Down: "ImageCropperDown",
                                RightDown: "ImageCroppeImageCropperRightDown", LeftDown: "ImageCroppeImageCropperLeftDown", RightUp: "ImageCroppeImageCropperRightUp", LeftUp: "ImageCroppeImageCropperLeftUp",
                                Preview: "ImagePreview"
                            })
                            
                            $("ImageCropperBackground").style.zIndex = $("Right").style.zIndex + 1;
                            $("ImageCropperBackground").style.display = "";
                            //$("ImageCropperDrag").style.width = "100px";
                            //$("ImageCropperDrag").style.height = "60px";

                            $("ImageCropperBackground").ondblclick = function(){
                                var p = ic.Url;
                                var o = ic.GetPos();
                                $('PointX').value = o.Left;
                                $('PointY').value = o.Top;
                                $('CropperWidth').value = o.Width;
                                $('CropperHeight').value = o.Height;
                                //alert("Left:" + o.Left + " Top:" + o.Top + " Width:" + o.Width + " Top:" + o.Height + " BaseWidth:" + ic._layBase.width + " BaseHeight:" + ic._layBase.height);
                                //pw = ic._layBase.width; // 原图宽
                                //ph = ic._layBase.height;
                                ic.Close();
                                var selects = document.getElementsByTagName('select');
                                for(index = 0; index < selects.length; index++){
                                    selects[index].style.display = ($("ImageCropperBackground").style.display == '') ? 'none':'';
                                }
                       	    	
                                //var dest = "1_small.jpg";
                                //$("ImageCropperCreat").onload = function(){ this.style.display = ""; }
                                //$("ImageCropperCreat").src = "ImageCropper.ashx?source=" + p + "&dest=" + dest + "&x=" + x + "&y=" + y + "&w=" + w + "&h=" + h + "&pw=" + pw + "&ph=" + ph + "&" + Math.random();
                            }
                                    
                            var selects = document.getElementsByTagName('select');
                            for(index = 0; index < selects.length; index++){
                                selects[index].style.display = ($("ImageCropperBackground").style.display == '') ? 'none':'';
                            }
                        }
                    }

                </script>
            <div>
                <label></label>
                <input type="image" src="images/uploadimg2.gif" /></div>

        </div>
        <div id="Warning" runat="server"></div><div id="Loading" style="display: none;"><img src='../images/loading.gif' align='absmiddle' /> 上传中...</div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" />
        </div>
    </div></form>
</body>
</html>
