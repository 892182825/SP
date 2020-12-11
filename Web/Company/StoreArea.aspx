<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreArea.aspx.cs" Inherits="Company_StoreArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= GetTran("003036", "店铺分布表")%></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <font face="宋体">
        <div style="left: 8px; width: 10px; position: absolute; top: 0px; height: 10px">
            <form id="Form1" method="post" runat="server">
            <table id="Table1" style="z-index: 100; left: 0px; width: 576px; position: absolute;
                top: 0px; height: 198px" cellspacing="0" cellpadding="0" width="700" border="0">
                <tr height="40">
                    <td align="center">
                        <font style="font-weight: bold; font-size: 18px; color: black" face="宋体"><%= GetTran("003036", "店铺分布表")%></font>
                    </td>
                </tr>
                <tr height="25">
                    <td style="font-size:14px;">
                        查询时间:从<asp:Label ID="lbl_Begin" runat="server">Label</asp:Label>到<asp:Label ID="lbl_End" runat="server">Label</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="700px"
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" 
                        onrowdatabound="GridView1_RowDataBound" CssClass="tablemb">
                       
                        <HeaderStyle  Wrap="false"  Width="60"/>
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                <font face="宋体">
                                    <asp:Label ID="lbl_code" runat="server">Label</asp:Label></font>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="city" HeaderText="城市" />
                            <asp:BoundField DataField="Fnum" HeaderText="期初" />
                            <asp:BoundField DataField="Bnum" HeaderText="新增" />     
                            <asp:BoundField DataField="Eum" HeaderText="期未" />      
                        </Columns>
                    </asp:GridView> 
                    
                     <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" Width="700px"
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" 
                        onrowdatabound="GridView1_RowDataBound" CssClass="tablemb">
                       
                        <HeaderStyle  Wrap="false"  Width="60"/>
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                <font face="宋体">
                                    <asp:Label ID="lbl_code" runat="server">Label</asp:Label></font>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Country" HeaderText="城市" />
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
            </form>
        </div>
    </font>
</body>
</html>
