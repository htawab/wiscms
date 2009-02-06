
//JavaScript for Webb.WAVE.Controls.Upload.WebbUpload 2.0
//
//var m_currentSize;
//var m_totalSize;
//var m_currentFileName;
//var m_timeSpan;
//var m_avgSpeed;
//var m_uploadFinished;
//=========================
//o_status
//o_currentFile
//o_percent_1
//o_percent_2
//o_avgSpeed
//o_timeLeft
//o_but_ok
//o_but_cancel

function UpdateUploadStatus(){
	//alert("Upload start.");
	try{
		var m_percent						= m_currentSize*100/m_totalSize;
		if(m_percent<=0)m_percent=1;
		document.all.o_currentFile.innerHTML= "File Name: <font color=#CC3333>"+GetFileName()+"</font>";		
		document.all.o_percent_1.width		= m_percent+"%";
		document.all.o_percent_2.width		= (100-m_percent)+"%";
		document.all.o_avgSpeed.innerHTML	= "Speed: <font color=#CC3333>"+ConvertToFileSize(m_avgSpeed)+"/s</font>";
		document.all.o_status.innerHTML		= "Status: <font color=#CC3333>Uploading</font>";
		document.all.o_timeLeft.innerHTML	= "Time Remain: <font color=#CC3333>"+GetTimeReamin()+"</font>";		
	//	document.title						= m_percent+"% ---- Webb WAVE Uploading......";		
	}catch(ex){
		document.all.o_currentFile.innerHTML= "";
		document.all.o_percent_1.width		= "1";
		document.all.o_percent_2.width		= "100%";
		document.all.o_avgSpeed.innerHTML	= "Speed: <font color=#CC3333>Unknow</font>";
		document.all.o_status.innerHTML		= "Status: <font color=#CC3333>Unitialize</font>";
		document.all.o_timeLeft.innerHTML	= "Time Remain: <font color=#CC3333>Unknow</font>";
	//	document.title						= "0% ---- Webb WAVE Uploading......";
	}
	if(m_uploadFinished){
		document.all.o_currentFile.innerHTML= "File Name:";
		document.all.o_percent_1.width		= "100%";
		document.all.o_percent_2.width		= "1";
		document.all.o_avgSpeed.innerHTML	= "Speed:";
		document.all.o_status.innerHTML		= "Status: <font color=#CC3333>Finished. Moving or copying files.<br>Please wait or click 'OK' to close process bar.</font>";
		document.all.o_timeLeft.innerHTML	= "Time Remain:";
	}
	SetButtons();
}

function ConvertToFileSize(i_size){
	var m_rank				= 0;
	var m_decimal			= i_size;
	while(m_decimal/1024>1)
	{
		m_rank++;
		m_decimal	= m_decimal/1024;
	}
	m_decimal		= m_decimal.toString();
	m_decimal		= m_decimal.substring(0,m_decimal.indexOf(".")+3);
	//m_decimal		= Math.floor(m_decimal);
	var m_value;
	switch(m_rank)
	{
		default:
		case 0: m_value	= m_decimal+ " Bytes";	break;
		case 1: m_value	= m_decimal+ " KB";		break;
		case 2: m_value	= m_decimal+ " MB";		break;
		case 3: m_value	= m_decimal+ " GB";		break;
		case 4: m_value	= m_decimal+ " TB";		break;
		case 5: m_value	= m_decimal+ " EB";		break;
	}
	return m_value;
}

function GetFileName(){
	if(m_currentFileName.length>0){
		var i_start	= m_currentFileName.lastIndexOf("\\")+1;
		var i_end	= m_currentFileName.length-1;
		return m_currentFileName.substring(i_start,i_end)
	}
}

function GetTimeReamin(){
	var m_reaminSecond	= (m_totalSize-m_currentSize)/m_avgSpeed;
	return Math.floor(m_reaminSecond/3600)+ ":"+Math.floor((m_reaminSecond%3600)/60)+":"+Math.floor(m_reaminSecond%60);
}

function SetButtons(){
	if(m_currentSize<m_totalSize){
		document.all.o_but_ok.disabled		= true;
		document.all.o_but_cancel.disabled	= false;
	}else{
		document.all.o_but_ok.disabled		= false;
		document.all.o_but_cancel.disabled	= true;
	}
}

function OK_Click(){
//	alert("OK");
	window.opener=self;window.close();
	return false;
}

function Cancel_Click(){
//	alert("Cancel");
	if(CheckBrowser())	{
		dialogArguments.location.href=dialogArguments.location.href;
		window.close();
	}else{
		window.opener.opener=null;
		window.opener.location.href=window.opener.location.href;
		window.close();
		this.disabled=true;
	}
	return true;
}

function CheckBrowser(){
	if (navigator.appName == "Microsoft Internet Explorer") { 
		start = navigator.userAgent.indexOf("MSIE ") + "MSIE ".length 
		if(navigator.userAgent.indexOf(";", start) > 0) {
			end = navigator.userAgent.indexOf(";", start) 
		} else { 
			end = navigator.userAgent.indexOf(")", start) 
		} 
		version = parseFloat(navigator.userAgent.substring(start, end)) 
		if(version>5.5)	return true;
	}
	return false;
}