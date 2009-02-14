﻿<!--
var upLoadBox = {
    // 根据ID获得DOM节点
    $: function(i){
	     if(!document.getElementById)return false;
	     if(typeof i==='string'){
	   	     if(document.getElementById && document.getElementById(i)) {// W3C DOM
	              return document.getElementById(i);
           }
           else if (document.all && document.all(i)) {// MSIE 4 DOM
	              return document.all(i);
           }
           else if (document.layers && document.layers[i]) {// NN 4 DOM.. note: this won't find nested layers
	              return document.layers[i];
           } 
           else {
	              return false;
           }
	     }
	     else{return i;}
    },
    
    pageHeight: 0,
    yScroll: 0,
    xScroll: 0,
    marginTop: 0,
    
    oShadow: null,        // 遮照层
    oUpLoadWindow: null,   // 提示框层
    oBtnEnter: null,      // 提示框的确定按钮
    oBtnClose: null,      // 提示框的关闭按钮    
    
    strTitle: null,     // 提示框的标题
    strMessage: null,     // 提示框的提示内容
    bSlideWindow: false,  // 是否渐变显示提示框，默认渐变
    iWindowIcon: 1,       // 弹出窗口图标的风格
    
    init: function(title, message, bSlide){
	    this.strTitle = title;
	    this.strMessage = message;
	    this.bSlideWindow=bSlide?false:true;
    	   
	    var oWin=this.$('window');
        var oSha=this.$('shadow');
	    if(oSha && oWin) this.closeDiv();
        window.onload = function(){
            upLoadBox.createDiv();
            upLoadBox.setEvent();
        }
    }, 
    
    createDiv: function(obj){
		               
	   this.pageHeight=this.getPageHeight();
	   this.yScroll=this.getPageScroll()[1];
	   this.xScroll=this.getPageScroll()[0];    	   
    	   
       var shadow = document.createElement('div');
       shadow.setAttribute('id','shadow'); 
       shadow.style.height = document.body.offsetHeight + 'px';

	       var obj=document.createElement('div');
	       obj.setAttribute('id','window');
	       obj.style.zIndex='999';
	       if(document.all){
			       if(window.opera){
			       	    obj.style.opacity = 0.1;
			       }
			       else{
			       	    obj.style.filter = 'alpha(opacity=10)';
			       }	    
		     }
		     else{
			       obj.style.opacity = 0.1;
		     }
		   var windowBoxIn = document.createElement('div');
		   windowBoxIn.setAttribute('id','windowBoxIn');
		   
	       var divTitle = document.createElement('div');
	       divTitle.setAttribute('id','windowTitle');
	   
	       var H2 = document.createElement('h2');
	   
	       var IMG=document.createElement('img');
	       IMG.setAttribute('src','win.png');
	       IMG.setAttribute('alt','Window-Icon');
	   
	       var txtTitle=document.createTextNode(this.strTitle);
	       
	       H2.appendChild(IMG); 
	       H2.appendChild(txtTitle);
	   
	       var closeBar=document.createElement('div');
	       closeBar.setAttribute('id','closebar');

	       var A = document.createElement('a');
	       A.innerHTML='关闭窗口';
	       A.setAttribute('href','#1');
	       A.setAttribute('id','btnclose');
	       A.setAttribute('title','关闭窗口');
	   
	       closeBar.appendChild(A);
	      
	 
	       divTitle.appendChild(H2);
	       divTitle.appendChild(closeBar);
		   
		   //widowContent
		   var windowContent = document.createElement('div');
		   windowContent.setAttribute('class','windowContent');
		   
		   var ulTab = document.createElement('ul');
		   ulTab.setAttribute('id','tab');
		   
		   var li1 =document.createElement('li');
		   var a1 = document.createElement('a');
		   a1.setAttribute('href','javascript:tabChange(1)')
		   a1.setAttribute('onfocus','this.blur()');
		   a1.setAttribute('id','tabNoBorder');
		   a1.setAttribute('title','从图片列表中选择');
		   var a1Text = document.createTextNode('从图片列表中选择');
		   a1.appendChild(a1Text);
		   li1.appendChild(a1);
		   
		   var li2 =document.createElement('li');
		   var a2 = document.createElement('a');
		   a2.setAttribute('href','javascript:tabChange(2)');
		   a2.setAttribute('onfocus','this.blur()');
		   a2.setAttribute('id','');
		   a2.setAttribute('title','从电脑上传');
		   var a2Text = document.createTextNode('从电脑上传');
		   a2.appendChild(a2Text);
		   
		   li2.appendChild(a2);
		   
		   ulTab.appendChild(li1);
		   ulTab.appendChild(li2);
		   windowContent.appendChild(ulTab);
		   
		   var box1 = document.createElement('div');
		   box1.setAttribute('id','box1');
		   
		   var picChoose = document.createElement('div');
		   picChoose.setAttribute('id','picChoose');
		   var SelectBox = document.createElement('div');
		   SelectBox.setAttribute('id','SelectBox');
		   var dropSele = document.createElement('select');
		   var selectValue = document.createElement('option');
		   var selectText = document.createTextNode('选择图片目录');
		   
		   selectValue.appendChild(selectText);
		   dropSele.appendChild(selectValue);
		   SelectBox.appendChild(dropSele);
		   
		   var allPics = document.createElement('div');
		   allPics.setAttribute('id','allPics');
		   
		   

		   for (i=1;i<11;i++) {
			   allPics.innerHTML += '<a href="####"><img src="'+i+'aa.png" onClick="picShow(this)" /></a>'
			   }

		   
		   
		   picChoose.appendChild(SelectBox);
		   picChoose.appendChild(allPics);
		   
		   
		   box1.appendChild(picChoose);
		   
		   var picShow = document.createElement('div');
		   picShow.setAttribute('id','picShow');
		   var imgBox = document.createElement('div');
		   imgBox.setAttribute('id','imgBox');
		   var showBox = document.createElement('img');
		   showBox.setAttribute('src','noimg.gif');
		   showBox.setAttribute('id','showBox');
		   
		   imgBox.appendChild(showBox);
		   picShow.appendChild(imgBox);
		   
		   
		   
		   box1.appendChild(picShow);
		   
		   var clearDiv = document.createElement('div');
		   clearDiv.setAttribute('id','clear');
		   box1.appendChild(clearDiv);
		   
		   
		   
		   windowContent.appendChild(box1);
		   
		   
		   
		   
		   var box2 = document.createElement('div');
		   box2.setAttribute('id','box2');
		   box2.innerHTML = '<div class="picupLoad"><input type="file" /></div>'
		   		   
		   windowContent.appendChild(box2);
		   
		  
		   
		   //widowContent over
		   
		   var btnOut = document.createElement('div');
		   btnOut.setAttribute('id','btns');
		   btnOut.innerHTML = '<a href="#"></a>'
		   
	      

	       var btnEnter=document.createElement('a');
	  

	       document.body.appendChild(shadow);
	       obj.appendChild(windowBoxIn);
		   windowBoxIn.appendChild(divTitle);
	       windowBoxIn.appendChild(windowContent);
		   windowBoxIn.appendChild(btnOut);
	       document.body.appendChild(obj);
	       
	       this.oUpLoadWindow=obj;
	       this.oShadow=shadow;
	       this.oBtnEnter=btnEnter;
	       this.oBtnClose=A;
	   },    
	   
	   setEvent: function(){
	       if(!this.oUpLoadWindow || !this.oShadow || !this.oBtnEnter || !this.oBtnClose) return false;
	       
	       this.marginTop=this.yScroll+(this.pageHeight-this.oUpLoadWindow.offsetHeight)/2;
	       
	       this.fixPos();
	       
	       if(this.bSlideWindow){
	       	    this.slideDiv();
	       }
	       else{
	            if(document.all){
			             if(window.opera){
			                 this.oUpLoadWindow.style.opacity = 1;
			             }
			             else{	
			                 this.oUpLoadWindow.style.filter = '';
			             }
		          }
		          else{
			             this.oUpLoadWindow.style.opacity = 1;
		          }
	       }
     
	       // 设置关闭和确定按钮的功能--关闭(移除)提示框       
         this.oBtnEnter.onclick=this.oBtnClose.onclick=function(){
              upLoadBox.closeDiv();                             	
         }	 
     },
    
     slideDiv: function(){
	       var i=10;
	       var j=0.1;
	       var _fliter_=function(){
	   	       if(document.all){
	   	    	       if(i>100 || j>1){
	   	    	            if(tt) tt=window.clearInterval(tt);
			                  if(window.opera){
			                       this.oUpLoadWindow.style.opacity = 1;
			                  }
			                  else{	
	   	    	   	             upLoadBox.oUpLoadWindow.style.filter ='';
	   	    	            }
	   	    	            return false;
	   	    	        }
			              if(window.opera){
			                  upLoadBox.oUpLoadWindow.style.opacity = j;
			                  j += 0.1;
			              }
			              else{	
	   	                  upLoadBox.oUpLoadWindow.style.filter = 'alpha(opacity='+i+')';
	   	    	            i += 10;
	   	              }    
	   	       }
	   	       else{
	   	    	        if(j>1){
	   	    	             if(tt) tt=window.clearInterval(tt);
	   	    	             upLoadBox.oUpLoadWindow.style.opacity=1;
	   	    	             return false;
	   	    	        }
			              upLoadBox.oUpLoadWindow.style.opacity = j;
			              j += 0.1;
	   	       }
	       }
	       var tt=window.setInterval(_fliter_,50);
     },

     closeDiv: function(){
        var selects = document.getElementsByTagName('select');
        for(index = 0; index < selects.length; index++){
	        selects[index].style.display = '';
        }
     
     	    if(this.oUpLoadWindow.style.filter=='' || this.oUpLoadWindow.style.opacity==1){
               document.body.removeChild(this.oUpLoadWindow); 
               document.body.removeChild(this.oShadow); 
          }  
     },
     
     fixPos: function(){
     	    this.oUpLoadWindow.style.top  = this.marginTop+'px';
	        this.oUpLoadWindow.style.left = (document.body.offsetWidth-500)/2 + 'px';	
     },
     
     getPageScroll: function(){
     	    var xScroll, yScroll;
          if (self.pageYOffset) {
               yScroll = self.pageYOffset;
               xScroll = self.pageXOffset;
          } else if (document.documentElement && document.documentElement.scrollTop) {	 
          	   // Explorer 6 Strict
               yScroll = document.documentElement.scrollTop;
               xScroll = document.documentElement.scrollLeft;
          } else if (document.body) {
          	   // all other Explorers
               yScroll = document.body.scrollTop;
               xScroll = document.body.scrollLeft;	
          }
          return new Array(xScroll,yScroll); 
     }, 
     
     getPageHeight: function(){
          var windowHeight
          if (self.innerHeight) {	
          	   // all except Explorer
               windowHeight = self.innerHeight;
          } else if (document.documentElement && document.documentElement.clientHeight) { 
          	   // Explorer 6 Strict Mode
               windowHeight = document.documentElement.clientHeight;
          } else if (document.body) { // other Explorers
               windowHeight = document.body.clientHeight;
          }	
          return windowHeight
     }  	
}

window.onresize=function(){
	   if(upLoadBox.oUpLoadWindow){
	   	    var yScroll = upLoadBox.getPageScroll()[1];
	   	    var pageHeight = upLoadBox.getPageHeight();
	   	    var marginTop = yScroll+(pageHeight-180)/2;
	        upLoadBox.marginTop= marginTop;
	        upLoadBox.fixPos();
	   }
}




//-->