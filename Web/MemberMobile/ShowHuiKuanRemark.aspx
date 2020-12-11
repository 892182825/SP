<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowHuiKuanRemark.aspx.cs" Inherits="Member_ShowHuiKuanRemark" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=GetTran("001231","无标题页") %></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/detail.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="MemberPage">
    <form id="form1" runat="server">
    <div class="centerCon-1" style="height:100%">
    <div class="ctConPgList-1" style="position:absolute;top:50%;margin-top:-170px;height:290px;width:700px;left:50%;margin-left:-350px;">
      <table width="696px" style="height:100%;margin:0 auto" border="0" cellspacing="1" cellpadding="0">
	      <tr class="ctConPgTab">
	        <td><asp:Label ID="lb" Runat="server"></asp:Label></td>
	      </tr>
      </table>
    </div>
    </div>
    </form>
</div>
</body>
</html>

