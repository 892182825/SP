<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Member_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <%--<link rel="stylesheet" href="css/style.css" />--%>

    <title><%=GetTran("000000","会员登录") %></title>
   <%-- <link href="css/uselogin.css" rel="stylesheet" type="text/css" />--%>
   <%-- <script src="../Company/js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-1.7.1.min.js"></script>--%>


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
    <script src="../JS/sryz.js"></script>
    <%--<script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>--%>
    <%--<script src="js/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>--%>
    <style type="text/css" >
        * {
            padding: 0;
            border: 0;
            margin: 0;
            list-style: none;
            box-sizing: border-box;
        }

        body, html {
            height: 100%;
            overflow: hidden;
            font-family: "微软雅黑";
        }

        .scht_bg {
            height: 45%;
        }

      .scht_login {
            height: 420px;
            overflow: hidden;
            padding-top: 3%;
            width:350px;
            padding: 1%;
            background: #fff;
            margin: 10% 0 0 65%;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .scht_l {
            width: 100%;
            height: 20%;
            text-align: center;
        }

        .scht_r {
            width: 100%;
            height: 80%;
            position: relative;
        }

        .scht_l h5 {
            font-size: 30px;
            font-weight: normal;
            color: #90bdd0;
            padding-right: 0.8%;
        }

        .scht_l p {
            font-size: 20px;
            margin-top: 5%;
            color: #333;
        }

        .scht_r span {
            float: left;
            width: 15%;
            height: 100%;
            background: #e7f0f4;
        }

        .scht_r input {
            float: left;
            width: 85%;
            height: 100%;
            background: #f2f8fb;
            /*padding-left: 5px;*/
        }

        .scht_r div {
            overflow: hidden;
            margin-top: 7%;
            border-radius: 3px;
            height: 12%;
            border: 1px solid #ddd;
        }

        .scht_user span {
            background: #e7f0f4 url(images/scht_user.png) no-repeat center center;
            background-size: 63%;
        }

        .scht_password span {
            background: #e7f0f4 url(images/scht_password.png) no-repeat center center;
            background-size: 46%;
        }

        .scht_yzm span {
            background: #e7f0f4 url(images/scht_yzm1.png) no-repeat center center;
            background-size: 58%;
        }

        .scht_user1 span {
            background: #e7f0f4 url(../img/yyan.png) no-repeat center center;
            background-size: 63%;
        }
          .scht_r .scht_user1 {
            margin-top: 1%;
        }
           .scht_r div {
            overflow: hidden;
            margin-top: 7%;
            border-radius: 3px;
            height: 10%;
            border: 1px solid #ddd;
        }

        .scht_r input, select {
            float: left;
            width: 85%;
            height: 100%;
            background: #f2f8fb;
            /*padding-left: 5px;*/
        }

        .scht_login butten {
            background: #24aef1;
            border-radius: 3px;
            width: 100%;
            margin-top: 7%;
            color: #fff;
            height: 12%;
        }

        .scht_yzm input {
            width: 60%;
        }

        .scht_r a {
            text-decoration: none;
            color: #666;
            position: absolute;
            bottom: 8%;
            right: 5%;
            font-size: 12px;
        }

        .scht_yzm img {
            margin-left: 1%;
            display: inline-block;
        }    

        body {
            background: url(images/2login_bg.png) no-repeat 0 center;
            background-size: 100%;
        }

        .hearer_h5 h5 {
            font-size: 40px;
            height: 100px;
            line-height: 100px;
            width: 1220px;
            margin: 0 auto;
            color: #333;
            font-weight: normal;
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
    </style>

    
    
        <script type="text/javascript">
            window.alert = alert;
            setTimeout(function () {
                document.body.scrollTop = document.body.scrollHeight;
            }, 300);
            $(function () {
                //var Dheight = $(' .scht_r div').height()
                //$('.scht_r input').css({ 'lineHeight': Dheight + 'px', height: Dheight });
                //$('select').css({ height: Dheight });
                //var name = "Platform-CU=";
                //var ca = document.cookie.split(';');
                //for (var i = 0; i < ca.length; i++) {
                //    var c = ca[i].trim();
                //    if (c.indexOf(name) == 0) $("#ck").val(c.substring(name.length, c.length));
                //    else $("#ck").val(document.cookie)
                //}
                
                //$("#ck").val(document.cookie);
                

                

               

            });

      
        
        
        
    </script>
    
    <style>
        body{background:#000;}
        .login_in{padding:10px 5%;margin-top: -1%;}
        .login_in li{height:50px;line-height:50px;padding:5px 0;margin:10px 0;}
        /*.login_in li input,select{height:100%;width:100%;padding-left:5px;background:#eee;display: block;border-radius:3px ;}*/
        /*button{width:100%;background:#77c225;color:#fff;height:40px;border-radius:3px;font-size:16px}*/
        .yzm{overflow: hidden;}
        .yzm img{float:left}
        .login_in .yzm input{float:left;margin-right:5%;width:60%;}
        select{color:#757575;padding-left:0px;}
    </style>
</head>
<body>
   <%-- <p><%=Session["LanguageCode"] %></p>--%>
    <form id="form1" runat="server" style="height: 100%">
         
        <div style="height: 100%;width:100%;background-image: url(images/MAIN.png);background-repeat: no-repeat;background-size: 100% 100%;">
            <div style="margin-top:50%;width:100%;height:50px;position:absolute;text-align:center;">
                 <span style="font-size:25px;color: #000;">授权登录中...</span><br />
                <span style="font-size:20px;color: #000;">请稍后</span><br />

            </div>
        <ul class="login_in"  style="margin-top: 70%;">	
            <%--<li style="height:auto;">
                <img src="../images/img/logo.jpg"  style="width:100%;" />

            </li>--%>
            
            <li style="text-align: center;">
                
               
            </li>

            <%--<li><asp:TextBox ID="ck" Text="cookie" CssClass="form-control" runat="server" MaxLength="500"></asp:TextBox></li>--%>
    </ul>
        </div>
    
          <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;"> 

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h3>系统提示</h3>
                </div>
                <div class="modal-body">
                    <p id="pp">Here settings can be configured...</p>
                </div>
                <div class="modal-footer">
                    <a href="#" class="btn btn-default" data-dismiss="modal">确定</a>
                    <%--<a href="#" class="btn btn-primary" data-dismiss="modal">Save changes</a>--%>
                </div>
            </div>
        </div>
    </div>
         
     <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

<!-- library for cookie management -->
<script src="../js/jquery.cookie.js"></script>
<!-- calender plugin -->
<script src='../bower_components/moment/min/moment.min.js'></script>
<script src='../bower_components/fullcalendar/dist/fullcalendar.min.js'></script>
<!-- data table plugin -->
<script src='../js/jquery.dataTables.min.js'></script>

<!-- select or dropdown enhancer -->
<script src="../bower_components/chosen/chosen.jquery.min.js"></script>
<!-- plugin for gallery image view -->
<script src="../bower_components/colorbox/jquery.colorbox-min.js"></script>
<!-- notification plugin -->
<script src="../js/jquery.noty.js"></script>
<!-- library for making tables responsive -->
<script src="../bower_components/responsive-tables/responsive-tables.js"></script>
<!-- tour plugin -->
<script src="../bower_components/bootstrap-tour/build/js/bootstrap-tour.min.js"></script>
<!-- star rating plugin -->
<script src="../js/jquery.raty.min.js"></script>
<!-- for iOS style toggle switch -->
<script src="../js/jquery.iphone.toggle.js"></script>
<!-- autogrowing textarea plugin -->
<script src="../js/jquery.autogrow-textarea.js"></script>
<!-- multiple file upload plugin -->
<script src="../js/jquery.uploadify-3.1.min.js"></script>
<!-- history.js for cross-browser state change on ajax -->
<script src="../js/jquery.history.js"></script>
<!-- application script for Charisma demo -->
<script src="../js/charisma.js"></script>
    <script src="../JS/alertPopShow.js"></script>
               
        <script type="text/C#" language="javascript">
    window.history.forward(1);     
</script>   
    </form>

   
   
</body>

</html>

 
<script type="text/javascript" language="javascript">
    

    
    //function ClearTxt() {
    //    var txtname = document.getElementById("txtName");
    //    var txtpwd = document.getElementById("txtPwd");
    //    var txtyz = document.getElementById("txtValidate");
    //    if (txtname.value != "" || txtpwd.value != "" || txtyz.value != "") {
    //        txtname.value = "";
    //        txtpwd.value = "";
    //        txtyz.value = "";
    //    }
    //}
    function UpdateYZ() {
        var img1 = document.getElementById("img1");
        img1.src = "../image.aspx?t=" + Math.random();
    }
</script>


<script>
    function alert(data) {
        var x = document.getElementById("pp");
        x.innerHTML = data;
        $('#myModal').modal('show');

    }
</script>
       <%=msg %>