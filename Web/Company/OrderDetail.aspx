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
		    //���ֱ�ʱ��ǮҲҪ�䡣����������仯ʱһ��Ҫ���Ĵ˷�����
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
					    <asp:button id="Btn_Detail" runat="server"  Text="��ѯ" 
                                CssClass="anyes" onclick="Btn_Detail_Click"></asp:button>&nbsp;&nbsp;
                                <%=GetTran("000735", "��������")%>��
					        &nbsp;&nbsp; <%=GetTran("000522", "��ʼ")%>��
                            <asp:TextBox ID="DatePicker1" runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                            &nbsp;&nbsp; <%=GetTran("000567", "��ֹ")%>��
                            <asp:TextBox ID="DatePicker2" runat="server" onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                            &nbsp;&nbsp; <%=GetTran("000571", "���̺�")%>��<asp:textbox id="Tbx_num" runat="server" MaxLength="10" Width="104px"></asp:textbox>

                            &nbsp;&nbsp;<%=GetTran("000562")%>��
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
									<asp:TemplateField HeaderText="���" Visible="false">
										<ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
										<HeaderStyle Wrap="false" />
										<ItemTemplate>
											<asp:Label id="lbl_num" runat="server">Label</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateField>
									<asp:BoundField DataField="storeid" SortExpression="storeid" HeaderText="���̱��">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
						
						            <asp:TemplateField HeaderText="�곤����">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="a45" runat="server" Text='<%# Encryption.Encryption.GetDecipherName(DataBinder.Eval(Container, "DataItem.name").ToString()) %>'>
											</asp:Label>
										</ItemTemplate>
										<HeaderStyle Wrap="false" />
									</asp:TemplateField>
									
                                        
									<asp:BoundField DataField="ExpectNum" HeaderText="����">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									<asp:BoundField DataField="storeorderid" HeaderText="������">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									<asp:BoundField DataField="GeneOutBillPerson" HeaderText="���ⵥ��">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									
									
									<asp:TemplateField HeaderText="��������">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="a4t5" runat="server" Text='<%# GetBiaoZhunTime(DataBinder.Eval(Container, "DataItem.orderdatetime").ToString()) %>'>
											</asp:Label>
										</ItemTemplate>
										<HeaderStyle Wrap="false" />
									</asp:TemplateField>
									
									<asp:BoundField DataField="totalmoney" HeaderText="���" ItemStyle-CssClass="lab">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									<asp:BoundField DataField="totalpv" HeaderText="����" ItemStyle-CssClass="lab1">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<HeaderStyle Wrap="false" />
									</asp:BoundField>
									<asp:TemplateField HeaderText="�Ƿ�֧��">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderStyle Wrap="false" />
										<ItemTemplate>
											<asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ischeckout").ToString()=="Y"?GetTran("000233","��") :GetTran("000235","��") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ischeckout") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="�Ƿ����">
									<HeaderStyle Wrap="false" />
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isgeneoutbill").ToString()=="Y"?GetTran("000233","��"):GetTran("000235","��") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isgeneoutbill") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="�Ƿ񷢻�">
									<HeaderStyle Wrap="false" />
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.issent").ToString()=="Y"?GetTran("000233","��"):GetTran("000235","��")  %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.issent") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="�Ƿ��ջ�">
									<HeaderStyle Wrap="false" />
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.isreceived").ToString()=="Y"?GetTran("000233","��"):GetTran("000235","��")   %>'>
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
								            <th><%#GetTran("000012", "���")%></th>
								            <th><%#GetTran("000150", "����������")%></th>
								            <th><%#GetTran("000039", "��������������")%></th>
								            <th><%#GetTran("000045", "����")%></th>
								            <th><%#GetTran("000079", "������")%></th>
								            <th><%#GetTran("002158", "���ⵥ��")%></th>
								            <th><%#GetTran("000735", "��������")%></th>
								            <th><%#GetTran("000322", "���")%></th>
								            <th><%#GetTran("000414", "����")%></th>
								            <th><%#GetTran("002152", "�Ƿ�֧��")%></th>
								            <th><%#GetTran("002147", "�Ƿ����")%></th>
								            <th><%#GetTran("000328", "�Ƿ񷢻�")%></th>
								            <th><%#GetTran("001848", "�Ƿ��ջ�")%></th>
								        </tr>
								    </table>
								</EmptyDataTemplate>
							</asp:GridView>
					    </td>
					</tr>
					
				</TABLE>
				 <table width="100%">
				 <div>
                    <span style="font-size:12px; margin-left:28px; float:left;">�ܼ� </span> <span style="font-size:12px; float:right;"> ��ҳ���ϼƣ�<asp:Label ID="lab_bjehj" ForeColor="red" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;��ҳ���ֺϼƣ�<asp:Label ID="lab_bjfhj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;��ѯ����ܼƣ�<asp:Label ID="lab_cjezj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;��ѯ�����ܼƣ�<asp:Label ID="lab_cjfzj" runat="server" ForeColor="Red"></asp:Label></span>
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
                    <span id="span1" title="" ><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "�� ��"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title=""><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "˵ ��"))%></span>
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
                    <asp:LinkButton ID="OutToExcel" runat="server" Text="����EXECL" OnClick="OutToExcel_Click"
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
                                        <%=GetTran("006845", "1���鿴���̶�����һЩ��ϸ��Ϣ��")%>
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