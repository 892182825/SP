<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceBegin.aspx.cs" Inherits="Company_BalanceBegin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    		<script language="javascript">
		function bClick( bID){
			bID.value='<%=GetTran("001189", "请稍候......")%>';
		}
		function JJYFPrompt()
		{
			var sure = window.confirm('<%=GetTran("001191", "该期以后的奖金已经发放！确定要重新结算吗？（此操作可能导致该期以后的奖金出现负值）")%>');
			if (sure)
			{
				__doPostBack('lkbtn_JJYFTrue','');
			}
		}
		function JJGotPrompt()
		{
			var sure = window.confirm('<%=GetTran("001192", "该期以后的奖金已经添加！确定要撤消重新结算吗？")%>');
			if (sure)
			{
				__doPostBack('lkbtn_JJGotTrue','');
			}
		}
		</script>
		    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>

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
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><br /><div>
 <table   width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
				<tr>
					<td align="center">
						<table>
							<tr>
								<td>
									<asp:CheckBoxList id="CheckJie" runat="server" RepeatDirection="Horizontal" Runat="server">
										<asp:ListItem Value="2">保存计算差异</asp:ListItem>
										<asp:ListItem Value="0" Selected="True">周结算</asp:ListItem>
										<asp:ListItem Value="1">月结算</asp:ListItem>
										
									</asp:CheckBoxList>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="30">
					<td style="HEIGHT: 37px" align="center">
						<P>&nbsp;<asp:button id="beginCount" Width="152px" Text="开始结算" Runat="server" 
                                Height="22px"
								
                                 onclick="beginCount_Click"></asp:button>
							<asp:button id="reCount" Width="152px" Text="停止程序" Runat="server" Height="22px" 
								
                                onclick="reCount_Click"></asp:button>
                            <asp:linkbutton id="lkbtn_JJYFTrue"  runat="server" 
                                onclick="lkbtn_JJYFTrue_Click" style="DISPLAY: none" ForeColor="White">已发</asp:linkbutton>
                            <asp:linkbutton id="lkbtn_JJGotTrue"  runat="server" 
                                onclick="lkbtn_JJGotTrue_Click" style="DISPLAY: none" ForeColor="White">已添加</asp:linkbutton></P>
						<table class="biaozzi">
							<tr height="30" >
								<td style="HEIGHT: 28px">
									 &nbsp;<%=GetTran("001193", "特别提醒")%>：
												<BR>
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										<%=GetTran("001196", "在弹出写有“程序运行完毕......”字样的新窗口")%>&nbsp;
										<BR>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("001197", "之前，请不要关闭该系统中的任何窗口。")%>
										
									</P>
									<FONT>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT>
									<asp:label id="messageLabel" Runat="server" Font-Bold="True"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="40">
					<td vAlign="bottom" align="center">
                        <asp:button id="Button1" runat="server" 
                            Width="102px" Text="关闭窗口" Height="22px" 
							
                           onclick="Button1_Click"></asp:button>&nbsp;
					</td>
				</tr>
			</table>
			</div>
			</td>
			</tr>
			</table>
    </form>
</body>
</html>
