<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCBank.ascx.cs" Inherits="UserControl_UCBank" %>
<table id="tbBank" border="0" cellpadding="0px" cellspacing="0" style="font-size :9pt">
<tr>
<td><%= tran.GetTran("000047", "国家")%></td>
<td>
<asp:DropDownList ID="DropDownList1" runat="server" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList></td>
<td>
<td><%=tran.GetTran("000087", "银行")%></td>
<td>
<asp:DropDownList ID="ddlBank" runat="server" bgcolor="#EBF1F1">
                            </asp:DropDownList>
                            </td>
</tr>
</table>

                            