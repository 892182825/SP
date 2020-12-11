<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageCascade.aspx.cs" Inherits="Company_MessageCascade" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 tdansitional//EN" "http://www.w3.org/td/xhtml1/Dtd/xhtml1-tdansitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>

<style>
.anyes {
	font-size: 12px;
	color: #FFFFFF;
	text-decoration: none;
	background-image: url('images/anliudp000.GIF');
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
        <table width="100%" border="1" cellspacing="0" cellpadding="2"> 
			
			<tr>
			<td>
			<asp:Table ID="Table1" runat="server">
                </asp:Table>
			</td>
			</tr>		
			<tr>
			    <td colspan="2" align="center">
                    &nbsp;<asp:Button 
                        ID="Button2" runat="server" onclick="Button2_Click" Text="返回" class="anyes"/>
                </td>
			</tr>	
		</table>
    </form>
</body>
</html>
