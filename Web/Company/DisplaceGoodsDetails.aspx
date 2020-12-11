<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplaceGoodsDetails.aspx.cs"
    Inherits="Company_DisplaceGoodsDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>退货单祥细</title>
       <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;
        };
    </script>
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

<script type="text/javascript">
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

<script type="text/javascript">
<!--
//分别是奇数行默认颜色,偶数行颜色,鼠标放上时奇偶行颜色
    var aBgColor = ["#F1F4F8","#FFFFFF","#FFFFCC","#FFFFCC"];
    //从前面iHead行开始变色，直到倒数iEnd行结束

    function addTableListener(o,iHead,iEnd)
    {
        o.style.cursor = "normal";
        iHead = iHead > o.rows.length?0:iHead;
        iEnd = iEnd > o.rows.length?0:iEnd;
        for (var i=iHead;i<o.rows.length-iEnd ;i++ )
        {
            o.rows[i].onmouseover = function(){setTrBgColor(this,true)}
            o.rows[i].onmouseout = function(){setTrBgColor(this,false)}
        }
    }
    function setTrBgColor(oTr,b)
    {
        oTr.rowIndex % 2 != 0 ? oTr.style.backgroundColor = b ? aBgColor[3] : aBgColor[1] : oTr.style.backgroundColor = b ? aBgColor[2] : aBgColor[0];
    }
    window.onload = function(){addTableListener(document.getElementById('<%=Session["GridViewID"] %>'),0,0);}

function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
</script>

<SCRIPT language="javascript">
     function secBoard(n)
  
  {
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
</SCRIPT>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
            <td >
                <asp:GridView ID="gvDisplaceGoods" runat="server" AutoGenerateColumns="False" DataKeyNames="DisplaceOrderID"
                                OnRowDataBound="gvDisplaceGoods_RowDataBound"
                                CssClass="tablemb bordercss" Width="100%" EmptyDataText="暂无数据!">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                   
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="StoreID"
                                        HeaderText="换货店铺" SortExpression="StoreID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="DisplaceOrderID"
                                        HeaderText="换货单号" SortExpression="DisplaceOrderID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="RefundmentOrderID"
                                        HeaderText="对应退单号" SortExpression="RefundmentOrderID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="StoreOrderID"
                                        HeaderText="对应订单号" SortExpression="StoreOrderID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="OutStorageOrderID"
                                        HeaderText="对应出库单号" SortExpression="OutStrageOrderID">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="ExpectNum"
                                        HeaderText="期数" SortExpression="ExpectNum">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="是否审核" SortExpression="StateFlag">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" 
                                                Text='<%# GetYesOrNo(Eval("StateFlag")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StateFlag") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否失效" SortExpression="CloseFlag">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" 
                                                Text='<%# GetYesOrNo(Eval("CloseFlag")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CloseFlag") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="退货额">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" name="lblTotalMoney" Text='<%# Eval("OutTotalMoney", "{0:n2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="进货额">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" name="lblTotalMoney" Text='<%# Eval("InTotalMoney", "{0:n2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="MakeDocDate"
                                        HeaderText="换货单日期" SortExpression="MakeDocDate" DataFormatString="{0:d}">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                   
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellspacing="0" cellpadding="0" rules="all" border="1" style="width: 100%;
                                        border-collapse: collapse;">
                                        <tr>
                                           
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001863", "换货店铺")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001864", "换货单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001866", "对应退单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001867", "对应订单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000099", "对应出库单号")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("000605", "是否审核")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001811", "是否失效")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001875", "退货额")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001876", "进货额")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001878", "换货单日期")%>
                                            </th>
                                            
                                        </tr>
                                </EmptyDataTemplate>
                            </asp:GridView>
            </td>
        </tr>
       
       
    </table>
    <br />
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    <asp:GridView ID="gvReplacementDetail" runat="server" AutoGenerateColumns="false" width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD" class="tablemb">
                      <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="displaceorderid" HeaderText="换货单编号" />
                            <asp:TemplateField HeaderText="产品名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                   <%#Eval("productName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="OutQuantity" HeaderText="退货数量" />
                            <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataField="inquantity" HeaderText="进货数量" />
                            <asp:BoundField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" DataField="price" HeaderText="价格" />
                            <asp:TemplateField HeaderText="币种" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                  <%#Eval("Currency") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="pv" HeaderText="积分" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" ItemStyle-Wrap="false" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        <tr>
                <td align="left"><br />
                    <asp:Button ID="btnE" runat="server" Text="返 回" CssClass="anyes" 
                        onclick="btnE_Click" />
                </td>
            </tr>
        </table>

   <div id="cssrain">
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
        <tbody style="DISPLAY: block">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><a href="#">
                  <asp:ImageButton ID="bunExportExcel" runat="server"  
                        onclick="bunExportExcel_Click" ImageUrl="images/anextable.gif"/>
                  <%-- <img src="images/anextable.gif" width="49" height="47" border="0" /></a>
                  &nbsp;&nbsp;&nbsp;&nbsp;<a href="#"><img src="images/anprtable.gif" width="49" height="47" border="0" /></a> --%></td>
                </tr>
            </table></td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><%=GetTran("005833", "1、无")%>
                  </td>
                  <br />
                   
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
    
    </div>
  <%--  <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="导出Excel" />--%>
    </form>
</body>
</html>
