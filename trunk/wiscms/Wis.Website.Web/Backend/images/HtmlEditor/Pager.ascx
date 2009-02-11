<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="Wis.Website.Web.Admin.UserControls.Pager" %>
<table>
    <tr>
        <td style="width: 200px" runat="server" id="pagerSummary" >
        </td>
        <td style="width: 50px">
        
        </td>
        <td style="width: *">
        <asp:LinkButton runat="server" ID ="firstpage" Text="首页" OnClick="Firstpage_Click"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="prepage" Text="上一页" OnClick="Prepage_Click" ></asp:LinkButton> 
        <asp:LinkButton runat="server" ID ="nextpage" Text="下一页" OnClick="Nextpage_Click"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lastpage" Text="尾页" OnClick="Lastpage_Click" ></asp:LinkButton> 
        <asp:HiddenField runat="server" ID="currentpage" Value="1" /> 
         <asp:HiddenField runat="server" ID="hRecordCount" Value="0" /> 
        </td>
    </tr>
</table>
