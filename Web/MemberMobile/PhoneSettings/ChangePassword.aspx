<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="MemberMobile_PhoneSettings_ChangePassword" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
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

    <title>修改密码</title>
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
        ul {
            margin: 0px;
            padding: 0px;
        }

            ul li {
                list-style: none;
                margin-bottom: 5px;
            }

        .lispan {
            display: inline-block;
            font-size: 16px;
            font-weight: bold;
            margin-right: 10px;
            text-align: center;
            width: 70px;
            zoom: 1;
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

    <script language="javascript" type="text/javascript">
        window.alert = alert;
            $(function () {
                $("#oldPassword").blur(function () {
                    if ($("#oldPassword").val() == "") {
                        webToast("原密码不能为空", "middle", 3000);
                        document.getElementById("oldPassword").style.borderColor = "#b94a48";
                        return;
                    }
                    else {
                        document.getElementById("oldPassword").style.borderColor = "#468847";
                    }
                });
                $("#newPassword").blur(function () {
                    if ($("#newPassword").val() == "") {
                        webToast("请输入新密码", "middle", 3000);
                        document.getElementById("newPassword").style.borderColor = "#b94a48";
                        return;
                    }
                    else {
                        document.getElementById("newPassword").style.borderColor = "#468847";
                    }
                    if ($("#newPassword").val().length < 6 || $("#newPassword").val().length > 11) {
                        webToast("密码不能少于六位或大于十一位", "middle", 3000);
                        document.getElementById("newPassword").style.borderColor = "#b94a48";
                        return;
                    }
                    else {
                        document.getElementById("newPassword").style.borderColor = "#468847";
                    }
                });

                $("#newPassword2").blur(function () {
                    if ($("#newPassword2").val() == "") {
                        webToast("请输入新密码", "middle", 3000);
                        document.getElementById("newPassword2").style.borderColor = "#b94a48";
                        return;
                    }
                    else {
                        document.getElementById("newPassword2").style.borderColor = "#468847";
                    }
                    if ($("#newPassword2").val().length < 6 || $("#newPassword2").val().length > 11) {
                        webToast("密码不能少于六位或大于十一位", "middle", 3000);
                        document.getElementById("newPassword2").style.borderColor = "#b94a48";
                        return;
                    }
                    else {
                        document.getElementById("newPassword2").style.borderColor = "#468847";
                    }

                    if ($("#newPassword2").val() != $("#newPassword").val()) {
                        webToast("两次密码输入不一致", "middle", 3000);
                        document.getElementById("newPassword2").style.borderColor = "#b94a48";
                        return;
                    } else {
                        document.getElementById("newPassword2").style.borderColor = "#468847";
                    }
                });
        });

        function subform() {
            if ($("#oldPassword").val() == "") {
                webToast("原密码不能为空", "middle", 3000);
                return false;
            }
            if ($("#newPassword").val() == "") {
                webToast("新密码不能为空", "middle", 3000);
                return false;
            }
            if ($("#newPassword").val().length < 6 || $("#newPassword").val().length > 11) {
                webToast("密码不能少于六位或大于十一位", "middle", 3000);
                return false;
            }

            if ($("#newPassword2").val() == "") {
                webToast("确认密码不能为空", "middle", 3000);
                return false;
            }

            if ($("#newPassword2").val().length < 6 || $("#newPassword2").val().length > 11) {
                webToast("密码不能少于六位或大于十一位", "middle", 3000);
                return false;
            }

            if ($("#newPassword").val() != $("#newPassword2").val()) {
                webToast("两次密码输入不一致", "middle", 3000);
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" name="form1" method="post">

        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span runat="server" id="action" style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">修改密码</span>
            </div>
        </div>


        <div class="middle" style="margin-top:20px;">
           <%-- <div class="changeBox zcMsg" style="margin: 20px 0px 40px 0px">
            </div>--%>

            <ul>
                <li style="padding:5px;margin-left:5px;">
                    <label class="radio-inline">
                    <input type="radio" name="inlineRadioOptions" id="inlineRadio1" value="0" runat="server" checked="true"/> 登录密码
                    </label>

                    <label class="radio-inline">
                    <input type="radio" name="inlineRadioOptions" id="inlineRadio2" value="1" runat="server"/> 二级密码
                </label>
                </li>

                 <li>
                    <div class="input-group col-md-4">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock green"></i></span>
                        <asp:TextBox ID="oldPassword" CssClass="form-control" TextMode="Password" placeholder="原密码" runat="server" MaxLength="20"></asp:TextBox>
                    </div>
                </li>

                <li>
                    <div class="input-group col-md-4">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock red"></i></span>
                        <asp:TextBox ID="newPassword" CssClass="form-control" TextMode="Password" placeholder="新密码" runat="server" MaxLength="20"></asp:TextBox>
                    </div>
                </li>
                <li>
                    <div class="input-group col-md-4">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock yellow"></i></span>
                        <asp:TextBox ID="newPassword2" CssClass="form-control" TextMode="Password" placeholder="确认密码" runat="server" MaxLength="20"></asp:TextBox>
                    </div>


                </li>
            </ul>
        </div>
         <div class="changeBox zcMsg" style="margin: 20px 0px 40px 0px">
            </div>
        <div style="clear: both">
            <div class="registerMsg">
                 <span> 
                    <asp:Button ID="btn_submit" runat="server" style="width:50%;float:left;"
                                    Text="确 定" CssClass="btn btn-primary btn-lg" OnClientClick="return abc()" OnClick="btn_submit_Click" />
    	            </span>
    	            <span>
                      <asp:Button ID="btn_reset" runat="server"  style="width:50%;float:left;"
                                Text="重 置" CssClass="btn btn-primary btn-lg" OnClick="btn_reset_Click" />
                    </span>
            </div>
        </div>


        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
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
                        <a href="#" class="btn btn-default" data-dismiss="modal">确定</a>
                    </div>
                </div>
            </div>
        </div>
        <!-- #include file = "comcode.html" -->
        <script>
            function alert(data) {
                var x = document.getElementById("p");
                x.innerHTML = data;
                $('#myModal').modal('show');

            }
        </script>

        
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
