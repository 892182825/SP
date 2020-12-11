<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BrowseStoreOrders.aspx.cs" Inherits="Company_BrowseStoreOrders" EnableEventValidation="false" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../JS/SetHuiLv.js" type="text/javascript"></script>
    <script src="js/tianfeng.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;
        };
      
      function isDelete()
      {
         return window.confirm('<%=GetTran("000248")%>');
      }
      function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
</SCRIPT>
<script type="text/javascript" src="../js/SqlCheck.js"></script>
    
    </head>
<body onload="down2()">
    <form id="form1" runat="server" onsubmit="return filterSql_III()">
        <div ><br />
        <table>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                                <asp:Button ID="butt_Query" runat="server" Text="查 询" 
                                            style="cursor:pointer" CssClass="anyes"
                                                onclick="Button1_Click" align="absmiddle" Height="22px" />&nbsp;&nbsp;
                                <%=GetTran("000047")%>：<asp:DropDownList ID="DropCurrency" runat="server">
                                </asp:DropDownList>
                                
					            <asp:DropDownList ID="DropDownList_Items" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="DropDownList_Items_SelectedIndexChanged">
									<asp:ListItem Value="StoreOrderID">订单号</asp:ListItem>
									<asp:ListItem Value="PostalCode">邮政编码</asp:ListItem>
									<asp:ListItem Value="IsSent">是否发货</asp:ListItem>
									<asp:ListItem Value="InceptPerson">收货人姓名</asp:ListItem>
									<asp:ListItem Value="〖Country〗">收货人国家</asp:ListItem>
									<asp:ListItem Value="〖Province〗">收货人省份</asp:ListItem>
									<asp:ListItem Value="〖City〗">收货人城市</asp:ListItem>
									<asp:ListItem Value="InceptAddress">收货人地址</asp:ListItem>
									<asp:ListItem Value="Telephone">收货人电话</asp:ListItem>
									
									<asp:ListItem Value="TotalMoney">总价格</asp:ListItem>
									<asp:ListItem Value="ExpectNum">期数</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList_condition" runat="server">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtBox_keyWords" runat="server"  Width="80px" MaxLength="100"></asp:TextBox>
                                <asp:TextBox ID="txtBox_rq" Visible="false" runat="server" Width="80px" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                <%=GetTran("000060")%>&nbsp;&nbsp;
                                <%=GetTran("000067")%>：<asp:TextBox ID="txtBox_OrderDateTimeStart" runat="server" Width="80px"  CssClass="Wdate"
                                                onfocus="new WdatePicker()"></asp:TextBox>
                                                <%=GetTran("000068")%>：<asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server" Width="80px" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                                &nbsp;
                                                <%=GetTran("000070")%>：<asp:TextBox ID="txtBox_ConsignmentDateTimeStart" Width="80px" runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                              <%=GetTran("000068")%>：  
                                            <asp:TextBox ID="txtBox_ConsignmentDateTimeEnd" Width="80px" runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=GetTran("000074")%>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <table>
                        <tr>
                            <td style="border:rgb(147,226,244) solid 1px">
                                <asp:GridView ID="GridView_Order" runat="server" AutoGenerateColumns="False" 
                                    onselectedindexchanged="GridView1_SelectedIndexChanged"  width="100%" 
                                     Cssclass="tablemb bordercss"
                                
                                    onrowdatabound="GridView_Order_RowDataBound">
                                    
                                    <EmptyDataTemplate>
                                        <table cellspacing="0">
                                            <tr>
                                                <th nowrap>
                                                    <%=GetTran("000079")%>
                                                </th>
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
                                                <th nowrap>
                                                   <%=GetTran("000078")%>
                                                </th>
                                                <th nowrap>
                                                    <%=GetTran("000127")%>
                                                </th>
                                            </tr>                
                                        </table>
                                    </EmptyDataTemplate>
                        
                                    <Columns>
                                    
                                        <asp:TemplateField HeaderText="删除" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lButt_Delete" runat="server"  Visible='<%#IsDelete(Eval("IsCheckOut").ToString()) %>'
                                                    onclick="LinkButton1_Click" CommandName="select" OnClientClick="return isDelete()"><%=GetTran("000022")%></asp:LinkButton>
                                               
                                            </ItemTemplate>
                                      
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                      
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="详细">
                                            <ItemTemplate>
                                                <img src="images/fdj.gif" /><asp:LinkButton ID="lButt_Details" runat="server" CommandName="select" 
                                                    onclick="lkb_Click"><%=GetTran("000339", "详细")%></asp:LinkButton>
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
                                        <asp:TemplateField HeaderText="收货否" Visible="false">
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
                                                
                                                <%# GetName(DataBinder.Eval(Container, "DataItem.InceptPerson").ToString())%>
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
                                       
                                        <asp:BoundField DataField="TotalMoney" HeaderText="总金额" />
                                        
                                        <asp:BoundField DataField="TotalPV" HeaderText="总积分" />
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
                                                <asp:Label ID="Lab_ConsignmentDateTime" runat="server" Text='<%#Eval("issent").ToString()=="Y"?GetBiaoZhunTime(Eval("ConsignmentDateTime").ToString()):"--" %>'></asp:Label>
                                            </ItemTemplate>
                          
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                          
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Remark" runat="server" Text='<%#GetRemark(Eval("Description").ToString(),Eval("StoreOrderID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
       
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
       
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注详单">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Remark2" runat="server" Text='<%#GetRemark2(Eval("Description").ToString(),Eval("StoreOrderID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
       
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
       
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收获问题">
                                            <ItemTemplate>
                                                <asp:Label ID="aa" runat="server" Text='<%#GetShouHuoWT(Eval("feedback").ToString(),Eval("StoreOrderID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
       
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
       
                                        </asp:TemplateField>
                                    </Columns>

<HeaderStyle CssClass="tablebt bbb"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
                            </td>
                            <td></td><td></td>
                        </tr>
                        <tr>                           
                            <td colspan="3">
                                 <uc1:Pager ID="Pager1" runat="server" />                  
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                         <tr>
                            <td>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        <div id="cssrain" style="width:100%">
      <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="150"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
              <tr>
                <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
              </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block" id="tbody0">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><a href="#">
                  <asp:ImageButton ID="Butt_Excel" runat="server"  
                        onclick="Button2_Click" ImageUrl="images/anextable.gif"/>
                  </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td> <%--<%=GetTran("006849")%>--%>查看所有购物订单的基本信息</td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
    </form>
</body>
</html>