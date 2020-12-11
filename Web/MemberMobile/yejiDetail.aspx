<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yejiDetail.aspx.cs" Inherits="MemberMobile_yejiDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="css/style.css" />
    <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>

    <link href="../../css/bootstrap-cerulean.min.css" rel="stylesheet" />


    <link href="../css/charisma-app.css" rel="stylesheet" />
    <link href='../bower_components/fullcalendar/dist/fullcalendar.css' rel='stylesheet' />
    <link href='../bower_components/fullcalendar/dist/fullcalendar.print.css' rel='stylesheet' media='print' />
    <link href='../bower_components/chosen/chosen.min.css' rel='stylesheet' />
    <link href='../bower_components/colorbox/example3/colorbox.css' rel='stylesheet' />
    <link href='../bower_components/responsive-tables/responsive-tables.css' rel='stylesheet' />
    <link href='../bower_components/bootstrap-tour/build/css/bootstrap-tour.min.css' rel='stylesheet' />
    <link href='../css/jquery.noty.css' rel='stylesheet' />
    <link href='../css/noty_theme_default.css' rel='stylesheet' />
    <link href='../css/elfinder.min.css' rel='stylesheet' />
    <link href='../css/elfinder.theme.css' rel='stylesheet' />
    <link href='../css/jquery.iphone.toggle.css' rel='stylesheet' />
    <link href='../css/uploadify.css' rel='stylesheet' />
    <link href='../css/animate.min.css' rel='stylesheet' />

    <script src="../bower_components/jquery/jquery.min.js" type="text/javascript"></script>

    <title>奖金查询</title>
    <style type="text/css">
        .minMsg {
            background-color: #eee;
        }

            .minMsg ul {
                width: 100%;
                margin-bottom:50px;
            }

                .minMsg ul li {
                    background-color: #fff;
                    margin-top: 10px;
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border-top: 1px solid #eee;
                    border-bottom: 1px solid #eee;
                    height: 60px;
                    line-height: 20px;
                }

                    .minMsg ul li span {
                        padding-left: 2px;
                        height: 40px;
                        margin: 5px;
                        font-size: 30px;
                        width: 10%;
                        float: left;
                    }

                    .minMsg ul li .ctinfo1 {
                        font-size: 16px;
                        height: 50px;
                        line-height: 40px;
                        width: 30%;
                        margin: 0px;
                        float: left;
                        margin-left: 0px;
                        text-align:center;
                    }
                     .minMsg ul li .ctinfo2 {
                        font-size: 16px;
                        height: 50px;
                        line-height: 40px;
                        width:40%;
                        margin: 0px;
                        float: left;
                        margin-left: 0px;
                        text-align:center;
                    }

                    .minMsg ul li .ctinfo4 {
                        font-size: 16px;
                        height: 50px;
                        line-height: 40px;
                        width:30%;
                        margin:0px;
                        float: left;
                        margin-left: 0px;
                        text-align:center;
                    }
                    .minMsg ul li .ctinfo3 {
                        font-size: 15px;
                        height: 50px;
                        line-height: 40px;
                        width: 40%;
                        margin: 0px;
                        float: left;
                        color: #ff6a00;
                        margin-left: 0px;
                        text-align:center;
                    }
                   
    </style>

    <script type="text/javascript">
      


    </script>
</head>

<body>

    <form id="form1" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 35%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">奖金明细</span>
            </div>
        </div>


        <div class="middle">
            <input type="hidden" id="hdqs" value="1"  runat="server"/>
            <div id="div1" class="minMsg minMsg2" style="display: block">
                <div>
                    <input type="hidden" id="hdst" value="-1"  runat="server"/>

                    <ul id="mblist">
                        <li>
                           <%-- <span class='glyphicon glyphicon-th-list blue'></span>--%>
                            <div class='ctinfo1'>小区编号</div>
                            <div class='ctinfo4'>报单编号</div>
                            <div class='ctinfo2'>石斛积分</div>
                        </li>
                    </ul>

                </div>
            </div>
        </div>
             <!-- #include file = "comcode.html" -->
        
    </form>
    <script type="text/jscript">
        var cupindex = 1;
        var qs = $("#hdst").val();
        $(function () {

            getNext();
            $(".sctt li").click(function () {
                var ck = $(this).attr("atr");
                $("#hdst").val(ck);
                $(".sctt li").removeClass("cur");
                $(this).addClass("cur");

                cupindex = 1;
                getNext();
            });

        });
        function getNext() {
            var res = AjaxClass.yejiDetail(cupindex, qs).value;
            if (res != "") {
                if (cupindex == 1) $("#mblist").append(res);
                else {
                    $("#mom").remove(); $(res).appendTo("#mblist");
                }
                $("<div id='mom' class='more' onclick='getNext();' >加载更多..</div>").appendTo("#mblist"); cupindex += 1;
            } else {
                $("#mom").remove();
                $("#end").remove();
                $("<div id='end' class='end'>没有更多了~</div>").appendTo("#mblist");

            }
        }

    </script>



    <script type="text/javascript">
        $(function () {
            $('.mailbtn').on('click', function () {
                $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                var Mindex = $(this).index();
                $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

            })

        })
    </script>
</body>
</script>
       

</html>


