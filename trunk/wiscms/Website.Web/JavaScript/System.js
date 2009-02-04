var WebsiteObj = new Object();
WebsiteObj.System = new Object();
WebsiteObj.System.Ajax = new AJAXRequest;
WebsiteObj.System.TagPath = '/AjaxRequests/System.aspx';
WebsiteObj.System.TagContainerId = 'BetTag';
WebsiteObj.System.PagerId = 'Pager';
WebsiteObj.System.ContainerId = "RepeaterHolder";

var PageIndex = 1;
var responseText;
WebsiteObj.System.LoadData = function(pageIndex) {

    if (!WebsiteObj.System.Ajax) return;
    var pager = new Pager('PagerHolder');
    var count;
    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.TagPath + '?command=TagsCount',
	    function(req) {
        count = req.responseText; // 定义总记录数(必要)
    })
    pager.RecordCount = count;
    pager.ReloadFunction = 'WebsiteObj.System.LoadData';
    pager.PageIndex = pageIndex;
    pager.Method = 'Ajax';
    pager.PageSize = 20;
    pager.CreateHtml(1);
    //    WebsiteObj.System.Ajax.get(WebsiteObj.System.Path + '?page=' + pageIndex + '&pagesize=' + pager.PageSize, WebsiteObj.System.DataReceived);
    //    //    WebsiteObj.System.DataReceived(pager)
    PageIndex = pageIndex;
    WebsiteObj.System.Load(pageIndex);

}
WebsiteObj.System.Load = function(pageIndex) {

    if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/)) // 记忆滚动条位置
        document.body.scrollTop = parseInt(arr[1]);
    if (WebsiteObj.System.Ajax) {
        WebsiteObj.System.Ajax.get(WebsiteObj.System.TagPath + "?command=TagsList&PageIndex=" + pageIndex, WebsiteObj.System.LoadXml);
    }

}
WebsiteObj.System.LoadXml = function(req) {

    if (req.readyState != 4) return;
    if (req.status != 200) return;
    var html;
    var article = "System";


    html = ' <table cellpadding="0" cellspacing="0" class="schTable" id="System" name="System">';
    html += '  <tr class="cap">';
    html += ' <td width=30></td>';
    html += ' <td width=250>名称</td>';
    html += '  <td width=200>描述</td>';
    html += ' <td width=80>创建时间</td>';
    html += '  <td width=150>操作</td>';
    html += '  </tr>';

    var doc = req.responseXML.documentElement;
    if (doc.selectSingleNode("/L") == null) return;
    var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
    if (childNodesLength == 0) {
        html += '  <tr><td colspan=5><span class="red">无记录</span></td></tr>';
        html += ' </table>';
        if (Website.Get(WebsiteObj.System.TagContainerId)) {
            Website.Get(WebsiteObj.System.TagContainerId).innerHTML = html;
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
                var TemplateLabelList = getAttribute('V');
                var TagLists = TemplateLabelList.split(":");
                var TagId = TagLists[0];
                var TagName = TagLists[1];
                var Description = TagLists[2];
                var DateCreated = TagLists[3];
                var i = index + 1;
                html += '  <tr id="tr' + TagId + '">';
                html += '    <td id="td' + TagId + '">' + i + '</td>';
                html += '    <td class="alignLeft"><a href="TemplateLabelUpdate.aspx?TagId=' + TagId + '">' + TagName + '</a></td>';
                html += '     <td>' + Description + '</td>';
                html += '     <td>' + DateCreated + '</td>';
                html += '      <td valign=middle><a href="TemplateLabelUpdate.aspx?TagId=' + TagId + '"><img  src="../images/edit.gif" style="border:0px;vertical-align:middle;"/></a><a href="#" onclick="WebsiteObj.System.TagDel(' + TagId + ')"><img  src="../images/dels.gif" style="border:0px;vertical-align:middle;"/></a></td>';
                html += '   </tr>';
            }
        }
    }


    html += ' </table>';
    if (Website.Get(WebsiteObj.System.TagContainerId)) {
        Website.Get(WebsiteObj.System.TagContainerId).innerHTML = html;
    }
    var windowheight = document.body.clientHeight;
    if (windowheight < 480)
        Website.Get("divright").style.height = "480px";
    else
        Website.Get("divright").style.height = "100%";
}
///
WebsiteObj.System.TemplateLabelAddNew = function() {
window.location.href = "TemplateLabelAddNew.aspx";
}
WebsiteObj.System.TagDel = function(TagId) {
    if (!confirm("您确定要删除吗？")) return false;

    if (!WebsiteObj.System.Ajax) return;

    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.TagPath + '?command=TagDel&TagId=' + TagId,
	    function(req) {
        if (req.responseText == "1") {
            Website.Get("td" + TagId).innerHTML = "";
            Website.Get("tr" + TagId).style.display = "none";
        }
        else {
            alert("删除失败！")
        }
    })
}





//////////////////////////////////////////////
WebsiteObj.System.CategoryLoadData = function(pageIndex) {

    if (!WebsiteObj.System.Ajax) return;
    var pager = new Pager('PagerHolder');
    var count;
    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.TagPath + '?command=CategoryCount',
	    function(req) {
        count = req.responseText; // 定义总记录数(必要)
    })
    pager.RecordCount = count;
    pager.ReloadFunction = 'WebsiteObj.System.CategoryLoadData';
    pager.PageIndex = pageIndex;
    pager.Method = 'Ajax';
    pager.PageSize = 20;
    pager.CreateHtml(1);
    //    WebsiteObj.System.Ajax.get(WebsiteObj.System.Path + '?page=' + pageIndex + '&pagesize=' + pager.PageSize, WebsiteObj.System.DataReceived);
    //    //    WebsiteObj.System.DataReceived(pager)
    PageIndex = pageIndex;
    WebsiteObj.System.CategoryLoad(pageIndex);

}
WebsiteObj.System.CategoryLoad = function(pageIndex) {

    if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/)) // 记忆滚动条位置
        document.body.scrollTop = parseInt(arr[1]);
    if (WebsiteObj.System.Ajax) {
        WebsiteObj.System.Ajax.get(WebsiteObj.System.TagPath + "?command=CategoryList&PageIndex=" + pageIndex, WebsiteObj.System.CategoryLoadXml);
    }

}
WebsiteObj.System.CategoryLoadXml = function(req) {

    if (req.readyState != 4) return;
    if (req.status != 200) return;
    var html;
    var article = "System";


    html = ' <table cellpadding="0" cellspacing="0" class="schTable" id="System" name="System">';
    html += '  <tr class="cap">';
    html += ' <td width=30></td>';
    html += ' <td width=250>栏目名称</td>';
    html += '  <td width=200>父栏目</td>';
    html += ' <td width=80>排序</td>';
    html += '  <td width=150>操作</td>';
    html += '  </tr>';

    var doc = req.responseXML.documentElement;
    if (doc.selectSingleNode("/L") == null) return;
    var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
    if (childNodesLength == 0) {
        html += '  <tr><td colspan=5><span class="red">无记录</span></td></tr>';
        html += ' </table>';
        if (Website.Get("BetCategory")) {
            Website.Get("BetCategory").innerHTML = html;
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
                var CategoryList = getAttribute('V');
                var CategoryLists = CategoryList.split(":");
                var CategoryId = CategoryLists[0];
                var CategoryName = CategoryLists[1];
                var ParentId = CategoryLists[2];
                var ParentName = CategoryLists[3];
                var Rank = CategoryLists[4];
                html += '  <tr id="tr' + CategoryId + '">';
                html += '    <td id="td' + CategoryId + '"></td>';
                html += '    <td class="alignLeft"><a href="CategoryUpdate.aspx?CategoryId=' + CategoryId + '">' + CategoryName + '</a></td>';
                html += '     <td>' + ParentName + '</td>';
                html += '     <td>' + Rank + '</td>';
                html += '      <td valign=middle><a href="CategoryUpdate.aspx?CategoryId=' + CategoryId + '"><img  src="../images/edit.gif" style="border:0px;vertical-align:middle;"/></a><a href="#" onclick="WebsiteObj.System.CategoryDel(' + CategoryId + ')"><img  src="../images/dels.gif" style="border:0px;vertical-align:middle;"/></a></td>';
                html += '   </tr>';
            }
        }
    }


    html += ' </table>';
    if (Website.Get("BetCategory")) {
        Website.Get("BetCategory").innerHTML = html;
    }
    var windowheight = document.body.clientHeight;
    if (windowheight < 480)
        Website.Get("divright").style.height = "480px";
    else
        Website.Get("divright").style.height = "100%";
}
///
WebsiteObj.System.CategoryAdd = function() {
    window.location.href = "CategoryAdd.aspx";
}
WebsiteObj.System.CategoryDel = function(CategoryId) {
    if (!confirm("您确定要删除吗？")) return false;
    if (!WebsiteObj.System.Ajax) return;

    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.TagPath + '?command=CategoryDel&CategoryId=' + CategoryId,
	    function(req) {
        if (req.responseText == "1") {
            Website.Get("td" + CategoryId).innerHTML = "";
            Website.Get("tr" + CategoryId).style.display = "none";
        }
        else {
            alert("删除失败！")
        }
    })
}
//

WebsiteObj.System.LinkLoadData = function(pageIndex) {

    if (!WebsiteObj.System.Ajax) return;
    var pager = new Pager('PagerHolder');
    var count;
    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.TagPath + '?command=LinkCount',
	    function(req) {
        count = req.responseText; // 定义总记录数(必要)
    })
    pager.RecordCount = count;
    pager.ReloadFunction = 'WebsiteObj.System.LinkCount';
    pager.PageIndex = pageIndex;
    pager.Method = 'Ajax';
    pager.PageSize = 20;
    pager.CreateHtml(1);
    //    WebsiteObj.System.Ajax.get(WebsiteObj.System.Path + '?page=' + pageIndex + '&pagesize=' + pager.PageSize, WebsiteObj.System.DataReceived);
    //    //    WebsiteObj.System.DataReceived(pager)
    PageIndex = pageIndex;
    WebsiteObj.System.LinkLoad(pageIndex);

}
///
WebsiteObj.System.LinkLoad = function(pageIndex) {

    if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/)) // 记忆滚动条位置
        document.body.scrollTop = parseInt(arr[1]);
    if (WebsiteObj.System.Ajax) {
        WebsiteObj.System.Ajax.get(WebsiteObj.System.TagPath + "?command=LinkList&PageIndex=" + pageIndex, WebsiteObj.System.LinkLoadXml);
    }
}
WebsiteObj.System.LinkLoadXml = function(req) {

    if (req.readyState != 4) return;
    if (req.status != 200) return;
    var html;

    html = ' <table cellpadding="0" cellspacing="0" class="schTable" id="System" name="System">';
    html += '  <tr class="cap">';
    html += ' <td width=30></td>';
    html += ' <td width=250>名称</td>';
    html += '  <td width=200>连接地址</td>';
    html += '  <td width=80>Logo图片</td>';
    html += ' <td width=80>排序</td>';
    html += '  <td width=150>操作</td>';
    html += '  </tr>';

    var doc = req.responseXML.documentElement;
    if (doc.selectSingleNode("/L") == null) return;
    var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
    if (childNodesLength == 0) {
        html += '  <tr><td colspan=5><span class="red">无记录</span></td></tr>';
        html += ' </table>';
        if (Website.Get("BetLink")) {
            Website.Get("BetLink").innerHTML = html;
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
                var LinkList = getAttribute('V');
                var LinkLists = LinkList.split(",");
                var LinkId = LinkLists[0];
                var LinkName = LinkLists[1];
                var LinkUrl = LinkLists[2];
                var Logo = LinkLists[3];
                var Sequence = LinkLists[4];
                html += '  <tr id="tr' + LinkId + '">';
                html += '    <td id="td' + LinkId + '"></td>';
                html += '    <td class="alignLeft"><a href="LinkUpdate.aspx?LinkId=' + LinkId + '">' + LinkName + '</a></td>';
                html += '     <td><a href="' + LinkUrl + '">' + LinkUrl + '</a></td>';
                html += '     <td><img  src="' + Logo + '" width=60 height=32   style="border:0;"/></td>';
                html += '     <td>' + Sequence + '</td>';
                html += '      <td valign=middle><a href="LinkUpdate.aspx?LinkId=' + LinkId + '"><img  src="../images/edit.gif" style="border:0px;vertical-align:middle;"/></a><a href="#" onclick="WebsiteObj.System.LinkDel(' + LinkId + ')"><img  src="../images/dels.gif" style="border:0px;vertical-align:middle;"/></a></td>';
                html += '   </tr>';
            }
        }
    }
    html += ' </table>';
    if (Website.Get("BetLink")) {
        Website.Get("BetLink").innerHTML = html;
    }
    var windowheight = document.body.clientHeight;
    if (windowheight < 480)
        Website.Get("divright").style.height = "480px";
    else
        Website.Get("divright").style.height = "100%";
}

WebsiteObj.System.LinkDel = function(LinkId) {
    if (!confirm("您确定要删除吗？")) return false;

    if (!WebsiteObj.System.Ajax) return;

    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.TagPath + '?command=LinkDel&LinkId=' + LinkId,
	    function(req) {
        if (req.responseText == "1") {
            Website.Get("td" + LinkId).innerHTML = "";
            Website.Get("tr" + LinkId).style.display = "none";
        }
        else {
            alert("删除失败！")
        }
    });
}
WebsiteObj.System.LinkAdd = function() {
    window.location.href = "LinkAdd.aspx";
}
WebsiteObj.System.linkhtml = function() {
  
    if (!WebsiteObj.System.Ajax) return;

    WebsiteObj.System.Ajax.async = false;
    WebsiteObj.System.Ajax.get(
	    WebsiteObj.System.TagPath + '?command=linkhtml',
	    function(req) {
        if (req.responseText == "1") {
            alert("生成成功！")
        }
        else {
            alert("生成失败！")
        }
    });
}