var LastSelectObj;
//CSS背景控制
function overColor(Obj) {
    var elements = Obj.childNodes;
    for (var i = 0; i < elements.length; i++) {
        elements[i].className = "TR_BG"
        Obj.bgColor = ""; //颜色要改
    }

}
function outColor(Obj) {
    var elements = Obj.childNodes;
    for (var i = 0; i < elements.length; i++) {
        elements[i].className = "TR_BG_list";
        Obj.bgColor = "";
    }
}

//CSS背景控制
function useroverColor(Obj) {
    var elements = Obj.childNodes;
    for (var i = 0; i < elements.length; i++) {
        elements[i].className = "bg_over"
        Obj.bgColor = ""; //颜色要改
    }

}
function useroutColor(Obj) {
    var elements = Obj.childNodes;
    for (var i = 0; i < elements.length; i++) {
        elements[i].className = "bg_out";
        Obj.bgColor = "";
    }
}


//判断是否数组
function isArray(obj) {
    if (obj.constructor == window.Array)
        return true;
    else
        return false;
}

function selectFile(type, obj, height, width) {

    var ShowObj = obj;
    if (isArray(obj) && obj.length > 1)
        ShowObj = obj[1];
    showfDiv(ShowObj, "loading...", width, height);
    LastSelectObj = obj;

    var options = {
        method: 'get',
        parameters: "heights=" + height,
        onComplete: function(transport) {
            var returnvalue = transport.responseText;
            if (returnvalue.indexOf("??") > -1)
                showfDiv(ShowObj, 'Error', width, height);
            else
                var tempstr = returnvalue;
            showfDiv(ShowObj, tempstr, width, height);
        }
    };
    var arrtype = type.split("|")[0]
    switch (arrtype) {
        case "CategoryList":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=CategoryList', options);
            break;
        case "pic":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=pic', options);
            break;
        case "file":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=file', options);
            break;
        case "video":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=video', options);
            break;
        case "templet":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=templet', options);
            break;
        case "UploadImage":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=UploadImage', options);
            break;
        case "UploadFile":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=UploadFile', options);
            break;
        case "UploadVideo":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=UploadVideo', options);
            break;
        case "ReleasePath":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=ReleasePath', options);
            break;
        case "videoEdit":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=videoEdit', options);
            break;
        case "cutimg":
            new Ajax.Request('/Backend/dialog/iframe.aspx?FileType=cutimg&ImagePath=' + ShowObj.value, options);
            break;
    }
}
//function cutimg(obj) {
//    showfDiv(ShowObj, "loading...", 300, 200);
//    LastSelectObj = obj;
//    
//}

 
position = function(x, y) {
    this.x = x;
    this.y = y;
}
getPosition = function(oElement) {
    var objParent = oElement
    var oPosition = new position(0, 0);
    while (objParent.tagName != "BODY") {
        oPosition.x += objParent.offsetLeft;
        oPosition.y += objParent.offsetTop;
        objParent = objParent.offsetParent;
    }
    return oPosition;
}
///
function showfDiv(obj, content, width, height) {
    var pos = getPosition(obj);
    var objDiv = document.getElementById("s_id");
    if (objDiv == null) {
        objDiv = document.createElement("div");
        objDiv.id = "s_id";
    }
    objDiv.className = "selectStyle";
    objDiv.style.position = "absolute";
    var tempheight = pos.y;
    var tempwidth1, tempheight1;
    var windowwidth = document.body.clientWidth;
    var windowheight = document.body.clientHeight;

    var isIE5 = (navigator.appVersion.indexOf("MSIE 5") > 0) || (navigator.appVersion.indexOf("MSIE") > 0 && parseInt(navigator.appVersion) > 4);
    var isIE55 = (navigator.appVersion.indexOf("MSIE 5.5") > 0);
    var isIE6 = (navigator.appVersion.indexOf("MSIE 6") > 0);
    var isIE7 = (navigator.appVersion.indexOf("MSIE 7") > 0);

    if (isIE5 || isIE55 || isIE6 || isIE7) { var tempwidth = pos.x + 305; } else { var tempwidth = pos.x + 312; }
    objDiv.style.width = width + "px";
    objDiv.innerHTML = content;
    if (tempwidth > windowwidth) {
        tempwidth1 = tempwidth - windowwidth
        objDiv.style.left = (pos.x - tempwidth1) + "px";
    }
    else {
        if (isIE5 || isIE55 || isIE6 || isIE7) { objDiv.style.left = (pos.x) + "px"; } else { objDiv.style.left = (pos.x) + "px"; }
    }
    if (isIE5 || isIE55 || isIE6 || isIE7) { objDiv.style.top = (pos.y + 22) + "px"; } else { objDiv.style.top = (pos.y + 22) + "px"; }

    if ((pos.y + 22 + height) > windowheight) {
        objDiv.style.top = (pos.y - height - 30) + "px";
    }
    if ((pos.y - height - 30) < 0)
        objDiv.style.top = (pos.y + 22) + "px";
    if ((pos.x + width) > windowwidth) {
        var b = pos.x + width - windowwidth;
        objDiv.style.left = (pos.x - b - 22) + "px";
    }
    objDiv.style.display = "";
    document.ondblclick = function() { if (objDiv.style.display == "") { objDiv.style.display = "none"; } }
    document.body.appendChild(objDiv);
}

function closefDiv() {
    try {

        document.getElementById("s_id").style.display = "none";
    }
    catch (e) {
        parent.document.getElementById("s_id").style.display = "none";
    }
}
function ReturnFun(Return_Strs) {
    if (isArray(LastSelectObj)) {
        for (var i = 0; i < LastSelectObj.length; i++) {
            SetValue(LastSelectObj[i], Return_Strs[i]);
        }
    }
    else {
        SetValue(LastSelectObj, Return_Strs);
    }
    document.getElementById("s_id").style.display = "none";
}
function ReturnFunVdieo(content) {
    var oEditor = FCKeditorAPI.GetInstance("ContentHtml");
    if (oEditor.EditMode == FCK_EDITMODE_WYSIWYG) {
        oEditor.InsertHtml(content);
    }
    else {
        document.getElementById("s_id").style.display = "none";
        return false;
    }
    document.getElementById("s_id").style.display = "none";
}

function SetValue(obj, val) {
    if (obj == null || typeof (obj) == "undefined") {
        alert("选择失败，请重新选择。");
    }
    else {
        if (val == null || typeof (val) == "undefined")
            val = '';
        obj.value = val;
    }
}

function ShowDivPic(obj, Urls, exname, length) {
    var Url = Urls.replace("\\", "/");
    var pos = getPosition(obj)
    var objDiv = document.createElement("div");
    objDiv.className = "lionrong"; // For IE
    objDiv.id = "showpic_id";
    objDiv.style.position = "absolute";
    var tempheight = pos.y;
    var tempwidth1, tempheight1;
    var windowwidth = document.body.clientWidth;

    var isIE5 = (navigator.appVersion.indexOf("MSIE 5") > 0) || (navigator.appVersion.indexOf("MSIE") > 0 && parseInt(navigator.appVersion) > 4);
    var isIE55 = (navigator.appVersion.indexOf("MSIE 5.5") > 0);
    var isIE6 = (navigator.appVersion.indexOf("MSIE 6") > 0);
    var isIE7 = (navigator.appVersion.indexOf("MSIE 7") > 0);
    switch (exname) {
        case ".jpg": case ".gif": case ".bmp": case ".ico": case ".png": case ".jpeg": case ".tif":
            if (length < 12000) {
                if (Url == "") {
                    var content = "无图片";
                }
                else {
                    var content = "<img src='" + Url + "' border='0' width='200px' />";
                }
            }
            else {
                var content = "<img src='" + Url + "' border='0' width='100px'/>";
            }
            break;
        case ".swf":
            if (length < 12000) {
                var content = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\">";
                content += "<param name=\"movie\" value=\"" + Url + "\" />"
                content += "<param name=\"quality\" value=\"high\" />"
                content += "<embed src=\"" + Url + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\"></embed>"
                content += "</object>"
            }
            else {
                var content = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"100px\">";
                content += "<param name=\"movie\" value=\"" + Url + "\" />"
                content += "<param name=\"quality\" value=\"high\" />"
                content += "<embed src=\"" + Url + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"100px\"></embed>"
                content += "</object>"
            }
            break;
            break;
        case ".html": case ".htm": case ".aspx": case ".shtm": case ".shtml": case ".asp":
            var content = "Path:" + Url;
            break;
        default:
            var content = "Path:" + Url;
            break;
    }
    if (isIE5 || isIE55 || isIE6 || isIE7) { var tempwidth = pos.x + 250; } else { var tempwidth = pos.x + 250; }
    objDiv.innerHTML = content;
    if (tempwidth > windowwidth) {
        tempwidth1 = tempwidth - windowwidth
        objDiv.style.left = (pos.x - tempwidth1) + "px";
    }
    else {
        if (isIE5 || isIE55 || isIE6 || isIE7) { objDiv.style.left = (pos.x) + "px"; } else { objDiv.style.left = (pos.x) + "px"; }
    }
    if (isIE5 || isIE55 || isIE6 || isIE7) { objDiv.style.top = (pos.y + 18) + "px"; } else { objDiv.style.top = (pos.y + 18) + "px"; }

    objDiv.style.left = "250px";
    objDiv.style.top = (pos.y - 68) + "px";
    objDiv.style.display = "";
    document.onclick = function() { if (objDiv.style.display == "") { objDiv.style.display = "none"; } }
    document.body.appendChild(objDiv);
}
function hiddDivPic() {
    var objDiv = document.getElementById("showpic_id");
    if (objDiv != null && objDiv != "undefined") {
        document.body.removeChild(objDiv);
    }
}