<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberAccount.aspx.cs" Inherits="Company_MemberAccount" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table cellpadding="0" cellspacing="0" class="biaozzi" width="100%">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="anyes" 
                        onclick="Button1_Click" />&nbsp;
                    <%=GetTran("000024", "会员编号")%>：<asp:TextBox ID="txtnumber" runat="server"></asp:TextBox>&nbsp;
                    <%=GetTran("000025","会员姓名")%>：<asp:TextBox ID="txtname" runat="server"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="Jackpot-Out">可用FTC账户</asp:ListItem>
                        <asp:ListItem Value="fuxiaoin-fuxiaoout">冻结FTC</asp:ListItem>
                        <asp:ListItem Value="pointBIn-pointBOut">报单账户</asp:ListItem>
                         <asp:ListItem Value="pointAIn-pointAOut">奖金账户（USDT）</asp:ListItem>
                        <asp:ListItem Value="zzye-xuhao">奖金账户（USDT）</asp:ListItem>
                        
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem Value="-1">不限</asp:ListItem>
                        <asp:ListItem Value=">">大于</asp:ListItem>
                        <asp:ListItem Value="<">小于</asp:ListItem>
                        <asp:ListItem Value="=">等于</asp:ListItem>
                        <asp:ListItem Value=">=">大于等于</asp:ListItem>
                        <asp:ListItem Value="<=">小于等于</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtmoney" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" class="tablemb" width="100%">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        Width="100%" onrowdatabound="GridView1_RowDataBound">
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="会员编号" />
                            <asp:BoundField DataField="name" HeaderText="会员姓名" />
                            <asp:BoundField DataField="kyjb" HeaderText="可用FTC账户" DataFormatString="{0:f4}" />
                            <asp:BoundField DataField="fx" HeaderText="冻结FTC" DataFormatString="{0:f4}" />
                            <asp:BoundField DataField="xf" HeaderText="报单账户" DataFormatString="{0:f4}" />
                             <asp:BoundField DataField="tzjb" HeaderText="奖金账户（USDT）" DataFormatString="{0:f4}" />
                            <asp:BoundField DataField="xfjf" HeaderText="消费积分" DataFormatString="{0:f4}" />
                            <asp:BoundField DataField="sczh" HeaderText="锁仓账户" DataFormatString="{0:f4}" />
                            <asp:BoundField DataField="sfsd" HeaderText="释放速度" DataFormatString="{0:f4}" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    
                    <uc1:Pager ID="Pager1" runat="server" />
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
