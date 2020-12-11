<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinanceStat.aspx.cs" Inherits="Company_FinanceStat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
        <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
        <script src="../JS/QCDS2010.js" type="text/javascript"></script>
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
<SCRIPT language="javascript">

  function typechange()
  {
    if(document.getElementById("DropDownList1").value==0)
    {
        document.getElementById("typeobj").style.display="block";
    }else
    {
        document.getElementById("typeobj").style.display="none";
    }
  }
</SCRIPT>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td>
					<table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi"><tr>
						<td align="center">
                                   
									<asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">Label</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;<asp:hyperlink id="GO_Back" runat="server" Target="_self" NavigateUrl="FinanceStat.aspx"><%=GetTran("001317", "返回走势图")%></asp:hyperlink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
						</tr></table>
						<table border="0" cellpadding="0" cellspacing="0" class="biaozzi">
						
							<TR id="td1" runat="server">
								<td height="48"  >
                                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="anyes" runat="server"
                                        onclick="Button1_Click" />
                                         &nbsp;&nbsp;
                            <asp:DropDownList ID="DropQiShu1" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropQiShu2" runat="server">
                            </asp:DropDownList>
                                        </td><td>
                                    &nbsp;<%=GetTran("001316", "请选择拨出率类型")%>：<asp:DropDownList ID="DropDownList1" runat="server" onchange="typechange()"
                                        >
                                        <asp:ListItem Value="0">金额</asp:ListItem>
                                        <asp:ListItem Value="1">PV</asp:ListItem>
                                    </asp:DropDownList>
                                    </td><td style="display:none">
								<span id="typeobj"><asp:Label id="Label2" runat="server">&nbsp;<%=GetTran("001315", "请选择币种")%>:</asp:Label>&nbsp;
									<asp:dropdownlist id="DropCurrency" runat="server"></asp:dropdownlist></span></td>
                                    
							</TR>
						</table>
						<table width="100%" border="0" cellpadding="0" cellspacing="0"  class="tablemb">
							<tr>
								<td vAlign="top" style="word-break:keep-all;word-wrap:normal;"><div id="griddiv" ><asp:table id="Table1"  runat="server" border="1" width="100%" cellPadding="0"
										cellSpacing="0"></asp:table></div></td>
							</tr>
						</table>
					</td>
				</tr>
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
                                    <td><%=GetTran("006874", "1、显示公司每次报单结算周期中，公司（总金额、总业绩）的总收入，总支出，拨出率。")%>
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
</html>
