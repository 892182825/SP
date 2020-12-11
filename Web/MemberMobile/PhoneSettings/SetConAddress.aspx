<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetConAddress.aspx.cs" Inherits="MemberMobile_PhoneSettings_SetConAddress" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/CountryCityCode_mobile.ascx" TagPrefix="Uc1" TagName="CountryCityCode_mobile" %>
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

    <title>收货地址</title>
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

        .tabletd1 {
            width: 30%;
            max-width: 100%;
            margin-bottom: 5px;
            padding: 5px;
            text-align: right;
            font-size: 14px;
        }

        .web_toast {
            position: fixed;
            margin: 50px 10px;
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

        @font-face {
            font-family: 'mui';
            src: url(fonts/mui.tff);
        }

        .mui-content {
            background: url(img/zcbg.png) 0 0 repeat;
        }

        select.ctConPgFor {
            height: 30px;
            padding: 0;
            font-size: 14px;
            padding-left: 5px;
        }


        .ctConPgTxt {
            height: 30px;
            padding: 0;
            font-size: 14px;
            padding-left: 5px;
            width:100%;
        }

        #go {
            width: 100%;
            margin: 0;
            padding: 0;
            top: 0;
            height: 28px;
            color: #FFF;
            text-align: center;
            background-color: #46a123;
            color: #FFF;
            border: 1px solid #666;
            background-image: linear-gradient(#46a123,#46a123);
            outline: none;
            cursor: pointer;
            padding: 2px 20px;
            border-radius: 3px;
            text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);
            box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.3), 0 1px 1px rgba(0, 0, 0, 0.15);
        }

        .registerRow .title {
            float: left;
            width: 27%;
            text-align: right;
            line-height: 30px;
        }

        .registerRow .inputBox {
            width: 70%;
            float: left;
        }

        .registerRow {
            overflow: hidden;
            padding-bottom: 10px;
        }

        form {
            height: 100%;
                margin-bottom: 55px;
        }

        .title {
            font-size: 13px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        window.alert = alert;
        function isIntTel(txtStr) {
            var validSTR = "1234567890";
            for (var i = 1; i < txtStr.length + 1; i++) {
                if (validSTR.indexOf(txtStr.substring(i - 1, i)) == -1) {
                    return false;
                }
            }
            return true;
        }
        $(function () {
            $('#CountryCity1_ddlX').change( function () {
               var sobj = document.getElementById("Txtyb1");
               sobj.value = AjaxClass.GetAddressCode($(this).val()).value;
            });


            $("#Txtyddh").blur(function () {
                if ($("#Txtyddh").val() == "" || $("#Txtyddh").val().length!=11) {
                    webToast("收货手机号码格式不对", "middle", 3000);
                    document.getElementById("Txtyddh").style.borderColor = "#b94a48";
                    return false;
                }
                else {
                    var txtyddh = $("#Txtyddh").val();
                    var isInt = isIntTel(txtyddh);
                    if (!isInt) {
                        webToast("手机只能输入数字", "middle", 3000);
                        document.getElementById("Txtyddh").style.borderColor = "#b94a48";
                        return false;
                    }
                    document.getElementById("Txtyddh").style.borderColor = "#468847";
                }
            }); 


            $("#Txtyb1").blur(function () {
                if ($("#Txtyb1").val().length!=6) {
                    webToast("收货邮编格式不对", "middle", 3000);
                    document.getElementById("Txtyb1").style.borderColor = "#b94a48";
                    return false;
                }
                else
                {
                    var Txtyb1 = $("#Txtyb1").val();
                    var isInt = isIntTel(Txtyb1);
                    if (!isInt) {
                        webToast("邮编只能输入数字", "middle", 3000);
                        return false;
                    }
                    document.getElementById("Txtyb1").style.borderColor = "#468847";
                }
            });

            $("#txtdizhi").blur(function () {
                if ($("#txtdizhi").val() == "") {
                    webToast("地址不能为空", "middle", 3000);
                    document.getElementById("txtdizhi").style.borderColor = "#b94a48";

                    return false;
                }
                else {
                    document.getElementById("txtdizhi").style.borderColor = "#468847";
                }
            });
        });
        function subform() {
            if ($("#Txtyddh").val() == ""||$("#Txtyddh").val().length!=11) {
                webToast("收货手机格式不对", "middle", 3000);
                return false;
            }
            var checkaddress = offmTelOnblur3();
            if (checkaddress == 0) {
                webToast("请选择地址！", "middle", 3000); 
                return false;
            }

            if ($("#Txtyb1").val() == "") {
                webToast("收货邮编不能为空", "middle", 3000);
                return false;
            }
            if ($("#txtdizhi").val() == "") {
                webToast("收货地址不能为空", "middle", 3000);
                return false;
            }

            return true;
        }

        function offmTelOnblur3() {
            //debugger;
            if ($("#CountryCity1_ddlCountry").val() == "请选择") {
                return 0;
            }
            if ($("#CountryCity1_ddlP").val() == "请选择") {
                return 0;
            } if ($("#CountryCity1_ddlCity").val() == "请选择") {
                return 0;
            }
            if ($("#CountryCity1_ddlX").val() == "请选择") {
                return 0;
            }

            if ($("#CountryCity2_ddlCountry").val() == "请选择") {
                return 1;
            }
            if ($("#CountryCity2_ddlP").val() == "请选择") {
                return 1;
            } if ($("#CountryCity2_ddlCity").val() == "请选择") {
                return 1;
            }
            if ($("#CountryCity2_ddlX").val() == "请选择") {
                return 1;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="overflow:auto;">
            <div class="navbar navbar-default" role="navigation">
                <div class="navbar-inner">
                    <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                    <span runat="server" style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">收货地址</span>
                </div>
            </div>

            <div class="clearfix registerRow" style="margin: 20px 0px 20px 0px">
            </div>
            <div class="clearfix registerRow">
                <span class="title">
                    <span>收货电话：</span>
                </span>
                <span class="inputBox">
                    <span>
                        <asp:TextBox ID="Txtyddh" onkeypress="return kpyzsz();" onkeyup="   szxs(this);" onafterpaste="szxs(this)" CssClass="form-control" runat="server" MaxLength="11"></asp:TextBox>
                    </span>
                </span>
            </div>

            <div class="clearfix registerRow">
                <span class="title">
                    <span>收货姓名：</span>
                </span>
                <span class="inputBox">
                    <span>
                        <asp:TextBox ID="TxtyConsignee" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" onkeyup="ValidateValue(this)" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:HiddenField  ID="hiddid" runat="server"/>
                    </span>
                </span>
            </div>



            <div class="clearfix registerRow">
                <span class="title">
                    <span>收货地址：</span>
                </span>
                <span class="inputBox">
                    <Uc1:CountryCityCode_mobile runat="server" ID="CountryCity1" />
                </span>
            </div>


              <div class="clearfix registerRow">
                <span class="title">
                    <span>收货邮编：</span>
                </span>
                <span class="inputBox">
                    <span>
                        <asp:TextBox ID="Txtyb1" CssClass="form-control" onkeypress="return kpyzsz();" onkeyup="   szxs(this);" onafterpaste="szxs(this)" runat="server" MaxLength="6"></asp:TextBox>
                        <span id="span1" style="color: red;"></span></span>
                </span>
            </div>

            <br />
            <div class="clearfix registerRow">
                <span class="title">
                    <span>详细地址：</span>
                </span>
                <span class="inputBox">
                    <span>
                        <asp:TextBox ID="txtdizhi" runat="server" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" onkeyup="ValidateValue(this)" MaxLength="30" CssClass="form-control"></asp:TextBox>
                    </span>
                </span>
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
