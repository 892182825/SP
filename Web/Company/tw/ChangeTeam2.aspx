<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeTeam2.aspx.cs" Inherits="Company_tw_ChangeTeam2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../../JS/QCDS2010.js" type="text/javascript"></script>
 
    <script language="javascript" type="text/javascript">
        var aa = [
            '<%=GetTran("000032", "管 理") %>'

        ];
    </script>
    <script src="../js/ChangeTeam2.js" type="text/javascript"></script>
</head>
<body  onload="down2()">
    <form id="form1" runat="server">
    <br />
    <br />
    <div align="center">
    <table width="600" border="0" align="center" cellpadding="0" cellspacing="1" class="tablett">
        <tr>
            <th colspan="5">
                <%=GetTran("005018", "网络调整")%>
            </th>
        </tr>
        <tr>
            <td align="right" bgcolor="EBF1F1">
                <asp:Label ID="Label2" runat="server"><%=GetTran("000729", "请输入要调网的编号")%>：</asp:Label>
                </td>
                <td colspan=4 align=left bgcolor="#F8FBFD">
                <asp:TextBox ID="txtbh" runat="server" Width="100px" ToolTip="调整编号" 
                    MaxLength="100"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="提 交" OnClick="Button1_Click" CssClass="anyes"></asp:Button>
            </td>
        </tr>
        <tr>
        <td bgcolor="EBF1F1" align="right"><asp:Label ID="lblwz" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label></td>
            <td colspan="4" height="20px"  bgcolor="#F8FBFD" align=left>
                <asp:Label ID="lblbh" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="EBF1F1"><%=GetTran("000659", "原位置")%>：</td>
            <td align="right" bgcolor="#F8FBFD">
                <%=GetTran("000043", "推荐人编号")%>：
                <br /><%=GetTran("000192", "推荐人姓名")%>： 
            </td>
            <td align="left" width="6%" bgcolor="#F8FBFD">
                <asp:Label ID="lbltuijian" runat="server" Width="72px"></asp:Label>
            </td>
            <%--<td align="right" bgcolor="#F8FBFD">
                <%=GetTran("000706", "安置人编号")%>：
                <br /><%=GetTran("000097", "安置人姓名")%>：
            </td>
            <td align="left" bgcolor="#F8FBFD">
                <asp:Label ID="lblanzhi" runat="server" Width="72px"></asp:Label>
            </td    >--%>
        </tr>
        <tr>
            <td align="right" bgcolor="EBF1F1">
                <%=GetTran("000660", "新位置")%>：
            </td>
            <td align="right" bgcolor="#F8FBFD">
                <%=GetTran("000043", "推荐人编号")%>：
                <br /><%=GetTran("000192", "推荐人姓名")%>：
          </td>
          <td align="left" bgcolor="#F8FBFD">
                <asp:TextBox ID="txttuijian" runat="server" onblur="GetName1(this.value)" Width="100px" MaxLength="100"></asp:TextBox>
                <div style="margin-top:5px;width:100px">
                    <asp:Label ID="Label1" runat="server"  Text=""></asp:Label>&nbsp;
                </div>
            </td>
            <%--<td  align="right" bgcolor="#F8FBFD">
                <%=GetTran("000706", "安置人编号")%>：
                <br /><%=GetTran("000097", "安置人姓名")%>：
            </td>
            <td align="left" bgcolor="#F8FBFD">
                <asp:TextBox ID="txtanzhi" runat="server" onblur="GetName2(this.value)" Width="100px" MaxLength="10"></asp:TextBox>
                <div style="margin-top:5px;width:100px"><asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    &nbsp;
                </div>
            </td>--%>
        </tr>
        <tr style=" display:none;">
            <td align="right" bgcolor="EBF1F1">
                区位：
            </td>
            <td align="left" bgcolor="#F8FBFD" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">左区</asp:ListItem>
                    <asp:ListItem Value="2">右区</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td bgcolor="EBF1F1"></td>
            <td colspan="4" height="20px"  bgcolor="#F8FBFD" align="left">
                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
        <td bgcolor="EBF1F1"></td>
            <td colspan="4"  bgcolor="#F8FBFD" align="left">            
                <asp:Button ID="btn_re" runat="server" Text="开始调整" 
                    OnClick="btn_re_Click"  CssClass="another">
                </asp:Button>
            &nbsp;
                <asp:Button ID="btn_fh" runat="server" Text="返 回"
                    OnClick="btn_fh_Click"  CssClass="another">
                </asp:Button>
            </td>
        </tr>
    </table>
    </div><br />
	<div id="cssrain" style="width:100%">
      <table width="99%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
        <tr>
          <td width="80">
          <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
           <tr>
               <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                
             </tr>
          </table></td>
          <td><a href="#"><img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block">
          <tr>
            <td  style="padding-left:20px">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>１、<%=GetTran("006839", "输入要调网的会员编号，修改推荐或安置编号就能完成，一个任意大的网络的整体移动及相关处理。")%>。
                  </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
   
    </form>
</body>
</html>
