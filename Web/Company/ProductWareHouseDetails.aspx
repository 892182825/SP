<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductWareHouseDetails.aspx.cs" Inherits="Company_ProductWareHouseDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>产品各仓库明细</title>
     <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form ID="form1" runat="server">
    <br />
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="500" class="biaozzi" align="center">
			<tr>
				<td colspan="2" align="center" ><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
			</tr>
			<tr >
				<td >
					<asp:Label ID="lblCondition" runat="server">Label</asp:Label>：
					<asp:TextBox ID="txtCondition" runat="server" Width="300"></asp:TextBox>
					
					<asp:DropDownList ID="ddlWareHouse" runat="server"></asp:DropDownList>
				</td>
				<td >
					<asp:Button ID="btnStore" runat="server" Text="按服务机构汇总" onclick="btnStore_Click" CssClass="another"  />&nbsp;&nbsp;
					<asp:Button ID="btnStock" runat="server" Text="仓库汇总" onclick="btnStock_Click" CssClass="another" />&nbsp;&nbsp;
					<asp:Button ID="btnProductWareHouseDetails" runat="server" Text="产品仓库明细" 
                        onclick="btnProductWareHouseDetails_Click" CssClass="another" />&nbsp;&nbsp;
					<asp:Button ID="btnProductStoreDetails" runat="server"  Text="产品店铺明细" 
                        onclick="btnProductStoreDetails_Click" CssClass="another" />&nbsp;&nbsp;
					<asp:Button ID="btnImage" runat="server" Text="图形分析" onclick="btnImage_Click" CssClass="another" style="cursor:pointer"/>
				</td>
			</tr>
			<tr>
			    <td colspan="2"><p style="color:Red; padding-left:60px;"><%=GetTran("007586","多个产品查询时，产品编码之间用 ； 分隔开，例如：P001;P002")%></p></td>
			</tr>
		</table>						
	</div>
    </form>
</body>
</html>
