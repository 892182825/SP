<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VIPCardManage.aspx.cs" Inherits="Company_VIPCardManage"
    EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=GetTran("001440","编号分配") %></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>

    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>

    <script type="text/javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
        window.onload=function()
	    {
	        down2();
	    };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="white-space: nowrap; height: 38px">
                            <%=GetTran("000037","店编号") %>
                            <asp:TextBox ID="txt_StoreID" runat="server" Width="118px" MaxLength="10"></asp:TextBox>&nbsp;<%=GetTran("001487", "的卡号范围，并按")%>&nbsp;
                            <asp:DropDownList ID="dropSortBy" runat="server">
                                <asp:ListItem Value="RangeID" Selected="True">&nbsp;编号范围&nbsp;</asp:ListItem>
                                <asp:ListItem Value="StoreID">&nbsp;店编号&nbsp;</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="dropSortMode" runat="server" Visible="False">
                                <asp:ListItem Value="DESC">降序</asp:ListItem>
                                <asp:ListItem Value="ASC">升序</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<%=GetTran("000864", "排序")%>&nbsp;&nbsp;
                            <asp:Button ID="btn_Query" runat="server" Height="23" Text="查 询" OnClick="btn_Query_Click1"
                                CssClass="anyes" align="left"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap">
                            <asp:Label ID="label2" runat="server">&nbsp; <%=GetTran("000448","从") %>&nbsp;</asp:Label><asp:TextBox
                                ID="txt_BeginCard" runat="server" MaxLength="10" Enabled="False" ReadOnly="True"
                                Width="60px"></asp:TextBox>&nbsp;
                            <%=GetTran("000205", "到")%>&nbsp;<asp:TextBox ID="txt_EndCard" runat="server" Width="60px"
                                MaxLength="10"></asp:TextBox>
                            （<%=GetTran("000505", "数量")%>：<asp:TextBox ID="txt_CardCount" runat="server" Width="60px"
                                MaxLength="10"></asp:TextBox>）&nbsp;
                            <%=GetTran("001460", "给店铺")%>&nbsp;<asp:TextBox ID="txt_ToStoreID" runat="server"
                                Width="100px" MaxLength="15"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="Button1" runat="server"
                                    Text="新增卡号范围" OnClick="Button1_Click" CssClass="another" align="left"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="xianshi" runat="server">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="word-break: keep-all; word-wrap: normal; border: rgb(147,226,244) solid 1px">
                                <asp:GridView ID="DataGrid1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    AllowSorting="false" OnRowCommand="DataGrid1_RowCommand" OnRowDataBound="DataGrid1_RowDataBound"
                                    CssClass="tablemb bordercss">
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="RangeID" SortExpression="rangeid" HeaderText="范围编号" />
                                        <asp:TemplateField HeaderText="取消分配">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lkbtn_delete" runat="server" CommandName="DELETE">取消分配</asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <span></span>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="重新分配">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lkbtn_update" runat="server" CommandName="UPDATE">重分配到</asp:LinkButton>
                                                <asp:TextBox ID="txt_ToStore" runat="server" MaxLength="15"></asp:TextBox>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="StoreID" SortExpression="storeid" HeaderText="店编号" />
                                        <asp:BoundField DataField="BeginCard" SortExpression="begincard" HeaderText="起始卡号" />
                                        <asp:BoundField DataField="EndCard" SortExpression="endcard" HeaderText="结束卡号" />
                                        <asp:BoundField DataField="_inuse" SortExpression="_inuse" HeaderText="分配状态" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table class="tablemb" backcolor="#F8FBFD" width="100%">
                                            <tr>
                                                <th>
                                                    <%=GetTran("001462", "范围编号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("001466", "取消分配")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("001467", "重新分配")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("000037", "店编号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("001469", "起始卡号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("001470", "结束卡号")%>
                                                </th>
                                                <th>
                                                    <%=GetTran("001472", "分配状态")%>
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td>
                            </td>
                            <td style="width: 100%">
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <p>
                    <asp:GridView ID="DataGrid2" runat="server" Width="100%" AutoGenerateColumns="False"
                        CssClass="tablemb bordercss">
                        <AlternatingRowStyle BackColor="#F1F4F8" />
                        <HeaderStyle />
                        <Columns>
                            <asp:BoundField DataField="RangeID" HeaderText="范围编号" />
                            <asp:BoundField DataField="StoreID" HeaderText="店编号" />
                            <asp:BoundField DataField="BeginCard" HeaderText="起始卡号" />
                            <asp:BoundField DataField="EndCard" SortExpression="endcard" HeaderText="结束卡号" />
                            <asp:BoundField DataField="_inuse" SortExpression="_inuse" HeaderText="分配状态" />
                        </Columns>
                    </asp:GridView>
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top">
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
                                    <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                        align="middle" onclick="down2()" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                            <tbody style="display: block">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Download" runat="server" Text="导出Excel" OnClick="Download_Click"
                                                        Style="display: none;"></asp:Button><a href="#"><img onclick="__doPostBack('Download','');"
                                                            src="images/anextable.gif" width="49" height="47" border="0" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                            <tbody style="display: none">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <%=GetTran("001473", "１、设置各店铺可使的编号，编号区间段的分配。")%>
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
    <asp:LinkButton ID="prevbtn" Style="display: none" Text="上一页" runat="server"></asp:LinkButton><asp:LinkButton
        ID="fristbtn" Style="display: none" Text="首页" runat="server"></asp:LinkButton><asp:LinkButton
            ID="nextbtn" Style="display: none" Text="下一页" runat="server"></asp:LinkButton><asp:LinkButton
                ID="lastbtn" Style="display: none" Text="尾页" runat="server"></asp:LinkButton><asp:LinkButton
                    ID="Gobtn" Style="display: none" Text="定位" runat="server"></asp:LinkButton>
    </form>
</body>
</html>
