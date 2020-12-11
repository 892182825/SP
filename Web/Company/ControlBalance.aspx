<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ControlBalance.aspx.cs" Inherits="Company_ControlBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调控结算</title>
                <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript">
	window.onerror=function()
    {
        return true;
    };
    window.onload=function()
	{
	    down2();
	};
    </script>
    <script language="javascript">
			var newEditWin=0;
			function openCountWin(qs) 
			{
			
				var param='toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=no,width=500,height=400,left=300, top=200,screenX=100,screenY=0';
			
				if( newEditWin ){
						if( !newEditWin.closed ){
							window.alert('<%=GetTran("001129", "您已经打开了一个计算窗口！\n请关闭窗口在再选择结算其他期！")%>');
							newEditWin.focus();
		        			return;
		        			}
		 		}
				var sure = window.confirm('<%=GetTran("001130", "您确定要结算")%>'+'<%=GetTran("000156", "第")%>'+qs+'<%=GetTran("001131", "期奖金？？？")%>');
				if(sure){
				//window.open("BalanceBegin.aspx?action=begin&qs=" + qs,'nW', param);
					newEditWin = window.open("BalanceBegin.aspx?action=begin&qs=" + qs,'nW', param);
					newEditWin.focus();
				}
												
			}
			function confimAddNew()
			{
				if(window.confirm('<%=GetTran("001134", "您确定要创建新一期？？？")%>')) return true;
				else return false;
			
			
			}	
		</script>
		        <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

		</head>
<body>
    <form id="form1" runat="server">
    <br />
<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td>
						<table  width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
							<tr>
								<td height="48">
									<%=GetTran("001135", "显示从")%>&nbsp;
									<asp:TextBox id="TextBox1" runat="server" Width="64px"></asp:TextBox>&nbsp;<%=GetTran("001136", "期至")%>&nbsp;
									<asp:TextBox id="TextBox2" runat="server" Width="64px"></asp:TextBox>&nbsp;<%=GetTran("001137", "期的结算列表")%>&nbsp;
									<asp:Label id="Label1" runat="server"></asp:Label>
									<asp:Button id="Button1" runat="server" Text="确 定"
										
                                        onclick="Button1_Click" CssClass="anyes"></asp:Button></td>
							</tr>
						</table>
						<table id="__01"  width="100%" border="0" cellpadding="0" cellspacing="0" >
							<tr>
								<td valign="top">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                        Width="100%" onrowdatabound="GridView1_RowDataBound" CssClass="tablemb">
                                        <RowStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tablebt"/>
                                        <AlternatingRowStyle BackColor="#F1F4F8" />
                                        <Columns>
                                            <asp:BoundField HeaderText="创建日期" DataField="Date" DataFormatString="{0:d}"/>
                                            <asp:BoundField HeaderText="结算期数" DataField="ExpectNum"/>
                                            <asp:TemplateField HeaderText="操作">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink id="HyperJieSuan" runat="server" Visible="False"><%#GetTran("001138", "结算")%></asp:HyperLink>
													<DIV id="jiesuan" runat="server"></DIV>
													<INPUT id=HidQishu type=hidden value='<%# DataBinder.Eval(Container, "DataItem.ExpectNum")%>' name=HidQishu runat="server">
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                       
                                    </asp:GridView>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<BR>
			<table  width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="DISPLAY: none">
				<tr>
					<td align="center" style="HEIGHT: 55px">
						显示从 期至 期的结算列表
						<br>
					</td>
				</tr>
				<TR align="center">
					<TD>
						<DIV style="DISPLAY: inline; WIDTH: 235px; HEIGHT: 15px" ms_positioning="FlowLayout"></DIV>
						<table>
							<tr>
								<td>
									<asp:table id="qishuTable" ForeColor="red" Runat="server" BorderWidth="0px" GridLines="Both"
										BorderStyle="Solid" HorizontalAlign="Center" Width="200px"></asp:table>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
			 <div id="cssrain" style="width:100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="80px">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2">
                                <span id="span1" title="" onmouseover="cutDescription()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()" /></a></td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody1">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><%=GetTran("006872", "1、根据调控的参数进行奖金调控结算。")%></td>
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
</html>
