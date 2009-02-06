var userAgent = navigator.userAgent.toLowerCase();
var opera = (userAgent.indexOf('opera') != -1);
var opera8 = ((userAgent.indexOf('opera 8') != -1 || userAgent.indexOf('opera/8') != -1) ? 1 : 0);
var ns4 = (document.layers) ? true : false;
var ie4 = (document.all && !document.getElementById) ? true : false;
var ie5 = (document.all && document.getElementById) ? true : false;
var ns6 = (!document.all && document.getElementById) ? true : false;

// TODO:用Javascript类似类/结构体的方式实现
function HtmlEditor(name) { //初始化属性
	this.DesignViewCss = '';      //
	this.SourceViewCss = '';      //
	this.PlainTextViewCss = '';   //
	this.PreviewViewCss = '';     //
	this.StartDesignerView = 1;   //
	this.ReadOnly = true;         // 只读
}

HtmlEditor.prototype.Initialize = function(){
	//
}

/// <summary>
/// 根据ID获得元素。
/// </summary>
function getElementById(id) {
	var element = null;

	if (ie5 || ns6 || opera){
		element = document.getElementById(id);
	}else if (ie4) {
		element = document.all[id];
	}else if (ns4) {
		element = document.layers[id];
	}else {
	    eval('element = ' + id + ';'); // TODO:Test
	}
	
	return element;
}

// init designer
function HtmlEditor_Initialize(designer, hiddenid, readOnly) {

    // readonly
    if(readOnly)
        designer.document.designMode = 'Off';
    else
        designer.document.designMode = 'On';

	// set hidden value.
    var o = getElementById(hiddenid); // TODO:兼容性
	designer.document.open();
	designer.document.write(o.value);
	designer.document.close();

    // readonly
    if(readOnly)
        designer.document.contentEditable = 'False';
    else
        designer.document.contentEditable = 'True';

    // apply styles
	HtmlEditor_Scrollbar_Styles(designer);
}

/// <summary>
/// 设定滚动条的样式。
/// TODO:配置滚动条的样式
/// TODO:项目有5处使用，只要一次调用？需要测试切换不同视图时需要不需要重新执行该函数
/// </summary>
function HtmlEditor_Scrollbar_Styles(designer) {
	bodyStyle = designer.document.body.style;
	
	bodyStyle.scrollbar3dLightColor= '#D4D0C8';
	bodyStyle.scrollbarArrowColor= '#000000';
	bodyStyle.scrollbarBaseColor= '#D4D0C8';
	bodyStyle.scrollbarDarkShadowColor= '#D4D0C8';
	bodyStyle.scrollbarFaceColor= '#D4D0C8';
	bodyStyle.scrollbarHighlightColor= '#808080';
	bodyStyle.scrollbarShadowColor= '#808080';
	bodyStyle.scrollbarTrackColor= '#D4D0C8';
	bodyStyle.border='0';
}

function HtmlEditor_OnKeyDown(designer, designerView) {
    // 切换视图
    // TODO:'Ctrl+TAB' or 'TAB'	
//    if ((designer.event.keyCode == 9 && designer.event.ctrlKey) || designer.event.keyCode == 9) {
//	    
//        switch(designerView)
//        {
//            case 'DesignView':
//		        HtmlEditor_SetActiveTab(document.getElementById('$ClientID$_DesignViewTab'));
//		        $ClientID$_ChangeDesignerView(editor,document.getElementById('$ClientID$_Temp'),'$ClientID$_Toolbars', false, true);
//                break;
//            case 'SourceView':
//                break;
//            case 'PlainTextView':
//                break;
//            case 'PreviewView':
//                break;
//        }
//	    $ClientID$_Designer.event.cancelBubble = true;
//	    $ClientID$_Designer.event.returnValue = false;
//    }

    // 过滤双引号
    if (designer.event.keyCode == 222 && designer.event.shiftKey && designerView == 'DesignView') {
		var sel = designer.document.selection;
		if (sel.type == 'Control') return;
		var r = sel.createRange();
		var before = HtmlEditor_CharBefore(r); //?HtmlEditor_CharBefore
		var after = HtmlEditor_CharAfter(r); //?HtmlEditor_CharAfter
		var r = sel.createRange();

		if (before == 'start') {
			r.pasteHTML('&#8220;');
			designer.event.cancelBubble = true;
			designer.event.returnValue = false;
			return false;
		} else if (before != ' ' && after == 'end') {
			r.pasteHTML('&#8221;');
			designer.event.cancelBubble = true;
			designer.event.returnValue = false;
			return false;
		} else if (before == ' ' && after == 'end') {
			r.pasteHTML('&#8220;');
			designer.event.cancelBubble = true;
			designer.event.returnValue = false;
			return false;
		} else if (before != ' ' && after == ' ') {
			r.pasteHTML('&#8221;');
			designer.event.cancelBubble = true;
			designer.event.returnValue = false;
			return false;
		} else {
			r.pasteHTML('&#8220;');
			designer.event.cancelBubble = true;
			designer.event.returnValue = false;
			return false;
		}
	}
	
	// 处理回车
	if (designer.event.keyCode == 13) { // 
		var sel = designer.document.selection;
		if (sel.type == 'Control') {
			return;
		}
		
		var r = sel.createRange();
		if ((!HtmlEditor_CheckTag(r.parentElement(),'LI'))&&(!HtmlEditor_CheckTag(r.parentElement(),'H'))) { //?HtmlEditor_CheckTag
			r.pasteHTML('<br>');
			designer.event.cancelBubble = true; 
			designer.event.returnValue = false; 
			r.select();
			r.collapse(false);
			return false;
		}
	} 
}

/// <summary>
/// OnPaste。
/// Determines what happens when a user pastes into the control
/// TODO: 在on paste事件中处理
/// help the user pasting formatted content from Microsoft Word and other applications, 
/// and apply different types of format stripping:
/// 1. Strip Word-formatting on paste 
/// 2. Strip Word-formatting on paste (cleaning fonts and sizes) 
/// 3. Forced format stripping on Paste 
/// 4. Word Content in Clipboard Interception 
/// 5. Strip Word-formatting after paste 
/// 6. Paste plain _Text 
/// 7. Paste as HTML
/// </summary>
function HtmlEditor_Disabled_OnPaste() {
    alert('禁用粘贴功能。');
    return false;
}

function HtmlEditor_HtmlRemoved_OnPaste(designer) {

    // 提示是否清除word格式
    
}
function HtmlEditor_HtmlRemoved_OnPaste(designer) {
    var text = window.clipboardData.getData('Text');
    text = text.replace(/<[^>]*>/gi,'');
    designer.focus();
    s = designer.document.selection.createRange();
    s.pasteHTML(text); 
    return false;
}

/// <summary>
/// 粘贴。
/// </summary>
function HtmlEditor_OnPaste(designer, designerView, tempDesigner, switchMode) {
    // 预览状态不允许粘贴
    if(designerView == 'PreviewView') return false;
    
    if(designerView == 'PlainTextView')
    {
        if(switchMode == 'Normal')
        {
            if (!confirm('警告！在纯文本视图粘贴将丢失所有格式，您确认继续粘贴吗？')){
	            return false;
            }
        }
    }
    
    var html = HtmlEditor_GetClipboardData(tempDesigner); // 为Html
    // window.clipboardData.getData('Text')); // 为纯文本
    
    var r = /<\w[^>]* class="?MsoNormal"?/gi;
    if ( r.test(html)){
        if(confirm('清除 Word 格式后再粘贴？'))
        {
            html = StripWordFormatting(html);
        }
    }

    if(designerView == 'SourceView')
    {
        // html = HtmlEncode(html);
    }

    html=RemoveLocation(html);
   

          
    designer.focus();
    s = designer.document.selection.createRange();
    s.pasteHTML(html); 

    // TODO:remove first
    if(designerView == 'PlainTextView')
    {
        designer.focus(); 
	    // 全选文本，清理格式
	    designer.document.execCommand('SelectAll');
	    designer.document.execCommand('removeFormat','',null);
	    designer.focus();
	    designer.document.body.innerHTML = designer.document.body.innerText;
    }
    
	return false;
}
   // remove window.location
function RemoveLocation(htmlContent)
{
     var pattern = location.protocol + '//' + location.host + location.pathname + location.search;
    while(htmlContent.lastIndexOf(pattern) > 0){
        htmlContent = htmlContent.replace(pattern, '');
    }
    return htmlContent;
}
/// <summary>
/// 获取粘贴板的内容。
/// </summary>
function HtmlEditor_GetClipboardData(temp) {
	temp.innerHTML = '';
	
	var rng = document.body.createTextRange() ;
	rng.moveToElementText(temp);
	rng.execCommand('Paste') ;
	
	var html = temp.innerHTML ;
	temp.innerHTML = '';
	
	return html;
}

/// <summary>
/// 对Html进行编码。
/// </summary>
function HtmlEncode(html){
	html = html.replace(/\n/g,"<br>");  // \n
	html = html.replace(/\ /g,"&nbsp;");// \ 
	html = html.replace(/&/g, "&amp;") ; // &
	html = html.replace(/"/g, "&quot;") ;// "
	html = html.replace(/</g, "&lt;") ; // <
	html = html.replace(/>/g, "&gt;") ; // >
	html = html.replace(/\t/g,"&nbsp;&nbsp;&nbsp;&nbsp;");//\t
	
	// TODO:符号转义
	return html;
}

/// <summary>
/// 清除 Html 的 Word 格式。
/// </summary>
function StripWordFormatting(html){

	html = html.replace(/<o:p>&nbsp;<\/o:p>/g, '');
	html = html.replace(/o:/g, '');
	html = html.replace(/<st1:.*?>/g, '');

	html = html.replace(/<\/?SPAN[^>]*>/gi, "" );
	html = html.replace(/<(\w[^>]*) class=([^ |>]*)([^>]*)/gi, "<$1$3") ;
	html = html.replace(/<(\w[^>]*) style="([^"]*)"([^>]*)/gi, "<$1$3") ;
	html = html.replace(/<(\w[^>]*) lang=([^ |>]*)([^>]*)/gi, "<$1$3") ;
	html = html.replace(/<\\?\?xml[^>]*>/gi, "") ;
	html = html.replace(/<\/?\w+:[^>]*>/gi, "") ;
	html = html.replace(/&nbsp;/, " " );
	
	// remove empty tags
	html = html.replace(/<font>/g, '');
	//html = html.replace(/<span>/g, '');

	return html;
}

/// <summary>
/// 获取编辑器的Html内容。
/// </summary>
function HtmlEditor_GetText(designer, designerView) {
	switch (designerView){
		case 'DesignView': // 设计视图
			return designer.document.body.innerHTML; 
		case 'SourceView': // 源代码视图
			return designer.document.body.innerText; 
		case 'PlainTextView': // 纯文本视图
			return designer.document.body.innerText; 
		case 'PreviewView': // 预览视图
			return designer.document.body.innerHTML; 
	}
	
	return null;
}

/// <summary>
/// 设置编辑器的Html内容。
/// </summary>
function HtmlEditor_SetText(designer, designerView, html) {
    if(designerView == 'PreviewView') return; // 预览视图

    designer.focus();
    if (designer.document.selection.type.toLowerCase() != "none"){
		designer.document.selection.clear() ;
	}
	
	switch (designerView){
		case 'DesignView': // 设计视图
			designer.document.body.innerHTML = html;
			break;
		case 'SourceView': // 源代码视图
		    // html = HtmlEncode(html);
			designer.document.body.innerText = html; 
			break;
		case 'PlainTextView': // 纯文本视图
		    // html = HtmlEncode(html);
			designer.document.body.innerText = html; 
			break;
	}
}

/// <summary>
/// 保存编辑器的Html内容。
/// </summary>
function HtmlEditor_Save(designer, hiddenHtml, designerView) {
    var o = getElementById(hiddenHtml);
    
    if(o == null) return false;
    o.value = HtmlEditor_GetText(designer, designerView);
    
	if (o.value == "<P>&nbsp;</P>") {
		o.value = "";
	}
	
    // remove window.location
    o.value=RemoveLocation(o.value);
	
	return true;
}

function HtmlEditor_Format(designer, designerView, format) {
	if (designerView != 'DesignView') return;

	designer.focus();
	designer.document.execCommand(format,'',null);
}

function HtmlEditor_CheckTag(item, tagName) {
	if (item.tagName.search(tagName)!=-1) {
		return item;
	}
	if (item.tagName=='BODY') {
		return false;
	}
	item = item.parentElement;
	return HtmlEditor_CheckTag(item,tagName);
}

function HtmlEditor_CharBefore(sel) {
	if (sel.move('character',-1) == -1) {
		sel.expand('character');
		return sel.text;
	} else {
		return 'start';
	}
}

function HtmlEditor_CharAfter(sel) {
	var sel2 = sel;
	if (sel.expand('character')) {
		sel2.move('character',1);
		sel2.expand('character');
		return sel2.text;
	} else {
		return 'end';
	}
}

function HtmlEditor_CharBefore(r) {
	if (r.move('character',-1) == -1) {
		r.expand('character');
		return r.text;
	} else {
		return 'start';
	}
}

function HtmlEditor_CharAfter(r) {
	var r2 = r;
	if (r.expand('character')) {
		r2.move('character',1);
		r2.expand('character');
		return r2.text;
	} else {
		return 'end';
	}
}
function HtmlEditor_GetRangeReference(designer) {
	designer.focus();
	var objReference = null;
	var RangeType = designer.document.selection.type;
	var selectedRange = designer.document.selection.createRange();

	switch(RangeType) {
		case 'Control' :
			if (selectedRange.length > 0 )  {
				objReference = selectedRange.item(0);
			}
			break;
		case 'None' :
			objReference = selectedRange.parentElement();
			break;
		case 'Text' :
			objReference = selectedRange.parentElement();
			break;
	}
	return objReference;
}

function HtmlEditor_SetStyle(buttonTD,style,checkstyle) {
	if (buttonTD == null) return;

	if (buttonTD.className != checkstyle)
		buttonTD.className = style;
}

function HtmlEditor_GetStyle(className) {
	underscore = className.indexOf('_');
	if (underscore < 0) return className;
	return className.substring(underscore+1);
}