<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auditingmemberagain.aspx.cs"
    Inherits="Store_auditingmemberagain" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GetTran("001701", "购货确认")%></title>
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
    <link rel="Stylesheet" href="CSS/company.css" type="text/css" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script type="text/javascript" language="javascript">
        function CheckSql() {
            filterSql();//防SQL注入

            __doPostBack('lkbtnQuery', '');
        }
        
        function Dialogsearch(bh, name, orderid, ordertype, paycrr, totalMoney, storeid) {
            var url = "PaymentSearch.aspx?bh=" + bh + "&name=" + name + "&orderid=" + orderid + "&ordertype=" + ordertype + "&paycrr=" + paycrr + "&totalMoney=" + totalMoney + "&storeid=" + storeid;

            var QueryString = window.showModalDialog(url, "", "center:yes;scroll:0;status:0;help:0;resizable:1;dialogWidth:400px;dialogHeight:300px");
            window.location = "auditingmemberagain.aspx";
        }
			
        //----------------转换汇率----------------------
        var defaultcur;
        
        window.onload = function () {
            defaultcur = document.getElementById("ddlCurrency").value;
        }
        
        function ChangeCurrency() {
            var curr = AjaxClass.GetCurrency_Ajax(defaultcur, document.getElementById("ddlCurrency").value).value;
            if (curr != 0 && curr != null && curr != "") {
                var allSpan = document.getElementsByTagName("span");
                for (var i = 0; i < allSpan.length; i++) {
                    if (allSpan[i].name == "lblTotalMoney") {
                        allSpan[i].innerHTML = allSpan[i].innerHTML.replace(",", "");

                        allSpan[i].innerHTML = (parseFloat(allSpan[i].innerHTML) / parseFloat(curr)).toFixed(2);
                    }
                }
                //document.getElementById("lbbaodanmoney").innerHTML=(parseFloat(document.getElementById("lbbaodanmoney").innerHTML)/parseFloat(curr)).toFixed(2)
                defaultcur = document.getElementById("ddlCurrency").value;
            }
        }
        //-----------------------------------
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table>
            <tr>
                <td>
                    <table class="biaozzi">
                        <tr>
                            <td>
                                <input type="button" class="anyes" value="<%=GetTran("000011", "搜索")%>" onclick="CheckSql()"
                                    class="anyes" />
                                <asp:LinkButton ID="lkbtnQuery" runat="server" OnClick="btnQuery_Click" Style="display: none;">搜索</asp:LinkButton>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVolume" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <%=GetTran("000719", "并且")%>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlContion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlContion_SelectedIndexChanged">
                                    <asp:ListItem Value="MI.Number" Selected="True">会员编号</asp:ListItem>
                                    <asp:ListItem Value="MI.Name">会员姓名</asp:ListItem>
                                    <asp:ListItem Value="MI.Direct">推荐编号</asp:ListItem>
                                    <asp:ListItem Value="MI.PaperNumber">证件号码</asp:ListItem>
                                    <asp:ListItem Value="MI.Bank">开户银行</asp:ListItem>
                                    <asp:ListItem Value="MI.BankCard">银行帐号</asp:ListItem>
                                    <asp:ListItem Value="MO.OrderID">订单号</asp:ListItem>
                                    <asp:ListItem Value="MO.TotalMoney">金额</asp:ListItem>
                                    <asp:ListItem Value="MO.TotalPv">积分</asp:ListItem>
                                </asp:DropDownList>
                            
                                <asp:DropDownList ID="ddlcompare" runat="server">
                                </asp:DropDownList>
                            
                                <asp:TextBox ID="txtContent" runat="server"></asp:TextBox>
                                &nbsp;&nbsp;
                                报单时间：<asp:TextBox ID="txtBox_OrderDateTimeStart" runat="server" Width="80px"  CssClass="Wdate"
                                                onfocus="new WdatePicker()"></asp:TextBox>
                                                <%=GetTran("000068")%>：<!--TextBox2--><asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server" Width="80px" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            
                 <asp:RadioButtonList ID="rtbiszf" Visible="false" runat="server"  AutoPostBack="true"
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="MO.DefrayState!=0">已支付</asp:ListItem>
                        <asp:ListItem Value="MO.DefrayState=0"  Selected="True">未支付</asp:ListItem>
                    </asp:RadioButtonList>
                            <%=GetTran("000562", "币种")%>：</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCurrency" runat="server" onChange="ChangeCurrency()"> </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           <%-- <tr>
                <td class=" biaozzi">
                    报单余额： &nbsp;&nbsp; &nbsp;
                    <asp:Label ID="lbbaodanmoney" runat="server" Text=""></asp:Label><asp:Label ID="lbmessage"
                        runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td  style="border: rgb(147,226,244) solid 1px">
                    <div style="width: 1300px">
                        <center>
                            <asp:GridView ID="gv_browOrder" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowCommand="gv_browOrder_RowCommand" OnRowDataBound="gv_browOrder_RowDataBound"
                                 CssClass="tablemb bordercss">
                                <HeaderStyle Wrap="false" BorderStyle="None" />
                                <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                <RowStyle HorizontalAlign="Center" Wrap="false" />
                                <Columns>
                                    <asp:TemplateField HeaderText="查看">
                                        <ItemTemplate>
                                             <img src="images/fdj.gif" /> <asp:LinkButton ID="linkbtnquery" runat="server" Text='<%#GetTran("000440", "查看")%>' CommandArgument='<%#Eval("orderid")+"|" +Eval("storeid") %>' CommandName="qur"
                                                ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="确认">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="IsAuditing" runat="server" Text='<%#GetTran("000064", "确认")%>'
                                                CommandName="Auditing" CommandArgument='<%# Eval("OrderID")+"|" +Eval("Number")%>'  ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                         <HeaderTemplate>收款</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Literal ID="labcz" runat="server"></asp:Literal>
           
                                      </ItemTemplate>
                                  </asp:TemplateField >
                                    <asp:TemplateField HeaderText="修改" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="HyperModify" runat="server" Text='<%#GetTran("000259", "修改")%>'  CommandArgument='<%#Eval("orderid") %>' CommandName="MDF"
                                                CausesValidation="false"   />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="delLink" runat="server" Text='<%#GetTran("000022", "删除")%>' OnClientClick='<%# "return confirm(\""+GetTran("000665", "您真的要删除此行吗？")+"\");" %>'
                                                CausesValidation="false" CommandName="Del" CommandArgument='<%#Eval("Number")+"|"+Eval("OrderExpectNum")+"|"+Eval("OrderID")+"|"+Eval("StoreID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                    <asp:BoundField HeaderText="会员编号" DataField="number" />
                                    <asp:TemplateField HeaderText="会员姓名">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# GetNumberName(Eval("name")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="支付状态">
							            <ItemStyle HorizontalAlign="Center" />
							            <ItemTemplate>
							                <%# GetPayStatus(DataBinder.Eval(Container.DataItem, "DefrayState").ToString())%>
							            </ItemTemplate>
							        </asp:TemplateField>
							        <asp:TemplateField HeaderText="支付方式">
							            <ItemStyle HorizontalAlign="Center" />
							            <ItemTemplate>
							                <%# GetDefrayName(DataBinder.Eval(Container.DataItem, "defrayType").ToString())%>
							            </ItemTemplate>
						        	</asp:TemplateField>
						        	 <asp:TemplateField HeaderText="发货方式">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# GetSendWay(DataBinder.Eval(Container.DataItem,"SendWay").ToString()) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:BoundField HeaderText="期数" DataField="orderExpectNum" />
                                    <asp:TemplateField HeaderText="金额">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server"  name="lblTotalMoney" Text='<%# Eval("totalMoney", "{0:n2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="积分" DataField="totalPv" DataFormatString="{0:n2}" HtmlEncode="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="报单日期">
							            <ItemStyle HorizontalAlign="Center" />
							            <ItemTemplate>
							                <%# GetRegisterDate(DataBinder.Eval(Container.DataItem, "OrderDate").ToString())%>
							            </ItemTemplate>
							        </asp:TemplateField>
							        <asp:BoundField HeaderText="操作者编号" DataField="Operatenum" />
                                     <asp:TemplateField HeaderText="公司确认状态">
							            <ItemStyle HorizontalAlign="Center" />
							            <ItemTemplate>
							                <%# GetCompany(DataBinder.Eval(Container.DataItem, "DefrayState").ToString())%>
							            </ItemTemplate>
							        </asp:TemplateField>
							        <asp:TemplateField HeaderText="店铺确认状态" Visible="false">
							            <ItemStyle HorizontalAlign="Center" />
							            <ItemTemplate>
							                <%# GetStore(DataBinder.Eval(Container.DataItem, "IsReceivables").ToString())%>
							            </ItemTemplate>
							        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
								        <ItemTemplate>
									        <%#SetVisible(DataBinder.Eval(Container.DataItem, "remark").ToString(), DataBinder.Eval(Container.DataItem, "orderId").ToString())%>
								        </ItemTemplate>
							        </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table class="tablemb bordercss" width="100%"  cellpadding="0" cellspacing="0">
                                        <tr>
                                            <th>
                                                <%=GetTran("000440", "查看")%>
                                            </th>
                                         
                                            <th> <%=GetTran("006048", "公司确认")%></th>
                                               <th><%=GetTran("006049", "店铺确认")%></th>   
                                            <th>
                                                <%=GetTran("000259", "修改")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000022", "删除")%>
                                            </th>
                                             <th>
                                                <%=GetTran("005605", "收款")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000024", "会员编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000025", "会员姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000775", "支付状态")%>
                                            </th>
                                            <th>
                                               <%=GetTran("000186", "支付方式")%> 
                                            </th>
                                            <th>
                                                <%=GetTran("001345", "发货方式")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000322", "金额")%>
                                            </th>
                                            <th>
                                               <%=GetTran("000414", "积分")%> 
                                            </th>
                                            <th>
                                                <%=GetTran("001429", "报单日期")%>
                                            </th>
                                            <th>
                                                <%=GetTran("007078", "操作者编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("006050", "公司确认状态")%>
                                            </th>
                                            <th>
                                                <%=GetTran("006051", "店铺确认状态")%>
                                            </th>
                                            <th>
                                               <%=GetTran("000078", "备注")%> 
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            
                        </center>
                    </div>
                </td>
            </tr>
            <tr>
                <td><uc1:Pager ID="Pager1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <table bordercolor="#cccccc" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="0">
            <!--<tr>
								<td height="36"><input class="button_green" onmousedown="this.className='button_out'" id="BtnConfirm" onmouseover="this.className='button_green'"
										style="DISPLAY: none" onmouseout="this.className='button_green'" type="submit" value="导出Excel"
										name="BtnConfirm"></td>
							</tr>-->
          <%--  <tr>
                <td class="zihz12">
                    <%=GetTran("000649", "功能说明")%> ：
                </td>
            </tr>--%>
           <%-- <tr>
                <td class="zihz12">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("001708", "1、显示所有会员在本店铺网上购物的报单；")%> 
                </td>
            </tr>
            <tr>
                <td class="zihz12">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("001709", "2、对会员网上购物的报单进行修改和删除（未支付的报单）；")%> 
                </td>
            </tr>
            <tr>
                <td class="zihz12">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("001710", " 3、对会员网上购物未支付的报单可修改支付方式并进行支付操作。")%>
                </td>
            </tr>--%>
        </table>
    </div>
     <%=msg %>
    </form>
</body>
</html>
