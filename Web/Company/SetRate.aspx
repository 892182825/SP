<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetRate.aspx.cs" Inherits="Company_SetRate"
    EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

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

<head runat="server">
    <title>SetRate</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />

    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>

    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>

</head>

<script type="text/javascript">
    function fanyi(){
        return confirm('<%=GetTran("001718","确实要删除吗？") %>');
    }
</script>

<body>
    <form id="Form1" method="post" runat="server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
        <tr style="display:none;">
            <td style="white-space: nowrap">
                <%=GetTran("002041", "货币名称")%>：&nbsp;&nbsp;
                <asp:TextBox ID="txtNewCurrency" runat="server" onblur="javascript:this.value='';" onkeyup="javascript:this.value='';"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnAddNewCurrency" runat="server" Text="添 加" Style="cursor: pointer"
                    CssClass="anyes" OnClick="btnAddNewCurrency_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <table style="display:none">
                    <tr>
                        <td><asp:Button ID="btnUpdateRate" runat="server" Text="同步汇率" CssClass="anyes" onclick="btnUpdateRate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td> <asp:Button ID="btnSetCurr" runat="server" CssClass="anyes" Text="设置汇率" 
                                onclick="btnSetCurr_Click" /></td>
                        <td>
                            <asp:RadioButtonList ID="rdSetCurr" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">系统汇率</asp:ListItem>
                                <asp:ListItem Value="1">即时汇率</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td> &nbsp; </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvCurrency" runat="server" AllowSorting="True" DataKeyNames="ID"
                    AutoGenerateColumns="False" OnRowDataBound="gvCurrency_RowDataBound" Width="100%"
                    OnRowCommand="gvCurrency_RowCommand" CssClass="tablemb" OnRowCancelingEdit="gvCurrency_RowCancelingEdit"
                    OnRowUpdating="gvCurrency_RowUpdating" OnRowEditing="gvCurrency_RowEditing" OnSorting="gvCurrency_Sorting">
                    <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                    <HeaderStyle Wrap="false" />
                    <RowStyle HorizontalAlign="Center" Wrap="false" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" UpdateText="更新" HeaderText="编辑" CancelText="取消" />
                        <asp:BoundField DataField="ID" SortExpression="ID" ReadOnly="true" HeaderText="汇率编号" />
                        <asp:BoundField DataField="quancheng" HeaderText="币种全称" />
                        <asp:BoundField DataField="CurrencyName" SortExpression="currencyname" ReadOnly="True"
                            HeaderText="货币名称" />
                        <asp:TemplateField SortExpression="Rate" HeaderText="汇率">
                            <ItemTemplate>
                                <asp:Label ID="Label_rate" runat="server"><%#Eval("Rate") %></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox_rate" runat="server" Text='<%#Eval("Rate")%>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否启用">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetFlag(Eval("Flag")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkFlag" runat="server" Checked='<%# GetFlagEdit(Eval("Flag")) %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <uc1:Pager ID="Pager1" runat="server" />
    <table width="100%">
        <tr>
            <td>
                <div id="cssrain" style="width: 100%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="150">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                        <td class="sec2" onclick="secBoard(0)">
                                            <span id="span1" title="" onmouseover="cut()">
                                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                        </td>
                                        <td class="sec1" onclick="secBoard(1)">
                                            <span id="span2" title="" onmouseover="cut1()">
                                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                        onclick="down3()" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2" style="display:none;">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                            <tbody style="display: block" id="tbody0">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnExcelCurrency" runat="server" Text="Excel" OnClick="btnExcelCurrency_Click"
                                                        Style="display: none" />
                                                    <a href="#" id="imgCurrency" runat="server">
                                                        <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcelCurrency','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                            <tbody style="display: none" id="tbody1">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <%=GetTran("000224", "操作说明")%>：<br />
                                                    1、<%=GetTran("002110", "添加系统使用的币种，并设置该币种兑换比例。")%>
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
    <%=msg %>
    </form>
</body>
</html>
