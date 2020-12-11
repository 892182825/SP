<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoneyDetailReport.aspx.cs" Inherits="Company_MoneyDetailReport" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
        <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script><script type="text/javascript" src="js/tianfeng.js"></script>
    <script language="javascript" type="text/javascript">
	window.onerror=function()
    {
        return true;
    };
    window.onload=function()
	{
	    down2();
	    secBoardOnly(0);
	};
    </script>
     <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
		<STYLE>v\:* { BEHAVIOR: url(#default#VML) }
	o\:* { BEHAVIOR: url(#default#VML) }
	
		</STYLE>
		<script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
		<script>
		function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
										<TABLE cellSpacing="0" cellPadding="0" border="0" class="biaozzi">
											<TR>
											<TD>
											<asp:linkbutton id="lkSubmit1" style="DISPLAY: none" Runat="server" Text="修 改" onclick="lkSubmit1_Click"></asp:linkbutton>
                             <input class="anyes" id="Button2E" onclick="CheckText('lkSubmit1')" type="button" value='<%=GetTran("000048","查 询")%>'/>
                                       <asp:button style="DISPLAY: none" id="Btn_Detail" runat="server" Text="查询" 
                                            CssClass="anyes" onclick="Btn_Detail_Click"></asp:button>&nbsp;&nbsp;&nbsp;</TD>
											<td style="WIDTH: 64px"><%=GetTran("000570", "汇出日期")%>：</td>
												<TD>&nbsp;<%=GetTran("000522", "起始")%>&nbsp;&nbsp;</TD>
												<TD>
												<asp:TextBox ID="DatePicker1" CssClass="Wdate" Width="85px" runat="server" onfocus="new WdatePicker()" />
												</TD>
												<TD>&nbsp;&nbsp;<%=GetTran("000567", "终止")%>&nbsp;&nbsp;</TD>
												<TD>
												<asp:TextBox ID="DatePicker2" CssClass="Wdate" Width="85px" runat="server" onfocus="new WdatePicker()" />
												</TD>
												<TD>&nbsp;&nbsp;<%=GetTran("000150", "店铺编号")%>：&nbsp;&nbsp;
										<asp:textbox id="Tbx_num" runat="server" MaxLength="10" Width="104px"></asp:textbox></TD>
									
											</TR>
										</TABLE>
										<br />
										<table width="100%" border="0" cellpadding="0" cellspacing="0">
					<TR>
						<TD style="word-break:keep-all;word-wrap:normal"><asp:gridview id="DataGrid1" runat="server" Width="100%" 
                                AutoGenerateColumns="False" BorderStyle="Solid" onrowdatabound="DataGrid1_RowDataBound" CssClass="tablemb">
								<AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt" Wrap="false"/>
                                <RowStyle HorizontalAlign="Center" />
								<Columns>
									<asp:TemplateField HeaderText="序号">
										<ItemTemplate>
											<asp:Label id="lbl_num" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateField>
									<asp:BoundField DataField="remitnumber" SortExpression="remitnumber" HeaderText="店铺编号">
									</asp:BoundField>
									<asp:BoundField DataField="name" HeaderText="店长姓名">
									</asp:BoundField>
									<asp:BoundField DataField="PayExpectNum" HeaderText="期数">
									</asp:BoundField>
									<asp:TemplateField HeaderText="金额">
										<ItemTemplate>
											<asp:Label ID="Label1" CssClass="lab" runat="server" Text='<%# getstr(DataBinder.Eval(Container, "DataItem.RemitMoney").ToString()) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:BoundField DataField="StandardCurrency" HeaderText="币种">
									</asp:BoundField>
									<asp:BoundField DataField="Use" HeaderText="用途">
									</asp:BoundField>
									<asp:BoundField DataField="ReceivablesDate" HeaderText="收款日期" DataFormatString="{0:d}" ItemStyle-Wrap="false">
									</asp:BoundField>
									<asp:BoundField DataField="PayWay" HeaderText="支付方式">
									</asp:BoundField>
									<asp:BoundField DataField="Managers" HeaderText="经办人">
									</asp:BoundField>
									<asp:BoundField DataField="ConfirmType" HeaderText="确认方式" Visible="false">
									</asp:BoundField>
									<asp:BoundField DataField="RemittancesID" HeaderText="汇单号">
									</asp:BoundField>
									<asp:BoundField DataField="RemittancesDate" HeaderText="汇出日期" DataFormatString="{0:d}" ItemStyle-Wrap="false">
									</asp:BoundField>
									<asp:BoundField Visible="False" DataField="RemittancesBank" HeaderText="汇出银行">
									</asp:BoundField>
									<asp:BoundField Visible="False" DataField="ImportBank" HeaderText="汇入银行">
									</asp:BoundField>
									<asp:BoundField DataField="Sender" HeaderText="汇款人">
									</asp:BoundField>
									<asp:BoundField DataField="RemittancesMoney" ItemStyle-CssClass="lab1" HeaderText="汇出金额" DataFormatString="{0:f2}">
									</asp:BoundField>
									<asp:BoundField DataField="RemittancesCurrency" HeaderText="汇出币种">
									</asp:BoundField>
									<asp:BoundField DataField="isgsqr" HeaderText="是否审核">
									</asp:BoundField>
									<asp:BoundField DataField="Remark" HeaderText="备注"></asp:BoundField>
								</Columns>
								<EmptyDataTemplate>
                            <table class="tablemb" Width="100%" >
                                <tr>
                                    <th>
                                        <%=GetTran("000012", "序号")%>
                                    </th>
                                    <th>
                                         <%=GetTran("000150", "店铺编号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000039", "店长姓名")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000045", "期数")%>
                                    </th>
                                    
                                    <th>
                                        <%=GetTran("000322", "金额")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000562", "币种")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000588", "用途")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000591", "收款日期")%>
                                    </th>
                                    
                                    
                                    <th>
                                        <%=GetTran("000186", "支付方式")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000519", "经办人")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000595", "确认方式")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000597", "汇单号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000570", "汇出日期")%>
                                    </th>
                                    
                                    <th>
                                        <%=GetTran("000600", "汇出银行")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000601", "汇入银行")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000602", "汇款人")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000603", "汇出金额")%>
                                    </th>
                                    <th>
                                         <%=GetTran("000604", "汇出币种")%>
                                    </th>
                                    
                                    <th>
                                        <%=GetTran("000605", "是否审核")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000078", "备注")%>
                                    </th>
                                </tr>                
                            </table>
                        </EmptyDataTemplate>
							</asp:gridview></TD>
					</tr>
				</table>
				<div>
                <span style="font-size:12px; margin-left:28px; float:left;"><%=GetTran("000247", "总计")%>  </span> <span style="font-size:12px; float:right;"><%=GetTran("007549", "本页金额合计")%> ：<asp:Label ID="lab_bjehj" ForeColor="red" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<%=GetTran("007583", "本页汇出金额合计")%>：<asp:Label ID="lab_bhcjehj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;<%=GetTran("007552", "查询金额总计")%>：<asp:Label ID="lab_cjezj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;<%=GetTran("007584", "查询汇出金额总计")%>：<asp:Label ID="lab_chcjezj" runat="server" ForeColor="Red"></asp:Label></span>
             </div>
				          <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
          <tr>
            <td><uc1:Pager ID="Pager1" runat="server" /></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
</table>
<div id="cssrain" style="width:100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="80px">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTableOnly">
                        <tr>
                            <td class="secOnly" onclick="secBoardOnly(0)">
                                <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()" /></a></td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody1">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><%=GetTran("006865", "1、显示店铺汇款的一些详细信息。")%></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
			<asp:linkbutton id="prevbtn" style="DISPLAY: none" Text="上一页" Runat="server"></asp:linkbutton><asp:linkbutton id="fristbtn" style="DISPLAY: none" Text="首页" Runat="server"></asp:linkbutton><asp:linkbutton id="nextbtn" style="DISPLAY: none" Text="下一页" Runat="server"></asp:linkbutton><asp:linkbutton id="lastbtn" style="DISPLAY: none" Text="尾页" Runat="server"></asp:linkbutton><asp:linkbutton id="Gobtn" style="DISPLAY: none" Text="定位" Runat="server"></asp:linkbutton>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
    function heji() {
        var lab = 0;
        var lab1 = 0;
        $('.lab').each(
        function() {
            lab = parseFloat($(this).text().replace(',', '')) + lab;
        }

    );
        $('#lab_bjehj').html(lab == 0 ? "0" : lab);
        $('.lab1').each(
        function() {
            lab1 = parseFloat($(this).text().replace(',', '')) + lab1;
        }
    );
        $('#lab_bhcjehj').html(lab1 == 0 ? "0" : lab1);
    };

    window.onload = heji();
</script>