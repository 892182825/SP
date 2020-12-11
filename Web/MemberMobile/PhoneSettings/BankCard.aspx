<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankCard.aspx.cs" Inherits="MemberMobile_PhoneSettings_BankCard" EnableEventValidation="false" %>

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

    <title>银行卡</title>
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
            padding: 0px;
            text-align: right;
            font-size: 14px;
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
            $("#txtCName").blur(function () {
                if ($("#txtCName").val() == "") {
                    webToast("开户姓名不能为空", "middle", 3000);
                    document.getElementById("txtCName").style.borderColor = "#b94a48";
                    return;
                }
            });

            $("#txtBankbranchname").blur(function () {
                if ($("#txtBankbranchname").val() == "") {
                    webToast("支行名称不能为空", "middle", 3000);
                    document.getElementById("txtBankbranchname").style.borderColor = "#b94a48";
                    return;
                }
            });

            $("#txtcard").blur(function () {
                if ($("#txtcard").val() == "") {
                    webToast("卡号不能为空", "middle", 3000);
                    document.getElementById("txtcard").style.borderColor = "#b94a48";
                    return;
                }
            });
        });

        function subform() {
            if ($("#txtCName").val() == "") {
                webToast("开户姓名不能为空", "middle", 3000);
                return false;
            }
            if ($("#txtBankbranchname").val() == "") {
                webToast("支行名称不能为空", "middle", 3000);
                return false;
            }
            if ($("#txtcard").val() == "") {
                webToast("卡号不能为空", "middle", 3000);
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

                    <span style="color: #fff; font-size: 18px; margin-left: 33%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">银行卡</span>
                </div>
            </div>



            <div class="middle">
                <div>
                    <span style="font-size: 18px; color: red;">您的银行卡账号必须与您的实名认证信息保持一致，以免不必要的资金纠纷!</span>
                </div>

                <table style="width: 100%;">
                    <tr>
                        
                        <td class="input-group col-md-4" style="padding-right:15px;padding-left:15px">
                        <div class="input-group col-md-4">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox ID="txtName" CssClass="form-control" placeholder="真实姓名" MaxLength="10"  ReadOnly="true" runat="server" meta:resourcekey="txtNameResource1"></asp:TextBox>
                        </div>

                        </td>
                    </tr>

                    <tr>
                        
                        <td class="input-group col-md-4" style="padding-right:15px;padding-left:15px">
                            <div class="input-group col-md-4">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user green"></i></span>
                                <asp:TextBox ID="txtCName" CssClass="form-control" placeholder="开户姓名" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)"  onkeyup="ValidateValue(this)"  runat="server" meta:resourcekey="txtCNameResource1"></asp:TextBox>
                            </div>
                        </td>
                    </tr>


                    <tr>
                        
                        <td class="input-group col-md-4" style="padding-right:15px;padding-left:15px">
                            <span style="float: left; margin-right: 2px; width: 100%">
                                <asp:DropDownList ID="ddlcard" CssClass="ctConPgFor" runat="server" DataTextField="Name" draw="m_country"
                                    Style="width: 100%; height: 38px; border: 1px solid #ccc; border-radius: 3px"
                                    DataValueField="Name">
                                </asp:DropDownList></span>
                        </td>
                    </tr>

                     <tr>
                       
                        <td class="input-group col-md-4" style="padding-right:15px;padding-left:15px">
                            <div class="input-group col-md-4">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-bookmark green"></i></span>
                                <asp:TextBox ID="txtBankbranchname" CssClass="form-control" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" onkeyup="ValidateValue(this)"  placeholder="支行名称"  runat="server" meta:resourcekey="txtBankbranchnameResource1"></asp:TextBox>
                            </div>
                        </td>
                    </tr>


                    <tr>
                        

                        <td class="input-group col-md-4" style="padding-right:15px;padding-left:15px">
                            <div class="input-group col-md-4">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-credit-card green"></i></span>
                                <asp:TextBox ID="txtcard" CssClass="form-control" onkeypress="return kpyzsz();" onkeyup="sz(this)" onafterpaste="sz(this)" placeholder="银行卡号" MaxLength="50" runat="server" meta:resourcekey="txtcardResource1"></asp:TextBox>

                            </div>
                        </td>
                    </tr>


                </table>
            </div>

            <div style="clear: both">
                <div class="registerMsg">
                    <asp:Button ID="btn_submit" CssClass="btn btn-primary btn-lg" Style="width: 100%;" Text="确认" OnClientClick="return subform();"
                        OnClick="btn_submit_Click" runat="server" />
                </div>
            </div>
        </div>
         <div style="height: 55px;"></div>


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
        <div id="colorbox" class="" role="dialog" tabindex="-1" style="display: none;">
            <div id="cboxWrapper">
                <div>
                    <div id="cboxTopLeft" style="float: left;"></div>
                    <div id="cboxTopCenter" style="float: left;"></div>
                    <div id="cboxTopRight" style="float: left;"></div>
                </div>
                <div style="clear: left;">
                    <div id="cboxMiddleLeft" style="float: left;"></div>
                    <div id="cboxContent" style="float: left;">
                        <div id="cboxTitle" style="float: left;"></div>
                        <div id="cboxCurrent" style="float: left;"></div>
                        <button type="button" id="cboxPrevious"></button>
                        <button type="button" id="cboxNext"></button>
                        <button id="cboxSlideshow"></button>
                        <div id="cboxLoadingOverlay" style="float: left;"></div>
                        <div id="cboxLoadingGraphic" style="float: left;"></div>
                    </div>
                    <div id="cboxMiddleRight" style="float: left;"></div>
                </div>
                <div style="clear: left;">
                    <div id="cboxBottomLeft" style="float: left;"></div>
                    <div id="cboxBottomCenter" style="float: left;"></div>
                    <div id="cboxBottomRight" style="float: left;"></div>
                </div>
            </div>
            <div style="position: absolute; width: 9999px; visibility: hidden; display: none; max-width: none;"></div>
        </div>
    </form>
</body>
</html>
