<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetNetTree.aspx.cs" Inherits="Company_SetNetTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SetWorldTimeByIP_Country</title>
    <link type="text/css" href="CSS/Company.css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" >
        function modifyReally()
        {
           return confirm('确定要修改吗？')
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
        <table border="0" cellpadding="0" cellspacing="3" class="tablemb" align="center" width="450px">
            <tr>
                <td colspan="3" align="center">网络图设置</td>
            </tr>
            <tr>
                <td style="white-space:nowrap" align="right">店铺可查看网络图：</td>
                <td style="white-space:nowrap">
                    <asp:CheckBoxList ID="cbList1" runat="server"></asp:CheckBoxList>
                </td>
                <td style="white-space:nowrap">
                    可查看层数：<asp:TextBox ID="TxtCengS" runat="server" MaxLength="6" Width="30px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td style="white-space:nowrap" align="right">会员可查看网络图：</td>
                <td style="white-space:nowrap">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                </td>
                <td style="white-space:nowrap">
                    可查看层数：<asp:TextBox ID="TextBox1" runat="server" MaxLength="6" Width="30px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button runat="server" ID="btnOk" Text="确 定" 
                        OnClientClick="return modifyReally()" CssClass="anyes" 
                        onclick="btnOk_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="butt_Query"value='<%=GetTran("000421","返回") %>' style="cursor:pointer" Class="anyes" onclick="history.back()"/>
                </td>
            </tr>
        </table>    
    </div>
    </form>
    <%=msg %>
</body>
</html>
