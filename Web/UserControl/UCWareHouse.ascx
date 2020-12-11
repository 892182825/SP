<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCWareHouse.ascx.cs" Inherits="UserControl_UCWareHouse" %>
<table style="font-size:9pt;">
<tr>
<td><%=tran.GetTran("000386", "仓库")%></td>
<td> <asp:DropDownList ID="DropDownList1" runat="server" 
                         DataTextField="WareHouseName" DataValueField="WareHouseID" AutoPostBack="True" 
                         onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                         
                     </asp:DropDownList>                  
  </td>
<td><%=tran.GetTran("000390", "库位")%></td>
<td><asp:DropDownList ID="DropDownList2" runat="server" DataTextField="SeatName" DataValueField='DepotSeatID'> </asp:DropDownList></td>
</tr>
</table>