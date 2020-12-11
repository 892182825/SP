<%@ Page Language="C#" AutoEventWireup="true" CodeFile="code.aspx.cs" Inherits="MemberMobile_code" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <title></title>
    <link rel="stylesheet" href="css/style.css">
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <script src="js/qrcode.js"></script>
    <style>
        baby {
        
        padding: 0;
    border: 0;
    margin: 0;
    list-style: none;
    box-sizing: border-box;
        }
        #erweima {
        
        margin-top: 25%;
    position: absolute;
    left: 50%;
    margin-left: -100px;
    
        }
        .zg {
        opacity: 0.5;
        position: fixed;
    top: 0;
    right: 0;
    bottom: 50px;
    left: 0;
    
    background:url(img/timg.jpg) no-repeat 0px 0px;
    background-size: 100%;
        }
        .wz {
        width:100%;
        text-align:center;
        position: absolute;
    margin-top: 10%;
        }
        .fh {
            z-index: 1000;
    position: fixed;
    margin-top: 100%;
    margin-left: 45%;
        font-size: 22px;
        }

    </style>
    <script>
        $(function () {
                var qrcode = new QRCode(document.getElementById("erweima"), {
                    width: 200,
                    height: 200
                });
                var aa = '<%=weburl %>MemberMobile/RegisterMemberTG.aspx?id=<%=epurl %>';
                qrcode.makeCode(aa);            });
    </script>
</head>
    
<body>
    <form id="form1" runat="server">
        <div class="zg"></div>
    <div id="erweima">
    
    </div>
        <div class="wz"><span>扫一扫进行注册</span> 
            <br />
            <span>会员可分享二维码邀请用户</span>
        </div>

        <div class="fh"><a href="javascript:history.go(-1)">返回</a></div>
    </form>
    <!-- #include file = "comcode.html" -->
</body>
</html>
