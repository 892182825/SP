<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberNetMap.aspx.cs" Inherits="Company_MemberNetMap" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MemberNetMap</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link rel=Stylesheet href="CSS/Company.css" type="text/css" />
		<style type="text/css">.STYLE1 { FONT-SIZE: 12px; LINE-HEIGHT: 20px }
	        .STYLE2 { COLOR: #00cc00; FONT-SIZE: 12px; LINE-HEIGHT: 20px }
	        .STYLE3 { COLOR: red ;FONT-SIZE: 12px; LINE-HEIGHT: 20px}
	        .STYLE4 { COLOR: #b3b3b3 ;FONT-SIZE: 12px; LINE-HEIGHT: 20px}
	        
	        .Tb22 td{
    
     font-size:12px;
}
		</style>
		<script type="text/javascript">
		    function ShowView(abc)
		    {
		    document.getElementById("HiddenField1").value=abc.firstChild.nodeValue;
		        document.getElementById("TextBox1").value = abc.firstChild.nodeValue;
		        document.getElementById("lkn_submit").click();

		    }
		    
		    function DataBind()
		    {
		    //debugger;
		        var topBh = document.getElementById('TextBox1').value;
                var manageId = '<%=GetManageId() %>';
                var isAnzhi = '<%=isAnzhi2() %>';
                
                var isTop = AjaxClass.GetIsTop(topBh.toString(),isAnzhi,manageId.toString()).value;
                
                if(isTop=="1")
                {
                     document.getElementById('top'+topBh).style.color='red';
                }
		    }
		    
		    function showcy2()
		    {
		        window.location.href="CommonlyNetwork.aspx";
		        return false;
		    }
		    function showcy3()
		    {
		        window.location.href="GraphNet.aspx";
		        return false;
		    }
		</script>
	</HEAD>
	<body onload="javascript:DataBind()" >
		<form id="Form1" method="post" runat="server">
	<br />
			<table width="100%" class="tablemb"  border="0px">
				<tr>
					<td >
					<asp:button  id="Button1" runat="server" CssClass=" anyes" Text="显示" onclick="Button1_Click"></asp:button>
					&nbsp;
					<%=GetTran("000045", "期数")%>：
					<asp:dropdownlist id="DropDownList_QiShu" runat="server"></asp:dropdownlist>
					<%=GetTran("000719", "并且")%><%=GetTran("000673", "网络起点ID")%>：
					<asp:textbox id="TextBox1" runat="server" Width="120"></asp:textbox>
					
					<asp:LinkButton ID="lkn_submit" runat="server" Text="提交"  style="display:none"
                                        onclick="lkn_submit_Click"></asp:LinkButton>
					<asp:Button ID="Button2" runat="server" Text="常用(1)" CssClass="anyes" Visible="false" Enabled="false"  />
					<asp:Button ID="Button5" runat="server" Text="常用(2)" CssClass="anyes" Visible="false" style="display:none;" OnClientClick="return showcy2()"/>
					<asp:Button ID="Button6" runat="server" Text="常用(3)" CssClass="anyes" Visible="false" OnClientClick="return showcy3()"/>
				                    <asp:Button ID="Button3" runat="server" Text="表格" CssClass="anyes" 
                        onclick="Button3_Click"  Visible="false" />
				                    <asp:Button ID="btnZk" runat="server" Text="展开" CssClass="anyes" 
                            onclick="btnZk_Click" Visible="false" />
				                    <asp:Button ID="Button4" runat="server" Text="伸缩" CssClass="anyes" 
                        onclick="Button4_Click" />
					    <asp:HiddenField ID="HiddenField1" runat="server" />
					</td>
				</tr>
			</table>
            
			<table class="tablema"  border="0px" width="100%" >
				<tr>
					<td >
					    <table  class="tablema">
					        <tr>
					            <td>
					              <%=GetTran("000369", "可查看网络")%>  
					        
					                <asp:Repeater ID="Repeater2" Runat="server">
							            <ItemTemplate>
								            <b><%#DataBinder.Eval(Container.DataItem, "wlt")%></b>&nbsp;<span style="color:black"> / </span>&nbsp;
							            </ItemTemplate>
						            </asp:Repeater>
					            </td>
					        </tr>
					    </table>
					</td>
				</tr>
				<tr>
				    <td>
			            <div id="divDH" runat="server">  </div>
		            </td>
		        </tr>
		        
				<tr>
				    <td align="center"  colspan="3">
						<table>
							<tr>
								<td align="center">
									<div id="Divt1" runat="server"></div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="left" >
						1.<span class="STYLE1"><%=GetTran("000674", "点击节点上的图标，可以查看会员三层以内的下属")%>。</span><br>
						2.<span class="STYLE2"><%=GetTran("000676", "绿色背景为老会员")%>。</span><br>
						3.<span class="STYLE3"><%=GetTran("000677", "红色背景为当期新增会员")%>。</span><br>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
			</table>
			<asp:textbox id="txt_PressKeyFlag" style="DISPLAY: none" Runat="server"></asp:textbox></form>
	</body>
</HTML>
