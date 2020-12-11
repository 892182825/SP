<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowOrderDetails.aspx.cs" Inherits="Company_ShowOrderDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=GetTran("000884", "订单明细")%></title>
    <link rel="Stylesheet" href="Css/Company.css"  type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <br /><br />
    <center>
    <table class="biaozzi" width="100%" >
    <tr>
    <td>
        <h3 style="text-align:center;"> <%=GetTran("000884", "订单明细")%> </h3>
        </td>
        </tr><tr><td>
        
        <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
            <td >
                  <asp:GridView ID="gvStoreOrder" runat="server" AutoGenerateColumns="False" width="100%"
                    CssClass="tablemb bordercss" OnRowDataBound="gvStoreOrder_RowDataBound">
                    <Columns>
                       
                        <asp:BoundField DataField="ordergoodsid" HeaderText="订单号" />
                        <asp:TemplateField HeaderText="总金额">
                            <ItemTemplate>
                                <asp:Label ID="Label6" name="lblTotalMoney" runat="server" Text='<%# Eval("TotalMoney") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TotalPV" HeaderText="总积分" />
                        <asp:BoundField DataField="ExpectNum" HeaderText="购货期数" />
                        <asp:TemplateField FooterText="IsCheckOut" HeaderText="支付状态">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# GetCheckState(Eval("IsCheckOut")) %>'></asp:Label>
                                <asp:Label ID="lblCheckOut" runat="server" Text='<%# Eval("IsCheckOut") %>' Style="display: none;"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="订货类型">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# GetType(Eval("ordertype")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="支付方式">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# GetOrderType(Eval("PayType")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField FooterText="activeflag" HeaderText="订单类型">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# GetActiveFlag(Eval("ordertype")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField HeaderText="国家" DataField="Country" ItemStyle-Wrap="false" HeaderStyle-Wrap ="false"  />       
                        <asp:TemplateField HeaderText="收货人地址">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货人">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货人电话">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        

                       
                    </Columns>
                    <AlternatingRowStyle BackColor="#F1F4F8" />
                   
                </asp:GridView>
            </td>
            
        </tr>
     
       
    </table>
    <br />
    <asp:GridView ID="gvOrderDetail" runat="server" AutoGenerateColumns="False"  Width="100%" 
                        CssClass="tablemb" onrowdatabound="gvOrderDetail_RowDataBound" >
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="商品名称" />
                <asp:BoundField DataField="Quantity" HeaderText="商品数量" />
                <asp:BoundField DataField="Price" HeaderText="商品单价" />
                <asp:BoundField DataField="Pv" HeaderText="商品积分" />
            </Columns>
            <AlternatingRowStyle BackColor="#F1F4F8" />
        </asp:GridView>
        </td></tr><tr><td align="left">
        &nbsp;&nbsp;&nbsp; <a href="#" onclick="javascript:history.back(1);" > &nbsp;<%=GetTran("000421", "返回")%> </a>
        </td>
        </tr>
    </table>
    </center>
    </form>
</body>
</html>
