<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowNetworkViewDP.aspx.cs" Inherits="Company_ShowNetworkViewDP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <style>A:link { FONT-SIZE: 12px }
	A:visited { FONT-SIZE: 12px }
	A:active { FONT-SIZE: 12px }
	A:hover { FONT-SIZE: 12px }
	BODY { FONT-SIZE: 12px }
	TD { FONT-SIZE: 12px }
	.ls { FONT-SIZE: 12px }
	.ls2 { FONT-SIZE: 16px }
	</style>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body >
		<form id="Form2" method="post" runat="server">
		<br />
		<br />
			<TABLE  class="tablemb"  width="100%">
				<TBODY>
					<TR >
						<TD >&nbsp;
							<asp:label id="lbl_msg" runat="server"><%=GetTran("000915", "显示层数")%></asp:label>
                            <asp:textbox id="TextBox1" runat="server" Width="43px"></asp:textbox>
                            <asp:dropdownlist id="DropDownList_Qishu" runat="server" Height="20px" 
                                Width="91px"></asp:dropdownlist>&nbsp;&nbsp;
							<asp:button id="Button1" runat="server" Text="显示"  
								  onclick="Button1_Click" CssClass="anyes"></asp:button>&nbsp;&nbsp;&nbsp;
							<asp:button id="Button2" runat="server" Text="回到顶部" CssClass=" anyes" 
								 onclick="Button2_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<A href="QueryNetworkViewDP.aspx" style="display:none"><U><%=GetTran("000421", "返回")%></U></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("000879", "以下图中红色代表新增人员")%><BR>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<TABLE id="Table1"  width="100%" border="1" cellpadding="0" cellspacing="0"  class="tablemb" >
				<tr>
					<td class="ls2" style="FONT-SIZE: 12pt" noWrap="noWrap" height="100%">
						<asp:Repeater ID="Repeater1" Runat="server">
							<ItemTemplate>
								<p style="margin-bottom: -25px"><%#DataBinder.Eval(Container.DataItem, "xinxi")%></p>
							</ItemTemplate>
						</asp:Repeater>
						<p><FONT></FONT>&nbsp;</p>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</html>
