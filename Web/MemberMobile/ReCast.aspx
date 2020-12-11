<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReCast.aspx.cs" Inherits="MemberMobile_ReCast" %>

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
    <title>FTC申请锁仓</title>
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
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">	    申请锁仓</span>
            </div>
        </div>
    <div>

        <div style="margin-top: 30px;background-image:url(img/jb-1.png);width:90%;height:150px;margin-left: 5%;color:#fff;background-repeat: no-repeat;background-size: 100% 100%;text-align: center;-moz-background-size: 100% 100%;">
                  <div style="font-size: 18px;padding-top: 28px;">当前等级</div>
                  <div style="font-size: 35px;margin-top: 12px;font-weight: 600;"><asp:Label ID="Jackpot" runat="server" Text="0"></asp:Label></div>
                     </div>
        
    <div  style="margin-top: 10%;color: #aea79f;margin-top: 30px;">
        <ul>
            <li>
               
                
            </li>
            <li style="display:none;">
                
                    <div class="input-group col-md-4" style="margin-left:10%;">
                    <span class="input-group-addon"><i class="glyphicon">
                        <img src="images/bzsz.png" width="20" height="20" /></i></span>
                         <asp:TextBox ID="ft" CssClass="form-control" onkeypress="return kpyzsz();" onkeyup="   szxs(this);" onafterpaste="szxs(this)" AutoPostBack="true" style="display:none;width: 80%;"  placeholder="金额" MaxLength="20" runat="server"></asp:TextBox>
                       
                   </div>
                
                   
            </li>
            <li style="background-color: #e1e3e6;height: 50px;"> <div style="color: #000;margin-left: 10%;font-size: 18px;height: 45px;padding-top: 10px;font-weight: 500;">锁仓金额：</div></li>
            <li>
                <asp:HiddenField ID="newprice" Value="" runat="server" />
                <asp:HiddenField ID="hidnew" Value="" runat="server" />
                <br />
                    <div>
                        <asp:Button ID="tz500" CssClass="btn btn-qianse btn-lg" style="width: 42%;margin-left: 5%;font-size: 24px;height: 70px;font-weight: 600;"  Text="500" OnClientClick="return je(this.value);"
                            runat="server"  />
                        <asp:Button ID="tz1000" CssClass="btn btn-qianse btn-lg" style="width: 42%;margin-left: 5%;font-size: 24px;height: 70px;font-weight: 600;"  Text="1000" OnClientClick="return je(this.value);"
                            runat="server"  />
                        <asp:Button ID="tz3000" CssClass="btn btn-qianse btn-lg" style="width: 42%;margin-left: 5%;font-size: 24px;height: 70px;font-weight: 600;margin-top: 5%;"  Text="3000" OnClientClick="return je(this.value);"
                            runat="server"  />
                    <asp:Button ID="tz5000" CssClass="btn btn-qianse btn-lg" style="width: 42%;margin-left: 5%;font-size: 24px;height: 70px;font-weight: 600;margin-top: 5%;"  Text="5000" OnClientClick="return je(this.value);"
                            runat="server"  />
                       <asp:Button ID="tz100" CssClass="btn btn-qianse btn-lg" style="width:26%;margin-left: 29%; font-size:14px;display:none;"  Text="100" OnClientClick="return je(this.value);"
                            runat="server"  />
                        
                    </div></li>
            <li id="fxpay" runat="server"  class="zfInfo_3" style="height:auto;color: #000;font-size: 16px;display:none;">
               
            </li> 
            <li>
                <asp:Button ID="ImageButton1"  runat="server" Height="35px" Width="93%" Style="height: 45px;width: 80%;margin-top: 25px;border-radius: 10px;font-size: 20px;margin-left: 10%;color: #FFF;border: 1px solid #9E9E9E;background-image: linear-gradient(#54b4eb, #2fa4e7 60%, #1d9ce5);text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);font-weight: 900;"
                    Text="提交" OnClick="ImageButton1_Click"  />
            </li>
        </ul>
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
