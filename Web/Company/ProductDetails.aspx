<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs" Inherits="Company_ProductDetails" EnableEventValidation="false" %>

<%@ Register TagPrefix="ucl" TagName="uclPagerSorting" Src="~/UserControl/Pager.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
        <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <%--<script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>--%>
    <script type="text/javascript" language="javascript">    
        
      window.onerror=function()
	    {
	        return true;
	    };
	    
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
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
//       for(i=0;i<secTable.cells.length;i++)
//      secTable.cells[i].className="sec1";
//    secTable.cells[n].className="sec2";
//    for(i=0;i<mainTable.tBodies.length;i++)
//      mainTable.tBodies[i].style.display="none";
//    mainTable.tBodies[n].style.display="block";
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
<body>
    <form id="form1" runat="server">
    <br />
     <div>        
        <table cellSpacing="0" cellPadding="0" width="100%" border="0" class="tablemb">			
			<tr>
				<td align="center">
					<asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" 
                        onrowdatabound="gvProduct_RowDataBound" Width="100%" AllowSorting="true" 
                        onsorting="gvProduct_Sorting" >
                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle CssClass="tablemb" Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />						
						<Columns>						    									
							<asp:BoundField DataField="TypeName" SortExpression="TypeName" HeaderText="产品类名" />	
							<%--<asp:BoundField DataField ="ProductID" SortExpression="ProductID" HeaderText="产品编号" Visible="false" />--%>														
							<asp:BoundField DataField="ProductCode" SortExpression="ProductCode" HeaderText="产品编码" />															
							<asp:BoundField DataField="ProductName" SortExpression="ProductName" HeaderText="产品名称" />															
							<asp:BoundField DataField="ProductSpecName" SortExpression="ProductSpecName" HeaderText="产品规格" />														
							<asp:BoundField DataField="ProductTypeName" SortExpression="ProductTypeName" HeaderText="产品型号" />															
							<asp:BoundField DataField="ProductUnitName" SortExpression="ProductUnitName" HeaderText="产品单位" />
							<asp:BoundField DataField ="Country" SortExpression="Country" HeaderText="产品所属国家" ItemStyle-HorizontalAlign="Left" />															
							<asp:BoundField DataField="ProductArea" SortExpression="ProductArea" HeaderText="产品产地" ItemStyle-HorizontalAlign="Left" />															
							<asp:TemplateField HeaderText="优惠单价" SortExpression="PreferentialPrice" ItemStyle-HorizontalAlign="Right">								
								<ItemTemplate>
									<asp:Label ID="Label1" runat="server" Text='<%# getstr(DataBinder.Eval(Container, "DataItem.PreferentialPrice").ToString()) %>'>
									</asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="优惠积分" SortExpression="PreferentialPrice" ItemStyle-HorizontalAlign="Right">								
								<ItemTemplate>
									<asp:Label ID="Label2" runat="server" Text='<%# getstr(DataBinder.Eval(Container, "DataItem.PreferentialPv").ToString()) %>'>
									</asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="普通单价" SortExpression="CommonPrice" ItemStyle-HorizontalAlign="Right">								
								<ItemTemplate>
									<asp:Label ID="Label3" runat="server" Text='<%# getstr(DataBinder.Eval(Container, "DataItem.CommonPrice").ToString()) %>'>
									</asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="普通积分" SortExpression="CommonPV" ItemStyle-HorizontalAlign="Right">							
								<ItemTemplate>
									<asp:Label ID="Label4" runat="server" Text='<%# getstr(DataBinder.Eval(Container, "DataItem.CommonPV").ToString()) %>'>
									</asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="AlertnessCount" SortExpression="AlertnessCount" HeaderText="预警数量" ItemStyle-HorizontalAlign="Right" />														
							<%--<asp:BoundField DataField="isCombineProduct" SortExpression="isCombineProduct" HeaderText="是否组合产品" />--%>
							<asp:TemplateField HeaderText="是否组合产品" SortExpression="isCombineProduct">
							    <ItemTemplate>
							        <asp:Label ID="lblIsCombineProduct" runat="server" Text='<%# JudgeIsCombineProduct(DataBinder.Eval(Container,"DataItem.isCombineProduct").ToString()) %>'></asp:Label>							        
							    </ItemTemplate>							   
							</asp:TemplateField>
																						
							<%--<asp:BoundField DataField="isSell" SortExpression="isSell" HeaderText="是否销售" />			--%>												
							<asp:TemplateField HeaderText="是否销售" SortExpression="isSell">
							    <ItemTemplate>
							       <asp:Label ID="lblIsSell" runat="server" Text='<%# JudgeIsSell(DataBinder.Eval(Container,"DataItem.isSell").ToString()) %>'></asp:Label>							       							        
							    </ItemTemplate>
							</asp:TemplateField>
						</Columns>
						<EmptyDataTemplate>
						    <table width="100%">
						        <tr>
						            <th><%=GetTran("001852", "产品类名")%></th>
						            <th><%=GetTran("000263", "产品编码")%></th>
						            <th><%=GetTran("000501", "产品名称")%></th>
						            <th><%=GetTran("000880", "产品规格")%></th>
						            <th><%=GetTran("000882", "产品型号")%></th>
						            <th><%=GetTran("001868", "产品单位")%></th>
						            <th><%=GetTran("001872", "产品所属国家")%></th>
						            <th><%=GetTran("001877", "产品产地")%></th>
						            <th><%=GetTran("001882", "优惠单价")%></th>
						            <th><%=GetTran("001883", "优惠积分")%></th>
						            <th><%=GetTran("001886", "普通单价")%></th>
						            <th><%=GetTran("001888", "普通积分")%></th>
						            <th><%=GetTran("000365", "预警数量")%></th>
						            <th><%=GetTran("001890", "是否组合产品")%></th>
						            <th><%=GetTran("001891", "是否销售")%></th>
						        </tr>
						    </table>
						</EmptyDataTemplate>
					</asp:GridView>
				</td>
			</tr>
			<tr>
				<td><asp:Label ID="lblMessage" runat="server">Label</asp:Label></td>
			</tr>			
		</table> 	
		<ucl:uclPagerSorting ID="Pager1" runat="server" />	
    </div>
        <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)" style="white-space: nowrap;">
                                <span id="span1" title="" onmouseover="cut()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)" style="white-space: nowrap;">
                                <span id="span2" title="" onmouseover="cut1()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#">
                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down2()" style="vertical-align: middle" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnExcel" Text="Excel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                                        <a href="#">
                                            <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcel','');" /></a>
                                            
                                            
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
                                        1、<%=GetTran("001902", "查看产品的一些详细信息")%>。
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
</html>
