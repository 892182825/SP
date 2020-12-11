<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberPerformance.ascx.cs" Inherits="UserControl_MemberPerformance" %>

    	<table width="100%" border="0" cellspacing="1" cellpadding="0">
    	<tr id="tr1" class="ctConPgTab" runat="server">
    	    <td colspan="6">
    	        <%=BLL.Translation.Translate("007570", "个人业绩")%>
    	    </td>
    	</tr>
    	  <tr>
    	    <th align="right"><%=BLL.Translation.Translate("006062","新增人数")%>：</th>
    	    <td align="left">
               <asp:Label ID="lblNewPeople" runat="server" Text="0.00"></asp:Label></td>
    	    <th align="right"><%=BLL.Translation.Translate("000451","新增业绩")%>：</th>
    	    <td align="left"> <asp:Label ID="lblNewYeji" runat="server" Text="0.00"></asp:Label></td>
    	    <th align="right"><%=BLL.Translation.Translate("8133","新个复消")%>：</th>
    	    <td align="left"> <asp:Label ID="lblCurrentOneMark2" runat="server" Text="0.00"></asp:Label></td>
  	    </tr>
    	  <tr>
    	    <th align="right"><%=BLL.Translation.Translate("000933","总网人数")%>：</th>
    	    <td align="left"> <asp:Label ID="lblTotalPeople" runat="server" Text="0.00"></asp:Label></td>
    	    <th align="right"><%=BLL.Translation.Translate("007303","总网业绩")%>：</th>
    	    <td align="left"> <asp:Label ID="lblTotalYeji" runat="server" Text="0.00"></asp:Label></td>
    	    <th align="right"><%=BLL.Translation.Translate("8134","总个复消")%>：</th>
    	    <td align="left"><asp:Label ID="lblTotalOneMark2" runat="server" Text="0.00"></asp:Label></td>
  	    </tr>
  	  </table>