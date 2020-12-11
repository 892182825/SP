<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SHJF.aspx.cs" Inherits="MemberMobile_SHJF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>便民生活</title>
        <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=yes" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="css/style.css" />
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
</head>
<body >
    <form id="form1" runat="server">
        <div style="width:100%;height:600px;margin:0 0 0 0;background-color:#fff">
   
            <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">	    生活缴费</span>
            </div>
        </div>
            
                      <div style="margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;color: #666;">生活缴费</div>
                  
            <div style="width: 90%; height: 70px; margin-top: 15px; margin-left: 5%;">
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="../MemberMobile/MemberCZXF.aspx">
                    <img src="images/cz.png" width="32" height="32" /><br />
                    民生充值
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="../MemberMobile/BMPhone.aspx">
                    <img src="img/shouji.png" width="32" height="32" /><br />
                    手机话费
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="../MemberMobile/BMsdm.aspx">
                    <img src="img/shuidianmei.png" width="32" height="32" /><br />
                    水电煤
                      
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="../MemberMobile/BMyx.aspx">
                    <img src="img/youxi.png" width="32" height="32" /><br />
                    游戏
                      
                </a>
                
                
            </div>
            <div style="width: 90%; height: 70px; margin-top: 15px; margin-left: 5%;">
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="../MemberMobile/BMfeiji.aspx">
                    <img src="img/feiji.png" width="32" height="32" /><br />
                    飞机票
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="../MemberMobile/BMhuoche.aspx">
                    <img src="img/huoche.png" width="32" height="32" /><br />
                    火车票
                      
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="../MemberMobile/BMqiche.aspx">
                    <img src="img/qichepiao.png" width="32" height="32" /><br />
                    汽车票
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="../MemberMobile/BMjyk.aspx">
                    <img src="img/jiayou.png" width="32" height="32" /><br />
                    加油卡
                </a>
            </div>
           
              <div style="margin-left: 10px;margin-top:20px; font-size:18px;padding-top: 23px;font-weight:bold;color: #666;">购物娱乐</div>
            <div style="width: 90%; height: 70px; margin-top: 15px; margin-left: 5%;">
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="img/jd.jpg" width="32" height="32" /><br />
                    京东
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="img/taobao.jpg" width="32" height="32" /><br />
                    淘宝
                      
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="img/meituan.jpg" width="32" height="32" /><br />
                    美团
                      
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="img/xiecheng.jpg" width="32" height="32" /><br />
                    携程
                </a>
            </div>
            <div style="margin-left: 10px;margin-top:20px; font-size:18px;padding-top: 23px;font-weight:bold;color: #666;">健康公益</div>
            <div style="width: 90%; height: 70px; margin-top: 15px; margin-left: 5%;">
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="img/aixin.jpg" width="32" height="32" /><br />
                    爱心捐赠
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="img/yljk.jpg" width="32" height="32" /><br />
                    医疗健康
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="img/gykc.png" width="32" height="32" /><br />
                    公益课程
                </a>
                
            </div>
            <div style="margin-left: 10px;margin-top:20px; font-size:18px;padding-top: 23px;font-weight:bold;color: #666;">体育彩票</div>
            <div style="width: 90%; height: 70px; margin-top: 15px; margin-left: 5%;">
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="img/tiyu.jpg" width="32" height="32" /><br />
                    体育服务
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #666;" href="">
                    <img src="logo/caipiao.jpg" width="32" height="32" /><br />
                    彩票
                      
                </a>
                
            </div>

            <div style="box-shadow:#e8e5e5 0px 0px 10px;width:100%;height:70px;background-color:#f9f9ff;margin-top:10px">
                      <div style="margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;color: #666;">敬请期待</div>
                  </div>
            </div>
    </form>
</body>
</html>
