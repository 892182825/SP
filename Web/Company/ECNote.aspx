<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ECNote.aspx.cs" Inherits="Company_ECNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("005635", "收款确认备注查看")%></title>
    
<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
    <tr align="center">
    <th><%=GetTran("000744", "查看备注")%></th>
    </tr>
    <tr align="center">
    <td> 
        <div id="divnode" runat="server">
        
        </div>
    </td>
    </tr>
    <tr align="center">
        <td>
        <a onclick="javascript:window.close()" style="cursor:hand"><%=GetTran("004156", "关闭")%></a>
        </td>
    </tr>
   </table>
    </form>
</body>
</html>
