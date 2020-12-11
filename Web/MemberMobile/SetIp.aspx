<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetIp.aspx.cs" Inherits="Member_SetIp" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>时区设置</title>
    <link href="CSS/Member.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function a()
        {
            window.parent.frames["topFrame"].location.href=window.parent.frames["topFrame"].location.href.replace("#","");
	                        window.parent.frames["leftmenu"].location.href=window.parent.frames["leftmenu"].location.href;
	                        window.parent.frames["mainframe"].location.href=window.parent.frames["mainframe"].location.href;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="10" align="center"	border="0" width="400" style="WIDTH: 400px; HEIGHT: 216px" class="biaozzi">
				<tr>
					<td align="left">
						<table border="0" cellpadding="1" cellspacing="0" style="width: 531px;" class="tablemb">
							<tr align="center">
							    <td>请选择时区</td>
							</tr>
							<tr align="center">
							    <td>
							        <asp:RadioButtonList ID="ip" runat="server" RepeatDirection="Horizontal">
							            <asp:ListItem Value="0">每次登录地所属地区</asp:ListItem>
							            <asp:ListItem Value="1">首次注册所属地区</asp:ListItem>
							        </asp:RadioButtonList>
							    </td>
							</tr>
							<tr align="center">
							    <td>
							        <asp:Button ID="btnOk" runat="server" Text="确 定" CssClass="anyes" 
                                        onclick="btnOk_Click" />
							    </td>
							</tr>
						</table>
                               
					</td>
				</tr>
			</table>
    </form>
</body>
</html>
