<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefundmentOrderForMemberAdd.aspx.cs" Inherits="Company_ProductOrders_RefundmentOrderBrowseForMember" EnableEventValidation="false"  %>

<%@ Register src="~/UserControl/ExpectNum.ascx" tagname="ExpectNum" tagprefix="uc1" %>
<%@ Register src="~/UserControl/Pager.ascx" tagname="Pager" tagprefix="uc2" %>
<%@ Register src="../../UserControl/CountryCity.ascx" tagname="CountryCity" tagprefix="uc3" %>
<%@ Register src="../../UserControl/UCBank.ascx" tagname="UCBank" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员退货申请</title>
    
    <script language="javascript" type="text/javascript" src="../../js/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/jquery-1.4.1-vsdoc.js"></script>
    <link href="../CSS/Company.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../../javascript/My97DatePicker/WdatePicker.js"></script>
<script src="../../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript">

        function FunCheckOrUnCheckAll(obj) {
            var isChecked = obj.checked;
            var hidObj = document.getElementById("<%=hidSelectStatus.ClientID %>");
            var hidValue = 0;
            if (isChecked) {
                hidValue = 1;
            }
            else {
                hidValue = 0;
            }
            hidObj.value = hidValue;
            var hidBtn = document.getElementById("<%=lbtnSelectAll.ClientID %>");
            hidBtn.click();
        }

        function FunIninChk() {
            var hidObj = document.getElementById("<%=hidSelectStatus.ClientID %>");
            var obj = document.getElementById("chkAll");
            if (obj != null && obj != undefined) {
                var hidValue = hidObj.value;
                if (hidValue == 1)
                    obj.checked = true;
                else
                    obj.checked = false;
            }
        }
        function FunReCheck() {
            var lbtn = document.getElementById("<%=lbtn_SelectCheck.ClientID %>");

            if (lbtn != null && lbtn != undefined) {
                lbtn.click();
            }
        }

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

        function FunAddQuantity(objid) {
            var objTxt = document.getElementById(objid);
            alert(objTxt);
        }

        function FunAddQuantity(objid) {
            var objTxt = document.getElementById(objid);
            alert(objTxt);
        }
        function FunRefreshDetails() {
            var objBtn = document.getElementById("<%=lbl_ReBindRefundmentDetails.ClientID %>");
            if (objBtn != null && objBtn != null)
                objBtn.click();
        }
        //<input type='checkbox' id='chkAll' onclick='FunCheckOrUnCheckAll(this);'"
        function GetCCode_s2(xian)
        { }
    </script>

    
    <style type="text/css">
        #secTable
        {
            width: 150px;
        }
        
        .frameclass{
            width:800px;overflow-x:auto; overflow-y:scroll;word-break:break-all; display :none;
            border-left:solid 1px #c6d6fd;
            border-top:solid 1px #c6d6fd;
            border-right:solid 1px #c6d6fd;
            border-bottom:solid 1px #c6d6fd;
            text-align:left;
        }
        #tbReturnOrderBill td{background-color:#ffffff;}
        .cell1_1{ text-align:center;width:120px;}
        .cell1_2{ text-align:left;width:146px;}
        .cell2_1{ width:120px;text-align:center;}
        .cell2_2{ text-align:left;width:146px;}
        .cell3_1{ text-align:center;width:120px;}
        .cell3_2{width:142px;text-align:left;}
        .title{ font-size:11pt;font-weight:bold;padding-top:8px;padding-bottom:5px;text-align:center;}
        .titleM{ font-size:10pt;font-weight:bold;padding-top:4px;padding-bottom:4px;text-align:center;background-color: #CAE1D9}
        .avgCell{ width:10%;text-align:center;font-size:9pt;}
        #txt_reson
        {
            width: 239px;
        }
        .tdleft{ text-align:left;}
        .tdcenter{ text-align:center;}
        .tdright{ text-align:right;}
		.hidObj{ display:none;}
		.expObj{ display:block;}
        .style1
        {
            color: #FF0066;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server" ></asp:ScriptManager>
    <br />
       <table align="center" width="90%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                        class="biaozzi">
                        
    <asp:Panel ID="pnl_title" runat="server" Visible="true" >
                        <tr>
                            <td colspan="4" align="center" nowrap="nowrap" bgcolor="#EBF1F1" style="font-size:11pt;font-weight:bold;
                            padding-top:8px;padding-bottom:5px;">                              
                            <%=GetTran("007739", "会员退货")%> <asp:Label ID="lblDealingOrderID" runat="server" ></asp:Label><asp:Label ID="lbl_Title" runat="server" ></asp:Label></td>
                        </tr>
   </asp:Panel>
                        <tr>
                        <td align="center" >
                        
    <div>
    <asp:Panel ID="pnl_OrderList" runat="server">
    <table width="99%" class="biaozzi" border="0" cellpadding="0" cellspacing="0" style=" word-break: keep-all;word-wrap: normal;text-align:left;">
        <tr>
            <td>
               
                <asp:Button ID="btnSearch" runat="server" CssClass="anyes" Text="查询" OnClick="btnSearch_Click"  />
                                    <span style=" display:none;">
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Selected="True" Value="0">报单期数</asp:ListItem>
                    <asp:ListItem Value="1">审核期数</asp:ListItem>
                </asp:DropDownList><uc1:ExpectNum ID="ExpectNum1" runat="server" />
                <%=this.GetTran("000719", "并且")%></span>
                <asp:DropDownList ID="ddlContion" runat="server" Style="word-break: keep-all; word-wrap: normal;"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlContion_SelectedIndexChanged">
                    <asp:ListItem Value="B.OrderID" Selected="True">订单号</asp:ListItem>
                    <asp:ListItem Value="B.TotalMoney">金额</asp:ListItem>
                    <asp:ListItem Value="B.TotalPv">积分</asp:ListItem>
                    <asp:ListItem Value="A.StoreId">编号</asp:ListItem>
                    <asp:ListItem Value="A.Number">会员编号</asp:ListItem>
                    <asp:ListItem Value="A.Name">会员姓名</asp:ListItem>                    
                </asp:DropDownList>                
                <asp:DropDownList ID="ddlcompare" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtContent" runat="server" Width="207px"></asp:TextBox>&nbsp;&nbsp;
                <asp:LinkButton  style="display:none;"
                    ID="lbtnSelectAll" runat="server" onclick="lbtnSelectAll_Click"></asp:LinkButton>
                <asp:LinkButton  style="display:none;"
                    ID="lbtn_SelectCheck" runat="server" onclick="lbtn_SelectCheck_Click" ></asp:LinkButton>
                <asp:HiddenField ID="hidSelectStatus" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    <table width="100%"  style="word-break:keep-all;word-wrap:normal;">
        <tr>
            <td style="border:rgb(147,226,244) solid 1px">
                <asp:GridView ID="gv_browOrder" runat="server" AutoGenerateColumns="False" 
                    BackColor="#F8FBFD" CssClass="tablemb bordercss" 
                    Width="100%" onrowcommand="gv_browOrder_RowCommand" 
                    onrowdatabound="gv_browOrder_RowDataBound" 
                    ondatabound="gv_browOrder_DataBound">
                    <Columns>
                                           <asp:TemplateField HeaderText="退货">
                                                                <ItemTemplate>                                                                                                   
                                                                  <asp:CheckBox ID="chk" Visible ="false"  runat="server" onclick="FunReCheck();"/>
                                                                  <asp:LinkButton ID="lbtn_Refundment" runat="server" CommandName ="refund" CommandArgument='<%# Eval("OrderId") %>' ><%=GetTran("002224", "退货")%></asp:LinkButton>
                                                                </ItemTemplate>
                                             
                                                                <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                                                <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                             
                                            </asp:TemplateField>                      
                                            <asp:BoundField HeaderText="订单号"  DataField="OrderId"></asp:BoundField>
                                            <asp:BoundField HeaderText="会员编号" DataField="number"></asp:BoundField>
                                            <asp:BoundField HeaderText="会员姓名" DataField="name"></asp:BoundField>
                                            <asp:BoundField HeaderText="报单店铺编号" DataField="OStoreId" Visible="false" ></asp:BoundField>
                                            <asp:BoundField HeaderText="报单类型" DataField="orderType"  Visible="false" ></asp:BoundField>                       
                                            <asp:BoundField HeaderText="期数" DataField="orderExpectNum"></asp:BoundField>
                                            <asp:TemplateField HeaderText="金额">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalMoney" name="lblTotalMoney" runat="server" Text='<%# Eval("totalMoney", "{0:n2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                            <asp:BoundField HeaderText="积分" DataField="totalPv" DataFormatString="{0:n2}" HtmlEncode="False">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="报单时间" ItemStyle-Wrap="false">
							                        <ItemStyle HorizontalAlign="Center" />
							                        <ItemTemplate>
							                            <%# GetRegisterDate(DataBinder.Eval(Container.DataItem, "OrderDate").ToString())%>
							                        </ItemTemplate>
							                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="查看" >    
                                                <ItemStyle HorizontalAlign="Center" />                      
                                                  <ItemTemplate>
                                                   <img src="../images/fdj.gif" /> 
                                                    <asp:LinkButton ID="lbtn_Details" runat="server"   CommandArgument='<%#Eval("OrderId") %>' 
                                                          CommandName='details' ><%=GetTran("007744", "查看订单明细")%></asp:LinkButton>
                                                    <asp:Label ID="lbl_OrderId" runat ="server" Text ='<%#Eval("OrderId") %>'  Visible="false"></asp:Label>
                                                  </ItemTemplate>
                                               </asp:TemplateField>
                            
                    </Columns>                    
            <EmptyDataTemplate>
                <table cellspacing="0" style="width:100%;">
                    <tr>
                        <th nowrap>
                           <%=GetTran("002224", "退货")%>
                        </th>
                        <th nowrap>
                            <%=GetTran("000079", "订单号")%>
                        </th>
                        <th nowrap>
                           <%=GetTran("000024", "会员编号")%>
                        </th>
                        <th nowrap>
                            <%=GetTran("000025", "会员姓名")%>
                        </th>
                        <th nowrap>
                           <%=GetTran("000045", "期数")%>
                        </th>
                        <th nowrap>
                           <%=GetTran("000322", "金额")%>
                        </th>
                        <th nowrap>
                            <%=GetTran("000414", "积分")%>
                        </th>
                        <th nowrap>
                           <%=GetTran("005942","报单时间") %>
                        </th>
                        <th nowrap>
                          <%=GetTran("000440","查看") %>
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="#F1F4F8" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
        <td>        
     <uc2:Pager ID="Pager1" runat="server" />
    <script type="text/javascript" >
   // FunIninChk();
    </script>
        </td>
        </tr>
        <tr>
        <td>
            <asp:Button ID="btn_ReturnConfrim" runat="server" CssClass="anyes" Visible="false" 
                OnClick="btn_ReturnConfrim_Click" Text="确定退货" />
                &nbsp; <input id="btnBack" value='<%=GetTran("000421","返回") %>' class="anyes" type="button" onclick="javascript:window.location.href='RefundmentOrderForMemberList.aspx';"/>
        </td>
        </tr>
    </table>
    </asp:Panel>
    
    <asp:Panel ID="pnl_OrderDetailsList" Visible="false" runat="server"  >   
    <table width="100%" style="word-break:keep-all;word-wrap:normal;">
    <tr>
    <td>
        <asp:GridView ID="gv_OrderDetails" runat="server" AutoGenerateColumns="False" 
            Cssclass="tablemb bordercss" width="100%">
            <Columns>
                <asp:TemplateField HeaderText="订单号">
                    <ItemTemplate>
                        <asp:Label ID="lbl_OrderID" runat="server" 
                            Text='<%# Eval("OrderId") %>'></asp:Label>
                        <asp:Label ID="lbl_ProductID" Visible ="false"  runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                        <asp:Label ID="lbl_UseQuantity" Visible ="false"  runat="server" Text='<%# Eval("UseQuantity") %>'></asp:Label>
                        <asp:Label ID="lbl_Quantity" Visible ="false"  runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </asp:TemplateField>
                <asp:BoundField DataField="ProductCode" HeaderText="产品编码" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ProductName" HeaderText="产品名称" />
                <asp:BoundField DataField="UnitPrice" HeaderText="单价" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UnitPV" HeaderText="积分" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="订单数量" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LeftQuantity" HeaderText="剩余数量" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="退货数量" Visible="false">
                    <EditItemTemplate>                        
                    </EditItemTemplate>
                    <ItemTemplate>
                       <asp:TextBox ID="txt_UseNum" runat="server" Text='<%# Eval("UseQuantity") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <table cellspacing="0">
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
                        <th nowrap  style="display:none;">
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
    <td style="text-align:center;">
               
            <asp:Button ID="btn_ConfrimDetails" runat="server" CssClass="anyes"  Visible="false" 
            Text="确定" OnClick="btn_ConfrimDetails_Click"  />
            &nbsp;
            <asp:Button ID="btn_Cancel" runat="server" CssClass ="anyes" Text ="取消" Visible="false"  onclick="btn_Cancel_Click" 
                    />
            &nbsp;
            <asp:Button ID="btn_backtoOrderList" runat="server" CssClass ="anyes" Text ="返回" 
                    onclick="btn_backtoOrderList_Click" />
                &nbsp;</td></tr>
    </table>
     </asp:Panel>
     
    <asp:Panel ID="pnl_ReturnOrderConfirm" runat="server" Visible="false" >
     <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                        class="biaozzi" >
                      
                        <tr>
                        <td align="center" >
                       
                        <table id="tbReturnOrderBill" border="0" cellpadding="0px" cellspacing="1px" style="background-color:Gray;width:100%;">
                        <tr>
                            <td colspan="6" align="center" nowrap="nowrap" bgcolor="#EBF1F1" style="font-size:11pt;font-weight:bold;
                            padding-top:8px;padding-bottom:5px; background-color:#EBF1F1;">                              
                               <%=GetTran("007779", "退货申请单")%> </td>
                        </tr>
                        
                        <tr>
                        <td class="cell1_1">                        
                           <%=GetTran("005611", "订单编号")%> 
                            </td>
                        <td style="text-align :left;" colspan="5">
                        
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
                            <asp:TextBox ID="txt_ApplyName" runat="server" Width="108px" ReadOnly="true" ></asp:TextBox>                    
                         </td>
                        <td class="cell3_1">
                        
                           <%=GetTran("000024", "会员编号")%> </td>
                        <td class="cell3_2">
                        
                            <asp:Label ID="lbl_MemberNumber" runat="server"></asp:Label>
                        
                        </td>
                        </tr>
                            <tr>
                                <td class="cell1_1">
                                   <%=GetTran("001814", "退货日期")%> </td>
                                <td class="cell1_2">                          
                                <asp:TextBox ID="txt_refundmentDate" runat="server"  CssClass='Wdate' onclick='WdatePicker()'></asp:TextBox>
                                    </td>
                                <td class="cell1_1">
                                    <%=GetTran("007781", "加入日期")%>  
                                </td>
                                <td class="cell2_2">                                
                                <asp:Label ID="lbl_RegesterDate" runat="server"></asp:Label>
                                    </td>
                                <td class="cell3_1">
                                   <%=GetTran("000115", "联系电话")%> </td>
                                <td class="cell3_2">
                                    <asp:TextBox ID="txt_Phone" runat="server" Width="115px"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr style=" display:none;">
                                <td class="cell1_1">
                                    <%=GetTran("007782", "退款方式")%></td>
                                <td style="text-align:left;" colspan="5">
                                    <asp:RadioButton ID="rbtn_1" runat="server" Checked="true" 
                                        GroupName="AccountBackType" onclick="FunRefundTypeSelect(0)" Text="退还至电子账户" />
                                    <asp:RadioButton ID="rbtn_0"  onclick="FunRefundTypeSelect(0)"  Text="现金退还" GroupName="AccountBackType"  runat="server" />
                                    <asp:RadioButton ID="rbtn_2"  onclick="FunRefundTypeSelect(1)" Text="退还银行账户" GroupName="AccountBackType"  runat="server" />
                                </td>
                            </tr>
                            <tr id="TrBackType_1" class="hidObj">
                                <td class="tdcenter cell1_1">
                                    <%=GetTran("001243", "开户行")%></td>
                                <td colspan="5" style="text-align :left;">                                   
                                    <uc4:UCBank ID="UCBank1" runat="server" />
                                </td>
                            </tr>
                            <tr id="TrBackType_2" class="hidObj">
                                <td class="tdcenter cell1_1">
                                    <%=GetTran("006046", "支行名")%></td>
                                <td  colspan="5" style="text-align:left;">                                    
                                         <asp:TextBox ID="txt_BankBranch" runat="server" Width="283px"></asp:TextBox>
                                         <span class="style1">*</span></td>
                            </tr>
                            <tr ID="TrBackType_3" class="hidObj">
                                <td class="tdcenter cell1_1">
                                   <%=GetTran("000086", "开户名")%> </td>
                                <td colspan="5" style="text-align:left;">
                                    <asp:TextBox ID="txt_BankBookName" runat="server"></asp:TextBox>
                                    <span class="style1">*</span></td>
                            </tr>
                            <tr ID="TrBackType_4" class="hidObj">
                                <td class="tdcenter cell1_1">
                                    <%=GetTran("000111", "银行账号")%></td>
                                <td colspan="5" style="text-align:left;">
                                    <asp:TextBox ID="txt_BankCard" runat="server" Width="227px"></asp:TextBox>
                                    <span class="style1">*</span></td>
                            </tr>
                            <tr>
                                <td class="cell1_1">
                                   <%=GetTran("007783", "退货地址")%> </td>
                                <td  colspan="5" style="text-align:left;">                                    
                                        <asp:Label ID="lbl_ReturnAddress" runat="server" Visible="False"></asp:Label>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                        <td><uc3:CountryCity ID="CountryCity1" runat="server" />
                                        </td>
                                        <td>
                                        <asp:TextBox runat="server" ID="txt_addressDetails" MaxLength="100" Width="160px"></asp:TextBox>
                                            <span class="style1">*</span></td></tr>
                                        </table> 
                                </td>
                            </tr>
                            <tr>
                                <td class="cell1_1">
                                    <%=GetTran("007784", "报单总额")%></td>
                                <td  colspan="2"  style="text-align:center;">
                                    
                            <asp:Label ID="lbl_MemberTotalMoney" runat="server"></asp:Label>
                                    
                                    </td>
                                <td style="text-align:center;">
                                   <%--<%=GetTran("007785", "领取奖金总额")%>--%>报单总PV</td>
                                <td colspan="2"  style="text-align:center;">
                                            <asp:Label ID="lab_TotalPV" runat="server"></asp:Label>
                            <asp:Label ID="lbl_MemberJJ" runat="server" Visible="false"></asp:Label>
                            
                            </td>
                            </tr>
                            <tr>
                                <td class="titleM" colspan="6">
                                   <%= GetTran("007786", "退货原因")%> </td>
                            </tr>
                            <tr>                           
                                <td ></td>
                                <td colspan="5" style="text-align:left;">
                               <asp:TextBox ID="txt_Reson" runat="server"  MaxLength="100" 
                                            Width="542px" TextMode="MultiLine" Height="91px" ></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td class="cell1_1">
                                    &nbsp;<%=GetTran("007787", "注意事项")%>&nbsp;</td>
                                <td  colspan="5" style="text-align:left;">
                                    <asp:Literal ID="ltrl_Warming" runat="server" ></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleM" colspan="6">
                                    <%=GetTran("007788", "退货产品详细")%></td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                <asp:UpdatePanel ID="upd_Details" runat ="server" >
                                <ContentTemplate>
                                <table border="0px" cellpadding="0" cellspacing="0"  style="width:100%;">
                                <tr>
                                <td colspan="10" style="text-align:center;">
                                    <asp:GridView ID="gv_OrderDetailsAll" runat="server" 
                                        AutoGenerateColumns="False" Cssclass="tablemb bordercss" width="100%" 
                                        onrowcommand="gv_OrderDetailsAll_RowCommand" 
                                        onrowdatabound="gv_OrderDetailsAll_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="订单号">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_OrderID" runat="server" Text='<%# Eval("OrderId") %>'></asp:Label>
                                                    <asp:Label ID="lbl_ProductID" runat="server" Text='<%# Eval("ProductID") %>' 
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_LeftQuantity" runat="server" 
                                                        Text='<%# Eval("LeftQuantity") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_Quantity" runat="server" Text='<%# Eval("Quantity") %>' 
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_ProductName" runat="server" Text='<%# Eval("ProductName") %>' 
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
                                            <asp:BoundField DataField="Quantity" HeaderText="订单数量">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeftQuantity" HeaderText="剩余数量">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="退货数量">
                                                <ItemTemplate>
                                                 <table  cellpadding="0" cellspacing="0" border="0">
                                                 <tr>
                                                     <td rowspan="2" style="text-align:right;">
                                                        <asp:TextBox ID="txt_UseQuantity" style="width:60px;" runat ="server" onchange="FunRefreshDetails()"   Text='<%# Eval("UseQuantity") %>'></asp:TextBox>
                                                     </td>
                                                     <td style="width:16px;" valign="bottom">
                                                        <asp:ImageButton ID="imgBtnUp" CommandName="add" CommandArgument='<%# Eval("OrderId")+"_"+Eval("ProductID") %>' runat="server"  ImageUrl="../images/QuantityAdd.png" AlternateText="数量加1" />
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                    <td style="width:16px;" valign="top">                                                 
                                                        <asp:ImageButton ID="imgBtnSub" CommandName="sub"  runat="server" ImageUrl="../images/QuantitySub.png" AlternateText="数量减1" />
                                                    </td>
                                                 </tr>
                                                 </table>
                                                <asp:Label ID="lbl_UseNum" runat="server" Visible="false" Text='<%# Eval("OrderId")+"_"+Eval("ProductID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table cellspacing="0">
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
                               
                               <asp:LinkButton ID="lbl_ReBindRefundmentDetails" runat="server" 
                                        onclick="lbl_ReBindRefundmentDetails_Click"></asp:LinkButton>
                               
                               </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                </td>
                            </tr>
                            <tr>
                                <td class="tdcenter" colspan="6">
                                    <asp:Button ID="btn_ConfrimAndSubmit" runat="server" CssClass="anyes" 
                                        OnClick="btn_ConfrimAndSubmit_Click" Text="确定" />
                                    &nbsp;<asp:Button ID="btn_back" runat="server" CssClass="anyes" 
                                        onclick="btn_back_Click" Text="返回" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdcenter" colspan="6">
                                    <asp:Label ID="lblMsg" runat="server" Font-Size="9pt" ForeColor="Red"></asp:Label>
                                    
                                </td>
                            </tr>
                        </table>
                           <div id="divAgreement" class="frameclass"></div>
                        </td>
                        </tr>
                        </table>
    </asp:Panel>
     </div>
                        
                        
                        </td>
                        </tr>
                        </table>
    
    
    </form>
</body>
</html>
