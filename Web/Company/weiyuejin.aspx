<%@ Page Language="C#" AutoEventWireup="true" CodeFile="weiyuejin.aspx.cs" Inherits="weiyuejin" %>

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
                    
                    会员编号：<asp:TextBox ID="txtnumber" runat="server"></asp:TextBox>
                   
                    扣除时间：<asp:TextBox ID="txtbeigandate" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                    至<asp:TextBox ID="txtenddate" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                 
                </td>
            </tr>
        </table>


        <div style="height:20px; font-size:12px; margin-left:20px; padding:10px;">
            已处理违约金总计： <asp:Label ID="lblwyjzj" runat="server" Text="0.00"></asp:Label>
        </div>
        <table cellpadding="0" cellspacing="0" width="98%" align="center" class="tablemb">
            <tr>
                <th>会员编号</th>
                <th>违约金</th>  
                <th>扣除时间</th>
                  <th>备注</th>
                 
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr align="center">
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("Number") %></td>
                          <td style="border-bottom:1px solid #88E0F4;"><%# Convert.ToDouble( Eval("HappenMoney")).ToString("0.00") %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("HappenTime") %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("Remark") %></td> 
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
