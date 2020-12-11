<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDeptAdd.aspx.cs" Inherits="Company_AddCompanyDept" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
		<title>创建新系统部门</title>
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
                height: 19px;
            }
        </style>
  </HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><br /><div style="width:100%" style="text-align:center;">
        <table background="#F8FBFD" class="tablemb" width="400px">
                <tr>
                    <td><br />
                        &nbsp;</td>
                    <td align="left">
                        <h4 id="title" runat="server"><%=GetTran("000949", "添加部门")%></h4>
                        </td>
                        
                </tr>
                <tr>
                    <td class="style1"align="right">
                         <%=GetTran("001020", "公司部门名")%>：</td>
                    <td class="style1" align="left">
						<asp:TextBox ID="txtName" runat="server" MaxLength="10" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>

                        &nbsp;</td>
                    <td align="left"><br />
                        <asp:Button ID="btnAdd" runat="server" onclick="BtnAdd_Click" Text="添 加"  CssClass="anyes"/>
                        <INPUT onclick="javascript: window.location = 'CompanyDeptManage.aspx'"" type="button" value='<%=GetTran("000421", "返回")%>' class="anyes">
                        </td>
                </tr>
            </table>
        </div>
      <br /></td>
  </tr>
</table>
		</form>
	</body>
</HTML>
