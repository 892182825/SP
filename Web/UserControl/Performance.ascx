<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Performance.ascx.cs" Inherits="UserControl_Performance" %>

<style type="text/css">
     .yjtab2
        {
        	width:100%;
        	height:100%;
        	border-left:rgb(176,176,176) solid 1px;
        	border-top:rgb(176,176,176) solid 1px;
        }
        
        .yjtab2 td
        {
        	line-height:30px;
        	border-right:rgb(176,176,176) solid 1px;
        	border-bottom:rgb(176,176,176) solid 1px;
        }
</style>
<table  border="0" cellspacing="0" cellpadding="0" class="yjtab2">

    	  <tr>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left">
               <asp:Label ID="CurrentOneMark" runat="server" Text="0.00"></asp:Label></td>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="CurrentTotalNetRecord" runat="server" Text="0.00"></asp:Label></td>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="CurrentNewNetNum" runat="server" Text="0.00"></asp:Label></td>
  	    </tr>
    	  <tr>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="labTotalNetNum" runat="server" Text="0.00"></asp:Label></td>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="labDTotalNetNum" runat="server" Text="0.00"></asp:Label></td>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"><asp:Label ID="labTotalNetRecord1" runat="server" Text="0.00"></asp:Label></td>
  	    </tr>
    	  <tr>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="labCurrentOneMark1" runat="server" Text="0.00"></asp:Label></td>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="labFxlj" runat="server" Text="0.00"></asp:Label></td>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"><asp:Label ID="labCurrentTotalNetRecord" runat="server" Text="0.00"></asp:Label></td>
  	    </tr>
    	  <tr>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="labdCurrentTotalNetRecord2" runat="server" Text="0.00"></asp:Label></td>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="labdTotalNetRecord2" runat="server" Text="0.00"></asp:Label></td>
    	    <td align="right" style="font-weight:bold;">xxxx：</td>
    	    <td align="left"> <asp:Label ID="storeid" runat="server" Text="0.00"></asp:Label></td>
  	    </tr>
  	    <tr>
  	        <td align="right" style="font-weight:bold;">xxxx：</td>
  	        <td align="left"><asp:Label ID="level" runat="server" Text="0.00"></asp:Label></td>
  	        <td align="right"></td>
  	        <td align="left"></td>
  	        <td align="right"></td>
  	        <td align="left"></td>
  	    </tr>
  	  </table>