G = function(i){
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

window.onload = function() {
    var left = G('left');
    var right = G('right');
    if(!left || !right) return;
    
    var leftHeight = left.offsetHeight;
    var rightHeight = right.offsetHeight;
    if (leftHeight > rightHeight)
        right.style.height = leftHeight+"px";
    else
        left.style.height = rightHeight+"px";
}
LRH = function() {
    var left = G('left');
    var right = G('right');
    if(!left || !right) return;
    left.style.height = "auto";
    var leftHeight = left.offsetHeight;
    var rightHeight = right.offsetHeight;
    if (leftHeight > rightHeight)
        right.style.height = leftHeight+"px";
    else
        left.style.height = rightHeight+"px";
}
