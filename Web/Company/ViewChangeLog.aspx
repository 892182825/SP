<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewChangeLog.aspx.cs" Inherits="Company_ViewChangeLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViewChangeLog</title>

    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>

    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>

    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />

    <script language="javascript" type="text/javascript">
    function cutDescription()
    {
         document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
    }
    
    window.onload=function()
    {
        down2();               
    };
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
        <tr style="white-space: nowrap">
            <td style="white-space: nowrap">
                <span id="spanConten" runat="server" style="white-space: nowrap"></span>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <input type="button" class="anyes" value='<%=GetTran("000096", "返 回")%>' onclick="javascript:history.back()" />
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="80">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(1)">
                                <span id="span2" title="" onmouseover="cutDescription()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#">
                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down3()" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2" style="width: 100%">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody1">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <%=GetTran("004103", "日志详细信息")%><br />
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
