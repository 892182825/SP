<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceRunning_Proc.aspx.cs" Inherits="Company_BalanceRunning_Proc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var xmlHttp;

        function createXMLHttpRequest() {
            if (window.ActiveXObject)
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            else if (window.XMLHttpRequest)
                xmlHttp = new XMLHttpRequest();

        }

        function setJS(id, qs, jstype) {
            if (xmlHttp == null)
                createXMLHttpRequest();

            xmlHttp.open("get", "BalanceRunning_Proc.aspx?action=ajax&id=" + id + "&qs=" + qs + "&jstype=" + jstype + "&date=" + new Date().getTime(), true);
            xmlHttp.onreadystatechange = stateChange;

            xmlHttp.send(null);
        }

        function stateChange() {
            if (xmlHttp.readyState == 4) {
                if (xmlHttp.status == 200) {
                    var err = xmlHttp.responseText;

                    if (err == "0")
                        alert("结算成功，程序运行完毕！");
                    else
                        alert("结算失败！" + err);

                    window.parent.location.href = window.parent.location.href;
                }
            }
        }

        function butClick() {
            document.getElementById("div1").style.display = "none";
            document.getElementById("div2").style.display = "";

            var id = '<%=Request.QueryString["id"] %>';
            var qs = '<%=Request.QueryString["qs"] %>';
            var jstype = "0";
            //if (document.getElementById("RadioButtonList1_1").checked)
            //    jstype = "0";
            if (document.getElementById("RadioButtonList1_2").checked)
                jstype = "1";

            setJS(id, qs, jstype);
        }


        function pgLoad(showdivid) {
            document.getElementById("div1").style.display = "none";
            document.getElementById("div2").style.display = "none";

            document.getElementById(showdivid).style.display = "";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div1">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                <tr>
                    <td align="center">
                        <%=GetTran("", "特别提醒")%>：
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <%=GetTran("", "在弹出写有“程序运行完毕......”字样的新")%>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <%=GetTran("", "窗口之前，请不要关闭该系统中的任何窗口。")%>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <%=GetTran("", "如果结算时间明显超时，说明系统出了问题，")%>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <%=GetTran("", "这时，可以关闭任何窗口，并联系系统维护人")%>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <%=GetTran("", "员。明显超时：远远超过上一次的结算时间。")%>
                    </td>
                </tr>

                <tr>
                    <td align="center">
                        <input id="RadioButtonList1_2" type="checkbox" name="RadioButtonList2" value="1"  checked="checked" /><label for="RadioButtonList1_2">月结</label>
                    </td>
                </tr>

                <tr>
                    <td align="center">
                        <br>
                        <input type="button" value="开始结算" class="anyes" onclick="butClick()">
                    </td>
                </tr>
            </table>
        </div>

        <div id="div2" style="display: none;">
            <div align='center' style='width: 100%; font-size: 10pt; padding-top: 60px;'>
                结算程序正在运行...<br>
                <br>
                <img src='images/ajax-loader.gif'><br />
                <br>
                <asp:Button ID="Button1" runat="server" Text="停止结算" Class='anyes'
                    OnClick="Button1_Click1" />
            </div>
        </div>
    </form>
</body>
</html>
