<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditingMemberOrder.aspx.cs" Inherits="Member_AuditingMemberOrder" %>

<%@ Register Src="../UserControl/MemberPager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="x-ua-compatible" content="ie=7" />
<head id="Head1" runat="server">
    <title>报单支付</title>

    <link href="CSS/detail.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../javascript/My97DatePicker/WdatePicker.js"></script>
        <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />
        <style type="text/css">
        span input {
            width: 7%;
        }

        label {
            float: left;
        }
        #Pager1_div2 {
            background-color:white;

        }
    </style>
    <script type="text/javascript" language="javascript">
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

    <script type="text/javascript" language="javascript">
        function secBoard(n) {
            for (i = 0; i < secTable.cells.length; i++)
                secTable.cells[i].className = "sec1";
            secTable.cells[n].className = "sec2";
            for (i = 0; i < mainTable.tBodies.length; i++)
                mainTable.tBodies[i].style.display = "none";
            mainTable.tBodies[n].style.display = "block";
        }
    </script>

</head>


<body>
    <form id="form2" runat="server">

        <uc1:top runat="server" ID="top" />
        <div class="rightArea clearfix">
            <div class="rightAreaIn">
                <div class="fiveSquareBox clearfix searchFactor">
                    <span><%=GetTran("000838","查询条件")%>：</span>
                    <span class="onePart">
                        <asp:DropDownList ID="ddlQiShu" CssClass="ctConPgFor" runat="server">
                        </asp:DropDownList>
                        <span>&nbsp <%=GetTran("001429","报单日期") %>：</span>
                        <asp:TextBox ID="txtDate" style="margin-top: 5px;" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                        <span> &nbsp  <%=GetTran("000205","到") %> &nbsp </span>

                        <asp:TextBox ID="txtDateEnd" style="margin-top: 5px;" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Height="27px" Width="47px" Style="margin-top: 1px; margin-left: 8px; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="搜 索" OnClick="btnSearch_Click" CssClass="anyes" />
                    </span>


                </div>
                <div class="noticeEmail width100per mglt0">
                    <div class="pcMobileCut">
                        <div class="noticeHead">
                            <div>
                                <i class="glyphicon glyphicon-file"></i>
                                <span><%=GetTran("007286","报单支付") %></span>
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
                    <HeaderStyle HorizontalAlign="Center" CssClass="ctConPgTab"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#F1F4F8" />
                    <Columns>
                        <asp:TemplateField HeaderText="查看明细">
                            <HeaderTemplate>
                                <span>查看明细 </span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton style="width:auto" ID="hlnk" runat="server" ImageUrl="images/view-button.png" PostBackUrl='<%# Eval("OrderID","ShowOrderDetails.aspx?byy={0}") %>'></asp:ImageButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                支付
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton style="width:auto" ID="HyperLinkPayMent" OnCommand="linkbtnOK_Click" CommandArgument='<%# Eval("OrderID") %>' ImageUrl="images/view-button1-.png" runat="server"></asp:ImageButton>
                                <input type="hidden" id="HidDefrayType" value='<%#DataBinder.Eval(Container,"DataItem.defraytype") %>' name="his" runat="server" />
                                <input type="hidden" id="HidDefrayState" value='<%#DataBinder.Eval(Container,"DataItem.zhifu") %>' name="his" runat="server" />
                                <input type="hidden" id="HidOrderID" value='<%#DataBinder.Eval(Container,"DataItem.OrderID") %>' name="hids" runat="server" />
                                <input type="hidden" id="HidExpectNum" value='<%#DataBinder.Eval(Container,"DataItem.OrderExpectNum") %>' name="hidnum" runat="server" />
                                <input type="hidden" id="HidTotalMoney" value='<%#DataBinder.Eval(Container,"DataItem.totalmoney") %>' name="hidm" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                修改
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton style="width:auto" ID="linkbtnModify" ImageUrl="images/view-button2.png" runat="server" CommandArgument='<%#Eval("storeId")+":"+Eval("OrderID")+":"+Eval("number")%>'
                                    OnCommand="linkbtnModify_Command"></asp:ImageButton>
                            </ItemTemplate>

                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                删除
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton style="width:auto" ID="linkbtnDelete" runat="server" CommandName="Dele" ImageUrl="images/view-button3.png"
                                    CommandArgument='<%#Eval("OrderID")+":"+Eval("number")+":"+Eval("storeId") %>' OnClientClick="return confirm('确定删除会员报单吗？');" OnCommand="linkbtnDelete_Click"></asp:ImageButton>
                            </ItemTemplate>

                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="支付状态">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# GetPayStatus(DataBinder.Eval(Container.DataItem, "zhifu").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false" HeaderText="支付方式">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# GetDefrayName(DataBinder.Eval(Container.DataItem, "defrayType").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货途径">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Common.GetSendWay(DataBinder.Eval(Container.DataItem,"SendWay").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发货方式">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Common.GetSendType(DataBinder.Eval(Container.DataItem, "Sendtype").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OrderExpectNum" HeaderText="期数"></asp:BoundField>
                        <asp:BoundField DataField="OrderID" HeaderText="订单号"></asp:BoundField>
                        <asp:TemplateField HeaderText="金额" ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalMoney" name="lblTotalMoney" runat="server" Text='<%# Eval("totalMoney", "{0:n2}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Totalpv" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                            HeaderText="积分">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="StoreID" HeaderText="确认店铺" Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="订购类型">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Common.GetMemberOrderType (DataBinder.Eval(Container.DataItem, "fuxiaoname").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订购日期">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("orderdate")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        <table cellspacing="1" cellpadding="1" width="100%">
                            <tr class="ctConPgTab">
                                <th>
                                    <%=GetTran("000811", "明细")%>
                                </th>
                                <th>
                                    <%=GetTran("000938", "支付")%>
                                </th>
                                <th>
                                    <%=GetTran("000259", "修改")%>
                                </th>
                                <th>
                                    <%=GetTran("000022", "删除")%>
                                </th>
                                <th>
                                    <%=GetTran("007416", "收货途径")%>
                                </th>

                                <th>
                                    <%=GetTran("001345", "发货方式")%>
                                </th>
                                <th>
                                    <%=GetTran("000045", "期数")%>
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
                                    <%=GetTran("007535", "订购类型")%>
                                </th>
                                <th>
                                    <%=GetTran("000510", "订购日期")%>
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <uc1:Pager ID="Pager1" runat="server" />

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
            $('#rtbiszf').css({ 'width': '19%', 'margin-left': '5px' });
            $('#rtbiszf input').css({ 'width': '10%', 'top': '-8px', 'margin-left': '5px' });
            $('#Pager1_pageTable').css('margin-right', '0px');
            $('#Pager1_btn_submit').css('Height', '22px');
            $('#Pager1_btn_submit').css('width', '70px');
            $('#Pager1_pageTable').css('color', '#333');
            $('#Pager1_ShowTable').css('color', '#333');
        })
    </script>
</body>

</html>
--%>




<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditingMemberOrder.aspx.cs" Inherits="Member_AuditingMemberOrder" %>
<%@ Register Src="../Membermobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title><%=GetTran("007286", "报单支付")%></title>
    <link rel="stylesheet" href="CSS/style.css">
    <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
    <script src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            a.dianji();
        })
        var a = {
            dianji: function () {
                $(".DD").on("click", function () {
                    location.href = "/MemberDateUpdatePhone/Index?";
                })
            },
        }

    </script>
        <script type="text/jscript">
        $(function () {
            $('#rdbtnType label').css('float', 'left');
            $('#rtbiszf').css({ 'width': '19%', 'margin-left': '5px' });
            $('#rdbtnType input').css({ 'width': '5%', border: 0, 'margin': '5px' });
            $('#Pager1_pageTable').css('margin-right', '0px');
            $('#Pager1_btn_submit').css('Height', '22px');
            $('#Pager1_btn_submit').css('width', '50px');
            $('#Pager1_pageTable').css('color', '#333');
            $('#Pager1_ShowTable').css('color', '#333');
        })
    </script>
    <style>
        body {
            padding: 50px 2% 60px;
        }

        .xs_footer li a {
            display: block;
            padding-top: 40px;
            background: url(images/img/shouy1.png) no-repeat center 8px;
            background-size: 32px;
        }

        .xs_footer li .a_cur {
            color: #77c225;
        }

      
        .fiveSquareBox {
            margin-bottom:4px;
        }
        input[type=checkbox] {
            float: right;
    width: auto;
    margin-right: 45px;
        }
        .proLayerLine ul li {
            overflow:hidden;
            width:50%;
            float:left;
            line-height:28px;
        }
        .proLayerLine ul {
            overflow:hidden
        }

          .rq{width :100%}   
    </style>
    <script type="text/javascript">
        $(function(){
            var lang = $("#lang").text();
            if (lang != "L001") {
                // alert("AuditingMemberOrder");
                $(".rq").css("width", "100%");
            }
        })
    </script>
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form2" runat="server">
        <div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>

        <%=GetTran("007286", "报单支付")%>
           
        </div>
        <div style="overflow:hidden">
            <div id="qq2" class="fiveSquareBox clearfix searchFactor">
                <span style="width: 50%;overflow:hidden;line-height:30px;float:left">
                    <span style="float:left"><%=GetTran("000838","查询条件")%>：</span>
                      <asp:DropDownList ID="ddlQiShu" CssClass="ctConPgFor" runat="server" Style="width: 50%;float:left;margin-top: 3%;">
                        </asp:DropDownList>
                </span>
                <span style="overflow: hidden">
                    <span style="float: left" class="rq"><%=GetTran("001429","报单日期") %>：</span>

                    <asp:TextBox ID="txtDate" Style="margin-top: 5px; width: 31%" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                    <span style="float: left; width: 9%; text-align: center"><%=GetTran("000205","到") %></span>
                    <asp:TextBox ID="txtDateEnd" Style="margin-top: 5px; width: 31%" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                </span>
               <asp:Button ID="btnSearch" runat="server" Height="27px" Width="100%" Style="margin-top: 1px; margin-left: -1px; padding: 4px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="搜 索" OnClick="btnSearch_Click" CssClass="anyes" />
            </div>
        </div>
        <div class="middle">

            <div class="minMsg minMsg2" style="display: block">

                <div class="minMsgBox">
                    <div class="dianji">
                        <ol>
                           <asp:Repeater ID="rep_TransferList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div>
                                            <a href='<%# Eval("OrderID","BaodanZfXx.aspx?OrderID={0}") %>'>
                                                <%=GetTran("000079", "订单号")%>： <%#Eval("OrderID") %>
                                                <label style="color: #e06f00;"> <%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%>
                                                    <%#  (Convert.ToDouble(  Eval("totalMoney", "{0:n2}"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2")  %></label>
                                        </div>
                                        <p>
                                            <%#  DateTime.Parse(Eval("orderdate").ToString()).AddHours(8) %>
                                            <label style="color:#666"><%# GetPayStatus(DataBinder.Eval(Container.DataItem, "zhifu").ToString())%> </label>
                                        </p>
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
      <!-- #include file = "comcode.html" -->

        <script type="text/javascript">
            $(function () {
                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

                })

            })
        </script>
            <uc1:PageSj ID="Pager1" runat="server" />
    </form>
</body>
</html>

