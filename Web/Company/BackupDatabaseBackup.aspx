<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackupDatabaseBackup.aspx.cs" Inherits="Company_BackupDatabase" %>

<%@ Register TagPrefix="ucl" TagName="uclPager" Src="~/UserControl/Pager.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BackupDatabase</title>
     <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
     <script language="javascript" type="text/javascript" src="../JS/SqlCheck.js"></script>
     <script language="javascript" type="text/javascript">
        function lbtnDelete()
        {
            return confirm('<%=GetTran("001718","确实要删除吗?") %>');            
        }

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

    
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />      
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
        <br />
        <table width="100%">
			<tr>
				<td >				
					<table border="0" cellpadding="0" cellspacing="0" class="biaozzi">
				        <tr>
					        <td align="center"><%=GetTran("004194","请填写将要备份的文件名例如：filename.bak") %>&nbsp; 
							        <%=GetTran("004195","(注意文件名后要加后缀名.bak）" )%></td>
				        </tr>
				        <tr>
					        <td align="center"><asp:TextBox id="txtPath" runat="server" Width="238px" MaxLength="30"></asp:TextBox><font color="red">*</font>&nbsp;					        
						       &nbsp;&nbsp;
						        <asp:Button ID="btnCheckPath" runat="server" Text="开始备份" style="cursor:pointer" CssClass="another" 
                                    onclick="btnCheckPath_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnToDataBackup" runat="server" CssClass="another" 
                                    Text="转到数据备份" onclick="btnToDataBackup_Click" />
                            </td>
				        </tr>
				        <tr>
					        <td align="center"><asp:label id="Label1" runat="server" Width="678px"></asp:label>&nbsp;
					        </td>
				        </tr>
			        </table>
			        <table border="0" cellpadding="0" cellspacing="0"  width="100%" class="biaozzi" >
						<tr >
						    <td align="center" ><FONT face="宋体" ><%=GetTran("004192","数 据 库 备 份" )%></FONT>&nbsp;&nbsp;&nbsp;					    				
					    				
				   		                <asp:GridView ID="gvBackupDatabase" runat="server" Width="100%" 
                                         AutoGenerateColumns="false" AllowSorting="true" class="tablemb"
                                        onsorting="gvBackupDatabase_Sorting" 
                                    onrowdatabound="gvBackupDatabase_RowDataBound" >
					                    <Columns>
					                        <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
					                            <ItemTemplate>
					                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" 
                                                        oncommand="lbtnDelete_Command" OnClientClick="return lbtnDelete();"><%=GetTran("000022", "删除")%></asp:LinkButton>
					                                <asp:LinkButton ID="lbtnDownFile" runat="server" 
                                                        CommandName="DownFile" oncommand="lbtnDownFile_Command"><%=GetTran("000245", "下载")%></asp:LinkButton>
    								             </ItemTemplate>
					                        </asp:TemplateField>			
					                        <asp:BoundField DataField="DataBackupID" SortExpression="DataBackupID" HeaderText="序号" ItemStyle-Wrap="false" />
					                        <asp:BoundField DataField="DataBackupTime" SortExpression="DataBackupTime" HeaderText="备份时间" ItemStyle-Wrap="false" />
					                        <asp:BoundField DataField="PathFileName" SortExpression="PathFileName" HeaderText="路径及文件名" ItemStyle-Wrap="false" />
					                        <asp:BoundField DataField="OperatorNum" SortExpression="OperatorNum" HeaderText="操作员" ItemStyle-Wrap="false" />						                        					                        
					                    </Columns>
					                    <EmptyDataTemplate>
					                        <table width="100%">
					                            <tr>
					                                <th><%=GetTran("000015", "操作")%></th>
					                                <th><%=GetTran("000012", "序号")%></th>
					                                <th><%=GetTran("004190", "备份时间")%></th>
					                                <th><%=GetTran("004189", "路径及文件名")%></th>
					                                <th><%=GetTran("000662", "操作员")%></th>
					                            </tr>
					                        </table>
					                    </EmptyDataTemplate>
					                    </asp:GridView>
					            </td>
					    </tr>
					</table>									        
				</td>
			</tr>
			<tr>
			    <td>
			        <asp:label id="lblMessage" Runat="server"></asp:label>
				</td>								        
			</tr>
			<ucl:uclPager ID="uclPager" runat="server" />						
		</table>
		<br />
		<br />
		<br />
		<br />
		<br />
	  <div id="cssrain" style="width:100%">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80px">
            <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
            <tr>                
                <td class="sec2">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
            </tr>
            </table>
            </td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="img1" onclick="down3()"/></a></td>
        </tr>
      </table>
	        <div id="divTab2">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">       
        <tbody style="DISPLAY:block">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><%=GetTran("000224","操作说明") %>:<br /> 
                  <%=GetTran("004188","1.将整个数据库备份到服务器上") %>      
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
