<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisputeSheet_tx.aspx.cs" Inherits="Company_DisputeSheet_tx" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table cellpadding="0" cellspacing="0" width="98%" align="center" class="biaozzi">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="anyes" OnClick="Button1_Click" />
                    单号：<asp:TextBox ID="txtdh" runat="server"></asp:TextBox>
                    买入人编号：<asp:TextBox ID="txtnumber" runat="server"></asp:TextBox>
                    卖出人编号：<asp:TextBox ID="txtsnumber" runat="server"></asp:TextBox>
                    提交时间：<asp:TextBox ID="txtbeigandate" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                    至<asp:TextBox ID="txtenddate" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                    是否撤销：
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="-2">全部</asp:ListItem>
                        <asp:ListItem Value="1,3,11">待查收</asp:ListItem>
                        <asp:ListItem Value="20,2">已查收</asp:ListItem>
                        <asp:ListItem Value="0">待撮合</asp:ListItem>
                        <asp:ListItem Value="-1">已撤销</asp:ListItem>
                        <asp:ListItem Value="98,99">已作废</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="98%" align="center" class="tablemb">
            <tr>
                <th>交易单号</th>
                <th>卖出人编号</th>
                <th>卖出人姓名</th>
                <th>卖出人账号</th>
                <th>卖出石斛积分</th>
                <th>买入人编号</th>
                <th>买入人姓名</th>
                <th>买入人账号</th>
               
               
                <th>提交时间</th>
                <th>状态</th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr align="center">
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("remittancesid") %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("number") %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("tname") %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("winfo") %></td>
                 <td style="border-bottom:1px solid #88E0F4;"><%# Convert.ToDouble(Eval("InvestJB")).ToString("f2") %></td>
                        
                         <td style="border-bottom:1px solid #88E0F4;"><%# Eval("rnumber") %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("rname") %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("rinfo")  %></td>
                        
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("WithdrawTime") !=DBNull.Value ? DateTime.Parse(Eval("WithdrawTime").ToString()).AddHours(8).ToString() :"" %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# getstate(Eval("shenhestate").ToString()) %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="9">

                    <uc1:Pager ID="Pager1" runat="server" />

                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
