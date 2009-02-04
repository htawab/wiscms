var WebsiteObj = new Object();
WebsiteObj.System = new Object();
WebsiteObj.System.Ajax = new AJAXRequest;
WebsiteObj.System.CommentPath = '/AjaxRequests/Comment.aspx';
WebsiteObj.System.CommentContainerId = 'BetTag';

var responseText;

WebsiteObj.System.Hits = function() {
    if (!document.getElementById("ArticleId")) return;
    var ArticleId = document.getElementById("ArticleId").value;
    if (!WebsiteObj.System.Ajax) return;
    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.CommentPath + '?command=Hits&ArticleId=' + ArticleId,
	    function(req) {
        document.getElementById("Hits").innerHTML = "浏览次数： " + req.responseText;
    });
}
WebsiteObj.System.Comments = function() {
    if (!document.getElementById("ArticleId")) return;
    var ArticleId = document.getElementById("ArticleId").value;
    if (!WebsiteObj.System.Ajax) return;
    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.CommentPath + '?command=Comments&ArticleId=' + ArticleId,
	    function(req) {
    document.getElementById("Comments").innerHTML = req.responseText;
    });
}
//读取评论
WebsiteObj.System.CommentsLoad = function() {
    if (!document.getElementById("ArticleId")) return;
    var ArticleId = document.getElementById("ArticleId").value;
    if (!WebsiteObj.System.Ajax) return;
    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(WebsiteObj.System.CommentPath + '?command=CommentsLoad&ArticleId=' + ArticleId,
    function(req) {
        if (req.readyState != 4) return;
        if (req.status != 200) return;
        var html ="";
        var doc = req.responseXML.documentElement;
        if (doc.selectSingleNode("/L") == null) return;
        var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
        var node;
        for (var index = 0; index < childNodesLength; index++) {
            node = doc.selectSingleNode("/L/A[" + index + "]");
            if (node) {
                with (node) {
                    var Comment = getAttribute('V');
                    var Comments = Comment.split(",");
                    var Title = Comments[0];
                    var Commentator = Comments[1];
                    var ContentHtml = Comments[2];
                    var Original = Comments[3]
                    var DateCreated = Comments[4];
                    html += ' <dt>' + Title + '</dt>';
                    html += '<dd>' + ContentHtml + '</dd>';
                    html += '<dd class="ip"><span><a>' + Commentator + '</a>：  发表于<a>' + DateCreated + '</a> IP地址：<a>' + Original + '</a><span></dd>';
                }
            }
        }
        if (document.getElementById("pinglunhtml"))
            document.getElementById("pinglunhtml").innerHTML = html;
    });
}
//
WebsiteObj.System.CommentsInsert = function() {
    if (!document.getElementById("ArticleId")) return;
    if (!document.getElementById("Title")) return;
    if (!document.getElementById("ContentHtml")) return;
    var ArticleId = document.getElementById("ArticleId").value;
    var Title = document.getElementById("Title").value.replace('&', '');
    var ContentHtml = document.getElementById("ContentHtml").value.replace('&', '');
    if (ContentHtml =="") {
        alert("评论内容为空！");
        return;
    }
    if (!WebsiteObj.System.Ajax) return;
    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.CommentPath + '?command=CommentsInsert&ArticleId=' + ArticleId + '&Title=' + escape(Title) + '&ContentHtml=' + escape(ContentHtml),
	    function(req) {
        if (req.responseText == "1") {
            alert("提交成功！谢谢您的参与");
            var Comments = document.getElementById("Comments").innerHTML;

            document.getElementById("Comments").innerHTML = parseInt(Comments) + 1;
            document.getElementById("Title").value = "";
            document.getElementById("ContentHtml").value = "";
            WebsiteObj.System.CommentsLoad();
        }
        else
            alert("提交失败！");
    });

}
WebsiteObj.System.CommentsCancel = function() {
    document.getElementById("Title").value = "";
    document.getElementById("ContentHtml").value = "";
}
////WebsiteObj.System.Load = function(pageIndex) {

//    if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/)) // 记忆滚动条位置
//        document.body.scrollTop = parseInt(arr[1]);
//    if (WebsiteObj.System.Ajax) {
//        WebsiteObj.System.Ajax.get(WebsiteObj.System.TagPath + "?command=TagsList&PageIndex=" + pageIndex, WebsiteObj.System.LoadXml);
//    }

//}
//WebsiteObj.System.LoadXml = function(req) {

//    if (req.readyState != 4) return;
//    if (req.status != 200) return;
//    var html;
//    var article = "System";


//    html = ' <table cellpadding="0" cellspacing="0" class="schTable" id="System" name="System">';
//    html += '  <tr class="cap">';
//    html += ' <td width=30></td>';
//    html += ' <td width=250>名称</td>';
//    html += '  <td width=200>描述</td>';
//    html += ' <td width=80>创建时间</td>';
//    html += '  <td width=150>操作</td>';
//    html += '  </tr>';

//    var doc = req.responseXML.documentElement;
//    if (doc.selectSingleNode("/L") == null) return;
//    var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
//    if (childNodesLength == 0) {
//        html += '  <tr><td colspan=5><span class="red">无记录</span></td></tr>';
//        html += ' </table>';
//        if (Website.Get(WebsiteObj.System.TagContainerId)) {
//            Website.Get(WebsiteObj.System.TagContainerId).innerHTML = html;
//        }
//        var windowheight = document.body.clientHeight;
//        if (windowheight < 480)
//            Website.Get("divright").style.height = "480px";
//        return;
//    }
//    var node;

//    for (var index = 0; index < childNodesLength; index++) {
//        node = doc.selectSingleNode("/L/A[" + index + "]");

//        if (node) {
//            with (node) {
//                var TemplateLabelList = getAttribute('V');
//                var TagLists = TemplateLabelList.split(":");
//                var TagId = TagLists[0];
//                var TagName = TagLists[1];
//                var Description = TagLists[2];
//                var DateCreated = TagLists[3];
//                var i = index + 1;
//                html += '  <tr id="tr' + TagId + '">';
//                html += '    <td id="td' + TagId + '">' + i + '</td>';
//                html += '    <td class="alignLeft"><a href="TemplateLabelUpdate.aspx?TagId=' + TagId + '">' + TagName + '</a></td>';
//                html += '     <td>' + Description + '</td>';
//                html += '     <td>' + DateCreated + '</td>';
//                html += '      <td valign=middle><a href="TemplateLabelUpdate.aspx?TagId=' + TagId + '"><img  src="../images/edit.gif" style="border:0px;vertical-align:middle;"/><a href="#" onclick="WebsiteObj.System.TagDel(' + TagId + ')"><img  src="../images/dels.gif" style="border:0px;vertical-align:middle;"/></a></td>';
//                html += '   </tr>';
//            }
//        }
//    }


//    html += ' </table>';
//    if (Website.Get(WebsiteObj.System.TagContainerId)) {
//        Website.Get(WebsiteObj.System.TagContainerId).innerHTML = html;
//    }
//    var windowheight = document.body.clientHeight;
//    if (windowheight < 480)
//        Website.Get("divright").style.height = "480px";
//    else
//        Website.Get("divright").style.height = "100%";
//}
///
function AJAXRequest() {
    var xmlPool = [], objPool = [], AJAX = this, ac = arguments.length, av = arguments;
    var xmlVersion = ["MSXML2.XMLHTTP", "Microsoft.XMLHTTP"];
    var eF = emptyFun = function() { };
    var av = ac > 0 ? typeof (av[0]) == "object" ? av[0] : {} : {};
    var encode = $GEC(av.charset + "");
    this.url = getp(av.url, "");
    this.content = getp(av.content, "");
    this.method = getp(av.method, "POST");
    this.async = getp(av.async, true);
    this.timeout = getp(av.timeout, 3600000);
    this.ontimeout = getp(av.ontimeout, eF);
    this.onrequeststart = getp(av.onrequeststart, eF);
    this.onrequestend = getp(av.onrequestend, eF);
    this.oncomplete = getp(av.oncomplete, eF);
    this.onexception = getp(av.onexception, eF);
    if (!getObj()) return false;
    function getp(p, d) { return p != undefined ? p : d; }
    function getObj() {
        var i, j, tmpObj;
        for (i = 0, j = xmlPool.length; i < j; i++) if (xmlPool[i].readyState == 0 || xmlPool[i].readyState == 4) return xmlPool[i];
        try { tmpObj = new XMLHttpRequest; }
        catch (e) {
            for (i = 0, j = xmlVersion.length; i < j; i++) {
                try { tmpObj = new ActiveXObject(xmlVersion[i]); } catch (e2) { continue; }
                break;
            }
        }
        if (!tmpObj) return false;
        else { xmlPool[xmlPool.length] = tmpObj; return xmlPool[xmlPool.length - 1]; }
    }
    function $(id) { return document.getElementById(id); }
    function $N(d) { var n = d * 1; return (isNaN(n) ? 0 : n); }
    function $VO(v) { return (typeof (v) == "string" ? (v = $(v)) ? v : false : v); }
    function $GID() { return ((new Date) * 1); }
    function $SOP(id, ct) { objPool[id + ""] = ct; }
    function $LOP(id) { return (objPool[id + ""]); }
    function $SRP(f, r, p) { return (function(s) { s = f(s); for (var i = 0; i < r.length; i++) s = s.replace(r[i], p[i]); return (s); }); }
    function $GEC(cs) {
        if (cs.toUpperCase() == "UTF-8") return (encodeURIComponent);
        else return ($SRP(escape, [/\+/g], ["%2B"]));
    }
    function $ST(obj, text) {
        var nn = obj.nodeName.toUpperCase();
        if ("INPUT|TEXTAREA".indexOf(nn) > -1) obj.value = text;
        else try { obj.innerHTML = text; } catch (e) { };
    }
    function $CB(cb) {
        if (typeof (cb) == "function") return cb;
        else {
            cb = $VO(cb);
            if (cb) return (function(obj) { $ST(cb, obj.responseText); });
            else return emptyFun;
        }
    }
    function $GP(p, v, d, f) {
        var i = 0;
        while (i < v.length) { p[i] = v[i] ? f[i] ? f[i](v[i]) : v[i] : d[i]; i++; }
        while (i < d.length) { p[i] = d[i]; i++; }
    }
    function send(purl, pc, pcbf, pm, pa) {
        var purl, pc, pcbf, pm, pa, ct, ctf = false, xmlObj = getObj(), ac = arguments.length, av = arguments;
        if (!xmlObj) return false;
        var pmp = pm.toUpperCase() == "POST" ? true : false;
        if (!pm || !purl) return false;
        var ev = { url: purl, content: pc, method: pm };
        purl += (purl.indexOf("?") > -1 ? "&" : "?") + "timestamp=" + $GID();
        xmlObj.open(pm, purl, pa);
        AJAX.onrequeststart(ev);
        if (pmp) xmlObj.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        ct = setTimeout(function() { ctf = true; xmlObj.abort(); }, AJAX.timeout);
        xmlObj.onreadystatechange = function() {
            if (ctf) { AJAX.ontimeout(ev); AJAX.onrequestend(ev); }
            else if (xmlObj.readyState == 4) {
                ev.status = xmlObj.status;
                try { clearTimeout(ct); } catch (e) { };
                try { if (xmlObj.status == 200) pcbf(xmlObj); else AJAX.onexception(ev); }
                catch (e) { AJAX.onexception(ev); }
                AJAX.onrequestend(ev);
            }
        }
        if (pmp) xmlObj.send(pc); else xmlObj.send("");
        return true;
    }
    this.setcharset = function(cs) { encode = $GEC(cs); }
    this.get = function() {
        var p = [], av = arguments;
        $GP(p, av, [this.url, this.oncomplete], [null, $CB]);
        if (!p[0] && !p[1]) return false;
        return (send(p[0], "", p[1], "GET", this.async));
    }
    this.update = function() {
        var p = [], purl, puo, pinv, pcnt, av = arguments;
        $GP(p, av, [emptyFun, this.url, -1, -1], [$CB, null, $N, $N]);
        if (p[2] == -1) p[3] = 1;
        var sf = function() { send(p[1], "", p[0], "GET", this.async); };
        var id = $GID();
        var cf = function(cc) {
            sf(); cc--; if (cc == 0) return;
            $SOP(id, setTimeout(function() { cf(cc); }, p[2]));
        }
        cf(p[3]);
        return id;
    }
    this.stopupdate = function(id) {
        clearTimeout($LOP(id));
    }
    this.post = function() {
        var p = [], av = arguments;
        $GP(p, av, [this.url, this.content, this.oncomplete], [null, null, $CB]);
        if (!p[0] && !p[2]) return false;
        return (send(p[0], p[1], p[2], "POST", this.async));
    }
    this.postf = function() {
        var p = [], fo, vaf, pcbf, purl, pc, pm, ac = arguments.length, av = arguments;
        fo = ac > 0 ? $VO(av[0]) : false;
        if (!fo || (fo && fo.nodeName != "FORM")) return false;
        vaf = fo.getAttribute("onvalidate");
        vaf = vaf ? (typeof (vaf) == "string" ? new Function(vaf) : vaf) : null;
        if (vaf && !vaf()) return false;
        $GP(p, [av[1], fo.getAttribute("action"), fo.getAttribute("method")], [this.oncomplete, this.url, this.method], [$CB, null, null]);
        pcbf = p[0]; purl = p[1];
        if (!pcbf && !purl) return false;
        pc = this.formToStr(fo); if (!pc) return false;
        if (p[2].toUpperCase() == "POST")
            return (send(purl, pc, pcbf, "POST", true));
        else {
            purl += (purl.indexOf("?") > -1 ? "&" : "?") + pc;
            return (send(purl, "", pcbf, "GET", true));
        }
    }
    /* formToStr
    // from SurfChen <surfchen@gmail.com>
    // @url     http://www.surfchen.org/
    // @license http://www.gnu.org/licenses/gpl.html GPL
    // modified by xujiwei
    // @url     http://www.xujiwei.cn/
    */
    this.formToStr = function(fc) {
        var i, qs = "", and = "", ev = "";
        for (i = 0; i < fc.length; i++) {
            e = fc[i];
            if (e.name != '') {
                if (e.type == 'select-one' && e.selectedIndex > -1) ev = e.options[e.selectedIndex].value;
                else if (e.type == 'checkbox' || e.type == 'radio') {
                    if (e.checked == false) continue;
                    ev = e.value;
                }
                else ev = e.value;
                ev = encode(ev);
                qs += and + e.name + '=' + ev;
                and = "&";
            }
        }
        return qs;
    }
}