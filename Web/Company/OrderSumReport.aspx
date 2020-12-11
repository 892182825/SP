<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderSumReport.aspx.cs" Inherits="Company_OrderSumReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title> <%=msg%>
		</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		
		<style type="text/css">TABLE.colortest { BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid;font-size:10pt;color:rgb(0,85,117); }
	.colortest TD { BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid }
	</style>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" style="font-size:10pt;color:rgb(0,85,117);">
					<TR>
						<TD height="40" align="center"><font style="FONT-WEIGHT: bold; FONT-SIZE: 18px"> <%=GetTran("002215", "订单汇总表")%>(
								<asp:Label id="lbl_title" runat="server">Label</asp:Label>)</font></TD>
					</TR>
					<TR height="30">
						<TD>
							<table width="100%" style="font-size:10pt;color:rgb(0,85,117);">
								<tr>
								    
									<td width="40%" align="left"><%=GetTran("000669", "汇总区间")%>:<%=GetTran("000448", "从")%>
										<asp:Label id="lbl_Begin" runat="server">Label</asp:Label><%=GetTran("000205", "到")%>
										<asp:Label id="lbl_End" runat="server">Label</asp:Label></td>
									<td width="30%">
										<asp:Label id="lbl_message" runat="server">Label</asp:Label></td>
									<td width="30%" align="right"><%=GetTran("000562", "币种")%>:<%=GetTran("000563", "人民币")%>&nbsp;&nbsp;&nbsp; <%=GetTran("000518", "单位")%>:<%=GetTran("000564", "元")%>&nbsp;&nbsp;&nbsp;</td>
								</tr>
							</table>
							<asp:Literal id="Literal1" runat="server"></asp:Literal>
						</TD>
					</TR>
					<TR>
						<TD align="left">
							<%=GetTran("000565", "制表时间")%>:
							<asp:Label id="lbl_maketime" runat="server">Label</asp:Label></TD>
					</TR>
					<TR>
						<TD align="center"></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
