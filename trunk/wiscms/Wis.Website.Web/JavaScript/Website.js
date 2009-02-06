var Website = new Object();
//=========================Website页面全局变量=======================//
Website.UserAgent = navigator.userAgent.toLowerCase();
Website.IsIE4 = (document.all && !document.getElementById) ? true : false;
Website.IsIE5 = (document.all && document.getElementById) ? true : false;
Website.IsIE6 = Website.UserAgent.search('msie') > 0 && navigator.userAgent.search('6') > 0;
Website.IsIE = Website.UserAgent.search('msie') > 0;

Website.IsNS = Website.UserAgent.indexOf('gecko') > -1;
Website.IsNS4 = (document.layers) ? true : false;
Website.IsNS6 = (!document.all && document.getElementById) ? true : false;

Website.IsOpera = Website.UserAgent.indexOf('opera') > -1;
Website.IsOpera8 = ((Website.UserAgent.indexOf('opera 8') != -1 || Website.UserAgent.indexOf('opera/8') != -1) ? 1 : 0);

Website.isMozilla = Website.UserAgent.indexOf('mozilla/5.') > -1;
Website.IsSF = Website.UserAgent.indexOf('safari') > -1;

/// <summary>
/// 根据ID获得元素。
/// </summary>
Website.Get = function(id) {
    if (Website.IsIE5 || Website.IsNS6 || Website.IsOpera) {
        return document.getElementById(id);
    } else if (Website.IsIE4) {
        return document.all[id];
    } else if (Website.IsNS4) {
        return document.layers[id];
    }
}

/// <summary>
/// 根据name获得元素。
/// </summary>
Website.GetElementsByTagName = function(name) {
    return window.document.getElementsByTagName ? window.document.getElementsByTagName(name) : new Array()
}

/// <summary>
/// 根据元素ID获取值。
/// </summary>
Website.GetValue = function(id) {
    var obj = Website.Get(id);
    var tag = obj.tagName;

    if (obj == null) return null;

    // 要判断是select还是input
    if (tag == "TEXTAREA")
        return obj.value;
    if (tag == "INPUT") {
        var type = obj.type;
        if (type == 'checkbox' || type == 'radio')
            return obj.checked;
        else
            return obj.value;
    }
    if (tag == "SELECT") {
        if (!obj.multiple)
            return obj.options[obj.selectedIndex].value;
        else {
            var returnValue = "";
            for (index = 0; index < obj.options.length; index++) {
                if (obj.options[index].selected)
                    returnValue += obj.options[index].value + ",";
            }
            if (returnValue.length > 0)
                returnValue = returnValue.substring(0, returnValue.length - 1);

            return returnValue;
        }
    }
}

/// <summary>
/// 清除前后空格。
/// </summary>
Website.Trim = function(s) {
    return s.replace(/(^\s*)|(\s*$)/g, "");
}

/// <summary>
/// 判断字符串是否为整型数字。
/// </summary>
Website.IsInt = function(s) {
    if (Website.Trim(s) == '') return false;

    var re = new RegExp('^([1-9]*[0-9]*)$');
    if (s.search(re) == -1) return false;

    if (s.length > 1)
        if (s.charAt(0) == '0')
        return false;

    return true;
}

/// <summary>
/// 判断字符串是否为Decimal。
/// </summary>
Website.IsDecimal = function(s) {
    if (Website.Trim(s) == '') return false;

    var re = new RegExp('^(\\+|-)?[0-9][0-9]*(\\.[0-9]*)?$'); // return s.match(/^\d+\.\d{2}$/);
    if (s.search(re) == -1) return false;

    if (s.length > 1)
        if (s.charAt(0) == '0')
        return false;

    return true;
}

/// <summary>
/// 将String类型解析为Date类型。
/// ParseDate('2006-1-1') return new Date(2006,0,1)
/// ParseDate(' 2006-1-1 ') return new Date(2006,0,1)
/// ParseDate('2006-1-1 15:14:16') return new Date(2006,0,1,15,14,16)
/// ParseDate(' 2006-1-1 15:14:16 ') return new Date(2006,0,1,15,14,16)
/// ParseDate('2006-1-1 15:14:16.254') return new Date(2006,0,1,15,14,16,254)
/// ParseDate(' 2006-1-1 15:14:16.254 ') return new Date(2006,0,1,15,14,16,254)
/// ParseDate('不正确的格式') retrun null
/// </summary>
Website.ParseDate = function(str) {
    if (typeof str == 'string') {
        var results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) *$/);

        if (results && results.length > 3)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]));

        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);
        if (results && results.length > 6)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]));

        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);
        if (results && results.length > 7)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]), parseInt(results[7]));
    }

    return null;
}

//========================Website.Odds类==============================//
// <TABLE cellSpacing="0" cellPadding="2" width="100%" border="0"><TBODY><TR><TD class="tabblack2" align="center" width="60" bgColor="#000000" height="25">开赛时间</TD><TD class="tabblack2" align="center" width="40" bgColor="#000000">得分</TD><TD class="tabblack2" align="center" bgColor="#000000">球队</TD><TD class="tabblack2" align="center" width="90" bgColor="#000000">亚洲盘</TD><TD class="tabblack2" align="center" width="90" bgColor="#000000">大小</TD><TD class="tabblack2" align="center" width="90" bgColor="#000000">亚洲盘(上半场)</TD><TD class="tabblack2" align="center" width="90" bgColor="#000000">大/小(上半场)</TD></TR><TR>
Website.Live = new Object();

/// <summary>
/// 重置 Live 表单的样式。
/// </summary>
Website.Live.ResetStyle = function() {
    // 
}

/// <summary>
/// 删除一条记录。
/// </summary>
Website.Live.DeleteTableRow = function(row) {
    var table = getTableByRow(row);
    with (table) {
        if (table.activeRow == row) {
            setAttribute("activeRow", null);
            setAttribute("activeCell", null);
        }
        var rowIndex = row.rowIndex;
        row.removeNode(true);
    }
}

/// <summary>
/// 插入一条记录。
/// </summary>
Website.Live.InsertTableRow = function(table, mode, row, empty) {
    if (!row) row = table.tBodies[0].rows[0];

    var newRow = table.repeatRow.cloneNode(!empty);
    switch (mode) {
        case "begin": 
            {
                table.tBodies[0].insertAdjacentElement("afterBegin", newRow);
                break;
            }
        case "before": 
            {
                row.insertAdjacentElement("beforeBegin", newRow);
                break;
            }
        case "after": 
            {
                row.insertAdjacentElement("afterEnd", newRow);
                break;
            }
        default: 
            {
                table.tBodies[0].insertAdjacentElement("beforeEnd", newRow);
                break;
            }
    }

    if (!_document_loading) resetDataTableStyle(table, newRow.rowIndex);
    return newRow;
}

Website.Live.SelectRecord = function(table, record) {
    var selectedRecords = table.getAttribute("selectedRecords");
    pArray_ex_insert(selectedRecords, record);
}

Website.Live.UnselectRecord = function(table, record) {
    var selectedRecords = table.getAttribute("selectedRecords");
    pArray_ex_delete(selectedRecords, record);
}

//var html = '<TABLE cellSpacing=0 cellPadding=0 width="100%" align=left border=0><TBODY><TR><TD style="BORDER-RIGHT: #efefef 1px solid; BORDER-TOP: #efefef 1px solid; BORDER-LEFT: #efefef 1px solid; BORDER-BOTTOM: #efefef 1px solid" vAlign=top align=left bgColor=#efefef>';
//html += '<TABLE cellSpacing=0 cellPadding=2 width=100% border=0><TBODY><TR><TD class=tabblack2 align=middle width=60 bgColor=#000000 height=25>开赛时间</TD><TD class=tabblack2 align=middle width=40 bgColor=#000000>得分</TD><TD class=tabblack2 align=middle bgColor=#000000>球队</TD><TD class=tabblack2 align=middle width=90 bgColor=#000000>亚洲盘</TD><TD class=tabblack2 align=middle width=90 bgColor=#000000>大小</TD><TD class=tabblack2 align=middle width=90 bgColor=#000000>亚洲盘(上半场)</TD><TD class=tabblack2 align=middle width=90 bgColor=#000000>大/小(上半场)</TD></TR>';
//html += '<TR><TD class=tabblack2 align=middle bgColor=#F5F5F6 colSpan=7 height=30>暂时没有可投注的滚球赔率，请稍候</TD></TR>';
//html += "</TBODY></TABLE>";
//html += "</TD></TR></TBODY></TABLE>";

//========================Website.Form类==============================//
Website.Form = new Object();

Website.Form.InputFocus = function(elements) {
    for (var i = 0; i < elements.length; i++) {
        elements[i].onfocus = function() {
            this.className += " InputFocus";
        }

        elements[i].onblur = function() {
            this.className = this.className.replace(new RegExp(" InputFocus\\b"), "");
        }
    }
}

Website.Form.InputBlur = function() {
    window.event.srcElement.runtimeStyle.backgroundColor = "";
}

Website.Form.AttachOnloadEvent = function(type, tag, parentId) {
    if (window.attachEvent) {
        window.attachEvent("onload", function() {
            var elements = (parentId == null) ? document.getElementsByTagName(tag) : document.getElementById(parentId).getElementsByTagName(tag);
            type(elements);
        });
    }
}

//Website.Form.AttachOnloadEvent(Website.Form.InputFocus, "INPUT");
//Website.Form.AttachOnloadEvent(Website.Form.InputFocus, "TEXTAREA");

//========================Website.MaskBox类==============================//
Website.MaskBox = new Object();

/// <summary>
/// 遮盖框。
/// </summary>
/// <param name="title">标题</param>
/// <param name="url">引用地址</param>
/// <param name="boxWidth">框宽度</param>
/// <param name="boxHeight">框高度</param>
/// <returns>遮盖成功返回True，否则返回False。</returns>
Website.MaskBox.Open = function() {
    if (arguments.length < 4) return false;

    var title = arguments[0];
    var box = arguments[1];
    var boxWidth = arguments[2];
    var boxHeight = arguments[3];

    var maskId = arguments[4];
    if (maskId == null || maskId == "") {
        var now = new Date();
        maskId = now.getTime();
    }

    Website.MaskBox.ScreenConvert();

    var html = "";
    html = html + '<TABLE border=0 align="center" cellPadding=0 cellSpacing=0>';
    html = html + '  <TBODY>';
    html = html + '    <TR>';
    html = html + '      <TD vAlign=top><DIV class=Block>';
    html = html + '        <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>';
    html = html + '          <TBODY>';
    html = html + '            <TR>';
    html = html + '              <TD align=center width=28 height=30><IMG height=17 src="images/itemHeader.gif" width=4 align=absMiddle></TD>';
    html = html + '              <TD class=Header>' + title + '</TD>';
    html = html + '              <TD align=center width=50 onClick="Website.MaskBox.Close();" style="cursor:hand"><IMG src="images/exit.gif" width="16" height="16" align=absMiddle>关闭</TD>';
    html = html + '            </TR>';
    html = html + '          </TBODY>';
    html = html + '        </TABLE>';
    html = html + '        <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>';
    html = html + '          <TBODY>';
    html = html + '            <TR>';
    html = html + '              <TD background="images/bgHeader.gif"><IMG height=4 src="images/spacer.gif" width=1></TD>';
    html = html + '            </TR>';
    html = html + '          </TBODY>';
    html = html + '        </TABLE>';
    html = html + '        <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>';
    html = html + '          <TBODY>';
    html = html + '            <TR>';
    html = html + '              <TD><IMG height=1 src="images/spacer.gif" width=1></TD>';
    html = html + '            </TR>';
    html = html + '          </TBODY>';
    html = html + '        </TABLE>';
    html = html + '        <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>';
    html = html + '          <TBODY>';
    html = html + '            <TR>';
    html = html + '              <TD vAlign="top" width="' + boxWidth + 'px" height="' + boxHeight + 'px">' + box + '</TD>';
    html = html + '              </TR>';
    html = html + '          </TBODY>';
    html = html + '        </TABLE>';
    html = html + '      </DIV></TD>';
    html = html + '    </TR>';
    html = html + '  </TBODY>';
    html = html + '</TABLE>';

    Website.MaskBox.DialogShow(html, 300, 180);

    return true;
}

Website.MaskBox.ScreenConvert = function() {
    var objScreen = Website.Get("ScreenOver");
    if (!objScreen)
        var objScreen = document.createElement("div");

    var oS = objScreen.style;
    objScreen.id = "ScreenOver";
    oS.display = "block";
    oS.top = oS.left = oS.margin = oS.padding = "0px";
    if (document.body.clientHeight) {
        var wh = document.body.clientHeight + "px";
    }
    else if (window.innerHeight) {
        var wh = window.innerHeight + "px";
    }
    else {
        var wh = "100%";
    }

    oS.width = "100%";
    oS.height = wh; oS.position = "absolute";
    oS.zIndex = "3";
    if ((!Website.IsSF) && (!Website.IsOpera)) {
        oS.background = "#181818";
    } else {
        oS.background = "#F0F0F0";
    }

    oS.filter = "alpha(opacity=40)";
    oS.opacity = 40 / 100;
    oS.MozOpacity = 40 / 100;
    document.body.appendChild(objScreen);
    var allselect = Website.GetElementsByTagName("select");
    for (var i = 0; i < allselect.length; i++)
        allselect[i].style.visibility = "hidden";
}


Website.MaskBox.DialogShow = function(showdata, leftX, topY) {
    var objDialog = Website.Get("DialogMove");
    if (!objDialog) objDialog = document.createElement("div");

    objDialog.id = "DialogMove";
    var oS = objDialog.style;
    oS.display = "block";
    oS.top = topY + "px";
    oS.left = leftX + "px";
    oS.margin = "0px";
    oS.padding = "0px";
    oS.position = "absolute";
    oS.zIndex = "5";
    oS.background = "#FFF";

    objDialog.innerHTML = showdata;
    document.body.appendChild(objDialog);
}


/// <summary>
/// 关闭遮盖框。
/// </summary>
Website.MaskBox.Close = function() {
    Website.MaskBox.ScreenClean();

    var objDialog = Website.Get("DialogMove");
    if (objDialog) objDialog.style.display = "none";
}

Website.MaskBox.ScreenClean = function() {
    var objScreen = Website.Get("ScreenOver");
    if (objScreen) objScreen.style.display = "none";

    var allselect = Website.GetElementsByTagName("select");
    for (var i = 0; i < allselect.length; i++)
        allselect[i].style.visibility = "visible";
}

/// <summary>
/// 将指定元素内的复选框的Checked状态跟指定复选框的Checked状态保持相同。
/// </summary>
/// <param name="cb">指定复选框</param>
/// <param name="elementName">元素名称</param>
function CheckedAllInElement(cb, elementName) {
    if (!document.getElementById(elementName)) return; // 没有找到元素
    var element = document.getElementById(elementName);
    var checkboxs = element.getElementsByTagName('input');
    for (var index = 0; index < checkboxs.length; index++) {
        var e = checkboxs[index];
        if (e.type == 'checkbox' && e.disabled == false) {
            e.checked = cb.checked;
        }
    }
}

function SelectColor(what, selectColor) {
    var oBackgroundColor = document.getElementById(what);

    var oSelectBackgroundColor = document.getElementById(selectColor);
    var url = "../../selcolor.htm?color=" + encodeURIComponent(oBackgroundColor.value);

    var returnValue = showModalDialog(url, window, "dialogWidth:350px;dialogHeight:350px;help:no;scroll:no;status:no");
    if (returnValue) {
        oBackgroundColor.value = returnValue;
        oSelectBackgroundColor.style.backgroundColor = returnValue;
    }
}
