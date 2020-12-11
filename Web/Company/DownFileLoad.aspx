<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownFileLoad.aspx.cs" Inherits="Company_DownFileLoad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
     <base target="_self">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <table align="center"><tr><td>
        <asp:FileUpload ID="PhotoPath" runat="server" /></td></tr>
        <tr><td align="center">
            <asp:Button ID="Button1" runat="server" Text="确 定" onclick="Button1_Click" CssClass="anyes"/></td></tr>
        </table>
    </div>
    </form>
</body>
</html>
