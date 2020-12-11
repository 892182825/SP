<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowNetworkBiaoGeView.aspx.cs" Inherits="Member_ShowNetworkBiaoGeView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="CSS/Member.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
		    function ShowView(isAnzhi,qishu,number)
		    {
		    debugger;
		        var isTrue=false;
		        if(isAnzhi=="1")
		        {
		            isTrue = true;
		        }
                var loginBh = '<%=GetLoginMember() %>'
                var bhCw = AjaxClass.GetLogoutCw(number.toString(),qishu,isTrue).value;
                var loginCw = AjaxClass.GetLogoutCw(loginBh,qishu,isTrue).value;
                var showCs = AjaxClass.GetShowCengS(0).value;
                
                if(showCs- (bhCw -loginCw)<=1)
                {
                    return;
                }

                window.location.href = "ShowNetworkBiaoGeView.aspx?flag=1&bh="+number;
		    }
		</script>
		<style type="text/css">
        a
        {
        	color:black;
        	font-size:12px;
        }
        .divFont
        {
        	color:black;
            font-size:13px;	
        }
    
    </style>
</head>
	<body bgcolor="#ACA899" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
			<form id="Form2" method="post" runat="server">
				<br>
<br>
				<table width="798" border="0" align="center" cellpadding="0" cellspacing="0" background="../images/word_04.gif" id="_word">
					<tr>
                        <td colspan="3">
	                        <img src="../images/word_01.jpg" width="798" height="125" alt="" /></td>
                    </tr>
							<tr>
								<td width="115">&nbsp;</td>
						<td width="568" bgcolor="#FFFFFF"> <div id="wanluo" runat="server" style="width:95%;float:left;" class="divFont"></div>
						        <div id="divBack" runat="server" style="width:5%;float:left;" ></div>
									<FONT face="宋体">
										<asp:table id="Table1" runat="server" width="100%" border="0" cellspacing="1" cellpadding="0" BackColor="Black" class="zihz13"></asp:table></FONT></td>
								<td width="115">&nbsp;</td>
							</tr>
							<tr>
						<td colspan="3">
	                        <img src="../images/word_05.jpg" width="798" height="100" alt="" />
                        </td>
					</tr>
				</table>
					<br>
<br>
			</form>
	</body>
</html>

</html>
