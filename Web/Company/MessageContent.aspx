<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageContent.aspx.cs" Inherits="Company_MessageContent" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 tdansitional//EN" "http://www.w3.org/td/xhtml1/Dtd/xhtml1-tdansitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
   <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>

<style>
.anyes {
	font-size: 12px;
	color: #FFFFFF;
	text-decoration: none;
	background-image: url('images/menudp.GIF');
	background-repeat: repeat-x;
	border: 1px solid #132022;
	cursor:pointer;
	font-family: Arial, Helvetica, sans-serif;
	}
	input
	{
		border-width:0px;
		}
</style>
<body>

    <form id="form1" runat="server">
        <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="0"> 
			<tr>
				<td align="left">
				
					
					<table border="0"  cellpadding="0" cellspacing="0" class="biaozzi">		
						<tr>
						    <td align="right" class="anyes"><%=GetTran("000613", "日期")%>：</td>						    
							<td style="border: 1px solid #132022; border-bottom:0px solid #132022">
							    <asp:TextBox id="Text_Date" runat="server" Width="390px" ReadOnly="true"  BorderStyle="None"></asp:TextBox>
						    </td>
						</tr>
						<tr>
						    <td align="right" class="anyes"><%=GetTran("000784", "接收者")%>：</td>							
							<td style="border: 1px solid #132022; border-bottom:0px solid #132022">
								<asp:TextBox id="Text_Recive" runat="server" Width="390px" ReadOnly="true" BorderStyle="None"></asp:TextBox>
							</td>
						</tr>
						<tr>
						    <td align="right" class="anyes"><%=GetTran("000781", "标题")%>：</td>
							<td style="border: 1px solid #132022; border-bottom:0px solid #132022">
								<asp:TextBox id="Text_Title" runat="server" Width="390px" ReadOnly="true" BorderStyle="None"></asp:TextBox>
							</td>
						</tr>
						<tr>
						    <td align="right" class="anyes"><%=GetTran("000779", "发送者")%>：</td>
							<td style="border: 1px solid #132022; border-bottom:0px solid #132022">
								<asp:TextBox id="Text_Send" runat="server" Width="390px" ReadOnly="true" BorderStyle="None"></asp:TextBox>
							</td>
						</tr>				
						<tr>
						    <td align="right" valign="top" class="anyes"> <%=GetTran("000013", "内容")%>：</td>
							<td>
							    <div style="WIDTH:680px;height:350px;border:gray solid 1px;overflow:auto;padding-left:5px;padding-top:5px;">
							    <span id="DetailSpan" runat="server" style="display:block; word-break: break-all;word-wrap:break-word" ></span>
							    </div>
							</td>
						</tr>
						<tr>
						    <td colspan="2" align="center">
						        <br>
                                &nbsp;<asp:Button 
                                    ID="Button2" runat="server" onclick="Button2_Click" Text="返回" class="anyes"/>
                            </td>
						</tr>
					</table>
				</td>
			</tr>				
		</table>
    </form>
</body>
</html>
