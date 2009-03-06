<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleAddPhoto.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleAddPhoto" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>录入图片信息 - 内容管理 - 常智内容管理系统</title>
    <script src="Article/wis.js" language="javascript" type="text/javascript"></script>
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
        #ImageCropperDrag {border:1px dashed #fff; width:<%=ThumbnailWidth %>px; height:<%=ThumbnailHeight %>px; top:10px; left:10px; cursor:move; }
        #ImagePreview {border:1px dashed #fff; width:<%=ThumbnailWidth %>px; height:<%=ThumbnailHeight %>px; overflow:hidden; position:relative;}
    </style>
    <script type="text/javascript" language="javascript">
        function Check() {
            if ($("Photo$ctl03").value == "") {
                alert("请先浏览图片");
                return false;
            }
            $("Loading").style.display = "";
            return true;
        }
    </script>
</head>
<body style="background: #d6e7f7"><form id="form1" runat="server">
    <div>
        <div id="Position">当前位置：<asp:HyperLink ID="HyperLinkCategory" runat="server"></asp:HyperLink> » <a href="ArticleUpdate.aspx?ArticleGuid=<%=Request["ArticleGuid"] %>">录入内容</a> » 录入图片信息</div>
        <div class="add_step">
            <ul>
                <li>第一步：选择分类</li>
                <li>第二步：录入内容</li>
                <li class="current_step">第三步：录入更多内容</li>
                <li>第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main add_step3">
            <div><label>浏览图片：</label><FileUploads:DJFileUpload ID="Photo" runat="server" AllowedFileExtensions=".png,.jpg,.gif" /><FileUploads:DJAccessibleProgressBar ID="DJAccessibleProgrssBar1" runat="server" /></div>
            <div>
                <label>制作缩略图：</label>
                <asp:HiddenField ID="PointX" runat="server" />
                <asp:HiddenField ID="PointY" runat="server" />
                <asp:HiddenField ID="CropperWidth" runat="server" />
                <asp:HiddenField ID="CropperHeight" runat="server" />
                <FileUploads:DJUploadController ID="DJUploadController1" runat="server" ReferencePath="Backend/images/HtmlEditor/Dialogs/InsertPhotos/"  />
                <span class="unCheck" id="ThumbnailSpan1" onclick="Thumbnail_Load(event, 1);"><input name="Thumbnail" type="radio" value="Thumb" />按高度宽度最佳缩放</span> 
                <span class="unCheck" id="ThumbnailSpan2" onclick="Thumbnail_Load(event, 2);"><input name="Thumbnail" type="radio" value="Cropper" />在大图中手工裁剪</span>
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
            </div>
            <div id="ThumbConfig" style="display:none">
                <label>参数设置：</label>
                <span><input name="Stretch" type="checkbox" value="1" />拉伸</span>
                <span><input name="Beveled" type="checkbox" value="1" />斜角</span>
            </div>
            <div>
                <label>缩略图预览：<br />(<%=ThumbnailWidth %>*<%=ThumbnailHeight %>) &nbsp;&nbsp;</label>
                <div id="ImagePreview"><img src="images/noimg2.gif" alt="缩略图预览" /></div>
            </div>
                <script type="text/javascript" language="javascript">
                    var span1 = $("ThumbnailSpan1");
                    var span2 = $("ThumbnailSpan2");
                    var radio1 = span1.getElementsByTagName("input")[0];
                    var radio2 = span2.getElementsByTagName("input")[0];
                    function Thumbnail_Load(e, index){
                        var url = $("Photo$ctl03").value;
                        if(url == "")
                        {
                            alert("请先浏览图片");
                            return;
                        }
                        
                        if (index == 1) {
                            span1.className = "checked"
                            span2.className = "unCheck"
                            radio1.checked = true;
                            radio2.checked = false;                            
                            $("ThumbConfig").style.display == "";
                        }
                        else {
                            span1.className = "unCheck"
                            span2.className = "checked"
                            radio1.checked = false;
                            radio2.checked = true;
                            $("ThumbConfig").style.display == "none";
                        }                      
                        
                        if(radio1.checked)
                        {
                            var tempImg = document.createElement("img");
                            tempImg.src = url;
	                        var iWidth = tempImg.width, iHeight = tempImg.height, scale = iWidth / iHeight;
	                        
	                        //按比例设置
                            var fixWidth = <%=ThumbnailWidth %>;
                            var fixHeight = <%=ThumbnailHeight %>;
	                        if(fixHeight){ iWidth = (iHeight = fixHeight) * scale; }
	                        if(fixWidth && (!fixHeight || iWidth > fixWidth)){ iHeight = (iWidth = fixWidth) / scale; }

                            var img = $("ImagePreview").getElementsByTagName("img")[0];
	                        with(img.style){
		                        // 设置样式
		                        width = parseInt(iWidth) + "px";
		                        height = parseInt(iHeight) + "px";
		                        top = "";
		                        left = "";
		                        // 切割预览图
		                        clip = "rect(0px " + tempImg.width + "px " + tempImg.height + "px 0px)";
	                        }                            
                            img.src = url;                        	
                        }
                        
                        if ($("ImageCropperBackground").style.display == "none" && radio2.checked)
                        {
                            e = e || window.event;
                            $("ImageCropperBackground").style.left = e.clientX + "px";
                            $("ImageCropperBackground").style.top = e.clientY + "px";
                        
                            var cropper = new ImageCropper("ImageCropperBackground", "ImageCropperDrag", url, {
                                Color: "#000",
                                Resize: false,
                                Right: "ImageCropperRight", Left: "ImageCropperLeft", Up:	"ImageCropperUp", Down: "ImageCropperDown",
                                RightDown: "ImageCroppeImageCropperRightDown", LeftDown: "ImageCroppeImageCropperLeftDown", RightUp: "ImageCroppeImageCropperRightUp", LeftUp: "ImageCroppeImageCropperLeftUp",
                                Preview: "ImagePreview"
                            })
                            
                            $("ImageCropperBackground").style.zIndex = $("Position").style.zIndex + 1;
                            $("ImageCropperBackground").style.display = "";
                            $("ImageCropperBackground").ondblclick = function(){
                                var p = cropper.Url;
                                var o = cropper.GetPos();
                                $('PointX').value = o.Left;
                                $('PointY').value = o.Top;
                                $('CropperWidth').value = o.Width;
                                $('CropperHeight').value = o.Height;
                                cropper.Close();
                                var selects = document.getElementsByTagName('select');
                                for(index = 0; index < selects.length; index++){
                                    selects[index].style.display = ($("ImageCropperBackground").style.display == '') ? 'none':'';
                                }
                            }
                                    
                            var selects = document.getElementsByTagName('select');
                            for(index = 0; index < selects.length; index++){
                                selects[index].style.display = ($("ImageCropperBackground").style.display == '') ? 'none':'';
                            }
                        }
                    }

                </script>
            <div>
        </div>
        <div id="Warning" runat="server"></div>
        <div id="Loading" style="display: none;"><img src='images/loading.gif' align='absmiddle' /> 上传中...</div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" OnClientClick="javascript:return Check();" />
        </div>
    </div></form>
</body>
</html>
