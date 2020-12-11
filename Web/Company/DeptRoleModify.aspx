<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptRoleModify.aspx.cs" Inherits="Company_DeptRoleModify" %>

<%@ Register src="../UserControl/UCPermission.ascx" tagname="UCPermission" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改角色</title>
<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>
<script src="js/tianfeng.js" type="text/javascript"></script>

</head>

<body onload="down2()">
<form id="form1" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="biaozzi">
  <tr>
    <td valign="top"><br /><div>
        <table class="tablemb" width="99%">
            <tr>
                <td align="right">
                    &nbsp;
                    <%=GetTran("000992", "角色名称")%>：</td>
                <td >
                    &nbsp;
                    <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td  align="right">
                    &nbsp;
                   <%=GetTran("000331", "部门")%>：</td>
                <td >
                    &nbsp;
                    <asp:DropDownList ID="ddlDepts" runat="server" ondatabound="ddlDepts_DataBound">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">                    
            <uc1:UCPermission ID="UCPermission1" runat="server" />
                </td>
            </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td><asp:Button ID="btnUpt" runat="server" Text="修改" style="cursor:hand;" 
                        onclick="btnUpt_Click" CssClass="anyes" />
                    <INPUT onclick="javascript: window.location='DeptRolesManage.aspx'" type="button" style="cursor:hand;"  value='<%=GetTran("000421", "返回")%>'  class="anyes"></td>
                    <td>
                        &nbsp;</td>
                </tr>
        </table></div>
      <br /></td>
  </tr>
 
</table>
    </form>
</body>
</html>
