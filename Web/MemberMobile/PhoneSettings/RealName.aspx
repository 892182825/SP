<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RealName.aspx.cs" Inherits="MemberMobile_PhoneSettings_RealName"  EnableEventValidation="false"%>

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

    <title>资料修改</title>
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
    <script src="../../JS/sryz.js"></script>
   <%-- <script src="../../Company/js/jquery-1.4.3.min.js" type="text/javascript"></script>--%>
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

            $("#txtName").blur(function () {
                if ($("#txtName").val() == "") {
                    webToast("姓名不能为空", "middle", 3000);
                    document.getElementById("txtName").style.borderColor = "#b94a48";
                    return;
                }
            });

            $("#txtcard").blur(function () {
                if ($("#txtcard").val() == "") {
                    webToast("身份证不能为空", "middle", 3000);
                    document.getElementById("txtcard").style.borderColor = "#b94a48";
                    return;
                }
                else {
                    var card = $("#txtcard").val();
                    var result = AjaxClass.VerifyPaperNumber(card).value;
                    if (result == null) {
                        webToast("无效的身份证", "middle", 3000);
                        document.getElementById("txtcard").style.borderColor = "#b94a48";
                        return;
                    }
                    else {
                        if (result.indexOf(",") <= 0) {
                            webToast(result, "middle", 3000);
                            document.getElementById("txtcard").style.borderColor = "#b94a48";
                            return;
                        }
                    }
                    document.getElementById("txtcard").style.borderColor = "#468847";
                }
              
            });

        });

        function subform() {
            if ($("#txtName").val() == "") {
                webToast("姓名不能为空", "middle", 3000);
                return false;
            }

            if ($("#txtcard").val() == "") {
                webToast("身份证不能为空", "middle", 3000);
                return false;
            } else {
                var card = $("#txtcard").val();
                var hcard = $("#Hfcard").val()
                if (hcard == "") {
                    var result = AjaxClass.VerifyPaperNumber(card).value;
                    if (result == null) {
                        webToast("无效的身份证", "middle", 3000);
                        return false;
                    }
                    else {
                        if (result.indexOf(",") <= 0) {
                            webToast(result, "middle", 3000);
                            return false;
                        }
                    }
                } 
            }
            if (confirm('您确认身份信息正确吗？')) {
                return true;
            } else {
                return false;
            }
           
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="navbar navbar-default" role="navigation">
                <div class="navbar-inner">
                    <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                    <span style="color: #fff; font-size: 18px; margin-left: 35%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">资料修改</span>
                </div>
            </div>
            <div class="middle" style="margin-top:20px;">
                <ul>
                    <li>
                        <div >
                            <span style="font-size:18px;color:red;font-weight:bold;">注意事项：您的身份实名信息关系到您的资金交易账号，请认真填写!</span>
                        </div>
                    </li>


                    <li>
                        <div class="input-group col-md-4">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user green"></i></span>
                            <asp:TextBox ID="txtName" CssClass="form-control" placeholder="姓名" MaxLength="10"  runat="server" meta:resourcekey="txtNameResource1"></asp:TextBox>
                        </div>
                    </li>
                     <li>
                        <div class="input-group col-md-4">
                            <span class="input-group-addon"><i class="glyphicon  glyphicon-th-large green"></i></span>
                            <asp:TextBox ID="txtcard" CssClass="form-control" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" onkeyup="ValidateValue(this)" placeholder="身份证" MaxLength="18" runat="server" meta:resourcekey="txtcardResource1"></asp:TextBox>
                            <asp:HiddenField  runat="server" id="Hfcard" Value=""/>
                        </div>
                    </li>
                </ul>
            </div>

            <div style="clear: both">
                <div class="registerMsg">
                    <asp:Button ID="btn_submit" CssClass="btn btn-primary btn-lg" Style="width: 100%;" Text="确认" OnClientClick="return subform();"
                        OnClick="btn_submit_Click" runat="server" />
                </div>
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
