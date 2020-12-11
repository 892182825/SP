<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowBillDetailsB.aspx.cs"
    Inherits="Company_ShowBillDetailsB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>单据详细</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript">
    function fanyi()
    {
        return '<%=GetTran("000421","有效") %>';
    }
</script>

<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
            <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
                border="0" class="tablemb" align="center">
                <tr>
                    <td>
                        <asp:GridView ID="givDoc" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="tablemb bordercss" OnRowDataBound="givDoc_RowDataBound" AlternatingRowStyle-Wrap="False"
                            FooterStyle-Wrap="False" HeaderStyle-Wrap="False" PagerStyle-Wrap="False" SelectedRowStyle-Wrap="False">
                            <EmptyDataTemplate>
                                <table class="tablebt" width="100%">
                                    <th>
                                        <%=GetTran("001151", "单据类型")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000407", "单据编号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001153", "开出时间")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000519", "开出人")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000041", "总金额")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000414", "积分")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000045", "期数")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000655", "审核人")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001155", "审核时间")%>
                                    </th>
                                </table>
                            </EmptyDataTemplate>
                            <FooterStyle Wrap="False"></FooterStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="单据类型">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Type(Eval("DocTypeID")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Wrap="false" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                    <HeaderTemplate>
                                        <span>单据编号</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("DocID")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="开出时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <span>开出时间</span></HeaderTemplate>
                                    <ItemTemplate>
                                        <span>
                                            <%#Getdatetime(Eval("DocMakeTime")) %></span></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="开出人" DataField="DocMaker" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                    <HeaderStyle Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="总金额" DataField="TotalMoney" ItemStyle-Wrap="false">
                                    <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="积分" DataField="TotalPV">
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="期数" DataField="ExpectNum" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                    <HeaderStyle Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="审核人">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DocAuditer") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Empty.GetString(Eval("DocAuditer").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审核时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                    <HeaderTemplate>
                                        <span>审核时间</span></HeaderTemplate>
                                    <ItemTemplate>
                                        <span>
                                            <%#Getdatetime(Eval("DocAuditTime"))%></span></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle Wrap="False"></PagerStyle>
                            <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <AlternatingRowStyle BackColor="#F1F4F8" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%">
                <tr>
                    <td style="border: rgb(147,226,244) solid 1px">
                        <asp:GridView ID="givDocDitals" runat="server" AutoGenerateColumns="False" OnRowDataBound="givDocDitals_RowDataBound"
                            Width="100%" border="0" CellPadding="0" CellSpacing="1" bgcolor="#F8FBFD" CssClass="tablemb bordercss"
                            HeaderStyle-CssClass="tablebt bbb">
                            <Columns>
                                <asp:BoundField HeaderText="单据编号" DataField="DocID" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="产品编号" DataField="ProductCode" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="产品名称" DataField="ProductName" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="数量" DataField="ProductQuantity" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" DataField="TotalPrice"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="总积分" DataField="Pv" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="期数" DataField="ExpectNum" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="单据状态" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Convert.ToInt32(Eval("StateFlag"))==1?GetTran("001072","有效") :GetTran("001069","无效") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <%--报损--%>
                                <asp:TemplateField HeaderText="仓库" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <span>
                                            <%#Eval("inWareHouseName")%></span>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="库位" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <span>
                                            <%#Eval("inSeatName")%></span>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <%--报溢--%>
                                <asp:TemplateField HeaderText="仓库" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="La2" runat="server" Text='<%#Eval("WareHouseName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="库位" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="La3" runat="server" Text='<%#Eval("SeatName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <%--调拨单--%>
                                <asp:TemplateField HeaderText="调出仓库" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="La4" runat="server" Text='<%#Eval("inWareHouseName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调出库位" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="La5" runat="server" Text='<%#Eval("inSeatName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调入仓库" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="La6" runat="server" Text='<%#Eval("WareHouseName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调入库位" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="La7" runat="server" Text='<%#Eval("SeatName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#F1F4F8" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="left">
                        <input type="button" id="butt_Query" value='<%=GetTran("000421","返回") %>' style="cursor: pointer"
                            class="anyes" onclick="history.back()" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
