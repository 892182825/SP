<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetHolidays.aspx.cs" Inherits="Company_SetParams_SetHolidays" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交易时间</title>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <script src="../../javascript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../JS/jquery-1.2.6.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
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
                <tr>
                    <td align="right">开始时间</td>
                    <td>
                        <asp:TextBox ID="txtBox_OrderDateTimeStart"
                            runat="server" Width="160px" CssClass="Wdate" onfocus="new WdatePicker()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">结束时间</td>
                    <td>
                        <asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server" Width="160px"
                            onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">节假日：</td>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="10" Width="160px"> </asp:TextBox>
                    </td>
                </tr>
                <tr style="white-space: nowrap">
                    <td colspan="2" align="center">
                        <br />
                        <asp:Button ID="btnOK" runat="server" Text="确 定" OnClientClick="return ale()" OnClick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;
                    <asp:Button ID="lbtnReturn" runat="server" Text="返 回" CssClass="anyes" OnClick="lbtnReturn_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
