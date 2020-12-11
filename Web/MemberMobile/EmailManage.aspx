<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailManage.aspx.cs" Inherits="MemberMobile_EmailManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="css/style.css" />
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <title></title>
    <style>
        .uuulll {
            margin-top: 10%;
        }
      .uuulll li {
        
            height: 40px;
    line-height: 40px;
    border: 1px solid #d5d5d5;
    padding-left: 20px;
        margin-bottom: 20px;
        }
        .iii {
        
        float: right;
    margin-top: 10px;
    margin-right: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:30%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">邮件管理</span>
            </div>
              </div>
    <div>
    <ul class="uuulll">
        <li onclick="location.href='../membermobile/xieyoujian.aspx'"><a href="#">写邮件<i class="glyphicon glyphicon-chevron-right glyphicon-white iii"></i></a></li>
            <li onclick="location.href='../membermobile/fajianx.aspx'"><a href="#">发件箱<i class="glyphicon glyphicon-chevron-right glyphicon-white iii"></i></a></li>
            <li onclick="location.href='../membermobile/shoujianx.aspx'"><a href="#">收件箱<i class="glyphicon glyphicon-chevron-right glyphicon-white iii"></i></a></li>
            <li onclick="location.href='../membermobile/feijianx.aspx'"><a href="#">废件箱<i class="glyphicon glyphicon-chevron-right glyphicon-white iii"></i></a></li>
            <li onclick="location.href='../membermobile/ddcy.aspx'"><a href="#">公告查阅<i class="glyphicon glyphicon-chevron-right glyphicon-white iii"></i></a></li>
    </ul>
    </div>
    </form>
     <!-- #include file = "comcode.html" -->
    <script>
        $(function () {
            $(".glyphicon").removeClass("a_cur");
            $("#c5").addClass("a_cur");
        });
    </script>
</body>
</html>
