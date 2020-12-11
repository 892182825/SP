<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemBaseLine.aspx.cs" Inherits="Company_SetParams_MemBaseLine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("002173", "会员报单底线")%></title>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">
        function ale() {
            return confirm('<%=GetTran("002246", "确定要更改会员报单底线吗？")%>');
        }
        
        function CheckTextOK() {
            filterSql_II('lbtnOK');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
    <div>
        <table width="300px" align="center" cellpadding="0" cellspacing="3" class="tablemb">
            <tr >
                <td align="right"><%=GetTran("002173", "会员报单底线")%>：</td>
                <td><input type="text" id="txtMemBaseLine" maxlength="8" runat="server" value="0" onkeyup="value=value.replace(/[^\d]/g,'')" /></td>
            </tr>
            <tr style="white-space:nowrap">
                <td colspan="2" align="center"><br />                                               
                    <asp:Button ID="btnOK" runat="server" Text="确 定" OnClientClick="return ale()" onclick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;
                    <asp:Button ID="lbtnReturn" runat="server" Text="返 回" CssClass="anyes" onclick="lbtnReturn_Click" />               
                </td>
            </tr>             
        </table>    
    </div>
    </form>
</body>
</html>