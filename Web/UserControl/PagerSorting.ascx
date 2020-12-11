<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagerSorting.ascx.cs" Inherits="UserControl_PagerSorting" %>



<table id="ShowTable" runat="server" height="30px;" width="100%" style="font-family: 宋体;	font-size: 12px;	line-height: 24px;	color: #005575;	text-decoration: none;">
    <tr>
        <td align="left" width="60%" nowrap="nowrap" >
            <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
        </td>
        <td nowrap="nowrap" align="right" >
            <table id="pageTable" runat="server"  style="font-family: 宋体;	font-size: 12px;	line-height: 24px;	color: #005575;	text-decoration: none;">
                <tr>
                    <td nowrap="nowrap">
                        <asp:ImageButton  ID="btnFirst" runat="server" 
                            ImageUrl="~/Company/images/one.gif" width="49" height="17" border="0" 
                            align="absmiddle" onclick="btnFirst_Click"  />
                    </td>
                    <td nowrap="nowrap">
                        <asp:ImageButton ID="btnPrevious" runat="server" 
                            ImageUrl="~/Company/images/up.gif" width="49" height="17" border="0" 
                            align="absmiddle" onclick="btnPrevious_Click" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:ImageButton ID="btnNext" runat="server" 
                            ImageUrl="~/Company/images/down.gif" width="49" height="17" border="0" 
                            align="absmiddle" onclick="btnNext_Click" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:ImageButton ID="btnGo" runat="server" ImageUrl="~/Company/images/last.gif" 
                            width="49" height="17" border="0" align="absmiddle" onclick="btnGo_Click"  />
                    </td>
                    <td nowrap="nowrap" valign="middle"><label>跳转到</label>
                        <asp:DropDownList ID="ddlPageList" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlPageList_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
