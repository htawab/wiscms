function OpenFull(url){
	var w=window.open("","","scrollbars");
	if(document.all){
		w.moveTo(0,0);
		w.resizeTo(screen.width,screen.height);
	}
	w.location=url;
}

$ = function(i){
     if(!document.getElementById) return false;
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
}