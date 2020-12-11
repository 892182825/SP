<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configDefaultSMS.aspx.cs" Inherits="Company_configDefaultSMS" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>短信内容预设</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
      <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="js/tianfeng.js" type="text/javascript"></script>

    <script type="text/javascript">
	 function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
        
   function secBoard(n)
      {
           for(i=0;i<secTable.cells.length;i++)
          secTable.cells[i].className="sec2";
        secTable.cells[n].className="sec1";
        for(i=0;i<mainTable.tBodies.length;i++)
          mainTable.tBodies[i].style.display="none";
        mainTable.tBodies[n].style.display="block";
      }
    </script>


    <style type="text/css">
        a:link {
	color: #075C79;
	text-decoration: none;
}
a:visited {
	text-decoration: none;
	color: #336666;
}
a:hover {
	text-decoration: none;
	color: #FF3300;
}
a:active {
	text-decoration: none;
}
        table{font-size:9pt;}       
        
        .tdh{text-align:center ;font-size:10pt;background-image:url(images/tabledp.gif);color:White;font-weight:bold;height:25px;}
        .tdL{text-align:right ;width:30%;}
        .tdC{text-align:left ;width:40%;}
        .tdR{text-align:left ;}
        .td1{text-align:right ;width:40%;}
        .td2{text-align:left ;width:60%;}       
        .tdh1{text-align:right ;width:20%;height:22px;}
        .tdh2{text-align:left ;width:40%;height:22px;}
        .tdh3{text-align:left ;width:20%;height:22px;}
        .tdh4{text-align:center ;width:20%;height:22px;}
        
        .tb{font-size:9pt;border-left:solid 1px lightblue;border-top:solid 1px lightblue;}
        .tb td{border-bottom:solid 1px lightblue;border-right:solid 1px lightblue;}
    </style>
</head>

<body >
    <form id="form1" runat="server">
    <div>
     <br />	     
     <asp:Panel ID="PanelList" runat="server">
	 <table class ="tb" border="0" width="98%"  cellpadding="0" cellspacing="0" >                                
        <tr>
                                    <td class="tdh">
                                        <%=GetTran("000015", "操作")%></td>    
                                    <td class="tdh">
                                        <%=GetTran("006620", "预设类型")%></td>
                                    <td class="tdh">
                                       <%=GetTran("006621", "预设内容")%> </td>
                                     <td class="tdh">
                                       <%=GetTran("005783", "是否发送")%> </td>     
                                                                     
                                </tr>
                                <tr>
                                 <td class="tdh4"> 
                                      <asp:Button 
                                                    ID="btnRegSet" Text="设置" Runat="server" onclick="btnRegSet_Click" 
                                                    style="cursor:pointer" CssClass="anyes" CausesValidation="False" />&nbsp;</td>        
                                    <td class="tdh1">
                                         <%=GetTran("005779", "会员注册通知")%>：
                                    </td>
                                    <td class="tdh2">
                                        <asp:Label ID="lblRegContent" runat="server">--</asp:Label>
                                        </td>
                                     <td class="tdh3"> 
                                         <asp:Label ID="lblIsOpenReg" runat="server" Text="--"></asp:Label>
                                      </td>    
                                                              
                                </tr>      
   
                                <tr>
                                 <td class="tdh4"> 
                                      <asp:Button 
                                                    ID="btnSetSend" Text="设置" Runat="server" 
                                                    style="cursor:pointer" CssClass="anyes" onclick="btnSetSend_Click" 
                                              CausesValidation="False" />&nbsp;</td>     
                                    <td class="tdh1">
                                         <%=GetTran("005780", "发货通知")%>：
                                    </td>
                                    <td class="tdh2">
                                        <asp:Label ID="lblSendContent" runat="server">--</asp:Label>
                                        </td>
                                     <td class="tdh3"> 
                                         <asp:Label ID="lblIsOpenSend" runat="server">--</asp:Label>
                                      </td>    
                                                                 
                                </tr>                          
           
                                <tr> 
                                 <td class="tdh4"> 
                                      <asp:Button 
                                                    ID="btnSetRemit" Text="设置" Runat="server" 
                                                    style="cursor:pointer" CssClass="anyes" 
                                              onclick="btnSetRemit_Click" CausesValidation="False" />&nbsp;</td>            
                                    <td class="tdh1">
                                         <%=GetTran("005781", "汇款审核通知")%>：
                                    </td>
                                    <td class="tdh2">
                                        <asp:Label ID="lblRemitContent" runat="server">--</asp:Label>
                                        </td>
                                     <td class="tdh3"> 
                                         <asp:Label ID="lblIsOpenRemit" runat="server">--</asp:Label>
                                      </td>    
                                                         
                                </tr>                         
                  
                                <tr>
                                 <td class="tdh4"> 
                                      <asp:Button 
                                                    ID="btnSetRev" Text="设置" Runat="server" 
                                                    style="cursor:pointer" CssClass="anyes" onclick="btnSetRev_Click" 
                                              CausesValidation="False" />&nbsp;</td>   
                                    <td class="tdh1">
                                         <%=GetTran("005782", "应收账款通知")%>：
                                    </td>
                                    <td class="tdh2">
                                        <asp:Label ID="lblRevContent" runat="server">--</asp:Label>
                                        </td>
                                     <td class="tdh3"> 
                                         <asp:Label ID="lblIsOpenRev" runat="server">--</asp:Label>
                                      </td>    
                                                                   
                                </tr>  
                                
                                
                                 <tr>
                                 <td class="tdh4"> 
                                      <asp:Button 
                                                    ID="btnMemberFindSet" Text="设置" Runat="server" 
                                                    style="cursor:pointer" CssClass="anyes" 
                                              CausesValidation="False" onclick="btnMemberFindSet_Click" />&nbsp;</td>   
                                    <td class="tdh1">
                                         <%=GetTran("007228", "会员找回密码通知")%>：
                                    </td>
                                    <td class="tdh2">
                                        <asp:Label ID="lblMemberFind" runat="server">--</asp:Label>
                                        </td>
                                     <td class="tdh3"> 
                                         <asp:Label ID="lblMemberOpen" runat="server">--</asp:Label>
                                      </td>    
                                                                   
                                </tr>   
                                
                                
                                 <tr>
                                 <td class="tdh4"> 
                                      <asp:Button 
                                                    ID="btnStoreFindSet" Text="设置" Runat="server" 
                                                    style="cursor:pointer" CssClass="anyes" 
                                              CausesValidation="False" onclick="btnStoreFindSet_Click" />&nbsp;</td>   
                                    <td class="tdh1">
                                         <%=GetTran("007231", "店铺找回密码通知")%>：
                                    </td>
                                    <td class="tdh2">
                                        <asp:Label ID="lblStoreFind" runat="server">--</asp:Label>
                                        </td>
                                     <td class="tdh3"> 
                                         <asp:Label ID="lblStoreOpen" runat="server">--</asp:Label>
                                      </td>    
                                                                   
                                </tr>                                                                                                     
    </table> 
    </asp:Panel> 
        <asp:Panel ID="PanelReg" runat="server">
         <table  class ="tb"  border="0" width="98%"   cellpadding="0" cellspacing="0" >         
                                  <tr>                                     
                                     <td class="tdh" colspan ="2">
                                       <%=GetTran("006893", "会员注册通知内容预设")%></td>                                    
                                </tr>
                                <tr>
                                    <td class="td1">
                                         <%=GetTran("005779", "会员注册通知")%>：
                                    </td>
                                    <td class="td2">
                                        <asp:textbox id="txtDefaultSMSReg" Runat="server" 
                                                    MaxLength="1000" Height="51px" TextMode="MultiLine" Width="395px"></asp:textbox>
                                                <asp:RequiredFieldValidator ID="rfvReg" runat="server" 
                                                    ControlToValidate="txtdefaultSMSReg" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <br />
                                        <%=GetTran("006623", "例：[Name]你好，欢迎使用管理系统，[profile]，感谢您的使用。")%> <br />                                      
                                        <%=GetTran("006622", "说明：[Name]为会员姓名或昵称，[profile]为接受人个人信息。")%> 
                                       </td>                                                                                          
                                </tr>
                                  <tr>
                                      <td class="td1">
                                           <%=GetTran("005783", "是否发送")%></td>
                                      <td class="td2">
                                          <asp:RadioButtonList ID="rbtnReg" runat="server" Repeatdirection="Horizontal">
                                              <asp:ListItem Value="1">是</asp:ListItem>
                                              <asp:ListItem Value="0">否</asp:ListItem>
                                          </asp:RadioButtonList>
                                      </td>                                                                
                                  </tr>
                                  <tr>
                                      <td class="td1">
                                          &nbsp;</td>
                                      <td class="td2">
                                          &nbsp;&nbsp;<asp:Button ID="btnSetreg" Runat="server" CssClass="anyes" 
                                              onclick="btnSetreg_Click" style="cursor:pointer" Text="设 置" />
                                          &nbsp;<asp:Button ID="btnResetreg" Runat="server" CausesValidation="False" 
                                              CssClass="anyes" onclick="btnResetreg_Click" style="cursor:pointer" 
                                              Text="重 置" />
                                          &nbsp;<asp:Button ID="btnBackReg" Runat="server" CausesValidation="False" 
                                              CssClass="anyes" onclick="btnBackReg_Click" style="cursor:pointer" Text="返回" />
                                      </td>                                                                     
                                  </tr>
        </table> 
                                </asp:Panel>
        <asp:Panel ID="PanelSent" runat="server">
         <table  class ="tb"  border="0" width="98%"   cellpadding="0" cellspacing="0" >         
                                <tr>
                                   
                                    <td class="tdh" colspan ="2">
                                        <%=GetTran("006854", "发货通知内容预设")%></td>                                    
                                </tr>    
         <tr>
                                    <td class="td1">
                                    <%=GetTran("005780", "发货通知")%>：
                                     </td>
                                    <td class="td2">
                                     <asp:textbox id="txtDefaultSmsSend" Runat="server" 
                                                    MaxLength="1000" Height="51px" TextMode="MultiLine" Width="395px"></asp:textbox>
                                                <asp:RequiredFieldValidator ID="rfvSend" runat="server" 
                                                    ControlToValidate="txtdefaultSmsSend" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <br />
                                        <%=GetTran("006619", "例：[Name]你好，您好的货己发出，[profile]，请注意查收，联系电话：xxx。")%> <br />                                      
                                        <%=GetTran("006626", "说明：[Name]为接收人姓名或昵称，[profile]为订单信息。")%>                                         
                                    </td> 
                                                                        
                                </tr>
                                <tr>
                                    <td class="td1">
                                         <%=GetTran("005783", "是否发送")%></td>
                                    <td class="td2">
                                        <asp:RadioButtonList ID="rbtnSend" runat="server" Repeatdirection="Horizontal">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>                                    
                                    
                                </tr>
                                <tr>
                                    <td class="td1">
                                        &nbsp;</td>
                                    <td class="td2">
                                        &nbsp;
                                        <asp:Button ID="btnSend" Runat="server" CssClass="anyes" 
                                            onclick="btnSend_Click" style="cursor:pointer" Text="设 置" />
                                        &nbsp;<asp:Button ID="btnResetSend" Runat="server" CausesValidation="False" 
                                            CssClass="anyes" onclick="btnResetSend_Click" style="cursor:pointer" 
                                            Text="重 置" />
                                        &nbsp;<asp:Button ID="btnBackReg0" Runat="server" CausesValidation="False" 
                                            CssClass="anyes" onclick="btnBackReg_Click" style="cursor:pointer" Text="返回" />
                                    </td>                                   
                                    
                                </tr>
     </table> 
     </asp:Panel>
    <asp:Panel ID="PanelRemitance" runat="server">
     <table  class ="tb"  border="0" width="98%"   cellpadding="0" cellspacing="0" >    
         <tr>
                                    <td class="tdh" colspan="2">
                                    <%=GetTran("005781", "汇款审核通知")%>
                                     </td>
                                     </tr> 
                                    <tr>
                                        <td class="td1">
                                            <%=GetTran("005781", "汇款审核通知")%>： 
                                        </td>
                                        <td class="td2">
                                            <asp:TextBox ID="txtDefaultSmsRemittance" Runat="server" Height="51px" 
                                                MaxLength="1000" TextMode="MultiLine" Width="395px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRemittance" runat="server" 
                                                ControlToValidate="txtDefaultSmsRemittance" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <br />      
                                            <%=GetTran("006628", "例：[Name]你好，您的汇款己收到，[profile]，谢谢您的合作。")%> <br />                                      
                                            <%=GetTran("006627", "说明：[Name]为接收人姓名或昵称，[profile]为汇单信息。")%>                                                                                          
                                          
                                        </td>                                       
                                       
                                    </tr>
                                    <tr>
                                        <td class="td1">
                                             <%=GetTran("005783", "是否发送")%></td>
                                        <td class="td2">
                                            <asp:RadioButtonList ID="rbtnRemittance" runat="server" 
                                                Repeatdirection="Horizontal" style="margin-left: 0px">
                                                <asp:ListItem Value="1">是</asp:ListItem>
                                                <asp:ListItem Value="0">否</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                       
                                        
                                    </tr>
                                    <tr>
                                        <td class="td1">
                                            &nbsp;</td>
                                        <td class="td2">
                                            &nbsp;
                                            <asp:Button ID="btnSetremittance" Runat="server" CssClass="anyes" 
                                                onclick="btnSetremittance_Click" style="cursor:pointer" Text="设 置" />
                                            &nbsp;<asp:Button ID="btnResetremittance" Runat="server" CausesValidation="False" 
                                                CssClass="anyes" onclick="btnResetremittance_Click" style="cursor:pointer" 
                                                Text="重 置" />
                                            &nbsp;<asp:Button ID="btnBackReg1" Runat="server" CausesValidation="False" 
                                                CssClass="anyes" onclick="btnBackReg_Click" style="cursor:pointer" Text="返回" />
                                        </td>
                                                                 
                                    
                                </tr>
     </table> 
    </asp:Panel>
    <asp:Panel ID="PanelSetreceivables" runat="server">
     <table  class ="tb"  border="0" width="98%"   cellpadding="0" cellspacing="0" >          
                                <tr>
                                <td class="tdh" colspan="2">
                                    <%=GetTran("006891", "应收账款通知内容预设")%></td>
                                 </tr> 
                                <tr>
                                    <td class="td1">
                                    <%=GetTran("005782", "应收账款通知")%>：
                                     </td>
                                    <td class="td2">
                                     <asp:textbox id="txtDefaultSMSReceivables" Runat="server" 
                                                    MaxLength="1000" Height="51px" TextMode="MultiLine" Width="396px"></asp:textbox>      
                                                <asp:RequiredFieldValidator ID="rfvReceivables" runat="server" 
                                                    ControlToValidate="txtDefaultSMSReceivables" Display="Dynamic" 
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <br />                                
                                          <%=GetTran("006630", "例：[Name]你好，公司己向您的账户汇入指定金额，[profile]，请及时查收。")%> <br />                                      
                                            <%=GetTran("006629", "说明：[Name]为接收人姓名或昵称，[profile]为汇款信息。")%>                           
                                       
                                        </td>
                                                                                                     
                                </tr>
                                <tr>
                                    <td class="td1">
                                         <%=GetTran("005783", "是否发送")%></td>
                                    <td class="td2">
                                        <asp:RadioButtonList ID="rbtnReceivables" runat="server" 
                                            Repeatdirection="Horizontal">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>                                   
                                    
                                </tr>
                                <tr>
                                    <td class="td1">
                                        &nbsp;</td>
                                    <td class="td2">
                                        &nbsp;&nbsp;<asp:Button ID="btnSetreceivables" Runat="server" CssClass="anyes" 
                                            onclick="btnSetreceivables_Click" style="cursor:pointer" Text="设 置" />
                                        &nbsp;<asp:Button ID="btnResetreceivables" Runat="server" CausesValidation="False" 
                                            CssClass="anyes" onclick="btnResetreceivables_Click" style="cursor:pointer" 
                                            Text="重 置" />
                                        &nbsp;<asp:Button ID="btnBackReg2" Runat="server" CausesValidation="False" 
                                            CssClass="anyes" onclick="btnBackReg_Click" style="cursor:pointer" Text="返回" />
                                    </td>                                   
                                   
                                </tr>
   </table> 
                                </asp:Panel>
                                
                                
   <asp:Panel ID="PaneLoginPassFind" runat="server">
     <table  class ="tb"  border="0" width="98%"   cellpadding="0" cellspacing="0" >          
                                <tr>
                                <td class="tdh" colspan="2">
                                    <%=GetTran("007229", "会员找回密码通知内容预设")%></td>
                                 </tr> 
                                <tr>
                                    <td class="td1">
                                    <%=GetTran("007228", "会员找回密码通知")%>：
                                     </td>
                                    <td class="td2">
                                     <asp:textbox id="txtDefaultSMSFind" Runat="server" 
                                                    MaxLength="1000" Height="51px" TextMode="MultiLine" Width="396px"></asp:textbox>      
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ControlToValidate="txtDefaultSMSFind" Display="Dynamic" 
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <br />                                
                                          <%=GetTran("006630", "例：[Name]你好，公司己向您的账户汇入指定金额，[profile]，请及时查收。")%> <br />                                      
                                            <%=GetTran("006629", "说明：[Name]为接收人姓名或昵称，[profile]为汇款信息。")%>                           
                                       
                                        </td>
                                                                                                     
                                </tr>
                                <tr>
                                    <td class="td1">
                                         <%=GetTran("005783", "是否发送")%></td>
                                    <td class="td2">
                                        <asp:RadioButtonList ID="rbtLoginPassFind" runat="server" 
                                            Repeatdirection="Horizontal">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>                                   
                                    
                                </tr>
                                <tr>
                                    <td class="td1">
                                        &nbsp;</td>
                                    <td class="td2">
                                        &nbsp;&nbsp;<asp:Button ID="btnLoginPassFind" Runat="server" CssClass="anyes" 
                                             style="cursor:pointer" Text="设 置" onclick="btnLoginPassFind_Click" />
                                        &nbsp;<asp:Button ID="btnresetresceLoginPassFind" Runat="server" CausesValidation="False" 
                                            CssClass="anyes" style="cursor:pointer" 
                                            Text="重 置" onclick="btnresetresceLoginPassFind_Click" />
                                        &nbsp;<asp:Button ID="Button3" Runat="server" CausesValidation="False" 
                                            CssClass="anyes" onclick="btnBackReg_Click" style="cursor:pointer" Text="返回" />
                                    </td>                                   
                                   
                                </tr>
   </table> 
                                </asp:Panel>
                                
                                
                                   <asp:Panel ID="PaneStorePassFind" runat="server">
     <table  class ="tb"  border="0" width="98%"   cellpadding="0" cellspacing="0" >          
                                <tr>
                                <td class="tdh" colspan="2">
                                    <%=GetTran("007230", "店铺找回密码通知内容预设")%></td>
                                 </tr> 
                                <tr>
                                    <td class="td1">
                                    <%=GetTran("007231", "店铺找回密码通知")%>：
                                     </td>
                                    <td class="td2">
                                     <asp:textbox id="txtStorePassFind" Runat="server" 
                                                    MaxLength="1000" Height="51px" TextMode="MultiLine" Width="396px"></asp:textbox>      
                                                <asp:RequiredFieldValidator ID="reqStroePassFind" runat="server" 
                                                    ControlToValidate="txtStorePassFind" Display="Dynamic" 
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <br />                                
                                          <%=GetTran("006630", "例：[Name]你好，公司己向您的账户汇入指定金额，[profile]，请及时查收。")%> <br />                                      
                                            <%=GetTran("006629", "说明：[Name]为接收人姓名或昵称，[profile]为汇款信息。")%>                           
                                       
                                        </td>
                                                                                                     
                                </tr>
                                <tr>
                                    <td class="td1">
                                         <%=GetTran("005783", "是否发送")%></td>
                                    <td class="td2">
                                        <asp:RadioButtonList ID="rbtStorePassFind" runat="server" 
                                            Repeatdirection="Horizontal">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>                                   
                                    
                                </tr>
                                <tr>
                                    <td class="td1">
                                        &nbsp;</td>
                                    <td class="td2">
                                        &nbsp;&nbsp;<asp:Button ID="btnStorePassFind" Runat="server" CssClass="anyes" 
                                             style="cursor:pointer" Text="设 置" onclick="btnStorePassFind_Click" />
                                        &nbsp;<asp:Button ID="btnStoreResFind" Runat="server" CausesValidation="False" 
                                            CssClass="anyes" style="cursor:pointer" 
                                            Text="重 置" onclick="btnStoreResFind_Click" />
                                        &nbsp;<asp:Button ID="Button4" Runat="server" CausesValidation="False" 
                                            CssClass="anyes" onclick="btnBackReg_Click" style="cursor:pointer" Text="返回" />
                                    </td>                                   
                                   
                                </tr>
   </table> 
                                </asp:Panel>
    </div>
    
      <div id="cssrain" style ="width:100%">
                   <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="80px">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                   <td   class="sec2"  onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                       align="middle" onclick="down2()" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2" style ="display:none">

                            <tbody style="display: block;">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                   <%=GetTran("005785", "短信内容预设")%><br /> 
                    1.<%=GetTran("006808", "预设短信发送的内容，预设内容在200字符以内。")%>               
                  
                   
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
</html>
