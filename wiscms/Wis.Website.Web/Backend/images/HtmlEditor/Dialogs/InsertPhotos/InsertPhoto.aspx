<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertPhoto.aspx.cs" Inherits="Wis.Website.Web.Backend.images.HtmlEditor.Dialogs.InsertPhotos.InsertPhoto" %>
<%@ Register Assembly="Wis.Toolkit" Namespace="Wis.Toolkit.WebControls.FileUploads" TagPrefix="FileUploads" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="PageHeader" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上传图片</title>
    <link href="InsertPhoto.css" rel="stylesheet" media="all" type="text/css" />
    <script type="text/javascript" src="../Dialog.js"></script>
    <script type="text/javascript" language="javascript">
    var AllowImageSize = "100"; // TODO:上传图片的大小可配置
    
    var InsertPhoto = new Object();
    InsertPhoto.Action = "INSERT";
    //InsertPhoto.Selection = parent.document.selection.createRange();
    //InsertPhoto.RangeType = parent.document.selection.type;
    if (InsertPhoto.RangeType && InsertPhoto.RangeType == "Control") {
	    if (InsertPhoto.Selection.item(0).tagName == "IMG"){
		    InsertPhoto.Action = "MODI";
		    InsertPhoto.Title = "修改";
		    InsertPhoto.CheckFlag = "url";
		    InsertPhoto.Control = oSelection.item(0);
		    InsertPhoto.ImageSrc = oControl.getAttribute("src", 2);
		    InsertPhoto.Alt = oControl.alt;
		    InsertPhoto.Border = oControl.border;
		    InsertPhoto.BorderColor = oControl.style.borderColor;
		    InsertPhoto.Filter = oControl.style.filter;
		    InsertPhoto.Align = oControl.align;
		    InsertPhoto.Width = oControl.width;
		    InsertPhoto.Height = oControl.height;
		    InsertPhoto.VSpace = oControl.vspace;
		    InsertPhoto.HSpace = oControl.hspace;
	    }
    }
    
    function InitDocument(){
	    if(InsertPhoto.Filter) SearchSelectValue($("ImageFilter"), InsertPhoto.Filter);
	    if(InsertPhoto.Align) SearchSelectValue($("ImageAlign"), InsertPhoto.Align.toLowerCase());

	    if(InsertPhoto.ImageSrc) $("ImageSrc").value = InsertPhoto.ImageSrc;
	    if(InsertPhoto.Alt) $("ImageAlt").value = InsertPhoto.Alt;
	    if(InsertPhoto.Border) $("ImageBorder").value = InsertPhoto.Border;
	    if(InsertPhoto.BorderColor) $("ImageBordercolor").value = InsertPhoto.BorderColor;
	    if(InsertPhoto.BorderColor) $("ImageBordercolor").style.backgroundColor = InsertPhoto.BorderColor;
	    if(InsertPhoto.Width) $("ImageWidth").value = InsertPhoto.Width;
	    if(InsertPhoto.Height) $("ImageHeight").value = InsertPhoto.Height;
	    if(InsertPhoto.VSpace) $("ImageVspace").value = InsertPhoto.VSpace;
	    if(InsertPhoto.HSpace) $("ImageHspace").value = InsertPhoto.HSpace;

	    PreviewImage(InsertPhoto.ImageSrc);
    }

    function PreviewImage(src){
	    if(!src) return;	    
	    $("ImageSrc").value = src;
	    if (src.lastIndexOf(".") <= 0){
	        divPreview.innerHTML = "<img src='noimg.gif' id='imgPreview' />";
	        imgPreviewSize.innerHTML = "";
	        return;
	    }

	    if (src){
		    if (!Wis.Browser.IsIE7){
		        $("divPreview").innerHTML = "<img src='" + src + "' id='imgPreview' />";
		    }
		    
            var bw = $("divPreview").offsetWidth;
            var bh = $("divPreview").offsetHeight;

            $("divPreview").innerHTML = "<div id=imgPreviewDiv style=\"filter : progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image) " + $("ImageFilter").value + ";WIDTH:10px; HEIGHT:10px;\"></div>";
            $("imgPreviewDiv").filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = src;
            var w = $("imgPreviewDiv").offsetWidth;
            var h = $("imgPreviewDiv").offsetHeight;
            var sw,sh;

            if ((w>bw)||(h>bh)){
	            var nw = bw/w;
	            var nh = bh/h;
	            if (nw>nh){
		            sh = bh;
		            sw = w*nh;
	            }else{
		            sw = bw;
		            sh = h*nw;
	            }
            }else{
	            sw = w;
	            sh = h;
            }

            $("imgPreviewDiv").style.width = sw;
            $("imgPreviewDiv").style.height = sh;
            $("imgPreviewDiv").filters.item("DXImageTransform.Microsoft.AlphaImageLoader").sizingMethod = 'scale';	

            $("imgPreviewSize").innerHTML = w + " * " + h;
	    }else{
		    $("tdPreview").innerHTML = "";
	    }
    }

    function ok(){
	    ImageBorder.value = ToInt(ImageBorder.value);
	    ImageWidth.value = ToInt(ImageWidth.value);
	    ImageHeight.value = ToInt(ImageHeight.value);
	    ImageVspace.value = ToInt(ImageVspace.value);
	    ImageHspace.value = ToInt(ImageHspace.value);

	    if (!IsColor(ImageBordercolor.value)){
		    BaseAlert(ImageBordercolor, "提示：\n\n无效的边框颜色值！");
		    return false;
	    }
    	
    	// 遍历 Box2 的所有input
        var inputs = document.getElementById('Box2').getElementsByTagName('input');
        for(index = 0; index < inputs.length; index++){
	        if (inputs[index].type="file" && !IsExt(inputs[index].value, "gif|jpg|jpeg|bmp")){
		        UploadError("ext");
		        return false;
	        }
        }    	

	    DisableItems();
	    
	    ReturnValue();
    }

    function ReturnValue(){
        // 
        
	    InsertPhoto.ImageSrc = ImageSrc.value;
	    InsertPhoto.Alt = ImageAlt.value;
	    InsertPhoto.Border = ImageBorder.value;
	    InsertPhoto.BorderColor = ImageBordercolor.value;
	    InsertPhoto.Filter = ImageFilter.options[ImageFilter.selectedIndex].value;
	    InsertPhoto.Align = ImageAlign.value;
	    InsertPhoto.Width = ImageWidth.value;
	    InsertPhoto.Height = ImageHeight.value;
	    InsertPhoto.VSpace = ImageVspace.value;
	    InsertPhoto.HSpace = ImageHspace.value;

	    if (sAction == "MODI") {
		    InsertPhoto.Control.src = InsertPhoto.ImageSrc;
		    InsertPhoto.Control.alt = InsertPhoto.Alt;
		    InsertPhoto.Control.border = InsertPhoto.Border;
		    InsertPhoto.Control.style.borderColor = InsertPhoto.BorderColor;
		    InsertPhoto.Control.style.filter = InsertPhoto.Filter;
		    InsertPhoto.Control.align = InsertPhoto.Align;
		    InsertPhoto.Control.width = InsertPhoto.Width;
		    InsertPhoto.Control.height = InsertPhoto.Height;
		    InsertPhoto.Control.style.width = InsertPhoto.Width;
		    InsertPhoto.Control.style.height = InsertPhoto.Height;
		    InsertPhoto.Control.vspace = InsertPhoto.VSpace;
		    InsertPhoto.Control.hspace = InsertPhoto.HSpace;
		    if (InsertPhoto.ImageSrc){
			    InsertPhoto.Selection.execCommand("CreateLink", false, InsertPhoto.ImageSrc);
			    var link = InsertPhoto.Selection(0).parentNode;
			    link.target = "_blank";
		    }
	    }else{
		    var img = '';
		    if (InsertPhoto.Filter!=""){
			    img = img + 'filter:' + InsertPhoto.Filter + ';';
		    }
		    if (InsertPhoto.BorderColor!=""){
			    img = img + 'border-color:' + InsertPhoto.BorderColor + ';';
		    }
		    if (img!=""){
			    img=' style = "' + img + '"';
		    }
		    img = '<img id=eWebEditor_TempElement_Img src="' + InsertPhoto.ImageSrc + '"' + img;
		    if (InsertPhoto.Border!=""){
			    img = img + ' border="' + InsertPhoto.Border + '"';
		    }
		    if (InsertPhoto.Alt!=""){
			    img = img + ' alt="' + InsertPhoto.Alt + '"';
		    }
		    if (InsertPhoto.Align!=""){
			    img = img + ' align="' + InsertPhoto.Align + '"';
		    }
		    if (InsertPhoto.Width!=""){
			    img = img + ' width="' + sWidth + '"';
		    }
		    if (InsertPhoto.Height!=""){
			    img = img + ' height="' + InsertPhoto.Height + '"';
		    }
		    if (sVSpace!=""){
			    img = img + ' vspace="' + InsertPhoto.VSpace+'"';
		    }
		    if (InsertPhoto.HSpace!=""){
			    img = img + ' hspace="' + InsertPhoto.HSpace + '"';
		    }
		    img = img + '>';
		    if (InsertPhoto.ImageSrc){
			    img = '<a id=eWebEditor_TempElement_Img_Href href="' + InsertPhoto.ImageSrc + '" target="_blank">' + img + '</a>';
		    }
		    dialogArguments.insertHTML(img);

		    var oTempElement = dialogArguments.eWebEditor.document.getElementById("eWebEditor_TempElement_Img");
		    if (InsertPhoto.ImageSrc){
			    oTempElement.src = InsertPhoto.ImageSrc;
			    var oTempElementHref = dialogArguments.eWebEditor.document.getElementById("eWebEditor_TempElement_Img_Href");
			    oTempElementHref.setAttribute("href", InsertPhoto.ImageSrc);
			    oTempElementHref.removeAttribute("id");
		    }else{
			    oTempElement.src = InsertPhoto.ImageSrc;
		    }
		    oTempElement.removeAttribute("id");
	    }

	    //window.returnValue = null;
	    //window.close();
    }
    
    function DisableItems(){
        var inputs = document.getElementsByTagName('input');
        for(index = 0; index < inputs.length; index++){
	        inputs[index].disabled=true;
        }
    }

    /// <summary>
    /// 边框颜色。    /// </summary>
    function SelectColor(){
        var dialogsPath = '../';
        var url = dialogsPath + "selcolor.htm?color=" + encodeURIComponent($("ImageBordercolor").value);
        var color = showModalDialog(url, window, "dialogWidth:0px;dialogHeight:0px;help:no;scroll:no;status:no"); // dialogWidth:280px;dialogHeight:250px;
        if (color) {
            $("ImageBordercolor").value = color;
            $("RectBordercolor").style.backgroundColor = color;
        }
    }
    </script>
</head>

<script language="javascript" type="text/javascript">
function tabChange(boxId) {
    var Box1 = document.getElementById("Box1");
    var Box2 = document.getElementById("Box2");
    var tabIn = document.getElementById("tab").getElementsByTagName("a");
    if (boxId == 1) {
        Box1.style.display="block";
        Box2.style.display="none";
        tabIn[0].className ="tabNoBorder";
        tabIn[1].className ="";
    }
    if (boxId == 2) { 
        Box1.style.display = "none";
        Box2.style.display = "block";
        tabIn[0].className = "";
        tabIn[1].className = "tabNoBorder";
    }
}
  
function picShow(obj) {
    var picSrc = obj.src;
    var imgPreview = document.getElementById("imgPreview");
    if (picSrc != "") {
        imgPreview.src = obj.src;
        if (imgPreview.width>160) {
            imgPreview.style.width = 160+"px";
        } else {
            imgPreview.style.width = "";
        }
    }
}
</script>

<body onload="InitDocument()"><form id="MainForm" runat="server">
    <div class="widowContent">
        <ul class="tab" id="tab">
            <li><a href="javascript:tabChange(1)" class="tabNoBorder" onfocus="this.blur();">从图片库中选择</a></li>
            <li><a href="javascript:tabChange(2)" class="" onfocus="this.blur();">从电脑上传</a></li>
        </ul>
        <div id="Box1">
            <div id="picChoose">
                <div id="SelectBox">
                    <asp:DropDownList ID="DropDownListDirectory" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListDirectory_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div id="allPics">
                    <asp:Repeater ID="RepeaterImages" EnableViewState="false" runat="server">
                        <ItemTemplate><a href="####"><img src="<%# DataBinder.Eval(Container.DataItem, "ImagePath") %>" onclick="PreviewImage(this.src);" /></a></ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div id="picShow">
                <div id="divPreview"><img src="noimg.gif" id="imgPreview" /></div>
                <div id="imgPreviewSize"></div>
                <div class="imgInputA">
                    图片地址：<input id="ImageSrc" type="text" /> </div>
                <div class="imgInputA">
                    说明文字：<input type=text id=ImageAlt size=10 value="" />
                </div>
                <div class="imgInputB">
                    边框粗细：<input style="width:80px" type=text id=ImageBorder size=10 value="" ONKEYPRESS="event.returnValue=IsDigit();">                    边框颜色：<input style="width:62px" type=text id=ImageBordercolor size=7 value=""><img border="0" src="Images/rect.gif" width="18" style="cursor:hand" id="RectBordercolor" onclick="SelectColor('bordercolor')" align=absmiddle />                </div>
                <div class="imgInputB">
                    特殊效果：<select id=ImageFilter size=1 style="width:80px" onchange="PreviewImage()">					<option value='' selected lang=DlgComNone></option>					<option value='Alpha(Opacity=50)'>半透明</option>					<option value='Alpha(Opacity=0, FinishOpacity=100, Style=1, StartX=0, StartY=0, FinishX=100, FinishY=140)' lang=DlgImgAlpha2>线型透明</option>					<option value='Alpha(Opacity=10, FinishOpacity=100, Style=2, StartX=30, StartY=30, FinishX=200, FinishY=200)' lang=DlgImgAlpha3></option>					<option value='blur(add=1,direction=14,strength=15)' lang=DlgImgBlur1>模糊效果</option>					<option value='blur(add=true,direction=45,strength=30)' lang=DlgImgBlur2>风动模糊</option>					<option value='Wave(Add=0, Freq=60, LightStrength=1, Phase=0, Strength=3)' lang=DlgImgWave>正弦波纹</option>					<option value='gray' lang=DlgImgGray>黑白照片</option>					<option value='Chroma(Color=#FFFFFF)' lang=DlgImgChroma>白色透明</option>					<option value='DropShadow(Color=#999999, OffX=7, OffY=4, Positive=1)' lang=DlgImgDropShadow>投射阴影</option>					<option value='Shadow(Color=#999999, Direction=45)' lang=DlgImgShadow>阴影</option>					<option value='Glow(Color=#ff9900, Strength=5)' lang=DlgImgGlow>发光</option>					<option value='flipv' lang=DlgImgFlipv>垂直翻转</option>					<option value='fliph' lang=DlgImgFliph>左右翻转</option>					<option value='grays' lang=DlgImgGrays>降低彩色</option>					<option value='xray' lang=DlgImgXray>X光照片</option>					<option value='invert' lang=DlgImgInvert>底片</option>					</select>                    对齐方式：<select id=ImageAlign size=1 style="width:80px">					<option value='' selected lang=DlgComDefault>默认</option>					<option value='left' lang=DlgAlignLeft>左对齐</option>					<option value='right' lang=DlgAlignRight>右对齐</option>					<option value='top' lang=DlgAlignTop>顶部</option>					<option value='middle' lang=DlgAlignMiddle>中部</option>					<option value='bottom' lang=DlgAlignBottom>底部</option>					<option value='absmiddle' lang=DlgAlignAbsmiddle>绝对居中</option>					<option value='absbottom' lang=DlgAlignAbsbottom>绝对底部</option>					<option value='baseline' lang=DlgAlignBaseline>基线</option>					<option value='texttop' lang=DlgAlignTexttop>文本顶部</option>					</select>                </div>
                <div class="imgInputB">
                    图片宽度：<input style="width:80px" type=text id=ImageWidth size=10 value="" ONKEYPRESS="event.returnValue=IsDigit();" maxlength=4 />                    图片高度：<input style="width:80px" type=text id=ImageHeight size=10 value="" ONKEYPRESS="event.returnValue=IsDigit();" maxlength=4 />                </div>
                <div class="imgInputB">
                    上下间距：<input style="width:80px" type=text id=ImageVspace size=10 value="" ONKEYPRESS="event.returnValue=IsDigit();" maxlength=2>                    左右间距：<input style="width:80px" type=text id=ImageHspace size=10 value="" ONKEYPRESS="event.returnValue=IsDigit();" maxlength=2>                </div>                        </div>
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
