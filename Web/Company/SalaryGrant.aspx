<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryGrant.aspx.cs" Inherits="Company_ReleaseGrant" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工资发放</title>

    <script language="jscript">
		function CanclePrompt( )
		{
            var sure=confirm('<%=GetTran("001364", "该期奖金已经发放！确定要撤消吗？")%>');
			if (sure)
			{
				__doPostBack('lkbtn_CancleTrue','');
			}
		}
    </script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>

    <script language="javascript" type="text/javascript">
	window.onerror=function()
    {
        return true;
    };
    window.onload=function()
	{
	    down2();
	};
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="display: none;">
                    <tr>
                        <td>
                            &nbsp;
                            <asp:Button ID="lkbtn_CancleTrue" runat="server" Text="Button" Width="0px" Height="0px"
                                OnClick="lkbtn_CancleTrue_Click" Style="display: none;"></asp:Button>&nbsp;<input
                                    id="txt_qishu" type="hidden" name="txt_qishu" runat="server">
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="word-break: keep-all; word-wrap: normal">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" CssClass="tablemb">
                                <HeaderStyle HorizontalAlign="Center" CssClass="tablebt" />
                                <RowStyle HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <Columns>
                                    <asp:BoundField HeaderText="期数" DataField="ExpectNum" />
                                    <asp:TemplateField HeaderText="添加">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="add_jj" CommandName="add_jj" CommandArgument='<%# Eval("ExpectNum") %>'><%#GetTran("001367", "奖金添加到电子帐户中")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="撤销">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="cancle_jj" CommandName="cancle_jj" CommandArgument='<%# Eval("ExpectNum") %>'><%#GetTran("001369", "将添加到电子帐户中的奖金撤消")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="add_View" CommandName="add_View" CommandArgument='<%# Eval("ExpectNum") %>'><%#GetTran("000440", "查看")%></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td valign="middle" width="55%">
                                        <uc1:Pager ID="Pager1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td height="48">
                            &nbsp;
                            <asp:Button ID="Button4" runat="server" Text="工资发布" OnClick="Button4_Click" CssClass="another"
                                Style="display: none;"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="80px">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2">
                                <span id="span1" title="" onmouseover="cutDescription()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#">
                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down2()" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody1">
                    <tr>
                        <td style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <%=GetTran("006875", "1、将结算确认无误的会员工资发放到会员的电子账户中去；")%>
                                        <br />
                                        <%=GetTran("006876", "2、发放过那一期不能再结算，如果因某种原因确实要重新结算，请先撤消发放后，再重新结算。 ")%>
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
