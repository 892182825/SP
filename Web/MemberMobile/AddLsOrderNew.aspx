<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddLsOrderNew.aspx.cs" Inherits="MemberMobile_AddLsOrderNew" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>确认订单</title>

    <link href="../CSS/bootstrap-cerulean.min.css" rel="stylesheet" />
    <link href="../css/charisma-app.css" rel="stylesheet" />




    <script src="../bower_components/jquery/jquery.min.js"></script>
    <script src="js/jquery-1.7.1.min.js"></script>

    <style type="text/css">
        body {
            background: #fff;
            font-family: "Segoe UI","Lucida Grande",Helvetica,Arial,"Microsoft YaHei",FreeSans,Arimo,"Droid Sans","wenquanyi micro hei","Hiragino Sans GB","Hiragino Sans GB W3",FontAwesome,sans-serif;
            font-weight: 400;
            color: #333;
            font-size: 1.6rem;
            line-height: 1.6;
            padding: 0;
            margin: 0;
        }
         a, a:focus, a:hover, a:active {
            outline: none;
            color: #333;
            text-decoration: none;
        }
         a {
            text-decoration: none;
            cursor: pointer;
        }
        *, :after, :before {
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

        html {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            font-size: 10px;
        }

        

        .orderSite {
            padding: 5px;
            background-color: #fff;
            margin-bottom: 5px;
            border-top: 1px solid #e6e6e6;
        }

            .orderSite p {
                font-size: 16px;
                color: #404040;
            }

            .orderSite a {
                position: relative;
                display: block;
                padding: .5rem;
                color: #666;
                border-bottom: 1px solid #e6e6e6;
            }

            .orderSite span {
                font-size: 16px;
                color: #999;
            }





        a, ins {
            text-decoration: none;
        }

       

        .am-close, .am-icon-btn, [class*=am-icon-] {
            display: inline-block;
        }

        i {
            font-style: normal;
        }

        .shopcart-list {
            overflow: hidden;
            background: #fff;
            margin-top: 1rem;
            padding-bottom: 50px;
        }

        ol, ul, li {
            padding: 0;
            margin: 0;
            list-style: none;
        }

        .shopcart-list li {
            border-bottom: 1px solid #ddd;
            padding: 3% 1rem;
            overflow: hidden;
            position: relative;
        }

        .shop-pic {
            float: left;
            width: 25%;
            width: 6.5rem;
        }

        img {
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            vertical-align: middle;
        }

        .order-mid {
            float: left;
            width: 70%;
            color: #909090;
            margin-left: 0.625rem;
            font-size: 1.4rem;
            display: -webkit-box;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
            overflow: hidden;
            white-space: normal;
            text-overflow: ellipsis;
        }

        .order-price {
            font-size: 1.6rem;
            margin-top: 8px;
        }

            .order-price i {
                font-style: normal;
                float: right;
            }

        .order-infor {
            margin: 0 3%;
            width: 94%;
            padding: 1rem 0;
        }

        .order-infor-first {
            overflow: hidden;
            margin-bottom: 5px;
        }

            .order-infor-first i {
                float: right;
                font-style: normal;
                color: #909090;
            }

        .am-icon-angle-right:before {
            content: "\f105";
        }

        [class*=am-icon-]:before {
            display: inline-block;
            font: normal normal normal 1.6rem/1 FontAwesome,sans-serif;
            font-size: inherit;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            -webkit-transform: translate(0,0);
            -ms-transform: translate(0,0);
            transform: translate(0,0);
        }

        .add-address .am-icon-angle-right {
            float: right;
            padding-right: 1rem;
        }
        div {
        display: block;
     }
        .shop-fix {
            height: 5rem;
            position: fixed;
            bottom: 0px;
            background: #fff;
            width: 100%;
            padding: 0 3%;
        }
        .order-text {
            float: left;
            line-height: 5rem;
            font-size: 1.8rem;
            color: #666;
        }
        .js-btn {
            float: right;
            border-radius: 5px;
            background: #cb2527;
            color: #fff;
            padding: 3px 12px;
            margin-top: 14px;
        }
        .margindiv {
                background: #eee; height: 10px;
                position: fixed;
                bottom: 60px;
                width: 100%;
                padding: 0 3%; 
        }

          div.orderSite i {
                    float: right;
                    font-size: 1px;
                    padding: 5px;
                    text-shadow: 2px 2px 5px hsl(0, 0%, 61%);
                    color:#666;
                }
    </style>

    <script type="text/javascript">

        function abled() {

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
            <div class="navbar navbar-default" role="navigation" id="herderdiv">
                <div class="navbar-inner">
                    <a class="btn btn-primary btn-lg" style="float: left; padding: 2px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                    <span style="color: #fff; font-size: 18px; margin-left: 35%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">确认订单</span>
                </div>
            </div>

            <div class="orderSite" id="divaddress">
              
            </div>
            
            <div style="background: #eee; height: 10px;"></div>

            <ul class="shopcart-list" style="padding-bottom: 0;" id="ulshoplist">
               
            </ul>

            <ul class="order-infor">
                <li class="order-infor-first">
                    <span>商品总计：</span>
                    <i >$<asp:Label ID="lbtotalPrice" runat="server" Text="0" ></asp:Label></i>
                </li>
                <li class="order-infor-first">
                    <span>运费：</span>
                    <i>
                        <asp:Label ID="labCarryMoney" runat="server" Text="0.00"></asp:Label></i>
                </li>
            </ul>

            <div class="margindiv"></div>
            <div style="height: 55px;"></div>

            <div class="shop-fix">
	    	    <div class="order-text">
	    		    应付总额：<span>$<asp:Label ID="labrealparice" runat="server" Text="0"></asp:Label></span>
	    	    </div>
	    	    <a href="javascript:;" class="js-btn" runat="server" onserverclick="StartRecord_click">提交订单</a>
	        </div>

            <asp:HiddenField ID="AgainTime" Value="0" runat="server" />
    </form>

    <script type="text/javascript">
        $(function () {
            getaddress();
            getDetail();
        });
        function getDetail() {
            var res = window.AjaxMemShopCart.AddLsOrderDetail().value;
            if (res !== "") {
                $("#ulshoplist").append(res);
            }
        }

        function getaddress() {
            var res = window.AjaxMemShopCart.AddLsOrderAddress().value;
            if (res !== "") {
                $("#divaddress").html(res);
            }
        }
    </script>
</body>
</html>
