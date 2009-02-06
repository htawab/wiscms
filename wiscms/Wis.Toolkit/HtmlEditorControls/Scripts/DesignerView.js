/// <summary>
/// 设定编辑器的视图状态:设计视图、源代码视图、纯文本视图、预览视图。
/// </summary>
function HtmlEditor_ChangeDesignerView(designer, designerView, toolbars, currentDesignerView, switchMode, designViewCss, sourceViewCss, plainTextViewCss, previewViewCss) {
	if (designerView == currentDesignerView) return false;

	var content;
	var theBody = '';
	switch (designerView){
		case 'DesignView':
			if (currentDesignerView == 'DesignView' || currentDesignerView == 'PreviewView')
				content = designer.document.body.innerHTML;
			else
				content = designer.document.body.innerText;

            // 外部样式
            if (designViewCss != '' || sourceViewCss != '' || plainTextViewCss != '' || previewViewCss != '')
            {
				// 设定外部样式
                designer.document.styleSheets[0].disabled = false;
                designer.document.styleSheets[1].disabled = true;
                designer.document.styleSheets[2].disabled = true;
                designer.document.styleSheets[3].disabled = true;
            }

            if (designer.DesignViewCss == "")
            {
			    designer.document.body.style.fontFamily = '';
			    designer.document.body.style.fontSize = '';
            }

            // 将工具条显示
		    if (toolbars != null) {
			    toolbars.style.display = 'inline';
		    }

		    designer.document.body.innerHTML = content;				
		    HtmlEditor_Scrollbar_Styles(designer); // TODO:不用每次执行？
		    designer.focus(); 

		    break;
		case 'PlainTextView': // 纯文本视图
            if(switchMode == 'Normal')
            {
	            if (!confirm('警告！切换到纯文本视图将丢失所有格式，您确认切换到纯文本视图吗？')){
		            return false;
	            }
	        }
	        
			if (currentDesignerView == "DesignView" || currentDesignerView == "PreviewView")
				content = designer.document.body.innerHTML;
			else
				content = designer.document.body.innerText;

			designer.document.body.innerHTML=content;
			HtmlEditor_Scrollbar_Styles(designer);
			designer.focus(); 

		    // 全选文本，清理格式
		    designer.document.execCommand('SelectAll');
		    designer.document.execCommand('removeFormat','',null);
		    designer.focus();
		    designer.document.body.innerHTML = designer.document.body.innerText;

            // 设定外部样式
            if (plainTextViewCss != '' || sourceViewCss != '' || designViewCss != '' || previewViewCss != '')
            {
                designer.document.styleSheets[0].disabled = true;
                designer.document.styleSheets[1].disabled = true;
                designer.document.styleSheets[2].disabled = false;
                designer.document.styleSheets[3].disabled = true;
            }
            
            if (toolbars != null) {
				toolbars.style.display = 'none';
			}
			
		    break;
		case 'SourceView': // 源代码视图
			if (currentDesignerView == 'DesignView' || currentDesignerView == 'PreviewView')
				content = designer.document.body.innerHTML;
			else
				content = designer.document.body.innerText;

            // remove window.location
            content=RemoveLocation(content);
            // 设定外部样式
            if (designViewCss != '' || sourceViewCss != '' || plainTextViewCss != '' || previewViewCss != '')
            {
                designer.document.styleSheets[0].disabled = true;
                designer.document.styleSheets[1].disabled = false;
                designer.document.styleSheets[2].disabled = true;
                designer.document.styleSheets[3].disabled = true;
            }

            if (sourceViewCss == '')
            {
			    designer.document.body.style.fontFamily = '宋体';
			    designer.document.body.style.fontSize = '10pt';
            }

            // 将工具条隐藏
			if (toolbars != null) {
				toolbars.style.display = 'none';
			}

			designer.document.body.innerText = content;

			break;
        case 'PreviewView':
		    if (currentDesignerView == 'DesignView' || currentDesignerView == 'PreviewView')
			    content = designer.document.body.innerHTML;
		    else
			    content = designer.document.body.innerText;

            if (designer.PreviewViewCss == '')
            {
		        designer.document.body.style.fontFamily = '';
		        designer.document.body.style.fontSize = '';
            }

            // 设定外部样式
            if (plainTextViewCss != '' || sourceViewCss != '' || designViewCss != '' || previewViewCss != '')
            {
                designer.document.styleSheets[0].disabled = true;
                designer.document.styleSheets[1].disabled = true;
                designer.document.styleSheets[2].disabled = true;
                designer.document.styleSheets[3].disabled = false;
            }

            // 预览状态隐藏工具条
			if (toolbars != null) {
				toolbars.style.display = 'none';
			}

			designer.document.body.innerHTML = content;		
			HtmlEditor_Scrollbar_Styles(designer);
			designer.focus();

			break;
	} // End switch

    return true;

} // End Function
// currentDesignerView = designerView; // 替换为新的视图

function HtmlEditor_DesignerView_TabOn(td, tdDesignView, tdSourceView, tdPlainTextView, tdPreviewView) {
    tdDesignView.className = 'HtmlEditor_DesignView_TabOff';	
    tdSourceView.className = 'HtmlEditor_DesignView_TabOff';	
    tdPlainTextView.className = 'HtmlEditor_DesignView_TabOff';	
    tdPreviewView.className = 'HtmlEditor_DesignView_TabOff';	

	td.className = 'HtmlEditor_DesignView_TabOn';
}

// <summary>
// 改变编辑区高度
// </summary>
function HtmlEditor_ChangeSize(designerId, size, originalHeight){
    
    // height自适应
    // var firefoxVersion = navigator.userAgent.substring(navigator.userAgent.indexOf("Firefox")).split("/")[1];
    // if (designer.contentDocument && designer.contentDocument.body.offsetHeight){   
    //   designer.height = designer.contentDocument.body.offsetHeight + (firefoxVersion >= 0.1? 16 : 0);   // ns6 syntax  
    // }   
    // else if (designer.Document && designer.Document.body.scrollHeight){   
    //   designer.height = designer.Document.body.scrollHeight;   //ie5+ syntax 
    // }
    
    //var BrowserInfo = new Object() ; // 浏览器版本检测
    //BrowserInfo.MajorVer = navigator.appVersion.match(/MSIE (.)/)[1] ;
    //BrowserInfo.MinorVer = navigator.appVersion.match(/MSIE .\.(.)/)[1] ;
    //BrowserInfo.IsIE55OrMore = BrowserInfo.MajorVer >= 6 || ( BrowserInfo.MajorVer >= 5 && BrowserInfo.MinorVer >= 5 ) ;

    //if (!BrowserInfo.IsIE55OrMore){
	//    alert('此功能需要IE5.5版本以上的支持！');
	//    return false;
    //}
    var designer = getElementById(designerId);
    if (designer)
    {
        var newHeight = parseInt(designer.height) + parseInt(size);
        if(newHeight < originalHeight)
        {
            alert('编辑区高度不能再缩小');
            return;
        }
        
        designer.height = newHeight; // ? Supported in Mozilla Firefox
        
        //for (var index=0; index < parent.frames.length; index++){
	    //    if (parent.frames[index].document == designer.document){
		//        var o = parent.frames[index].frameElement;
		//        var height = parseInt(o.offsetHeight);
        //
		//        if (height + size >= 300){
		//	        o.height = height + size;
		//        }
		//        break;
	    //    }
        //}        
    }
        
    // TODO:可以让用户输入一个值，根据该值进行调整    
}
