/// <summary>
/// 初始化属性。
/// </summary>
function Pager(name) {
    this.Name = name;      //对象名称
    this.RecordCount = 0;  // 总记录数(必需项)
    this.PageSize = 10;    // 每页显示记录数
    this.ArgumentName = 'PageIndex'; //参数名
    this.ReloadFunction = ''; // 重载方法
    this.ShowTimes = 1;    //打印次数
    this.ControlID = null; // 装载容器
    this.PageIndex = 1;     // 当前页
    this.Method = 'Ajax';   // 分页方式，支持Ajax和Argument
}

/// <summary>
/// 总页数。
/// </summary>
Pager.prototype.GetPageCount = function() {
    var pageCount = this.RecordCount / this.PageSize + ((this.RecordCount % this.PageSize == 0) ? 0 : 1);
    if (isNaN(parseInt(pageCount))) pageCount = 1;
    if (pageCount < 1) pageCount = 1;
    return parseInt(pageCount);
}

/// <summary>
/// 当前页数。
/// </summary>
Pager.prototype.GetPageIndex = function() {
    if (this.Method == 'Argument') {
        var args = location.search;
        var reg = new RegExp('[\?&]?' + this.ArgumentName + '=([^&]*)[&$]?', 'gi');
        var chk = args.match(reg);
        var pageIndex = RegExp.$1;

        // 进行当前页数的验证
        if (isNaN(parseInt(pageIndex))) pageIndex = 1;
        if (pageIndex < 1) pageIndex = 1;

        // 进行总页数的验证
        var pageCount = this.GetPageCount();
        if (pageIndex > pageCount) pageIndex = pageCount;

        this.PageIndex = parseInt(pageIndex);
    }

    return this.PageIndex;
}

/// <summary>
/// 生成html代码。
/// </summary>
Pager.prototype.CreateHtml = function(mode) {

    var pageIndex = this.GetPageIndex();

    var pageCount = this.GetPageCount();

    if (pageCount <= 1) {
        document.getElementById(this.Name).innerHTML = "";
        return;
    }
    var html = '', prevPage = pageIndex - 1, nextPage = pageIndex + 1;
    if (mode == '' || typeof (mode) == 'undefined') mode = 0;
    switch (mode) {
        case 0: //模式1 (页数,首页,前页,后页,尾页)
            html += '<span class="PagerStat">第' + pageIndex + '页/共' + pageCount + '页</span>';
            html += '<span class="number">';
            if (prevPage < 1) {
                html += '<span title="首页">首页</span>';
                html += '<span title="上一页">上一页</span>';
            } else {
                html += '<span title="首页"><a href="' + this.CreateUrl(1) + '">首页</a></span>';
                html += '<span title="上一页"><a href="' + this.CreateUrl(prevPage) + '">上一页</a></span>';
            }
            for (var i = 1; i <= pageCount; i++) {
                if (i > 0) {
                    if (i == pageIndex) {
                        html += '<span title="第' + i + '页" class="current">' + i + '</span>';
                    } else {
                        html += '<span title="第' + i + '页"><a href="' + this.CreateUrl(i) + '">' + i + '</a></span>';
                    }
                }
            }
            if (nextPage > pageCount) {
                html += '<span title="下一页">下一页</span>';
                html += '<span title="尾页">尾页</span>';
            } else {
                html += '<span title="下一页"><a href="' + this.CreateUrl(nextPage) + '">下一页</a></span>';
                html += '<span title="尾页"><a href="' + this.CreateUrl(pageCount) + '">尾页</a></span>';
            }
            html += '</span><br />';
            break;
        case 1: //模式1 (10页缩略,首页,前页,后页,尾页)  // 27930页有问题
            html += '<span><span class="Pager">共' + this.RecordCount + '条记录&nbsp;第' + pageIndex + '页/共' + pageCount + '页</span>';
            if (prevPage < 1) {
            } else {
                html += '<a href="' + this.CreateUrl(1) + '" title="首页" class="Pager">首页</a>';
                html += '<a href="' + this.CreateUrl(prevPage) + '" title="上一页" class="Pager">上一页</a>';
            }
            if (pageIndex % 10 == 0) {
                var startPage = pageIndex - 9;
            } else {
                var startPage = pageIndex - pageIndex % 10 + 1;
            }
            if (startPage > 10) html += '<a href="' + this.CreateUrl(startPage - 1) + '" title="前10页" class="Pager">...</a>';
            for (var i = startPage; i < startPage + 10; i++) {
                if (i > pageCount) break;
                if (i == pageIndex) {
                    html += '<span title="第' + i + '页" class="Current">' + i + '</span>';
                } else {
                    html += '<a href="' + this.CreateUrl(i) + '" title="第' + i + '页" class="Pager">' + i + '</a>';
                }
            }
            if (pageCount >= startPage + 10) html += '<a href="' + this.CreateUrl(startPage + 10) + '" title="下10页" class="Pager">...</a>';
            if (nextPage > pageCount) {
            } else {
                html += '<a href="' + this.CreateUrl(nextPage) + '" title="下一页" class="Pager">下一页</a>';
                html += '<a href="' + this.CreateUrl(pageCount) + '" title="尾页" class="Pager">尾页</a>';
            }
            html += '</span>';
            //alert(html);
            break;
        case 2: //模式2 (前后缩略,页数,首页,前页,后页,尾页) 
            html += '<span class="count">第' + pageIndex + '页/共' + pageCount + '页</span>';
            html += '<span class="number">';
            if (prevPage < 1) {
                html += '<span title="首页">首页</span>';
                html += '<span title="上一页">上一页</span>';
            } else {
                html += '<span title="首页"><a href="' + this.CreateUrl(1) + '">首页</a></span>';
                html += '<span title="上一页"><a href="' + this.CreateUrl(prevPage) + '">上一页</a></span>';
            }
            if (pageIndex != 1) html += '<span title="Page 1"><a href="' + this.CreateUrl(1) + '">1</a></span>';
            if (pageIndex >= 5) html += '<span>...</span>';
            if (pageCount > pageIndex + 2) {
                var endPage = pageIndex + 2;
            } else {
                var endPage = pageCount;
            }
            for (var i = pageIndex - 2; i <= endPage; i++) {
                if (i > 0) {
                    if (i == pageIndex) {
                        html += '<span title="第' + i + '页">' + i + '</span>';
                    } else {
                        if (i != 1 && i != pageCount) {
                            html += '<span title="第' + i + '页"><a href="' + this.CreateUrl(i) + '">' + i + '</a></span>';
                        }
                    }
                }
            }
            if (pageIndex + 3 < pageCount) html += '<span>...</span>';
            if (pageIndex != pageCount) html += '<span title="第' + pageCount + '页"><a href="' + this.CreateUrl(pageCount) + '">' + pageCount + '</a></span>';
            if (nextPage > pageCount) {
                html += '<span title="下一页">下一页</span>';
                html += '<span title="尾页">尾页</span>';
            } else {
                html += '<span title="下一页"><a href="' + this.CreateUrl(nextPage) + '">下一页</a></span>';
                html += '<span title="尾页"><a href="' + this.CreateUrl(pageCount) + '">尾页</a></span>';
            }
            html += '</span><br />';
            break;
        case 3: //模式3 (箭头样式,首页,前页,后页,尾页) (only IE) 
            html += '<span class="count">第' + pageIndex + '页/共' + pageCount + '页</span>';
            html += '<span class="arrow">';
            if (prevPage < 1) {
                html += '<span title="首页">9</span>';
                html += '<span title="上一页">7</span>';
            } else {
                html += '<span title="首页"><a href="' + this.CreateUrl(1) + '">9</a></span>';
                html += '<span title="上一页"><a href="' + this.CreateUrl(prevPage) + '">7</a></span>';
            }
            if (nextPage > pageCount) {
                html += '<span title="下一页">8</span>';
                html += '<span title="尾页">:</span>';
            } else {
                html += '<span title="下一页"><a href="' + this.CreateUrl(nextPage) + '">8</a></span>';
                html += '<span title="尾页"><a href="' + this.CreateUrl(pageCount) + '">:</a></span>';
            }
            html += '</span><br />';

            break;
        case 4: //模式3 (箭头样式,首页,前页,后页,尾页) (only IE) 
            html += '<span class="count">第' + pageIndex + '页/共' + pageCount + '页</span>';
            html += '<span class="number">';
            if (prevPage < 1) {
                html += '<span title="首页">首页</span>';
                html += '<span title="上一页">上一页</span>';
            } else {
                html += '<span title="首页"><a href="' + this.CreateUrl(1) + '">首页</a></span>';
                html += '<span title="上一页"><a href="' + this.CreateUrl(prevPage) + '">上一页</a></span>';
            }
            if (nextPage > pageCount) {
                html += '<span title="下一页">下一页</span>';
                html += '<span title="尾页">尾页</span>';
            } else {
                html += '<span title="下一页"><a href="' + this.CreateUrl(nextPage) + '">下一页</a></span>';
                html += '<span title="尾页"><a href="' + this.CreateUrl(pageCount) + '">尾页</a></span>';
            }
            html += '</span><br />';

            break;
        case 5: //模式5 (下拉框)
            if (pageCount < 1) {
                html += '<select name="PagerSelect" disabled>';
                html += '<option value="0">No Pages</option>';
            } else {
                var chkSelect;
                html += '<select name="PagerSelect" onchange="self.location.href = this.options[this.selectedIndex].value;">';
                for (var i = 1; i <= pageCount; i++) {
                    if (pageIndex == i) chkSelect = ' selected="selected"';
                    else chkSelect = '';
                    html += '<option value="' + this.CreateUrl(i) + '"' + chkSelect + '>第' + i + '页/共' + pageCount + '页</option>';
                }
            }
            html += '</select>';
            break;
        case 6: //模式6 (输入框)
            html += '<span class="input">';
            if (pageCount < 1) {
                html += '<input type="text" name="PagerInput" value="No Pages" class="itext" disabled="disabled">';
                html += '<input type="button" name="go" value="GO" class="ibutton" disabled="disabled"></option>';
            } else {
                html += '<input type="text" value="Input Page:" class="ititle" readonly="readonly">';
                html += '<input type="text" id="pageInput' + this.ShowTimes + '" value="' + pageIndex + '" class="itext" title="Input page" onkeypress="return ' + this.Name + '.FormatInputPage(event);" onfocus="this.select()">';
                html += '<input type="text" value=" / ' + pageCount + '" class="icount" readonly="readonly">';
                html += '<input type="button" name="go" value="GO" class="ibutton" onclick="' + this.Name + '.GoPage(document.getElementById(\'pageInput' + this.ShowTimes + '\').value);">';
            }
            html += '</span>';

            break;
        default:
            html = '请指定正确的 mode: ' + mode;
            break;
    }

    // 如果总页数小于等于1，则不显示
    this.ShowTimes += 1;
    //document.write('<div id="pages_' + this.Name + '_' + this.ShowTimes + '" class="Pager"></div>');
    //document.getElementById('pages_' + this.Name + '_' + this.ShowTimes).innerHTML = html;
    document.getElementById(this.Name).innerHTML = html;
}

/// <summary>
/// 生成页面跳转url。
/// </summary>
Pager.prototype.CreateUrl = function(pageIndex) {
    if (isNaN(parseInt(pageIndex))) pageIndex = 1;
    if (pageIndex < 1) pageIndex = 1;

    // 进行总页数的验证
    var pageCount = this.GetPageCount();
    if (pageIndex > pageCount) pageIndex = pageCount;

    if (this.ReloadFunction == '') {
        var url = location.protocol + '//' + location.host;
        if (location.pathname.substr(0, 1) == '/')
            url += location.pathname;
        else
            url += '/' + location.pathname;

        var args = location.search;
        var reg = new RegExp('([\?&]?)' + this.ArgumentName + '=[^&]*[&$]?', 'gi');
        args = args.replace(reg, '$1');
        if (args == '' || args == null) {
            args += '?' + this.ArgumentName + '=' + pageIndex;
        } else if (args.substr(args.length - 1, 1) == '?' || args.substr(args.length - 1, 1) == '&') {
            args += this.ArgumentName + '=' + pageIndex;
        } else {
            args += '&' + this.ArgumentName + '=' + pageIndex;
        }
        return url + args;
    }
    else {
        return 'javascript:' + this.ReloadFunction + '(' + pageIndex + ')';
    }
}

/// <summary>
/// 跳转页面。
/// </summary>
Pager.prototype.GoPage = function(pageIndex) { //页面跳转
    var turnTo = 1;
    if (typeof (pageIndex) == 'object') {
        turnTo = pageIndex.options[pageIndex.selectedIndex].value;
    } else {
        turnTo = pageIndex;
    }
    self.location.href = this.CreateUrl(turnTo);
}

/// <summary>
/// 限定输入页数格式。
/// </summary>
Pager.prototype.FormatInputPage = function(e) {
    var key;
    var ie = navigator.appName == "Microsoft Internet Explorer" ? true : false;
    if (!ie)
        key = e.which;
    else
        key = event.keyCode;

    if (key == 8 || key == 46 || (key >= 48 && key <= 57))
        return true;
    else
        return false;
}