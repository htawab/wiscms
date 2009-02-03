
//Global variable that keeps track of all ASB 
//javascript objects on the page
var asbArray;
var disableMessage = false;

function asbAddObj(sTextBoxID, oJSAutoSuggestBox)
{
	if (typeof(asbArray) == "undefined")
		asbArray=new Array();
			
	asbArray[sTextBoxID]=oJSAutoSuggestBox;
}


function asbGetObj(sTextBoxID)
{
	return asbArray[sTextBoxID];
}

function findPos(obj) {
	var curleft = curtop = 0;
	if (obj.offsetParent) {
		curleft = obj.offsetLeft
		curtop = obj.offsetTop
		while (obj = obj.offsetParent) {
			curleft += obj.offsetLeft
			curtop += obj.offsetTop
		}
	}
	return [curleft,curtop];
}
///////////////////////////////////////////////////
//Class that stores all auto suggest box properties
///////////////////////////////////////////////////

function JSAutoSuggestBox()
{	
	//Class properties
	var msTextBoxID;
	var msMenuDivID;	
	var mnMaxSuggestChars;
	var mnKeyPressDelay;	
	var msMenuCSSClass;
	var msMenuItemCSSClass;
	var msSelMenuItemCSSClass;
	var mbUseIFrame;
	var MenuWidth;
	var ControlID;
    var HiddenTextBoxID;    
	var BlankPageURL;
	var currentValue = '';
	var AutoCallBack;
	
	var mbHasFocus;


	//Internal attributes
	var mnSelMenuItem = 0;	
	var mbCancelSubmit;
	var msOldTextBoxValue="";
	

	//Class methods
	this.GetKey				=GetKey;
	this.GetTextBoxCtrl		=GetTextBoxCtrl;
	this.GetMenuDiv			=GetMenuDiv;
		
	this.GetDataFromServer	=GetDataFromServer;
	
	this.SetSelectedValue	=SetSelectedValue;
	this.SetTextBoxValue	=SetTextBoxValue;
	this.GetTextBoxValue	=GetTextBoxValue;
	
    this.OnMouseClick		=OnMouseClick;
	this.OnMouseOver		=OnMouseOver;
 
 	this.OnKeyDown			=OnKeyDown;
	this.OnKeyPress			=OnKeyPress;
	this.OnKeyUp			=OnKeyUp;
    
    this.OnBlur				=OnBlur;
    
    this.GetSelMenuItemDiv	=GetSelMenuItemDiv;
    this.GetMenuItemDivID	=GetMenuItemDivID;
    this.GetMenuItemDiv		=GetMenuItemDiv;
    
    this.MoveUp				=MoveUp;
    this.MoveDown			=MoveDown;
    
    this.SelectMenuItem		=SelectMenuItem;
    this.UnselectMenuItem	=UnselectMenuItem;
    
    this.IsVisibleMenuDiv	=IsVisibleMenuDiv;
	this.MoveMenuDivIfAbsolutePos	=MoveMenuDivIfAbsolutePos;
	this.ShowMenuDiv		=ShowMenuDiv;
	this.HideMenuDiv		=HideMenuDiv;
		

	//Detects what key was pressed
	function GetKey(evt)
	{
		evt = (evt) ? evt : (window.event) ? event : null;
		if (evt)
		{
			var cCode = (evt.charCode) ? evt.charCode :
					((evt.keyCode) ? evt.keyCode :
					((evt.which) ? evt.which : 0));
			return cCode; 
		}
	}
	
	
	function GetTextBoxCtrl()
	{
		return document.getElementById(this.msTextBoxID);
	}
	
	
	function GetMenuDiv()
	{
		return document.getElementById(this.msMenuDivID);
	}
	
	
    function GetDataFromServer(sValue)
	{
	    disableMessage = true;
	    
        Anthem_InvokeControlMethod(
            this.ControlID,
            'GetSuggestions',
            [sValue],
            function(result){ disableMessage = false; }
        );
	}	
	
	
	function SetSelectedValue(sValue)
	{
		//TRACE("SetSelectedValue: " + sValue);
	
		var hdnSelectedValue=document.getElementById(this.HiddenTextBoxID);
		hdnSelectedValue.value=sValue;
	}

    function GetSelectedValue()
	{
		var hdnSelectedValue=document.getElementById(this.HiddenTextBoxID);
		return hdnSelectedValue.value;
	}

	function SetTextBoxValue()
	{
		var divMenuItem=this.GetSelMenuItemDiv();
			
		if(divMenuItem)
		{
			var sValue=divMenuItem.getAttribute('value');
			TRACE("SetTextBoxValue : Set selected item to " + sValue);
			
			var textboxDisplay = divMenuItem.getAttribute('textboxdisplay');
		
			//Set selected value of control to the value of selected menu item
			this.SetSelectedValue(sValue);
				
			var txtCtrl=this.GetTextBoxCtrl();
			txtCtrl.value = textboxDisplay; //GetInnerHtml(divMenuItem, sValue);
		}
	}


	function GetTextBoxValue()
	{
		var txtCtrl=this.GetTextBoxCtrl();
		return(txtCtrl.value);
	}
	
	
				
	function OnMouseClick(nMenuIndex)
	{
		this.mnSelMenuItem=nMenuIndex;
	
		this.SetTextBoxValue();
		this.HideMenuDiv();
	}
	


	function OnMouseOver(nMenuIndex)
	{
		this.SelectMenuItem(nMenuIndex);
	}
	
	
		
	function OnKeyDown(evt)
	{
		//TRACE("OnKeyDown : " + this.GetKey(evt) + ", " + this.msTextBoxID);
	
		//Indicate that control has focus
		this.mbHasFocus=true;
		
		//Save current text box value before key press takes affect
		this.msOldTextBoxValue=this.GetTextBoxValue();
		//TRACE("OnKeyDown : old text box value='" + this.msOldTextBoxValue + "'");
		
		var nKey;
		nKey=this.GetKey(evt);
				
		//TRACE("OnKeyDown : Key is " + nKey);
				
		//Detect if the user is using the down button
		if(nKey==38) //Up arrow
		{
			this.MoveDown()
		}
		else if(nKey==40) //Down arrow
		{
			this.MoveUp()
		}
		else if(nKey==13) //Enter
		{
			//TRACE("OnKeyDown : IsVisibleMenuDiv - " + this.IsVisibleMenuDiv());
			if (this.IsVisibleMenuDiv())
			{
				this.HideMenuDiv();
				
				evt.cancelBubble = true;
				
				if (evt.returnValue) evt.returnValue = false;
				if (evt.stopPropagation) evt.stopPropagation();
				
				this.mbCancelSubmit=true;
     		}
     		else
     		{
     			this.mbCancelSubmit=false;
     		}
		}
		else
		{
			this.HideMenuDiv();
		}
				
		return true;
	}
	
	
	function OnKeyPress(evt)
	{
		//TRACE("OnKeyPress : " + this.GetKey(evt));
		if ((this.GetKey(evt)==13) && (this.mbCancelSubmit)) 
		{
			return false;
		}
			
		return true;
	}
	
	
	function OnKeyUp(evt)
	{
		var nKey;
		nKey=this.GetKey(evt);
		
		//TRACE("OnKeyUp : " + nKey);
		
		
		//Skip up/down/enter
		if ((nKey!=38) && (nKey!=40) && (nKey!=13))
		{
			var sNewValue;
			sNewValue=this.GetTextBoxValue();
			
			//Limit num of characters to display suggestions	
			if ((sNewValue.length <= this.mnMaxSuggestChars) && (sNewValue.length > 0))
			{
				//TRACE("OnKeyUp : Getting data for '" + sNewValue + "'");
				
				var divMenu = this.GetMenuDiv();
				if (divMenu.timer) window.clearTimeout(divMenu.timer);
				
				//Add escape char to single quote
				sNewValue=sNewValue.replace(/\'/, "\\\'");
				
				//Set timer to update div.  If user types quickly return suggestions when he stops.  
				var sFunc="asbGetObj('" + this.msTextBoxID + "').GetDataFromServer('" + sNewValue + "')";
				//TRACE("OnKeyUp : " + sFunc);
							
				divMenu.timer = window.setTimeout(sFunc, this.mnKeyPressDelay);
			}
		
		
			if (this.msOldTextBoxValue!=sNewValue)
			{
				this.SetSelectedValue("");
			}
		}
	}
		
	
	function OnBlur()
	{
		//TRACE("OnBlur");
	
		this.HideMenuDiv();
		this.mbHasFocus=false;
				
		var selectedValue = document.getElementById(this.HiddenTextBoxID).value;
		if(this.AutoCallBack == true && this.currentValue != selectedValue)
		{
		      Anthem_InvokeControlMethod(
                this.ControlID,
                'FireSelectedValueChangedEvent',
                [selectedValue],
                function(result){ }
            );
        }
        this.currentValue = selectedValue;
	}
	
		
	function GetSelMenuItemDiv()
	{
		return this.GetMenuItemDiv(this.mnSelMenuItem);
	}
			
			
	function GetMenuItemDivID(nMenuItem)
	{
		return (this.msTextBoxID + "_mi_" + nMenuItem);
	}
	
		
	function GetMenuItemDiv(nMenuItem)
	{
		var sDivMenuItemID=this.GetMenuItemDivID(nMenuItem);
		return document.getElementById(sDivMenuItemID)
	}
		

	function MoveUp()
	{
		var nMenuItem;
		nMenuItem=this.mnSelMenuItem+1;
		//TRACE('Item com o foco no moveup: ' + nMenuItem);
		//Check if menu item exists
		if(this.GetMenuItemDiv(nMenuItem))
		{
			this.SelectMenuItem(nMenuItem)
		}
	}


	function MoveDown()
	{
		var nMenuItem;
		nMenuItem=this.mnSelMenuItem-1;
		
		if(nMenuItem!=0)
		{
			this.SelectMenuItem(nMenuItem)
		}
	}


	//Highlights a div
	function SelectMenuItem(nMenuItem)
	{
		var divMenuItem=this.GetMenuItemDiv(nMenuItem)
					
		if(divMenuItem)
		{
			if (nMenuItem!=this.mnSelMenuItem)
			{
				this.UnselectMenuItem();
				
				this.mnSelMenuItem=nMenuItem;
				this.SetTextBoxValue();
						
				divMenuItem.className=this.msSelMenuItemCSSClass;
			}
		}
	}


	//unhighlights a div
	function UnselectMenuItem()
	{
		var divMenuItem=this.GetSelMenuItemDiv()
	
		if(divMenuItem)
		{
			divMenuItem.className=this.msMenuItemCSSClass;
		}
	}


	
	function IsVisibleMenuDiv()
	{
		if (this.GetMenuDiv().style.visibility == 'hidden')
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	
	
	
	function MoveMenuDivIfAbsolutePos()
	{
		var txtCtrl=this.GetTextBoxCtrl();
		var divMenu=this.GetMenuDiv();
		
		if (txtCtrl.style.position != "absolute" && txtCtrl.style.position != "")
			return;
			
		//TRACE("MoveMenuDivIfAbsolutePos Moving absolute");
	    
		divMenu.style.position = "absolute"; //"fixed";
		//Move menu right under text box --> Estava dando problema no IE7
		
		var xy = findPos(txtCtrl);
		
		divMenu.style.left = xy[0] + "px";//   (txtCtrl.offsetLeft + parseInt(window.document.body.leftMargin)) + "px";
		divMenu.style.top	= xy[1] + 20 + "px";// (txtCtrl.offsetTop + txtCtrl.offsetHeight + parseInt(window.document.body.topMargin)) + "px";
		
		
	}
		
	
	function ShowMenuDiv(sDivContent)
	{
	    //if(!IsIE())
		    this.MoveMenuDivIfAbsolutePos();
		
		//TRACE("ShowMenuDiv : " + this.msTextBoxID);
	
		var divMenu=this.GetMenuDiv();
		var sInnerHtml;
		
        //divMenu.style.width = this.GetTextBoxCtrl().offsetWidth + 'px';
                
		//Use IFrame of the same size as div		
		if (IsIE() && this.mbUseIFrame) 
		{
			sInnerHtml = "<div id='" + this.msMenuDivID + "_content'>";
			sInnerHtml += sDivContent;
			sInnerHtml += "</div>";
			
			var sBlankPage=this.BlankPageURL;	//Use blank page to hide 'nonsecure items' message in IE when using HTTPS
			sInnerHtml += "<iframe id='" + this.msMenuDivID + "_iframe' src='" + sBlankPage  + "' frameborder='1' scrolling='no'></iframe>";
		}
		else
		{
			sInnerHtml=sDivContent;
		}
		
		
		divMenu.innerHTML = sInnerHtml;
		
		
		if (IsIE() && this.mbUseIFrame) 
		{
			var divContent;
			divContent=document.getElementById(this.msMenuDivID + "_content");
			
			var divIframe;
			divIframe=document.getElementById(this.msMenuDivID + "_iframe");
			
					
			//Remember display type
			divContent.className=this.msMenuCSSClass;
			divMenu.className="asbMenuBase";
				
			divIframe.style.width = divContent.offsetWidth + 'px';
			divIframe.style.height = divContent.offsetHeight + 'px';
			divIframe.marginTop = "-" + divContent.offsetHeight + 'px';
					
		}	
		
		divMenu.style.visibility = 'visible';
	}
	

				
	function HideMenuDiv()
	{
		this.GetMenuDiv().style.visibility = 'hidden';
		this.mnSelMenuItem=0;
	}
	
	
	//Utility functions	(don't need this.)
	function IsIE()
	{
		return ( navigator.appName=="Microsoft Internet Explorer" ); 
	}
	
	
	function TRACE(sText)
	{
	    return;
		var txtTRACE=document.getElementById("txtASBTRACE");
		if (txtTRACE!=null)
			txtTRACE.value = txtTRACE.value + sText + "\n";
	}
}

