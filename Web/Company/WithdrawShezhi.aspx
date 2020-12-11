<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WithdrawShezhi.aspx.cs" Inherits="Company_WithdrawShezhi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>

</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
    <div>
        <table width="300px" align="center" cellpadding="0" cellspacing="3" class="tablemb">
            <tr >
                <td>

                    <span><%=GetTran("008146","提现最低金额")%>：</span>
                    <asp:TextBox ID="WithdrawMinMoney" runat="server" Width="100px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
               <tr >
                <td>

                    <span><%=GetTran("008147","提现手续费比例")%>：</span>
                    <asp:TextBox ID="WithdrawSXF" runat="server" Width="100px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr style="white-space:nowrap">
                <td colspan="2" align="center"><br />                                               
                    <asp:Button ID="btnOK" runat="server" Text="确 定" CssClass="anyes" onclick="btnOK_Click"/>&nbsp;&nbsp;
                     <input type="button" id="butt_Query"value='<%=GetTran("000421","返回") %>' style="cursor:pointer" class="anyes" onclick="history.back()"/>              
                </td>
            </tr>             
        </table>    
    </div>
    </form>
    <%=msg %>
</body>
</html>
