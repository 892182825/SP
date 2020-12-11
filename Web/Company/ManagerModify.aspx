<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerModify.aspx.cs" Inherits="Company_ManagerModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML xmlns="http://www.w3.org/1999/xhtml">
  <HEAD>
		<title>创建新管理员</title>
		<link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script src="../JS/QCDS2010.js" type="text/javascript"></script>

      <script src="js/tianfeng.js" type="text/javascript"></script>

</SCRIPT>
        <style type="text/css">
            .style1
            {
                width: 50%;
            }
            .style2
            {
                width: 50%;
            }
        </style>
  </HEAD>
	<body >
		<form id="Form1" runat="server">
		<br />
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="tablemb">
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style1">
                       <h4> <%=GetTran("001091", "修改管理员")%></h4></td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("001066", "管理员编号")%>：</td>
                    <td class="style1">
												<asp:TextBox ID="txtNumber" runat="server" MaxLength="10"></asp:TextBox>
                                            </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                       <%=GetTran("000107", "姓名")%>：</td>
                    <td class="style1">
                    <asp:TextBox ID="txtName" runat="server" MaxLength="10"></asp:TextBox>
                                            </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                       <%=GetTran("000331", "部门")%>：</td>
                    <td class="style1">
                    <asp:DropDownList ID="ddlDepts" runat="server" AutoPostBack="True" 
                        ondatabound="ddlDepts_DataBound" 
                        onselectedindexchanged="ddlDepts_SelectedIndexChanged">
                    </asp:DropDownList>
                                            </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("000333", "角色")%>：</td>
                    <td class="style1">
                    <asp:DropDownList ID="ddlRoles" runat="server" ondatabound="ddlRoles_DataBound">
                    </asp:DropDownList>
                                            </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr><td align="right"><%=GetTran("006710", "是否有查看会员安置权限")%>：</td><td style="text-align:left">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                        RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td></tr>
                <tr><td align="right"><%=GetTran("006711", "是否有查看会员推荐权限")%>：</td><td style="text-align:left">
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" 
                        RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td></tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td class="style1">
                    <asp:Button ID="btnUpt" runat="server" Text="修 改" OnClick="BtnUpt_Click" CssClass="anyes" />
                                                    <INPUT onclick="javascript: window.location='ManagerManage.aspx'" type="button" value='<%=GetTran("000421", "返回")%>' class="anyes"></td>
                    <td>
                        &nbsp;</td>
                </tr> 
                </table><table>
                <tr>
                <td valign="top">
                <div id="cssrain">
                    <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="80">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                                    <tr>
                                        <td class="sec2">
                                           <%=GetTran("000628", "说明")%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="img1"
                                        onclick="down2()" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2">
                        <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="Table2">
                            <tbody style="display: block">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
	 1、<%=GetTran("001095", "修改系统管理员")%>。

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                </td>
                </tr>
            </table>
		</form>
					</body>

