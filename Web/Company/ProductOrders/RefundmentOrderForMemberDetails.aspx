<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefundmentOrderForMemberDetails.aspx.cs" Inherits="Company_ProductOrders_RefundmentOrderForMemberDetails" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register Src="../../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc3" %>
<%@ Register Src="../../UserControl/UCWareHouse.ascx" TagName="UCWareHouse" TagPrefix="uc6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员退货明细</title>

    <script language="javascript" type="text/javascript" src="../../js/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/jquery-1.4.1-vsdoc.js"></script>
    <link href="../CSS/Company.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../javascript/My97DatePicker/WdatePicker.js"></script>
    <script src="../../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript">
       function FunRefundTypeSelect(selectValue) {
           // var defaultValue = 0;
           if (selectValue == 0) {
               for (var i = 1; i <= 4; i++) {
                   document.getElementById("TrBackType_" + i.toString()).className = "hidObj";
                   // document .getElementById ("groupsell1").className
               }
           }
           else {
               for (var i = 1; i <= 4; i++) {
                   document.getElementById("TrBackType_" + i.toString()).className = "expObj";
               }
           }
       }
    </script>

    <style type="text/css">
        #secTable {
            width: 150px;
        }

        .frameclass {
            width: 800px;
            overflow-x: auto;
            overflow-y: scroll;
            word-break: break-all;
            border-left: solid 1px #c6d6fd;
            border-top: solid 1px #c6d6fd;
            border-right: solid 1px #c6d6fd;
            border-bottom: solid 1px #c6d6fd;
            text-align: left;
        }

        #tbReturnOrderBill td {
            background-color: #ffffff;
        }

        .cell1_1 {
            text-align: center;
            width: 120px;
        }

        .cell1_2 {
            text-align: left;
            width: 146px;
        }

        .cell2_1 {
            width: 120px;
            text-align: center;
        }

        .cell2_2 {
            text-align: left;
            width: 146px;
        }

        .cell3_1 {
            text-align: center;
            width: 120px;
        }

        .cell3_2 {
            width: 142px;
            text-align: left;
        }

        .title {
            font-size: 11pt;
            font-weight: bold;
            padding-top: 8px;
            padding-bottom: 5px;
            text-align: center;
        }

        .titleM {
            font-size: 10pt;
            font-weight: bold;
            padding-top: 4px;
            padding-bottom: 4px;
            text-align: center;
            background-color: #CAE1D9;
        }

        .avgCell {
            width: 10%;
            text-align: center;
            font-size: 9pt;
        }

        #txt_reson {
            width: 239px;
        }

        .tdleft {
            text-align: left;
        }

        .tdcenter {
            text-align: center;
        }

        .tdright {
            text-align: right;
        }

        .hidObj {
            display: none;
        }

        .expObj {
            display: block;
        }

        .style1 {
            height: 82px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <br />
        <table align="center" width="80%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
            class="biaozzi">


            <tr>
                <td align="center">

                    <div>
                        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                            class="biaozzi">
                            <tr>
                                <td align="center">
                                    <br />
                                    <table id="tbReturnOrderBill" border="0" cellpadding="0px" cellspacing="1px" style="background-color: Gray; width: 100%;">
                                        <tr>
                                            <td align="center" nowrap="nowrap" style="font-size: 11pt; font-weight: bold; background-color: #EBF1F1; padding-top: 8px; padding-bottom: 5px;"
                                                colspan="6">
                                                <asp:Label ID="lbl_AuditTag" Visible="false" runat="server"><%=GetTran("000761", "审核")%></asp:Label><%=GetTran("007779", "退货申请单")%>[<asp:Label ID="lblDealingOrderID" runat="server"></asp:Label>]
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cell1_1">
                                                <%=GetTran("005611", "订单编号")%> 
                                            </td>
                                            <td style="text-align: left;" colspan="5">

                                                <asp:Label ID="lbl_OrderIDS" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cell1_1">
                                                <%=GetTran("000025", "会员姓名")%></td>
                                            <td class="cell1_2">
                                                <asp:Label ID="lbl_MemberName" runat="server"></asp:Label>
                                            </td>
                                            <td class="cell2_1">
                                                <%=GetTran("007780", "申请人姓名")%> </td>
                                            <td class="cell2_2">
                                                <asp:Label ID="lbl_ApplyName" runat="server"></asp:Label>
                                            </td>
                                            <td class="cell3_1">

                                                <%=GetTran("000024", "会员编号")%></td>
                                            <td class="cell3_2">

                                                <asp:Label ID="lbl_MemberNumber" runat="server"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cell1_1">
                                                <%=GetTran("001814", "退货日期")%> </td>
                                            <td class="cell1_2">
                                                <asp:Label ID="lbl_ReturnDate" runat="server"></asp:Label>
                                            </td>
                                            <td class="cell1_1">
                                                <%=GetTran("007781", "加入日期")%>  
                                            </td>
                                            <td class="cell2_2">
                                                <asp:Label ID="lbl_RegesterDate" runat="server"></asp:Label>
                                            </td>
                                            <td class="cell3_1">
                                                <%=GetTran("000115", "联系电话")%></td>
                                            <td class="cell3_2">
                                                <asp:Label ID="lbl_Phone" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr style="display: none;">
                                            <td class="cell1_1">
                                                <%=GetTran("007782", "退款方式")%></td>
                                            <td style="text-align: left;" colspan="5">
                                                <asp:Label ID="lbl_refundmentTypeName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="pnl_bankInfo" runat="server">
                                            <tr id="TrBackType_1" class="hidObj">
                                                <td class="tdcenter">
                                                    <%=GetTran("001243", "开户行")%></td>
                                                <td colspan="5" style="text-align: left;">

                                                    <asp:Label ID="lbl_BankName" runat="server"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr id="TrBackType_2" class="expObj">
                                                <td class="tdcenter">
                                                    <%=GetTran("006046", "支行名")%></td>
                                                <td colspan="5" style="text-align: left;">
                                                    <asp:Label ID="lbl_BankBranch" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="TrBackType_3" class="expObj">
                                                <td class="tdcenter">
                                                    <%=GetTran("000086", "开户名")%> </td>
                                                <td colspan="5" style="text-align: left;">
                                                    <asp:Label ID="lbl_BankAccountName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="TrBackType_4" class="expObj">
                                                <td class="tdcenter">
                                                    <%=GetTran("000111", "银行账号")%></td>
                                                <td colspan="5" style="text-align: left;">
                                                    <asp:Label ID="lbl_BankAccountNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td class="cell1_1">
                                                <%=GetTran("007783", "退货地址")%> </td>
                                            <td colspan="5" style="text-align: left;">
                                                <asp:Label ID="lbl_ReturnAddress" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cell1_1">
                                                <%=GetTran("007784", "报单总额")%></td>
                                            <td colspan="2" style="text-align: center;">

                                                <asp:Label ID="lbl_MemberTotalMoney" runat="server"></asp:Label>

                                            </td>
                                            <td style="text-align: center;">
                                                <%--<%=GetTran("007785", "领取奖金总额")%>--%>报单总PV</td>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Label ID="lab_mtpv" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_MemberJJ" runat="server" Visible="false"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titleM" colspan="6">
                                                <%= GetTran("007786", "退货原因")%> </td>
                                        </tr>
                                        <tr>
                                            <td class="style1"></td>
                                            <td colspan="5" class="style1" style="text-align: left;">
                                                <asp:Label ID="lbl_Reson" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="cell1_1">&nbsp;<%=GetTran("007787", "注意事项")%>&nbsp;</td>
                                            <td colspan="5" style="text-align: left;">
                                                <asp:Literal ID="ltrl_Warming" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titleM" colspan="6">
                                                <%=GetTran("007788", "退货产品详细")%></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <table border="0px" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <asp:GridView ID="gv_OrderDetailsAll" runat="server"
                                                                AutoGenerateColumns="False" CssClass="tablemb bordercss" Width="100%">
                                                                <Columns>
                                                                    <%--<asp:TemplateField HeaderText="订单号">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_OrderID" runat="server" Text='<%# Eval("OriginalDocID") %>'></asp:Label>
                                                    <asp:Label ID="lbl_ProductID" runat="server" Text='<%# Eval("ProductID") %>' 
                                                        Visible="false"></asp:Label>                                                   
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductCode" HeaderText="产品编码">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductName" HeaderText="产品名称" />
                                            <asp:BoundField DataField="UnitPrice" HeaderText="单价">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UnitPV" HeaderText="积分">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OrderQuantity" HeaderText="订单数量">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeftQuantity" HeaderText="剩余数量">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QuantityReturning" HeaderText="退货数量">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>--%>

                                                                    <asp:TemplateField HeaderText="订单号">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_OrderID" runat="server" Text='<%# Eval("DocID") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_ProductID" runat="server" Text='<%# Eval("ProductID") %>'
                                                                                Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ProductCode" HeaderText="产品编码">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProductName" HeaderText="产品名称" />
                                                                    <asp:BoundField DataField="UnitPrice" HeaderText="单价">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PV" HeaderText="积分">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="productQuantity" HeaderText="退货数量">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table cellspacing="0" style="width: 100%;">
                                                                        <tr>
                                                                            <th nowrap>
                                                                                <%=GetTran("000079", "订单号")%> 
                                                                            </th>
                                                                            <th nowrap>
                                                                                <%=GetTran("000263", "产品编码")%>  
                                                                            </th>
                                                                            <th nowrap>
                                                                                <%=GetTran("000501", "产品名称")%> 
                                                                            </th>
                                                                            <th nowrap>
                                                                                <%=GetTran("000503", "单价 ")%> 
                                                                            </th>
                                                                            <%= GetTran("000414", "积分 ")%>
                                                                            <th nowrap>
                                                                                <%=GetTran("007774", "订单数量")%> 
                                                                            </th>
                                                                            <th nowrap>
                                                                                <%= GetTran("007720", "剩余数量")%>  
                                                                            </th>
                                                                            <th nowrap>
                                                                                <%=GetTran("001982", "退货数量")%> 
                                                                            </th>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="tablebt bbb" />
                                                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="avgCell"><%= GetTran("007802", "统计")%></td>
                                                        <td class="avgCell">&nbsp;</td>
                                                        <td class="avgCell"><%=GetTran("000041", "总金额")%>：</td>
                                                        <td class="avgCell">
                                                            <asp:Label ID="lbl_MemberTotalMoney2" runat="server">0</asp:Label>
                                                        </td>
                                                        <td class="avgCell"><%=GetTran("004148", "总PV")%>：</td>
                                                        <td class="avgCell">
                                                            <asp:Label ID="lbl_MemberTotalPV" runat="server">0</asp:Label>
                                                        </td>
                                                        <td class="avgCell"><%=GetTran("001916", "退货总金额")%>：</td>
                                                        <td class="avgCell">
                                                            <asp:Label ID="lbl_ReturnTotalMoney" runat="server">0</asp:Label>

                                                        </td>
                                                        <td class="avgCell"><%=GetTran("007803", "退货总PV")%>：</td>
                                                        <td class="avgCell">
                                                            <asp:Label ID="lbl_ReturnTotalPV" runat="server">0</asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="pnl_warehouse" runat="server" Visible="true">

                                            <tr>
                                                <td class="tdcenter" colspan="1">
                                                    <%=GetTran("007820", "产品退货入库")%> </td>
                                                <td class="tdleft" colspan="5">
                                                    <uc6:UCWareHouse ID="UCWareHouse1" runat="server" />
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td class="tdcenter" colspan="6">&nbsp;<asp:Button ID="btn_AuditSubmit" runat="server" CssClass="anyes"
                                                Text="确定退货" OnClick="btn_AuditSubmit_Click" />
                                                &nbsp;<asp:Button ID="btnBack0" runat="server" CssClass="anyes"
                                                    Text="返回" OnClick="btnBack0_Click" />&nbsp;
                                        <asp:Panel ID="pnl_Back" runat="server" Visible="false">
                                            <input id="btnBack" value='<%= GetTran("000421", "返回") %>' class="anyes" type="button" onclick="javascript:history.back();" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divAgreement" class="frameclass" style="display: none;">
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnl_ReturnOrderConfirm" runat="server" Visible="false">
                        </asp:Panel>
                    </div>


                </td>
            </tr>
        </table>


    </form>
</body>
</html>
