<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NetWorkDisplaySetting.aspx.cs"
    Inherits="Company_NetWorkDisplaySetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=GetTran("001231","查询控制") %></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>
    <script type="text/javascript">
    </script>
    <script type="text/javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>

<script type="text/javascript">
		    window.onerror=function()
		    {
		        return true;
		    };
		</script>
    <script type="text/javascript" language="javascript">
     function secBoard(n)
  {
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
    </script>

    <script type="text/javascript">
	   window.onload=function()
	    {
	        down2();
	    };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="80%" border="0" cellpadding="0" cellspacing="5" class="tablemb" align="center">
        <tr>
            <td>
                <%=GetTran("006913", "公司网络图显示内容")%> <asp:Label ID="LblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                   
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnsetup" runat="server" Text="确定" CssClass="anyes" OnClick="btnsetup_Click"
                    Style="cursor: hand"></asp:Button>
            </td>
        </tr>
        
        <tr>
            <td>
                <%=GetTran("006914", "店铺网络图显示内容")%><asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal">
                   
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="Button1" runat="server" Text="确定" CssClass="anyes"
                    Style="cursor: hand" onclick="Button1_Click"></asp:Button>
            </td>
        </tr>
        
        <tr>
            <td>
                <%=GetTran("006916", "会员网络图显示内容")%> <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatDirection="Horizontal">
                   
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="Button2" runat="server" Text="确定" CssClass="anyes"
                    Style="cursor: hand" onclick="Button2_Click"></asp:Button>
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width:100%">
         <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
        <tr>
          <td width="80">
          <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                                    <tr>
                                        <td class="sec2">
                                            <span id="sp" title='<%=GetTran("000033")%>'><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033"))%></span>
                                            
                                        </td>
                                    </tr>
                                </table></td>
          <td><img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></td>
        </tr>
      </table>
        <div id="divTab2">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
 
                <tbody style="display: block">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        1、<%=GetTran("001591", "设置公司系统、店铺系统、会员系统中的网络图中可显示的内容")%>。<br />
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
    <%=msg %>
</body>
</html>
