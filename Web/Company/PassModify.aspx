<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PassModify.aspx.cs" Inherits="Company_PassModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>密码修改</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
            <script language="javascript" type="text/javascript">
    function confirmvlue()
    {
        return confirm('<%=GetTran("001688", "确定要清空吗？")%>');
    }
    function down2()
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
    </script>
    </script>
         <script language="javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>
</head>
<body onload="down2()">
    <form id="Form1" method="post" runat="server">
    <br />
     <table align="center" width="400px" border="0" cellpadding="0" cellspacing="3" class="tablemb">
		<tr>
		    <td style="white-space:nowrap" align="right"><%=GetTran("001344", "原密码")%>：</td>
		    <td style="white-space:nowrap"><asp:TextBox ID="txtOldPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox></td>
		</tr>
		<tr>
		    <td style="white-space:nowrap" align="right"><%=GetTran("001348", "新密码")%>：</td>
		    <td style="white-space:nowrap"><asp:TextBox ID="txtNewPassword" runat="server" MaxLength="10" TextMode="Password" ></asp:TextBox></td>
		</tr>
		<tr>
		    <td style="white-space:nowrap" align="right"><%=GetTran("001780", "确认新密码")%>：</td>
		    <td style="white-space:nowrap"><asp:TextBox ID="txtInputAgainNewPassword" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox></td>		    
		</tr>
		<tr>
		    <td colspan="2" align="center" ><br />
		        <asp:Button ID="btnSubmit" runat="server" Text="确 定" onclick="btnSubmit_Click" CssClass="anyes" />&nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" Text="重 填" onclick="btnReset_Click"  CssClass="anyes" OnClientClick="return confirmvlue()" />
		    </td>
		</tr>
	</table>
	<br />	
	<br />
	<br />
	<br />
	<br />
     <div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
        <tr>
          <td width="80"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
              <tr>                
                <td class="sec2"><span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span></td>   
              </tr>
          </table></td>
          <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down3()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2" >
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">       
        <tbody style="DISPLAY:block">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                  <%=GetTran("001783", "1、修改管理员登陆密码。")%>      
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
