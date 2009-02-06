var WebsiteObj = new Object();
WebsiteObj.Page = new Object();
WebsiteObj.Page.Ajax = new AJAXRequest;
WebsiteObj.Page.PagePath = '/AjaxRequests/Article.aspx';
WebsiteObj.Page.PageContainerId = 'BetPage';
WebsiteObj.Page.PagerId = 'Pager';
WebsiteObj.Page.ContainerId = "RepeaterHolder";

WebsiteObj.Page.LoadData = function(pageIndex) {

    if (!WebsiteObj.Page.Ajax) return;
    var pager = new Pager('PagerHolder');
    var count;
    WebsiteObj.Page.Ajax.async = false;
    WebsiteObj.Page.Ajax.get(
	    WebsiteObj.Page.PagePath + '?command=PageCount',
	    function(req) {
        count = req.responseText; // 定义总记录数(必要)
    })
    pager.RecordCount = count;
    pager.ReloadFunction = 'WebsiteObj.Page.LoadData';
    pager.PageIndex = pageIndex;
    pager.Method = 'Ajax';
    pager.PageSize = 20;
    pager.CreateHtml(1);
    //    WebsiteObj.Page.Ajax.get(WebsiteObj.Page.Path + '?page=' + pageIndex + '&pagesize=' + pager.PageSize, WebsiteObj.Page.DataReceived);
    //    //    WebsiteObj.Page.DataReceived(pager)
    WebsiteObj.Page.Load(pageIndex);
}
//WebsiteObj.Page.DataReceived = function(request) {
//    alert(request);
//    if (request.readyState != 4) return;
//    if (request.status != 200) return;
//    if (document.getElementById('DataHolder')) {
//        document.getElementById('DataHolder').innerText = request.responseText;
//    }
//}
WebsiteObj.Page.Load = function(pageIndex) {

    if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/)) // 记忆滚动条位置
        document.body.scrollTop = parseInt(arr[1]);
    if (WebsiteObj.Page.Ajax) {
        WebsiteObj.Page.Ajax.get(WebsiteObj.Page.PagePath + "?command=PageList&PageIndex=" + pageIndex, WebsiteObj.Page.LoadXml);
    }

}
WebsiteObj.Page.LoadXml = function(req) {

    if (req.readyState != 4) return;
    if (req.status != 200) return;
    var html;
    var page = "Page";


    html = ' <table cellpadding="0" cellspacing="0" class="schTable" id="Page" name="Page">';
    html += '  <tr class="cap">';
    html += ' <td width=10></td>';
    html += ' <td width=400>页面标题</td>';
    html += ' <td width=100>栏目</td>';
    html += '  <td>操作</td>';
    html += '  </tr>';

    var doc = req.responseXML.documentElement;
    if (doc.selectSingleNode("/L") == null) return;
    var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
    if (childNodesLength == 0) {
        html += '  <tr><td colspan=4><span class="red">无记录</span></td></tr>';
        html += ' </table>';
        if (Website.Get(WebsiteObj.Page.PageContainerId)) {
            Website.Get(WebsiteObj.Page.PageContainerId).innerHTML = html;
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
                var PageList = getAttribute('V');
                var PageLists = PageList.split(";");
                var PageId = PageLists[0];
                var CategoryId = PageLists[1];
                var CategoryName = PageLists[2];
                var Enable = PageLists[3];
                var Title = PageLists[4];
                var DateCreated = PageLists[5];
                html += '  <tr id="tr' + PageId + '">';
                html += '    <td id="td' + PageId + '"></td>';
                html += '    <td class="alignLeft"><a href="PageUpdate.aspx?PageId=' + PageId + '">' + Title + '</a></td>';
                html += '     <td>' + CategoryName + '</td>';
                html += '      <td valign=middle><a href="PageUpdate.aspx?PageId=' + PageId + '"><img  src="../images/edit.gif" style="border:0px;vertical-align:middle;"/></a> 生成静态页面 <a href="#" onclick="WebsiteObj.Page.PageDel(' + PageId + ')"><img  src="../images/dels.gif" style="border:0px;vertical-align:middle;"/></a></td>';
                html += '   </tr>';
            }
        }
    }
    html += ' </table>';
    if (Website.Get(WebsiteObj.Page.PageContainerId)) {
        Website.Get(WebsiteObj.Page.PageContainerId).innerHTML = html;
    }
    var windowheight = document.body.clientHeight;
    if (windowheight < 480)
        Website.Get("divright").style.height = "480px";
    else
        Website.Get("divright").style.height = "100%";
}
///
WebsiteObj.Page.PageAdd = function() {
    window.location.href = "PageAdd.aspx";
}

WebsiteObj.Page.PageDel = function(PageId) {
    if (!confirm("您确定要删除吗？")) return false;

    if (!WebsiteObj.Page.Ajax) return;

    WebsiteObj.Page.Ajax.async = false;
    WebsiteObj.Page.Ajax.get(
	    WebsiteObj.Page.PagePath + '?command=PageDel&PageId=' + PageId,
	    function(req) {
        if (req.responseText == "1") {
            Website.Get("td" + PageId).innerHTML = "";
            Website.Get("tr" + PageId).style.display = "none";
        }
        else {
            alert("删除失败！")
        }
    })
}
