<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GraphNetFrame.aspx.cs" Inherits="Member_GraphNetFrame" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1"  %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>常用图</title>
</head>
<body>
    <div class="MemberPage">
    
    <form id="form1" runat="server">
    <uc1:top runat="server" ID="top" />
        <iframe src='GraphNet.aspx' width="1003" height="760" frameborder="0">
        </iframe>

    </form>
    <uc2:bottom runat="server" ID="bottom" />
    </div>
</body>
</html>
