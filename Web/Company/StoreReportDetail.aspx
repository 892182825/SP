<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreReportdetail.aspx.cs" Inherits="Company_StoreReportDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= str %><%= GetTran("000671", "汇款报表")%></title>
    
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
		<style type="text/css">TABLE.colortest { BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid }
	.colortest TD { BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid }
		</style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
<table id="table1" cellspacing="0" cellpadding="0" class="biaozzi" width="100%" border="0">
						<tr>
							<td align="center" colSpan="2" height="30"><font style="FONT-WEIGHT: bold; FONT-SIZE: 18px;"><%=GetTran("000671", "汇款报表")%>(
									<asp:label id="lbl_title" runat="server"></asp:label>)</font></td>
						</tr>
						<tr height="20">
							<td align="left" width="30%"><%=GetTran("000669", "汇总区间")%>:<%=GetTran("000448", "从")%>
								<asp:label id="lbl_BeginDate" runat="server">Label</asp:label><%=GetTran("000205", "到")%>
								<asp:label id="lbl_EndDate" runat="server">Label</asp:label></td>
					
							<td align="right" width="30%"><%=GetTran("000562", "币种")%>:<%=GetTran("000563", "人民币")%>&nbsp;&nbsp;&nbsp;&nbsp; 
								<%=GetTran("000518", "单位")%>:<%=GetTran("000564", "元")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
						</tr>
						<tr>
							<td colSpan="2"><asp:literal id="Literal1" runat="server"></asp:literal></td>
						</tr>
						<tr>
							<td style="HEIGHT: 15px" colspan="3"></td>
						</tr>
						<tr>
							<td colSpan="2">
								<%=GetTran("000565", "制表时间")%>：<asp:label id="lbl_time" runat="server">Label</asp:label></td>
							
					</tr>
				</table>
    </form>
</body>
</html>
