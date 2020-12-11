<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberArea.aspx.cs" Inherits="Company_MemberArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("001483", "会员汇总表(省份)")%></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" style="zwidth: 576px; " cellspacing="0" cellpadding="0" width="576" border="0" class="biaozzi">
            <tr height="40">
                <td align="center">
                    <font style="FONT-STYLE: normal; FONT-SIZE: 20px; FONT-WEIGHT: bold"><%=GetTran("001483", "会员汇总表(省份)")%></font>
                </td>
            </tr>
            <tr height="25">
                <td>
                    <%=GetTran("001465", "查询时间:从")%> 
                    <asp:Label ID="lbl_Begin" runat="server">Label</asp:Label> <%=GetTran("000205", "到")%>
                    <asp:Label ID="lbl_End" runat="server">Label</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="1020px" style="word-break:keep-all;word-wrap:normal;"
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CssClass="tablemb"
                        onrowdatabound="GridView1_RowDataBound">
                       <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt"/>
                                <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                <font face="宋体">
                                    <asp:Label ID="lbl_code" runat="server">Label</asp:Label></font>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="province" HeaderText="省份" />
                            <asp:BoundField DataField="Fnum" HeaderText="期初" />
                            <asp:BoundField DataField="Bnum" HeaderText="新增" />     
                            <asp:BoundField DataField="Eum" HeaderText="期未" />      
                        </Columns>
                    </asp:GridView>
                  <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" Width="1020px" style="word-break:keep-all;word-wrap:normal;"
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CssClass="tablemb"
                        onrowdatabound="GridView1_RowDataBound">
                       <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt"/>
                                <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                <font face="宋体">
                                    <asp:Label ID="lbl_code" runat="server">Label</asp:Label></font>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Country" HeaderText="省份" />
                            <asp:BoundField DataField="Fnum" HeaderText="期初" />
                            <asp:BoundField DataField="Bnum" HeaderText="新增" />     
                            <asp:BoundField DataField="Eum" HeaderText="期未" />      
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lbl_message" runat="server">Label</asp:Label>
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
    </form>
</body>
</html>
