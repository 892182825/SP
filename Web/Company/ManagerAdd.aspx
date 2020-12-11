<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerAdd.aspx.cs" Inherits="Company_ManagerAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
		<title>创建新管理员</title>
		<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script><script>
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

<script type="text/javascript">
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
<SCRIPT language="javascript">
     function secBoard(n)
  
  {
  //  document.Form1.sa.value=n;
  //  document.getElementById("s").click();
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
</SCRIPT>
        <style type="text/css">
            .style1
            {
                width: 50%;
            }
            .style2
            {
                width: 50%;
            }
        </style>
  </HEAD>
	<body onload="down2()">
		<form id="Form1" method="post" runat="server">
		<br />
		<center>
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style1" align="left">
                        <h4 ><%=GetTran("000325")%></h4></td>
                </tr>
                <tr>
                    <td  align="right">
                        <%=GetTran("000327")%></td>
                    <td  style="text-align:left">
												<asp:TextBox ID="txtNumber" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td  align="right">
                        <%=GetTran("000107")%>：</td>
                    <td style="text-align:left">
												<asp:TextBox ID="txtName" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("000331")%>：</td>
                    <td style="text-align:left">
												<asp:DropDownList ID="ddlDepts" runat="server" AutoPostBack="True"  Width="155px"
                        ondatabound="ddlDepts_DataBound" 
                        onselectedindexchanged="ddlDepts_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("000333")%>：</td>
                    <td style="text-align:left">
												<asp:DropDownList ID="ddlRoles" runat="server"  Width="155px">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr><td align="right"><%=GetTran("006710", "是否有查看会员安置权限")%>：</td><td style="text-align:left">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                        RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td></tr>
                <tr><td align="right"><%=GetTran("006711", "是否有查看会员推荐权限")%>：</td><td style="text-align:left">
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" 
                        RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td></tr>
                <tr>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
													<asp:Button ID="btnAdd" runat="server" Text="添 加" OnClick="BtnAdd_Click"  CssClass="anyes"/>&nbsp;
													<INPUT onclick="javascript: window.location ='ManagerManage.aspx'" type="button" value='<%=GetTran("000421", "返回")%>' class="anyes"></td>
                </tr>
                </table>
                </center>
                <table>
                <tr>
    <td valign="top">
                 
	
	</td>
  </tr>
</table>
<div id="cssrain">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="80">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                                    <tr>
                                        <td class="sec2">
                                            <span id="sp" title='<%=GetTran("000033")%>'><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033"))%></span>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="img1"
                                        onclick="down2()" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="Table2">
                            <tbody style="display: block">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
	<%=GetTran("000335")%>
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
</HTML>
