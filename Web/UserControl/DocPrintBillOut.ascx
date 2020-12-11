<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocPrintbillOut.ascx.cs"
    Inherits="UserControl_DocPrintbillOut" EnableTheming="true" %>
<link href="../Company/CSS/Company.css" rel="Stylesheet" type="text/css" />
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
                            <%=BLL.Translation.Translate ("004131", "单据号")%>：
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
                            <asp:Literal ID="litDocSecondAuditTime" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004138", "复核时间")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocSecondAuditTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litDocMaker" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004139", "开出人")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocMaker" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litDocAuditer" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000655", "审核人")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocAuditer" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litWareHouse" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000355", "仓库名称")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblWareHouse" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litStoreID" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000024", "会员编号")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblStoreID" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litBatchCode" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004142", "批次号")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblBatchCode" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litSeatName" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000357", "库位名称")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblSeatName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litCloseFlag" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004144", "是否关闭")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCloseFlag" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litStateFlagtime" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004145", "关闭时间")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblStateFlagtime" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litDocType" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("001151", "单据类型")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDocType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="litProvider" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("002020", "供应商")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblProvider" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litIsRubric" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("004146", "是否红单")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIsRubric" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Literal ID="litStateFlag" runat="server"></asp:Literal>
                            <%=BLL.Translation.Translate ("000986", "单据状态")%>：
                        </td>
                        <td align="left">
                            <asp:Label ID="lblStateFlag" runat="server"></asp:Label>
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
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCurrency" Visible="false" runat="server"></asp:Label>
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
                                    <asp:BoundField HeaderText="优惠价格" DataField="PreferentialPrice" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="优惠积分" DataField="PreferentialPV" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="单位" DataField="ProductUnitName" />
                                    <asp:BoundField HeaderText="仓库名称" DataField="WareHouseName" />
                                    <asp:BoundField HeaderText="库位名称" DataField="SeatName" />
                                    <asp:BoundField HeaderText="批次" DataField="BatchCode" />
                                    <asp:BoundField HeaderText="总数量" DataField="ProductQuantity" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="总金额" DataField="ProductTotal" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="总积分" DataField="pv" ItemStyle-HorizontalAlign="Right" />
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
                                                <%=BLL.Translation.Translate ("000518", "单位")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("000355", "仓库名称")%>
                                            </th>
                                             <th>
                                                <%=BLL.Translation.Translate ("000357", "库位名称")%>
                                            </th>
                                            <th>
                                                <%=BLL.Translation.Translate ("000658", "批次")%>
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
