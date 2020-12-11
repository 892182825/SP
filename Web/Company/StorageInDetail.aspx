<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInDetail.aspx.cs" Inherits="Company_StorageInDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
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
        }
	</script>       
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table style="width: 100%;" class="biaozzi" id="Table1" runat="server" cellpadding="0" cellspacing="1"
        border="0"  align="center">
        <tr><td>&nbsp;</td></tr>
        <tr><td align="center"><%=GetTran("003162","入库单详细信息")%></td></tr>
        <tr><td>&nbsp;</td></tr>
    </table>
     <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
            <td>
                <asp:GridView ID="gvInfo" runat="server" AutoGenerateColumns="False" OnRowCommand="gvInfo_RowCommand"
                                OnRowDataBound="gvInfo_RowDataBound" Width="100%" 
                               >
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />                                
                                <HeaderStyle HorizontalAlign="Center" CssClass="tablemb" Wrap="false" />
                                <Columns>
                                    <asp:TemplateField HeaderText="审核入库单" ItemStyle-Wrap="false" Visible=false>
                                        <ItemStyle Wrap="false" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAuditing" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocID") %>'
                                                    CommandName="Auditing"><%#GetTran("000064","确认")%></asp:LinkButton>
                                                <asp:LinkButton ID="btnnouse" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocID") %>'
                                                    CommandName="NoEffect" ><%#GetTran("001069","无效")%></asp:LinkButton>
                                                <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocID") %>'
                                                    CommandName="Del"><%#GetTran("000022","删除")%></asp:LinkButton>
                                                <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DocID") %>'
                                                    CommandName="Edit"><%#GetTran("000036","编辑")%></asp:LinkButton>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细" ItemStyle-Wrap="false" Visible=false>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "DocID", "StorageInDetail.aspx?id={0:d}") %>'
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
                                            <input id="hidwarehouseId" type="hidden" value='<%# DataBinder.Eval(Container.DataItem,"warehouseID") %>'
                                                runat="server" />
                                            <%# GetWarehouseName(DataBinder.Eval(Container.DataItem, "WareHouseID").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="库位名称" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                             <input id="changwei" type="hidden" value='<%# DataBinder.Eval(Container.DataItem,"DepotSeatID") %>'
                                                runat="server" />
                                                <%# GetDepotSeatName(DataBinder.Eval(Container.DataItem, "DepotSeatID").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BatchCode" HeaderText="批次" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="DocMakeTime" HeaderText="入库单日期" DataFormatString="{0:d}" ItemStyle-Width="100%" ItemStyle-Wrap="false" />
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
    <br />
    <table class="biaozzi" width="100%" border="0" cellpadding="0" cellspacing="0">
        
        <tr>
            <td>
                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnRowDataBound="gvProduct_RowDataBound" CssClass="tablemb">
                    <AlternatingRowStyle BackColor="#F1F4F8" />
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle Wrap="false" VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField HeaderText="产品编码" ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <%#GetProductcode(DataBinder.Eval(Container.DataItem,"ProductID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品名称" ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <%#GetProductName(DataBinder.Eval(Container.DataItem,"ProductID").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProductQuantity" HeaderText="数量" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="ProductUnitName" HeaderText="单位" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="ExpectNum" HeaderText="期数" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="UnitPrice" HeaderText="单价" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="PV" HeaderText="积分" ItemStyle-Wrap="false" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>            
            <td align="right" style="white-space:nowrap">
                <input type="button" onclick="location.href='StorageInBrowse.aspx'" value='<%=GetTran("000421","返回")%>' id="btnReturn" class="anyes" />
            </td>
        </tr>
    </table>               
    <div id="cssrain">
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
                            <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()" /></a></td>
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
                                                    <a href="#">
                                                        <asp:LinkButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click"
                                                            Style="display: none;"></asp:LinkButton>
                                                        <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;

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
                                                <td><%=GetTran("000945", "订单详细信息")%></td>
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
