<%@ Page Language="C#" AutoEventWireup="true" CodeFile="restpass.aspx.cs" Inherits="Company_restpass"  ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>restpass</title>
    <base target="_parent" />
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <script type="text/javascript" src="../bower_components/jquery/jquery.min.js"></script>
</head>
<body onload="javascript:onloadTxt()">
    <form id="form1" runat="server">
    <div>
    <br />
    <br />
    <table align="center"  width="50%" class="biaozzi">
    <tr>
        <th>
              <span><%=GetTran("000664", "密码重置")%> </span> 
            </th>
    </tr>
    <tr>
        <td>
            <span><asp:Literal ID="lit_number" runat="server"></asp:Literal></span>
            <span><asp:Literal ID="lit_name" runat="server"></asp:Literal></span>
        </td>
    </tr>
        <tr>
            <td>
                <asp:CheckBoxList ID="chkPass" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="one">一级密码</asp:ListItem>
                    <asp:ListItem Value="true">二级密码</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true" >
             <asp:ListItem Selected="True" Value="1" Text="重置为店铺编号">
            </asp:ListItem>
            <asp:ListItem Value="2" Text="产生一个随机密码通过邮件发送给用户">
            </asp:ListItem>
            <asp:ListItem  Value="3" Text="产生一个随机密码通过手机发送给用户">
            </asp:ListItem>
            <asp:ListItem Value="4" Text="手动填写密码">
            </asp:ListItem>
        </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lab1" runat="server" Visible="false"></asp:Label>
                <asp:TextBox ID="txt_1" runat="server" style="display:none" MaxLength ="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button CssClass="anyes" Text="确 定" runat="server"  ID="btn_ok" 
                    onclick="btn_ok_Click" OnClientClick="return yzEmail()"/>&nbsp;&nbsp;&nbsp;
                <input onclick="javascript:window.close();" type="button"  title='关闭' value ='<%=GetTran("000019","关闭")%>' class="anyes" />
            </td>
        </tr>
    </table>
    </div>
    </form>
    <script type="text/javascript" src="js/PassRest.js"></script>
    <script type="text/javascript" src="../JS/sryz.js"></script>
    <script type="text/javascript">
        var alt=[
            '<%=GetType() %>',
            '<%=GetTran("006685", "email格式不对！") %>'
        ]
    </script>
    <% =msg %>
</body>
</html>

