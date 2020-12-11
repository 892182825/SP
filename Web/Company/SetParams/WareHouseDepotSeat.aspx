<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WareHouseDepotSeat.aspx.cs" Inherits="Company_SetParams_WareHouseDepotSeat" EnableEventValidation="false"  %>

<%@ Register src="../../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>
<%@ Register src="../../UserControl/CountryCity.ascx" tagname="CountryCity" tagprefix="uc2" %>
<%@ Register src="../../UserControl/CountryCityPCode.ascx" tagname="CountryCityPCode" tagprefix="uc3" %>
<%--<%@ Register TagPrefix="ucl" TagName="uclWareHouse" Src="~/UserControl/WareHouse.ascx" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加仓库</title>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/SqlCheck.js"></script>
    <style type="text/css">
        .style1
        {
            width: 202px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function confirmvlue() {
            return confirm('<%=GetTran("001688", "确定要清空吗？")%>');
        }
        function confirmvlue1() {
            return confirm('<%=GetTran("001718", "确实要删除吗？")%>');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                <table border="0" cellpadding="0" cellspacing="0" class="biaozzi"><tr><td>
                <asp:Button ID="Button2" runat="server" Text="查 询" CssClass="anyes" 
                        onclick="Button2_Click"/>&nbsp;&nbsp;
                    <%=GetTran("000058", "请选择国家")%>：<asp:DropDownList
                        ID="DropDownList1" runat="server">
                    </asp:DropDownList>&nbsp;<%=GetTran("000355", "仓库名称")%>：<asp:TextBox ID="txtWareHouseName_S" runat="server" MaxLength="20"></asp:TextBox>&nbsp;<%=GetTran("001669", "仓库简介")%>：<asp:TextBox ID="txtWareHouseForShort_S"
                        runat="server" MaxLength="10"></asp:TextBox>&nbsp;<%=GetTran("001671", "仓库地址")%>：<asp:TextBox ID="txtWareHouseAddress_S" runat="server" MaxLength="20"></asp:TextBox>
                    &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="添加仓库" CssClass="anyes" 
                        onclick="Button1_Click"/>&nbsp;<asp:Button ID="Button5" runat="server" Text="返 回" CssClass="anyes" 
                        OnClick="btnBack_Click"/>
                </td></tr></table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gvWareHouse" runat="server" AutoGenerateColumns="false" 
                        onrowdatabound="gvWareHouse_RowDataBound" DataKeyNames="WareHouseID" 
                        Width="100%" AllowSorting="true"  CssClass="tablemb"  >
                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />
                        <Columns>                                
                            <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnWareHouseEdit" runat="server"
                                        oncommand="lbtnWareHouseEdit_Command"><%=GetTran("000259", "修改")%></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnWareHouseDelete" runat="server"
                                        OnClientClick="return confirmvlue1()" 
                                        oncommand="lbtnWareHouseDelete_Command"><%=GetTran("000022", "删除")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("WareHouseID","AddDepotSeat.aspx?WareHouseID={0}") %>'><%=GetTran("001724", "查看库位")%></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WareHouseID"  HeaderText="仓库编号" ItemStyle-Wrap="false" Visible="false" />
                            <asp:BoundField DataField="Name" HeaderText="所属国家" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="WareHouseName" HeaderText="仓库名称" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="WareHouseForShort"  HeaderText="仓库简称" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="WareHousePrincipal"  HeaderText="仓库负责人" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="WareHouseAddress" HeaderText="仓库所在地" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="WareHouseDescr"  HeaderText="描述" ItemStyle-Wrap="false" NullDisplayText="无" />
                            <asp:BoundField DataField="WareControl" HeaderText="权限控制" ItemStyle-Wrap="false" Visible="false" />
                        </Columns>
                        <EmptyDataTemplate>
                        <table class="tablemb" Width="100%" >
                                <tr>
                                    <th>
                                        <%=GetTran("000015", "操作")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000015", "操作")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000253", "仓库编号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000454", "所属国家")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000355", "仓库名称")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001675", "仓库简称")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001678", "仓库负责人")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001679", "仓库所在地")%>
                                    </th>
                                    <th>
                                       <%=GetTran("001680", "描述")%> 
                                    </th>
                                    <th>
                                        <%=GetTran("001683", "权限控制")%> 
                                    </th>
                                </tr>                
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>    
            <tr><td>
                <uc1:Pager ID="Pager1" runat="server" />
                </td></tr>        
        </table>
        <br />
                   <table id="tab1" runat="server" align="center" style="width:100%;">
                            <tr  id="trWareHouse" runat="server">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" class="tablemb" style="width:100%;">
                                        <tr>
                                            <td align="right" class="style1"><%=GetTran("000047", "国家")%>：</td>
                                            <td><asp:DropDownList ID="ddlCountry1" runat="server" AutoPostBack="false"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1"><%=GetTran("000355", "仓库名称")%>：</td>
                                            <td><asp:TextBox ID="txtWareHuseName" runat="server" MaxLength="20"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr1" style="display:none;">
                                            <td align="right" class="style1"><%=GetTran("000357", "库位名称")%>：</td>
                                            <td><asp:TextBox ID="Seat" runat="server" MaxLength="20"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1"><%=GetTran("001675", "仓库简称")%>：</td>
                                            <td><asp:TextBox ID="txtWareHouseForShort" runat="server" MaxLength="10"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1"><%=GetTran("001678", "仓库负责人")%>：</td>
                                            <td><asp:TextBox ID="txtWareHousePrincipal" runat="server" MaxLength="20"></asp:TextBox></td>
                                        </tr>                        
                                        <tr>
                                            <td align="right" class="style1"><%=GetTran("001679", "仓库所在地")%>：</td>
                                            <td>
                                               <table border="0" cellpadding ="0" cellspacing="0">
                                               <tr>
                                               <td>
                                                <%--   <uc3:CountryCityPCode ID="CountryCity1" runat="server" />--%>
                                                   <uc2:CountryCity ID="CountryCity1" runat="server" />

                                               </td>
                                               <td><asp:TextBox ID="txtWareHouseAddress" runat="server" MaxLength="20" 
                                                       Width="316px"></asp:TextBox></td>
                                               </tr></table> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1"><%=GetTran("001687", "仓库描述")%>：</td>
                                            <td><asp:TextBox ID="txtWareHouseDescr" runat="server" TextMode="MultiLine" MaxLength="100"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnAdd" runat="server" Text="确 定" onclick="btnAdd_Click" CssClass="anyes" />&nbsp;&nbsp;                                    
                                            <asp:Button ID="btnReset" runat="server" Text="清 空" CssClass="anyes" 
                                                OnClientClick="return confirmvlue();" 
                                                onclick="btnReset_Click" />                                                                                            
                                        </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                            
                   </table>                   
                </td>
            </tr>    
        </table>         
    </form>
</body>
</html>
