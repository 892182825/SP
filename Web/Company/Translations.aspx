<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Translations.aspx.cs" Inherits="Company_Translations" %>

<%@ Register TagPrefix="ucl" TagName="uclPager" Src="~/UserControl/PagerSorting.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=GetTran("006649", "多语言翻译")%></title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />    
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>  
     <script type ="text/javascript" language ="javascript" ></script>
     <script type="text/javascript" src="js/tianfeng.js"></script>




    <script type="text/javascript">     
            function FunEdit(keyCode,paras)
            {       
               var url="TranslationEdit.aspx?mode=1&keyCode="+keyCode;
               var flag=window.showModalDialog(url,"","dialogwidth=820px;dialogheight=350px;status=no;center:yes;scroll:no;" );		
               var backUrl="Translations.aspx?"+paras;        
	           window.location .href =backUrl;	
            }
            function FunAdd()
            {
               var url="TranslationsAdd.aspx?mode=0";
               var flag=window.showModalDialog(url,"","dialogwidth=820px;dialogheight=350px;status=no;center:yes;scroll:no;" );		
                window.location .href ="Translations.aspx?index=-1";  
            }
	      function cut()
            {
                 document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
            }
            function cut1()
            {
                 document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
            }
             window.onerror=function()
		        {
		            return true;
		        };
		    
          function secBoard(n)  
          {
               for(i=0;i<secTable.cells.length;i++)
              secTable.cells[i].className="sec1";
            secTable.cells[n].className="sec2";
            for(i=0;i<mainTable.tBodies.length;i++)
              mainTable.tBodies[i].style.display="none";
            mainTable.tBodies[n].style.display="block";
          }
   
    </script>
</head>
<body>
		<form id="Form1" method="post" runat="server">
		    <br />
			<table width="100%">
				<tr>
					<td>						
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
							<tr>
								<td >
								   <%=GetTran("006650", "显示语言")%>：<asp:DropDownList ID="ddlLanguageView1" runat="server"></asp:DropDownList>—
								   <asp:DropDownList ID="ddlLanguageView2" runat="server"></asp:DropDownList>								 
                                    &nbsp;&nbsp;<%=GetTran("006651", "词典键值")%>： 
                                    <asp:TextBox ID="txtQueryCode" runat="server"></asp:TextBox>
                                    &nbsp;<%=GetTran("006652", "查询关键字")%>：<asp:TextBox ID="txtKeywords" runat="server"></asp:TextBox>
                                    &nbsp;  &nbsp;&nbsp;&nbsp; <%=GetTran("006653", "查询语言")%>：<asp:DropDownList ID="ddlLanguage" runat="server">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnQuery" runat="server" CausesValidation="False" 
                                        CssClass="anyes" OnClick="btnQuery_Click" Text="查询" />
&nbsp; </td>
							</tr>
						</table>
						<br />
						<table width="100%" border="0" cellpadding="0" cellspacing="0" >							
							<tr>
								<td>
								        <asp:GridView ID="GridView1" runat="server" BackColor="White"  CssClass="tablemb" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
             OnRowDataBound="GridView1_RowDataBound" Width="100%">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
								<ucl:uclPager ID="Pager1" runat="server" />			
								</td>							
							</tr>
							<tr>
							<td>
							    <input id="Add" class="anyes" onclick="FunAdd();" type="button" title="添加翻译记录" value ='<%=GetTran("006038", "添加翻译记录")%>' /> <input id="Button1" onclick="javascript:window.location.href='LanguageManage.aspx';" 
                                        type="button" title="语言管理" value ='<%=GetTran("006021", "语言管理")%>' class="another" />
                                  
                                    </td>
							</tr>
										
						</table>
					</td>
				</tr>
			</table>
			
		  <div id="cssrain" style ="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80">
            <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" ID="secTable">
              <tr>
                  <td class="sec2" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                </tr>
            </table>
          </td>
          <td>
            <a href="#"><img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down3()"/></a>
          </td>
        </tr>
      </table>
	  <div id="divTab2" style ="display:none;">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">       
        <tbody style="DISPLAY:block">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><%=GetTran("006649", "多语言翻译")%><br /> 
                  1.<%=GetTran("006654", "根据一种语言翻译成其它语言。")%>
                    </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
		
		</form>
	</body>
</html>
