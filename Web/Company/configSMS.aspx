<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configSMS.aspx.cs" Inherits="Company_configSMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信网关参数设置</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
   <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onerror=function ()
        {
            return true;
        };
      
      function isDelete()
      {
         return window.confirm('<%=GetTran("000248")%>');
      }
        
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>
  
    <style type ="text/css" >
     table{font-size:9pt;}
     .tdh{text-align:center ;font-size:10pt;background-image:url(images/tabledp.gif);color:White;font-weight:bold;height:25px;}
      .tb{font-size:9pt;border-left:solid 1px lightblue;border-top:solid 1px lightblue;}
        .tb td{border-bottom:solid 1px lightblue;border-right:solid 1px lightblue;}
    </style>
</head>
  
<body >
    <form id="form1" runat="server">
    <div>
     <br />		  				   
    <table class ="tb"  style ="width:98%" cellSpacing="0" cellPadding="0" border="0">
		<TR>
											<TD class="tdh" colspan="2">
											<%=GetTran("005778", "短信网关设置")%>
											</TD>
										</TR>
										<TR>
											<TD style="WIDTH: 478px; HEIGHT: 18px">
												<P align="right"><FONT face="宋体"><%=GetTran("005805", "网关账号")%>：</FONT></P>
											</TD>
											<TD style="HEIGHT: 18px"><asp:textbox id="txtPassPort" Runat="server" MaxLength="10"></asp:textbox><FONT face="宋体" color="#ff0066">*</FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 478px; HEIGHT: 24px">
												<P align="right"><%=GetTran("005806", "网关密码")%>：</P>
											</TD>
											<TD style="HEIGHT: 24px"><asp:textbox id="txtPwd" runat="server" TextMode="Password" MaxLength="10"></asp:textbox><FONT face="宋体" color="#ff0066">*</FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 478px; HEIGHT: 24px; TEXT-ALIGN: right"><FONT face="宋体"><%=GetTran("005807", "网关地址")%>：</FONT>
											</TD>
											<TD style="HEIGHT: 24px"><FONT face="宋体">
													<asp:textbox id="txtUrl" runat="server" MaxLength="100" Width="264px">http://www.139000.com/send/gsend.asp?</asp:textbox><FONT face="宋体" color="#ff0066">*</FONT>（<%=GetTran("005808", "默认")%>：<A href="http://www.139000.com/send/gsend.asp">http://www.139000.com/send/gsend.asp</A>?）</FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 478px; HEIGHT: 24px; TEXT-ALIGN: right"><%=GetTran("000503", "单价")%>：</TD>
											<TD style="HEIGHT: 24px">
                                                <asp:TextBox ID="txtSMSUnitPrice" runat="server" Width="52px">0.1</asp:TextBox>
                                            </TD>
										</TR>
										<TR>
											<TD style="WIDTH: 478px; HEIGHT: 31px"><FONT face="宋体">&nbsp;</FONT></TD>
											<TD style="HEIGHT: 31px"><FONT face="宋体">&nbsp;&nbsp;&nbsp;</FONT>&nbsp;<FONT face="宋体"><asp:Button 
                                                    ID="btn_submit" Text="设 置" Runat="server" onclick="btn_submit_Click1" 
                                                    style="cursor:pointer" CssClass="anyes" />&nbsp;</FONT><input 
                                                    id="Reset1" class="anyes" type="reset" title="重 置" value ='<%=GetTran("006812", "重 置")%>' />&nbsp;
                                                <input 
                                                    id="btnBack" class="anyes" type="button"  title="返 回" value ='<%=GetTran("000421", "返 回")%>' onclick ="javascript:window.location.href='SetParameters.aspx';" /></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100%;text-align:center;"  colSpan="2">
                                                <asp:Label ID="lblSetFlag" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </TD>
										</TR>			
						
						</table>
    </div>
    <div id="cssrain" style ="width:100%">
                   <table width="100%" border="0" cellpadding="0" cellspacing="0" style="background-image:url('images/DMdp.gif')" >
                        <tr>
                            <td width="80px">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                   <td class="sec2" onclick="secBoard(1)">
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
                    <div id="divTab2" style ="display :none ;">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                            <tbody style="display: block">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <%=GetTran("005778", "短信网关设置")%><br /> 
                    1.<%=GetTran("005804", "设置短信网关的基本信息。")%><br />
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
