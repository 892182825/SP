<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="Company_OrderDetail" %>

<%@ Register src="../UserControl/Country.ascx" tagname="Country" tagprefix="uc1" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>OrderDetail</title>
		 <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" src="../javascript/Mymodify.js" type="text/javascript"></script>
		<script type="text/javascript">
		    //币种变时，钱也要变。（表格列数变化时一定要更改此方法）
            var first=true; 
            var from=0;
            function setHuiLv(th)
            {   
               
                if(first)
                {
                    from=AjaxClass.GetCurrency().value-0;
                    first=false;
                }
                
                var to=th.options[th.selectedIndex].value-0;
                
                
                var hl=AjaxClass.GetCurrency_Ajax(from,to).value;
                
                var trarr=document.getElementById("DataGrid1").getElementsByTagName("tr");
                for(var i=1;i<trarr.length;i++)
                {
                    trarr[i].getElementsByTagName("td")[6].innerHTML=
                    (parseFloat(trarr[i].getElementsByTagName("td")[6].firstChild.nodeValue.replace(/,/g,""))/hl).toFixed(2);
                }
                
                from=to;
            }
		</script>
		<script src="js/companyview.js" type="text/javascript"></script>
	</head>
	<body onload=" down2();">
	
		<form id="Form1" method="post" runat="server">
			
				<TABLE cellSpacing="0" cellPadding="0" border="0" style="width:100%" class="biaozzi">
				    <tr>
				        <td colspan="10"><br></td>
				    </tr>
					<TR >
					    <td valign="middle">
					    <asp:button id="Btn_Detail" runat="server"  Text="查询" 
                                CssClass="anyes" onclick="Btn_Detail_Click"></asp:button>&nbsp;&nbsp;
                                <%=GetTran("000735", "订单日期")%>：
					        &nbsp;&nbsp; <%=GetTran("000522", "起始")%>：
                            <asp:TextBox ID="DatePicker1" runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                            &nbsp;&nbsp; <%=GetTran("000567", "终止")%>：
                            <asp:TextBox ID="DatePicker2" runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                            &nbsp;&nbsp; <%=GetTran("000571", "店铺号")%>：<asp:textbox id="Tbx_num" runat="server" MaxLength="10" Width="104px"></asp:textbox>

                            &nbsp;&nbsp;<%=GetTran("000562")%>：
                                            <asp:dropdownlist id="Dropdownlist1" runat="server" Width="100px" 
                                    EnableViewState="False" onchange="setHuiLv(this)"></asp:dropdownlist>    
                           
                        </TD>
					</TR>
					 </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
					<tr>
					    <td  style="word-break:keep-all;word-wrap:normal;">
					        <asp:GridView id="DataGrid1" runat="server"    Width="100%"
								AutoGenerateColumns="False"  AllowSorting="True"
                                BorderStyle="Solid" onrowdatabound="DataGrid1_RowDataBound" >
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle  />
                                <RowStyle HorizontalAlign="Center" />
                                
								<Columns>
									<asp:TemplateField HeaderText="序号" Visible="false">
										<ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
										<HeaderStyle Wrap="false" />
										<ItemTemplate>
											<asp:Label id="lbl_num" runat="server">Label</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateField>
									<asp:BoundField DataField="storeid" SortExpression="storeid" HeaderText="店铺编号">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
						
						            <asp:TemplateField HeaderText="店长姓名">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="a45" runat="server" Text='<%# Encryption.Encryption.GetDecipherName(DataBinder.Eval(Container, "DataItem.name").ToString()) %>'>
											</asp:Label>
										</ItemTemplate>
										<HeaderStyle Wrap="false" />
									</asp:TemplateField>
									
                                        
									<asp:BoundField DataField="ExpectNum" HeaderText="期数">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									<asp:BoundField DataField="storeorderid" HeaderText="订单号">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									<asp:BoundField DataField="GeneOutBillPerson" HeaderText="出库单号">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									
									
									<asp:TemplateField HeaderText="订单日期">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="a4t5" runat="server" Text='<%# GetBiaoZhunTime(DataBinder.Eval(Container, "DataItem.orderdatetime").ToString()) %>'>
											</asp:Label>
										</ItemTemplate>
										<HeaderStyle Wrap="false" />
									</asp:TemplateField>
									
									<asp:BoundField DataField="totalmoney" HeaderText="金额" ItemStyle-CssClass="lab">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									<asp:BoundField DataField="totalpv" HeaderText="积分" ItemStyle-CssClass="lab1">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									<asp:TemplateField HeaderText="是否支付">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
										<ItemTemplate>
											<asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ischeckout").ToString()=="Y"?GetTran("000233","是") :GetTran("000235","否") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ischeckout") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="是否出库">
									<HeaderStyle Wrap="false" />
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isgeneoutbill").ToString()=="Y"?GetTran("000233","是"):GetTran("000235","否") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isgeneoutbill") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="是否发货">
									<HeaderStyle Wrap="false" />
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.issent").ToString()=="Y"?GetTran("000233","是"):GetTran("000235","否")  %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.issent") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="是否收货">
									<HeaderStyle Wrap="false" />
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isreceived").ToString()=="Y"?GetTran("000233","是"):GetTran("000235","否")   %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isreceived") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									
								</Columns>
								<EmptyDataTemplate>
								    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
								        <tr>
								            <th><%#GetTran("000012", "序号")%></th>
								            <th><%#GetTran("000150", "服务机构编号")%></th>
								            <th><%#GetTran("000039", "机构负责人姓名")%></th>
								            <th><%#GetTran("000045", "期数")%></th>
								            <th><%#GetTran("000079", "订单号")%></th>
								            <th><%#GetTran("002158", "出库单号")%></th>
								            <th><%#GetTran("000735", "订单日期")%></th>
								            <th><%#GetTran("000322", "金额")%></th>
								            <th><%#GetTran("000414", "积分")%></th>
								            <th><%#GetTran("002152", "是否支付")%></th>
								            <th><%#GetTran("002147", "是否出库")%></th>
								            <th><%#GetTran("000328", "是否发货")%></th>
								            <th><%#GetTran("001848", "是否收货")%></th>
								        </tr>
								    </table>
								</EmptyDataTemplate>
							</asp:GridView>
					    </td>
					</tr>
					
				</TABLE>
				 <table width="100%">
				 <div>
                    <span style="font-size:12px; margin-left:28px; float:left;">总计 </span> <span style="font-size:12px; float:right;"> 本页金额合计：<asp:Label ID="lab_bjehj" ForeColor="red" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;本页积分合计：<asp:Label ID="lab_bjfhj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;查询金额总计：<asp:Label ID="lab_cjezj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;查询积分总计：<asp:Label ID="lab_cjfzj" runat="server" ForeColor="Red"></asp:Label></span>
                 </div>
        <tr>
            <td align="right">
                <uc2:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
    </table>
				<div id="cssrain" style="width:100%">
      <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
              <tr>
                <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" ><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title=""><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
              </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2" style="display:none;">
      <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
      
        <tbody style="DISPLAY: block" id="tbody0">
          <tr>
            <td valign="middle" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                    <asp:LinkButton ID="OutToExcel" runat="server" Text="导出EXECL" OnClick="OutToExcel_Click"
                                            Style="display: none"></asp:LinkButton>
                                        <a href="#">
                                            <img src="images/anextable.gif" width="49" alt="" height="47" border="0" onclick="__doPostBack('OutToExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
                <tbody style="display: none;" id="tbody1">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <%=GetTran("006845", "1、查看店铺订单的一些详细信息。")%>
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
<script type="text/javascript" language="javascript">
    window.onload = function heji() {
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
        $('#lab_bjfhj').html(lab1 == 0 ? "0" : lab1);
    };

</script>