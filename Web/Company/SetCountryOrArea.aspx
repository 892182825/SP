<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetCountryOrArea.aspx.cs" Inherits="Company_SetCountryOrArea" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
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
<head runat="server">
    <title>SetCountryOrArea</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
</head>
    <script type="text/javascript">
	function fanyi()
	{
        return confirm('<%=GetTran("000248","确定要修改吗？") %>');
	}
	
	
	function setTitle()
	{
	    var arr=document.getElementById("DropDownList1").options;
	    
	    for(var i=0;i<arr.length;i++)
	    {
	        arr[i].title=arr[i].text;
	    }
	}
    </script>
<body onload="setTitle()">
    <form id="form1" runat="server">
    <br />        
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">														
		    <tr>
			    <td style="white-space:nowrap">
			        <%=GetTran("002104", "国家或地区名称")%>：
			        <asp:DropDownList ID="DropDownList1" runat="server" style="width:130px">
                    </asp:DropDownList>&nbsp;&nbsp;
&nbsp;<%=GetTran("002106", "国家编码")%>：<input type="text" id="txtCountryCode" maxlength="10" runat="server" onkeyup="value=value.replace(/[^\d]/g,'')" />&nbsp;&nbsp;
				    
		            <%=GetTran("002041", "货币")%>：	
				    <asp:dropdownlist ID="DropNewCurrency" runat="server" >
					    <asp:ListItem Value="-1">请选择货币</asp:ListItem>
				    </asp:dropdownlist>&nbsp;&nbsp;	
		            <asp:Button ID="btnAddNewCountry" runat="server" Text="添 加" 
                            onclick="btnAddNewCountry_Click" CssClass="anyes" />    				
			    </td>
		    </tr>
		    <tr><td>&nbsp;</td></tr>														
		    <tr>
			    <td>
			        <asp:GridView ID="gvCountry" runat="server" 
					     AllowSorting="True" AutoGenerateColumns="False" onrowdatabound="gvCountry_RowDataBound" DataKeyNames="ID,Name" 
                        onrowcommand="gvCountry_RowCommand" Width="100%" 
                        onsorting="gvCountry_Sorting" CssClass="tablemb" >																	
				        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle  Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />															    
    			    
					    <Columns>																	
						    <asp:TemplateField HeaderText="删除">
							    <ItemTemplate>																				
									    <asp:LinkButton ID="lbtnDelCountry" runat="server" CommandName="DelCountry" 
                                            OnClientClick="return fanyi()"><%=GetTran("000022", "删除")%></asp:LinkButton>																																					
						        </ItemTemplate>
						    </asp:TemplateField>
					        <asp:BoundField DataField="ID" SortExpression="ID" ReadOnly="true" HeaderText="国家编号" Visible="false" />
					        <asp:BoundField DataField="CountryCode" HeaderText="国家编码" />
					        <asp:BoundField DataField="CountryForShort" HeaderText="国家简称" />
					        <asp:BoundField DataField="Name" SortExpression="[name]" ReadOnly="True" HeaderText="国家"/>																							        
						    <asp:TemplateField SortExpression="Ratename" HeaderText="使用币种">																			
							    <ItemTemplate>
								    <asp:Label ID="Grid_lab_Currency" runat="server">
									    <%# DataBinder.Eval(Container, "DataItem.RateName")%>
								    </asp:Label>
							    </ItemTemplate>															
							    <EditItemTemplate>
								    <asp:DropDownList ID="Grid_drop_Currency" runat="server" ></asp:DropDownList>													
							    </EditItemTemplate>
						    </asp:TemplateField> 				
					    </Columns>
				    </asp:GridView>
			    </td>
		    </tr>
	    </table>
	    <table width="100%">							
							<tr>
							    <td>
							        <div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="150"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
              <tr>
                <td class="sec2" onclick="secBoard(0)">
                     <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
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
                  <td>
                  <asp:Button ID="btnExcelCountry" runat="server" Text="Excel" onclick="btnExcelCountry_Click" Style="display: none" />			  
                  <a href="#" id="imgCountry" runat="server"><img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcelCountry','');" /></a>
                  &nbsp;&nbsp;&nbsp;&nbsp;                  
                  </td>
                </tr>
            </table> </td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><%=GetTran("000224", "操作说明")%>：<br /> 
                  1、<%=GetTran("002137", "添加国家")%> 
                    </td>
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
    </div>
    </form>
</body>
</html>
