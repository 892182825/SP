<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetMemberPlace.aspx.cs" Inherits="SetMemberPlace" EnableEventValidation="false"  %>


<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CountryCityPCode1.ascx" TagName="CountryCityPCode"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/SearchPlacement_DoubleLines1.ascx" TagName="SearchPlacement_DoubleLines1"
    TagPrefix="uc2" %>


<%@ Register Src="~/UserControl/STop.ascx" TagPrefix="uc1" TagName="STop" %>
<%@ Register Src="~/UserControl/SLeft.ascx" TagPrefix="uc1" TagName="SLeft" %>
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
    <script src="../JS/sryz.js"></script>

        <link rel="stylesheet" href="css/style.css" />

    <%--<script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script src="js/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>--%>
    <%-- <script type="text/javascript" src="js/WdatePicker.js"></script>--%>

     <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script> 
   <%-- <link rel="stylesheet" href="css/style.css">--%>

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
       margin-bottom:10px;
        margin-top:10px;
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
            $("#txtName").blur(function () {
                if ($("#txtName").val() == "") {
                    webToast("姓名不能为空", "middle", 3000);
                    document.getElementById("txtName").style.borderColor = "#b94a48";
                   
                    return;
                }
                else {
                    document.getElementById("txtName").style.borderColor = "#468847";
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
    if ($("#txtName").val() == "") {
        webToast("姓名不能为空", "middle", 3000);
        return false;
    }

    if ($("#txtTele").val() == "") {
        webToast("手机号不能为空", "middle", 3000);
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

        $(function () {
            var lang = $("#lang").text();
            $('#Button1 ').css('display', 'none');
            //alert(lang);
            if (lang != "L001") {

                $('.changeBox ul li .changeLt').css({ 'width': '100%', 'text-align': 'left' })
                // alert(11111);
                $('.changeBox ul li .changeRt').css({ 'width': '100%', 'margin-left': '10%' })
                $('.changeBox ul li ').css('height', 'auto')

            }

        })

        window.alert =alert;
        //function alert(data) {
        //    var x = document.getElementById("p");
        //    x.innerHTML = data;
        //    $('#myModal').modal('show');
        //}
        
        
    </script>

</head>
<body>
     <b id="lang" style="display: none;"><%=Session["LanguageCode"] %></b>
      <form id="Form1" runat="server">
          <div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="first.aspx"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:25%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">支付激活会员</span>
            </div>
              </div>
        <div class="middle">
            <%--     <h2 class="regTabPrompt"></h2>--%>
            <div class="changeBox zcMsg" style="margin:20px 0px 40px 0px">
            <ul>
                <li style="height:auto">
                    <asp:HiddenField ID="hidorderid" Value="" runat="server" />
                    <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon  glyphicon-th-large green"></i></span>
                        <asp:Label ID="lblxjnumber" CssClass="form-control" runat="server" Text=""></asp:Label>
                       
                         
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
                        <asp:Label ID="lblname" CssClass="form-control" runat="server" Text=""></asp:Label>
                   
                </div>
                    </li>
                <li>
                 <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-phone"></i></span>
                           <asp:Label ID="lblphonenum"  CssClass="form-control" runat="server" Text=""></asp:Label>
                         </div>
                    </li> 
                <li>
                     <div class="input-group col-md-4">
                    <span style="float:left;">推荐：</span>
                        <asp:TextBox Enabled="false" style=" width:80%;" ID="txtDirect" placeholder="推荐编号" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>  
                         </div>
                    
                    <asp:HiddenField ID="HFTopNumber" runat="server" Value=""  />
                </li>
                 <li>
                        <asp:HiddenField ID="hiddirect" Value="" runat="server" />
                       <asp:HiddenField ID="hidDistrict"  Value="1" runat="server" />
                     <span style="float:left;">安置：</span>
                     <div class="input-group col-md-4">
                  
                    <asp:Button ID="jz" CssClass="btn btn-defaultg btn-lg" style="font-size:13px;width:35%;float:left;"  Text="自动寻位" OnClientClick="return quwei(1);"
                             runat="server"  />
                    <%--<asp:Button ID="jy" CssClass="btn btn-defaultg btn-lg" style="width:30%;float:left;margin-left:1%; font-size:13px;"  Text="极右滑落安置" OnClientClick="return quwei(2);"
                            runat="server"  />--%>

                    <asp:TextBox ID="txtplacemnet" placeholder="指定安置编号" onblur="shur();" CssClass="form-control" style="width:35%;float:left;margin-left:1%;  font-size:13px;" MaxLength="10"   onkeypress="return kpyzsz();" onkeyup="   szxs(this);" onafterpaste="szxs(this)"  OnTextChanged="txtplacemnet_TextChanged" runat="server"></asp:TextBox>
                         </div>
                    <asp:HiddenField ID="hidplacemnet"   Value="" runat="server" />
                </li>
            

                       <li>
                 <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock red"></i></span>
                     <asp:TextBox ID="txtpassword" placeholder="二级密码"  TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                         </div>
                    </li>

                 <li><div class="input-group col-md-4">
                     <p style="color:red; white-space:normal; height:90px; ">
                        重要提示：点击支付激活会员，系统将从您的可用石斛积分账户中扣除<asp:Label ID="lblkcjb" runat="server" Text="0.00"></asp:Label>石斛积分 + <asp:Label ID="lblttmoney" runat="server" Text="0.00"></asp:Label>注册积分                     </p>
                     </div>
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
          <%--  <input id="Checkbox1" type="checkbox">--%>  
        </div>
                    
                         <asp:Button ID="ImageButton1"  CssClass="btn btn-primary btn-lg" style="width:100%;"  Text="同意支付激活会员" OnClientClick="return submitchek();"
                            OnClick="Button1_Click" runat="server"  />
                    
        
       

            </div>
        	
         
            
           <%-- <div style="clear: both">
            </div>--%>
        
   </div>
              <!-- #include file = "comcode.html" -->
        <!--注册结束-->
            </div>
         
         
          

        <%--     <uc2:bottom runat="server" ID="bottom" />
         --%>    
  
   

        
  
   
    </form>
</body>
      
  <script type="text/javascript">
      //function alert(data) {
      //    var x = document.getElementById("p");
      //    x.innerHTML = data;
      //    $('#myModal').modal('show');

     // }

      function quwei(id) {
          var tuij = $("#txtDirect").val();
          $("#hiddirect").val(tuij);
          var direct = $("#hiddirect").val();
       
          var anz = AjaxClass.GetAtuosetPlace(direct).value;
          $("#hidDistrict").val(id);
 
          $("#hidplacemnet").val(anz);
          $("#txtplacemnet").val("");
         
          if (id == 1) {
              $("#jz").attr("class", "btn btn-success btn-log ");
              $("#jy").attr("class", "btn  btn-defaultg ");
          }
          if (id == 2) {
              $("#jy").attr("class", "btn btn-success btn-log   ");
              $("#jz").attr("class", "btn btn-defaultg");
          }
          return false;

      }
      function shur() {
          if ($("#jy").attr("class") != "btn btn-defaultg")
              $("#jy").attr("class", "btn btn-defaultg");
          if ($("#jz").attr("class") != "btn btn-defaultg")
              $("#jz").attr("class", "btn btn-defaultg");
          var san = $("#txtplacemnet").val();

          $("#hidplacemnet").val(san);
          // $("#txtplacemnet").attr("class", "from-control has-warning");
          // if(san)
      }

      function submitchek() {
          if (confirm("确定要激活此会员吗？")) {
            
              if ($("#hidplacemnet").val() == "") {
                 alert('请选择安置位置');
                 
                  return false;
              }
            if ($("#txtpassword").val() == "") {
                alert('请选输入二级密码');
                
                  return false;
            }
            return true;
          } else return false;
          
          
      }
     

</script>
       <%-- <script type="text/javascript" src="../MemberMobile/js/RegisterXY.js"></script>--%>

</html>
