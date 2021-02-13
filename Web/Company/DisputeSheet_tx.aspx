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
                   
                    卖出人账号：<asp:TextBox ID="txtsnumber" runat="server"></asp:TextBox>
                    委托时间：<asp:TextBox ID="txtbeigandate" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                    至<asp:TextBox ID="txtenddate" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                    交易状态：
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="-2">全部</asp:ListItem>
                      <asp:ListItem Value="0">待交易</asp:ListItem>
                        <asp:ListItem Value="1">交易成功</asp:ListItem>  
                        <asp:ListItem Value="-1">已撤销</asp:ListItem> 
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="98%" align="center" class="tablemb">
            <tr>
                <th>操作</th>
                <th>卖出人账号</th>
                <th>姓名</th>
                <th>星币类型</th>
                <th>卖出星币</th>
                 <th>卖出价格</th>
                <th>委托时间</th>
                <th>交易状态</th>
                  <th>交易时间</th>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server"  OnItemCommand="Repeater1_ItemCommand"   >
                <ItemTemplate>
                    <tr align="center">
                    <td style="border-bottom:1px solid #88E0F4;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirm('是否撤销？');" Visible='<%# Eval("shenhestate").ToString()=="0"?true:false %>' CommandArgument='<%# Eval("id") %>' CommandName="cs">撤销</asp:LinkButton>
                        </td> 
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("mobiletele") %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("tname") %></td>
                         <td style="border-bottom:1px solid #88E0F4;"><%#getCoinType( Eval("actype").ToString())  %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# Convert.ToDouble(Eval("InvestJB")).ToString("f4") %></td>  
                        <td style="border-bottom:1px solid #88E0F4;"><%# Convert.ToDouble(Eval("pricejb")).ToString("f4") %></td> 

                        <td style="border-bottom:1px solid #88E0F4;"><%# Eval("WithdrawTime") !=DBNull.Value ? Convert.ToDateTime(Eval("WithdrawTime")).ToString("yyyy-MM-dd HH:mm:ss") :"" %></td>
                        <td style="border-bottom:1px solid #88E0F4;"><%# getstate(Eval("shenhestate").ToString()) %></td>

                        <td style="border-bottom:1px solid #88E0F4;"><%#getstatedate(Eval("shenhestate").ToString() ,  Eval("AuditingTime")) %></td>
                    </tr>
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
