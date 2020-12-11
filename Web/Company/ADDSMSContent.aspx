<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADDSMSContent.aspx.cs" Inherits="Company_ADDSMSContent" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">   
    <base target="_self"></base>
    <title>短信预设管理</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/js.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/SqlCheck.js"></script>
   
   <script language=javascript type="text/javascript">
     function reloadopener()
		{
		    window.returnValue=1;
		    top.window.opener=null;
		    top.window.close();	    
		} 		
   </script>
</head>
<body onload="RTC()">
	<form ID="Form1" method="post" runat="server" > 
        <table width="100%" runat="server" id="tbAll">
		    <tr>
				<td>
				    <table ID="Table1" width="100%" class="biaozzi">
					    <tr>
							<td style="white-space:nowrap">
								    <asp:label ID="lblMessage" EnableViewState="False" ForeColor="Red"  Runat="server"></asp:label>
							</td>
						</tr>
					</table>				
					<asp:Panel ID="editPanel" Runat="server" >
						<table ID="Table3" width="100%" border="0px" cellpadding="0px" cellspacing="1px" class="tablemb">	
						      
																		
						 									
								<tr runat="server" id="tr1">
								    <td align="right" bgcolor="#EBF1F1"><%=GetTran("003224", "类别")%>：</td>									
								    <td colspan="2"><asp:TextBox ID="txtlb" Runat="server" MaxLength="50" 
                                            Width="180px" ></asp:TextBox><font color="red">*</font></td>
								</tr>
												
								<tr runat="server" id="tr2">
								    <td align="right" bgcolor="#EBF1F1"><%=GetTran("004193", "名称")%>：</td>									
								    <td colspan="2"><asp:TextBox ID="txtSMSName" Runat="server" MaxLength="50" 
                                            Width="180px" ></asp:TextBox><font color="red">*</font></td>
								</tr>
																
						        <tr runat="server" id="tr3">
								    <td align="right" bgcolor="#EBF1F1"><%=GetTran("007099", "短信内容")%>：</td>									
								    <td colspan="2"><asp:TextBox ID="txtBM" Runat="server" MaxLength="300" 
                                            Height="127px" TextMode="MultiLine" Width="283px" ></asp:TextBox><font color="red">*</font></td>
								</tr>
									                    																			
					  </table>
						<br />
						<table border="0px" cellpadding="0px" cellspacing="0px" width="100%">
						    <tr>
			                    <td align="center">
				                    <asp:button ID="doAddButtton" Text="保 存"  runat="server"  CssClass="anyes" 
                                        onclick="doAddButtton_Click"></asp:button>&nbsp;&nbsp;
				                    <input ID="Button1"  onclick="reloadopener()" type="button" value='<%=GetTran("004156","关 闭")%>' class="anyes" />
			                    </td>
					        </tr>			
						</table>
					</asp:Panel>								
				</td>				
			</tr>		    		
		</table>		
    </form>        
</body>
</html>
