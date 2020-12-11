<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Selldetails.aspx.cs" Inherits="Selldetails" %>

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
    <title> 交易记录明细</title>
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
       

        .mx
        {
            width: 100%;
        }

        .mx1
        {
            text-align: center;
            font-size: 20px;
            margin-top: 5%;
        }


        .mx2
        {
            float: left;
            width: 25%;
            font-size: 16px;
            margin-top: 5%;
            margin-left: 2%;
        }

        .mx3
        {
            float: right;
            width: 70%;
            font-size: 16px;
            text-align: right;
            margin-top: 5%;
            margin-right: 2%;
        }
    </style>
</head>

<body>
    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">交易记录明细</span>
            </div>
        </div>
        <div class="midls"> 
            <div class="topshow"> <p  ><a class="btn btn-primary" style="background-color:#517bcd;border:none;">卖</a></p>
                <p> <asp:Literal ID="litjbbb" Text="" runat="server"></asp:Literal></p>
                <p style="font-size:16px;font-weight:normal;"> <asp:Literal ID="litstate" runat="server"></asp:Literal></p>
                <div class="jindubg" > <asp:Literal ID="litjindu" runat="server"></asp:Literal> </div>
            </div>

              <ul class="sellif">
                  <asp:Literal ID="litseller" Text="" runat="server"></asp:Literal>
               <%-- <li><div class="fdiv">卖方账号</div><div class="sdiv">8888888888</div></li>
                <li><div class="fdiv">卖方姓名</div><div class="sdiv">张波</div></li>
                    <li><div class="fdiv">卖出石斛积分</div><div class="sdiv">200</div></li>
                <li><div class="fdiv">卖出价格</div><div class="sdiv">1.05</div></li>
                <li><div class="fdiv">卖出市值</div><div class="sdiv">&yen;210</div></li>
                  <li><div class="fdiv">收款类型</div><div class="sdiv">银行卡</div></li>
                <li><div class="fdiv">收款银行</div><div class="sdiv">中国银行</div></li>
                   <li><div class="fdiv">支行名称</div><div class="sdiv">泗凤路支行</div></li>
                     <li><div class="fdiv">收款卡号</div><div class="sdiv">6335444545454445555</div></li>
                   <li><div class="fdiv">支付宝</div><div class="sdiv">aaaa@dd.com</div></li>
                   <li><div class="fdiv">微信</div><div class="sdiv">1525555555</div></li> --%>
            </ul>

            <ul class="buyif">
                <asp:Literal ID="litbuyerinfo" runat="server"></asp:Literal>
               <%-- <li><div class="fdiv">买方账号</div><div class="sdiv">8888888888</div></li>
                 <li><div class="fdiv">买方姓名</div><div class="sdiv">李*文</div></li>
                <li><div class="fdiv">买入石斛积分</div><div class="sdiv">200</div></li>
                <li><div class="fdiv">买入价格</div><div class="sdiv">1.05</div></li>
                <li><div class="fdiv">买入市值</div><div class="sdiv">&yen;210</div></li>--%>
                 
            </ul>



        </div>
        <div class="confirmRemit" id="imgback" onclick="hideimg()">
            <img id="imgsrc" style="width:80%;margin-left:10%;"/>
        </div>
        <div id="bakg"></div>
        <!-- #include file = "comcode.html" -->

        <script>
            $(function () {
                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

                })

            });

            //交易完成列表
            function shoukuan(ele, wdid) {
                if (confirm("确认已收到对方转账了吗？")) {
                    $(ele).attr("class", "btn btn-default btn-lg");
                    $(ele).attr("onclick", "");
                    var rec = AjaxClass.ConfirmWithdrawSK(wdid).value;
                    if (rec == "-1") {
                        alert("长时间未操作，请先去登录！");
                        window.location.href = 'index.aspx';
                        return;
                    }else 
                    if (rec == "1") {
                        alert("收款成功！");
                        window.location.href = 'sellercenter.aspx?tp=3';
                    }else 
                    if (rec == "2") {
                        alert("已收款成功，不能重复操作！");
                        window.location.href = 'sellercenter.aspx?tp=3';
                    }

                }
            }

            function showimg(srcc) {
                $("#imgback").show();
                $("#imgsrc").attr('src', srcc);
                $("#bakg").show();
            }

            function hideimg() {
                $("#bakg").hide();
                $("#imgback").hide();
            }
        </script>
    </form>
</body>
</html>

