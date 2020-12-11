<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettingsIndex.aspx.cs" Inherits="MemberMobile_PhoneSettings_SettingsIndex" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link href="../../css/bootstrap-cerulean.min.css" rel="stylesheet" />
    <link href="../../css/charisma-app.css" rel="stylesheet" />

    <link href="../../bower_components/fullcalendar/dist/fullcalendar.css" rel="stylesheet" />

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
    <script src="../../JS/sryz.js"></script>
    <style type="text/css">
        ul.dashboard-list1 li {
            padding: 10px;
            list-style: none;
            
            border-top: 1px solid #eee;
        }

        ul.dashboard-list1 a:hover {
            text-decoration: none;
        }

        ul.dashboard-list1 {
            margin: 0;
            padding: 0px;
            margin-top:20px;
               margin-bottom:10px;
          background-color:#fff;
            
        }

            ul.dashboard-list1 li a {
                    display: block;
            }

                ul.dashboard-list1 li a span {
                    display: inline-block;
                    font-size: 18px;
                    font-weight: bold;
                    margin-right: 10px;
                    text-align: right;
                    width: 70px;
                    zoom: 1;
                    color: black;
                }

                ul.dashboard-list1 li a i {
                    float: right;
                    font-size: 10px;
                    padding: 6px;
                    text-shadow: 2px 2px 5px hsl(0, 0%, 61%);
                    /*color:white;*/
                }
        .box-contentn
        { padding:0px;
          margin:0px;
          background-color:#eee;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 35%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">设置</span>
            </div>
        </div>


        <div class="box-contentn">
            <ul class="dashboard-list1"> 
                <li>
                    <a href="CheckPassword.aspx?url=BindingMobilePhone&&type=bphone">更换绑定手机
                        <div style="float:right;">
                             <label id="labtel" runat="server"></label>
                             <i class="glyphicon glyphicon-chevron-right"></i>
                        </div>
                    </a>
                </li>
                    <li>
                    <a href="ChangePassword.aspx?type=setpwd" style="width: 100%">密码修改
                            <i class="glyphicon glyphicon-chevron-right"></i>
                    </a>
                </li>
               
                </ul>
             <ul class="dashboard-list1"> 
                <li>
                    <a href="CheckPassword.aspx?url=BankCard&&type=bcard">银行卡
                         <div style="float:right;">
                             <label id="labbankcard" runat="server"></label>
                             <i class="glyphicon glyphicon-chevron-right"></i>
                        </div>
                    </a>
                </li>

                 

                <li>
                    <a href="CheckPassword.aspx?url=BindZhifubao&&type=zhifubo">支付宝
                          <div style="float:right;">
                             <label id="labzhifubao" runat="server"></label>
                             <i class="glyphicon glyphicon-chevron-right"></i>
                        </div>
                    </a>
                </li>
                <li>
                    <a href="CheckPassword.aspx?url=BindingWeixin&&type=weixin">微信号
                          <div style="float:right;">
                             <label id="labweixin" runat="server"></label>
                             <i class="glyphicon glyphicon-chevron-right"></i>
                        </div>
                    </a>
                </li>

                </ul>
             <ul class="dashboard-list1"> 
                 <li>
                    <a href="CheckPassword.aspx?url=RealName&&type=name">资料修改
                         <div style="float:right;">
                             <label id="labname" runat="server"></label>
                             <i class="glyphicon glyphicon-chevron-right"></i>
                        </div>
                    </a>
                </li> 
                <li>
                    <a href="SetConAddress.aspx?type=setaddress">
                        <%-- <i class="glyphicon glyphicon-comment"></i>--%>
                        <%--<span class="yellow">254</span>--%>
                            收货地址
                            <i class="glyphicon glyphicon-chevron-right glyphicon-black"></i>
                    </a>
                </li>

                
                 </ul>
             <ul class="dashboard-list1"> 
                 <li>
                  <asp:Button ID="btn_reset" CssClass="btn btn-primary btn-lg" Style="width: 100%;" Text="退出登录" OnClientClick="return subform();"
                         runat="server"  OnClick="btn_reset_Click"/>
                </li>
                 </ul>
        </div>

       
        <!-- #include file = "comcode.html" -->
    </form>
</body>
</html>
