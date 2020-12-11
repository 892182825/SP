<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowOrderGoodsNote.aspx.cs"
    Inherits="Company_ShowOrderGoodsNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table style="width: 100%;">
            <tr>
                <td style="border: rgb(147,226,244) solid 1px">
                    <!--GridView1-->
                    <asp:GridView ID="GridView_Order" runat="server" AutoGenerateColumns="False" Width="100%"
                        CssClass="tablemb bordercss" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        OnRowDataBound="GridView_Order_RowDataBound">
                        <EmptyDataTemplate>
                            <table cellspacing="0" style="width: 100%;">
                                <tr>
                                    <th nowrap>
                                        <%=GetTran("000339", "详细")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000098", "订货店铺")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000079", "订单号")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("007356", "要货单号")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000045", "期数")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000100", "付款否")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000106", "订单类型")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000107", "姓名")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000108", "收货人国家")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000109", "省份")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000110", "城市")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000112", "收货地址")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000073", "邮编")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000041", "总金额")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000113", "总积分")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000115", "联系电话")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000118", "重量")%>
                                    </th>
                                    <th nowrap>
                                        <%=GetTran("000120", "运费")%>
                                    </th>
                                        <th nowrap>
                                        <%=GetTran("000067", "订货日期")%>
                                    </th>
                                        <th nowrap>
                                        <%=GetTran("000078", "备注")%>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="详细">
                                <ItemTemplate>
                                    <img src="images/fdj.gif" />
                                    <asp:LinkButton ID="lButt_Details" runat="server" CommandName="select" OnClick="lkb_Click"><%=GetTran("000339", "详细")%></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="订货店铺" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_StoreID" runat="server" Text='<%#Empty.GetString(Eval("StoreID").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="订单号">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_StoreOrderID" runat="server" Text='<%#Empty.GetString(Eval("ordergoodsid").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="要货单号" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_OutStorageOrderID" runat="server" Text='<%#Empty.GetString(Eval("fahuoorder").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="期数">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_ExpectNum" runat="server" Text='<%#Empty.GetString(Eval("ExpectNum").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="付款否" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_IsCheckOut" runat="server" Text='<%#StringFormat(Eval("IsCheckOut").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="订单类型">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_OrderType" runat="server" Text='<%#GetOrderType(Eval("OrderType").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <%# GetName(DataBinder.Eval(Container, "DataItem.InceptPerson").ToString())%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="收货人国家">
                                <ItemTemplate>
                                    <asp:Label ID="lab_Province" runat="server" Text='<%#Eval("country") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="省份">
                                <ItemTemplate>
                                    <asp:Label ID="lab_a" runat="server" Text='<%#Eval("province") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="城市">
                                <ItemTemplate>
                                    <asp:Label ID="b" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                                <HeaderStyle Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="收货地址">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_InceptAddress" runat="server" Text='<%#SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()),15) %>'
                                        title='<%#Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="left" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="邮编">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_PostalCode" runat="server" Text='<%#Empty.GetString(Eval("PostalCode").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="总金额">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_TotalMoney" runat="server" Text='<%#Empty.GetString(Eval("TotalMoney", "{0:N2}").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Right" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="总积分">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_TotalPV" runat="server" Text='<%#Empty.GetString(Eval("TotalPV", "{0:N2}").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="right" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="联系电话">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_Telephone" runat="server" Text='<%#Empty.GetString(Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString())) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="重量" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_Weight" runat="server" Text='<%#Empty.GetString(Eval("Weight", "{0:N2}").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="right" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="运费" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_Carriage" runat="server" Text='<%#Empty.GetString(Eval("Carriage", "{0:N2}").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Right" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="订货日期">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_OrderDateTime" runat="server" Text='<%#GetBiaoZhunTime(Eval("OrderDateTime").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="Lab_Remark" runat="server" Text='<%# Eval("Description")  %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="tablebt bbb"></HeaderStyle>
                        <AlternatingRowStyle BackColor="#F1F4F8" />
                    </asp:GridView>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <p>
        <asp:Button ID="butt_Query" runat="server" Text="返 回" Style="cursor: pointer" CssClass="anyes"
            align="absmiddle" Height="22px" OnClick="butt_Query_Click" />
    </p>
    </form>
</body>
</html>
