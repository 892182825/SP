﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellerCenter.aspx.cs" Inherits="Member_OnlinePayment" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">

    <title>交易中心</title>
 <%--   <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>--%>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
       <script src="../bower_components/jquery/jquery.min.js"></script>
    <link rel="stylesheet" href="css/style.css">
      <style>
        .zcMsg ul li .changeRt .zcSltBox
        {
            width: 80%;
        }

        .zcMsg ul li .changeRt .zcSltBox2
        {
            width: 39%;
        }

        #txtadvpass
        {
            width: 79%;
            border: 1px solid #ccc;
        }



        .moneyInfo3 a
        {
            float: left;
            width: 25%;
            text-align: center;
            height: 30px;
            font-size: 16px;
            line-height: 30px;
        }

        .moneyInfoSlt
        {
            background: forestgreen;
            color: #fff;
        }
       
        .changeRt ul {
         width:100%;
        } .changeRt ul li{
        list-style:none; float:left; text-align:center; font-size:20px;
        width:60px; height:60px; margin:5px; background-color:#505050;
        
        }    .csbz{
            background-color:#909090;
        }

          .ydq {
          
display: inline-block;/*行内元素*/
    width: 100%;/*可以挤掉文字，保证不跟文字在同一行*/
    height: 0;
    visibility: hidden;
}
    </style>
 
    <script language="javascript" type="text/javascript">
 
    $(function () {

        $(".glyphicon").removeClass("a_cur");
        $("#c2").addClass("a_cur");
        //加载
       // var tp = '<%= Session["cpage"]%>';
        showbs(0);
     
        // if (tp == 2) {$("#djye").show(); LoadJYZList(); }
        //if (tp == 3) {$("#sech").show(); LoadJYWCList(0); }
        loadsellandbuylist();
        //加载点击事件
        $(".scttjy li").click(function () {
            var ck = $(this).attr("atr");
            showbs(ck);
            //window.location.href = "sellercenter.aspx?tp=" + ck;
            //$(".scttjy li").removeClass("cur");
            //$(this).addClass("cur");
            //$("#buy").hide();
            //$("#sell").hide();
            //$("#djye").hide();
            //$("#sech").hide();
            //if (ck == 0) $("#buy").show();
            //if (ck == 1) $("#sell").show();
            //if (tp == 2) {$("#djye").show(); LoadJYZList(); }
            //if (tp == 3) {$("#sech").show(); LoadJYWCList(0); }

        });


    });

        function showbs( tp) {
            $(".scttjy li").removeClass("cur0");
            $(".scttjy li").removeClass("cur1");
            $(".scttjy li:eq(" + tp + ")").addClass("cur" + tp);

            $("#buy").hide();
            $("#sell").hide();
            $("#djye").hide();
            $("#sech").hide();
            if (tp == 0) {
                $("#buy").show(); $("#showcu10").show();
                Fosubprice('mrjg', 'Money', -1, 0);
            }
            if (tp == 1) {
                $("#sell").show(); $("#showcu10").show();
                Fosubprice('mcjg', 'txtsellcount', -1, 0);
            }
        }



    function Fosub(ecountid, showid, subtype, issell) {

        var count = parseFloat($("#" + ecountid).val());
        var cansell = parseFloat($("#hidblance").val());
        var sgprice = parseFloat($("#hidprice").val());
        var changem = 10;
        var canbuy = <%=mairu %>;
        var cans = 500; 
        cansell = cansell * sgprice; 
        if (subtype == 0) //减少
        {
            if (count > 0) {
                count -= changem;
            }
            else count = 0;
        }
        else if (subtype == 1) //增加
        {

            if (issell == 1) {

                if (cansell > cans) cansell = cans;
                if (cansell >= count + changem)
                    count += changem;
                else alert("已达到单笔最大可卖出价格");
            }
            if (issell == 0) {

                if (canbuy >= count + changem)
                    count += changem;
                else return;
            }
        }
        $("#" + ecountid).val(count);
        $("#" + showid).html(parseFloat(count / sgprice).toFixed(4));
    }

    function Fosubprice(ecountid, showid, subtype, issell) {

        var price = parseFloat($("#" + ecountid).val()); 
        if ($("#" + ecountid).val() == "") price = 0.0000;
        var changem =0.0001; 
        if (subtype == 0) //减少
        {
            if (price > 0) {
                price -= changem;
            }
            else price = 0;
        }
        else if (subtype == 1) //增加
        {
            price += changem;
               
        }
        $("#" + ecountid).val(parseFloat(price).toFixed(4));
        $("#" + showid).html(parseFloat(price*7 ).toFixed(4));
    }
    function Fosubsc(ecountid, showid, subtype, issell) {

        var count = parseFloat($("#" + ecountid).val());
        if (isNaN($("#" + ecountid).val())) count = 0;
        var cansell = parseFloat($("#hidblance").val()); 
        var changem = 1; 
        var cans = cansell;
         
        if (subtype == 0) //减少
        {
            if (count > 0) {
                count -= changem;
            }
            else count = 0;
        }
        else if (subtype == 1) //增加
        {

            if (issell == 1) { 
                if (cansell >= count + changem)
                    count += changem;
                else alert("已达到单笔最大可卖出价格");
            }
            if (issell == 0) {

                if (canbuy >= count + changem)
                    count += changem;
                else alert("已达到单次买入最大价格");
            }
        }
        var canuse = cansell - count;
        $("#" + ecountid).val(count);
        $("#" + showid).html(parseFloat(canuse  ).toFixed(4));
    }


    //充值确认录入卡号
    function choseCardRem(ele, cardtp) {
        $(ele).parent().children("a").attr("class", "btn btn-defaultr btn-lg");
        $(ele).addClass("btn btn-primary btn-lg");
        $("#hidremtype").val(cardtp);
        if (cardtp == 0) {
            $("#bkn").show();
        } else $("#bkn").hide();
    }

    //提现选择卡号
    function choseCard(ele, cardtp) {
        var cardinfo = AjaxClass.GetChoseCardInfo(cardtp).value;
        if (cardinfo == "-1") {
            alert("长时间未操作，请先去登录！");
            window.location.href = 'index.aspx';
            return;
        }
        if (cardtp == 0) {
            if (cardinfo == "") {
                if (confirm("您尚未绑定银行卡信息，请先去绑定！")) {
                    window.location.href = "phonesettings/bankcard.aspx";
                    return;
                }
            }
        }
        if (cardtp == 1) {
            if (cardinfo == "") {
                if (confirm("您尚未绑定支付宝账号信息，请先去绑定！")) {
                    window.location.href = "phonesettings/bindzhifubao.aspx";
                    return;
                }
            }
        }
        if (cardtp == 2) {
            if (cardinfo == "") {
                if (confirm("您尚未绑定微信账号信息，请先去绑定！")) {
                    window.location.href = "phonesettings/bindingweixin.aspx";
                    return;
                }
            }
        }
        if (cardinfo != "" && cardinfo != "-1") {
            $(ele).parent().children("a").removeClass("blue");
            $(ele).addClass("blue");
            $("#hidcardtype").val(cardtp);
            $("#cardinfo").html(cardinfo);
        }


    }

    function check(ele) {
        $("#hidBZ").val($(ele).attr("it"));
        $(".changeRt ul li").removeClass("csbz");
        $(ele).addClass("csbz");
    }
    function check2(ele) {
        $("#hidsellBZ").val($(ele).attr("it"));
        $(".changeRt ul li").removeClass("csbz");
        $(ele).addClass("csbz");
    }
    function addwithdrow() {
        var sellcount = parseFloat($("#sellsz").val());
        var sellprice = parseFloat($("#mcjg").val());
        var ctype = $("#hidcardtype").val();
        var acttype = $("#hidbscs").val(); //卖币类型
        var pass = $("#advpass").val();
        var yzm = $("#yzm").val();
        var sgprice = parseFloat($("#mcjg").val());
        //var zuida =/********/;
        if (sellcount == 0) {
            alert("请输入卖出数量");
            return;
        }
       /// var mcmin = '/********/';
      //  var mc = parseInt(mcmin)
      /************************************************************************************************/
       // var mcmax = /********/;
        //if(zuida>mcmax)
        //{
        //alert("每天最多挂单/********/");
        //    return;
        //}
       // sellcount = sellcount / sgprice;
        //if (pass == "") {
        //    alert("请输入二级密码！");
        //    return;
        //}
        //if (yzm == "") {
        //    alert("请输入验证码！");
        //    return;
        //}


        //if (ctype == "-1") {
        //    alert("请选择收款账户类型");
        //    return;
        //}
        sellintal = setInterval(enablesell, 1000);
        $("#Button1").attr("class", "btn btn-defaultb btn-lg");
        $("#Button1").attr("onclick", "");
      
        var rec = AjaxClass.AddWithdawNew(sellcount, pass, ctype, yzm, sellprice, acttype).value;
        if (rec == "-1") {
        alert("长时间未操作，请先去登录！");
            window.location.href = 'index.aspx';
            return;
        }
        if (rec == "-2") {
        alert("您的账户被冻结，无法完成操作！");
            window.location.href = 'index.aspx';
            return;
        }
        if (rec == "0") {
            if (confirm("卖出成功！")) {
        window.location.href = "sellerCenter.aspx";
            }
            return;
        }
        if (rec == "1") {
        alert("操作失败！"); return;
        }
        else {
        alert(rec);
        }
    }


        function loadsellandbuylist() {
            var actype = $("#hidbscs").val();
            var sellhtml = AjaxClass.Loadsellbuytop5(1, actype).value;
            var sellhtml1 = AjaxClass.Loadsellbuytop5(0, actype).value;
            $("#buylist").html(sellhtml);
            $("#selllist").html(sellhtml1);
    }

    var buytime = 5;
    var selltime = 5;
    var buyintal;
    var sellintal;
    function enablebuy() {
        if (buytime > 0) {
        buytime = buytime - 1;
            $("#sub").val("(" + buytime + "秒后可以操作)买入");
        }
        else {
        $("#sub").attr("class", "btn btn-primary btn-lg");
            $("#sub").attr("onclick", " abc();");
            $("#sub").val("买入");
            clearInterval(buyintal);
            buytime = 5;
        }
    }
    function enablesell() {
        if (selltime > 0) {
        selltime = selltime - 1;
            $("#Button1").val("(" + selltime + "秒后可以操作)卖出");
        }
        else {
        $("#Button1").attr("class", "btn btn-primary btn-lg blue");
            $("#Button1").attr("onclick", " addwithdrow();");
            $("#Button1").val("卖出");
            clearInterval(sellintal);
            selltime = 5;
        }
    }


    function delcsdj(elem,rmid) {
        var recb = AjaxClass.DelRemittance(rmid).value;
        if (recb == "-1") {
        alert("长时间未操作，请先去登录！");
            window.location.href = 'index.aspx';
            return;
        }
        if (recb == "1") {
        alert("删除成功！");
            $(elem).parent().parent().hide();
        } else if (recb == "0") {
        alert("操作失败！");
        }
    }


        function abc() {

       // alert("敬请期待..");
           
        
            var mrje = parseFloat($("#buysz").val());
            var mrjg = parseFloat($("#mrjg").val()); //买入价格
            var acttype =  $("#hidbscs").val() ;
        if (mrje <= 0) {alert("请输入买入数量！"); return false; }
            if (mrjg <= 0) {alert("请输入买入价格！"); return false; }
      
          //  buyintal = setInterval(enablebuy, 1000);
            $("#sub").attr("class", "btn btn-default btn-lg");
            $("#sub").attr("onclick", "");
           
            var rec = AjaxClass.AddRemittance(mrje, mrjg, acttype).value;
         
            if (rec ==1) { 
                alert("买入成功！");
               
                       
                } else {
                alert("买入失败");
                } 
 window.location.href = "sellerCenter.aspx";
        return false;
    }

    //交易中列表
    function LoadJYZList() {

        var djyhtml = AjaxClass.LoadSellerList(0).value;

        $("#djye ul").html(djyhtml);

        window.setInterval(function () {
            var divname = $('p[name="ddjs"]');
            for (var i = 0; i < divname.length; ){
                var retime = $(divname[i]).attr("retime");

                ShowCountDown(retime, divname[i]);
            }
                }, interval);

    }

    //交易完成列表
    function LoadJYWCList(pgidx) {
        var rehtml = AjaxClass.LoadSellerSuccessList(0).value;
        $("#sech").html(rehtml);
    }


    //交易完成列表
    function shoukuan(ele, wdid) {
        if (confirm("确认已收到对方转账了吗？")) {
            var rec = AjaxClass.ConfirmWithdrawSK(wdid).value;
            if (rec == "-1") {
                alert("长时间未操作，请先去登录！");
                window.location.href = 'index.aspx';
                return;
            }
            if (rec == "1") {
                alert("收款成功！");
                window.location.href = 'sellercenter.aspx?tp=3';
            }

        }
    }




    //取消未匹配到的汇款
    function cancelbuy(elem, hkid) {
        var recb = AjaxClass.CancelRemittance(hkid).value;
        if (recb == "-1") {
            alert("长时间未操作，请先去登录！");
            window.location.href = 'index.aspx';
            return;
        }
        if (recb == "1") {
            alert("撤销买入成功！");
            $(elem).parent().parent().hide();
        } else if (recb == "0") {
            alert("操作失败！");
        }

    }


    //取消未匹配到的汇款
    function cancelsell(elem, hkid) {


        var recb = AjaxClass.CancelWithdraw(hkid).value;
        if (recb == "-1") {
            alert("长时间未操作，请先去登录！");
            window.location.href = 'index.aspx';
            return;
        }
        if (recb == "1") {
            alert("撤销卖出成功！");
            $(elem).parent().parent().hide();
        }
        else if (recb == "0") {
            alert("操作失败！");
        }
        else if (recb == "-2") {
            alert("卖出已匹配到汇款，不能撤销！");
        }

    }

    //确认汇款
    function confirmRmit(hkid) {
        $("#chosbank").show();
        $("#bakg").show();
        $("#hidremid").val(hkid);
    } function closeremit() {
        $("#chosbank").hide();
        $("#bakg").hide();
    }
    function confirmRmitSub() {
        var rmid = $("#hidremid").val();
        if (parseInt(rmid) > 0) {
            var rtype = $("#hidremtype").val();
            var cardno = $("#cardacc").val();
            var name = $("#txtrname").val();
            var bkname = $("#txtbankname").val();
      
            if (rtype == "-1") { alert("请选择汇出类型"); return; };

            if (name == "") { alert("请输入姓名"); return; };
            if (rtype == "0") {
                if (bkname == "") {
                    alert("请输入银行名称"); return;
                }
            };
            if (cardno == "") { alert("请输入汇出账号"); return; };
            var red = AjaxClass.ConfirmRemittance(rmid, rtype, name, bkname, cardno).value;
            if (red == "1") {
                alert("确认汇款完成，立即查看汇入账号，进行汇款");
                $("#chosbank").hide(); $("#bakg").hide();
                window.location.href = "Sellbuydetails.aspx?rmid=" + rmid;
            }
            else {
                alert("在线交易匹配中,请稍后查看交易状态！");
                window.location.href = "sellerCenter.aspx?tp=2";
                $("#chosbank").hide(); $("#bakg").hide();
            }

        }
    }

    var interval = 1000;
    function ShowCountDown(dates, divname) {

        var now = new Date();
        var endDate = new Date(dates);
        var leftTime = endDate.getTime() - now.getTime();

        //alert(leftTime);
        var leftsecond = parseInt(leftTime / 1000);
        //var day1=parseInt(leftsecond/(24*60*60*6));
        var day1 = Math.floor(leftsecond / (60 * 60 * 24));
        var hour = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);
        var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour * 3600) / 60);
        var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour * 3600 - minute * 60);

        if (hour < 10) {
            hour = "0" + hour;
        }
        if (minute < 10) {
            minute = "0" + minute;

        }
        if (second < 10) {

            second = "0" + second;
        }
        //var cc = document.getElementById(divname);
        var str = "(" + hour + ":" + minute + ":" + second + ")";
        if (hour < 0 || minute < 0 || second < 0) {
            str = "(" + 00 + ":" + 00 + ":" + 00 + ")";
        }

        if (leftTime < 0) {
            str = "(" + 00 + ":" + 00 + ":" + 00 + ")";
        }

        divname.innerHTML = str;
    }
    //window.setInterval(function () {ShowCountDown('2017-10-20 23:59:59', 'divdown1'); }, interval);

    function sendRemitmesg(ele, rmid) {
        var cor = AjaxClass.ConfirmRemittanceSendMsg(rmid).value;
        if (cor == "-1") {
            alert("长时间未操作，请先去登录！");
            window.location.href = 'index.aspx';
            return;
        }
        if (cor == "1") {
            $(ele).val("已通知");
            $(ele).attr("class", "btn btn-default");
            alert("通知成功！");


        }

    }




    </script>

  
    <script language="javascript">

    
        function get_mobile_code(){
            var yzm=RndNum(6);
            
            $.get('POS.aspx', {mobile:jQuery.trim($('#MobileTele').val())}, function(msg) {
                alert(jQuery.trim(unescape(msg)));
                if(msg=='提交成功'){
                    RemainTime();
                }
            });
        };
        var iTime = 59;
        var Account;
        function RemainTime(){
            document.getElementById('zphone').disabled = true;
            var iSecond,sSecond="",sTime="";
            if (iTime >= 0){
                iSecond = parseInt(iTime%60);
                iMinute = parseInt(iTime/60)
                if (iSecond >= 0){
                    if(iMinute>0){
                        sSecond = iMinute + "分" + iSecond + "秒";
                    }else{
                        sSecond = iSecond + "秒";
                    }
                }
                sTime=sSecond;
                if(iTime==0){
                    clearTimeout(Account);
                    sTime='获取手机验证码';
                    iTime = 59;
                    document.getElementById('zphone').disabled = false;
                }else{
                    Account = setTimeout("RemainTime()",1000);
                    iTime=iTime-1;
                }
            }else{
                sTime='没有倒计时';
            }
            document.getElementById('zphone').value = sTime;
        }
        function RndNum(n){
            var rnd="";
            for(var i=0;i<n;i++)
                rnd+=Math.floor(Math.random()*10);
            return rnd;
        }
    </script>
</head>

<body>
    <!--页面内容宽-->
    <form id="form1" runat="server" name="form1" method="post">
        
        <%--  <div class="moneyInfo3" style="left:0px;overflow:hidden;zoom:1;width:100%;background-color:#fff">
            <a href="OnlinePayment.aspx" class="moneyInfoSlt">买入</a>
           <a href="OnlinePayment.aspx" class="moneyInfoSlt">卖出</a>
            <a href="DetailDHK.aspx">待交易</a>
            <a href="DetailDCS.aspx">已汇出</a>
            <a href="DetailYDZ.aspx">查询</a>
        </div>--%>
        <div style="margin-left:8%;margin-top:40px;margin-bottom:40px;"><h4 style="color:#fff">Saturn/USDT</h4></div>
        <div class="middle">
             <div class="topscrll" style="text-align-last:right;" >

               <asp:Label ID="lbltodayprice" style="float:right;text-align: right; font-size: 18px; color:#fff;" runat="server" Text="0.00"></asp:Label>
                <br />
                <i class="ydq"></i><asp:Label ID="lblzzlv"  style="float: right;margin-top: -16px; font-size: 12px;color:darkgray;" runat="server"  Text="0.00"></asp:Label><i class="ydq"></i>
                <asp:HiddenField ID="hidprice" Value="0" runat="server" />
            </div>
            <ul class="scttjy">
                <li atr="0">买入</li>
                <li  atr="1">卖出</li>
                 <asp:HiddenField ID="hidbscs" runat="server"  Value="1" />
            </ul>
           
            <div class="jiaoyi">
            <div class="content buy" id="buy"> 
                <ul>
                    <li>
                       
                        <ul id="buycc" class="cc"><li class="cr1" >A</li><li>B</li><li>C</li><li>D</li>
                            </ul>
                    </li> 
                     <li style=" font-size: 12px;color:darkgray">设置价格</li>
                    <li> 
                        <div class="changeRt">
                            <span style="" onclick="Fosubprice('mrjg','Money',0,0)" class="spmp">-</span>
                            <asp:TextBox ID="mrjg" ReadOnly="true" placeholder="买入价格" CssClass="form-control" runat="server" Text="0" Style="width: 120px;border-radius: 5px; font-size: 18px; text-align: center; float: left; border-radius: 0px;" onblur="Fosubprice('mrjg','Money',-1,0)"
                                MaxLength="6"></asp:TextBox><span onclick="Fosubprice('mrjg','Money',1,0)" class="spmp ">+</span> 
                        </div>
                    </li>
                    <li style=" font-size: 12px;color:darkgray">≈&yen;<span id="Money">0.0000 </span></li>
                    <li>
                        
                        <div class="changeRt">
                            <span style="" onclick="Fosub('buysz','mmp',0,0)" class="spmp">-</span>
                            <asp:TextBox ID="buysz" placeholder="买入量" CssClass="form-control" runat="server" Text="0" Style="width:120px;border-radius: 5px; font-size: 18px; text-align: center; float: left; border-radius: 0px;" onblur="Fosub('buysz','Money',-1,0)"
                                MaxLength="3"></asp:TextBox><span onclick="Fosub('buysz','mmp',1,0)" class="spmp ">+</span>
                        </div>

                        <%--<input type="button" id="sub"  name="sub" value="提 交" onclick="popDiv()" />--%>

                        <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
                        <p id ="mmp" style="display:none;"></p>
                    </li>
                    <li  style=" font-size: 12px;color:darkgray">             <asp:HiddenField ID="hidusdtblc" runat="server" Value="0" />     
                            最多可买：<asp:Label ID="qbye"  runat="server" Text="0.00"></asp:Label>
                    </li> 
                    <li>
                        <input id="sub"  type="button" runat="server" value="买入" class="busub" onclick="return abc();" />

                    </li>


                </ul>
            </div>
            <div class="content sell" id="sell">
                <ul> <li>
                        <ul id="sellcc" class="cc"><li class="cr2" >A</li><li>B</li><li>C</li><li>D</li>
                            </ul>
                    </li> 
                    <li style=" font-size: 12px;color:darkgray">设置价格</li>
                    <li>
                        
                        <div class="changeRt">
                        <span style="" onclick="Fosubprice('mcjg','txtsellcount',0,1)" class="spmp">-</span>  <asp:TextBox ID="mcjg" ReadOnly="true" placeholder="卖出价格" CssClass="form-control" runat="server" Text="" Style="width: 120px;border-radius: 5px; font-size: 18px; text-align: center; float: left; border-radius: 0px;"
                                MaxLength="6"></asp:TextBox><span onclick="Fosubprice('mcjg','txtsellcount',1,1)" class="spmp ">+</span>
                        </div>
                    </li>
                     <li style=" font-size: 12px;color:darkgray">≈&yen;<span id="txtsellcount">0.0000 </span></li> 
                    <li>
                       
                        <div class="changeRt">
                             <span style="" onclick="Fosubsc('sellsz','cnsel',0,1)" class="spmp">-</span> 
                            <asp:TextBox ID="sellsz" placeholder="卖出数量" CssClass="form-control" runat="server" Text="" Style="width: 120px;border-radius: 5px; font-size: 18px; text-align: center; float: left; border-radius: 0px;" onblur="Fosubsc('sellsz','cnsel',-1,1)"
                                MaxLength="3"></asp:TextBox>
                            <span onclick="Fosubsc('sellsz','cnsel',1,1)" class="spmp ">+</span>
                            <input type="hidden" value="0" id="hidblance" runat="server" />


                        </div>

                        <%--<input type="button" id="sub"  name="sub" value="提 交" onclick="popDiv()" />--%>

                        <input type="hidden" value="0" id="Hidden1" runat="server" />
                    </li>
                   <li  style=" font-size: 12px;color:darkgray">                  
                            可用：<span id="cnsel" runat="server">0.0000</span> 
                    </li>
                                                          
                    <li>
                        <input id="Button1"  type="button" runat="server" value="卖出" class="blue" onclick="return addwithdrow();" />

                    </li>

                </ul>
            </div>
               
            <div class="gmlb" id="showcu10">
                <div class="blance">
                    <p></p>
                    <div style="width:100%;color: #a9a9a9"><span style="float:left;">价格</span><span style="float:right;">数量</span></div>
                </div>

                <div class="zhlist" style=" ">
                    <ul id="buylist"   >
                      
                     
                    </ul>
                </div>
                <div style="border-bottom: 1px solid #ccc;width:100% ;height:1px;"></div>
                <div class="zhlist" style=" ">
                    <ul id="selllist">
                         
                    </ul>
                </div>
                <div style="margin-top: 10px;"> </div>
            </div>


                 </div>
            <ul class="scttjy" style="top:350px; display:none;">
                
                <li atr="2">待交易</li>
                <li atr="3">查询</li>
            </ul>
            <div class="content djye" id="djye">
                <ul>
                    <li class="title">
                        <div class="firstdiv">委托时间</div>
                        <div>数量/市值</div>
                        <div class="secdiv">状态</div>
                        <div class="fourdiv">操作</div>
                    </li>

                    
                </ul>
            </div>
            <div class="content djye" id="sech">
                <ul>
                    <li class="title">
                        <div class="firstdiv">委托时间</div>
                        <div>数量/市值</div>
                        <div class="fourdiv">交易方</div>
                        <div class="secdiv" style="float: right;">状态</div>
                    </li>
                   
                </ul>

            </div>
            <div id="bakg"></div>

            
             
            <!-- #include file = "comcode.html" -->

        </div>
    </form>
    
</body>
</html> 
 
