//------------------------------------------------------------------------------
// <copyright file="ScriptBlock.js" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

// Hotkeys
// SmartTags
// StatusBar


// Mouse event
function HtmlEditor_ToolbarButton_MouseOver()
{}

function HtmlEditor_ToolbarButton_MouseOut()
{}

function HtmlEditor_ToolbarButton_MouseDown()
{}

function HtmlEditor_ToolbarButton_MouseUp()
{}

/// <summary>
/// 设定Separator Bar。
/// </summary>
function HtmlEditor_SeparatorBar(designer, designerView) {}

/// <summary>
/// 设定Separator Bar。
/// </summary>
function HtmlEditor_Address(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();

	designer.document.execCommand('formatBlock','','<address>');
}

/// <summary>
/// 插入附件。
/// </summary>
function HtmlEditor_AttachFile(designer, designerView, dialogsPath) {
	if (designerView != 'DesignView') return;
	designer.focus();

	var script = dialogsPath + 'myInsertFlash.aspx'; // TODO:允许上传的文件类型
	
	showModalDialog(script, designer, 'dialogWidth:900px; dialogHeight:600px;help:0;status:0;resizeable:1;');
}

/// <summary>
/// 文本高亮颜色。
/// </summary>
function HtmlEditor_TextHighlightColor(designer,designerView, dialogsPath) {
	if (designerView != 'DesignView') return;
	script = dialogsPath + 'ColorPicker.htm?action=backcolor';
	color = showModalDialog(script, designer, 'dialogWidth:280px;dialogHeight:250px;status:0;scroll:0;help:0;');
	designer.document.execCommand('backcolor','',color);
}

/// <summary>
/// 字体颜色。
/// </summary>
function HtmlEditor_FontForeColor(designer, designerView, dialogsPath) {
	if (designerView != 'DesignView') return;
	script = dialogsPath + 'ColorPicker.htm?action=forecolor';
	color = showModalDialog(script,designer,'dialogWidth:280px;dialogHeight:250px;status:0;scroll:0;help:0;');
	designer.document.execCommand('forecolor','',color);
}

/// <summary>
/// 粗体。
/// </summary>
function HtmlEditor_Bold(designer, designerView) {
	if (designerView != 'DesignView') return;
	HtmlEditor_Format(designer, designerView, 'bold'); 
}

/// <summary>
/// 项目符号列表。
/// </summary>
function HtmlEditor_BulletedList(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'insertunorderedlist'); 
}

var changetype = 0;
/// <summary>
/// 更改大小写。
/// </summary>
function HtmlEditor_ChangeCase(designer, designerView) {
	var sel = designer.document.selection.createRange();
	var html = sel.htmlText;

	splitwords = html.split(' ');
	var f = '';

	for (var index=0; index<splitwords.length; index++) {
		switch (changetype) {
			case 0:
				f += splitwords[index].toUpperCase();
				break;
			case 1:
				f += splitwords[index].toLowerCase();
				break;
			case 2:
				tot = splitwords[index].length;
				if (tot > 1) {
					f += splitwords[index].substring(0,1).toUpperCase() + splitwords[index].substring(1,splitwords[index].length).toLowerCase();
				} else {
					f += splitwords[index].toUpperCase();
				}
				break;
		}
		if (index <(splitwords.length-1)) f += ' ';
	}
	sel.pasteHTML(f);
	sel = designer.document.selection.createRange();
	sel.findText(f);
	sel.select();

	//designer.focus();

	changetype++;
	if (changetype > 2) changetype = 0;
}
		
/// <summary>
/// 清空编辑器中所有的文字和 HTML 代码。
/// </summary>
function HtmlEditor_Clear(designer, designerView) {
	if (confirm('确实要删除编辑器中所有的文字和 HTML 代码吗？')) {	
		designer.document.body.innerHTML = '';
		designer.document.body.innerText = '';
	}
	designer.focus();
}

/// <summary>
/// 插入超链接。
/// </summary>
function HtmlEditor_CreateLink(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
    designer.document.execCommand('createlink', '1', null);
}

/// <summary>
/// 剪切。
/// </summary>
function HtmlEditor_Cut(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('cut', '', null);
}

/// <summary>
/// 复制。
/// </summary>
function HtmlEditor_Copy(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('copy', '', null);
}

/// <summary>
/// 查找。
/// </summary>
function HtmlEditor_Find(designer, designerView, dialogsPath) {
	var o = new Array();
	o['designer'] = designer;
	findArr = showModalDialog('FindReplace.html', o, 'dialogWidth:370px; dialogHeight:130px;help:0;status:0;');
	designer.focus();
}

/// <summary>
/// 查看使用帮助。
/// </summary>
function HtmlEditor_Help(designer,designerView, dialogsPath) {
    if (designerView != 'DesignView') return;
	designer.focus();

	var script = dialogsPath + 'Help.htm';
	showModalDialog(script,designer,'dialogWidth:400px; dialogHeight:300px;help:0;status:0;resizeable:1;');
}

/// <summary>
/// 增加缩进。
/// </summary>
function HtmlEditor_Indent(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'indent'); 
}

/// <summary>
/// 插入当前日期。
/// TODO:加入日历选择器，显示更丰富的插入日期功能。
/// </summary>
function HtmlEditor_InsertDate(designer,designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	var d = new Date();
	sel = designer.document.selection.createRange();
	sel.pasteHTML(d.toLocaleDateString());
}

/// <summary>
/// 插入Flash文件。
/// </summary>
function HtmlEditor_InsertFlash(designer, designerView, dialogsPath) {
    if (designerView != 'DesignView') return;
	designer.focus();

	var script = dialogsPath + 'myInsertFlash.aspx';
	showModalDialog(script,designer,'dialogWidth:900px; dialogHeight:600px;help:0;status:0;resizeable:1;');
}

/// <summary>
/// 插入水平线。
/// </summary>
function HtmlEditor_InsertHorizontalRule(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	HtmlEditor_Format(designer, designerView, 'inserthorizontalrule'); 
}

/// <summary>
/// 插入图片。
/// </summary>
function HtmlEditor_InsertImage(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('insertimage', 1, '');
}

/// <summary>
/// 插入照片。
/// </summary>
function HtmlEditor_InsertPhoto(designer, designerView, dialogsPath) {
	if (designerView != 'DesignView') return;
	designer.focus();

	var o = HtmlEditor_GetRangeReference(designer);
	if (o.tagName == 'IMG') {
		designer.document.execCommand('insertimage', 1, '');
		return;
	}

	var url = dialogsPath + 'InsertImage.aspx';
	showModalDialog(url, designer, 'dialogWidth:900px; dialogHeight:600px;help:0;status:0;resizeable:0;');
}

/// <summary>
/// 插入特殊符号。
/// </summary>
function HtmlEditor_InsertSymbol(designer, designerView, dialogsPath) {
	if (designerView != 'DesignView') return;
	designer.focus();

	var url = dialogsPath + 'InsertSymbol.htm';
	var symbol = showModalDialog(url, window, 'dialogWidth:350px; dialogHeight:220px;help:0;status:0;resizeable:1;');
	if(symbol == '' || symbol == null) return;
	sel = designer.document.selection.createRange();
	sel.pasteHTML(symbol);
}

/// <summary>
/// 插入表格。
/// </summary>
function HtmlEditor_InsertTable(designer,designerView, dialogsPath) {
	if (designerView != 'DesignView') return;
	designer.focus();

	var url = dialogsPath + 'table.htm';
	tableArr = showModalDialog(url, designer, 'dialogWidth:360px; dialogHeight:380px;help:0;status:0;resizeable:1;');

	if (tableArr != null) {
		var newTable = designer.document.createElement('TABLE');
		for(y=0; y<tableArr['rows']; y++) {
			var newRow = newTable.insertRow();
			for(x=0; x<tableArr['cols']; x++) {
				var newCell = newRow.insertCell();				
				if (tableArr['valigncells'] != '') {
					newCell.valign = tableArr['valigncells'];
				}
				if (tableArr['haligncells'] != '') {
					newCell.align = tableArr['haligncells'];
				}				
				if (tableArr['percentcols'] == true) {					
					newCell.width = Math.round((1 / tableArr['cols']) * 100) + '%';
				}					
			}
		}
		newTable.border = tableArr['border'];
		newTable.cellspacing = tableArr['cellspacing'];
		newTable.cellpadding = tableArr['cellpadding'];		
		if (tableArr['width'] != '') newTable.width = tableArr['width'];
		if (tableArr['height'] != '') newTable.height = tableArr['height'];

		if (designer.document.selection.type=='Control') {
			sel.pasteHTML(newTable.outerHTML);
		} else {
			sel = designer.document.selection.createRange();
			sel.pasteHTML(newTable.outerHTML);
		}
	}
}

/// <summary>
/// TODO:插入当前时间。
/// </summary>
function HtmlEditor_InsertTime(designer,designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	var d = new Date();
	sel = designer.document.selection.createRange();
	sel.pasteHTML(d.toLocaleTimeString());
}

/// <summary>
/// 斜体。
/// </summary>
function HtmlEditor_Italic(designer,designerView) { 
	HtmlEditor_Format(designer,designerView,'italic'); 
}

/// <summary>
/// 居中对齐。
/// </summary>
function HtmlEditor_JustifyCenter(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'justifycenter'); 
}

/// <summary>
/// 两端对齐。
/// </summary>
function HtmlEditor_JustifyFull(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'justifyfull'); 
}

/// <summary>
/// 左对齐。
/// </summary>
function HtmlEditor_JustifyLeft(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'justifyleft'); 
}

/// <summary>
/// 右对齐。
/// </summary>
function HtmlEditor_JustifyRight(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'justifyright'); 
}

/// <summary>
/// 数字项目列表。
/// </summary>
function HtmlEditor_NumberedList(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'insertorderedlist'); 
}

/// <summary>
/// 减少缩进。
/// </summary>
function HtmlEditor_Outdent(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'outdent'); 
}

/// <summary>
/// 粘贴。
/// </summary>
function HtmlEditor_Paste(designer,designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('paste','',null);
}

/// <summary>
/// 预先格式化的内容，追加<pre></pre>标签。一般HTML代码中的空格会被“过滤”，并不显示出来。
/// 而pre标签内的所有内容会完全按照在原代码内格式显示，包括空格、制表符（tab）等。
/// 所以这个pre标签一般用于显示有嵌套缩进的程序代码等。
/// </summary>
function HtmlEditor_PreFormatted(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('formatBlock','','<pre>');
}

/// <summary>
/// 打印。
/// </summary>
function HtmlEditor_Print(designer, designerView) { 
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('print','',null); 
}

/// <summary>
/// 删除线。
/// </summary>
function HtmlEditor_Strikethrough(designer, designerView) { 
	HtmlEditor_Format(designer, designerView, 'strikethrough'); 
}

/// <summary>
/// 下标。
/// </summary>
function HtmlEditor_Subscript(designer,designerView) { 
	HtmlEditor_Format(designer,designerView,'subscript'); 
}

/// <summary>
/// 上标。
/// </summary>
function HtmlEditor_Superscript(designer,designerView) { 
	HtmlEditor_Format(designer,designerView,'superscript'); 
}

/// <summary>
/// 重做。
/// </summary>
function HtmlEditor_Redo(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
 	designer.document.execCommand('redo', '', null);
}

/// <summary>
/// 清除格式。
/// </summary>
function HtmlEditor_RemoveFormat(designer, designerView, tempDesigner) { 
	HtmlEditor_Format(designer, designerView, 'removeFormat');
	
    var range       = designer.document.selection.createRange();
    if(range.text == '')
    {
        alert('请先选择文本。\n\n如果要清除整个编辑器内容的格式，请同时按Ctrl和A键选择所有文本');
        return;
    }
    
    var o = HtmlEditor_GetRangeReference(designer);
	if (o.tagName == 'IMG') {
		alert('不要选择图片，请选择文本。\n\n如果要清除整个编辑器内容的格式，请同时按Ctrl和A键选择所有文本');
		return;
	}
	
	designer.document.execCommand('copy', '', null);
	var html = HtmlEditor_GetClipboardData(tempDesigner);

	// 清除style和class
	html = html.replace(/<(\w[^>]*) class=([^ |>]*)([^>]*)/gi, "<$1$3") ;
	html = html.replace(/<(\w[^>]*) style="([^"]*)"([^>]*)/gi, "<$1$3") ;

    try {
	    if (designer.document.selection.type == 'Control') {
		    range.pasteHTML(html);
	    }
	    else {
		    range = designer.document.selection.createRange();
		    range.pasteHTML(html);
	    }
    }
    catch(err) {
        alert('请选择文本。');
        // err.name
        // err.message
    }
}

/// <summary>
/// 清除Scripts脚本。
/// </summary>
function HtmlEditor_RemoveScripts(designer, designerView) {
    var html;
    
    html = HtmlEditor_GetText(designer, designerView);
    html = html.replace(/<script[^>]*>[\w|\t|\r|\W]*<\/script>/gi,'');
    
    HtmlEditor_SetText(designer, designerView, html)
}

/// <summary>
/// 清除 Word 格式。
/// </summary>
function HtmlEditor_StripWordFormatting(designer, designerView) {
	designer.focus();
	var body = designer.document.body;
    // 清除 Word 样式
	for (var index = 0; index < body.all.length; index++) {
		tag = body.all[index]; // 兼容性？
		// if (tag.Attribute['className'].indexOf('mso') > -1)
		tag.removeAttribute('className', '',0);
		tag.removeAttribute('style', '',0);
	}

	var html = HtmlEditor_GetText(designer, designerView);
	html = StripWordFormatting(html);
	
	HtmlEditor_SetText(designer, designerView, html);
}

/// <summary>
/// 设定段落格式。
/// "<body>","<h1>","<h2>","<h3>","<h4>","<h5>","<h6>"
/// "正文","标题一","标题二","标题三","标题四","标题五","标题六"
/// </summary>
function HtmlEditor_Styles(designer, designerView, name, value) {
	if (designerView != 'DesignView') return;
	designer.focus();

	if (value == '<body>') {
		designer.document.execCommand('formatBlock','','Normal');
		designer.document.execCommand('removeFormat');
		return;
	}

	designer.document.execCommand('formatBlock','',value);
}

/// <summary>
/// TODO:单词总数。
/// </summary>
function HtmlEditor_SumWords(designer, designerView) {
    if (designerView != 'DesignView') return;
    var sum = 0;
    var rng = designer.document.body.createTextRange();
    rng.collapse(true);
    while(rng.move('word', 1)) {
	    sum++;
    }
    alert('大约  ' + sum + ' 字'); // 英文是以单词为单位进行统计
}

/// <summary>
/// 下划线。
/// </summary>
function HtmlEditor_Underline(designer,designerView) { 
	HtmlEditor_Format(designer,designerView,'underline'); 
}

/// <summary>
/// 撤销。
/// </summary>
function HtmlEditor_Undo(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('undo', '', null);
}

/// <summary>
/// 移除超链接。
/// </summary>
function HtmlEditor_Unlink(designer, designerView) {
	if (designerView != 'DesignView') return;
	designer.focus();
    designer.document.execCommand('unlink', '1', null);
}

/// <summary>
/// 设定字体。
/// </summary>
function HtmlEditor_FontFaces(designer, designerView, name, value) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('fontname', '', value);
}


/// <summary>
/// 设定字号。
/// </summary>
function HtmlEditor_FontSizes(designer, designerView, name, value) {
	if (designerView != 'DesignView') return;
	designer.focus();
	
    formatFont(designer, 'fontsize', value);
	//var v = parseInt(value) / 6;
	//designer.document.execCommand('fontsize', '', v);
	//obj.style.fontSize = v;
}

        
// formatFont("fontsize", this.getAttribute("value"));
function formatFont(designer, what, v){
	var IsVista = false;
	var IsIE = (navigator.appName == "Microsoft Internet Explorer");
    if(IsIE) {
	    if(navigator.userAgent.indexOf("Opera") > -1) IsIE = null;
	    
	    if(navigator.userAgent.indexOf("Windows NT 6.0") > -1) IsVista = true;
    }
    else {
	    if(navigator.userAgent.indexOf("Gecko")==-1) IsIE = null;
    }

    if(IsIE) {
        if(v == "楷体" && !IsVista) {
            v = "楷体_GB2312";
        }
        
        designer.document.execCommand("fontname", "", "HtmlEditor_Temp_FontName");
        if(!designer.document.body.keyupEvents)
        designer.document.body.keyupEvents = new Array();
        if(designer.document.selection.type!="Text") { // 如果没有选择文本，则是NONE
            designer.document.body.keyupEvents[what] = v;
            designer.document.body.onkeyup = function() {
                var es = designer.document.body.keyupEvents;
                if(es.fontname){
                    reaplceFontName("fontname", es.fontname)
                }
            }
        }
        else{
            reaplceFontName(designer, what, v);
        }
    }
    else {
        switch(what){
        case "fontname":
            designer.document.execCommand("fontname", "", v);
            break;
        case "fontsize":
            v = parseInt(v) / 6;
            designer.document.execCommand("fontsize", "", v);
            break;
        default:
            break;
        }
    }
}

function reaplceFontName(designer, what, v){
    var idocument = designer.document;
    var es = designer.document.body.keyupEvents;
    var a_Font = designer.document.body.getElementsByTagName("font");
    for (var i = 0; i < a_Font.length; i++){
        var o_Font = a_Font[i];
        if (o_Font.getAttribute("face") == "HtmlEditor_Temp_FontName"){
            delInFont(o_Font, what);
            setStyleValue(o_Font, what, v);
            es[what]=null;
            if(!es.fontsize && !es.fontname)
                o_Font.removeAttribute("face");
        }
    }
}
         
function delInFont(obj, what){
    var o_Children = obj.children;
    for (var j = 0; j < o_Children.length; j++){
        setStyleValue(o_Children[j], what, "");
        delInFont(o_Children[j], what);

        if (o_Children[j].style.cssText == ""){
            if ((o_Children[j].tagName == "FONT") || (o_Children[j].tagName == "SPAN")){
                o_Children[j].outerHTML = o_Children[j].innerHTML;
            }
        }
    }
}
		
function setStyleValue(obj, what, v){
	switch(what){
		case "fontname":
			obj.style.fontFamily = v;
			break;
		case "fontsize":
			//alert(v);
			obj.style.fontSize = v;
			break;
		default:
			break;
	}
}

/// <summary>
/// 设定字体颜色。
/// </summary>
function HtmlEditor_FontColors(designer, designerView, name, value) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('forecolor', '', value);
}

/// <summary>
/// 设定文本高亮颜色。
/// </summary>
function HtmlEditor_TextHighlightColors(designer, designerView, name, value) {
	if (designerView != 'DesignView') return;
	designer.focus();
	designer.document.execCommand('backcolor', '', value);
}