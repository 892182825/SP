<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocPrintOrderOut.ascx.cs" Inherits="UserControl_DocPrintOrderOut" %>
<link href="../Company/CSS/Store.css" rel="Stylesheet" type="text/css" />
<table>
    <tr>
        <td>
            <asp:Panel ID="showreficx" runat="server" Width="1000px" BorderWidth="0">
                <table cellpadding="0" cellspacing="0" border="0" width="100%" style="font-size: 14px;
                    font-family: '宋体'; text-decoration: none; line-height: 24px; border: 1px solid;
                    text-indent: 10px; white-space: normal;">
                    <tr align="center">
                        <td colspan="6">
                            <b>
                                <asp:Label ID="lblbilltypename" runat="server" Text="" Style="font-size: 18px; font-family: '宋体';"></asp:Label></b>
                        </td>
                    </tr>
                    <!--数据显示-->
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litbillID" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate("000079", "订单号")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblBillID" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litDocPrintDate" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004133", "打印日期")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblprintDate" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litOperateNum" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004134", "操作人编号")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblOperateNum" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litDocMakeTime" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004136", "开单时间")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocMakeTime" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litDocAuditTime" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("001155", "审核时间")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocAuditTime" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litDocMaker" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004139", "开出人")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocMaker" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litStoreID" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000150", "店铺编号")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblStoreID" runat="server"></asp:Label>
                        </td>
                         <td align="right">
                            <asp:Literal ID="litDocAuditer" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000655", "审核人")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocAuditer" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litDocType" runat="server"></asp:Literal>
                            报单类型：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litTotalMoney" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000041", "总金额")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblTotalMoney" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="littotalpv" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004148", "总PV")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lbltotalpv" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litCurrency" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004150", "币种名称")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litNote" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000078", "备注")%>：
                        </td>
                        <td colspan="5" align="left">
                            <asp:Label ID="lblNote" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litAddress" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004151", "发货地址")%>：
                        </td>
                        <td colspan="5" align="left">
                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:GridView ID="givShow" runat="server" AutoGenerateColumns="False" BorderWidth="0"
                                CellPadding="0" CellSpacing="1" CssClass="tablemb" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="产品编码" DataField="ProductCode" />
                                    <asp:BoundField HeaderText="产品名称" DataField="ProductName" />
                                    <asp:BoundField HeaderText="期数" DataField="ExpectNum" />
                                    <asp:BoundField HeaderText="优惠价格" DataField="price" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="优惠积分" DataField="pv" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="总数量" DataField="quantity" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="总金额" DataField="totalPrice" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="总积分" DataField="totalpv" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <%=BLL.Translation.Translate ("000558", "产品编号")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("000501", "产品名称")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("000045", "期数")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("000627", "优惠价格")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("001883", "优惠积分")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("004161", "总数量")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("000041", "总金额")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("000113", "总积分")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <!-- 处理入库，出库单据--->
            <asp:Panel ID="showin" runat="server">
            </asp:Panel>
        </td>
    </tr>
</table>
