var WebsiteObj = new Object();
WebsiteObj.Article = new Object();
WebsiteObj.Article.Ajax = new AJAXRequest;
WebsiteObj.Article.ArticlePath = '/AjaxRequests/Article.aspx';
WebsiteObj.Article.ArticleContainerId = 'BetArticle';
WebsiteObj.Article.PagerId = 'Pager';
WebsiteObj.Article.ContainerId = "RepeaterHolder";

var PageIndex = 1;
var categoryId;
var responseText;
var keysword = "";
var type = "";

WebsiteObj.Article.Seacrh = function() {
    categoryId = Website.Get("CategoryId").value;

    keysword = Website.Get("keysword").value;
    if (keysword == "") {
        alert("请输入关键字");
        return;
    }
    type = Website.Get("type").value;
    WebsiteObj.Article.LoadData(1);
}

WebsiteObj.Article.AllShow = function() {
    categoryId = "";
    keysword = "";
    type = "";
    WebsiteObj.Article.LoadData(1);
}

WebsiteObj.Article.CategoryId = function(o) {
    categoryId = o;
}

WebsiteObj.Article.LoadData = function(pageIndex) {

    if (!WebsiteObj.Article.Ajax) return;
    var pager = new Pager('PagerHolder');
    var count;
    WebsiteObj.Article.Ajax.async = false;
    WebsiteObj.Article.Ajax.get(
	    WebsiteObj.Article.ArticlePath + '?command=Count&CategoryId=' + categoryId + "&keysword=" + keysword + "&type=" + type,
	    function(req) {
        count = req.responseText; // 定义总记录数(必要)
    })
    pager.RecordCount = count;
    pager.ReloadFunction = 'WebsiteObj.Article.LoadData';
    pager.PageIndex = pageIndex;
    pager.Method = 'Ajax';
    pager.PageSize = 20;
    pager.CreateHtml(1);
    //    WebsiteObj.Article.Ajax.get(WebsiteObj.Article.Path + '?page=' + pageIndex + '&pagesize=' + pager.PageSize, WebsiteObj.Article.DataReceived);
    //    //    WebsiteObj.Article.DataReceived(pager)
    PageIndex = pageIndex;
    WebsiteObj.Article.Load(pageIndex);

}
//WebsiteObj.Article.DataReceived = function(request) {
//    alert(request);
//    if (request.readyState != 4) return;
//    if (request.status != 200) return;
//    if (document.getElementById('DataHolder')) {
//        document.getElementById('DataHolder').innerText = request.responseText;
//    }
//}
WebsiteObj.Article.Load = function(pageIndex) {

    if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/)) // 记忆滚动条位置
        document.body.scrollTop = parseInt(arr[1]);
    if (WebsiteObj.Article.Ajax) {
        WebsiteObj.Article.Ajax.get(WebsiteObj.Article.ArticlePath + "?command=ArticleList&PageIndex=" + pageIndex + "&CategoryId=" + categoryId + "&keysword=" + keysword + "&type=" + type, WebsiteObj.Article.LoadXml);
    }

}
WebsiteObj.Article.LoadXml = function(req) {

    if (req.readyState != 4) return;
    if (req.status != 200) return;
    var html;
    var article = "Article";


    html = ' <table cellpadding="0" cellspacing="0" class="schTable" id="Article" name="Article">';
    html += '  <tr class="cap">';
    html += ' <td width=30><input type="checkbox" id="ckeckid" onclick=CheckedAllInElement(this,"' + article + '") /></td>';
    html += ' <td width=350>标题</td>';
    html += '  <td width=100>作者</td>';
    html += ' <td width=80>栏目</td>';
    html += '  <td>操作</td>';
    html += '  </tr>';

    var doc = req.responseXML.documentElement;
    if (doc.selectSingleNode("/L") == null) return;
    var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
    if (childNodesLength == 0) {
        html += '  <tr><td colspan=5><span class="red">无记录</span></td></tr>';
        html += ' </table>';
        if (Website.Get(WebsiteObj.Article.ArticleContainerId)) {
            Website.Get(WebsiteObj.Article.ArticleContainerId).innerHTML = html;
        }
        var windowheight = document.body.clientHeight;
        if (windowheight < 480)
            Website.Get("divright").style.height = "480px";
        return;
    }
    var node;

    for (var index = 0; index < childNodesLength; index++) {
        node = doc.selectSingleNode("/L/A[" + index + "]");

        if (node) {
            with (node) {
                var ArticleList = getAttribute('V');
                var ArticleLists = ArticleList.split(";");
                var ArticleId = ArticleLists[0];
                var CategoryId = ArticleLists[1];
                var CategoryName = ArticleLists[2];
                var TabloidPath = ArticleLists[3];
                var TitleColor = ArticleLists[4]; // ArticleId
                var Author = ArticleLists[5];
                var Title = ArticleLists[6];
                var DateCreated = ArticleLists[7];
                html += '  <tr id="tr' + ArticleId + '">';
                html += '    <td id="td' + ArticleId + '"><input type="checkbox" id="ckeck' + ArticleId + '" value="' + ArticleId + '" name="ckeck' + ArticleId + '" /></td>';
                html += '    <td class="alignLeft"><a href="ArticleUpdate.aspx?ArticleId=' + ArticleId + '"><font color="' + TitleColor + '">' + Title + '</font></a></td>';
                html += '     <td>' + Author + '</td>';
                html += '     <td><a href="#" onclick="WebsiteObj.Article.CategoryId(' + CategoryId + '),WebsiteObj.Article.LoadData(1)">' + CategoryName + '</a></td>';
                html += '      <td valign=middle><a href="ArticleUpdate.aspx?ArticleId=' + ArticleId + '"><img  src="../images/edit.gif" style="border:0px;vertical-align:middle;"/></a> 生成静态页面 <a href="#" onclick="WebsiteObj.Article.ArticleDel(' + ArticleId + ')"><img  src="../images/dels.gif" style="border:0px;vertical-align:middle;"/></a></td>';
                html += '   </tr>';
            }
        }
    }


    html += ' </table>';
    if (Website.Get(WebsiteObj.Article.ArticleContainerId)) {
        Website.Get(WebsiteObj.Article.ArticleContainerId).innerHTML = html;
    }
    var windowheight = document.body.clientHeight;
    if (windowheight < 480)
        Website.Get("divright").style.height = "480px";
    else
        Website.Get("divright").style.height = "100%";
}
///
WebsiteObj.Article.ArticleAdd = function() {
    window.location.href = "ArticleAdd.aspx?CategoryId=" + categoryId;
}

WebsiteObj.Article.ArticleDel = function(ArticleId) {
    if (!confirm("您确定要删除吗？")) return false;

    if (!WebsiteObj.Article.Ajax) return;

    WebsiteObj.Article.Ajax.async = false;
    WebsiteObj.Article.Ajax.get(
	    WebsiteObj.Article.ArticlePath + '?command=ArticleDel&ArticleId=' + ArticleId,
	    function(req) {
        if (req.responseText == "1") {
            Website.Get("td" + ArticleId).innerHTML = "";
            Website.Get("tr" + ArticleId).style.display = "none";
        }
        else {
            alert("删除失败！")
        }
    })
}
////生成栏目静态页面
WebsiteObj.Article.CategoryHtml = function() {
    if (!WebsiteObj.Article.Ajax) return;
        WebsiteObj.Article.Ajax.get(WebsiteObj.Article.ArticlePath + '?command=CategoryHtml&CategoryId=' + categoryId,
	    function(req) {
        if (req.responseText == "0") {
            alert("生成失败！");
        }
        else {
            alert(req.responseText);
        }
    });
    closefDiv();
}
// 批量删除
WebsiteObj.Article.ArticleDelAll = function() {
    if (!confirm("您确定要删除吗？")) return false;
    var ArticleId = "";
    if (!WebsiteObj.Article.Ajax) return;
    var elements = document.getElementsByTagName('INPUT');
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].type != 'checkbox') continue;

        //         atchId = elements[i].MatchId;

        if (elements[i].checked) {
            ArticleId += elements[i].value + ",";
        }
    }
    if (ArticleId.length <= 0) {
        alert("请选择要删除的记录！");
        return false;
    }
    WebsiteObj.Article.Ajax.get(
	    WebsiteObj.Article.ArticlePath + '?command=ArticleDelAll&ArticleId=' + ArticleId,
	    function(req) {
        if (req.responseText == "1") {
            var count;
            WebsiteObj.Article.Ajax.async = false;
            WebsiteObj.Article.Ajax.get(
	    WebsiteObj.Article.ArticlePath + '?command=Count&CategoryId=' + categoryId + "&keysword=" + keysword + "&type=" + type,
	    function(reqcount) {
                count = reqcount.responseText; // 定义总记录数(必要)
            });
            var pageCount = parseInt(count) / 20 + ((parseInt(count) % 20 == 0) ? 0 : 1);
            if (isNaN(parseInt(pageCount))) pageCount = 1;
            if (pageCount < 1) pageCount = 1;
            if (PageIndex > pageCount) PageIndex = pageCount;
            WebsiteObj.Article.LoadData(PageIndex);
        }
        else {
            alert("删除失败！")
        }
    })
}
