<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ok.aspx.cs" Inherits="Company_ok" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <base target="_self"></base>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="js/tianfeng.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tablemb">
            <tr>
                <td>
                    汇款金额：<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr style="display:none">
                <td>
                    匹配金额：<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr style="display:none">
                <td>
                    实汇金额：$<asp:TextBox ID="TextBox1" runat="server" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr style="display:none"><td>汇款凭证：<asp:Image ID="imgpingz" runat="server"  Width="100px" /> </td></tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" Text="确认收款" CssClass="anyes" 
                        onclick="Button1_Click" OnClientClick="return checkedcf('确定收到该笔汇款了吗?')" />
                </td>
            </tr>
        </table>
    </div>
        <div></div>
    </form>
</body>
</html>






