<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginSetting.aspx.cs" Inherits="Company_LoginSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GetTran("000569","系统开关") %></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>

   <script type="text/javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>

    <script type="text/javascript" language="javascript">
   function secBoard(n)
  {
       for(i=0;i<secTable.cells.length;i++)
      secTable.cells[i].className="sec1";
    secTable.cells[n].className="sec2";
    for(i=0;i<mainTable.tBodies.length;i++)
      mainTable.tBodies[i].style.display="none";
    mainTable.tBodies[n].style.display="block";
  }
          window.onload=function()
	    {
	        down2();
	    };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
        <tr>
            <td>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="3" align="center">
                    <tr>
                        <td valign="top" align="center">
                            <table width="400px" border="0" cellpadding="0" cellspacing="5" class="tablemb">
                                <tr style="display:none">
                                    <td align="center">
                                        <%=GetTran("000594","当前状态：") %>
                                        <asp:Label ID="LblStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                   <b> <%=GetTran("006704", "禁止登陆请打勾")%></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:CheckBoxList ID="cb_list" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="H">会员</asp:ListItem>
                                            <asp:ListItem Value="D" style="display:none;">店铺</asp:ListItem>
                                            <asp:ListItem Value="G">管理员</asp:ListItem>
                                             <asp:ListItem Value="B" style="display:none;" >分公司</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="BtnPermissionNormal" runat="server" Text="确 定" OnClick="BtnPermissionNormal_Click"
                                            CssClass="anyes" Style="cursor: hand"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel runat="server" ID="panel1" Visible="false">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <span>黑名单列表</span>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                    <tr>
                                        <asp:Panel ID="BasicNumber" runat="server" Visible="false">
                                            <td align="left">
                                                按编号搜索：
                                                <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                                                <asp:Button ID="btSearchByUserID" runat="server" Text="搜索"></asp:Button>
                                            </td>
                                        </asp:Panel>
                                        <td>
                                            <asp:LinkButton ID="SearchIP" runat="server">按IP搜索</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <asp:Panel ID="BasicIP" runat="server" Visible="false">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <p>
                                                    按IP地址搜索：
                                                    <asp:TextBox ID="TxtIP" runat="server"></asp:TextBox>&nbsp;按IP地址搜索：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="SeaIPButton" runat="server" Text="搜索"></asp:Button></p>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                    <tr>
                                        <td align="center" colspan="2" style="border:rgb(147,226,244) solid 1px">
                                            <asp:DataGrid ID="grdBlacklistDisplay" runat="server" Width="100%" AllowSorting="false"
                                                AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" CssClass="tablemb bordercss">
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <span></span>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelectRow" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="UserID" HeaderText="编号"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="StateDisplay" HeaderText="状态"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="UserTypeDisplay" HeaderText="类别"></asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle Position="Top" PageButtonCount="20"></PagerStyle>
                                            </asp:DataGrid>
                                            <asp:DataGrid ID="IPGrid" runat="server" Width="100%" AllowSorting="false" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="id" HeaderText="ID"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="UserIP" HeaderText="IP地址段"></asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" Position="TopAndBottom" Mode="NumericPages">
                                                </PagerStyle>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <input onclick="location.href='BlacklistManager.aspx'" type="button" value="添加黑名单" />
                                            <asp:Button ID="btAllowList" runat="server" Text="允许所选用户登录"></asp:Button>
                                            <asp:Button ID="btDenyList" runat="server" Text="禁止所选用户登录"></asp:Button>
                                            <asp:Button ID="btRomoveFromList" runat="server" Text="从黑名单中移除"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <%=GetTran("000616","注意：黑名单中显示禁止登录的用户将不能登陆，不在列表或显示允许登录的用户依然可以登录系统") %>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <div id="cssrain" style="width:100%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
                        <tr>
                            <td width="80">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="Table1">
                                    <tr>
                                        <td class="sec2">
                                            <span id="sp" title='<%=GetTran("000033")%>'>
                                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033"))%></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                    onclick="down2()" />
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                            <!-- <tbody style="display: block">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <img src="images/anextable.gif" width="49" height="47" border="0" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <img src="images/anprtable.gif" width="49" height="47" border="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                             -->
                            <tbody style="display: block">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    1、<%=GetTran("000585","设置是否允许管理员、店铺、会员登录") %>。<br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
    </form>
</body>
</html>
