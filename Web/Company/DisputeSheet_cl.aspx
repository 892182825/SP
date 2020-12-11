<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisputeSheet_cl.aspx.cs" Inherits="Company_DisputeSheet_cl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" />
    <script src="../JS/jquery-1.2.6.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="400px" class="tablemb" align="center">
            <tr>
                <th colspan="3" align="center">
                    纠纷单处理
                </th>
            </tr>
            <tr>
                <td align="right" width="100px">
                    单号：
                </td>
                <td align="left" width="100px">
                    <asp:Label ID="lbldh" runat="server" Text=""></asp:Label>
                </td>
                <td width="200px">
                    <a id="telephone" href="javascript:showphone();" runat="server">显示电话号码</a>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    买入方：
                </td>
                <td align="left">
                    <asp:Label ID="lblhfname" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="过错" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    卖出方：
                </td>
                <td align="left">
                    <asp:Label ID="lblsfname" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="过错" />
                </td>
            </tr>
            <tr id="dz" runat="server">
                <td colspan="2" align="center">
                    <asp:RadioButtonList ID="rbldz" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">撤销</asp:ListItem>
                        <asp:ListItem Value="2">到账</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:TextBox ID="txtmoney" runat="server"></asp:TextBox> 石斛积分
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="执行" CssClass="anyes" OnClick="Button1_Click" />
                </td>
                <td align="center">
                    <asp:Button ID="Button2" runat="server" Text="暂不执行" CssClass="anyes" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
<script>
    function showphone() {
        var str = $("#HiddenField1").val();
        var phone = $("#telephone").text();
        if(phone=="显示电话号码")
            $("#telephone").text(str);
        else
            $("#telephone").text("显示电话号码");
    }
</script>