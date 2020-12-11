<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryLNetworkView.aspx.cs" Inherits="Company_QueryLNetworkView" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</head>
<body >
        <div align=center>
		<form id="Form1" method="post" runat="server">
			<table  class="tablemb" >
				
					</br>
					</br>
					</br>
					
						<tr align="center" >
							<th  colspan="2"><strong><%=GetTran("000459", "链路网络")%></strong>&nbsp;
							</th>
						</tr>
						<tr>
					        <td  align="right" ><%=GetTran("000369", "可查看网络")%>：</td>
					        <td align="left" >
						        <asp:DropDownList ID="ddlTeam" runat="server">
						        </asp:DropDownList>
					        </td>
				        </tr>
						<tr>
							<td align="right"><%=GetTran("000373", "网络起点编号")%>：</td>
							<td align="left"><asp:textbox id="txtBox_GLBH" runat="server"></asp:textbox></td>
						</tr>
						<tr>
							<td align="right"><%=GetTran("000375", "要查看的期数")%>：</td>
							<td align="left"><asp:dropdownlist id="DropDownList_QiShu" runat="server"></asp:dropdownlist></td>
						</tr>
						<tr>
							<td align="right"><%=GetTran("000376", "选择结构类型")%>：</td>
							<td align="left">
								<asp:radiobuttonlist id="RadioButtonList_Type" runat="server" 
                                    RepeatDirection="Horizontal" RepeatLayout="Flow" 
                                    onselectedindexchanged="RadioButtonList_Type_SelectedIndexChanged" 
                                    AutoPostBack="True">
									<asp:ListItem Value="llt_az">安置链路图</asp:ListItem>
									<asp:ListItem Value="llt_tj" Selected="True">推荐链路图</asp:ListItem>
								</asp:radiobuttonlist></td>
						</tr>
						<tr>
							<td colspan="2"><asp:button id="btn_Submit" runat="server" Text="确定" CssClass=" anyes" 
									onclick="btn_Submit_Click"></asp:button></td>
						</tr>
					
				
				</table>
		
		</form>
		<%=msg %>
		</div>
	</body>
</html>
