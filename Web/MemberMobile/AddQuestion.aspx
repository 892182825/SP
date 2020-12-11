<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuestion.aspx.cs" Inherits="Store_AddQuestion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link type="text/css" rel="Stylesheet" href="Css/member.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:90%;" class="biaozzi">
        <br />
        <asp:TextBox ID="txtQuestion" runat="server" Height="150px" 
            TextMode="MultiLine" Width="600px" Font-Size="12px"></asp:TextBox>
            <br />
            <asp:Button ID="btnAdd" runat="server" Text="提交" CssClass="anyes"  onclick="btnAdd_Click" />
            &nbsp;&nbsp;<input type="button" class="anyes" value='<%=GetTran("000421", "返回")%>' onclick="javascript:history.back(1);" />
        </div>
        &nbsp;<%=msg %>
    </form>
</body>
</html>
