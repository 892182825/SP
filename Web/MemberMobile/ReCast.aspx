<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReCast.aspx.cs" Inherits="ReCast" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="css/style.css">

    <link  href="../css/bootstrap-cerulean.min.css" rel="stylesheet">

    <link href="../css/charisma-app.css" rel="stylesheet">
    <link href='../bower_components/fullcalendar/dist/fullcalendar.css' rel='stylesheet'>
    <link href='../bower_components/fullcalendar/dist/fullcalendar.print.css' rel='stylesheet' media='print'>
    <link href='../bower_components/chosen/chosen.min.css' rel='stylesheet'>
    <link href='../bower_components/colorbox/example3/colorbox.css' rel='stylesheet'>
    <link href='../bower_components/responsive-tables/responsive-tables.css' rel='stylesheet'>
    <link href='../bower_components/bootstrap-tour/build/css/bootstrap-tour.min.css' rel='stylesheet'>
    <link href='../css/jquery.noty.css' rel='stylesheet'>
    <link href='../css/noty_theme_default.css' rel='stylesheet'>
    <link href='../css/elfinder.min.css' rel='stylesheet'>
    <link href='../css/elfinder.theme.css' rel='stylesheet'>
    <link href='../css/jquery.iphone.toggle.css' rel='stylesheet'>
    <link href='../css/uploadify.css' rel='stylesheet'>
    <link href='../css/animate.min.css' rel='stylesheet'>
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <title>购买矿机</title>
    <style>
                ul li {
       list-style:none; 
       margin-bottom:5%;
        }

        .web_toast {
    position: fixed;
    margin: 150px 10px;
    z-index: 9999;
    display: none;
    display: block;
    padding: 10px;
    color: #FFFFFF;
    background: rgba(0, 0, 0, 0.7);
    font-size: 1.4rem;
    text-align: center;
    border-radius: 4px;
}
        .btn-qianse {
            background-color: #e1e3e6;
            color:#000;
        }
    </style>

    <script>
        window.alert = alert;
        //$(function () {
        //    $("#ft").blur(function () {
        //        if ($("#ft").val() == "" || $("#ft").val()<=0) {
        //            webToast("复投金额不能为空或小于0", "middle", 3000);
        //            document.getElementById("ft").style.borderColor = "#b94a48";

        //            return;
        //        }
        //        else {
        //            document.getElementById("ft").style.borderColor = "#468847";
        //        }
        //    });
        //});
        $(function (){
            //$("#subto").click(function(){
            //    var sen = $("#hidetp").val();
            //    var res = AjaxPro.AjaxClass.GetSendPost(sen).value;
            //    if (res == 1) {
            //        $(".showhid").html = "购买矿机成功！";
            //        setTimeout(function () { $(".showhid").hide(); }, 3000);
            //    }
            //    else if (res == -1) {
            //        $(".showhid").html = "用户未登录！";
            //        setTimeout(function () { $(".showhid").hide(); }, 2000);
            //    } else if (res == -2) {
            //        $(".showhid").html = "请选择购买的矿机！";
            //        setTimeout(function () { $(".showhid").hide(); }, 2000);
            //    }
            //    else if (res == -3) {
            //        $(".showhid").html = "账户余额不足以支付！";
            //        setTimeout(function () { $(".showhid").hide(); }, 2000);
            //    }
            //    else if (res == 0) {
            //        $(".showhid").html = "支付失败！";
            //        setTimeout(function () { $(".showhid").hide(); }, 2000);
            //    }


            //});
        });
        ///点击购买
        function showbuy(num) {
            $(".showhid").show();
            $("#hidetp").val(num);
            var html = "";
            if (num == 1) html += "恭喜您，抢到免费体验矿机，请立即领取！"; 
            if (num == 2) html += "您当前购买需支付50U！"; 
            if (num == 3) html += "您当前购买需支付100U！";
            if (num == 4) html += "您当前购买需支付500U！";
            if (num == 5) html += "您当前购买需支付1000U！";
            if (num == 6) html += "您当前购买需支付1500U！";
            if (num == 7) html += "您当前购买需支付3000U！";
            $("#sbuyinfo").html(html);
        }

        function hidediv() {
            $(".showhid").hide();
        }

        function je(id) {
            $("#ft").val("");
            var num = parseFloat($("#hidnew").val());
           var je = id / num;
           $("#je").text(parseFloat(je).toFixed(4));
           $("#newprice").val(id);
           
           $("#tz500").attr("class", "btn btn-qianse btn-lg");
           $("#tz1000").attr("class", "btn btn-qianse btn-lg");
            $("#tz3000").attr("class", "btn btn-qianse btn-lg");
            $("#tz5000").attr("class", "btn btn-qianse btn-lg");
            $("#tz100").attr("class", "btn btn-qianse btn-lg");
           
            
            if (id == $("#tz500").val()) $("#tz500").attr("class", "btn btn-success btn-lg");
            if (id == $("#tz1000").val()) $("#tz1000").attr("class", "btn btn-success btn-lg");
            if (id == $("#tz3000").val()) $("#tz3000").attr("class", "btn btn-success btn-lg");
            if (id == $("#tz5000").val()) $("#tz5000").attr("class", "btn btn-success btn-lg");
            if (id == $("#tz100").val()) $("#tz100").attr("class", "btn btn-success btn-lg");
           

            return false;
        }

      




    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"> </a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">	    购买矿机</span>
            </div>
        </div>
    <div>
 
         <div class="showhid">
            <div id="sbuyinfo"  style="height:70%;"> </div>
            <div> <asp:HiddenField ID="hidetp" runat="server"  Value="0"/>
                <input type="button" class="canc" value="取消" onclick="hidediv()" />
                <asp:Button ID="Button1" runat="server" Text="确认购买" OnClick="Button1_Click" />
                 
                
            </div>
        </div>

        <div id="getshow" runat="server" class="buylist">
             
        </div>  


    
    </div>

        <div class="modal fade" id="myModall" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h3>系统提示</h3>
                </div>
                <div class="modal-body">
                    <p id="p">Here settings can be configured...</p>
                </div>
                <div class="modal-footer">
                    <a href="#" class="btn btn-default"  data-dismiss="modal">关闭</a>
                    <a href="#" class="btn btn-primary" style="display:none;" id="tiaoz" >确定</a>
                </div>
            </div>
        </div>
    </div>
        <script>
            function alertt(data) {
                var x = document.getElementById("p");
                x.innerHTML = data;
                $('#myModall').modal({ backdrop: 'static', keyboard: false });
                $('#myModall').modal('show');
                
            }
</script>
         <!-- #include file = "comcode.html" -->

    
    </form>
    

   
</body>
   
</html>
