<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberTop.ascx.cs" Inherits="UserControl_MemberTop" %>
<link href="../member/css/style.css" rel="stylesheet" type="text/css" />

<%--<script src="../JS/jquery-3.1.1.min.js"></script>--%>
<%--<script src="../Member/js/jquery-1.4.3.min.js"></script>--%>
<script>
    $(function () {
        $('.U_li').hover(function () {
            $(this).children('span').addClass('cur').parent('.U_li').siblings('.U_li').children('span').removeClass('cur');
            $(this).children('.ol_bg').show().parent('.U_li').siblings('.U_li').children('.ol_bg').hide();
        }, function () {
            $('.U_li span').removeClass('cur');
            $('.U_li .ol_bg').hide();
        })
    })

</script>
 <script type="text/javascript">
     $(function () {
         var lang = $('#lang').text();
         if (lang != "L001") {
             $('..nav_in .U_li span').css({'font-size': '12px' })
         }
     })
    </script>
<style>
    @font-face {
        font-family: 'iconfont';
        src: url('../Member/fonts/iconfont.eot');
        src: url('../Member/fonts/iconfont.eot?#iefix') format('embedded-opentype'), url('../Member/fonts/iconfont.woff') format('woff'), url('../Member/fonts/iconfont.ttf') format('truetype'), url('../Member/fonts/iconfont.svg#iconfont') format('svg');
    }

    .iconfont {
        font-family: "iconfont" !important;
        font-size: 18px;
        font-style: normal;
        -webkit-font-smoothing: antialiased;
        -webkit-text-stroke-width: 0.2px;
        -moz-osx-font-smoothing: grayscale;
        padding-right: 3px;
        position: absolute;
        left: 0;
    }
</style>
<!--顶部信息,logo,help-->
 <b id="lang" style="display: none;"><%=Session["LanguageCode"] %></b>
<div class="header">
    <div class="header_in">
        <div class="logo"> <img src="../images/img/logo.png" style="height:60px;" /></div>
        <div class="nav">
            <div class="nav_in">
                <ul>
                    <!--首页-->
                    <li><a class="U_li" href="../member/first.aspx" style="color: #fff;"><span><%=tran.GetTran("001478", "首页")%></span></a></li>


                    <!--团队结构-->
                    <li class="U_li">
                        <span><%=tran.GetTran("007283", "团队结构")%></span>
                        <div class="ol_bg">
                            <ol>
                                <li  id="tj" runat="server"><a href="../member/MemberNetMapFrame.aspx?isAnzhi=tj"><i class="iconfont">&#xe605;</i><%=tran.GetTran("000395", "推荐网络图")%></a></li>
                                <li  id="az" runat="server"><a href="../member/GraphNetFrame.aspx"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe637;</i><%=tran.GetTran("000366", "安置网络图")%></a></li>
                                <li  id="lianlu" runat="server"><a href="../member/QueryLinkNetworkView.aspx"><i class="iconfont">&#xe632;</i><%=tran.GetTran("007284", "会员链路图")%></a></li>
                            </ol>
                            <div class="ol_bg_b"></div>
                        </div>
                    </li>

                    <!--报单管理-->
                    <li class="U_li"><span><%=tran.GetTran("007049", "报单管理")%></span>
                        <div class="ol_bg">
                            <ol>
                                <li><a href="../RegisterMember/RegisterMember.aspx"><i class="iconfont" style="font-size: 22px;">&#xe602;</i><%=tran.GetTran("007285", "注册新人")%></a></li>
                                <li><a href="../member/BrowseMemberOrders.aspx"><i class="iconfont" style="font-size: 24px; left: -2px">&#xe62c;</i><%=tran.GetTran("004009", "注册浏览")%></a></li>
                                <%--<li><a href="../member/AuditingMemberRegister.aspx">注册单支付</a></li>--%>
                                <li><a href="../RegisterMember/ShopingList.aspx?type=new"><i class="iconfont" style="font-size: 20px;">&#xe757;</i><%=tran.GetTran("001174", "复消报单")%></a></li>
                                <%--        <li><a href="../member/membertrade.aspx"><i class="iconfont" style="font-size:24px;left:-2px">&#xe757;</i><%=tran.GetTran("001174", "复消报单")%>2</a></li>--%>

                                <li><a href="../RegisterMember/ShopingList.aspx?type=Sj"><i class="iconfont" style="font-size: 20px;">&#xe757;</i><%=tran.GetTran("008111", "升级报单")%></a></li>
                                <li><a href="../RegisterMember/ShopingList.aspx?type=Fxth"><i class="iconfont" style="font-size: 20px;">&#xe757;</i><%=tran.GetTran("008112", "复消提货报单")%></a></li>
                                <li><a href="../member/memberorder.aspx"><i class="iconfont" style="font-size: 22px;">&#xe62c;</i><%=tran.GetTran("001567", "报单浏览")%></a></li>
                                <li><a href="../member/AuditingMemberOrder.aspx"><i class="iconfont">&#xe628;</i><%=tran.GetTran("007286", "报单支付")%></a></li>
                                <li><a href="../member/Receiving.aspx"><i class="iconfont">&#xe60a;</i><%=tran.GetTran("004025", "收货确认")%></a></li>
                                <li><a href="../member/RegisterStore.aspx"><i class="iconfont">&#xe60a;</i><%=tran.GetTran("006614", "申请服务机构")%></a></li>
                                <%--<li><a href="../member/RefundmentOrderForMemberAdd.aspx">退货申请</a></li>
        <li><a href="../member/RefundmentOrderForMemberList.aspx">退货查询</a></li>--%>
                            </ol>
                            <div class="ol_bg_b"></div>
                        </div>

                    </li>

                    <!--财务管理-->
                    <li class="U_li"><span><%=tran.GetTran("001657", "财务管理")%></span>
                        <div class="ol_bg">
                            <ol>
                                <li><a href="../member/DetialQuery.aspx"><i class="iconfont">&#xe62c;</i><%=tran.GetTran("006064", "最新奖金")%></a></li>
                                <li><a href="../member/BasicSearch.aspx"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62c;</i><%=tran.GetTran("004052", "历史奖金")%></a></li>
                                <li><a href="../member/OnlinePayment.aspx"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62e;</i><%=tran.GetTran("005864", "账户充值")%></a></li>
                                <li><a href="../member/ResultBrowse.aspx"><i class="iconfont" style="font-size: 24px; left: -2px">&#xe62c;</i><%=tran.GetTran("005843", "充值浏览")%></a></li>
                               <%-- <li><a href="../member/PhoneRecharge.aspx"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62e;</i><%=tran.GetTran("008028", "手机充值")%></a></li>
                                <li><a href="../member/FindRecharge.aspx"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62e;</i><%=tran.GetTran("008028", "手机充值") + tran.GetTran("000340", "查询")%></a></li>--%>
                                <li><a href="../member/RemSecan.aspx"><i class="iconfont" style="font-size: 20px; left: -2px">&#xe679;</i> <%=tran.GetTran("9112", "转账汇款管理")%></a></li>
                                <li><a href="../AccountDetail/AccountDetail.aspx?type=AccountXJ"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62c;</i><%=tran.GetTran("007288", "现金账户明细")%></a></li>
                                <li><a href="../AccountDetail/AccountDetail.aspx?type=AccountXF"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62c;</i><%=tran.GetTran("007289", "消费账户明细")%></a></li>
                                <li><a href="../AccountDetail/AccountDetail.aspx?type=AccountFX"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62c;</i><%=tran.GetTran("008115", "复消账户明细")%></a></li>
                                <li><a href="../AccountDetail/AccountDetail.aspx?type=AccountFXth"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62c;</i><%=tran.GetTran("008116", "复消提货账户明细")%></a></li>
                                
                                <li><a href="../AccountDetail/AccountDetail.aspx?type=AccountZSJF"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62c;</i><%=tran.GetTran("9118","赠送积分")%></a></li>
                                <li><a href="../AccountDetail/AccountDetail.aspx?type=AccountSFJF"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62c;</i><%=tran.GetTran("9119","释放积分")%></a></li>

                                 <li id="dlsblli" runat="server"><a href="../member/dlsblset.aspx"><i class="iconfont" style="font-size: 22px; left: -2px">&#xe62c;</i><%=tran.GetTran("000000","比例设置")%></a></li>

                                <li id="txsq" runat="server"><a href="../member/MemberCash.aspx"><i class="iconfont">&#xe618;</i><%=tran.GetTran("007290", "提现申请")%></a></li>
                                <li id="txsqll" runat="server"><a href="../member/MemberWithdraw.aspx"><i class="iconfont" style="font-size: 24px; left: -2px">&#xe62c;</i><%=tran.GetTran("007291", "提现申请浏览")%></a></li>
                                <li><a href="../member/MoneyManage.aspx"><i class="iconfont" style="font-size: 20px; left: -2px">&#xe679;</i><%=tran.GetTran("005866", "电子转账")%></a></li>
                                <li><a href="../member/TransferList.aspx"><i class="iconfont" style="font-size: 24px; left: -2px">&#xe62c;</i><%=tran.GetTran("007292", "转账浏览")%></a></li>
                            </ol>
                            <div class="ol_bg_b"></div>
                        </div>
                    </li>

                    <!--个性修改-->
                    <li class="U_li"><span><%=tran.GetTran("001668", "个性修改")%></span>
                        <div class="ol_bg">
                            <ol>
                                <li><a href="../member/updatePass.aspx"><i class="iconfont">&#xe61a;</i><%=tran.GetTran("001782", "密码修改")%></a></li>
                                <li><a href="../member/MemberInfo.aspx"><i class="iconfont">&#xe6a7;</i><%=tran.GetTran("004057", "资料修改")%></a></li>
                                <li><a href="../member/CurrencyDetail.aspx"><i class="iconfont">&#xe62e;</i><%=tran.GetTran("007293", "币种设置")%></a></li>
                            </ol>
                            <div class="ol_bg_b"></div>
                        </div>
                    </li>

                    <!--信息中心-->
                    <li class="U_li"><span><%=tran.GetTran("001658", "信息中心")%> </span>
                        <div class="ol_bg">
                            <ol>
                                <li><a href="../member/DownLoadFiles.aspx"><i class="iconfont">&#xe626;</i><%=tran.GetTran("004027", "资料下载")%></a></li>
                                <li><a href="../member/QueryInfomation.aspx"><i class="iconfont">&#xe62c;</i><%=tran.GetTran("004028", "公告查询")%></a></li>
                                <li><a href="../member/EmailFPage.aspx"><i class="iconfont">&#xe61c;</i><%=tran.GetTran("005149", "邮件管理")%></a></li>
                                <%--<li><a href="WriteEmail.aspx">写邮件</a></li>
        <li><a href="ReceiveEmail.aspx">收件箱</a></li>
        <li><a href="SendEmail.aspx">发件箱</a></li>
        <li><a href="UnusedEmail.aspx">废件箱</a></li>--%>
                            </ol>
                            <div class="ol_bg_b"></div>
                        </div>
                    </li>
                </ul>
            </div>
            <a class="close" href="../logout.aspx?tp=huiy"><%--<%=tran.GetTran("001652", "退出")%>--%></a>
        </div>

    </div>
</div>
<asp:HiddenField ID="dingbuuse" runat="server" />
