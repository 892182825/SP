<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BrowseMemberOrders.aspx.cs" Inherits="BrowseMemberOrders" %>

<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报单浏览</title>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript">
        function down2() {
            if (document.getElementById("divTab2").style.display == "none") {
                document.getElementById("divTab2").style.display = "";
                document.getElementById("imgX").src = "images/dis1.GIF";
            }
            else {
                document.getElementById("divTab2").style.display = "none";
                document.getElementById("imgX").src = "images/dis.GIF";
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function secBoard(n) {

            var tdarr = document.getElementById("secTable").getElementsByTagName("td");

            for (var i = 0; i < tdarr.length; i++) {
                tdarr[i].className = "sec1";
            }
            tdarr[n].className = "sec2";

            var tbody0 = document.getElementById("tbody0");
            tbody0.style.display = "none";
            var tbody1 = document.getElementById("tbody1");
            tbody1.style.display = "none";


            document.getElementById("tbody" + n).style.display = "block";
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            if ($.browser.msie && $.browser.version == 6) {
                FollowDiv.follow();
            }
        });
        FollowDiv = {
            follow: function () {
                $('#cssrain').css('position', 'absolute');
                $(window).scroll(function () {
                    var f_top = $(window).scrollTop() + $(window).height() - $("#cssrain").height() - parseFloat($("#cssrain").css("borderTopWidth")) - parseFloat($("#cssrain").css("borderBottomWidth"));
                    $('#cssrain').css('top', f_top);
                });
            }
        }
    </script>

    <style type="text/css">
        #secTable {
            width: 150px;
        }
    </style>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
</head>
<body onload="down2()">
    <form id="form1" runat="server">
        <br />
        <table width="99%" border="0" cellpadding="0" cellspacing="0" style="word-break: keep-all; word-wrap: normal;" class="biaozzi">
            <tr>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="anyes" Text=" " OnClick="btnSearch_Click" />
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Selected="True" Value="0">报单期数</asp:ListItem>
                        <asp:ListItem Value="1">审核期数</asp:ListItem>
                    </asp:DropDownList><uc1:ExpectNum ID="ExpectNum1" runat="server" />
                    <%=this.GetTran("000719", "并且")%>
                    <asp:DropDownList ID="ddlContion" runat="server" Style="word-break: keep-all; word-wrap: normal;"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlContion_SelectedIndexChanged">
                        <asp:ListItem Value="A.Number" Selected="True">会员编号</asp:ListItem>
                        <asp:ListItem Value="A.Name">会员姓名</asp:ListItem>
                        <asp:ListItem Value="B.OrderID">订单号</asp:ListItem>
                        <asp:ListItem Value="B.TotalMoney">金额</asp:ListItem>
                        <asp:ListItem Value="B.InvestJB">石斛积分</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlcompare" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtContent" runat="server"></asp:TextBox><%=this.GetTran("000731", "的报单")%>&nbsp;&nbsp;
                                <%=GetTran("000775", "支付状态")%>：
                            <asp:DropDownList ID="ddliszf" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddliszf_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="B.DefrayState!=0">已支付</asp:ListItem>
                                <asp:ListItem Value="B.DefrayState=0">未支付</asp:ListItem>
                            </asp:DropDownList>
                    &nbsp;
                <%=GetTran("005942", "报单时间") %>：
                <asp:TextBox ID="TextBox1" runat="server" Width="81" CssClass="Wdate" onfocus="new WdatePicker()"></asp:TextBox>
                    -
                <asp:TextBox ID="TextBox2" runat="server" Width="81" CssClass="Wdate" onfocus="new WdatePicker()"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div style="width: 1500px">
            <table width="99%" style="word-break: keep-all; word-wrap: normal;">
                <tr>
                    <td style="border: rgb(147,226,244) solid 1px">
                        <asp:GridView ID="gv_browOrder" runat="server" AutoGenerateColumns="False" OnRowDataBound="gv_browOrder_RowDataBound"
                            OnRowCommand="gv_browOrder_RowCommand" BackColor="#F8FBFD" CssClass="tablemb bordercss"
                            Width="100%">
                            <Columns>
                                <%--<asp:BoundField HeaderText="错误信息" DataField="error" Visible="false">
                                    <ItemStyle Wrap="false" />
                                </asp:BoundField>--%>
                                <asp:TemplateField Visible="false">
                                    <HeaderTemplate>
                                        发货
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkbtnOk" runat="server" Text="发货" CommandName="E" Visible='<%# Eval("isSend").ToString()=="0" %>' 
                                           CommandArgument='<%#Eval("OrderID")+":"+Eval("Number")+":"+Eval("defraytype")+":"+Eval("DefrayState")+":"+Eval("orderExpectNum")+":"+Eval("isAgain")+":"+Eval("OStoreID") %>'
                                            OnClientClick="return confirm('您确定要发货吗?')"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <HeaderTemplate>
                                        修改
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkbtnModify" runat="server" CommandName="M"
                                            CommandArgument='<%#Eval("OrderID")+":"+Eval("Number")+":"+Eval("defraytype")+":"+Eval("DefrayState")+":"+Eval("orderExpectNum")+":"+Eval("isAgain")+":"+Eval("OStoreID") %>'> <%=this.GetTran("000259", "修改")%></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="会员编号" DataField="number"></asp:BoundField>
                                <asp:BoundField HeaderText="会员姓名" DataField="name"></asp:BoundField>
                               <%-- <asp:BoundField HeaderText="报单类型" DataField="orderType"></asp:BoundField>

                                <asp:BoundField HeaderText="支付状态" DataField="PayStatus"></asp:BoundField>
                                <asp:TemplateField HeaderText="支付方式">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Common.GetOrderPayType(DataBinder.Eval(Container.DataItem, "defraytype").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField HeaderText="期数" DataField="orderExpectNum"></asp:BoundField>
                                <asp:BoundField HeaderText="审核期数" DataField="PayExpectNum"></asp:BoundField>
                                <asp:BoundField HeaderText="订单号" DataField="OrderId"></asp:BoundField>
                                <asp:TemplateField HeaderText="报单类型">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Common.GetOrderType(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "orderType")))%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="金额">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalMoney" CssClass="labb" name="lblTotalMoney" runat="server" Text='<%# Eval("totalMoney", "{0:n2}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="FTC" DataField="totalpv" ItemStyle-CssClass="lab1" DataFormatString="{0:n2}" HtmlEncode="False">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="确认">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Getgsqueren(DataBinder.Eval(Container.DataItem, "gsqueren").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="报单时间" ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# GetRegisterDate(DataBinder.Eval(Container.DataItem, "OrderDate").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="收货人手机号">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConMobilPhone" CssClass="lab" name="lblConMobilPhone" runat="server" Text='<%# Eval("ConMobilPhone") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="收货人地址">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConAddress" CssClass="lab" name="lblConAddress" runat="server" Text='<%# Eval("ConAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="备注" DataField="remark" Visible="false"></asp:BoundField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <%# "<span title='"+Eval("remark").ToString()+"'>"+Eval("remark").ToString().Substring(0, (Eval("remark").ToString().Length > 5) ? 5 : Eval("remark").ToString().Length)+"</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        查看
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <img src="images/fdj.gif" />
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("OrderId") %>'
                                            CommandName='<%#Eval("OStoreId") %>' OnCommand="LinkButton1_Command"> <%=this.GetTran("000440", "查看")%></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        删除
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkbtnDelete" runat="server" CommandName="D" Visible='<%#(Convert.ToInt32( Eval("OrderExpectNum") )>(maxExpect-1)  &&Eval("defraystate").ToString()=="1"&&Eval("isAgain").ToString()=="0" )||(Eval("defraystate").ToString()=="0") %>'
                                            CommandArgument='<%#Eval("OrderID")+":"+Eval("Number")+":"+Eval("defraytype")+":"+Eval("DefrayState")+":"+Eval("orderExpectNum")+":"+Eval("isAgain")+":"+Eval("OStoreID") %>'><%=this.GetTran("000022", "删除")%></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                </td></tr>
                            <tr>
                                <th>
                                    <%=this.GetTran("000742", "错误信息")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000811", "确认")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000259", "修改")%>
                                </th>

                                <th>
                                    <%=this.GetTran("000024", "会员编号")%> 
                                </th>
                                <th>
                                    <%=this.GetTran("000025", "会员姓名")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000455", "报单类型")%>
                                </th>
                             <%--   <th>
                                    <%=this.GetTran("000775", "支付状态")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000186", "支付方式")%>
                                </th>--%>
                                <th>
                                    <%=this.GetTran("000045", "期数")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000780", "审核期数")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000079", "订单号")%> 
                                </th>
                                <th>
                                    <%=this.GetTran("000322", "金额")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000000", "USDT")%>
                                </th>
                                s<th>
                                    <%=GetTran("006048", "公司确认")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000057", "注册日期")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000000", "收货人手机号")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000000", "收货人地址")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000078", "备注")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000440", "查看")%>
                                </th>
                                <th>
                                    <%=this.GetTran("000022", "删除")%> 
                                </th>
                            </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <%=this.GetTran("001742", "暂无数据")%>
                                !    
                            </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="#F1F4F8" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <span style="font-size: 12px; margin-left: 28px; float: left;"><%=GetTran("000247", "总计 ")%> </span><span style="font-size: 12px; float: right;"><%=GetTran("007549", "本页金额合计")%>：<asp:Label ID="lab_bjehj" ForeColor="red" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp; 本页报单业绩合计：<asp:Label ID="lab_bjfhj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;<%=GetTran("007552", "查询金额总计")%>：<asp:Label ID="lab_cjezj" runat="server" ForeColor="Red"></asp:Label>&nbsp;&nbsp;&nbsp;查询业绩总计：<asp:Label ID="lab_cjfzj" runat="server" ForeColor="Red"></asp:Label></span>
        </div>
        <uc2:Pager ID="Pager1" runat="server" Visible="false" />
        <br />
        <div style="width: 100%">
            <table>
                <tr>
                    <td valign="top">
                        <div id="cssrain" style="width: 100%">
                            <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                                <tr>
                                    <td width="150">
                                        <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                            <tr>
                                                <td class="sec2" onclick="secBoard(0)">
                                                    <span id="span1" title=""><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                                </td>
                                                <td class="sec1" onclick="secBoard(1)">
                                                    <span id="span2" title=""><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <a href="#">
                                            <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                                onclick="down2()" /></a>
                                    </td>
                                </tr>
                            </table>
                            <div id="divTab2" style="display: none;">
                                <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="Table1">
                                    <tbody style="display: block" id="tbody0">
                                        <tr>
                                            <td valign="bottom" style="padding-left: 20px">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="images/anextable.gif" OnClick="btnExcel_Click" />

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
                                                        <tr>
                                                            <td>１、<%=this.GetTran("006935", "查看会员的所有报单；")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>２、<%=this.GetTran("006936", "删除未支付的报单；")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>３、<%=this.GetTran("006937", "对用现金支付的已支付报单可以修改和删除（仅限当前期的报单）。")%>
                                                            </td>
                                                        </tr>
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
        </div>
    </form>
</body>
<script type="text/javascript" language="javascript">
    function Check() {
        confirm('<%=this.GetTran("001366", "您确定要删除吗？")%>');
    }

    //window.onload = heji();
    window.onload = function heji() {
        var labb = 0;
        var lab1 = 0;
        $('.labb').each(
            function () {
                labb = parseFloat($(this).text().replace(',', '')) + labb;
            }
        );
        $('#lab_bjehj').html(labb == 0 ? "0" : labb.toFixed(4));
        $('.lab1').each(
            function () {
                lab1 = parseFloat($(this).text().replace(',', '')) + lab1;
            }
        );
        $('#lab_bjfhj').html(lab1 == 0 ? "0" : lab1.toFixed(4));
    }
</script>
</html>
