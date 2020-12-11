<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigAlipay.aspx.cs" Inherits="Company_SetParams_ConfigAlipay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GetTran("006676", "快钱人民币网关设置")%></title>
    <base target="_self" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <link href="../CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../js/tianfeng.js" type="text/javascript"></script>

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

    <style type="text/css">
        table
        {
            font-size: 9pt;
        }
        .tb
        {
            font-size: 9pt;
            border-left: solid 1px lightblue;
            border-top: solid 1px lightblue;
        }
        .tb td
        {
            border-bottom: solid 1px lightblue;
            border-right: solid 1px lightblue;
        }
        .tdh
        {
            text-align: center;
            width: 100%;
            font-size: 11pt;
        }
        .tdL
        {
            text-align: right;
            width: 40%;
            float: left;
            height: 22px;
            margin-top: 5px;
        }
        .tdC
        {
            text-align: left;
            float: left;
            height: 22px;
        }
        .tdR
        {
            text-align: left;
            float: left;
        }
        .style1
        {
            text-align: center;
            width: 100%;
            font-size: 11pt;
            height: 24px;
        }
        .style2
        {
            color: #FF0066;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; width: 100%">
        <br />
        <table border="0" width="98%" class="tb" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style1" style="background-image: url(../images/tabledp.gif); color: White;
                    height: 25px;">
                    <%=GetTran("007050", "支付宝设置")%>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tdL">
                        <span class="style2">*</span><span title='支付宝账号'><%=GetTran("001439", "支付宝账号")%></span>
                        ：
                    </div>
                    <div class="tdC">
                        <asp:TextBox ID="txtCard" runat="server" Width="450px" MaxLength="50"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tdL">
                        <span title='合作伙伴ID'>
                            <%=GetTran("007051", "合作伙伴ID")%></span>：</div>
                    <div class="tdC">
                        <asp:TextBox ID="txtId" runat="server" Width="450px" MaxLength="50" TextMode="SingleLine"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tdL">
                        <span title='交易安全校验码'>
                            <%=GetTran("007052", "交易安全校验码")%></span>：</div>
                    <div class="tdC">
                        <asp:TextBox ID="txtKey" runat="server"  Width="450px" MaxLength="100" TextMode="Password"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tdL">
                        <span title='重定下地址'>
                            <%=GetTran("007053", "重定下地址")%></span>：</div>
                    <div class="tdC">
                        <asp:TextBox ID="txtReturnUrl" runat="server" Width="450px" MaxLength="150" TextMode="SingleLine"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tdL">
                        <span title='服务器通知地址'>
                            <%=GetTran("007054", "服务器通知地址")%></span>：</div>
                    <div class="tdC">
                        <asp:TextBox ID="txtPostUrl" runat="server" Width="450px" MaxLength="150" TextMode="SingleLine"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="anyes" OnClick="btnSubmit_Click"
                        Style="cursor: pointer" Text="提交" />
                    &nbsp;
                    <input id="btnBack" class="anyes" onclick="javascript:window.location.href='../SetParameters.aspx';"
                        type="button" value='<%=GetTran("000421","返回") %>' />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
            <tr>
                <td width="80px">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(1)">
                                <span id="span2" title="" onmouseover="cut1()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#">
                        <img src="../images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            align="middle" onclick="down2()" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2" style="display: none;">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        1.<%=GetTran("006679", "快钱网关设置")%><br />
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
</html>
