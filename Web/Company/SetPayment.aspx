<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPayment.aspx.cs" Inherits="Company_SetPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ProductColor</title>
    
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>    
</head>
<body >
    <form id="form1" runat="server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div>      
                    <br/><br/><br/> 
                    <b><%=GetTran("007954", "系统支付方式设置")%></b>
                    <br/> 
                                 
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablemb" >            
            <tr  style="display:none;"  >
                <td style="width:30%" align="left"><%=GetTran("007955", "服务机构系统使用支付方式")%>：</td></tr>
                <tr>
                <td align="left">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                    
                </td>
                <td style="width:30%">&nbsp;</td>
            </tr>
            <tr  style="display:none;">
                <td style="width:30%" align="right"><%=GetTran("007956", "服务机构“复消报单”支付方式")%>：</td>
                <td align="left">
                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                </td>
                <td style="width:30%">&nbsp;</td>
            </tr>
            <tr style="display:none;">
                <td style="width:30%" align="right"><%=GetTran("007957", "服务机构“在线订货”支付方")%>：</td>
                <td align="left">
                    <asp:CheckBoxList ID="CheckBoxList3" runat="server" 
                        RepeatDirection="Horizontal"></asp:CheckBoxList>
                </td>
                <td style="width:30%">&nbsp;</td>
            </tr>
            <tr style="display:none;">
                <td style="width:30%" align="right"><%=GetTran("007958", "服务机构“在线支付--充值”方式")%>：</td>
                <td align="left">
                    <asp:CheckBoxList ID="CheckBoxList4" runat="server" 
                        RepeatDirection="Horizontal"></asp:CheckBoxList>
                </td>
                <td style="width:30%">&nbsp;</td>
            </tr>
            <tr >
                <td style="width:30%" align="left"><%=GetTran("007959", "会员系统使用支付方式")%>：</td></tr>
                <tr>
                <td align="left"    >
                    <asp:CheckBoxList ID="CheckBoxList5" runat="server" 
                        RepeatDirection="Horizontal"  Width="700px"></asp:CheckBoxList>
                </td>
                <td style="width:30%">&nbsp;</td>
            </tr>
            <tr style="display:none;">
                <td style="width:30%" align="right"><%=GetTran("007960", "会员“复消报单”支付方式")%>：</td>
                <td align="left">
                    <asp:CheckBoxList ID="CheckBoxList6" runat="server" 
                        RepeatDirection="Horizontal"></asp:CheckBoxList>
                </td>
                <td style="width:30%">&nbsp;</td>
            </tr>
            <tr style="display:none;">
                <td style="width:30%" align="right"><%=GetTran("007961", "会员“在线支付--充值”方式")%>：</td>
                <td align="left">
                    <asp:CheckBoxList ID="CheckBoxList7" runat="server" 
                        RepeatDirection="Horizontal"></asp:CheckBoxList>
                </td>
                <td style="width:30%">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan=2>
                    <asp:Button ID="btnAdd" Text="确 定"  runat="server" onclick="btnAdd_Click" style="cursor:pointer;display:block" CssClass="anyes" />     
                </td>
                <td style="width:30%">&nbsp;</td>
            </tr>
        </table>
        <br/><br/><br/> 
        <b><%=GetTran("007962", "网银支付接口设置")%></b><br/> 
        <table   width="100%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
        <tr>
                <td><%=GetTran("007963","是否启用网上银行支付")%></td><td>
                    <asp:CheckBox ID="cbkwangyinqiyong" Text="启用" runat="server" /></td>
        </tr>
        
        <tr>
        <td><%=GetTran("007964", "网银支付直连接口")%>：</td>     <td align="left"> 
            <asp:RadioButtonList ID="rdowangyinlist"  RepeatDirection="Horizontal" runat="server">
            </asp:RadioButtonList>
            </td><td>
            <a href="SetParams/ConfigAlipay.aspx"> <%=GetTran("007965", "设置支付宝网关")%></a>  &nbsp;&nbsp;&nbsp;&nbsp;<a href="SetParams/ConfigsftPay.aspx"> <%=GetTran("007966", "设置盛付通网关")%></a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="SetParams/ConfigQuickPay.aspx"> <%=GetTran("007967", "设置快钱网关")%></a>
         
        </td>
        </tr>
        <tr>
        <td align="center" colspan=3>
                    <asp:Button ID="btnsetwangyin" Text="设置"  runat="server"   
                        style="cursor:pointer;display:block" CssClass="anyes" 
                        onclick="btnsetwangyin_Click" />     
                </td>
        
        </tr>
        </table>
        
        
        <br />          
    </div>                
            </td>
        </tr>
        <tr>
            <td>
                <div id="divProductColor" runat="server">
    </div>                
            </td>
        </tr>
        <tr>
            <td>
                <div id="cssrain">
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
          <td><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                  </td>
                </tr>
            </table> </td>
          </tr>
        </tbody>
        <tbody >
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><%=GetTran("005628", "支付方式管理")%>
               <!--   1.<%=GetTran("005629", "对会员和店铺的付款方式进行添加，修改，删除操作")%>-->
                    </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
            </td>
        </tr>
    </table>
    </form>
    <%=msg %>
</body>
</html>
