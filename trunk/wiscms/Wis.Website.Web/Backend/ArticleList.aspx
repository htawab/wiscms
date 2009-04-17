<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleList" %>

<%@ Register assembly="Wis.Toolkit" namespace="Wis.Toolkit.WebControls" tagprefix="Wis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <link href="images/MessageBox/MessageBox.css" rel="stylesheet" type="text/css" />
    <script src="images/MessageBox/MessageBox.js" language="javascript" type="text/javascript"></script>
    <script src="wis.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript">
        function Search_ClientClick(IsKey) {
            var args = location.search;
            var reg = new RegExp('[\?&]?CategoryGuid=([^&]*)[&$]?', 'gi');
            var chk = args.match(reg);
            var categoryGuid = RegExp.$1;

            var keywords = $('SearchKeywords');4
            if(keywords.value=="")
            {
                alert("请输入关键字");
                return false;
            }
            var targetpath = location.protocol + "//" + location.host + location.pathname;
            var queryPath = targetpath + '?CategoryGuid=' + categoryGuid +  '&Keywords=' + escape(keywords.value);
            if(!IsKey)
                window.location = queryPath;
            else{    
                var link = $("Search");
                link.href = queryPath;
                link.click();
            }
        }  
    </script>
</head>
<body style="background: #d6e7f7">
<form runat="server" id="ArticleListForm">
    <div class="right">
        <div id="Position">
            所在位置：
            <asp:SiteMapPath ID="MySiteMapPath" runat="server" PathSeparator=" » ">
                <PathSeparatorStyle Font-Bold="True" ForeColor="#5D7B9D" />
                <CurrentNodeStyle ForeColor="#333333" />
                <NodeStyle Font-Bold="True" ForeColor="#7C6F57" />
                <RootNodeStyle Font-Bold="True" ForeColor="#5D7B9D" />
            </asp:SiteMapPath>
        </div>
        <div class="listBox artcleList" id="listBox">
            <div class="AticleSch">
                <label>搜索内容：</label><input id="SearchKeywords" type="text" value="<%=Request["Keywords"] %>" onkeydown="if(event.keyCode==13){Search_ClientClick(true);return false;}"/><A id="linkSearch" href="#" target="_blank"><IMG id="imgSearchButton" onclick="Search_ClientClick(false);return false;" alt="搜索" src="images/schbtn.gif" /></A>
                <a href='ArticleAddNew.aspx?CategoryGuid=<% =Request["CategoryGuid"] %>' class="addNews">添加新闻</a>
                <div class="clear"></div>
            </div>
            <div id="Warning" runat="server"></div>
            <asp:Repeater ID="RepeaterArticleList" runat="server">
            <HeaderTemplate>
                <ul class="top" style="background-color:#c6daed">
                    <li class="listSort">分类(<a href='ArticleList.aspx'>所有</a>)</li>
                    <li class="listTitle">标题</li>
                    <li class="listTime">时间</li>
                    <li class="listUser">作者</li>
                    <li class="listOperate">操作</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul style="background-color:#e0e9f5">
                    <li class="listSort"><a href='ArticleList.aspx?CategoryGuid=<%# DataBinder.Eval(Container.DataItem, "Category.CategoryGuid")%>'><%# DataBinder.Eval(Container.DataItem, "Category.CategoryName")%></a></li>
                    <li class="listTitle"><a href='<%=ReleaseDirectory %>/<%# DataBinder.Eval(Container.DataItem, "Category.CategoryId")%>/<%# DataBinder.Eval(Container.DataItem, "DateCreated.Year")%>-<%# DataBinder.Eval(Container.DataItem, "DateCreated.Month")%>-<%# DataBinder.Eval(Container.DataItem, "DateCreated.Day")%>/<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>.htm' target='_blank'><%# DataBinder.Eval(Container.DataItem, "Title")%></a></li>
                    <li class="listTime"><%# DataBinder.Eval(Container.DataItem, "DateCreated")%></li>
                    <li class="listUser"><%# DataBinder.Eval(Container.DataItem, "Author")%></li>
                    <li class="listOperate Operate"><a href='<%=ReleaseDirectory %>/<%# DataBinder.Eval(Container.DataItem, "Category.CategoryId")%>/<%# DataBinder.Eval(Container.DataItem, "DateCreated.Year")%>-<%# DataBinder.Eval(Container.DataItem, "DateCreated.Month")%>-<%# DataBinder.Eval(Container.DataItem, "DateCreated.Day")%>/<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>.htm' target='_blank'>预览</a>&nbsp;<a href="ArticleUpdate.aspx?ArticleId=<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>">修改</a>&nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" OnClientClick="return confirm('确认删除吗?');" CommandName='<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>'  OnCommand="LinkButtonDelete_Click">删除</asp:LinkButton>&nbsp;<asp:LinkButton ID="LinkButtonRelease" runat="server" OnClientClick="return confirm('确认发布吗?');" CommandName='<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>'  OnCommand="LinkButtonRelease_Click">发布</asp:LinkButton></li>
                </ul>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <ul style="background-color:#f1f6fc">
                    <li class="listSort"><a href='ArticleList.aspx?CategoryGuid=<%# DataBinder.Eval(Container.DataItem, "Category.CategoryGuid")%>'><%# DataBinder.Eval(Container.DataItem, "Category.CategoryName")%></a></li>
                    <li class="listTitle"><a href='<%=ReleaseDirectory %>/<%# DataBinder.Eval(Container.DataItem, "Category.CategoryId")%>/<%# DataBinder.Eval(Container.DataItem, "DateCreated.Year")%>-<%# DataBinder.Eval(Container.DataItem, "DateCreated.Month")%>-<%# DataBinder.Eval(Container.DataItem, "DateCreated.Day")%>/<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>.htm' target='_blank'><%# DataBinder.Eval(Container.DataItem, "Title")%></a></li>
                    <li class="listTime"><%# DataBinder.Eval(Container.DataItem, "DateCreated")%></li>
                    <li class="listUser"><%# DataBinder.Eval(Container.DataItem, "Author")%></li>
                    <li class="listOperate Operate"><a href='<%=ReleaseDirectory %>/<%# DataBinder.Eval(Container.DataItem, "Category.CategoryId")%>/<%# DataBinder.Eval(Container.DataItem, "DateCreated.Year")%>-<%# DataBinder.Eval(Container.DataItem, "DateCreated.Month")%>-<%# DataBinder.Eval(Container.DataItem, "DateCreated.Day")%>/<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>.htm' target='_blank'>预览</a>&nbsp;<a href="ArticleUpdate.aspx?ArticleId=<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>">修改</a>&nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" OnClientClick="return confirm('确认删除吗?');" CommandName='<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>'  OnCommand="LinkButtonDelete_Click">删除</asp:LinkButton>&nbsp;<asp:LinkButton ID="LinkButtonRelease" runat="server" OnClientClick="return confirm('确认发布吗?');" CommandName='<%# DataBinder.Eval(Container.DataItem, "ArticleId")%>'  OnCommand="LinkButtonRelease_Click">发布</asp:LinkButton></li>
                </ul>
            </AlternatingItemTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            <div class="page" style="width: 786px;">
                <Wis:MiniPager ID="MiniPager1" runat="server"></Wis:MiniPager>
            </div>
        </div>
    </div>
</form>    
</body>
</html>