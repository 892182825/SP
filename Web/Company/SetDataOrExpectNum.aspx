<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetDataOrExpectNum.aspx.cs" Inherits="SetDataOrExpectNum"  EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>期数/日期设置</title>
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/SqlCheck.js"></script>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
        function cutManagement()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cutDescription()
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
	<form ID="Form1" method="post" runat="server" onsubmit="return filterSql_III()">
	    <br />
		<table width="100%" >
			<tr>
				<td>
					<table width="100%" >
						<tr>
							<td>
								<table border="0" cellpadding="0" cellspacing="0" class="biaozzi">
									<tr>
										<td ><%=GetTran("001522", "系统显示为")%>：</td>
										<td >
											<asp:radiobuttonlist ID="RadioQishuDate" runat="server" RepeatDirection="Horizontal">
												<asp:ListItem Value="0">期数</asp:ListItem>
												<asp:ListItem Value="1">时间</asp:ListItem>
												<asp:ListItem Value="2">期数加时间</asp:ListItem>
											</asp:radiobuttonlist>
										</td>
										<td>
											&nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Text="保 存" onclick="btnSave_Click" CssClass="anyes" />
										</td>
									</tr>
								</table>									
							</td>
						</tr>
					</table>
				
					<table width="100%" border="0" cellpadding="0" cellspacing="0">									
						<tr>
							<td >
							    <asp:GridView ID="gvExpectNumDate" runat="server" AutoGenerateColumns="False"  DataKeyNames="ExpectNum"
                                    AllowSorting="True"  CssClass="tablemb"                                                  
                                    onrowcancelingedit="gvExpectNumDate_RowCancelingEdit"                                                    
                                    onrowediting="gvExpectNumDate_RowEditing"                                                    
                                    onrowdatabound="gvExpectNumDate_RowDataBound" 
                                    onrowupdating="gvExpectNumDate_RowUpdating" Width="100%" 
                                    onsorting="gvExpectNumDate_Sorting" >																								
									<AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                    <HeaderStyle  Wrap="false" />
                                    <RowStyle HorizontalAlign="Center"  Wrap="false" />
									<Columns>
									    <asp:BoundField DataField="ExpectNum" SortExpression="ExpectNum" ReadOnly="true"  HeaderText="期数"/>																												
										<asp:TemplateField SortExpression="Date" HeaderText="时间">
										    <ItemTemplate>
										        <asp:Label ID="Riqi" runat="server">
													<%#Eval("Date")%>
												</asp:Label>														        
										    </ItemTemplate>
										    <EditItemTemplate>														        
										        <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("Date")%>'></asp:TextBox>														        
										    </EditItemTemplate>											
                                        </asp:TemplateField>  
                                        <asp:TemplateField SortExpression="Date" HeaderText="开始时间">
										    <ItemTemplate>
										        <asp:Label ID="starRiqi" runat="server">
													<%#Eval("stardate")%>
												</asp:Label>														        
										    </ItemTemplate>
										    <EditItemTemplate>														        
										        <asp:TextBox ID="txtStar" runat="server" Text='<%#Eval("stardate")%>'></asp:TextBox>														        
										    </EditItemTemplate>											
                                        </asp:TemplateField>     
                                        <asp:TemplateField SortExpression="Date" HeaderText="结束时间">
										    <ItemTemplate>
										        <asp:Label ID="endRiqi" runat="server">
													<%#Eval("enddate")%>
												</asp:Label>														        
										    </ItemTemplate>
										    <EditItemTemplate>														        
										        <asp:TextBox ID="txtEnd" runat="server" Text='<%#Eval("enddate")%>'></asp:TextBox>														        
										    </EditItemTemplate>											
                                        </asp:TemplateField>                                           
                                        <asp:CommandField ButtonType="Link" CancelText="取消"  EditText="修改时间"  UpdateText="更新" HeaderText="编辑" ShowEditButton="True" />																								

									 </Columns>
								</asp:GridView>
							</td>
						</tr>
					</table>
									
				</td>
			</tr>
			<tr>
			    <td>
			        <div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="150px"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
            <tr>
                <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" onmouseover="cutManagement()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cutDescription()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
            </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down3()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block" id="tbody0">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><asp:Button ID="btnExcel" Text="Excel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                  <a href="#"><img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcel','');"/></a>&nbsp;&nbsp;&nbsp;&nbsp; 
                  </td>
                </tr>
            </table> </td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><%=GetTran("001533", "设置期数与日期的对应关系")%></td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
			    </td>
			</tr>
		</table>
		<%=msgstr%>
	</form>
</body>
</html>
