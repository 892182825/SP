<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRewareHouse.aspx.cs" Inherits="Company_AddRewareHouse" %>

<%@ Register Src="../UserControl/Country.ascx" TagName="Country" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>库存调拨</title>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <script language="javascript" src="../js/SqlCheck.js"></script>
     <script src="../JS/sryz.js"></script>
    <style type="text/css">
        td
        {
            white-space: nowrap;
            word-wrap: normal;
        }
        .tablehead th
        {
            background-image: url(images/tabledp.gif);
            height: 25px;
            font-size: 10pt;
            color: White;
        }
    </style>

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
	
	window.onerror=function()
	{
	    return true;
	};
		window.onload=function()
	{
	    down2();
	};
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
</script>
<script language="javascript" type="text/javascript">
    function CheckText()
	{
		filterSql();
	}
</script>
<script language="javascript" type="text/javascript">
    function numnberF(txtnumber)
    {
        if(isNaN(txtnumber.value))
        {
            alert('<%=GetTran("001145", "请输入数字！") %>');
            txtnumber.value=0;
            txtnumber.focus();
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br>
        <table align="center" style="width: 100%">
            <tr>
                <td>
                    <table class="biaozzi" >
                        <tr>
                        <td>
                        <asp:linkbutton id="lkSubmit" style="DISPLAY: none" Runat="server" Text="提交" onclick="lkSubmit_Click"></asp:linkbutton>
<input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value="<%=GetTran("000750", "确认调拨")%>"></input>
                         <asp:Button ID="btnProfit" runat="server" style="DISPLAY: none"  OnClick="btnProfit_Click" Text="确认调拨" CssClass="another" />
                            </td>
                            <td align="right">
                                &nbsp;<%=GetTran("000308", "选择国家")%>：
                            </td>
                            <td>
                                <%--<uc1:Country ID="Country1" runat="server" />--%><asp:DropDownList ID="DropCurrery"
                                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropCurrery_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                &nbsp;<%=GetTran("000514", "原始单据")%>：
                            </td>
                            <td>
                                <asp:TextBox ID="txtoldDocID" runat="server" Width="108px" MaxLength="50" onkeyup = "ValidateValue(this)"></asp:TextBox>
                            </td>
                            <td align="right">
                                &nbsp;<%=GetTran("000519", "经办人员")%>：
                            </td>
                            <td>
                                <asp:TextBox ID="txtOperator" runat="server" Width="108px" MaxLength="50" onkeyup = "ValidateValue(this)"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr></table>
                        <table class="biaozzi" >
                        <tr>
                            <td align="right">
                                <%=GetTran("000704", "调出仓库")%>：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddloutWareHouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddloutWareHouse_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <%=GetTran("000705", "调出库位")%>：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddloutDepotSeatID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddloutDepotSeatID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <%=GetTran("000702", "调入仓库")%>：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlinwareHouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlinwareHouse_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <%=GetTran("000703", "调入库位")%>：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlintDepotSeatID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" align="right">
                                <%=GetTran("000078", "备注")%>：
                            </td>
                            <td colspan="7">
                                <asp:TextBox ID="txtReamrk" MaxLength="500" runat="server" Height="60px" TextMode="MultiLine" Width="568px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td width="100%">
                                <table class="tablehead" width="98%" cellspacing="1" border="0" style="border-left: #88E0F4 solid 1px;
                                    border-top: #88E0F4 solid 1px; border-right: #88E0F4 solid 1px;">
                                    <tr>
                                        <th style="width: 10%">
                                            <%=GetTran("000558", "产品编号")%>
                                        </th>
                                        <th style="width: 10%">
                                            <%=GetTran("000501", "产品名称")%>
                                        </th>
                                        <th style="width: 25%">
                                           <%=GetTran("000560", "产品信息")%> 
                                        </th>
                                        <th style="width: 10%">
                                            <%=GetTran("000561", "产品库存")%>
                                        </th>
                                        <th style="width: 32%">
                                            <%=GetTran("001868", "单位")%>
                                        </th>
                                        <th style="width: 13%">
                                            <%=GetTran("000505", "数量")%>
                                        </th>
                                    </tr>
                                </table>
                                <div style="width: 100%; height: 290px; overflow-y: auto; overflow-x: hidden">
                                <table Width="100%">
                                    <tr>
                                        <td style="border:rgb(147,226,244) solid 1px" valign="top">
                                    <asp:GridView ID="givproduct" runat="server" AutoGenerateColumns="False" Width="100%"
                                         CssClass="tablemb bordercss" OnRowDataBound="givproduct_RowDataBound1" AlternatingRowStyle-Wrap="False"
                                        FooterStyle-Wrap="False" HeaderStyle-Wrap="False" PagerStyle-Wrap="False" RowStyle-Wrap="False"
                                        SelectedRowStyle-Wrap="False" ShowHeader="False">
                                        <PagerStyle Wrap="False"></PagerStyle>
                                        <%--<EmptyDataTemplate>
                                        <table Class="tablebt"  Width="100%">
                                            <tr>
                                                <th>
                                                    产品编号
                                                </th>
                                                <th>
                                                    产品名称
                                                </th>
                                                <th>
                                                    产品信息
                                                </th>
                                                <th>
                                                    产品库存
                                                </th>
                                                
                                                <th>
                                                    单位
                                                </th>
                                                <th>
                                                    数量
                                                </th>--%>
                                        <%--<th>
                                                    币种
                                                </th>
                                               
                                            </tr>    
                                            <tr style="">
                                                <td colspan="7">
                                                    没有数据
                                                </td>
                                            </tr>               
                                        </table>
                                    </EmptyDataTemplate>--%>
                                        <FooterStyle Wrap="False"></FooterStyle>
                                        <RowStyle Wrap="False"></RowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="产品编号">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductCode" runat="server" Text='<%#Eval("productCode") %>'></asp:Label>
                                                    <asp:Label ID="lblproductID" Visible="false" runat="server" Text='<%#Eval("ProductID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" HorizontalAlign="center" Width="10%" />
                                                <HeaderStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="产品名称">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                                    <asp:Label ID="lblhuoID" Visible="false" runat="server" Text='<%#Eval("ProductID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" HorizontalAlign="center" Width="10%" />
                                                <HeaderStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="产品信息">
                                                <ItemTemplate>
                                                    <font color="#3366FF"><%=GetTran("000503", "单价")%>：</font>
                                                    <asp:Label ID="LblPrice" runat="server" Text='<%#Eval("PreferentialPrice") %>'></asp:Label>
                                                    <font color="#FF9900"><%=GetTran("000414", "积分")%>：</font>
                                                    <asp:Label ID="LblPV" runat="server" Text='<%#Eval("PreferentialPV") %>' />
                                                    <input type="hidden" id="hidbigproductUnitID" value='<%#Eval("BigProductUnitID") %>'
                                                        name="hidbigproductUnitID" runat="server" />
                                                    <input type="hidden" id="HidBigUnitName" value='<%#Eval("BigProductUnitName") %>'
                                                        name="HidBigUnitName" runat="server" />
                                                    <input type="hidden" id="HidsmallProductUnitID" value='<%#Eval("SmallProductUnitID") %>'
                                                        name="HidsmallProductUnitID" runat="server" />
                                                    <input type="hidden" id="HidSmallUnitName" value='<%#Eval("SmallProductUnitName") %>'
                                                        name="HidSmallUnitName" runat="server" />
                                                    <input type="hidden" id="HidBigSmallMultiPle" value='<%#Eval("BigSmallMultiple") %>'
                                                        name="HidBigSmallMultiPle" runat="server" />
                                                    <input type="hidden" id="HidNeedNumber" value='<%#Eval("TotalIn") %>' name="HidNeedNumber"
                                                        runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" HorizontalAlign="left" Width="25%" />
                                                <HeaderStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="产品库存">
                                                <ItemTemplate>
                                                    <asp:Label ID="WHouseDate" runat="server" Text='<%#Eval("Qunum") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" HorizontalAlign="right" Width="10%" />
                                                <HeaderStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单位">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rdoBtnCate" RepeatDirection="Horizontal" runat="server">
                                                        <asp:ListItem Selected="True" Value="0" Text="大件"></asp:ListItem>
                                                        <asp:ListItem Text="小件" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" HorizontalAlign="left" Width="32%" />
                                                <HeaderStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="数量">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPdtNum" Width="60" Text="0" EnableViewState="false" runat="server"  MaxLength="10" onblur="numnberF(this)">0</asp:TextBox>
                                                    <asp:Label ID="lblmessage" Text="必选数量" Visible="false" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rf" runat="server" ErrorMessage="必选数量" InitialValue=""
                                                        ControlToValidate="txtPdtNum" Display="Static"><%=GetTran("001990", "*填写")%></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="false" HorizontalAlign="center" Width="13%" />
                                                <HeaderStyle Wrap="false" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="币种">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbizhong" Text='<%#GetCurrency(Eval("Country")) %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        </Columns>
                                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <AlternatingRowStyle Wrap="False" BackColor="#F1F4F8"></AlternatingRowStyle>
                                    </asp:GridView></td>
                                    </tr>
                                </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="biaozzi">
                                <br>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="cssrain" style="width: 100%">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="80">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)">
                                             <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                        </td>
                            <%--<td class="sec1" onclick="secBoard(1)">
                                
                            </td>--%>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <a href="#">
                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down2()" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        1、<%=GetTran("000791", "产品在不同仓库、库位之间调拨")%>。
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
                <tbody style="display: none">
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
</html>
