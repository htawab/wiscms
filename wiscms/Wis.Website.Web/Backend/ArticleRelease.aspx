<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleRelease.aspx.cs" Inherits="Wis.Website.Web.Backend.ArticleRelease" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <script src="Article/wis.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function Check() {
//            if ($("Photo$ctl03").value == "") {
//                alert("请先浏览图片");
//                return false;
//            }
            $("Loading").style.display = "";
            return true;
        }
    </script>    
</head>
<body style="background: #d6e7f7"><form id="form1" runat="server">
    <div>
        <div id="Position">当前位置：添加新闻</div>
        <div class="add_step">
            <ul>
                <li>第一步：选择分类</li><li>第二步：基本信息</li>
                <li>第三步：扩展信息</li><li class="current_step">第四步：发布静态页</li>
            </ul>
        </div>
        <div class="add_main add_step4">
            信息录入完成，以下相关页面将被重新生成
            <asp:Repeater ID="RepeaterReleaseList" runat="server">
            <HeaderTemplate>
                <ul class="top">
                    <li class="step4_li4"><input type="checkbox" checked="checked" disabled="disabled" /></li>
                    <li class="step4_li1">标题</li>
                    <li class="step4_li2">模板路径</li>
                    <li class="step4_li3">发布路径</li>
                </ul>
            </HeaderTemplate>
            <ItemTemplate>
                <ul>
                    <li class="step4_li4"><input type="checkbox" checked="checked" disabled="disabled" /></li>
                    <li class="step4_li1" title="<%# DataBinder.Eval(Container.DataItem, "Title")%>"><%# DataBinder.Eval(Container.DataItem, "Title")%></li>
                    <li class="step4_li2" title="<%# DataBinder.Eval(Container.DataItem, "Template.TemplatePath")%>"><%# DataBinder.Eval(Container.DataItem, "Template.TemplatePath")%></li>
                    <li class="step4_li3" title="<%# DataBinder.Eval(Container.DataItem, "ReleasePath")%>"><%# DataBinder.Eval(Container.DataItem, "ReleasePath")%></li>
                </ul>
            </ItemTemplate>
            </asp:Repeater>
            <div class="clear"></div>
        </div>
        <div id="Warning" runat="server"></div>
        <div id="Loading" style="display: none;"><img src='images/loading.gif' align='absmiddle' /> 上传中...</div>
        <div class="add_button">
            <asp:ImageButton ID="ImageButtonNext" runat="server" ImageUrl="images/nextStep.gif" onclick="ImageButtonNext_Click" OnClientClick="javascript:return Check();" />
        </div>
    </div></form>
</body>
</html>