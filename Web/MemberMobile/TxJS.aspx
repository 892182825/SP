<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TxJS.aspx.cs" Inherits="MemberMobile_TxJS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">

    <title></title>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
    <script src="js/jquery-1.7.1.min.js"></script>
    <link rel="stylesheet" href="css/style.css">
    <script language="javascript" type="text/javascript">
        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            filterSql_II(btname);
        }
    </script>
    <script type="text/javascript">

        function tiaozhuan(hkid, hkmoney) {

            window.location.href = "/MemberMobile/DCSXX.aspx?hkid=" + hkid + " &hkmoney=" + hkmoney;
        }

    </script>
    <style>
        .changeBox ul li .changeLt {
            width: 30%;
        }

        .changeBox ul li .changeRt {
            width: 70%;
        }

            .changeBox ul li .changeRt .textBox {
                width: 80%;
            }

        .zcMsg ul li .changeRt .zcSltBox {
            width: 80%;
        }

        .zcMsg ul li .changeRt .zcSltBox2 {
            width: 39%;
        }

        #txtadvpass {
            width: 79%;
            border: 1px solid #ccc;
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

        .moneyInfo3 a {
            float: left;
            width: 25%;
            text-align: center;
            height: 30px;
            font-size: 16px;
            line-height: 30px;
        }

        .moneyInfoSlt {
            background: forestgreen;
            color: #fff;
        }

        #Button1 {
            /* display: block; */
            height: 25px;
            line-height: 25px;
            text-align: center;
            background: #85ac07;
            width: 64px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 34%;
            /* margin-top: 11%; */
        }

        #Button2 {
            display: block;
            height: 28px;
            line-height: 30px;
            text-align: center;
            background: #85ac07;
            width: 58px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 238%;
            margin-top: 11%;
        }

        #Button3 {
            display: block;
            height: 28px;
            line-height: 30px;
            text-align: center;
            background: #85ac07;
            width: 58px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 141%;
            margin-top: 11%;
        }

        #sub {
            display: block;
            height: 35px;
            line-height: 35px;
            text-align: center;
            background: #85ac07;
            width: 100%;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
        }

    </style>


</head>

<body>
    <!--页面内容宽-->
    <form id="form1" runat="server" name="form1" method="post">

        <div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>
            <%=GetTran("002272", "账户充值")%>
            <div class="tt_r">
            </div>

        </div>
        <div class="moneyInfo3" style="left: 0px; overflow: hidden; zoom: 1; width: 100%; background-color: #fff">
            <a href="OnlinePayment.aspx" >充值</a>
            <a href="DetailDHK.aspx">待汇出</a>
            <a href="DetailDCS.aspx" class="moneyInfoSlt">待查收</a>
            <a href="DetailYDZ.aspx">已到账</a>
        </div>
        <div class="minMsg minMsg2" style="display: block">
            <div class="minMsgBox">
                <div id="js">
                    <div style="margin-left: 3%">
                        <div style="font-size:18px;margin-top: 25px;"><%=GetTran("009026", "解释原因")%>：</div>
                        <div>
                            <asp:TextBox ID="txtEnote" CssClass="ctConPgTxt2" TextMode="MultiLine" Style="border: 1px solid #ccc; border-radius: 3px; width: 96%" Height="83px" runat="server" MaxLength="250" />
                        </div>
                    </div>
                    <div class="changeBtnBox zc">
                        <asp:Button ID="sub" runat="server" Text="提 交"
                            CssClass="changeBtn" OnClick="btn_Click" />
                        <%--   <input type="button" id="sub"  name="sub" value="提 交" onclick="popDiv()" /> --%>

                        <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <input type="hidden"  id="Hkid" runat="server"/>
  <!-- #include file = "comcode.html" -->
    </form>
</body>
</html>
<script type="text/jscript">
    $(function () {
        $('#bottomDiv').css('display', 'none');

        $("#Pager1_div2").css('background-color', '#FFF')
    })

    //function popDiv() {

    //    window.location.href = "/MemberMobile/OnlinePayQD.aspx";
    //}
</script>







