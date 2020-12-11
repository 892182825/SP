<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowNetworkViewNew.aspx.cs" Inherits="Company_ShowNetworkViewNew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>QueryAnZhiNetworkView1</title>
        <script src="../JS/jquery.js" type="text/javascript"></script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link rel="Stylesheet" href="CSS/Company.css" type="text/css" />
		 <style type="text/css">
		   A:link { FONT-SIZE: 12px }
			A:visited { FONT-SIZE: 12px }
			A:active { FONT-SIZE: 12px }
			A:hover { FONT-SIZE: 12px }
			BODY { FONT-SIZE: 12px }
			TD { FONT-SIZE: 12px }
			.ls { FONT-SIZE: 11px }
			.ls1 { FONT-SIZE: 11px;color:Purple; }
			.ls2 { FONT-SIZE: 20px }
		</style>
		<!--------------设置网络图的样式(根据需要修改)------------------->
<style type="text/css">
span.ss{cursor:pointer;font-size:12px; }
span.bh{cursor:pointer; color:red;font-size:12px;}
span.xm{cursor:pointer;color:black;font-size:12px;}
span.js{cursor:pointer;color:black;font-size:12px;}
span.bs{cursor:pointer;color:blue;font-size:12px;}
.box{cursor:pointer; font: 8pt "宋体"; position: absolute; background: LightGrey;color:blue;z-index:101 }
</style>
<style type="text/css">

.STYLE1 {	font-size: 12px;	
	color: #333;
}
.STYLE1 a 
{
	
	font-size: 12px;
	color: #333;
	text-decoration: none;
}
.STYLE1 a:hover {
	font-size: 12px;
	color: #FF0000;
	text-decoration: none;
}
.STYLE2 {
	font-size: 12px;
	color: #DFDFDF;
}
.STYLE3 {
	font-size: 7px;
	color: #DFDFDF;
	line-height: 1px;
}
.box{
    filter: progid:DXImageTransform.Microsoft.Shadow(Color=#999999,Direction=120,strength=3);
	border: 1px solid #F3F3F3;
	text-indent: 8px;
	line-height: 22px;
	width: 150px;
}
 .tr{}
        .td{}
        .img{}
</style>
<script type="text/javascript" src="../JS/TW.js"></script>

<script src="js/NetworkViewNew.js" type="text/javascript"></script>





    
		
	</head>
	<body>
		<form id="form22" method="post" runat="server">
		<br />
			<table id="table2" cellspacing="1" cellpadding="0" width="100%" border="0">
				<tbody>
					<tr>
						<td>
							<table  cellSpacing="1" cellPadding="0" width="100%" border="0" class="tablemb">
								<tbody>
									<tr >
										<td style="height: 25px" colspan="2">
										    <asp:button  id="button1"  runat="server" CssClass="anyes"  text="显示" 
                                                onclick="button1_Click"></asp:button>&nbsp;&nbsp;&nbsp;
											<asp:label id="lbl_msg" runat="server"><%=GetTran("000045", "期数")%></asp:label><asp:dropdownlist id="dropdownlist_qishu" runat="server"></asp:dropdownlist>
											<%=GetTran("000024", "会员编号")%><asp:textbox id="txtbianhao" Width="85" MaxLength="10" runat="server"></asp:textbox>
											<asp:textbox id="txtceng" runat="server" width="24px" ></asp:textbox><%=GetTran("000845", "层")%>
											
                                            <asp:LinkButton ID="lkn_submit1" runat="server" style="display:none" Text="提交"
                                        onclick="lkn_submit1_Click"></asp:LinkButton>
                                        
											<asp:button id="button2" Visible="false"  runat="server" CssClass="anyes"  text="回到顶部" 
                                                onclick="button2_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<a style="display:none" href="javascript:window.history.back()"><u><%=GetTran("000421", "返回")%><%=GetTran("000846", "以下图中深红色代表已审核新增人员，淡红色代表未审核新增人员")%></u></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                
											    <asp:Button ID="btnCY" runat="server" Text="常用" CssClass="anyes" 
                                                    onclick="btnCY_Click" />
                                
									           <asp:Button ID="Button3" runat="server" Text="表格" CssClass="anyes" 
                                                    onclick="Button3_Click" />
                                                
                                                 <asp:Button ID="Button4" runat="server" Text="展开" CssClass="anyes" 
                                                onclick="Button4_Click"  />
                                                
                                                <asp:Button ID="Button8" runat="server" Text="伸缩" Enabled="false" 
                                                CssClass="anyes" onclick="Button8_Click" />
                                                <br />
										</td>
									</tr>
								</tbody>
							</table>
							<table  cellSpacing="0"  cellPadding="0"  width="100%" >
							    <tr>
							        <td>
							            <table  class="tablema">
							                <tr>
							                     <td>
                                                    <asp:Button ID="Button7" runat="server" Text="转到推荐" CssClass="anyes" 
                                                                    onclick="Button7_Click" /></td>
							                    <td> 可查看网络<asp:Repeater ID="Repeater2" Runat="server">
									                    <ItemTemplate>
										                    <b><%#DataBinder.Eval(Container.DataItem, "wlt")%></b>
									                    </ItemTemplate>
								                    </asp:Repeater></td>
							                </tr>
							            </table>
							       </td>
							    </tr>
							    <tr>
							        <td>
							            <div id="divDH" runat="server" class="STYLE1"></div>
							        </td>
							    </tr>
							    <tr>
									<td  nowrap>
									     <br />
										<div id="statr0" style="overflow: auto; width:100%;  text-indent: 11mm;  letter-spacing: 0em;"
											runat="server"></div>
										<br>
									</td>
								 </tr>
							</table>
						</td>
					</tr>
				</tbody>
			</table>
			
			
			<div id="aabb" align="center">
			</div>
		</form>
	</body>
</html>


