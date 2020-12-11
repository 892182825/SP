<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductStock1.aspx.cs" Inherits="Company_ProductStock" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head>
		<title>
			<%=msg%>
		</title>
		<link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
	</head>
	<body style="padding:0px;margin:0px;">
		<form id="Form1" runat="server">
		<table class="biaozzi" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td align="center" height="40"><font style="FONT-WEIGHT: bold; FONT-SIZE: 18px;"><asp:label id="lbl_title" runat="server"></asp:label><%=GetTran("001947","各产品库存明细表")%></font></td>
			</tr>
			<tr>
				<td height="30">&nbsp;&nbsp;<asp:label id="lbl_flag" runat="server" Font-Bold="True"></asp:label>&nbsp;&nbsp;
					<asp:label id="lbl_storename" runat="server" Font-Bold="True"></asp:label></td>
			</tr>
			<tr>
				<td>				
				    <asp:GridView id="gvProduct" runat="server" AutoGenerateColumns="False" 
                        Width="100%" onrowdatabound="gvProduct_RowDataBound" CssClass="tablemb" >
						<AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />                 
						<Columns>
						    <asp:TemplateField HeaderText="序号" ItemStyle-Wrap="false">
						        <ItemTemplate>						        
						            <asp:Label id="lbl_code" runat="server"></asp:Label>						            
						        </ItemTemplate>						        
						    </asp:TemplateField>
							<asp:BoundField DataField="Productcode" HeaderText="产品编码" ItemStyle-Wrap="false" />
							<asp:BoundField DataField="ProductName" HeaderText="产品名称" ItemStyle-Wrap="false" />
							<asp:BoundField DataField="ProductUnitName" HeaderText="单位" ItemStyle-Wrap="false" />
							<asp:TemplateField HeaderText="单价" ItemStyle-Wrap="false">
						        <ItemTemplate>						        
						            <asp:Label ID="Label1" runat="server" Text='<%# getstr(DataBinder.Eval(Container, "DataItem.Price").ToString()) %>'></asp:Label>					            
						        </ItemTemplate>						        
						    </asp:TemplateField>
							<asp:BoundField DataField="TotalIn" HeaderText="入库数量" ItemStyle-Wrap="false" />
							<asp:BoundField DataField="TotalOut" HeaderText="出库数量" ItemStyle-Wrap="false" />
							<asp:BoundField DataField="TotalEnd" HeaderText="实际数量" ItemStyle-Wrap="false" />
							<asp:BoundField DataField="Alertnesscount" HeaderText="在途数量" ItemStyle-Wrap="false" />							
						</Columns>
					</asp:GridView>				
				</td>
			</tr>					
		</table>
		</form>
	</body>
</HTML>
