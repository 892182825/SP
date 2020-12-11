<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StrikeBalances2.aspx.cs"
    Inherits="Company_StrikeBalances2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">

	function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table align="center" width="500px" border="0" cellpadding="0" cellspacing="3" class="tablemb">
        <tr>
            <td align="right">
                <%=GetTran("001525", "请输入编号")%>：
            </td>
            <td>
                <asp:Label ID="Number" runat="server" MaxLength="10" Width="88px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <%=GetTran("001185", "输入金额")%>：
            </td>
            <td>
                <asp:Label ID="money" runat="server" Width="88px" MaxLength="20"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <%=GetTran("001526", "请输入原因")%>：
            </td>
            <td>
                <asp:TextBox ID="question" runat="server" Width="296px" TextMode="MultiLine" Height="128px"
                    MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="35">
            </td>
            <td>
                <font face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="提 交" OnClick="Button1_Click" CssClass="anyes"
                        Style="cursor: hand;"></asp:Button>&nbsp;
                    <input type="button" id="butt_Query" value='<%=GetTran("000421","返回") %>' style="cursor: pointer"
                        class="anyes" onclick="history.back()" />
                </font>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
