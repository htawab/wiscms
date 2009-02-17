<!--
var annexUpLoadBox = {
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
    	   
	    var oWin=this.$('upLoadwindow');
        var oSha=this.$('shadow');
	    if(oSha && oWin) this.closeDiv();
	    
        //window.onload = function(){
           annexUpLoadBox.createDiv();
           annexUpLoadBox.setEvent();
        //}
    }, 
    
    createDiv: function(obj){
		               
	   this.pageHeight=this.getPageHeight();
	   this.yScroll=this.getPageScroll()[1];
	   this.xScroll=this.getPageScroll()[0];    	   
    	   
       var shadow = document.createElement('div');
       shadow.setAttribute('id','shadow'); 
       shadow.style.height = document.body.offsetHeight + 'px';

	       var obj=document.createElement('div');
	       obj.setAttribute('id','upLoadwindow');
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
	       IMG.setAttribute('src','../images/win.png');
	       IMG.setAttribute('alt','');
	   
	       var txtTitle=document.createTextNode(this.strTitle);
	       
	       H2.appendChild(IMG); 
	       H2.appendChild(txtTitle);
	   
	       var closeBar1=document.createElement('div');
	       closeBar1.setAttribute('id','closebar1');

	       var A = document.createElement('a');
	       A.innerHTML='关闭窗口';
	       A.setAttribute('href','#1');
	       A.setAttribute('id','btnclose1');
	       A.setAttribute('title','关闭窗口');
	   
	       closeBar1.appendChild(A);
	      
	 
	       divTitle.appendChild(H2);
	       divTitle.appendChild(closeBar1);
		   
		   //widowContent
		   var windowContent = document.createElement('div');
		   windowContent.setAttribute('class','windowContent');
		   windowContent.innerHTML='<iframe id="oIframe" name="oIframe" src="../images/annexUpLoad/upLoad.html" frameborder="0" scrolling="no"></iframe>'	   
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
              annexUpLoadBox.closeDiv();                             	
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
	   	    	   	             annexUpLoadBox.oUpLoadWindow.style.filter ='';
	   	    	            }
	   	    	            return false;
	   	    	        }
			              if(window.opera){
			                  annexUpLoadBox.oUpLoadWindow.style.opacity = j;
			                  j += 0.1;
			              }
			              else{	
	   	                  annexUpLoadBox.oUpLoadWindow.style.filter = 'alpha(opacity='+i+')';
	   	    	            i += 10;
	   	              }    
	   	       }
	   	       else{
	   	    	        if(j>1){
	   	    	             if(tt) tt=window.clearInterval(tt);
	   	    	             annexUpLoadBox.oUpLoadWindow.style.opacity=1;
	   	    	             return false;
	   	    	        }
			              annexUpLoadBox.oUpLoadWindow.style.opacity = j;
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
	   if(annexUpLoadBox.oUpLoadWindow){
	   	    var yScroll = annexUpLoadBox.getPageScroll()[1];
	   	    var pageHeight = annexUpLoadBox.getPageHeight();
	   	    var marginTop = yScroll+(pageHeight-180)/2;
	        annexUpLoadBox.marginTop= marginTop;
	        annexUpLoadBox.fixPos();
	   }
}


function tabChange(boxId) {
var Box1 = document.getElementById("box1");
var Box2 = document.getElementById("box2");
var tabIn = document.getElementById("tab").getElementsByTagName("a");


if (boxId==1) {
Box1.style.display="block";
Box2.style.display="none";
tabIn[0].id ="tabNoBorder";
tabIn[1].id ="";
}
if (boxId==2) {
Box1.style.display="none";
Box2.style.display="block";
tabIn[0].id ="";
tabIn[1].id ="tabNoBorder";
}

}

function picShow(obj) {
var picSrc = obj.src;
var showBox = document.getElementById("showBox");
if (picSrc != "") {
 showBox.src=obj.src;
 
 if (showBox.width>160) {
 showBox.style.width=160+"px";
 }
 else {
 showBox.style.width="";
 }
}
}


//-->