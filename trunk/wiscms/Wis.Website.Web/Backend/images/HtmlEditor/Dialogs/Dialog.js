String.prototype.Contains = function(s){
    return (this.indexOf(s)>-1);
};

String.prototype.StartsWith = function(s){
    return (this.substr(0,s.length)==s);
};

String.prototype.EndsWith = function(s, ignoreCase){
    var L1 = this.length;
    var L2 = s.length;
    if (L2>L1){
        return false;
    }
    if (ignoreCase){
        var oRegex = new RegExp(s+'$','i');
        return oRegex.test(this);
    }
    else{
        return (L2==0 || this.substr(L1-L2, L2)==s);
    }
};

var $ = function(i){
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
}
    
var Wis = new Object();
Wis.Browser = new Object();
Wis.Browser.UserAgent = navigator.userAgent.toLowerCase();
Wis.Browser.IsIE = Wis.Browser.UserAgent.Contains('msie');
Wis.Browser.IsIE7 = Wis.Browser.UserAgent.Contains('msie 7');
Wis.Browser.IsSP2 = Wis.Browser.UserAgent.Contains("sv1");
Wis.Browser.IsGecko = Wis.Browser.UserAgent.Contains('gecko/');
Wis.Browser.IsSafari = Wis.Browser.UserAgent.Contains('safari');
Wis.Browser.IsOpera = Wis.Browser.UserAgent.Contains('opera');
Wis.Browser.IsMac = Wis.Browser.UserAgent.Contains('macintosh');
Wis.Browser.IsCompatible = function(){
    if ( Wis.Browser.IsIE && !Wis.Browser.IsMac && !Wis.Browser.IsOpera ){
        var s_Ver = navigator.appVersion.match(/MSIE (.\..)/)[1];
        return ( s_Ver >= 5.5 );
    }
    return false;
}

Wis.Params = new Object();
Wis.QueryString = document.location.search.substr(1).split("&");
for (var index=0; index<Wis.QueryString.length; index++){
    var params = Wis.QueryString[index].split("=");
    Wis.Params[params[0]] = params[1];
}
//Wis.Params.LinkField = Wis.Params["id"];

function IsDigit(){
    return ((event.keyCode >= 48) && (event.keyCode <= 57));
}

function ToInt(str){
    str=BaseTrim(str);
    if (str!=""){
        var sTemp=parseFloat(str);
        if (isNaN(sTemp)){
            str="";
        }
        else{
            str=sTemp;
        }
    }
    return str;
}

function IsColor(color){
    var temp=color;
    if (temp=="") return true;
    if (temp.length!=7) return false;
    return (temp.search(/\#[a-fA-F0-9]{6}/) != -1);
}

function BaseAlert(theText, notice){
    alert(notice);
    theText.focus();
    theText.select();
    return false;
}

function IsExt(url, opt){
    var sTemp;
    var b = false;
    var s = opt.toUpperCase().split("|");
    for (var i=0; i<s.length; i++ ){
        sTemp = url.substr(url.length-s[i].length-1);
        sTemp = sTemp.toUpperCase();
        s[i]  = "." + s[i];
        if (s[i] == sTemp){
            b=true;break;
        }
    }
    return b;
}


/// <summary>
/// 
/// </summary>
function SearchSelectValue(s, v){
    for (var index=0; index<s.length; index++){
        if (s.options[index].value == v){
            s.selectedIndex = index;
            return true;
        }
    }
    return false;
}