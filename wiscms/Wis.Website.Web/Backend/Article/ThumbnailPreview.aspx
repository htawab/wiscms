<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThumbnailPreview.aspx.cs" Inherits="Wis.Website.Web.Backend.dialog.Cutimg_view" %>
<html>
<head>
    <title></title>
    <style type="text/css">
    #tbHole img{width:1;height:1;}
    v\:*{Behavior:url(#default#VML)}
    </style>
</head>
<body onclick="HideMenu();" ondblclick="" oncontextmenu="ShowMenu(event);return false;"><form id="ThumbnailForm" runat="server">
<div id="bxHole" onclick="HideMenu();" onselectstart="return(false)" ondragstart="return(false)" onmousedown="return(false)" oncontextmenu="ShowMenu(event);return false;" style="position:absolute;left:0;top:0;width:<%=(ImageWidth + 4)%>;height:<%=(ImageHeight + 4)%>;border:1px solid #808080;background:url('<%=ImagePath %>')">
    <table id="tbHole" cellpadding="0" cellspacing="0" width="100%" height="100%" style="position:absolute">
        <tr height="<%=TopY %>">
            <td width="<%=TopX %>"><img></td>
            <td width="<%=ThumbnailWidth %>><img></td>
            <td><img></td>
        </tr>
        <tr height="<%=ThumbnailHeight %>">
            <td><img></td>
            <td onmousedown="$('bxHole').dragStart(event,0)" style="background:transparent;filter:;-moz-opacity:1;cursor:move;border:1px solid white !important"><img></td>
            <td><img></td>
        </tr>
        <tr>
            <td><img></td>
            <td><img></td>
            <td><img></td>
        </tr>
    </table>
    <img id="bxHoleMove1" src="../images/41_ie7ub8xprga8.gif" style="cursor:nw-resize;position:absolute;width:5;height:5;border:1px solid white;background:#BCBCBC">
    <img id="bxHoleMove2" src="../images/41_ie7ub8xprga8.gif" style="cursor:sw-resize;position:absolute;width:5;height:5;border:1px solid white;background:#BCBCBC">
    <img id="bxHoleMove3" src="../images/41_ie7ub8xprga8.gif" style="cursor:nw-resize;position:absolute;width:5;height:5;border:1px solid white;background:#BCBCBC">
    <img id="bxHoleMove4" src="../images/41_ie7ub8xprga8.gif" style="cursor:sw-resize;position:absolute;width:5;height:5;border:1px solid white;background:#BCBCBC">
</div>
<div id="resultTxtShow"></div>
<script>
function $(obj){
    return typeof(obj)=="object"?"obj":document.getElementById(obj);
}
bxHole_ini();
function bxHole_ini(){
    var bx=$("bxHole"),tb=$("tbHole")
    bx.w0=tb.rows[0].cells[1].offsetWidth;
    bx.h0=tb.rows[1].offsetHeight;
    bx.w_img=<%=ThumbnailWidth %>;
    bx.h_img=<%=ThumbnailHeight %>;
    bx.dragStart = function(e,dragType){
        bx.dragType=dragType;
        bx.px=tb.rows[0].cells[0].offsetWidth;
        bx.py=tb.rows[0].offsetHeight;
        bx.pw=tb.rows[0].cells[1].offsetWidth;
        bx.ph=tb.rows[1].offsetHeight;
        bx.sx=e.screenX;
        bx.sy=e.screenY;
    }
    bx.onmouseup=function(){
        if(bx.dragType==null)
            return;
        var w=tb.rows[0].cells[1].offsetWidth;
        var h=tb.rows[1].offsetHeight;
        bx.dragType=null;
        if(w/h>bx.w0/bx.h0)
            tb.rows[0].cells[1].style.width=h*bx.w0/bx.h0;
        else
            tb.rows[1].style.height=w*bx.h0/bx.w0;
        bx.setTip();
    }
    bx.onmousemove=function(e){
        var x,y,w,h;
        if(bx.dragType==null)
            return;
        if(e==null)
            e=event;
        x=Math.max(bx.px+e.screenX-bx.sx,1);
        y=Math.max(bx.py+e.screenY-bx.sy,1);
        w=Math.min(bx.pw+e.screenX-bx.sx,tb.offsetWidth-bx.px-1);
        h=Math.min(bx.ph+e.screenY-bx.sy,tb.offsetHeight-bx.py-1);
        if(bx.dragType==0){
            x=Math.min(x,tb.offsetWidth-bx.pw-1);
            y=Math.min(y,tb.offsetHeight-bx.ph-1);
            w=bx.pw;
            h=bx.ph;
        }
        if(bx.dragType==1||bx.dragType==4)
            w=bx.pw+bx.px-x;
        if(bx.dragType==1||bx.dragType==2)
            h=bx.ph+bx.py-y;
        if(bx.dragType==2||bx.dragType==3)
            x=bx.px;
        if(bx.dragType==3||bx.dragType==4)
            y=bx.py;
        w=Math.max(w,bx.w0);//最小宽，bx.w0/2表示一半
        h=Math.max(h,bx.h0);//最小高，bx.h0/2表示一半
        if(bx.dragType==1||bx.dragType==4)
            x=bx.pw+bx.px-w;
        if(bx.dragType==1||bx.dragType==2)
            y=bx.ph+bx.py-h;
        tb.rows[0].cells[0].style.width=x;
        tb.rows[0].cells[1].style.width=w;//改变宽
        tb.rows[0].style.height=y;
        tb.rows[1].style.height=h;//改变高
        $("bxHole").setTip();
    }
    bx.setTip=function(){
        var x = tb.rows[0].cells[0].offsetWidth;
        var y = tb.rows[0].offsetHeight;
        var w = tb.rows[0].cells[1].offsetWidth;
        var h = tb.rows[1].offsetHeight;
        var per;

        $("bxHoleMove1").style.left=$("bxHoleMove4").style.left=x-3;
        $("bxHoleMove1").style.top=$("bxHoleMove2").style.top=y-3;
        $("bxHoleMove2").style.left=$("bxHoleMove3").style.left=x+w-4;
        $("bxHoleMove3").style.top=$("bxHoleMove4").style.top=y+h-4;

        if(w/h>bx.w0/bx.h0)
            w=h*bx.w0/bx.h0;
        else
            h=w*bx.h0/bx.w0;
        per=bx.h0/h;
        
        $("x").value = Math.round((x-1));
        $("y").value = Math.round((y-1));
        $("w").value = Math.round(w);
        $("h").value = Math.round(h);
    }
}

function ShowMenu(e)
{
    var obj;
    var x, y;
    var docheight,docwidth,dh,dw; 

    e = e || window.event;
    x = e.clientX;
    y = e.clientY;

    obj = $('ThumbnailCut');
    docwidth   = document.body.clientWidth;
    docheight = document.body.clientHeight;

    dw = (obj.offsetWidth + x) - docwidth;
    dh = (obj.offsetHeight + y) - docheight;

    if(dw > 0)
    {
        obj.style.left = (x - dw) + document.body.scrollLeft + "px"; 
    }
    else
    {
        obj.style.left = x + document.body.scrollLeft + "px";
    }
    if(dh > 0)
    {
        obj.style.top = (y - dh) + document.body.scrollTop + "px"
    }
    else
    {
        obj.style.top   = y + document.body.scrollTop + "px";
    }

    obj.style.display = "";
}

function HideMenu()
{
    $('ThumbnailCut').style.display = "none";
} 
</script>
     <div id="ThumbnailCut" style="display:none">
        <asp:TextBox ID="x" runat="server" Width="27px"></asp:TextBox>
        <asp:TextBox ID="y" runat="server" Width="27px"></asp:TextBox>
        <asp:TextBox ID="w" runat="server" Width="33px"></asp:TextBox>
        <asp:TextBox ID="h" runat="server" Width="33px"></asp:TextBox>     
        <asp:LinkButton ID="LinkButtonCut" runat="server" onclick="LinkButtonCut_Click">裁减</asp:LinkButton>
    </div></form>
</body>
</html>