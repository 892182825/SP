<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigsftPay.aspx.cs" Inherits="ConfigsftPay" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GetTran("000000", "盛付通网关设置")%></title>
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
                   <%=GetTran("007928", "盛付通网关设置")%>
                </td>
            </tr>
             <tr>
                <td>
                    <div class="tdL">
                        <span class="style2">*</span> <%=GetTran("007929", "盛大通行证帐号")%>
                        ：
                    </div>
                    <div class="tdC">
                        <asp:TextBox ID="txtaccount" runat="server" Width="146px" MaxLength="20"></asp:TextBox>
                        <asp:Label runat="server" ID="Label1" ForeColor="#FF0066"></asp:Label></div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tdL">
                        <span class="style2">*</span> <%=GetTran("007931", "盛付通网关商户号")%>
                        ：
                    </div>
                    <div class="tdC">
                        <asp:TextBox ID="txtAcctId" runat="server" Width="146px" MaxLength="20"></asp:TextBox>
                        <asp:Label runat="server" ID="lblIDFlag" ForeColor="#FF0066"></asp:Label></div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="tdL">
                      <span class="style2">*</span>  <%=GetTran("007932", "盛付通网关密钥")%>：</div>
                    <div class="tdC">
                        <asp:TextBox ID="txtKey" runat="server" Width="145px"  TextMode="Password"></asp:TextBox>
                        <asp:Label runat="server" ID="lblKeyFlag" ForeColor="#FF0066"></asp:Label></div>
                </td>
            </tr>
             <tr>
                <td>
                    <div class="tdL">
                        <span title='服务器通知地址'>
                            <%=GetTran("007054", "服务器通知地址")%></span>：</div>
                    <div class="tdC">
                        <asp:TextBox ID="txtBgUrl" runat="server" Width="145px" MaxLength="100" ></asp:TextBox>
                        <asp:Label runat="server" ID="lblBgUrl" ForeColor="#FF0066"></asp:Label></div>
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
