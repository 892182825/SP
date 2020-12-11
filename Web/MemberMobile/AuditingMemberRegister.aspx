<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditingMemberRegister.aspx.cs" Inherits="Member_BrowseMemberOrders" %>
<%@ Register Src="../UserControl/MemberPager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1"  %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="x-ua-compatible" content="ie=11" />
<head id="Head1" runat="server">
    <title>无标题页</title>
  <link href="CSS/detail.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../js/SqlCheck.js"></script>
    <script language="javascript">
    function CheckText()
	{
		//防SQL注入
		filterSql();
	}

     var defaultcur;
        
//    window.onload=function (){
//        defaultcur=document.getElementById("ddlCurrency").value;
//    }

    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="MemberPage">
     <uc1:top runat="server" ID="top" />
     <div class="ctConPgList">
      	<ul>
        	<li><asp:DropDownList ID="ddlVolume" CssClass="ctConPgFor"  runat="server"></asp:DropDownList><%=this.GetTran("000719", "并且")%></li>
            <li>
                    <asp:DropDownList ID="ddlContion" runat="server"  CssClass="ctConPgFor"  AutoPostBack="True" OnSelectedIndexChanged="ddlContion_SelectedIndexChanged">
                  <%--      <asp:ListItem Value="B.error">错误信息</asp:ListItem>--%>
                        <asp:ListItem Value="A.Number" Selected="True">会员编号</asp:ListItem>
                        <asp:ListItem Value="A.Name">会员姓名</asp:ListItem>
                          <asp:ListItem Value="A.Direct">推荐编号</asp:ListItem>
                        <asp:ListItem Value="A.Placement">安置编号</asp:ListItem>
                       <%-- <asp:ListItem Value="A.Storeinfo">所属服务机构</asp:ListItem>--%>
                        <asp:ListItem Value="B.OrderID">订单号</asp:ListItem>
                        <asp:ListItem Value="B.TotalMoney">金额</asp:ListItem>
                        <asp:ListItem Value="B.TotalPv">积分</asp:ListItem>
                    </asp:DropDownList>
             </li>
            <li><asp:DropDownList ID="ddlcompare"  CssClass="ctConPgFor"  runat="server"></asp:DropDownList></li>
            <li><asp:TextBox ID="txtContent" runat="server" CssClass="ctConPgTxt" MaxLength="30"></asp:TextBox><%=this.GetTran("000731", "的报单")%> </li>
            <li><asp:Button ID="btnQuery" runat="server" Text="搜 索" OnClick="btnQuery_Click"  CssClass="anyes"  OnClientClick="CheckText()"/></li>
        </ul>
	    
	  </div>
         <div class="ctConPgList-1">                  
                    <asp:GridView ID="gv_browOrder" runat="server" CellSpacing="1" AutoGenerateColumns="False"  Width="100%" 
                        OnRowDataBound="gv_browOrder_RowDataBound" >
                        <HeaderStyle  Wrap="false" CssClass="tr"  />
                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />
                        <Columns>
                              <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                 查看明细
                                </HeaderTemplate>
                                <ItemTemplate>
                                     <asp:ImageButton ID="linkbtnquery" ImageUrl="images/view-button.png" runat="server"  CommandName="Query" CommandArgument='<%# Eval("OrderID") %>'
                                         OnCommand="linkbtnquery_Click"></asp:ImageButton>
                                </ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                  支付
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="linkbtnOk"  ImageUrl="images/view-button1-.png" runat="server" CommandName="OK" CommandArgument='<%# Eval("OrderID") %>'
                                        OnCommand="linkbtnOK_Click"></asp:ImageButton>
                                </ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                 修改
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="linkbtnModify" ImageUrl="images/view-button2.png" runat="server" CommandArgument='<%#Eval("OrderID")+":"+Eval("storeId")+":"+Eval("number")+":"+Eval("lackproductmoney")%>'
                                        oncommand="linkbtnModify_Command"></asp:ImageButton>
                                </ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                  删除
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="linkbtnDelete" OnClientClick="return confirm('确定删除会员报单吗？');" ImageUrl="images/view-button3.png" runat="server" CommandName="Dele"
                                    CommandArgument='<%#Eval("OrderID")+":"+Eval("number")+":"+Eval("storeId")+":"+Eval("lackproductmoney") %>' OnCommand="linkbtnDelete_Click"></asp:ImageButton>
                                </ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="错误信息" Visible="false">
							    <ItemStyle HorizontalAlign="Center" />
							    <ItemTemplate>
							        <%# GetError(DataBinder.Eval(Container.DataItem, "Error").ToString())%>
							    </ItemTemplate>
							</asp:TemplateField>
                            <asp:BoundField HeaderText="会员编号" DataField="number" ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                              </asp:BoundField>
                            <asp:TemplateField HeaderText="会员姓名">
							    <ItemStyle HorizontalAlign="Center" />
							    <ItemTemplate>
							        <%# GetNumberName(DataBinder.Eval(Container.DataItem, "name").ToString())%>
							    </ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField HeaderText="推荐编号" DataField="direct" ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                              </asp:BoundField>
                              <asp:BoundField HeaderText="安置编号" DataField="placement" ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                              </asp:BoundField>
                            <asp:BoundField HeaderText="所属店铺" Visible="false" DataField="storeId" ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                              </asp:BoundField>
                            <asp:TemplateField HeaderText="支付方式" Visible="false">
							    <ItemStyle HorizontalAlign="Center" />
							    <ItemTemplate>
							        
		              <input type="hidden" id="HiddefrayState" value='<%#DataBinder.Eval(Container,"DataItem.defraystate") %>' name="hids" runat="server" />
							        <input type="hidden" id="HidorderExpectNum" value='<%#DataBinder.Eval(Container,"DataItem.orderExpectNum") %>' name="hids" runat="server" />
							        <input type="hidden" id="HidOrderId" value='<%#DataBinder.Eval(Container,"DataItem.OrderId") %>' name="hids" runat="server" />
							        <input type="hidden" id="HidTotalMoney" value='<%#DataBinder.Eval(Container,"DataItem.TotalMoney") %>' name="hids" runat="server" />
							    </ItemTemplate>
							</asp:TemplateField>
                            <asp:BoundField HeaderText="期数" DataField="orderExpectNum" 
                                  ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                              </asp:BoundField>

                               <asp:TemplateField HeaderText="审核期数" Visible="false">
							    <ItemStyle HorizontalAlign="Center" />
							    <ItemTemplate>
							        <%# GetPayNum(DataBinder.Eval(Container.DataItem, "PayExpectNum").ToString())%>
							    </ItemTemplate>
							</asp:TemplateField>
                            <asp:BoundField HeaderText="订单号" DataField="OrderId" ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                              </asp:BoundField>

                           <asp:TemplateField HeaderText="金额" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalMoney" name="lblTotalMoney" runat="server" Text='<%# Eval("totalMoney", "{0:n2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            <asp:BoundField HeaderText="积分" DataField="totalPv" DataFormatString="{0:n2}" ItemStyle-Wrap="false" 
                                  HtmlEncode="False">
                                <ItemStyle HorizontalAlign="Right" />
                              </asp:BoundField>
                            <asp:TemplateField HeaderText="注册日期" Visible="false" ItemStyle-Wrap="false">
							    <ItemStyle HorizontalAlign="Center" />
							    <ItemTemplate>
							        <%# GetRegisterDate(DataBinder.Eval(Container.DataItem, "RegisterDate").ToString())%>
							    </ItemTemplate>
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="备注" Visible="false">
								<ItemTemplate>
									<%#SetVisible(DataBinder.Eval(Container.DataItem, "remark").ToString(), DataBinder.Eval(Container.DataItem, "orderId").ToString())%>
								</ItemTemplate>
							</asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table  width="100%" cellpadding="1" cellspacing="1">
                            <tr class="ctConPgTab">
                            <th><%=this.GetTran("000811", "查看明细")%></th><th><%=this.GetTran("000938", "支付")%></th><th><%=this.GetTran("000259", "修改")%></th><th><%=this.GetTran("000022","删除")%></th><th><%=this.GetTran("000024", "会员编号")%></th><th><%=this.GetTran("000025", "会员姓名")%></th><th><%=this.GetTran("000000", "推荐编号")%></th><th><%=this.GetTran("000000", "安置编号")%></th><th><%=this.GetTran("000045", "期数")%></th><th><%=this.GetTran("000780", "审核期数")%></th>
                            <th><%=this.GetTran("000079", "订单号")%></th><th><%=this.GetTran("000322", "金额")%></th><th><%=this.GetTran("000414", "积分")%></th><%--<th><%=this.GetTran("000057", "注册日期")%>--%></th>
                            </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>                                                
               </div>
       <uc1:Pager ID="Pager1" runat="server" />  
  <div class="ctConPgList-3">
      	<h1><%=this.GetTran("000649", "功能说明")%>：</h1>
        <p>1、<%=this.GetTran("005849", "显示未支付的会员注册报单")%>；</p>
        <p>	2、<%=this.GetTran("005850", "在会员里面的注册的首次报单，当前期的未支付的报单可以进行修改、删除操作")%>；</p>
      </div>
						
	<uc2:bottom runat="server" ID="bottom" />
	</div>
						<%=msg %>
    </form>
</body>
</html>
