<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReWareHouse.aspx.cs" Inherits="Company_ReWareHouse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/Country.ascx" TagName="Country" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看调拨</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script src="../JS/SqlCheck.js" type="text/javascript"></script>

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
    </script>

    <script language="javascript" type="text/javascript">
    function CheckText()
	{
		filterSql();
	}
	
	window.onload=function()
	{
	    down2();
	};
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="visdiv" runat="server"> 
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <br />
                    <table width="" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                                <asp:Button ID="btnE" runat="server" Text="添加调拨" CssClass="anyes" OnClick="btnE_Click" />
                                &nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="lkSubmit" runat="server" Style="display: none" Text="提交" OnClick="lkSubmit_Click"></asp:LinkButton>
                                <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value='<%=GetTran("000340","查询")%>'></input>
                                <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClientClick="return vali()"
                                    OnClick="BtnConfirm_Click" CssClass="anyes" Style="display: none"></asp:Button>
                            </td>
                            <td align="right">
                                &nbsp;<%=GetTran("000308", "选择国家")%>：
                            </td>
                            <td>
                                <uc2:Country ID="Country1" OnCSelectedIndexChanged="Country1_SelectedIndexChanged" runat="server" style="width: 89px" />
                            </td>
                            <td align="right">
                                &nbsp;<%=GetTran("000685", "入仓库")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlcangku" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcangku_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                &nbsp;<%=GetTran("000687", "入库位")%>：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlkuwei" runat="server">
                                    <asp:ListItem Value="-1">请选择</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table width="100%">
                        <tr>
                            <td style="border:rgb(147,226,244) solid 1px">
                                <asp:GridView ID="givDoc" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnRowCommand="givDoc_RowCommand" CssClass="tablemb bordercss" OnRowDataBound="givDoc_RowDataBound"
                                    AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                                    PagerStyle-Wrap="False" SelectedRowStyle-Wrap="False">
                                    <EmptyDataTemplate>
                                        <table class="tablebt" width="100%">
                                            <tr>
                                             <th>
                                                    <%=GetTran("000399", "查看详细")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000407", "单据编号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000702", "调入仓库")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000703", "调入库位")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000704", "调出仓库")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000705", "调出库位")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000708", "调拨时间")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000045", "期数")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000414", "积分")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000041", "总金额")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000078", "备注")%>
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <Columns>
                                     <asp:TemplateField HeaderText="查看详细" ShowHeader="False">
                                            <ItemTemplate>
                                                <img src="images/fdj.gif" /><asp:LinkButton ID="Button1" runat="server" CausesValidation="false" CommandName="Details"
                                                    CommandArgument='<%#Eval("DocID") %>' ><%=GetTran("000399", "查看详细")%></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单据编号" ShowHeader="False">
                                            <ItemTemplate>
                                                <span><%#Eval("DocID") %></span>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="调入仓库" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span>
                                                    <%#Eval("warehousename")%></span></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="调入库位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span>
                                                    <%#Eval("seatname") %></span></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="调出仓库" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span>
                                                    <%#getwarehouse(Eval("inwarehouseid"))%></span></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="调出库位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span>
                                                    <%#getDepot(Eval("indepotseatid"))%></span></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="调拨时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <span>
                                                    <%#Getdatetime(Eval("DocMakeTime")) %></span></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="期数" DataField="ExpectNum" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="center">
                                            <HeaderStyle Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="积分" DataField="TotalPV">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="总金额" DataField="TotalMoney" ItemStyle-Wrap="false">
                                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                            <HeaderStyle Wrap="false" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="查看备注">
                                            <ItemTemplate>
                                                <asp:Label ID="Lab_Description" runat="server" Text='<%#GetRemark(Eval("Note").ToString(),Eval("DocID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="center" />
                                            <HeaderStyle Wrap="false" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle Wrap="False"></PagerStyle>
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
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
                <td>&nbsp;
                </td>
            </tr>
        </table>
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
                                            <asp:LinkButton ID="btnDownExcel" runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                                Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                                    width="49" height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                   
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
                                            1、<%=GetTran("007158", "根据条件查询各个仓库的调拨明细情况")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2、<%=GetTran("007159", "可进行各仓库之间或同一仓库不同库位之间的产品调拨")%>。
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
         </div>
    </form>
</body>
</html>
