<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePassReset.aspx.cs"
    Inherits="Company_ManagePassReset" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=GetTran("007216","管理员密码重置") %></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
    <br />
    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="left" style="white-space: nowrap">
                            <asp:Button ID="BtnConfirm" runat="server" Text="查 询" CssClass="anyes" OnClick="BtnConfirm_Click">
                            </asp:Button>
                            &nbsp;<%=GetTran("001066", "管理员编号")%>：<asp:TextBox ID="Number" runat="server" Width="80px"
                                MaxLength="10"></asp:TextBox>
                            &nbsp;<%=GetTran("002049", "管理员姓名")%>：<asp:TextBox ID="Name" runat="server" Width="80px"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" class="tablemb" width="100%">
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="true" AutoGenerateColumns="False"
                                BorderStyle="Solid" OnRowDataBound="GridView1_RowDataBound">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablemb" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <a href="#" onclick="showManagerDetail('<%#DataBinder.Eval(Container.DataItem,"id")%>','3','<%#DataBinder.Eval(Container.DataItem,"Number")%>')">
                                                <%# GetTran("000664", "密码重置")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Number" HeaderText="管理员编号"></asp:BoundField>
                                    <asp:BoundField DataField="mName" HeaderText="管理员名称" />
                                    <asp:BoundField DataField="dName" HeaderText="角色名"></asp:BoundField>
                                    <asp:BoundField DataField="Dept" HeaderText="部门名"></asp:BoundField>
                                    <asp:TemplateField HeaderText="创建日期">
                                        <ItemTemplate>
                                            <%# DateTime.Parse(Eval("BeginDate").ToString()).AddHours(Convert.ToDouble(Session["WTH"]))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table class="biaozzi" width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("001066", "操作")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000024", "管理员编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001067", "管理员名称")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000963", "角色名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000964", "部门名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000967", "创建时间")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)" style="white-space: nowrap;">
                                <span id="span1" title="" onmouseover="cut()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)" style="white-space: nowrap;">
                                <span id="span2" title="" onmouseover="cut1()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#">
                        <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                            onclick="down2()" style="vertical-align: middle" /></a>
                </td>
            </tr>
        </table>
        <div id="divTab2" style="display: none;">
            <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="download" Text="导出EXECL" runat="server" OnClick="download_Click"
                                            Style="display: none;"></asp:LinkButton>
                                        <a href="#">
                                            <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('download','');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
                <tbody style="display: none" id="tbody1">
                    <tr>
                        <td style="padding-left: 20px">
                            1、<%=GetTran("007216", "管理员密码重置 ")%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/companyview.js"></script>

</body>
</html>
