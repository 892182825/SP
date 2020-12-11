<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowBillDetails.aspx.cs" Inherits="Company_ShowBillDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <center >
        <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
        <td>
          <asp:GridView  ID="givDoc" runat="server" AutoGenerateColumns="False" Width="100%"
                              
                CssClass="tablemb bordercss" OnRowDataBound="givDoc_RowDataBound"
                                AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                                PagerStyle-Wrap="False" SelectedRowStyle-Wrap="False">
                                <EmptyDataTemplate>
                                    <table class="tablebt" width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000399", "查看详细")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000407", "单据编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000409", "报损仓库")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000410", "报损库位")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000413", "报损时间")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000414", "积分")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000041", "总金额")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000078", "备注")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <FooterStyle Wrap="False"></FooterStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="查看详细" ShowHeader="False" Visible=false>
                                        <ItemTemplate>
                                            <img src="images/fdj.gif" /><asp:LinkButton ID="Button1" runat="server" CausesValidation="false" CommandName="Details"
                                                CommandArgument='<%#Eval("DocID") %>' Text="查看详细"><%=GetTran("000399", "查看详细")%></asp:LinkButton>
                                                <asp:HiddenField ID="hiddftotal" runat="server" Value='<%#Eval("TotalMoney") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="center" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="单据编号" ShowHeader="False">
                                        <ItemTemplate>
                                            <span>
                                                <%#Eval("DocID") %></span>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="center" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="报损仓库" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span>
                                                <%#Eval("warehousename")%></span></ItemTemplate>

<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="报损库位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span>
                                                <%#Eval("seatname") %></span></ItemTemplate>

<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="报损时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span>
                                                <%#Getdatetime(Eval("DocMakeTime")) %></span></ItemTemplate>

<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="期数" DataField="ExpectNum" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="积分" DataField="TotalPV">
                                        <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="总金额" DataField="TotalMoney" ItemStyle-Wrap="false">
                                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundField>
                                   
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
                <td style="border:rgb(147,226,244) solid 1px"><asp:GridView ID="givDocDitals" runat="server" AutoGenerateColumns="False" 
            onrowdatabound="givDocDitals_RowDataBound"
            width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD"  CssClass="tablemb bordercss"
            HeaderStyle-CssClass="tablebt bbb">
            <Columns>
                <asp:BoundField HeaderText="单据编号" DataField="DocID" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="产品编号" DataField="ProductCode" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="产品名称" DataField="ProductName" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="数量" DataField="ProductQuantity" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" DataField="TotalPrice" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                <asp:BoundField HeaderText="币种" DataField="DocID" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField HeaderText="总积分" DataField="Pv" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="期数" DataField="ExpectNum" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
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
                        <span> <%#Eval("inWareHouseName")%></span>
                    </ItemTemplate>
                    <ItemStyle Wrap="false" />
                    <HeaderStyle Wrap="false" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="库位" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <span> <%#Eval("inSeatName")%></span>
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
        </asp:GridView></td>
            </tr>
        </table>
          <table width="100%">
          <tr>
            <td align="left">
               
            <input type="button" ID="butt_Query"value='<%=GetTran("000421","返回") %>' style="cursor:pointer" Class="anyes" onclick="history.back()"/></td>
          </tr>
          </table>
        
        
        </center>
    </div>
    </form>
</body>
</html>
