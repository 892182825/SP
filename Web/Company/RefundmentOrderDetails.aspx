<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefundmentOrderDetails.aspx.cs"
    Inherits="Company_RefundmentOrderDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>退货单详情</title>
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

    <script language="javascript">
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
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
       <br />
        <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
            <td >
                <asp:GridView ID="gvRefundmentBrowse" runat="server" AutoGenerateColumns="False"
                                Width="100%" CssClass="tablemb bordercss"
                                 OnRowDataBound="gvRefundmentBrowse_RowDataBound1">
                                 <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    
                                    <asp:BoundField DataField="client" SortExpression="client" HeaderText="退货店铺" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="docID" SortExpression="docID" HeaderText="退货单号" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpectNum" SortExpression="ExpectNum" HeaderText="期数"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="是否审核" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "StateFlag").ToString().Trim() == "1" ? GetTran("000233", "是") : GetTran("000235", "否")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否失效" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "CloseFlag").ToString().Trim() == "1" ? GetTran("000233", "是") : GetTran("000235", "否")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle Wrap="False"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="退货总价">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" name="lblTotalMoney" Text='<%# Eval("totalMoney") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="totalPV" HeaderText="退货总积分" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="退货日期" SortExpression="dat_docMakeTime">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("docMakeTime")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                  
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellspacing="0" cellpadding="0" rules="all" border="1" style="width: 100%;
                                        border-collapse: collapse;">
                                        <tr>
                                          
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001808", "退货店铺")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001809", "退货单号")%>
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
                                                <%=GetTran("001812", "退货总价")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001813", "退货总积分")%>
                                            </th>
                                            <th style="white-space: nowrap">
                                                <%=GetTran("001814", "退货日期")%>
                                            </th>
                                           
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
            </td>
        </tr>
       
       
    </table>
    <br />
        <table  style="width:100%">
            <tr>
                <td>
                    <asp:GridView ID="gvRefundmentDetail" runat="server" AutoGenerateColumns="false"
                        Width="100%" border="0" CellPadding="0" CellSpacing="0" class="tablemb"  >
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="docID" HeaderText="退货编号" />
                            <asp:TemplateField HeaderText="产品名称">
                                <ItemTemplate>
                                    <%#Eval("productName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="productQuantity" HeaderText="退货数量" />
                            <asp:BoundField DataField="ExpectNum" HeaderText="期数" />
                            <asp:BoundField DataField="unitprice" HeaderText="价格" />
                            <asp:BoundField DataField="pv" HeaderText="积分" />
                        </Columns>
          
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left">
               <br />
                    <asp:Button ID="btnhistory" runat="server" Text="返 回" CssClass="anyes" 
                        onclick="btnhistory_Click" /> 
                </td>
            </tr>
        </table>
    </div>
    
   <div id="cssrain">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
              <tr>
                <td class="sec2" onclick="secBoard(0)">
                                             <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                        </td>    
              </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
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
                  <td>
                  </td>
                  <br />
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>

  <%--  <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="导出Excel" />--%>
    </form>
</body>
</html>
