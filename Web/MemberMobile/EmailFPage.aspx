<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailFPage.aspx.cs" Inherits="Member_EmailFPage" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/detail.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        window.onload = function() {
            //parent.document.all("Email_Left").style.height=document.body.scrollHeight; 
            //parent.document.all("Email_Right").style.height=document.body.scrollHeight;

            var herf = document.location.href;
            var ids = new Array();
            ids = herf.split('=');
            if (ids.length > 1) {
                var url =<%= returnUrl() %>;
                if (url != "1" && url != "") {
                    parent.document.all("Email_Right").src = url;
                }
                parent.document.all("Email_Right").src = "MessageContent.aspx?id=" + ids[1].toString() + "&T=MessageReceive&source=ReceiveEmail.aspx";
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        <!--
        function setTab(name, cursel, n, clsname) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? clsname : "";
                con.style.display = i == cursel ? "block" : "none";
            }
        }
        //-->
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="MemberPage" >
        <Uc1:MemberTop ID="MTop" runat="server" />
        <table style="width: 1220px; vertical-align: top;margin:0 auto" border="1" cellpadding="0" cellspacing="0" height="1000">
            <tr>
                <td style="width: 170px;" valign="top">
                    <iframe src="EmialMenu.aspx" id="Email_Left" scrolling="no" frameborder="0" style="width: 100%; vertical-align: top;"></iframe>
                </td>
                <td style="width: 830px; vertical-align: top;">
                    <div style="color: #FFF;">
                        <iframe src="ReceiveEmail.aspx" id="Email_Right" scrolling="auto" frameborder="0"
                            width="100%" height="700" style="vertical-align: top;"></iframe>
                    </div>
                </td>
            </tr>
        </table>
        <Uc1:MemberBottom ID="Bottom" runat="server" />
    </div>
    </form>
</body>
</html>
