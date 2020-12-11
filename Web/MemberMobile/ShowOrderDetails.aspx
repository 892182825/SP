<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowOrderDetails.aspx.cs"
    Inherits="Member_ShowOrderDetails" %>

<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="x-ua-compatible" content="ie=11" />
<head id="Head1" runat="server">
    <title></title>
    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />

    
    <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />
    
    <style>
        .tablemb th {
            padding: 10px 16px;
            border-left: #bebebe !important;
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            color: #333;
            text-decoration: none;
            /* background-image: url(../images/tabledp.gif); */
            background-repeat: repeat-x;
            text-align: center;
            text-indent: 10px;
        }

        .tablemb {
            font-family: Arial;
            /* font-size: 12px; */
            /* color: #333; */
            /* margin-top: 90px; */
            text-decoration: none;
            line-height: 31px;
            background-color: #FAFAFA;
            /* border: 1px solid #88F4AE; */
            text-indent: 10px;
            white-space: normal;
            background: url(../../images/img/mws-table-header.png) left bottom repeat-x;
        }
    </style>
    <script type="text/javascript">

        function getArgs(parm) //parm: 地址栏上参数名称
        {
            var pars = location.search;//获取当前url
            var pos = pars.indexOf('?');//查找第一个?
            pars = pars.substring(pos + 1);//获取参数部分
            var ps = pars.split("&");
            var temp;
            var name, value, index;
            for (var i = 0; i < ps.length; i++) {
                temp = ps[i];
                index = temp.indexOf("=");
                if (index == -1) continue;//如果参数中未包含=则继续
                name = temp.substring(0, index);//参数名称
                if (name == parm) {
                    value = temp.substring(index + 1);//参数的值
                    document.location.href = value + ".aspx";
                }
            }
        }
    </script>

</head>
<%--<body>
    <form id="Form1" method="post" runat="server">
        <div class="MemberPage">
            <uc1:top runat="server" ID="top" />
            <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="biaozzi">
                <tr>
                    <td height="48" align="center">
                        <font size="3"><b><%=GetTran("000884", "订单明细")%></b></font>
                    </td>
                </tr>
            </table>
            <div class="ctConPgList-1">
                <asp:GridView ID="gvorder" runat="server" Width="100%" CellSpacing="1" CellPadding="1" AutoGenerateColumns="False"
                    OnRowDataBound="gvorder_RowDataBound">
                    <HeaderStyle HorizontalAlign="Center" CssClass="ctConPgTab"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#F1F4F8" />
                    <Columns>

                        <asp:BoundField DataField="OrderExpectNum" HeaderText="期数"></asp:BoundField>
                        <asp:BoundField DataField="defraystate" HeaderText="支付状态"></asp:BoundField>
                        <asp:BoundField DataField="OrderID" HeaderText="订单号"></asp:BoundField>
                        <asp:BoundField Visible="False" DataField="Number" HeaderText="会员编号"></asp:BoundField>
                        <asp:BoundField Visible="False" DataField="Name" HeaderText="会员姓名"></asp:BoundField>
                        <asp:BoundField DataField="TotalMoney" ItemStyle-HorizontalAlign="Right" HeaderText="金额"
                            DataFormatString="{0:n2}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Totalpv" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                            HeaderText="积分">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="报单日期">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("orderdate")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StoreID" HeaderText="确认店铺"></asp:BoundField>
                        <asp:BoundField DataField="ordertype" HeaderText="报单途径"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table cellspacing="0" cellpadding="0" width="100%" border="1">
                            <tr>

                                <th>
                                    <%=GetTran("000045", "期数")%>
                                </th>
                                <th>
                                    <%=GetTran("000775", "支付状态")%>
                                </th>
                                <th>
                                    <%=GetTran("000079", "订单号")%>
                                </th>
                                <th>
                                    <%=GetTran("000322", "金额")%>
                                </th>
                                <th>
                                    <%=GetTran("000414", "积分")%>
                                </th>
                                <th>
                                    <%=GetTran("001429", "报单日期")%>
                                </th>
                                <th>
                                    <%=GetTran("000793", "确认店铺")%>
                                </th>
                                <th>
                                    <%=GetTran("000774", "报单途径")%>
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <br />
            <table cellspacing="0" cellpadding="0" border="0" width="100%" align="center">
                <tr>
                    <td valign="top" align="center">
                        <asp:GridView ID="myDatGrid" runat="server" AutoGenerateColumns="False" AllowSorting="false"
                            Width="100%" CellPadding="1" CellSpacing="1" OnRowDataBound="myDatGrid_RowDataBound">
                            <HeaderStyle HorizontalAlign="Center" CssClass="ctConPgTab"></HeaderStyle>
                            <Columns>
                                <asp:BoundField DataField="OrderID" ItemStyle-HorizontalAlign="Center" HeaderText="订单号"></asp:BoundField>
                                <asp:BoundField DataField="StoreID" HeaderText="店铺编号" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="产品名称" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="ProductTypeName" HeaderText="产品型号" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="Quantity" HeaderText="数量" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="Price" HeaderText="单价" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                <asp:BoundField DataField="pv" HeaderText="积分" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:f2}"></asp:BoundField>
                                <asp:BoundField DataField="totalmoney" HeaderText="总金额" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:f2}"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table cellspacing="0" cellpadding="0" border="1" width="100%">
                                    <tr>
                                        <th>
                                            <%=GetTran("000079", "订单号")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000150", "店铺编号")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000501", "产品名称")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000882", "产品型号")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000505", "数量")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000503", "单价")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000414", "积分")%>
                                        </th>
                                        <th>
                                            <%=GetTran("000041", "总金额")%>
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="biaozzi">
                        <br />
                        <asp:Button ID="btnE" runat="server" Text="返 回" CssClass="anyes"
                            OnClick="btnE_Click" />
                    </td>
                </tr>
            </table>
            <uc2:bottom runat="server" ID="bottom" />
        </div>
    </form>
</body>--%>

<body>
    <form id="form2" runat="server">
        <uc1:top runat="server" ID="top" />
        <div class="rightArea clearfix">
            <div class="rightAreaIn">
                <div class="noticeEmail width100per mglt0">
                    <div class="pcMobileCut">
                        <div class="noticeHead">
                            <div>
                                <i class="glyphicon glyphicon-file"></i>
                                <span>订单明细</span>
                            </div>
                        </div>
                        <div class="noticeBody">
                            <div class="tableWrap clearfix table-responsive">
                                <table class="table-bordered noticeTable">
                                    <tbody>
                                        <tr>
                                            <td style="padding: 0">
                                                <asp:GridView ID="gvorder" runat="server" Width="100%" CellSpacing="1" CellPadding="1" AutoGenerateColumns="False"
                                                    OnRowDataBound="gvorder_RowDataBound">
                                                 <HeaderStyle Wrap="false" CssClass="tablemb" />
                                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                                    <Columns>

                                                        <asp:BoundField DataField="OrderExpectNum" HeaderText="期数"></asp:BoundField>
                                                        <asp:BoundField DataField="defraystate" HeaderText="支付状态"></asp:BoundField>
                                                        <asp:BoundField DataField="OrderID" HeaderText="订单号"></asp:BoundField>
                                                        <asp:BoundField Visible="False" DataField="Number" HeaderText="会员编号"></asp:BoundField>
                                                        <asp:BoundField Visible="False" DataField="Name" HeaderText="会员姓名"></asp:BoundField>
                                                        <asp:BoundField DataField="TotalMoney" ItemStyle-HorizontalAlign="Right" HeaderText="金额"
                                                            DataFormatString="{0:n2}">
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Totalpv" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                                                            HeaderText="积分">
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="报单日期">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("orderdate")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="StoreID" HeaderText="确认店铺"></asp:BoundField>
                                                        <asp:BoundField DataField="ordertype" HeaderText="报单途径"></asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="1">
                                                            <tr>

                                                                <th>
                                                                    <%=GetTran("000045", "期数")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000775", "支付状态")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000079", "订单号")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000322", "金额")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000414", "积分")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("001429", "报单日期")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000793", "确认店铺")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000774", "报单途径")%>
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>


                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>


        <div class="rightArea clearfix">
            <div class="rightAreaIn">
                <div class="noticeEmail width100per mglt0">
                    <div class="pcMobileCut">

                        <div class="noticeBody">
                            <div class="tableWrap clearfix table-responsive">
                                <table class="table-bordered noticeTable">
                                    <tbody>
                                        <tr>
                                            <td style="padding: 0">
                                                <asp:GridView ID="myDatGrid" runat="server" AutoGenerateColumns="False" AllowSorting="false"
                                                    Width="100%" CellPadding="1" CellSpacing="1" OnRowDataBound="myDatGrid_RowDataBound">
                                                   <HeaderStyle Wrap="false" CssClass="tablemb" />
                                                    <Columns>
                                                        <asp:BoundField DataField="OrderID" ItemStyle-HorizontalAlign="Center" HeaderText="订单号"></asp:BoundField>
                                                        <asp:BoundField DataField="StoreID" HeaderText="店铺编号" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                        <asp:BoundField DataField="ProductName" HeaderText="产品名称" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                        <asp:BoundField DataField="ProductTypeName" HeaderText="产品型号" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                        <asp:BoundField DataField="Quantity" HeaderText="数量" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                        <asp:BoundField DataField="Price" HeaderText="单价" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                                        <asp:BoundField DataField="pv" HeaderText="积分" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:f2}"></asp:BoundField>
                                                        <asp:BoundField DataField="totalmoney" HeaderText="总金额" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:f2}"></asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table cellspacing="0" cellpadding="0" border="1" width="100%">
                                                            <tr>
                                                                <th>
                                                                    <%=GetTran("000079", "订单号")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000150", "店铺编号")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000501", "产品名称")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000882", "产品型号")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000505", "数量")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000503", "单价")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000414", "积分")%>
                                                                </th>
                                                                <th>
                                                                    <%=GetTran("000041", "总金额")%>
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td colspan="2">

                                                <%--  <asp:Button ID="btnE" runat="server" Text="返 回" CssClass="anyes" 
                    onclick="btnE_Click" />--%>
                                                <asp:Button ID="btnE" runat="server" Height="30px" Width="45px" Style="margin-top: 1px; padding: -1px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                                                    Text="搜 索" OnClick="btnE_Click" CssClass="anyes" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <uc2:bottom runat="server" ID="bottom" />
    </form>
    <script type="text/jscript">
        $(function () {
            $('#rtbiszf label').css('float', 'left');
            $('#rtbiszf').css('width', '18%');
            $('#Pager1_pageTable').css('margin-right', '0px');
            $('#Pager1_btn_submit').css('Height', '22px');
            $('#Pager1_btn_submit').css('width', '70px');
            $('#Pager1_pageTable').css('color', '#333');
            $('#Pager1_ShowTable').css('color', '#333');
            $('.rightArea').css({ 'height': '0px', 'min-height': '0' });

        })

    </script>
</body>

</html>
