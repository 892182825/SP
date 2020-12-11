<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInBrowse.aspx.cs" Inherits="Company_StorageInBrowse" EnableEventValidation="false" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>入库审批</title>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/SqlCheck.js"></script>
    <script type="text/javascript">
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
            down2();               
        };
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <table width="100%" border="0px" cellspacing="0px" cellpadding="0px">
        <tr>
            <td>
                <br />
                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px" class="biaozzi">
                    <tr>
                        <td>
                        <asp:Button ID="Btn_Search" runat="server" Text="查 询" OnClick="Btn_Search_Click" CssClass="anyes" />&nbsp;
                            <%=GetTran("000308","选择国家")%>：<asp:DropDownList ID="Country" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList ID="ddlCondition" runat="server" OnSelectedIndexChanged="ddlCondition_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="ExpectNum">期数</asp:ListItem>
                                <asp:ListItem Value="DocMaker">入库制单人</asp:ListItem>
                                <asp:ListItem Value="OperationPerson">业务员</asp:ListItem>
                                <asp:ListItem Value="Address">购货地址</asp:ListItem>
                                <asp:ListItem Value="TotalMoney">总金额</asp:ListItem>
                                <asp:ListItem Value="DocMakeTime">制单日期</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                            &nbsp;
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="Wdate" onfocus="WdatePicker()" Visible="false"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txtCondition" runat="server" MaxLength="15"></asp:TextBox>&nbsp;
                            <asp:DropDownList ID="Flag" runat="server">
                                <asp:ListItem Selected="False" Value="StateFlag='0' And CloseFlag='0'">未审核</asp:ListItem>
                                <asp:ListItem Value="StateFlag='1'">已审核</asp:ListItem>
                                <asp:ListItem Value="StateFlag='0' and CloseFlag='1'">无效</asp:ListItem>
                            </asp:DropDownList>                     
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0px" cellpadding="0px" border="0px" style="width: 100%" class="tablemb">
                    <tr>
                        <td>
                            <asp:GridView ID="gvInfo" runat="server" AutoGenerateColumns="False" OnRowCommand="gvInfo_RowCommand"
                                OnRowDataBound="gvInfo_RowDataBound" Width="100%" >
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />                                
                                <HeaderStyle HorizontalAlign="Center" CssClass="tablemb" Wrap="false" />
                                <Columns>
                                    <asp:TemplateField HeaderText="审核入库单" ItemStyle-Wrap="false">
                                        <ItemStyle Wrap="false" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAuditing" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocID")+"_"+DataBinder.Eval(Container.DataItem,"warehouseID")+"_"+DataBinder.Eval(Container.DataItem,"DepotSeatID")%>'
                                                    CommandName="Auditing"><%#GetTran("000064","确认")%></asp:LinkButton>
                                                <asp:LinkButton ID="btnnouse" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocID") %>'
                                                    CommandName="NoEffect" ><%#GetTran("001069","无效")%></asp:LinkButton>
                                                <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocID") %>'
                                                    CommandName="Del"><%#GetTran("000022","删除")%></asp:LinkButton>
                                                <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocID") %>'
                                                    CommandName="Edit"><%#GetTran("000036","编辑")%></asp:LinkButton>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                           <img src="images/fdj.gif" />   <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "DocID", "StorageInDetail.aspx?id={0:d}") %>'
                                                NAME="Hyperlink1"><%#GetTran("000440","查看")%></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DocID" HeaderText="入库单号" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="ExpectNum" HeaderText="期数" ItemStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="是否审核" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "StateFlag").ToString().Trim() == "1" ? GetTran("000233", "是") : GetTran("000235", "否")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否失效" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "CloseFlag").ToString().Trim() == "1" ? GetTran("000233", "是") : GetTran("000235", "否")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TotalMoney" HeaderText="总金额" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TotalPV" HeaderText="总积分" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="DocMaker" HeaderText="入库制单人" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="DocAuditer" HeaderText="审核人" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="DocAuditTime" HeaderText="审核日期" DataFormatString="{0:d}" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="Name" HeaderText="供应商" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="OperationPerson" HeaderText="业务员" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="Address" HeaderText="购货地址" ItemStyle-Wrap="false" />
                                    
                                    <asp:TemplateField HeaderText="仓库名称" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%# GetWarehouseName(DataBinder.Eval(Container.DataItem, "WareHouseID").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="库位名称" ItemStyle-Wrap="false">
                                        <ItemTemplate>

                                                <%# GetDepotSeatName(DataBinder.Eval(Container.DataItem, "DepotSeatID").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BatchCode" HeaderText="批次" ItemStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText = "入库单日期" ItemStyle-Wrap="false">                                            
                                            <ItemTemplate>
                                                <%#GetbyRegisterDate(Eval("DocMakeTime").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看备注" ItemStyle-Width="100%">
                                        <ItemTemplate>
                                            <img src="images/fdj.gif" /> <%#SetVisible(DataBinder.Eval(Container.DataItem,"Note").ToString() , DataBinder.Eval(Container.DataItem,"DocID").ToString(),"StorageInRemark.aspx" )%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th><%=GetTran("002164","审核入库单")%></th>
                                            <th><%=GetTran("000339","详细")%></th>
                                            <th><%=GetTran("002166","入库单号")%></th>
                                            <th><%=GetTran("000045","期数")%></th>
                                            <th><%=GetTran("000605","是否审核")%></th>
                                            <th><%=GetTran("001811","是否失效")%></th>
                                            <th><%=GetTran("000041","总金额")%></th>
                                            <th><%=GetTran("000562","币种")%></th>
                                            <th><%=GetTran("000113","总积分")%></th>
                                            <th><%=GetTran("002131","入库制单人")%></th>
                                            <th><%=GetTran("000655","审核人")%></th>
                                            <th><%=GetTran("001599","审核日期")%></th>
                                            <th><%=GetTran("002020","供应商")%></th>
                                            <th><%=GetTran("002021","业务员")%></th>
                                            <th><%=GetTran("002040","购货地址")%></th>
                                            <th><%=GetTran("000355","仓库名称")%></th>
                                            <th><%=GetTran("000357", "库位名称")%></th>
                                            <th><%=GetTran("000658","批次")%></th>
                                            <th><%=GetTran("002167","入库单日期")%></th>
                                            <th><%=GetTran("000744","查看备注")%></th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="99%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right">                            
                            <uc1:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div id="cssrain" style="width:100%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
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
                            <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
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
                                                    <asp:Button ID="image_download" runat="server" Text="Excel" OnClick="image_download_Click1"
                                                        Style="display: none;"></asp:Button><a href="#"><img src="images/anextable.gif" width="49"
                                                            height="47" border="0" onclick="__doPostBack('image_download','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                <td>1、<%=GetTran("002138","对未审核的单据可以进行审核、无效、编辑、删除操作")%></td>
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
    <%= msg %>
</body>
</html>
