<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryOutStorage.aspx.cs"
    Inherits="Company_QueryOutStorage" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>出库查询</title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
    function CheckText()
	{
		//这个方法是只有1个lkSubmit按钮时候 可直接用
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
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" runat="server" id="vistable">
        <tr>
            <td style="white-space: nowrap;">
                
                <br />
                <table class="biaozzi">
                    <tr>
                        <td style="white-space: nowrap">
                            <asp:Button ID="Button1" runat="server" Style="display: none" Text="查 询" CssClass="anyes"
                                OnClick="Button1_Click"></asp:Button>
                            <asp:LinkButton ID="lkSubmit" Style="display: none" runat="server" Text='<%=GetTran("000321","提交") %>' OnClick="lkSubmit_Click"></asp:LinkButton>
                            <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value='<%=GetTran("000321","提交") %>'></input>
                        </td>
                        <td align="right" style="white-space: nowrap">
                            <%=GetTran("000308", "选择国家")%>：
                        </td>
                        <td style="white-space: nowrap">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="white-space: nowrap">
                            &nbsp;<%=GetTran("000386", "仓库")%>：
                        </td>
                        <td style="white-space: nowrap">  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlWareHouse" runat="server" OnSelectedIndexChanged="ddlWareHouse_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp; &nbsp;
                            <%=GetTran("000390", "库位")%>：
                            <asp:DropDownList ID="ddlDepotSeat" runat="server">
                            </asp:DropDownList> </ContentTemplate></asp:UpdatePanel>
                        </td>
                        <td align="right" style="white-space: nowrap">
                            &nbsp;<%=GetTran("000398", "起始时间")%>：
                        </td>
                        <td style="white-space: nowrap">
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                        </td>
                        <td align="right" style="white-space: nowrap">
                            &nbsp;<%=GetTran("000405", "终止时间")%>：
                        </td>
                        <td style="white-space: nowrap">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="border: rgb(147,226,244) solid 1px">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" CssClass="tablemb bordercss">
                                                <RowStyle HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                                <Columns>
                                                 <asp:TemplateField HeaderText="查看详细">
                                                        <ItemTemplate>
                                                            <img src="images/fdj.gif" /><asp:LinkButton ID="LinkBtnSearch" runat="server" CommandName="LinkbtnXinxi"><%=GetTran("000399", "查看详细")%></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="产品编码">
                                                        <ItemTemplate>
                                                           <%#GetProductNameCode((DataBinder.Eval(Container, "DataItem.ProductID")).ToString()) %>
                                                            <input id="hidPrID" type="hidden" value='<%# DataBinder.Eval(Container,"DataItem.ProductID")%>'
                                                                runat="server" name="Hidprid" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="产品名称">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProName" runat="server" Text='<%# GetProName(DataBinder.Eval(Container,"DataItem.ProductID")) %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Oquantity" HeaderText="出库产品总数" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="TotalOMoney" HeaderText="出库产品总金额" DataFormatString="{0:f2}"
                                                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="Tquantity" HeaderText="退货产品总数" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="TotalTuiMoney" HeaderText="退货产品总金额" DataFormatString="{0:f2}"
                                                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="TotalQuantity" HeaderText="合计" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="TotalMoney" HeaderText="合计总金额" DataFormatString="{0:f2}"
                                                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:TemplateField HeaderText="币种" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <%#GetcurrencyName(DataBinder.Eval(Container.DataItem,"ProductID").ToString())%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <th>
                                                                <%=GetTran("000399", "查看详细")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000263", "产品编码")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000501", "产品名称")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000748", "出库产品总数")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000751", "出库产品总金额")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000753", "退货产品总数")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000757", "退货产品总金额")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000630", "合计")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000762", "合计总金额")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000562", "币种")%>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
                                    <tr>
                                        <td style="border: rgb(147,226,244) solid 1px">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                                                OnRowDataBound="GridView2_RowDataBound" CssClass="tablemb bordercss">
                                                <RowStyle HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="产品编码" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <%#GetProductNameCode((DataBinder.Eval(Container, "DataItem.ProductID")).ToString()) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProductName" HeaderText="产品名称" ItemStyle-Wrap="false"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="ProductQuantity" HeaderText="数量" ItemStyle-Wrap="false"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="UnitName" HeaderText="单位" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="PreferentialPrice" HeaderText="单价" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="ProductTotal1" HeaderText="总金额" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                                    <asp:TemplateField HeaderText="币种" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <%# getCurrencyName((DataBinder.Eval(Container, "DataItem.ProductID")).ToString()) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DocAuditTime" HeaderText="日期" DataFormatString="{0:d}"
                                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="DocAuditer" HeaderText="审核人" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="BatchCode" HeaderText="批次" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="DocType" HeaderText="类型" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                                    <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                        <HeaderTemplate>
                                                            备注</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <span>
                                                                <%#getSubRemark(Eval("Note"))%></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <th>
                                                                <%=GetTran("000263", "产品编码")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000501", "产品名称")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000505", "数量")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000518", "单位")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000503", "单价")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000041", "总金额")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000562", "币种")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000613", "日期")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000655", "审核人")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000658", "批次")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000453", "类型")%>
                                                            </th>
                                                            <th>
                                                                <%=GetTran("000078", "备注")%>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <uc1:Pager ID="Pager1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Back" runat="server" CssClass="anyes" Text="返 回" OnClick="Back_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
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
                                    <td>
                                        <asp:Button ID="btn" runat="server" Text="导出Excel" OnClick="btn_Click" Style="display: none;" /><a
                                            href="#"><img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btn','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <td>
                                        １、<%=GetTran("000728", "查询公司指定仓库的某一时段的各产品的出库细明情况")%>。
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
