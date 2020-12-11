<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDeptModify.aspx.cs" Inherits="Company_CompanyDeptModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统部门修改</title>
<link href="CSS/Company.css" rel="stylesheet" type="text/css" />        
<style type="text/css">
            .style1
            {
                height: 19px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><br /><div  style="text-align:center;">
        <table background="#F8FBFD" class="tablemb"  width="400px">
                <tr>
                    <td><br />
                        &nbsp;</td>
                    <td align="left">
                        <h4><%=GetTran("000949", "添加部门")%></h4>&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="style1"align="right">
                        <%=GetTran("001020", "公司部门名")%>：</td>
                    <td class="style1" align="left">
						<asp:TextBox ID="txtDept" runat="server"  MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td align="left"><br />
            <asp:Button ID="btnModify" runat="server" onclick="BtnModify_Click" Text="修 改" CssClass="anyes" />
            <INPUT onclick="javascript: window.location='CompanyDeptManage.aspx'" type="button" value='<%=GetTran("000421", "返回")%>' class="anyes">
                    </td>
                </tr>
            </table>
        </div>
      <br /></td>
  </tr>
</table>

   
    </div>
    </form>
</body>
</html>

