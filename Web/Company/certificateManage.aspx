<%@ Page Language="C#" AutoEventWireup="true" CodeFile="certificateManage.aspx.cs" Inherits="source_certificateManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=GetTran("006832", "快钱证书密码设置")%></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <style type ="text/css" >
    table{font-size:9pt;}
     .tb{font-size:9pt;border-left:solid 1px lightblue;border-top:solid 1px lightblue;}
        .tb td{border-bottom:solid 1px lightblue;border-right:solid 1px lightblue;}
    </style>
    
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
      <script type="text/javascript">
	function down3()
	{
		if(document.getElementById("divTab2").style.display=="none") 
		{
			document.getElementById("divTab2").style.display="";
			document.getElementById("imgX").src="images/dis1.GIF";
			
		}
		else 
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="images/dis.GIF";
		}
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
      secTable.cells[i].className="sec2";
    secTable.cells[n].className="sec1";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
   

   
	 $(document).ready(function(){
			if($.browser.msie && $.browser.version == 6) {
				FollowDiv.follow();
			}
	 });
	 FollowDiv = {
			follow : function(){
				$('#cssrain').css('position','absolute');
				$(window).scroll(function(){
				    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
					$('#cssrain').css( 'top' , f_top );
				});
			}
	  }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellSpacing="0" cellPadding="0" width="100%" border="0" class ="tb">
										<TR>
											<TD style="background-image:url(images/tabledp.gif);color:White;height:25px;font-weight:bold ;text-align:center ;" colspan="2"><%=GetTran("006832", "快钱证书密码设置")%></TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 18px">
												<P align="right"><FONT face="宋体" color="#ff0066">*</FONT><FONT face="宋体"><%=GetTran("006833", "数字证书密码")%>：</FONT></P>
											</TD>
											<TD style="HEIGHT: 18px"><asp:textbox id="txtCertificatePwd" TextMode="Password" Runat="server" MaxLength="32"></asp:textbox>&nbsp;<%=GetTran("006934", "注：密码为4~32位字符。")%><asp:RegularExpressionValidator 
                                                    ID="revPwd" runat="server" ControlToValidate="txtPwdConfirm" Display="Dynamic" 
                                                    ErrorMessage="格式错误" ValidationExpression=".{4,20}"></asp:RegularExpressionValidator>
                                               </TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 24px">
												<P align="right"><FONT face="宋体" color="#ff0066">*</FONT><FONT face="宋体"><FONT face="宋体"><%=GetTran("006834", "数字证书密码确认")%></FONT></FONT>：</P>
											</TD>
											<TD style="HEIGHT: 24px"><asp:textbox id="txtPwdConfirm" runat="server" TextMode="Password" MaxLength="32"></asp:textbox>&nbsp;<asp:RegularExpressionValidator 
                                                    ID="revPwd0" runat="server" ControlToValidate="txtCertificatePwd" 
                                                    Display="Dynamic" ErrorMessage="格式错误" ValidationExpression=".{4,20}"></asp:RegularExpressionValidator>
                                                </TD>
										</TR>
										<TR>
											<TD style="TEXT-ALIGN: right; HEIGHT: 24px"><%=GetTran("006835", "密码类型")%>：
											</TD>
											<TD style="HEIGHT: 24px"><FONT face="宋体"><asp:dropdownlist id="ddlPwdType" runat="server">
														<asp:ListItem Value="CertificatePriPub_pwd" Selected="True">合成证书密码</asp:ListItem>
														<asp:ListItem Value="CertificatePri_pwd">证书私钥密码</asp:ListItem>														
													</asp:dropdownlist></FONT></TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 31px"><FONT face="宋体">&nbsp;</FONT></TD>
											<TD style="HEIGHT: 31px"><FONT face="宋体">&nbsp;&nbsp;&nbsp;</FONT>
												<asp:button  id="btn_submit" 
													 runat="server" Text="设置" OnClick="btn_submit_Click" CssClass="anyes"></asp:button>
												&nbsp;
												<asp:button id="btn_reset" runat="server" CssClass="anyes" Text="重置"></asp:button>
												&nbsp;
												<input id="btnBack" type="button" 
                                                    onclick ="javascript:window.location.href='SetParameters.aspx';"  
                                                    value ='<%=GetTran("000421","返回") %>' class="anyes" />
												</TD>
										</TR>
										<TR>
											<TD style="WIDTH: 206px" align="center" colSpan="2"><FONT face="宋体"></FONT></TD>
										</TR>
									</table>
    </div>
      <div id="cssrain" style ="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80">
            <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" ID="secTable">
              <tr>
               <td class="sec2" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
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
                  <td>
                  1.<%=GetTran("006832", "快钱证书密码设置")%>
                    </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </form>
</body>
</html>
