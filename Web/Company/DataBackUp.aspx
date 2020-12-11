<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataBackUp.aspx.cs" Inherits="Company_DataBackUp" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("003225", "DataBackUp")%></title>
    <meta http-equiv="content-type" content="application/ms-excel;charset=UTF8" />    

    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script> 
    <script language="javascript" type="text/javascript">
        function backupMemberInfoReally()
        {
            return confirm('<%=GetTran("004220","确实要备份会员信息吗？") %>');            
        }
        
        function backupMemberDetailsReally()
        {
            return confirm('<%=GetTran("004222","确实要备份会员明细表吗?")%>');            
        }
        
        function backupOrderDetailReally()
        {
            return confirm('<%=GetTran("004229","确实要备份专卖店购货明细表吗?")%>');            
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
    <form ID="Form2" method="post" runat="server">        		
	<br />       
		    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="display:none;">
				<tr>
					<td>			
						<asp:Button ID="btnToBackupDatabase" runat="server"  CssClass="another" Text="转到数据库备份" onclick="btnToBackupDatabase_Click" />										
					</td>
				</tr>
			</table>
			<br />
			<table width="100%" border="0" cellpadding="0" cellspacing="3" class="tablemb">
		            <tr><td ><%=GetTran("003221", "备份会员信息")%></td></tr>								        
		            <tr><td style="white-space:nowrap"><%=GetTran("003220", "备份注册会员信息,备份后的文件将存放在服务器上的数据库里边")%></td></tr>
		            <tr>
						<td ><%=GetTran("003193","备份方式") %>：						    
						    <asp:DropDownList ID="ddlfs" runat="server" AutoPostBack="True" onselectedindexchanged="ddlfs_SelectedIndexChanged" 
                                >
							    <asp:ListItem Value="请选择备份方式">请选择备份方式</asp:ListItem>
							    <asp:ListItem Value="Date">按日期备份</asp:ListItem>
							    <asp:ListItem Value="ExpectNum">按期数</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>					
					
					<tr ID="tr1" runat="server">											    
						<td>
						    <%=GetTran("000559","开始时间") %>：
						    <asp:TextBox ID="txtBegin" runat="server" onfocus="WdatePicker()" CssClass="Wdate"  ></asp:TextBox>
						    <%=GetTran("001373", "截止时间")%>：
						    <asp:TextBox ID="txtEnd" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox>
						</td>
					</tr>																		
					
					<tr ID="tr2" runat="server">
						<td><%=GetTran("000061", "选择期数")%>：							
							<asp:DropDownList ID="ddlqishu" runat="server"></asp:DropDownList>
						</td>
					</tr>
		            <tr><td ><asp:Button  ID="btnBackupMemberInfo" runat="server" Text="备 份" OnClientClick="return backupMemberInfoReally();" CssClass="anyes" OnClick="btnBackupMemberInfo_Click" /></td></tr>
      							
					<tr><td ><br /><%=GetTran("003216", "备份会员明细表")%></td></tr>
					<tr><td ><%=GetTran("003213", "本操作将备份注册会员的详细信息.备份操作分两种：")%></td></tr>
					<tr><td ><%=GetTran("003211", "1.按日期.将备份介于一个日期范围内的所有会员详细信息")%></td></tr>
					<tr><td ><%=GetTran("003209", "2.按期数.将备份处于某个期数内的会员详细信息")%></td></tr>
					
					<tr>
						<td ><%=GetTran("003193","备份方式") %>：						    
						    <asp:DropDownList ID="ddlMemberDetails" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlMemberDetails_SelectedIndexChanged">
							    <asp:ListItem Value="请选择备份方式">请选择备份方式</asp:ListItem>
							    <asp:ListItem Value="Date">按日期备份</asp:ListItem>
							    <asp:ListItem Value="ExpectNum">按期数</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>					
					
					<tr ID="trDate" runat="server">											    
						<td>
						    <%=GetTran("000559","开始时间") %>：
						    <asp:TextBox ID="txtMemberBeginTime" runat="server" onfocus="WdatePicker()" CssClass="Wdate"  ></asp:TextBox>
						    <%=GetTran("001373", "截止时间")%>：
						    <asp:TextBox ID="txtMemberEndTime" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox>
						</td>
					</tr>																		
					
					<tr ID="trExpectNum" runat="server">
						<td><%=GetTran("000061", "选择期数")%>：							
							<asp:DropDownList ID="ddlExpectNum" runat="server"></asp:DropDownList>
						</td>
					</tr>										
					<tr>
						<td><asp:Button ID="btnBackupMemberDetails" runat="server" Text="备 份" CssClass="anyes"
                                onclick="btnBackupMemberDetails_Click" OnClientClick="return backupMemberDetailsReally()" /></td>
					</tr>
					
					<tr><td ><br /><%=GetTran("003195", "备份专卖店购货商品明细表")%></td></tr>
					<tr><td ><%=GetTran("003200", "本操作将备份店铺的购货明细信息分两种")%>：</td></tr>
					<tr><td ><%=GetTran("003198", "1.按日期备份，将备份介于一个日期范围内的所有明细信息")%></td></tr>
					<tr><td ><%=GetTran("003196", "2.按期数备份，将备份处于某个期数内的明细信息")%></td></tr>
					
					<tr>
						<td><%=GetTran("003193", "备份方式")%>：
							<asp:DropDownList ID="ddlOrderDetail" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlOrderDetail_SelectedIndexChanged" >
								<asp:ListItem Value="请选择备份方式">请选择备份方式</asp:ListItem>
								<asp:ListItem Value="Date">按日期备份</asp:ListItem>
								<asp:ListItem Value="ExpectNum">按期数</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
															
					<tr ID="trDataBackupDate" runat="server">
							<td><%=GetTran("000559","开始时间") %>：
						        <asp:TextBox ID="txtOrderBeginTime" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox>
						        <%=GetTran("001373", "截止时间")%>：
						        <asp:TextBox ID="txtOrderEndTime" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox>											
							</td>
						</tr>										
					
					<tr ID="trDataBackupExpectNum" runat="server">
					
						<td ><%=GetTran("000061", "选择期数")%>：						    
							<asp:DropDownList ID="ddlDataBackupExpectNum" runat="server"></asp:DropDownList>
						</td>
					</tr>										
					<tr>
						<td ><asp:Button ID="btnBackupOrderDetail" runat="server" Text="备 份" style="cursor:pointer" CssClass="anyes"
                                onclick="btnBackupOrderDetail_Click" OnClientClick="return backupOrderDetailReally()" /></td>
					</tr>
					<tr>
					    <td >
					        <asp:GridView ID="gvHidden" runat="server" Visible="false"></asp:GridView>				
					    </td>					    
					</tr>			
			    </table>				
			<br />
		    <br />
		    <br />
		    <br />
		    <br />
		    <br />
		    <div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80px"><table width="100%" style="height:28px;" border="0" cellpadding="0" cellspacing="0" id="secTable">
               <tr>
                    <td class="sec2">
                        <span id="span2" title="" onmouseover="cutDescription()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                    </td>
                </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" alt="" onclick="down3()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" style="height:68px;" border="0" cellspacing="0" class="DMbk" id="mainTable">       
        <tbody style="DISPLAY:block" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><%=GetTran("003186", "备份说明")%>:<br /> 
                  <%=GetTran("003180", "1.此功能可以备份会员的基本信息和会员的购货明细,及备份专卖店购货明细")%>      
                    </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
			<%= msg %>			
	</form>
</body>
</html>
