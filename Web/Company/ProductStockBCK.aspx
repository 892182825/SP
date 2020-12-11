<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductStockBCK.aspx.cs" Inherits="Company_ProductStock" EnableEventValidation="false" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>
			<%=msg%>
		</title>
	 <link rel="Stylesheet" type="text/css" href="CSS/Company.css" />

    <script language="javascript" type="text/javascript" src="../javascript/Mymodify.js"></script>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function check(obj)
    {
        if (obj.value=="不限")
        {
        obj.value="";   
        }
    }
    function check2(obj)
    {
        if(obj.value=="")
         {
         obj.value="不限";
         }
    } 
    </script>

    <style type="text/css">
        td
        {
            word-wrap: normal;
            white-space: nowrap;
        }
    </style>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
    function CheckText()
	{
		filterSql();
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

    <script language="javascript" type="text/javascript">
function secBoard(n)
{
      var tdarr=document.getElementById("secTable").getElementsByTagName("td");
        
        for(var i=0;i<tdarr.length;i++)
        {
            tdarr[i].className="sec1";
        }
        tdarr[n].className="sec2";
        
        var tbody0=document.getElementById("tbody0");
        tbody0.style.display="none";
        var tbody1=document.getElementById("tbody1");
        tbody1.style.display="none";
        
        
        document.getElementById("tbody"+n).style.display="block";
  }
    </script>

    <script type="text/javascript">
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

    <script language="javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
        window.onload=function()
	    {
	        down2();
	    };

    </script>

		
	</head>
	<body >
		<form ID="Form1" method="post" runat="server">
		    <br />
		    <div>
		        <table width="100%" border="0" cellpadding="0"  cellspacing="0" class="biaozzi">
		            <tr>
						<td align="center" ><asp:label ID="lbl_title" runat="server"></asp:label><%=GetTran("001947", "各产品库存明细表")%></td>
					</tr>
					<tr>
						<td>&nbsp;&nbsp;<asp:label ID="lbl_flag" runat="server" ></asp:label>&nbsp;&nbsp;
							<asp:label ID="lbl_storename" runat="server" ></asp:label></td>
					</tr>		            
		        </table>		
				<table width="100%" style="overflow:scroll" border="0" cellpadding="0" cellspacing="0" class="tablemb">				    
					<tr>
						<td>						   
						    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" 
                                Width="100%" onrowdatabound="gvProduct_RowDataBound"  CssClass="biaozzi" >
						        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                <HeaderStyle CssClass="tablemb" Wrap="false" />
                                <RowStyle HorizontalAlign="Center"  Wrap="false" />								
								<Columns>
								    <asp:BoundField DataField="WareHouseName" SortExpression="WareHouseName" HeaderText="仓库名称" ItemStyle-Wrap="false" />									
								    <asp:BoundField DataField="SeatName" SortExpression="SeatName" HeaderText="库位名称" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="ProductCode" SortExpression="ProductCode" HeaderText="产品编码" ItemStyle-Wrap="false" />							
								    <asp:BoundField DataField="ProductName" SortExpression="ProductName" HeaderText="产品名称" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="ProductBigUnitName" SortExpression="ProductBigUnitName" HeaderText="产品大单位" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="ProductSmallUnitName" SortExpression="ProductSmallUnitName" HeaderText="产品小单位" ItemStyle-Wrap="false" /><%--
								    <asp:BoundField DataField="ProductColorName" SortExpression="ProductColorName" HeaderText="产品颜色" Visible="false" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="ProductSpecName" SortExpression="ProductSpecName" HeaderText="产品规格" Visible="false" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="ProductSexTypeName" SortExpression="ProductSexTypeName" HeaderText="产品适用人群"  Visible="false" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="ProductStatusName" SortExpression="ProductStatusName" HeaderText="产品状态" Visible="false" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="Weight" SortExpression="Weight" HeaderText="产品重量" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="Cubage" SortExpression="Cubage" HeaderText="产品体积" Visible="false"  ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="CostPrice" SortExpression="CostPrice" HeaderText="成本价" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="CommonPrice" SortExpression="CommonPrice" HeaderText="普通价" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="CommonPV" SortExpression="CommonPV" HeaderText="普通积分" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />								    
								    <asp:BoundField DataField="PreferentialPrice" SortExpression="PreferentialPrice" HeaderText="优惠价" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
								    <asp:BoundField DataField="PreferentialPV" SortExpression="PreferentialPV" HeaderText="优惠积分" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />--%>
								    <asp:BoundField DataField="TotalIn" SortExpression="TotalIn" HeaderText="入库数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />										
									<asp:BoundField DataField="TotalOut" SortExpression="TotalOut" HeaderText="出库数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />																			
									<asp:BoundField DataField="TotalEnd" SortExpression="TotalEnd" HeaderText="结库数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />																			
									<asp:BoundField DataField="AlertnessCount" SortExpression="AlertnessCount" HeaderText="预警数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />																												
								</Columns>
							</asp:GridView>
							 
						</td>
					</tr>	
					  <tr>
                <td>
                    <uc1:Pager ID="Pager1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                 <p>
                    <asp:Button ID="Button3" runat="server" Text="返 回"  CssClass="anyes" 
                        onclick="Button3_Click" />
                    </p>
                </td>
            </tr>		
		</table>	
		</div>
				
                    <div id="cssrain" style="width: 100% ;">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)">
                                <span id="span1" title="" onmouseover="cut()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)">
                                <span id="span2" title="" onmouseover="cut1()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="white-space: nowrap">
                    <a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" 
                            onclick="down2()" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <a href="#">
                                            <asp:ImageButton ID="Butt_Excel" runat="server" ImageUrl="images/anextable.gif" OnClick="Butt_Excel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
                <tbody style="display: none" id="tbody1">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
		</form>		
	</body>
</HTML>
