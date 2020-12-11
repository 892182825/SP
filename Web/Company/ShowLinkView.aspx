<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowLinkView.aspx.cs" Inherits="Company_ShowLinkView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="Stylesheet" type="text/css" />
</head>
<body topmargin="10" >
		<form id="Form1" method="post" runat="server">
		<br />
		<br />
			<TABLE id="Table2" width="100%" border="0" cellpadding="0" cellspacing="1">
				<tr>
					<td>
						<table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablemb">
							<TR >
								<TD  colSpan="2"><asp:dropdownlist id="DropDownList1" runat="server"></asp:dropdownlist>&nbsp;&nbsp;
									<asp:button id="Button1" runat="server" Text="显示" CssClass=" anyes"  onclick="Button1_Click"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:button id="Button2" runat="server" Text="回到顶部" CssClass=" anyes"  onclick="Button2_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<A href="javascript:window.history.back()"><U><%=GetTran("000421", "返回")%></U></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color="red"><%=GetTran("000879", "以下图中红色代表新增人员")%></font>
									<BR>
								</TD>
							</TR>
						</table>
						<table width="100%" border="1" cellpadding="0" cellspacing="0" class="tablemb">
							<tr>
								<td><FONT face="宋体"> &nbsp;</FONT>
									<asp:Repeater ID="Repeater1" Runat="server">
										<ItemTemplate>
											<p style="margin-bottom: -23">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem, "xinxi")%></p>
										</ItemTemplate>
									</asp:Repeater>
									<p><FONT face="宋体"></FONT>&nbsp;</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			
		</form>
	</body>
</html>
