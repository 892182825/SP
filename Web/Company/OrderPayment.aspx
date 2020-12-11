<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderPayment.aspx.cs" Inherits="Company_OrderPayment" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script>
	 $(document).ready(function(){
			if($.browser.msie && $.browser.version == 6) {
				FollowDiv.follow();
			}
	 });
	 FollowDiv = {
			follow : function(){
				$('#cssrain').css('position','absolute');
				$(window).scroll(function(){
				    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
					$('#cssrain').css( 'top' , f_top );
				});
			}
	  }
    </script>

    <script language="javascript" type="text/javascript">
	function down2()
	{
		if(document.getElementById("divTab2").style.display=="none")
		{
			document.getElementById("divTab2").style.display="";
			document.getElementById("imgX").src="images/dis1.GIF";
			
		}
		else
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="images/dis.GIF";
		}
	}
    </script>

    <script language="javascript" type="text/javascript">
         function secBoard(n)
         {
              for(i=0;i<secTable.cells.length;i++)
              secTable.cells[i].className="sec1";
              secTable.cells[n].className="sec2";
              for(i=0;i<mainTable.tBodies.length;i++)
              mainTable.tBodies[i].style.display="none";
              mainTable.tBodies[n].style.display="block";
          }
    </script>

    <script type="text/javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
         }
         function isDelete1() { 
           
         
       return confirm('<%= GetTran("007701","确定要为店铺支付此订单么？") %>');

   }
   function isDelete2() { 
   
   
   return confirm('<%= GetTran("000248","确定要删除吗？") %>');
   
   }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top">
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td>
                            <%=GetTran("000150", "店铺编号")%>
                            <asp:TextBox ID="txtStoreID" runat="server" Width="120px" MaxLength="10"></asp:TextBox>
                           
                            <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="anyes" 
                                onclick="btnSelect_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table id="tbStore" runat="server" class="biaozzi" width="99%" style="border: solid 0xp white;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="right">
                                        <font face="宋体">
                                            <%=GetTran("000909", "当前货币")%>:</font>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%=GetTran("000911", "当前报单款订货余额")%>:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOrderMoney" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <font face="宋体">
                                            <%=GetTran("000913", "当前周转款订货余额")%>:</font>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTurnMoney" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: rgb(147,226,244) solid 1px">
                            <asp:GridView ID="gvStoreOrder" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,StoreID"
                                OnRowDataBound="gvStoreOrder_RowDataBound" OnDataBound="gvStoreOrder_DataBound"
                                Style="margin-right: 0px" CssClass="tablemb bordercss" Width="99%" 
                                onrowcommand="gvStoreOrder_RowCommand">
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSell" runat="server" />
                                            <asp:Literal ID="ltSell" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            
                                            <asp:LinkButton ID="lbtnOk" runat="server" CommandName="OK" CommandArgument='<%#Eval("OrderGoodsID")+":"+Eval("StoreId")+":"+Eval("IsCheckOut")+":"+Eval("TotalMoney")+":"+Eval("ordertype") %>' OnClientClick="return isDelete1();"><%# GetTran("000938", "支付")%></asp:LinkButton>
                                            <input id="HidTotalMoney" type="hidden" runat="server" value='<%#DataBinder.Eval(Container,"DataItem.TotalMoney") %>' />
                                            <asp:LinkButton ID="lbtnDel" runat="server" CommandName="D" CommandArgument='<%#Eval("OrderGoodsID")+":"+Eval("StoreId")+":"+Eval("IsCheckOut")+":"+Eval("TotalMoney")+":"+Eval("ordertype") %>' OnClientClick="return isDelete2();"><%# GetTran("000022", "删除")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="订单号" DataField="OrderGoodsID" />
                                    <asp:BoundField HeaderText="店铺编号" DataField="StoreID" />
                                    <asp:BoundField DataField="TotalMoney" HeaderText="金额" DataFormatString="{0:0.00}" />
                                    <asp:BoundField DataField="TotalPV" HeaderText="积分" DataFormatString="{0:0.00}" />
                                    <asp:BoundField DataField="TotalCommision" HeaderText="手续费" />
                                    <asp:TemplateField HeaderText="支付方式">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# GetOrderType(Eval("PayType")) %>'></asp:Label>
                                            <input id="HidPaytype" type="hidden" value='<%#DataBinder.Eval(Container,"DataItem.Paytype") %>' name="sg" runat="server" />
                                            <input id="StoreOrderID" type="hidden" value='<%#DataBinder.Eval(Container,"DataItem.OrderGoodsID") %>' name="sg" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="订货日期">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# GetOrderDate(Eval("OrderDateTime")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="订单详细信息">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbtnShow" runat="server" CommandArgument='<%# Eval("OrderGoodsID") %>'
                                                OnClick="lkbtnShow_Click"><%=GetTran("000440", "查看")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td>
                            <asp:Button ID="OKButtonID" runat="server" Text="订货付款" CssClass="anyes" OnClick="OKButtonID_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 100%">
                <div id="cssrain" style="width: 100%">
                    <table style="width: 99%;" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="80">
                                <table style="width: 100%" height="28" border="0" cellpadding="0" cellspacing="0"
                                    id="secTable">
                                    <tr>
                                        <td class="sec2" onclick="secBoard(0)">
                                            <span id="span1" title="" onmouseover="cut()">
                                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说  明"))%></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                        onclick="down2()" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2" style="width: 100%;display:none;">
                        <table style="width: 99%; height: 68px;" border="0" cellspacing="0" class="DMbk"
                            id="mainTable">
                            <tbody style="display: block">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    1、<%=GetTran("005792", "查询输入店铺的未支付的订单，协助店铺进行订单支付。")%><br>
                                                    2、<%=GetTran("005794", "付款选择选中的订单，是按照订单时间先后匹配当前可支付的订单。")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <%=msg %>
    </form>
</body>
</html>
