<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberNetMapFrame.aspx.cs" Inherits="Member_MemberNetMapFrame" %>

<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>常用图</title>
     <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" style="overflow:hidden">

        <uc1:top runat="server" ID="top" />
         <div class="rightArea clearfix" style="margin-left:430px;margin-top :40px">
        <div class="MemberPage">

            <iframe src='MemberNetMap.aspx?isAnzhi=<%=Request.QueryString["isAnzhi"] %>' width="1003" height="800" frameborder="0" style="margin-left:-250px"></iframe>



        </div>
             </div>
        <uc2:bottom runat="server" ID="bottom" />
    </form>
</body>
</html>
