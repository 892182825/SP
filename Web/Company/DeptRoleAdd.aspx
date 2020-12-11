<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptRoleAdd.aspx.cs" Inherits="Company_AddRoleDept" %>

<%@ Register src="../UserControl/UCPermission.ascx" tagname="UCPermission" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
  <HEAD>
		<title>添加角色</title>
		
<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>
<script>
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
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
  function A(a,b,c)
  {
  alert(a+b+c)
  }
</SCRIPT>
  </HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
		
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="biaozzi">
  <tr>
    <td valign="top"><br />
    <div>
       
			 <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
          <tr>
            <td>
</td>
                    <td >
				<h4><%=GetTran("000950", "添加角色")%></h4></td>
                    <td >
                        </td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("000992", "角色名称")%>：</td>
                    <td>
					<asp:TextBox ID="txtRoleName" runat="server"  MaxLength="10"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right"><%=GetTran("000331", "部门")%>：
											</td>
                    <td>
												<asp:DropDownList ID="ddlDepts" runat="server" >
                    </asp:DropDownList></td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        
																<uc1:UCPermission ID="UCPermission1" runat="server" /></td>
                    <td>
                     </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td><asp:Button ID="btnAdd" runat="server" Text="添 加" CssClass="anyes" style="cursor:hand;" onclick="btnAdd_Click" />
											<INPUT onclick="javascript:location.href='DeptRolesManage.aspx'" type="button" style="cursor:hand;" value='<%=GetTran("000421", "返回")%>' class="anyes"></td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            </div>
            
      <br />
      
      </td>
  </tr>
</table>


		</form>
	</body>
</HTML>
