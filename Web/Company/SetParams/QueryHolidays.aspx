<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryHolidays.aspx.cs" Inherits="Company_SetParams_QueryHolidays" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交易时间</title>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">
        function confirmvalue() {
            return confirm('<%=GetTran("001718", "确实要删除吗？")%>');
        }

        function cutManagement() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
    }

    function cutDescription() {
        document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
    }

    window.onload = function () {
        down3();
    };
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
        <br />
        <table width="100%">
            <tr>
                <td>
                    <div>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvMemberBank" runat="server" AllowSorting="true"
                                        AutoGenerateColumns="false" DataKeyNames="ID"
                                        OnRowDataBound="gvMemberBank_RowDataBound" Width="100%"
                                        OnSorting="gvMemberBank_Sorting" CssClass="tablemb">
                                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                        <HeaderStyle Wrap="false" />
                                        <RowStyle HorizontalAlign="Center" Wrap="false" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtEdit" runat="server"
                                                        OnCommand="lbtEdit_Command"><%=GetTran("000259", "修改")%></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtDelete" runat="server" OnClientClick="return confirmvalue()"
                                                        OnCommand="lbtDelete_Command"><%=GetTran("000022", "删除")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" SortExpression="ID" HeaderText="序号" ItemStyle-Wrap="false" Visible="false" />
                                            <asp:BoundField DataField="Content" SortExpression="Content" HeaderText="备注" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="StartTime" SortExpression="StartTime" HeaderText="开始时间" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="EndTime" SortExpression="EndTime" HeaderText="结束时间" ItemStyle-Wrap="false" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr style="white-space: nowrap">
                                <td>
                                    <asp:Button ID="btnAdd" Text="添 加" runat="server" OnClick="btnAdd_Click" CssClass="anyes" />&nbsp;&nbsp;
                    <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_Click" CssClass="anyes" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
