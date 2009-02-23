function OpenFull(url){
	var w=window.open("","","scrollbars");
	if(document.all){
		w.moveTo(0,0);
		w.resizeTo(screen.width,screen.height);
	}
	w.location=url;
}