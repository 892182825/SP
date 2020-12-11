<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreAccount.aspx.cs" Inherits="Company_StoreAccount" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
        <table cellpadding="0" cellspacing="0" class="biaozzi" width="100%">
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="anyes" 
                        onclick="Button1_Click" />&nbsp;
                    <%= GetTran("000037","服务机构编号")%>：<asp:TextBox ID="txtnumber" runat="server"></asp:TextBox>&nbsp;
                    <%=GetTran("000040","服务机构名称")%>：<asp:TextBox ID="txtname" runat="server"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="1">订货款余额</asp:ListItem>
                        <asp:ListItem Value="2">周转款余额</asp:ListItem>
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
                            <asp:BoundField DataField="storeid" HeaderText="服务机构编号" />
                            <asp:BoundField DataField="name" HeaderText="服务机构名称" />
                            <asp:BoundField DataField="DHK" HeaderText="订货款余额" DataFormatString="{0:f2}" />
                            <asp:BoundField DataField="ZZK" HeaderText="周转款余额" DataFormatString="{0:f2}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <table cellpadding="0" cellspacing="0" class="tablemb" width="100%">
                                <tr >
                                    <th><%#GetTran("000037", "服务机构编号")%></th>
                                    <th><%#GetTran("000040", "服务机构名称")%></th>
                                    <th><%#GetTran("000041", "总金额")%></th>
                                    <th><%#GetTran("007336", "订货款余额")%></th>
                                    <th><%#GetTran("008103", "周转款余额")%></th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
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