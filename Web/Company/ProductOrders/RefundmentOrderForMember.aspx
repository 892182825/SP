<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefundmentOrderForMember.aspx.cs" Inherits="Company_ProductOrders_RefundmentOrderForMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退货申请协议书</title>
    <link href="../CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../../JS/QCDS2010.js" type="text/javascript"></script>

<style type="text/css" >
.frameclass{width:800px;height:500px;overflow-x:auto; overflow-y:scroll;word-break:break-all;
 border-left:solid 1px #c6d6fd;
 border-top:solid 1px #c6d6fd;
 border-right:solid 1px #c6d6fd;
 border-bottom:solid 1px #c6d6fd;
 text-align:left;
 }
</style>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div style="height: auto; width: 100%; overflow: hidden;">
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#F8FBFD"
            class="biaozzi">
            <tr>
                <td>
                <asp:Panel ID="pnl_NumberConfirm"  Visible="true" runat="server" >
                 <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                        class="biaozzi">
                        <tr>
                            <td colspan="4" align="center" nowrap="nowrap" bgcolor="#EBF1F1" style="font-size:9pt;
                            padding-top:8px;padding-bottom:5px;">     
                            <table border="0px" cellpadding="0" cellspacing=0>
                            <tr>
                            <td><%=GetTran("000024", "会员编号")%>：</td>
                            <td><asp:TextBox ID="txt_MemberID" runat ="server" Width="100px" 
                                    AutoPostBack="True" ontextchanged="txt_MemberID_TextChanged" ></asp:TextBox></td>
                            <td> <asp:Label ID="lbl_MemberName" runat="server" ></asp:Label>
                                     </td>
                            </tr>
                                <tr>
                                    <td colspan="3"  style="text-align:right;">
                                       &nbsp;<asp:Button ID="btn_Confirm" runat="server" Text="确认" 
                                    CssClass="another" onclick="btn_Confirm_Click" />&nbsp;
                                     <input id="btnBack" value='<%=GetTran("000421", "返回") %>' class="another" type="button" onclick="javascript:window.location.href='RefundmentOrderForMemberList.aspx';"/> &nbsp;</td>
                                </tr>
                            </table>                         
                                
                                
                                     
                            </td>
                        </tr>
                        </table>
                  </asp:Panel>
                <asp:Panel ID="pnl_Argeement" Visible="false" runat="server" >
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                        class="biaozzi">
                        <tr>
                            <td align="center" nowrap="nowrap" bgcolor="#EBF1F1" style="font-size:11pt;font-weight:bold;
                            padding-top:8px;padding-bottom:5px;">                              
                                <%=GetTran("007736", "退货协议书")%></td>
                        </tr>
                        <tr>
                        <td align="center" >
                        <div id="divAgreement" class="frameclass">
                        
                          </div>
                        </td>
                        </tr>
                        <tr>
                            <td align="center" >                               
                                         <asp:Button ID="btn_Cancel" runat="server" CssClass="another" 
                                             onclick="btn_Cancel_Click" Text="不同意" />
                                         &nbsp;<asp:Button ID="btn_ArgeeConfirm" runat="server" Text="同意并确认" 
                                             CssClass="another" onclick="btn_ArgeeConfirm_Click" />
                                     
                            </td>
                        </tr>
                    </table>
              </asp:Panel>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
