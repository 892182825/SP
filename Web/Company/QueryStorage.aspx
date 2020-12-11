<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryStorage.aspx.cs" Inherits="Company_QueryStorage" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register src="../UserControl/PagerTwo.ascx" tagname="PagerTwo" tagprefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
        <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function aaa()
    {
   // var str="'";
   // var str1="=";
        for(var i=0;i<form1.elements.length;i++)
        {
            if(form1.elements[i].type=="text")
            {
                if(form1.elements[i].value.indexOf("'")!=-1||form1.elements[i].value.indexOf("=")!=-1)
                {
                    alert('查询条件里面不能输入特殊字符！');
                    return false;
                }
            }
        }
    }
    </script>
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
	        window.onload=function()
	    {
	        down2();
	    };
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
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" runat="server" id="vistable">
        <tr>
            <td style="white-space: nowrap">
               
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td style="width:5%"><asp:Button ID="btn_search" runat="server" Text="查 询" Style="display: none" OnClick="btn_search_Click"
                                CssClass="anyes" />&nbsp;
                            <asp:LinkButton ID="lkSubmit" runat="server" Text="查 询" OnClick="lkSubmit_Click"
                                Style="display: none"></asp:LinkButton>
                            <input class="anyes" id="bSubmit" onClick="CheckText()" type="button" value='<%=GetTran("000048", "查 询")%>'></input></td>
                        <td align="left" style="white-space: nowrap">
                            
                            <asp:UpdatePanel ID="UpdatePanel1" 
                                runat="server"><ContentTemplate>
                            &nbsp;<%=GetTran("000308", "选择国家")%>：<asp:DropDownList ID="DropDownList1" runat="server"
                                Width="68px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;<asp:Label ID="lbl_WareHouse" runat="server"><%=GetTran("000386", "仓库")%>：</asp:Label>
                            &nbsp;<asp:DropDownList ID="ddlWareHouse" runat="server" OnSelectedIndexChanged="ddlWareHouse_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp; <%=GetTran("000390", "库位")%>：
                            
&nbsp;<asp:DropDownList ID="ddlDepotSeat" runat="server">
                            </asp:DropDownList>
                            &nbsp; <%=GetTran("000828", "产品编码条件")%>：<asp:DropDownList ID="DropDownListBM" runat="server">
                                <asp:ListItem Value="全部" Selected="true">全部</asp:ListItem>
                                <asp:ListItem Value="产品编码">产品编码</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtProductCode" runat="server" Width="121px" MaxLength="50"></asp:TextBox></ContentTemplate></asp:UpdatePanel>
                        </td>
                        <!-- <td>
								
						   </td>
						   <td>
						   </td>
						   <td>
						        
						   </td>
						   <td>
							    
						   </td>
						   <td align="right" class="style4"></td>
						   <td style="WIDTH: 91px">
								
						   </td>
						   <td>
						        
						   </td>-->
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="border:rgb(147,226,244) solid 1px">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                Width="100%" CssClass="tablemb bordercss">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField DataField="WareHouseID" HeaderText="仓库编号" ItemStyle-Wrap="false"
                                        Visible="false" />
                                    <asp:TemplateField HeaderText="仓库名称" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# GetWarehouseName(DataBinder.Eval(Container.DataItem, "WareHouseID").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="库位编号" DataField="DepotSeatID" ItemStyle-Wrap="false"
                                        Visible="false" />
                                    <asp:TemplateField HeaderText="库位名称" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# GetDepotSeatName(DataBinder.Eval(Container.DataItem, "DepotSeatID").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="产品编码" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# GetProductNameCode((DataBinder.Eval(Container, "DataItem.productID")).ToString()) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProductName" HeaderText="产品名称" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="ProductSpecName" HeaderText="产品规格" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="ProductTypeName" HeaderText="产品型号" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="UnitName" HeaderText="单位" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="preferentialprice" HeaderText="会员价格" ItemStyle-Wrap="false"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:TemplateField HeaderText="币种" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# getCurrencyName(DataBinder.Eval(Container.DataItem,"ProductID").ToString() ) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TotalIn" HeaderText="入库数量" DataFormatString="{0:f0}" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="TotalOut" HeaderText="出库数量" DataFormatString="{0:f0}"
                                        ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="leftKucun" SortExpression="leftKucun" HeaderText="余结库存"
                                        DataFormatString="{0:f0}" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="AlertnessCount" HeaderText="预警数量" DataFormatString="{0:f0}"
                                        ItemStyle-Wrap="false" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000253", "仓库编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000355", "仓库名称")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000263", "产品编码")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000357", "产品名称")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000880", "产品规格")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000882", "产品型号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000518", "单位")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000885", "会员价格")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000562", "币种")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000359", "入库数量")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000362", "出库数量")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000888", "余结库存")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000365", "预警数量")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right" style="white-space: nowrap">
                            <uc1:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lkbtnesellog" class='midhong' OnClick="LinkButton1_Click" Visible="true">
                            <img runat="server" id="i1" alt="" src="images/dis2.GIF" align="absmiddle" style="border: 0px"><%=GetTran("001818", "查看可用库存")%></asp:linkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" id="p2" visible="false">
                    <table width="100%">
                        <tr>
                            <td style="border:rgb(147,226,244) solid 1px">
                                <asp:GridView ID="gvElogin" runat="server" AutoGenerateColumns="False" Width="100%"
                                     CssClass="tablemb bordercss" OnRowDataBound="gvElogin_RowDataBound">
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="tablebt" />
                                    <Columns>
                                        <asp:BoundField HeaderText="产品名称" DataField="productname" />
                                        <asp:BoundField HeaderText="总入库" DataField="totalin" />
                                        <asp:BoundField HeaderText="总出库" DataField="totalout" />
                                        <asp:BoundField HeaderText="可用库存" DataField="total" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000501", "产品名称")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001479", "总入库")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001482", "总出库")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001484", "可用库存")%>
                                            </th>
                                        </tr>
                                    </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc2:PagerTwo ID="Pager11" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <table>
        <tr>
            <td valign="top" style="white-space: nowrap">
                
            </td>
        </tr>
    </table>
    
    <div id="cssrain" style="width: 100%">
                    <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="150">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                        <td class="sec2" onClick="secBoard(0)"  style="white-space:nowrap;">
                                             <span id="span1" title="" onMouseOver="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                        </td>
                                        <td class="sec1" onClick="secBoard(1)"  style="white-space:nowrap;">
                                            <span id="span2" title="" onMouseOver="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
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
                        <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                            <tbody style="display: block" id="tbody0">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="white-space: nowrap">
                                                    <asp:Button ID="btnDownExcel" runat="server" Text="导出Excel" OnClick="btn_Click" Style="display: none;" /><a
                                                        href="#"><img src="images/anextable.gif" width="49" height="47" border="0" onClick="__doPostBack('btnDownExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <!--<a href="#">
                                                        <img src="images/anprtable.gif" width="49" height="47" border="0" /></a>-->
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
                                                <td style="size:12px;color:333333;">
                                                    1、<%=GetTran("001148", "查询各产品在公司仓库中的库存情况")%>。
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
