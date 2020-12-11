<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckPassword.aspx.cs" Inherits="MemberMobile_PhoneSettings_CheckPassword" EnableEventValidation="false" ValidateRequest="false"  %>

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

    <title>二级密码验证</title>
    <link href="../../css/bootstrap-cerulean.min.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link href="../../css/charisma-app.css" rel="stylesheet" />
    <link href='../../bower_components/fullcalendar/dist/fullcalendar.css' rel='stylesheet' />
    <link href='../../bower_components/fullcalendar/dist/fullcalendar.print.css' rel='stylesheet' media='print' />
    <link href='../../bower_components/chosen/chosen.min.css' rel='stylesheet' />
    <link href='../../bower_components/colorbox/example3/colorbox.css' rel='stylesheet' />
    <link href='../../bower_components/responsive-tables/responsive-tables.css' rel='stylesheet' />
    <link href='../../bower_components/bootstrap-tour/build/css/bootstrap-tour.min.css' rel='stylesheet' />
    <link href='../../css/jquery.noty.css' rel='stylesheet' />
    <link href='../../css/noty_theme_default.css' rel='stylesheet' />
    <link href='../../css/elfinder.min.css' rel='stylesheet' />
    <link href='../../css/elfinder.theme.css' rel='stylesheet' />
    <link href='../../css/jquery.iphone.toggle.css' rel='stylesheet' />
    <link href='../../css/uploadify.css' rel='stylesheet' />
    <link href='../../css/animate.min.css' rel='stylesheet' />

    <script src="../../bower_components/jquery/jquery.min.js"></script>
     <style>
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
    </style>

    <script language="javascript" type="text/javascript">
        
        function subform() {
            if ($("#txtPassword").val() == "") {
                webToast("密码不能为空", "middle", 3000);
                return false;
            }
            return true;
        }
       
        

    </script>
       <script type="text/javascript">
           function get_mobile_code() {
               var yzm = RndNum(6);
              
               $.get('../POS.aspx', { mobile: jQuery.trim($('#MobileTele').val())}, function (msg) {
                   alert(msg);
                   if (msg == '提交成功') {
                       RemainTime();
                   }
               });
           };
           var iTime = 59;
           var Account;
           function RemainTime() {
               document.getElementById('zphone').disabled = true;
               var iSecond, sSecond = "", sTime = "";
               if (iTime >= 0) {
                   iSecond = parseInt(iTime % 60);
                   iMinute = parseInt(iTime / 60)
                   if (iSecond >= 0) {
                       if (iMinute > 0) {
                           sSecond = iMinute + "分" + iSecond + "秒";
                       } else {
                           sSecond = iSecond + "秒";
                       }
                   }
                   sTime = sSecond;
                   if (iTime == 0) {
                       clearTimeout(Account);
                       sTime = '获取手机验证码';
                       iTime = 59;
                       document.getElementById('zphone').disabled = false;
                   } else {
                       Account = setTimeout("RemainTime()", 1000);
                       iTime = iTime - 1;
                   }
               } else {
                   sTime = '没有倒计时';
               }
               document.getElementById('zphone').value = sTime;
           }
           function RndNum(n) {
               var rnd = "";
               for (var i = 0; i < n; i++)
                   rnd += Math.floor(Math.random() * 10);
               return rnd;
           }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 25%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">二级密码验证</span>
            </div>
        </div>


        <div class="middle">
            <div  style="margin-top:30px">
            </div>
            <ul style="padding-left:0px;margin-left:  10%;margin-right: 10%;">
                <li style="list-style: none;margin-bottom:20px;height:40px">
                        <asp:HiddenField ID="MobileTele" Value="0" runat="server" />
                       
                        <div class="changeLt" style="width:40%;"><asp:TextBox ID="yzm" placeholder="输入验证码" CssClass="form-control"  runat="server" Text="" MaxLength="6"></asp:TextBox></div>
                        <div class="changeRt" style="width:55%;padding-left:10px;">
                            
                            <input id="zphone" type="button" CssClass="form-control" value=" 发送手机验证码 " onClick="get_mobile_code();" /></div>
                    </li>
                <li style="list-style: none;">
                    <div class="input-group col-md-4">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock red"></i></span>
                        <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="请输入你的二级密码" runat="server" MaxLength="20"></asp:TextBox>
                    </div>
                </li>

            </ul>
        </div>

        <div style="clear: both;padding-top:50px;">
            <div class="registerMsg">
                <asp:Button ID="btn_submit" CssClass="btn btn-primary btn-lg" Style="width: 100%;" Text="下一步" OnClientClick="return subform();"
                    OnClick="btn_submit_Click" runat="server" />
            </div>
        </div>



        
        <!-- #include file = "comcode.html" -->
       
<script src="../../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

<!-- library for cookie management -->
<script src="../../js/jquery.cookie.js"></script>
<!-- calender plugin -->
<script src='../../bower_components/moment/min/moment.min.js'></script>
<script src='../../bower_components/fullcalendar/dist/fullcalendar.min.js'></script>
<!-- data table plugin -->
<script src='../../js/jquery.dataTables.min.js'></script>

<!-- select or dropdown enhancer -->
<script src="../../bower_components/chosen/chosen.jquery.min.js"></script>
<!-- plugin for gallery image view -->
<script src="../../bower_components/colorbox/jquery.colorbox-min.js"></script>
<!-- notification plugin -->
<script src="../../js/jquery.noty.js"></script>
<!-- library for making tables responsive -->
<script src="../../bower_components/responsive-tables/responsive-tables.js"></script>
<!-- tour plugin -->
<script src="../../bower_components/bootstrap-tour/build/js/bootstrap-tour.min.js"></script>
<!-- star rating plugin -->
<script src="../../js/jquery.raty.min.js"></script>
<!-- for iOS style toggle switch -->
<script src="../../js/jquery.iphone.toggle.js"></script>
<!-- autogrowing textarea plugin -->
<script src="../../js/jquery.autogrow-textarea.js"></script>
<!-- multiple file upload plugin -->
<script src="../../js/jquery.uploadify-3.1.min.js"></script>
<!-- history.js for cross-browser state change on ajax -->
<script src="../../js/jquery.history.js"></script>
<!-- application script for Charisma demo -->
<script src="../../js/charisma.js"></script>
<script src="../../JS/alertPopShow.js"></script>

    </form>
</body>
</html>
