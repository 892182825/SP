<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterMemberTG.aspx.cs" Inherits="MemberMobile_RegisterMemberTG" EnableEventValidation="false"  %>

<%@ Register Src="../UserControl/CountryCityPCode1.ascx" TagName="CountryCityPCode"
    TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no"/>
    <meta name="apple-mobile-web-app-capable" content="yes"/>
    <meta name="apple-mobile-web-app-status-bar-style" content="black"/>
    <meta name="format-detection" content="telephone=no"/>
    <meta http-equiv="x-ua-compatible" content="ie=7" />

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

    <%--<script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script src="js/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>--%>
    <%-- <script type="text/javascript" src="js/WdatePicker.js"></script>--%>

    <%--   <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>--%>
    <link rel="stylesheet" href="css/style.css">

    <style type="text/css">

        .re_pro {
            color:red;
        }
        .changeBox ul li .changeLt {
            width: 30%;
        }

        .changeBox ul li .changeRt {
            width: 65%;
        }

            .changeBox ul li .changeRt .textBox {
                width: 100%;
            }

        .zcMsg ul li .changeRt .zcSltBox {
            width: 100%;
        }

        .zcMsg ul li .changeRt .zcSltBox2 {
            width: 39%;
        }

        #txtadvpass {
            width: 79%;
            border: 1px solid #ccc;
        }
		.xs_footer li a{display:block;padding-top:40px;background:url(images/shouy1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li:nth-of-type(2) a{background:url(images/jiangj1.png) no-repeat center 10px;background-size:32px;}
.xs_footer li:nth-of-type(3) a{background:url(images/xiaoxi1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li:nth-of-type(4) a{background:url(images/anquan1.png) no-repeat center 8px;background-size:27px;}
        #CountryCity2_dv_cpc span {
            width:39.5%
        }
        .changeBox{
            padding:30px 2% 10px
        }
        ul {
        margin:0px;
        padding:0px;
        }
        ul li {
       list-style:none; 
       margin-bottom:5px;
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

 

    <title>会员注册</title>
     <script type="text/javascript">
         function GetCCode_s2() {

         }
        

                  function ChangeNumber() {
                      var number = document.getElementById("txtNumber");
                      var bh = AjaxClass.GetMemberNumber().value;
                      number.value = bh;
                      $("#HFNumber").val(bh);
                  }
                 

                 
                 
    </script>
    <script type="text/javascript">
        $(function () {
            //$("[name='Direct']").blur(function () {
            //    if ($("[name='Direct']").val() == "") {
            //        webToast("推荐编号不能为空", "middle", 3000);
            //        return;
            //    }
            //    $.ajax({
            //        type: "POST",
            //        url: "/member/Tm",
            //        data: { id: $("[name='Direct']").val() },
            //        success: function (sesponseTest) {
            //            if (sesponseTest != "1") {
            //                webToast(sesponseTest, "middle", 3000);
            //                return;
            //            }
            //        }
            //    });
            //});
            $("#txtNamee").blur(function () {
                if ($("#txtNamee").val() == "") {
                    webToast("昵称不能为空", "middle", 3000);
                    document.getElementById("txtNamee").style.borderColor = "#b94a48";
                   
                    return;
                }
                else {
                    document.getElementById("txtNamee").style.borderColor = "#468847";
                }
            });
            //$("[name='Membername']").blur(function () {
            //    if ($("[name='Membername']").val() == "") {
            //        webToast("请输入姓名", "middle", 3000);
            //        return;
            //    }
            //})
            
            $("#txtTele").blur(function () {
                if ($("#txtTele").val() == "") {
                    webToast("手机号不能为空", "middle", 3000);
                    document.getElementById("txtTele").style.borderColor = "#b94a48";
                    return;
                }
                else {
                    document.getElementById("txtTele").style.borderColor = "#468847";
                }
                if (!phoneCheck.test($("#txtTele").val())) {
                    webToast("手机号码输入有误，请重新输入", "middle", 3000);
                    document.getElementById("txtTele").style.borderColor = "#b94a48";
                    return;
                }
                else {
                    document.getElementById("txtTele").style.borderColor = "#468847";
                }
                
            });
            $("#txtPassword").blur(function () {
                if ($("#txtPassword").val() == "") {
                    webToast("密码不能为空", "middle", 3000);
                    document.getElementById("txtPassword").style.borderColor = "#b94a48";
                    return;
                }
                else {
                    document.getElementById("txtPassword").style.borderColor = "#468847";
                }
                if ($("#txtPassword").val().length < 6 || $("#txtPassword").val().length > 10) {
                    webToast("密码不能少于六位或大于十位", "middle", 3000);
                    document.getElementById("txtPassword").style.borderColor = "#b94a48";
                    return;
                }
                else {
                    document.getElementById("txtPassword").style.borderColor = "#468847";
                }
            });
            $("#txtPassword2").blur(function () {
                if ($("#txtPassword2").val() == "") {
                    webToast("请确认密码", "middle", 3000);
                    document.getElementById("txtPassword2").style.borderColor = "#b94a48";
                    return;
                }
                else {
                    document.getElementById("txtPassword2").style.borderColor = "#468847";
                }
                if ($("#txtPassword2").val().length < 6 || $("#txtPassword2").val().length > 10) {
                    webToast("确认密码不能少于六位或大于十位", "middle", 3000);
                    document.getElementById("txtPassword2").style.borderColor = "#b94a48";
                    return;
                }
                else {
                    document.getElementById("txtPassword2").style.borderColor = "#468847";
                }
            });
        });

        var phoneCheck = /^1[0-9]{10}$/;
        var reg = /^[1-9]{1}[0-9]{14}$|^[1-9]{1}[0-9]{16}([0-9]|[xX])$/;

        function checkAll() {
            // alert('sdfgsdfsd3');
            //if ($("[name='Direct']").val() == "") {
            //    webToast("推荐编号不能为空", "middle", 3000);
            //    return;
            //}

          

            $("form").submit();
        }
        


</script>
     <script language="javascript" type="text/javascript">

         
function subform() {
    if ($("#txtNamee").val() == "") {
        webToast("昵称不能为空", "middle", 3000);
        return false;
    }

    if ($("#txtTele").val() == "") {
        webToast("手机号不能为空", "middle", 3000);
        return false;
    }
    if ($("#txtTele").val() == "") {
        webToast("手机号不能为空", "middle", 3000);
        return false;
    }
    if ($("#txtTele").val().length !=11) {
        webToast("手机号码格式不对", "middle", 3000);
        return false;
    }
    if ($("#txtPassword").val() == "") {
        webToast("密码不能为空", "middle", 3000);
        return false;
    }

    if ($("#txtPassword").val().length < 6 || $("#txtPassword").val().length > 10) {
        webToast("密码不能少于六位或大于十位", "middle", 3000);
        return false;
    }

    if ($("#txtPassword2").val() == "") {
        webToast("请确认密码", "middle", 3000);
        return false;
    }

    if ($("#txtPassword").val().length < 6 || $("#txtPassword2").val().length > 10) {
        webToast("确认密码不能少于六位或大于十位", "middle", 3000);
        return false;
    }

    if ($("#txtPassword").val() != $("#txtPassword2").val()) {
        webToast("两次密码输入不一致", "middle", 3000);
        return false;
    }
    if ($("#CB:checked").length == 0) {
        webToast("不同意注册协议则无法继续注册!", "middle", 3000);
        return false;
     }

    return true;

}
$(function () {
    $('.sideBar').height('94%')
})
    </script>
    <script type="text/javascript">

        //选择不同语言是将要改的样式放到此处


        window.alert =alert;
        //function alert(data) {
        //    var x = document.getElementById("p");
        //    x.innerHTML = data;
        //    $('#myModal').modal('show');
        //}
        
        
    </script>

</head>
<body>
     
      <form id="Form1" runat="server">
          <div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	
            
                <span style="color:#fff;font-size:18px;margin-left:45%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">注册</span>
            </div>
              </div>
        <div class="middle">
            <%--     <h2 class="regTabPrompt"></h2>--%>
            <div class="changeBox zcMsg" style="margin:20px 0px 40px 0px">
            <ul>
                <li style="height:auto">
                    <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon  glyphicon-th-large green"></i></span>
                        <asp:TextBox ID="txtNumber" CssClass="form-control"  MaxLength="50" ReadOnly="true" runat="server" style="border:0;color:red;font-size:16px"></asp:TextBox>
                       
                         
                     </div>
                    </li>
                <li>   
                    <%--<div class="changeLt">
                     
                       <b>*</b>
                    <%=GetTran("003176","真实姓名")%>：</div>
                     <div class="changeRt">
                       
                         <span id="info_name" style="display: none;" class="re_pro"></span>
                    </div>--%>

                    <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user blue"></i></span>
                         <asp:TextBox ID="txtNamee" CssClass="form-control" placeholder="昵称" MaxLength="10" onkeyup="ValidateValue(this)"  onkeydown="ValidateValue(this)" onblur="ValidateValue(this)"  runat="server" meta:resourcekey="txtNameResource1"></asp:TextBox>
                   
                </div>
                    </li>
                <li>
                 <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-phone"></i></span>
                        <asp:TextBox ID="txtTele" CssClass="form-control" onkeyup="sz(this)" onafterpaste="sz(this)" placeholder="手机号" runat="server" data-toggle="tooltip" MaxLength="11" meta:resourcekey="txtTeleResource1"></asp:TextBox>
                         </div>
                    </li>
               
                <li>
                     <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock red"></i></span>
                        <asp:TextBox  ID="txtPassword" CssClass="form-control" TextMode="Password"  placeholder="登录密码" runat="server" MaxLength="20"></asp:TextBox>  
                         </div>
                    
                    
                </li>
                 <li>
                     <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock yellow"></i></span>
                        <asp:TextBox  ID="txtPassword2" CssClass="form-control" TextMode="Password" placeholder="确认密码" runat="server" MaxLength="20"></asp:TextBox>  
                         </div>
                    
                   
                </li>


                <li style="height:auto">
                   <div class="changeLt">
                       <b>*</b>
                    收货地址:</div>
                     <div class="changeRt"  >
                        <uc1:CountryCityPCode ID="CountryCity2"  style="width:80%;" runat="server" />
                    </div></li>
                <li>
                     <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user blue"></i></span>
                        <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="详细地址" runat="server" MaxLength="150" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                         </div></li>



                <li>
                    <span class="re_text"><span style="color: Red"></span>
                       提货方式：</span> 
                    <span>
                    <asp:RadioButtonList ID="rbltotaltype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" >
                        <asp:ListItem Value="0" Selected="true">邮寄&nbsp;</asp:ListItem>
                        
                    </asp:RadioButtonList>
                    </span>
                </li>
                <%--<li>
                      
                    <uc2:SearchPlacement_DoubleLines1 ID="SearchPlacement_DoubleLines1"    runat="server" />
                   
                    <span id="info_txtPlacement" style="display: none;" class="re_pro"></span>
                     
                </li>--%>

<%--                <li>    <div class="changeLt">&nbsp;</div>      <div class="changeRt">
                       <input  type="button" style="float:left; width:80%; line-height:35px; height:35px;  padding:0 5px;text-align:center; background-color:#96c742; " onclick="AutoSet();"  value="<%=GetTran("009130","自动安置")%>"  />
                    </div>
                </li>

                <li style="display: none;"><span class="re_text"><span style="color: Red">* </span>区位：</span>
                    <span>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" meta:resourcekey="RadioButtonList1Resource1">
                            <asp:ListItem Selected="True" Value="1">左区</asp:ListItem>
                            <asp:ListItem Value="2">右区</asp:ListItem>
                        </asp:RadioButtonList>
                    </span></li>--%>
            </ul>
            
                        
                   
                   <%-- <td width="300">
                        <asp:Button ID="ImageButton1" CssClass="btn2" Text="注册" OnClientClick="return subform();"
                            OnClick="Button1_Click" runat="server" />
                    </td>--%>
             
            <div style="clear: both">
                 <div class="registerMsg">
          <%--  <input id="Checkbox1" type="checkbox">--%> <span style="display:block;overflow:hidden;text-align:left;">  <label for="checkbox" >
                           <asp:CheckBox ID="CB" runat="server" style="margin-top:5px"/><a style="text-decoration: none; color: #0092D2; cursor: pointer" onclick="showXY()"
                            target="_blank">《注册协议》</a></label>
                        </span>
        </div>
                    
                         <asp:Button ID="ImageButton1" CssClass="btn btn-primary btn-lg" style="width:100%;"  Text="注册" OnClientClick="return subform();"
                            OnClick="Button1_Click" runat="server"  />
                    
        
       

            </div>
        	
         
         
           <%-- <div style="clear: both">
            </div>--%>
        
   </div>

        <!--注册结束-->
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
                    <%--<a href="#" class="btn btn-primary" data-dismiss="modal">Save changes</a>--%>
                </div>
            </div>
        </div>
    </div>
          
   
        <%--     <uc2:bottom runat="server" ID="bottom" />
         --%>    
          <script type="text/javascript" src="../MemberMobile/js/RegisterXY.js"></script>
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
<script type="text/javascript" src="../JS/sryz.js"></script>

  
   
    </form>
</body>
      
  <script>
      function alert(data) {
          var x = document.getElementById("p");
          x.innerHTML = data;
          $('#myModal').modal('show');

      }
</script>
       <%-- <script type="text/javascript" src="../MemberMobile/js/RegisterXY.js"></script>--%>

</html>
