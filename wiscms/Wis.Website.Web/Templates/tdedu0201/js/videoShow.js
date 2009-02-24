// JavaScript Document
marqueesHeight=255;
stopscroll=false;
var scrollOut=document.getElementById("scrollOut");
with(scrollOut){
	  //style.width=500+"px";
	  style.height=marqueesHeight;
	  style.overflowX="visible";
	  style.overflowY="hidden";
	  noWrap=true;
	  onmouseover=new Function("stopscroll=true");
	  onmouseout=new Function("stopscroll=false");
  }
  preTop=0; currentTop=marqueesHeight; stoptime=0;
  scrollOut.innerHTML+=scrollOut.innerHTML;
  

function init_srolltext(){
  scrollOut.scrollTop=0;
  setInterval("scrollUp()",1);
}init_srolltext();

function scrollUp(){
  if(stopscroll==true) return;
  currentTop+=1;
  if(currentTop==marqueesHeight+1)
  {
  	stoptime+=1;
  	currentTop-=1;
  	if(stoptime==300) 
  	{
  		currentTop=0;
  		stoptime=0;  		
  	}
  }
  else {  	
	  preTop=scrollOut.scrollTop;
	  scrollOut.scrollTop+=1;
	  if(preTop==scrollOut.scrollTop){
	    scrollOut.scrollTop=marqueesHeight;
	    scrollOut.scrollTop+=1;
	    
	  }
  }

}
init_srolltext();