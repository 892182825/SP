<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewManage.aspx.cs" Inherits="Company_ViewManage" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML xmlns="http://www.w3.org/1999/xhtml">
	<HEAD>
		<title>GlManage</title>
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
                height: 221px;
            }
        </style>
</HEAD>
	<body>
		<form id="Form2" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top" class="style1">
        <br />
        <br /><div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        Width="100%" onrowcommand="GridView1_RowCommand" 
            BackColor="#F8FBFD" CssClass="tablemb" onrowdatabound="GridView1_RowDataBound">
            <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbDel" runat="server" CommandName="Del" 
                                    CommandArgument='<%#Eval("ID") %>'  
                                    OnClientClick="Javascript:return confirm('是否删除当前记录')">删除</asp:LinkButton>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="manageID" HeaderText="管理员编号" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Number" HeaderText="团队顶点编号" >
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="网络类型">
                            <ItemTemplate>
                                <%# GetNetType(DataBinder.Eval(Container.DataItem, "type").ToString())%>
                            </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                        </td></tr>
                                <tr>
                                    <th>
                                        管理员编号
                                    </th>
                                    <th>
                                        团队顶点编号
                                    </th>
                                    <th>
                                        网络图类型
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>   
                        </EmptyDataTemplate>
            <AlternatingRowStyle BackColor="#F1F4F8" />
                    </asp:GridView>
                    <uc1:Pager ID="Pager1" runat="server" />
                    </div>
      </td>
  </tr>  
    <tr>
    <td align="left">
       
        <asp:Button ID="Btn1" runat="server" class="another" Text="添 加" 
            onclick="Btn1_Click"/>
        &nbsp;
        <asp:Button ID="BtnBack" runat="server" class="another" Text = "返 回" 
            onclick="BtnBack_Click" />
    </td>
  </tr>
    <tr>
        <td align="left">
            <div id="Div1" runat="server" visible="false">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="biaozzi">
                    <tr align="left">
                        <td align="right" width="100">团队顶点编号：</td>
                        <td align="left"><asp:TextBox ID="txtNumber" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr align="left">
                        <td align="right" width="100">网路类别：</td>
                        <td align="left"><asp:RadioButtonList id="rbtType" runat=server 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True">推荐网络</asp:ListItem>
                                <asp:ListItem Value="1">安置网络</asp:ListItem>
                            </asp:RadioButtonList> </td>
                    </tr>
                    <tr align="left">
                        <td colspan="2">&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnAdd" runat="server" Text="确 定" 
                                class="another" onclick="BtnAdd_Click" />&nbsp; <asp:Button ID="BtnClear" 
                                runat="server" Text="取 消" class="another" onclick="BtnClear_Click" /></td>
                    </tr>           
                </table>
            </div>
        </td>
    </tr>  
  <tr>
    <td valign="top">
	
	</td>
  </tr>
</table>
<div id="cssrain" >
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="80">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                                    <tr>
                                        <td class="sec2">
                                            说 明
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
	1、设置管理员使用系统的权限。<br />
                      2、添加新的管理员，删除管理员。
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
<%=msg %>
</form>
					</body>
</HTML>
