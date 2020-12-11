<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberStore.aspx.cs" Inherits="Company_MemberStore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员分存表</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <font face="宋体">
            <table id="Table2" style="z-index: 101; left: 0px; width: 700px; position: absolute;
                top: 0px; height: 316px" cellspacing="0" cellpadding="0" width="616" border="0" class="biaozzi">
                <tr height="40">
                    <td align="center">
                        <font style="FONT-STYLE: normal; FONT-SIZE: 20px; FONT-WEIGHT: bold"><%=GetTran("001463", "会员汇总表(店铺)")%></font>
                    </td>
                </tr>
                <tr height="25">
                    <td>
                       <%=GetTran("001465", "查询时间:从")%> 
                        <asp:Label ID="lbl_begin2" runat="server">Label</asp:Label> <%=GetTran("000205", "到")%>
                        <asp:Label ID="lbl_End2" runat="server">Label</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="word-break:keep-all;word-wrap:normal">
                    
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="1020px" CssClass="tablemb" style="word-break:keep-all;word-wrap:normal;"
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" 
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
                            <asp:BoundField DataField="storeid" HeaderText="店铺编号" />
                            <asp:BoundField DataField="name" HeaderText="店长姓名" />
                           <asp:BoundField  DataField="Fnum" HeaderText="期初" />
                            <asp:BoundField DataField="Bnum" HeaderText="新增" />     
                            <asp:BoundField DataField="Enum" HeaderText="期末" />      
                        </Columns>
                    </asp:GridView>
                       
                        <asp:Label ID="lbl_message2" runat="server">Label</asp:Label>
                    </td>
                </tr>
            </table>
        </font>
    </div>
    </form>
</body>
</html>
