<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaodanXx.aspx.cs" Inherits="MemberMobile_BaodanXx" %>
<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>
<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title><%=GetTran("005011", "报单浏览")%></title>
    <link rel="stylesheet" href="CSS/style.css">
    <script type="text/javascript">
        $(function () {
            a.dianji();
        })
        var a = {
            dianji: function () {
                $("#ucPagerMb1").css('display', 'none');
            },
        }

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

     
    </style>
</head>

<body>
    <form id="form2" runat="server">
        <div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>
           <%=GetTran("005011", "报单浏览")%>
           
        </div>
        <div class="middle">

            <div class="minMsg minMsg2" style="display: block">
                <div class="minMsgBox">
                    <div>
                        <ol>
                            <asp:Repeater ID="rep_km" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div>
                                            <%=GetTran("000024", "会员编号")%>：<%#Eval("Number") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000775", "支付状态")%>：  <%# GetPayStatus(DataBinder.Eval(Container.DataItem, "defraystate").ToString())%>
                                            <br />
                                            <br />
                                            <%=GetTran("000186", "支付方式")%>：<%# GetOrderType(DataBinder.Eval(Container.DataItem, "defrayType"))%>
                                            <br />
                                            <br />
                                            <%=GetTran("007416", "收货途径")%>：<%# Common.GetSendWay(DataBinder.Eval(Container.DataItem,"SendWay").ToString()) %>
                                            <br />
                                            <br />
                                            <%=GetTran("001345", "发货方式")%>：<%# Common.GetSendType(DataBinder.Eval(Container.DataItem, "Sendtype").ToString())%>
                                            <br />
                                            <br />
                                            <%=GetTran("000045", "期数")%>： <%#Eval("OrderExpectNum") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000079", "订单号")%>：<%#Eval("OrderID") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000322", "金额")%>：<%#  (Convert.ToDouble( Eval("totalMoney", "{0:n2}"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2")  %>
                                            <br />
                                            <br />
                                            <%=GetTran("000414", "积分")%>：<%# Eval("Totalpv", "{0:n2}") %>
                                            <br />
                                            <br />
                                            <%=GetTran("007535", "订购类型")%>：<%# Common.GetMemberOrderType (DataBinder.Eval(Container.DataItem, "ordertype").ToString())%>
                                            <br />
                                            <br />
                                            <%=GetTran("000510", "订购日期")%>：<%# DateTime.Parse(Eval("orderdate").ToString()).AddHours(8) %>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                        <ol>
                             <asp:Repeater ID="rep_km1" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div>
                                            <%=GetTran("000501", "产品名称")%>：<%#Eval("ProductName") %>
                                            <br />
                                            <br />
                                            <div style="float: left">
                                                <%=GetTran("000882", "产品型号")%>：<%#Eval("ProductTypeName") %>
                                            </div>
                                            <div style="float: right">
                                                <%=GetTran("000505", "数量")%>：<%#Eval("Quantity") %>
                                            </div>
                                            <br />
                                                 <br />
                                            <div style="float: left">
                                                <%=GetTran("000503", "单价")%>：<lable style="color: #e06f00"><%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%>

                        <%#  (Convert.ToDouble( Eval("Price", "{0:n2}"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2")  %></lable>
                                            </div>
                                            <div style="float: right">
                                                <%=GetTran("000414", "积分")%>：<lable style="color: #e06f00"><%# Eval("Price", "{0:n2}") %></lable>
                                            </div>
                                             <br />
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>

                    </div>
                </div>
            </div>
        </div>
      <!-- #include file = "comcode.html" -->

        <script>
            $(function () {
                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

                })

            })
        </script>
      <%--  <uc1:ucPagerMb ID="ucPagerMb1" runat="server" />--%>
    </form>
</body>
</html>



