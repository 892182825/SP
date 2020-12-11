<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetJjTx.aspx.cs" Inherits="Company_SetJjTx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MemBaseLine</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">
        function ale()
        {
            return confirm('<%=GetTran("002246", "确定要更改会员报单底线吗？")%>');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
    <div>
        <table width="300px" align="center" cellpadding="0" cellspacing="3" class="tablemb">
            <tr >
                <td align="right"><%=GetTran("007943","是否需要“提现申请”")%>：</td>
                <td><asp:RadioButtonList runat="server" ID="rbtS" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                </asp:RadioButtonList></td>
            </tr>
            <tr style="white-space:nowrap">
                <td colspan="2" align="center"><br />                                               
                    <asp:Button ID="btnOK" runat="server" Text="确 定" CssClass="anyes" 
                        onclick="btnOK_Click" />&nbsp;&nbsp;
                     <input type="button" ID="butt_Query"value='<%=GetTran("000421","返回") %>' style="cursor:pointer" Class="anyes" onclick="history.back()"/>              
                </td>
            </tr>             
        </table>    
    </div>
    </form>
    <%=msg %>
</body>
</html>
