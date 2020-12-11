<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStoreOrder.aspx.cs" Inherits="Company_ViewStoreOrder" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 215px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div><br>
     <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
            <td >
                 <asp:GridView ID="GridView_Order" runat="server" AutoGenerateColumns="False" 
                                    width="100%" 
                                     Cssclass="tablemb bordercss"
                                
                                    onrowdatabound="GridView_Order_RowDataBound">
                                    
                                    <EmptyDataTemplate>
                                        <table cellspacing="0">
                                            <tr>
                                               
                                                <th nowrap>
                                                    <%=GetTran("000099")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000045")%>
                                                </th>
                                                
                                                <th nowrap>
                                                    <%=GetTran("000102")%>
                                                </th>
                                                                                                
                                                <th nowrap>
                                                    <%=GetTran("000104")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000106")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000107")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000108")%>
                                                </th>
                                                <th nowrap>
                                                   <%=GetTran("000109")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000110")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000112")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000073")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000041")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000113")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000115")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000118")%>
                                                </th>
                                           
                                                <th nowrap>
                                                    <%=GetTran("000121")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000067")%>
                                                </th>
                                                <th nowrap>
                                                     <%=GetTran("000070")%>
                                                </th>
                                                
                                            </tr>                
                                        </table>
                                    </EmptyDataTemplate>
                        
                                    <Columns>
                                    
                                       
                                        <asp:TemplateField HeaderText="订货店铺" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_StoreID" runat="server" Text='<%#Empty.GetString(Eval("StoreID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                   
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                   
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单号">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_StoreOrderID" runat="server" Text='<%#Empty.GetString(Eval("StoreOrderID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                          
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="期数">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_ExpectNum" runat="server" Text='<%#Empty.GetString(Eval("ExpectNum").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                           
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="付款否" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_IsCheckOut" runat="server" Text='<%#StringFormat(Eval("IsCheckOut").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                            
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货否">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_IsSent" runat="server" Text='<%#StringFormat(Eval("IsSent").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                       
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                       
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货否">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_IsReceived" runat="server" Text='<%#StringFormat(Eval("IsReceived").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                  
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                  
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单类型">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_OrderType" runat="server" Text='<%#GetOrderType(Eval("OrderType").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                       
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                       
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_InceptPerson" runat="server" Text='<%# Empty.GetString(Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                         
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                         
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="收货人国家">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lab_Province" runat="server" Text='<%#Eval("country") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="省份">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lab_a" runat="server" Text='<%#Eval("province") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="城市">
                                                        <ItemTemplate>
                                                            <asp:Label ID="b" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap=false />
                                                        <HeaderStyle Wrap=false />
                                                    </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="收货地址">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_InceptAddress" runat="server" Text='<%#SetFormatString(Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()),15) %>' title='<%#Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="left"  Wrap="false"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="邮编">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_PostalCode" runat="server" Text='<%#Empty.GetString(Eval("PostalCode").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                   
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                   
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总金额">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_TotalMoney" runat="server" Text='<%#Empty.GetString(Eval("TotalMoney", "{0:N2}").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Right"  Wrap="false"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="总积分">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_TotalPV" runat="server" Text='<%#Empty.GetString(Eval("TotalPV", "{0:N2}").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                  
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="right"  Wrap="false"/>
                                  
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="联系电话">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Telephone" runat="server" Text='<%#Empty.GetString(Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                         
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                         
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="重量">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Weight" runat="server" Text='<%#Empty.GetString(Eval("Weight", "{0:N2}").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                     
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="right"  Wrap="false"/>
                                     
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="运费" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Carriage" runat="server" Text='<%#Empty.GetString(Eval("Carriage", "{0:N2}").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Right"  Wrap="false"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物流公司">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_ConveyanceCompany" runat="server" Text='<%#Empty.GetString(Eval("ConveyanceCompany").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                     
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                     
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订货日期">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_OrderDateTime" runat="server" Text='<%#GetBiaoZhunTime(Eval("OrderDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                               
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                               
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发货日期">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_ConsignmentDateTime" runat="server" Text='<%#GetBiaoZhunTime(Eval("ConsignmentDateTime").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                          
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                          
                                        </asp:TemplateField>
                                       
                                    </Columns>

<HeaderStyle CssClass="tablebt bbb"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
            </td>
        </tr>
       
    </table>
    <br />
        <table style="width:100%;">
        <tr>
        <td>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD" class="tablemb"  
                                     HeaderStyle-CssClass="tablebt bbb" 
                EmptyDataText="无数据" onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="产品编号">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ProductCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="产品名称">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                    </ItemTemplate>

                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
                <asp:TemplateField HeaderText="数量">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                    </ItemTemplate>

                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
                <asp:TemplateField HeaderText="单位">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("ProductUnitName") %>'></asp:Label>
                    </ItemTemplate>
  
                    <ItemStyle HorizontalAlign="Center" />
  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="期数">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("ExpectNum") %>'></asp:Label>
                    </ItemTemplate>
            
                    <ItemStyle HorizontalAlign="Center" />
            
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单价">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                    </ItemTemplate>
      
                    <ItemStyle HorizontalAlign="Right" />
      
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("Pv") %>'></asp:Label>
                    </ItemTemplate>

                    <ItemStyle HorizontalAlign="Right" />

                </asp:TemplateField>
                
             </Columns>

<HeaderStyle CssClass="tablebt bbb"></HeaderStyle>
            <AlternatingRowStyle BackColor="#F1F4F8" />
        </asp:GridView>
        </td>
        </tr>
        <tr>
        <td align="center"><br>
            <input type="button" ID="butt_Query"value='<%=GetTran("000096","返 回") %>'
                                            style="cursor:pointer" Class="anyes" onclick="history.back()"/>
        </td>
        </tr>
        </table>

    </form>
</body>
</html>
