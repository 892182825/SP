<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberCash.aspx.cs" Inherits="Member_MemberCash" %>
<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
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
    <title>提现申请</title>
    <link rel="stylesheet" href="CSS/style.css">
    <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(
            function () {
                $('#money').blur(function () {
                    var money = $("#money").val();
                    var res = AjaxClass.Getsxf1(money);//违约金
                    var res1 = AjaxClass.Getsxf(money);//手续费
                    if (res != "") {
                        try {
                            $('#txsxf').css('display', 'inline');
                            //var txsxf =res1.value;
                            var djje = parseFloat(res1.value) + parseFloat(res.value)
                            $("#SXF").text(djje);
                            $("#HiddenField1").val(res.value)
                            $("#HiddenField2").val(res1.value)
                            $('#SXF').css('display', 'inline');

                        } catch (e) {
                            return false;
                        }
                    }

                })
            })
    </script>
    <script type="text/javascript">
        window.alert = alert;
        $(function () {
            a.dianji();
        })
        var a = {
            dianji: function () {
                $(".DD").on("click", function () {
                    location.href = "/MemberDateUpdatePhone/Index?";
                })
            },
        }

    </script>

    <script type="text/javascript">
        function abc() {
            //return true;
            if (confirm('您确定要向公司提现申请吗？')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                } else {
                    alert('不可重复提交！');
                    return false;
                }
            } else { return false; }
        }
    </script>

    <style>
        
 
        .fiveSquareBox {
            margin-bottom:4px;
        }
        input[type=checkbox] {
            float: right;
            width: auto;
            margin-right: 45px;
        }
        .proLayerLine ul li {
            overflow:hidden;
            width:50%;
            float:left;
            line-height:28px;

        }
        .proLayerLine ul {
            overflow:hidden
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
        .l_left {
            width: 45%;
    float: left;
    margin-top: 50px;
        }
        .l_right {
            width: 45%;
    float: left;
    height: 50px;
    margin-top: 50px;
    border-style: none dashed none none;
    border-color: #fff;
        }
    </style>
    <script type="text/javascript" >
        $(function () {
            //选择不同语言是将要改的样式放到此处
            var lang = $("#lang").text();
            // alert(1);
            if (lang != "L001") {
                //alert("1111");
                $('.zcMsg p').css({ 'font-size': '14px', 'white-space': 'normal', 'textAlign': 'left' })
                //alert("MemberCash");
                $('.changeBox ul li .changeLt').width('100%').css('textAlign', 'left')
                $('.changeBox ul li .changeRt').width('100%')
                $('.changeBox ul li').css('padding-left', '2%')
            }
        });
 </script> 
</head>

<body style="background-color: #f1f0f0;">
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
 
    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">	    申请提现</span>
            </div>
        </div>
      
        <div class="middle">
            <div class="changeBox zcMsg" style="position:relative;background-color: #f1f0f0;padding-top: 5%;">
                
                <div style="background-image:url(img/jb-1.png);width:90%;height:150px;margin-left: 5%;color:#fff;background-repeat: no-repeat;background-size: 100% 100%;text-align: center;-moz-background-size: 100% 100%;">
                  <div class="l_left">
                        <div class="changeLt"  style=" font-size: 16px;">静态FTC：</div>
                        <div class="changeRt">
                            <asp:Label ID="rmoney" runat="server" style="color:#fff;font-size: 23px;"></asp:Label>
                        </div>
                   </div>
                    <div class="l_right">
                        <div class="changeLt"  style=" font-size: 16px;">动态USDT：</div>
                        <div class="changeRt">
                            <asp:Label ID="Label1" runat="server" style="color:#fff;font-size: 23px;"></asp:Label>
                        </div>
                    </div>
                     </div>

                <div class="zzBox" id="Div1" style="margin-top: -3%;">
                <asp:Button ID="Button2" runat="server" Height="35px" Width="93%" style="height: 45px;width:93%;margin-top: 25px;border-radius: 5px;margin-left: 12px;font-size: 20px;font-weight: 600;background-color: #0057c8;color: #FFF;border: 1px solid #5da1fa;background-image: linear-gradient(#54b4eb, #2fa4e7 60%, #1d9ce5);text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                    Text="提现信息查询" CssClass="anyes"  OnClick="btn_reset_Click" />
            </div>
                    
                <div style="width: 90%;background-color: #fff;margin-left: 5%;margin-top: 5%;border-radius: 10px;">
                    
                
                <ul style="margin-left: 5%;padding-top: 5%;width: 95%;">
                    <li>
                        <div class="changeLt"  style=" font-size: 18px;width: 100%;text-align: left;font-weight: 600;">提现金额：</div>
                        <div class="changeRt">
                            <asp:TextBox CssClass="ctConPgTxt" ID="money" runat="server"  MaxLength="15" style="width:280px;height:40px;border-radius: 5px;"></asp:TextBox>
                            <div style="text-align: center;width:280px;">
                        <div style=" font-size: 12px;">提现会扣除<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>%作为提现手续费,提U扣5%。
                            
                                                     
                        </div>
                        
                        </div>
                    </li>

                    <li style="display:none;">
                        <div class="changeLt">会员编号：</div>
                        <div class="changeRt">
                            <asp:Label ID="number" runat="server"></asp:Label>
                        </div>
                    </li>
                     
                      
                       <li>
                          
                           <div class="changeLt"  style=" font-size: 14px;width: 100%;text-align: left;font-weight: 600;">账户选择</div>
                           <div class="changeRt">
                        <asp:RadioButtonList ID="DropDownList1" RepeatDirection="Horizontal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="静态FTC" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="动态U"></asp:ListItem>
                            
                            </asp:RadioButtonList>
                            </div>
                    </li>
                    <li>
                          
                           <div class="changeLt"  style=" font-size: 14px;width: 100%;text-align: left;font-weight: 600;">到账选择</div>
                           <div class="changeRt">
                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="1" Text="钱包" Selected="True"></asp:ListItem>
                           <%--<asp:ListItem Value="2" Text="商城"></asp:ListItem>--%>
                            
                            </asp:RadioButtonList>
                            </div>
                    </li>
                    

                    <li style="display:none">
                        <div class="changeLt" style="font-size: 12px">备注：</div>
                        <div class="changeRt">
                            <asp:TextBox CssClass="ctConPgTxt2" ID="remark" runat="server" Height="56px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </li>
                </ul>
            
            <div class="zzBox" id="zzBox" style="margin-top: -3%;">
                <asp:Button ID="Button1" runat="server" Height="35px" Width="93%" style="height: 45px;width:93%;margin-top: 25px;border-radius: 5px;margin-left: 12px;font-size: 20px;font-weight: 600;background-color: #0057c8;color: #FFF;border: 1px solid #5da1fa;background-image: linear-gradient(#54b4eb, #2fa4e7 60%, #1d9ce5);text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                    Text="提现申请" CssClass="anyes"  OnClick="Button1_Click" />
            </div>
            <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />

             <input type="hidden" id="Wyj" runat="server" />
            <div style="text-align: center; margin-bottom: 20px; margin-top: -20px;font-size: 14px;">
                       
                            
                            <br />
                            提现审核时间
                            <br />
                            09:30 - 18:00
                      
                  
                       
                       
                    </div>
                    </div>
            </div>
        </div>
        <br />
        <div class="modal fade" id="myModall" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal">×</button>--%>
                    <h3>系统提示</h3>
                </div>
                <div class="modal-body">
                    <p id="p">Here settings can be configured...</p>
                </div>
                <div class="modal-footer">
                   
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

    <script type="text/javascript">
        $(function () {
            $('.mailbtn').on('click', function () {
                $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                var Mindex = $(this).index();
                $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();
            })
        });
    </script>
    </form>
</body>
</html>

