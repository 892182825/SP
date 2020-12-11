<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductColor.aspx.cs" Inherits="Company_SetParams_ProductColor"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ProductColor</title>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript" src="../../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript">
        function ale()
        {
            return confirm('<%=GetTran("001222","确实要清空吗？")%>');
        }
        
        function ale1()
        {
           return confirm('<%=GetTran("001718","确实要删除吗？")%>');
        }
        
        function cutManagement()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        
        function cutDescription()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
        
        window.onload=function()
        {
            down3();               
        };
    </script>

</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvProductColor" runat="server" AllowSorting="true" AutoGenerateColumns="false"
                                    class="tablemb" DataKeyNames="ProductColorID" OnRowDataBound="gvProductColor_RowDataBound"
                                    Width="100%" OnSorting="gvProductColor_Sorting">
                                    <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                    <RowStyle HorizontalAlign="Center" Wrap="false" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtEdit" runat="server" OnCommand="lbtEdit_Command"><%#GetTran("000259", "修改")%></asp:LinkButton>
                                                <asp:LinkButton ID="lbtDelete" runat="server" OnClientClick="return ale1()"
                                                    OnCommand="lbtDelete_Command"><%#GetTran("000022", "删除")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProductColorID" SortExpression="productColorID" HeaderText="产品颜色编号"
                                            ItemStyle-Wrap="false" Visible="false" />
                                        <asp:BoundField DataField="ProductColorName" SortExpression="productColorName" HeaderText="产品颜色名称"
                                            ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="ProductColorDescr" SortExpression="productColorDescr"
                                            HeaderText="产品颜色说明" ItemStyle-Wrap="false" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnAdd" Text="添 加" runat="server" OnClick="btnAdd_Click" Style="cursor: pointer"
                                    CssClass="anyes" />&nbsp;&nbsp;
                                <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_Click" Style="cursor: pointer"
                                    CssClass="anyes" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divProductColor" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" class="tablemb">
                        <tr>
                            <td>
                                <asp:Label ID="lblProductColorName" runat="server" Text="颜色名称："></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProductColorName" MaxLength="10" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProductColorDescr" runat="server" Text="颜色描述："></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProductColorDescr" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table >
                        <tr>
                            <td>
                                <asp:Button ID="btnOK" runat="server" Text="确 定" OnClick="btnOK_Click" Style="cursor: pointer"
                                    CssClass="anyes" />&nbsp;&nbsp;
                                <asp:Button ID="btnReset" Text="清 空" runat="server" OnClick="btnReset_Click" Style="cursor: pointer"
                                    CssClass="anyes" OnClientClick="return ale()" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>          
    </table>
    <div id="cssrain" style="width:100%;display:none;">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
                <tr>
                    <td width="150px">
                        <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                            <tr>
                                <td class="sec2" onclick="secBoard(0)">
                                    <span id="span1" title="" onmouseover="cutManagement()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                </td>
                                <td class="sec1" onclick="secBoard(1)">
                                    <span id="span2" title="" onmouseover="cutDescription()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td><a href="#"><img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down3()" /></a></td>
                </tr>
            </table>
            <div id="divTab2">
                <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                    <tbody style="display: block" id="tbody0">
                        <tr>
                            <td valign="bottom" style="padding-left: 20px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnExcel" Text="Excel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                                            <a href="#">
                                                <img src="../images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                            <%=GetTran("002212","产品颜色管理")%><br />
                                            1.<%=GetTran("002213","对产品颜色进行添加，修改，删除操作")%>
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
