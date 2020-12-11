<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TixianXx.aspx.cs" Inherits="MemberMobile_TixianXx" %>
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
    <title><%=GetTran("009006", "提现申请浏览")%></title>
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

        .xs_footer li .a_cur {
            color: #77c225;
        }

        .xs_footer li:nth-of-type(2) a {
            background: url(images/img/jiangj1.png) no-repeat center 10px;
            background-size: 32px;
        }

        .xs_footer li:nth-of-type(3) a {
            background: url(images/img/xiaoxi1.png) no-repeat center 8px;
            background-size: 32px;
        }

        .xs_footer li:nth-of-type(4) a {
            background: url(images/img/anquan1.png) no-repeat center 8px;
            background-size: 27px;
        }
    </style>
</head>

<body>
    <form id="form2" runat="server">
        <div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>
          <%=GetTran("007291", "提现申请浏览")%>
           <%-- <div class="tt_r">
            </div>--%>
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
                                              <%=GetTran("006983", "审核状态")%>：<%# GetAuditState(DataBinder.Eval(Container.DataItem,"isAuditing").ToString()) %>
                                            <br />
                                            <br />
                                              <%=GetTran("006984", "提现金额")%>：   <%# Math.Round( (Convert.ToDouble(  GetWithdrawMoney(DataBinder.Eval(Container.DataItem, "WithdrawMoney").ToString()))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        )).ToString("#0.00") %>
                                           
                                            <br />
                                            <br />
                                            <%=GetTran("008150", "提现手续费")%>：  
                                            <%# Math.Round((Convert.ToDouble(  GetWithdrawMoney(DataBinder.Eval(Container.DataItem, "WithdrawSXF").ToString()))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        )).ToString("#0.00") %>
                                            <br />
                                            <br />
                                               <%=GetTran("006986", "提现时间")%>：   <%# GetWithdrawTime(DataBinder.Eval(Container.DataItem, "WithdrawTime").ToString())%>
                                            <br />
                                            <br />
                                              <%=GetTran("000923", "卡号")%>： <%# Encryption.Encryption.GetDecipherCard(DataBinder.Eval(Container.DataItem,"bankcard").ToString()) %>
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




