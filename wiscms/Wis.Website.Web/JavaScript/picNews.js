if (typeof sas == "undefined") var sas = new Object();
if (typeof sas.ued == "undefined") sas.ued = new Object();
if (typeof sas.ued.util == "undefined") sas.ued.util = new Object();
if (typeof sas.ued.FlashObjectUtil == "undefined") sas.ued.FlashObjectUtil = new Object();
sas.ued.FlashObject = function(swf, id, w, h, ver, c, useExpressInstall, quality, xiRedirectUrl, redirectUrl, detectKey) {
    if (!document.createElement || !document.getElementById)
        return;
    this.DETECT_KEY = detectKey ? detectKey : 'detectflash';
    this.skipDetect = sas.ued.util.getRequestParameter(this.DETECT_KEY);
    this.params = new Object();
    this.variables = new Object();
    this.attributes = new Array();
    this.useExpressInstall = useExpressInstall;
    if (swf) this.setAttribute('swf', swf);
    if (id) this.setAttribute('id', id);
    if (w) this.setAttribute('width', w);
    if (h) this.setAttribute('height', h);
    if (ver) this.setAttribute('version', new sas.ued.PlayerVersion(ver.toString().split(".")));
    this.installedVer = sas.ued.FlashObjectUtil.getPlayerVersion(this.getAttribute('version'), useExpressInstall);
    if (c) this.addParam('bgcolor', c);
    var q = quality ? quality : 'high';
    this.addParam('quality', q);
    var xir = (xiRedirectUrl) ? xiRedirectUrl : window.location;
    this.setAttribute('xiRedirectUrl', xir);
    this.setAttribute('redirectUrl', '');
    if (redirectUrl) this.setAttribute('redirectUrl', redirectUrl)
};
sas.ued.FlashObject.prototype = { setAttribute: function(name, value) {
        this.attributes[name] = value
    },
    getAttribute: function(name) {
        return this.attributes[name]
    },
    addParam: function(name, value) {
        this.params[name] = value
    },
    getParams: function() {
        return this.params
    },
    addVariable: function(name, value) {
        this.variables[name] = value
    },
    getVariable: function(name) {
        return this.variables[name]
    },
    getVariables: function() {
        return this.variables
    },
    createParamTag: function(n, v) {
        var p = document.createElement('param'); p.setAttribute('name', n); p.setAttribute('value', v); return p
    },
    getVariablePairs: function() {
        var variablePairs = new Array(); var key; var variables = this.getVariables();
        for (key in variables) {
            variablePairs.push(key + "=" + variables[key])
        }
        return variablePairs
    },
    getFlashHTML: function() { var flashNode = ""; if (navigator.plugins && navigator.mimeTypes && navigator.mimeTypes.length) { if (this.getAttribute("doExpressInstall")) this.addVariable("MMplayerType", "PlugIn"); flashNode = '<embed type="application/x-shockwave-flash" src="' + this.getAttribute('swf') + '" width="' + this.getAttribute('width') + '" height="' + this.getAttribute('height') + '"'; flashNode += ' id="' + this.getAttribute('id') + '" name="' + this.getAttribute('id') + '" '; var params = this.getParams(); for (var key in params) { flashNode += [key] + '="' + params[key] + '" ' } var pairs = this.getVariablePairs().join("&"); if (pairs.length > 0) { flashNode += 'flashvars="' + pairs + '"' } flashNode += '/>' } else { if (this.getAttribute("doExpressInstall")) this.addVariable("MMplayerType", "ActiveX"); flashNode = '<object id="' + this.getAttribute('id') + '" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="' + this.getAttribute('width') + '" height="' + this.getAttribute('height') + '">'; flashNode += '<param name="movie" value="' + this.getAttribute('swf') + '" />'; var params = this.getParams(); for (var key in params) { flashNode += '<param name="' + key + '" value="' + params[key] + '" />' } var pairs = this.getVariablePairs().join("&"); if (pairs.length > 0) { flashNode += '<param name="flashvars" value="' + pairs + '" />' } flashNode += "</object>" } return flashNode }, write: function(elementId) { if (this.useExpressInstall) { var expressInstallReqVer = new sas.ued.PlayerVersion([6, 0, 65]); if (this.installedVer.versionIsValid(expressInstallReqVer) && !this.installedVer.versionIsValid(this.getAttribute('version'))) { this.setAttribute('doExpressInstall', true); this.addVariable("MMredirectURL", escape(this.getAttribute('xiRedirectUrl'))); document.title = document.title.slice(0, 47) + " - Flash Player Installation"; this.addVariable("MMdoctitle", document.title) } } else { this.setAttribute('doExpressInstall', false) } if (this.skipDetect || this.getAttribute('doExpressInstall') || this.installedVer.versionIsValid(this.getAttribute('version'))) { var n = (typeof elementId == 'string') ? document.getElementById(elementId) : elementId; n.innerHTML = this.getFlashHTML() } else { if (this.getAttribute('redirectUrl') != "") { document.location.replace(this.getAttribute('redirectUrl')) } } }
};
sas.ued.FlashObjectUtil.getPlayerVersion = function(reqVer, xiInstall) {
    var PlayerVersion = new sas.ued.PlayerVersion(0, 0, 0);
    if (navigator.plugins && navigator.mimeTypes.length) {
        var x = navigator.plugins["Shockwave Flash"];
        if (x && x.description) {
            PlayerVersion = new sas.ued.PlayerVersion(x.description.replace(/([a-z]|[A-Z]|\s)+/, "").replace(/(\s+r|\s+b[0-9]+)/, ".").split("."))
        }
    }
    else {
        try {
            var axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
            for (var i = 3; axo != null; i++) {
                axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash." + i);
                PlayerVersion = new sas.ued.PlayerVersion([i, 0, 0])
            }
        }
        catch (e) { }
        if (reqVer && PlayerVersion.major > reqVer.major) return PlayerVersion;
        if (!reqVer || ((reqVer.minor != 0 || reqVer.rev != 0) && PlayerVersion.major == reqVer.major) || PlayerVersion.major != 6 || xiInstall) {
            try { PlayerVersion = new sas.ued.PlayerVersion(axo.GetVariable("$version").split(" ")[1].split(",")) }
            catch (e) { }
        }
    }
    return PlayerVersion
};
sas.ued.PlayerVersion = function(arrVersion) {
    this.major = parseInt(arrVersion[0]) || 0;
    this.minor = parseInt(arrVersion[1]) || 0;
    this.rev = parseInt(arrVersion[2]) || 0
};
sas.ued.PlayerVersion.prototype.versionIsValid = function(fv) {
    if (this.major < fv.major) return false; if (this.major > fv.major) return true;
    if (this.minor < fv.minor) return false; if (this.minor > fv.minor) return true;
    if (this.rev < fv.rev) return false; return true
};
sas.ued.util = { getRequestParameter: function(param) {
        var q = document.location.search || document.location.href.hash;
        if (q) {
            var startIndex = q.indexOf(param + "=");
            var endIndex = (q.indexOf("&", startIndex) > -1) ? q.indexOf("&", startIndex) : q.length; if (q.length > 1 && startIndex > -1) { return q.substring(q.indexOf("=", startIndex) + 1, endIndex) }
        }
        return ""
    }
};
if (Array.prototype.push == null) {
    Array.prototype.push = function(item) {
        this[this.length] = item; return this.length
    }
}
var getQueryParamValue = sas.ued.util.getRequestParameter;
var eduFlash = sas.ued.FlashObject;
function Cookie(document, name, hours, path, domain, secure) {
    this.$document = document;
    this.$name = name;
    this.$expiration = hours ? new Date((new Date()).getTime() + hours * 3600000) : null;
    this.$path = path ? path : null;
    this.$domain = domain ? domain : null;
    this.$secure = secure;
};
Cookie.prototype.store = function() {
    var cookieval = "";
    for (var prop in this) {
        if ((prop.charAt(0) == '$') || ((typeof this[prop]) == 'function')) continue;
        if (cookieval != "") cookieval += '&'; cookieval += prop + ':' + escape(this[prop]);
    }
    var cookie = this.$name + '=' + cookieval;
    if (this.$expiration) cookie += '; expires=' + this.$expiration.toGMTString();
    if (this.$path) cookie += '; path=' + this.$path;
    if (this.$domain) cookie += '; domain=' + this.$domain;
    if (this.$secure) cookie += '; secure';
    this.$document.cookie = cookie;
}; Cookie.prototype.load = function() {
    var allcookies = this.$document.cookie; if (allcookies == "") return false;
    var start = allcookies.indexOf(this.$name + '='); if (start == -1) return false;
    start += this.$name.length + 1;
    var end = allcookies.indexOf(';', start);
    if (end == -1) end = allcookies.length;
    var cookieval = allcookies.substring(start, end);
    var a = cookieval.split('&');
    for (var i = 0; i < a.length; i++) a[i] = a[i].split(':');
    for (var i = 0; i < a.length; i++)
        this[a[i][0]] = unescape(a[i][1]);
    return true;
};
Cookie.prototype.remove = function() {
    var cookie; cookie = this.$name + '='; if (this.$path) cookie += '; path=' + this.$path;
    if (this.$domain) cookie += '; domain=' + this.$domain; cookie += '; expires=Fri, 02-Jan-1970 00:00:00 GMT';
    this.$document.cookie = cookie;
};