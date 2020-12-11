<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowShouHuoWT.aspx.cs" Inherits="Company_ShowShouHuoWT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center"><br />
        <table  width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD" class="tablemb"  >
				<tr >
					<th background="images/tabledp.gif" class="tablebt" align="center" style="HEIGHT: 24px">
						<%=GetTran("002153", "查看收获问题")%>
					</th>
				</tr>
				<tr>
					<td><br>
						<br>
						<asp:Literal ID="Literal1" runat="server"></asp:Literal>
					</td>
				</tr>
				<tr>
					<td align="center"><br>
						<br>
						<br>
						</td>
				</tr>
			</table><br>
            <input type="button" ID="butt_Query" value='<%=GetTran("000096","返 回") %>'
                                            style="cursor:pointer" Class="anyes" onclick="history.back()"/>
    
    </div>
    </form>
</body>
</html>
