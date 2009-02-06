var WebsiteObj = new Object();
WebsiteObj.Meeting = new Object();
WebsiteObj.Meeting.Ajax = new AJAXRequest;
WebsiteObj.Meeting.MeetingPath = '/AjaxRequests/Meeting.aspx';
WebsiteObj.Meeting.MeetingContainerId = 'BetMeeting';
WebsiteObj.Meeting.PagerId = 'Pager';
WebsiteObj.Meeting.Path = "employees.ashx";
WebsiteObj.Meeting.ContainerId = "RepeaterHolder";

var responseText;
WebsiteObj.Meeting.LoadData = function(pageIndex) {
    if (!WebsiteObj.Meeting.Ajax) return;
    var pager = new Pager('PagerHolder');
    var count;
    WebsiteObj.Meeting.Ajax.async = false;
    WebsiteObj.Meeting.Ajax.get(
	    WebsiteObj.Meeting.MeetingPath + '?command=Count',
	    function(req) {
        count = req.responseText; // 定义总记录数(必要)
    })
    pager.RecordCount = count;
    pager.ReloadFunction = 'WebsiteObj.Meeting.LoadData';
    pager.PageIndex = pageIndex;
    pager.Method = 'Ajax';
    pager.PageSize = 19;
    pager.CreateHtml(1);
    WebsiteObj.Meeting.Ajax.get(WebsiteObj.Meeting.Path + '?page=' + pageIndex + '&pagesize=' + pager.PageSize, WebsiteObj.Meeting.DataReceived);
    WebsiteObj.Meeting.Load(pageIndex);
}
WebsiteObj.Meeting.DataReceived = function(request) {
    if (request.readyState != 4) return;
    if (request.status != 200) return;
    if (document.getElementById('DataHolder'))
        document.getElementById('DataHolder').innerText = request.responseText;
}
WebsiteObj.Meeting.Load = function(pageIndex) {

    if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/)) // 记忆滚动条位置
        document.body.scrollTop = parseInt(arr[1]);
    if (WebsiteObj.Meeting.Ajax) {
        WebsiteObj.Meeting.Ajax.get(WebsiteObj.Meeting.MeetingPath + "?command=MeetingList&PageIndex=" + pageIndex, WebsiteObj.Meeting.LoadXml);
    }
}
WebsiteObj.Meeting.LoadXml = function(req) {
    if (req.readyState != 4) return;
    if (req.status != 200) return;




    var html;
    html = '';
    var doc = req.responseXML.documentElement;
    if (doc.selectSingleNode("/L") == null) return;
    var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
    var node;

    for (var index = 0; index < childNodesLength; index++) {
        node = doc.selectSingleNode("/L/A[" + index + "]");


        if (node) {
            with (node) {
                var MeetingList = getAttribute('V');
                var MeetingLists = MeetingList.split(";");

                var eformsn = MeetingLists[0];
                var txtZhuChi = MeetingLists[1];
                var txtEndDate = MeetingLists[2];
                var txtRenShu = MeetingLists[3];
                var txtDate = MeetingLists[4]; // MeetingId
                var txtAddress = MeetingLists[5];
                var txtTitle = MeetingLists[6];
                var txtshenqingren = MeetingLists[7];
                var deptName = MeetingLists[8];

                html += '<li><span class="time">' + txtDate + '</span><a href="MeetingInfo.html?id=' + eformsn + '" target=_blank>' + txtTitle + ' [会议室：' + txtAddress + '] [预定部门：' + deptName + ']</a></li>'
            }
        }
    }
    if (childNodesLength < 19) {
        for (var i = 0; i < 19 - childNodesLength; i++)
            html += '<li> </li>';
    }
    if (Website.Get(WebsiteObj.Meeting.MeetingContainerId)) {
        Website.Get(WebsiteObj.Meeting.MeetingContainerId).innerHTML = html;
    }
}


///
WebsiteObj.Meeting.LoadXmlIndex = function(req) {
    if (WebsiteObj.Meeting.Ajax) {
        WebsiteObj.Meeting.Ajax.get(WebsiteObj.Meeting.MeetingPath + "?command=MeetingListIndex",
        function(req) {
            if (req.readyState != 4) return;
            if (req.status != 200) return;
            var html;
            html = '';
            var doc = req.responseXML.documentElement;
            if (doc.selectSingleNode("/L") == null) return;
            var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
            var node;

            for (var index = 0; index < childNodesLength; index++) {
                node = doc.selectSingleNode("/L/A[" + index + "]");


                if (node) {
                    with (node) {
                        var MeetingList = getAttribute('V');
                        var MeetingLists = MeetingList.split(";");

                        var ID = MeetingLists[0];
                        var txtZhuChi = MeetingLists[1];
                        var txtEndDate = MeetingLists[2];
                        var txtRenShu = MeetingLists[3];
                        var txtDate = MeetingLists[4]; // MeetingId
                        var txtAddress = MeetingLists[5];
                        var txtTitle = MeetingLists[6];
                        var txtshenqingren = MeetingLists[7];
                        var deptName = MeetingLists[8];
                        html += '<li><a href="hyxx/MeetingInfo.html?id=' + ID + '" target=_blank title="' + txtTitle + '">' + txtDate + ' | ' + txtTitle + '</a></li>';
                    }
                }
            }
            if (childNodesLength < 7) {
                for (var i = 0; i < 7 - childNodesLength; i++)
                    html += '<li> </li>';
            }
            if (Website.Get("meeting")) {
                Website.Get("meeting").innerHTML = html;
            }
        });
    }
}
///
WebsiteObj.Meeting.MeetingInfo = function() {

    var request = {
        QueryString: function(val) {
            var uri = window.location.search;
            var re = new RegExp("" + val + "\=([^\&\?]*)", "ig");
            return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
        },
        QueryStrings: function() {
            var uri = window.location.search;
            var re = /\w*\=([^\&\?]*)/ig;
            var retval = [];
            while ((arr = re.exec(uri)) != null)
                retval.push(arr[0]);
            return retval;
        },
        setQuery: function(val1, val2) {
            var a = this.QueryStrings();
            var retval = "";
            var seted = false;
            var re = new RegExp("^" + val1 + "\=([^\&\?]*)$", "ig");
            for (var i = 0; i < a.length; i++) {
                if (re.test(a[i])) {
                    seted = true;
                    a[i] = val1 + "=" + val2;
                }
            }
            retval = a.join("&");
            return "?" + retval + (seted ? "" : (retval ? "&" : "") + val1 + "=" + val2);
        }
    }
    if (WebsiteObj.Meeting.Ajax) {
        WebsiteObj.Meeting.Ajax.get(WebsiteObj.Meeting.MeetingPath + "?command=MeetingInfo&eformsn=" + request.QueryString("id"),
        function(req) {

            if (req.readyState != 4) return;
            if (req.status != 200) return;
            var html = '';
            var doc = req.responseXML.documentElement;
            if (doc.selectSingleNode("/L") == null) return;
            var childNodesLength = doc.selectSingleNode("/L").childNodes.length;
            var node;
            for (var index = 0; index < childNodesLength; index++) {
                node = doc.selectSingleNode("/L/A[" + index + "]");
                if (node) {
                    with (node) {
                        var MeetingInfo = getAttribute('V');
                        var MeetingInfos = MeetingInfo.split(";");
                        var eformsn = MeetingInfos[0];
                        var txtZhuChi = MeetingInfos[1];
                        var txtEndDate = MeetingInfos[2];
                        var txtRenShu = MeetingInfos[3];
                        var txtDate = MeetingInfos[4]; // MeetingId
                        var txtAddress = MeetingInfos[5];
                        var txtTitle = MeetingInfos[6];
                        var txtshenqingren = MeetingInfos[7];
                        var deptName = MeetingInfos[8];
                        var txtYuHuiRY = MeetingInfos[9];
                        var RichTextBox1 = MeetingInfos[10];
                        var Hits = MeetingInfos[11];
                        var Comments = MeetingInfos[12];
                        document.getElementById("Hits").innerHTML = "浏览次数：" + Hits;
                        document.getElementById("Comments").innerHTML = Comments;
                        document.getElementById("txtZhuChi").innerHTML = txtZhuChi;
                        document.getElementById("txtEndDate").innerHTML = txtEndDate;
                        document.getElementById("txtRenShu").innerHTML = txtRenShu;
                        document.getElementById("txtDate").innerHTML = txtDate;
                        document.getElementById("txtAddress").innerHTML = txtAddress;
                        document.getElementById("txtTitle").innerHTML = txtTitle;
                        document.getElementById("txtshenqingren").innerHTML = txtshenqingren;
                        document.getElementById("deptName").innerHTML = deptName;
                        document.getElementById("txtYuHuiRY").innerHTML = txtYuHuiRY;
                        document.getElementById("RichTextBox1").innerHTML = RichTextBox1;
                        document.getElementById("eformsn").value = eformsn;
                    }
                }
            }
        }
        );
    }
}
//
WebsiteObj.Meeting.CommentsLoad = function() {
    if (!document.getElementById("eformsn")) return;
    var eformsn = document.getElementById("eformsn").value;
    if (!WebsiteObj.Meeting.Ajax) return;
    WebsiteObj.Meeting.Ajax.async = false;
    WebsiteObj.Meeting.Ajax.get(WebsiteObj.Meeting.MeetingPath + '?command=CommentsLoad&eformsn=' + eformsn,
    function(req) {
        if (req.readyState != 4) return;
        if (req.status != 200) return;
        var html = "";
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

WebsiteObj.Meeting.CommentsInsert = function() {
    if (!document.getElementById("eformsn")) return;
    if (!document.getElementById("Title")) return;
    if (!document.getElementById("ContentHtml")) return;
    var eformsn = document.getElementById("eformsn").value;
    var Title = document.getElementById("Title").value.replace('&', '');
    var ContentHtml = document.getElementById("ContentHtml").value.replace('&', '');
    if (ContentHtml == "") {
        alert("评论内容为空！");
        return;
    }
    if (!WebsiteObj.Meeting.Ajax) return;
    WebsiteObj.Meeting.Ajax.async = false;
    WebsiteObj.Meeting.Ajax.get(
	    WebsiteObj.Meeting.MeetingPath + '?command=CommentsInsert&eformsn=' + eformsn + '&Title=' + escape(Title) + '&ContentHtml=' + escape(ContentHtml),
	    function(req) {
        if (req.responseText == "1") {
            alert("提交成功！谢谢您的参与");
            var Comments = document.getElementById("Comments").innerHTML;

            document.getElementById("Comments").innerHTML = parseInt(Comments) + 1;
            document.getElementById("Title").value = "";
            document.getElementById("ContentHtml").value = "";
            WebsiteObj.Meeting.CommentsLoad();
        }
        else
            alert("提交失败！");
    });

}
WebsiteObj.Meeting.CommentsCancel = function() {
    document.getElementById("Title").value = "";
    document.getElementById("ContentHtml").value = "";
}