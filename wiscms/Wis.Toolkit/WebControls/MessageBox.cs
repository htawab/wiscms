//------------------------------------------------------------------------------
// <copyright file="MessageBox.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Drawing;
using System.ComponentModel;

[assembly: TagPrefix("MessageBox", "WebControls")]

namespace Wis.Toolkit.WebControls
{
    [ToolboxBitmap(typeof(MessageBox), "DropdownMenu.bmp")]
    [System.Web.UI.ToolboxData("<{0}:DropdownMenu runat=server></{0}:DropdownMenu>")]
    [System.Web.UI.ValidationProperty("Text")]
    [System.ComponentModel.DefaultProperty("Text")]
    [System.ComponentModel.Designer(typeof(Wis.Toolkit.WebControls.HtmlEditorControls.HtmlEditorControlDesigner))]
    [System.ComponentModel.Description("Dropdown Menu")]
    public class MessageBox : System.Web.UI.WebControls.WebControl
    {
        /// <summary>
        /// 标题。
        /// </summary>
        public string Title
        {
            get { return ((string)(ViewState["Title"])); }
            set { ViewState["Title"] = value; }
        }

        /// <summary>
        /// 消息。
        /// </summary>
        public string Message
        {
            get { return ((string)(ViewState["Message"])); }
            set { ViewState["Message"] = value; }
        }

        /// <summary>
        /// 图片所在路径。
        /// </summary>
        public string ImagePath
        {
            get { return ((string)(ViewState["ImagePath"])); }
            set { ViewState["ImagePath"] = value; }
        }

                /// <summary>
        /// 引发 System.Web.UI.Control.PreRender 事件。
        /// </summary>
        /// <param _Name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnPreRender(EventArgs e)
        {
            string version = "<!--Copyright (C) Everwis Corporation.  All rights reserved. -->\r\n";
            base.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DropdownMenuCopyright", version);
            string scriptKey = "DropdownMenuScript";
            string scriptBlock = @"
<!--
var alertBox={
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
    oAlertWindow: null,   // 提示框层
    oBtnEnter: null,      // 提示框的确定按钮
    oBtnClose: null,      // 提示框的关闭按钮    
    
    strMessage: null,     // 提示框的提示内容
    bSlideWindow: false,  // 是否渐变显示提示框，默认渐变
    iWindowIcon: 1,       // 弹出窗口图标的风格
    
    init: function(strMsg, bSlide){
	    this.strMessage = strMsg;
	    this.bSlideWindow=bSlide?false:true;
    	   
	    var oWin=this.$('window');
        var oSha=this.$('shadow');
	    if(oSha && oWin) this.closeDiv();
        window.onload = function(){
            alertBox.createDiv();
            alertBox.setEvent();
        }
    }, 
    
    createDiv: function(obj){
        var selects = document.getElementsByTagName('select');
        for(index = 0; index < selects.length; index++){
	        selects[index].style.display = 'none';
        }
		               
	   this.pageHeight=this.getPageHeight();
	   this.yScroll=this.getPageScroll()[1];
	   this.xScroll=this.getPageScroll()[0];    	   
    	   
       var shadow = document.createElement('id');
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
		     
	       var divTitle = document.createElement('div');
	       divTitle.setAttribute('id','win-tl');
	   
	       var H2 = document.createElement('h2');
	   
	       var IMG=document.createElement('img');
	       IMG.setAttribute('src','win.png');
	       IMG.setAttribute('alt','Window-Icon');
	   
	       var txtTitle=document.createTextNode('标题 - 爱唱卡充值');
	       
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
	   
	       var titleRight=document.createElement('div');
	       titleRight.setAttribute('id','win-tr');	   
	 
	       divTitle.appendChild(H2);
	       divTitle.appendChild(closeBar);
	       divTitle.appendChild(titleRight);
	   
	       var Container = document.createElement('div');
	       Container.setAttribute('id','msg-content');
	   
	       var cntLeft=document.createElement('div');
	       cntLeft.setAttribute('id','msg-leftbar');
	   
	       var MSG=document.createElement('div');
	       MSG.setAttribute('id','msg');
	   
	       var INFO=document.createElement('div');
	       INFO.setAttribute('id','info');
	   
	       var H3 = document.createElement('h3');
	       H3.innerHTML='" + this.Title + @"';
	   
	       var P = document.createElement('p');
	       P.innerHTML='" + this.Message + @"';
	   
	       INFO.appendChild(H3);
	       INFO.appendChild(P);
	   
	       var Btns=document.createElement('div');
	       Btns.setAttribute('id','btns');
	   
	       var btnEnter=document.createElement('a');
	       btnEnter.setAttribute('id','btnok');
	       btnEnter.setAttribute('href','#1');
	   
	       var txtEnter=document.createTextNode('确定');
	   
	       btnEnter.appendChild(txtEnter);   
	       Btns.appendChild(btnEnter);
	   
	       MSG.appendChild(INFO);
	       MSG.appendChild(Btns);
	   
	       var cntRight=document.createElement('div');
	       cntRight.setAttribute('id','msg-rightbar');
	   
	       Container.appendChild(cntLeft);
	       Container.appendChild(MSG);
	       Container.appendChild(cntRight);   
	   

	       var msgBottom = document.createElement('div');
	       msgBottom.setAttribute('id','msg-bottom');
	   
	       var msgBLeft=document.createElement('div');
	       msgBLeft.setAttribute('id','msg-bottom-left');
	   
	       var msgBRight=document.createElement('div');
	       msgBRight.setAttribute('id','msg-bottom-right');

	       msgBottom.appendChild(msgBLeft);
	       msgBottom.appendChild(msgBRight);
	   
	       document.body.appendChild(shadow);
	       obj.appendChild(divTitle);
	       obj.appendChild(Container);
	       obj.appendChild(msgBottom);
	       document.body.appendChild(obj);
	       
	       this.oAlertWindow=obj;
	       this.oShadow=shadow;
	       this.oBtnEnter=btnEnter;
	       this.oBtnClose=A;
	   },    
	   
	   setEvent: function(){
	       if(!this.oAlertWindow || !this.oShadow || !this.oBtnEnter || !this.oBtnClose) return false;
	       
	       this.marginTop=this.yScroll+(this.pageHeight-this.oAlertWindow.offsetHeight)/2;
	       
	       this.fixPos();
	       
	       if(this.bSlideWindow){
	       	    this.slideDiv();
	       }
	       else{
	            if(document.all){
			             if(window.opera){
			                 this.oAlertWindow.style.opacity = 1;
			             }
			             else{	
			                 this.oAlertWindow.style.filter = '';
			             }
		          }
		          else{
			             this.oAlertWindow.style.opacity = 1;
		          }
	       }
     
	       // 设置关闭和确定按钮的功能--关闭(移除)提示框       
         this.oBtnEnter.onclick=this.oBtnClose.onclick=function(){
              alertBox.closeDiv();                             	
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
			                       this.oAlertWindow.style.opacity = 1;
			                  }
			                  else{	
	   	    	   	             alertBox.oAlertWindow.style.filter ='';
	   	    	            }
	   	    	            return false;
	   	    	        }
			              if(window.opera){
			                  alertBox.oAlertWindow.style.opacity = j;
			                  j += 0.1;
			              }
			              else{	
	   	                  alertBox.oAlertWindow.style.filter = 'alpha(opacity='+i+')';
	   	    	            i += 10;
	   	              }    
	   	       }
	   	       else{
	   	    	        if(j>1){
	   	    	             if(tt) tt=window.clearInterval(tt);
	   	    	             alertBox.oAlertWindow.style.opacity=1;
	   	    	             return false;
	   	    	        }
			              alertBox.oAlertWindow.style.opacity = j;
			              j += 0.1;
	   	       }
	       }
	       var tt=window.setInterval(_fliter_,50);
     },

     closeDiv: function(){
        var selects = document.getElementsByTagName('select');
        for(index = 0; index < selects.length; index++){
	        selects[index].style.display = 'block';
        }
     
     	    if(this.oAlertWindow.style.filter=='' || this.oAlertWindow.style.opacity==1){
               document.body.removeChild(this.oAlertWindow); 
               document.body.removeChild(this.oShadow); 
          }  
     },
     
     fixPos: function(){
     	    this.oAlertWindow.style.top  = this.marginTop+'px';
	        this.oAlertWindow.style.left = (document.body.offsetWidth-400)/2 + 'px';	
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
	   if(alertBox.oAlertWindow){
	   	    var yScroll = alertBox.getPageScroll()[1];
	   	    var pageHeight = alertBox.getPageHeight();
	   	    var marginTop = yScroll+(pageHeight-180)/2;
	        alertBox.marginTop= marginTop;
	        alertBox.fixPos();
	   }
}
//-->
";
            if (!base.Page.ClientScript.IsClientScriptBlockRegistered(scriptKey))
            {
                base.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptKey, scriptBlock);
            }
        }
    }
}
