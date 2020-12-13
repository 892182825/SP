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
            return confirm('<%=GetTran("004220","ȷʵҪ���ݻ�Ա��Ϣ��") %>');            
        }
        
        function backupMemberDetailsReally()
        {
            return confirm('<%=GetTran("004222","ȷʵҪ���ݻ�Ա��ϸ����?")%>');            
        }
        
        function backupOrderDetailReally()
        {
            return confirm('<%=GetTran("004229","ȷʵҪ����ר���깺����ϸ����?")%>');            
        }
        
        function cutDescription()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "˵ ��") %>';
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
						<asp:Button ID="btnToBackupDatabase" runat="server"  CssClass="another" Text="ת�����ݿⱸ��" onclick="btnToBackupDatabase_Click" />										
					</td>
				</tr>
			</table>
			<br />
			<table width="100%" border="0" cellpadding="0" cellspacing="3" class="tablemb">
		            <tr><td ><%=GetTran("003221", "���ݻ�Ա��Ϣ")%></td></tr>								        
		            <tr><td style="white-space:nowrap"><%=GetTran("003220", "����ע���Ա��Ϣ,���ݺ���ļ�������ڷ������ϵ����ݿ����")%></td></tr>
		            <tr>
						<td ><%=GetTran("003193","���ݷ�ʽ") %>��						    
						    <asp:DropDownList ID="ddlfs" runat="server" AutoPostBack="True" onselectedindexchanged="ddlfs_SelectedIndexChanged" 
                                >
							    <asp:ListItem Value="��ѡ�񱸷ݷ�ʽ">��ѡ�񱸷ݷ�ʽ</asp:ListItem>
							    <asp:ListItem Value="Date">�����ڱ���</asp:ListItem>
							    <asp:ListItem Value="ExpectNum">������</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>					
					
					<tr ID="tr1" runat="server">											    
						<td>
						    <%=GetTran("000559","��ʼʱ��") %>��
						    <asp:TextBox ID="txtBegin" runat="server" onfocus="WdatePicker()" CssClass="Wdate"  ></asp:TextBox>
						    <%=GetTran("001373", "��ֹʱ��")%>��
						    <asp:TextBox ID="txtEnd" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox>
						</td>
					</tr>																		
					
					<tr ID="tr2" runat="server">
						<td><%=GetTran("000061", "ѡ������")%>��							
							<asp:DropDownList ID="ddlqishu" runat="server"></asp:DropDownList>
						</td>
					</tr>
		            <tr><td ><asp:Button  ID="btnBackupMemberInfo" runat="server" Text="�� ��" OnClientClick="return backupMemberInfoReally();" CssClass="anyes" OnClick="btnBackupMemberInfo_Click" /></td></tr>
      							
					<tr><td ><br /><%=GetTran("003216", "���ݻ�Ա��ϸ��")%></td></tr>
					<tr><td ><%=GetTran("003213", "������������ע���Ա����ϸ��Ϣ.���ݲ��������֣�")%></td></tr>
					<tr><td ><%=GetTran("003211", "1.������.�����ݽ���һ�����ڷ�Χ�ڵ����л�Ա��ϸ��Ϣ")%></td></tr>
					<tr><td ><%=GetTran("003209", "2.������.�����ݴ���ĳ�������ڵĻ�Ա��ϸ��Ϣ")%></td></tr>
					
					<tr>
						<td ><%=GetTran("003193","���ݷ�ʽ") %>��						    
						    <asp:DropDownList ID="ddlMemberDetails" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlMemberDetails_SelectedIndexChanged">
							    <asp:ListItem Value="��ѡ�񱸷ݷ�ʽ">��ѡ�񱸷ݷ�ʽ</asp:ListItem>
							    <asp:ListItem Value="Date">�����ڱ���</asp:ListItem>
							    <asp:ListItem Value="ExpectNum">������</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>					
					
					<tr ID="trDate" runat="server">											    
						<td>
						    <%=GetTran("000559","��ʼʱ��") %>��
						    <asp:TextBox ID="txtMemberBeginTime" runat="server" onfocus="WdatePicker()" CssClass="Wdate"  ></asp:TextBox>
						    <%=GetTran("001373", "��ֹʱ��")%>��
						    <asp:TextBox ID="txtMemberEndTime" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox>
						</td>
					</tr>																		
					
					<tr ID="trExpectNum" runat="server">
						<td><%=GetTran("000061", "ѡ������")%>��							
							<asp:DropDownList ID="ddlExpectNum" runat="server"></asp:DropDownList>
						</td>
					</tr>										
					<tr>
						<td><asp:Button ID="btnBackupMemberDetails" runat="server" Text="�� ��" CssClass="anyes"
                                onclick="btnBackupMemberDetails_Click" OnClientClick="return backupMemberDetailsReally()" /></td>
					</tr>
					
					<tr><td ><br /><%=GetTran("003195", "����ר���깺����Ʒ��ϸ��")%></td></tr>
					<tr><td ><%=GetTran("003200", "�����������ݵ��̵Ĺ�����ϸ��Ϣ������")%>��</td></tr>
					<tr><td ><%=GetTran("003198", "1.�����ڱ��ݣ������ݽ���һ�����ڷ�Χ�ڵ�������ϸ��Ϣ")%></td></tr>
					<tr><td ><%=GetTran("003196", "2.���������ݣ������ݴ���ĳ�������ڵ���ϸ��Ϣ")%></td></tr>
					
					<tr>
						<td><%=GetTran("003193", "���ݷ�ʽ")%>��
							<asp:DropDownList ID="ddlOrderDetail" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlOrderDetail_SelectedIndexChanged" >
								<asp:ListItem Value="��ѡ�񱸷ݷ�ʽ">��ѡ�񱸷ݷ�ʽ</asp:ListItem>
								<asp:ListItem Value="Date">�����ڱ���</asp:ListItem>
								<asp:ListItem Value="ExpectNum">������</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
															
					<tr ID="trDataBackupDate" runat="server">
							<td><%=GetTran("000559","��ʼʱ��") %>��
						        <asp:TextBox ID="txtOrderBeginTime" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox>
						        <%=GetTran("001373", "��ֹʱ��")%>��
						        <asp:TextBox ID="txtOrderEndTime" runat="server" onfocus="WdatePicker()" CssClass="Wdate" ></asp:TextBox>											
							</td>
						</tr>										
					
					<tr ID="trDataBackupExpectNum" runat="server">
					
						<td ><%=GetTran("000061", "ѡ������")%>��						    
							<asp:DropDownList ID="ddlDataBackupExpectNum" runat="server"></asp:DropDownList>
						</td>
					</tr>										
					<tr>
						<td ><asp:Button ID="btnBackupOrderDetail" runat="server" Text="�� ��" style="cursor:pointer" CssClass="anyes"
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
                        <span id="span2" title="" onmouseover="cutDescription()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "˵ ��"))%></span>
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
                  <td><%=GetTran("003186", "����˵��")%>:<br /> 
                  <%=GetTran("003180", "1.�˹��ܿ��Ա��ݻ�Ա�Ļ�����Ϣ�ͻ�Ա�Ĺ�����ϸ,������ר���깺����ϸ")%>      
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
