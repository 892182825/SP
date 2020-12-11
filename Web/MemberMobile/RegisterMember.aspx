<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterMember.aspx.cs" Inherits="RegisterMember"
    EnableEventValidation="false" %>
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
    
    <%-- <script type="text/javascript" src="js/WdatePicker.js"></script>--%>

    <%--   <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>--%>
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
    <%--<script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script src="js/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>--%>
    <!-- jQuery -->
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
        .re_text
        { padding-top:8px;
        }
        .btn-qianse {
            background-color: #dcf3dc;
        }
    </style>

 

    <title>会员注册</title>
     <script type="text/javascript">
         var phoneCheck = /^1[0-9]{10}$/;
         $(function () {
             
             $("#txtName").blur(function () {
                 if ($("#txtName").val() == "") {
                     webToast("昵称不能为空", "middle", 3000);
                     document.getElementById("txtName").style.borderColor = "#b94a48";

                     return;
                 }
                 else {
                     document.getElementById("txtName").style.borderColor = "#468847";
                 }
             });
             

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


             //$("#SearchPlacement_DoubleLines1").blur(function () {
             //    if ($("#SearchPlacement_DoubleLines1").val() == "") {
             //        webToast("安置编号不能为空", "middle", 3000);
             //        document.getElementById("SearchPlacement_DoubleLines1").style.borderColor = "#b94a48";

             //        return;
             //    }
             //    else {
             //        document.getElementById("SearchPlacement_DoubleLines1").style.borderColor = "#468847";
             //    }
             //});
             
            
         });
      

         window.alert = alert;


     </script>

     <script language="javascript" type="text/javascript">
         function subform() {
             if ($("#txtName").val() == "") {
                 webToast("昵称不能为空", "middle", 3000);
                 return false;
             }

             if ($("#txtTele").val() == "") {
                 webToast("手机号不能为空", "middle", 3000);
                 return false;
             }
             if ($("#txtTele").val().length!= 11) {
                 webToast("手机号长度必须为11位", "middle", 3000);
                 return false;
             }
             if (!phoneCheck.test($("#txtTele").val())) {
                 webToast("手机号码输入有误，请重新输入", "middle", 3000);
                 document.getElementById("txtTele").style.borderColor = "#b94a48";
                 return false;
             }
             else {
                 document.getElementById("txtTele").style.borderColor = "#468847";
             }
             if ($("#CB:checked").length == 0) {
                 webToast("不同意注册协议则无法继续注册!", "middle", 3000);
                 return false;
             }
             if ($("#hidplacemnet").val() == "") {
                 webToast("请填写安置编号或点击自动寻位!", "middle", 3000);
                 return false;
             }
             var tuij = $("#txtDirect").val();
             $("#hiddirect").val(tuij);
             //if ($("#txtDirect").val() != $("#hiddirect").val())
             //{
             //    webToast("必须先更改推荐编号再自动寻位!", "middle", 3000);
             //    return false;
             //}

             return true;

         }

         $(function () {
             $('.sideBar').height('94%')
         })

         function quwei(id) {
             var tuij = $("#txtDirect").val();
             $("#hiddirect").val(tuij);
             var direct= $("#hiddirect").val();
             var anz = AjaxClass.GetAtuosetPlace(direct).value;
             $("#hidDistrict").val(id);
             $("#hidplacemnet").val(anz);
             $("#txtplacemnet").val("");
             if (id == 1) {
                 $("#jz").attr("class", "btn btn-success btn-log ");
                 $("#jy").attr("class", "btn  btn-qianse ");
             }
             if (id == 2){
                 $("#jy").attr("class", "btn btn-success btn-log   ");
                 $("#jz").attr("class", "btn btn-qianse");
             }
             return false;
            
         }
         function shur() {
             if ($("#jy").attr("class") != "btn btn-qianse")
                 $("#jy").attr("class", "btn btn-qianse");
             if ($("#jz").attr("class") != "btn btn-qianse")
                 $("#jz").attr("class", "btn btn-qianse");
             var san=$("#txtplacemnet").val();
            $("#hidplacemnet").val(san);
             // $("#txtplacemnet").attr("class", "from-control has-warning");
             // if(san)
         }
         function je(id)
         {
             $("#hidtzmoney").val(id);
             $("#tz300").attr("class", "btn btn-qianse btn-lg");
             $("#tz3000").attr("class", "btn btn-qianse btn-lg");
             $("#tz21000").attr("class", "btn btn-qianse btn-lg");
             if (id == $("#tz300").val()) $("#tz300").attr("class", "btn btn-success btn-lg"); 
             if (id == $("#tz3000").val()) $("#tz3000").attr("class", "btn btn-success btn-lg");
             if (id == $("#tz21000").val()) $("#tz21000").attr("class", "btn btn-success btn-lg");

             return false;
         }
     </script>
   

</head>
<body>
     <b id="lang" style="display: none;"><%=Session["LanguageCode"] %></b>
      <form id="Form1"   runat ="server">
       <div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="first.aspx"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:35%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">注册</span>
            </div>
              </div>
        <div class="middle">
            <%--     <h2 class="regTabPrompt"></h2>--%>
        
            <div class="changeBox zcMsg" style="margin:20px 0px 40px 0px">
            <ul>
                <li style="height:auto">
                   
                       <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon  glyphicon-th-large green"></i></span>
                        <asp:TextBox ID="txtNumber" CssClass="form-control"  MaxLength="50" ReadOnly="true" runat="server" style="border:0;color:red;font-size:16px;"></asp:TextBox>
                        <asp:HiddenField ID="HFNumber" runat="server" />
                         
                     </div>
                    </li>
                <li>   
                     <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user red"></i></span>
                         <asp:TextBox ID="txtName" CssClass="form-control" onkeyup="ValidateValue(this)" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" placeholder="昵称" MaxLength="10" runat="server" meta:resourcekey="txtNameResource1"></asp:TextBox>
                   
                </div>
                    </li>

               <%-- <li>
                     <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user blue"></i></span>
                        <asp:TextBox ID="txtPetname" MaxLength="50" placeholder="昵称" CssClass="form-control" runat="server" meta:resourcekey="txtPetnameResource1"></asp:TextBox>
                      </div>
                    <span id="info_txtPetname" style="display: none;" class="re_pro"></span>

                </li>--%>

                <%--<li >
                     <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user blue"></i></span>
                        <asp:TextBox ID="txtStore" CssClass="form-control" placeholder="所属服务机构" MaxLength="10" runat="server" meta:resourcekey="txtStoreResource1"></asp:TextBox>
                    </div><span id="info_txtStore" style="display: none;" class="re_pro"></span>
                </li>--%>
               <%-- <li>
                   <div class="changeLt">
                    <%=GetTran("000000","证件类型")%>：</div> 
                   <div class="changeRt">
                        <asp:DropDownList CssClass="zcSltBox" onchange="card();" ID="dplCardType" runat="server" meta:resourcekey="dplCardTypeResource1">
                        </asp:DropDownList>
                    </div></li>
                <li id="liZJHM">
                    <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user blue"></i></span>
                        <asp:TextBox ID="txtPapernumber" placeholder="证件号码" CssClass="textBox" runat="server" MaxLength="18" meta:resourcekey="txtPapernumberResource1"></asp:TextBox>
                    </div><span id="info_txtPapernumber" style="display: none;" class="re_pro"></span>
                </li>
                <li id="tr1">
                    <div class="changeLt">
                       <b>*</b>
                    <%=GetTran("000103", "会员性别")%>：</div>
                     <div class="changeRt">
                        <asp:RadioButtonList ID="RadioBtnSex" runat="server" Width="120px" RepeatLayout="Flow"
                            RepeatDirection="Horizontal" meta:resourcekey="RadioBtnSexResource1">
                            <asp:ListItem Value="1" Selected="true" meta:resourcekey="ListItemResource1">男</asp:ListItem>
                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource2">女</asp:ListItem>
                        </asp:RadioButtonList>
                    </div></li>
                <li id="tr2">
                   <div class="changeLt">
                       <b>*</b>
                    <%=GetTran("000105", "出生日期")%>：</div>
                     <div class="changeRt">
                        <asp:TextBox ID="txtBirthDate" onchange="checkDate(this);" onfocus="WdatePicker({maxDate:'#{%y-18}-%M-%d'});"
                            runat="server" CssClass="textBox" meta:resourcekey="txtBirthDateResource1"></asp:TextBox>
                    </div><span id="birthday_error" style="display: none;" class="re_pro"></span>
                </li>--%>
                <li>
                   <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-phone blue"></i></span>
                        <asp:TextBox ID="txtTele" CssClass="form-control" onkeypress="return kpyzsz();" onkeyup="sz(this)" onafterpaste="sz(this)" placeholder="手机号码" runat="server" MaxLength="11" meta:resourcekey="txtTeleResource1"></asp:TextBox>
                         </div>
                    <span id="info_txtTele" style="display: none;" class="re_pro"></span></li>
               <%-- <li>
                   <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user blue"></i></span>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="电子邮件" runat="server" MaxLength="30" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                    </div><span id="info_txtEmail" style="display: none;" class="re_pro"></span>
                </li>--%>
                <li style="height:auto">
                   <div class="changeLt">
                       <b>*</b>
                    <%=GetTran("003177","收货地址")%>：</div>
                     <div class="changeRt"  >
                        <uc1:CountryCityPCode ID="CountryCity2"  style="width:80%;" runat="server" />
                    </div></li>
                <li>
                     <div class="input-group col-md-4">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user blue"></i></span>
                        <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="详细地址" runat="server" MaxLength="150" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                         </div></li>
                <li>
                     <div class="input-group col-md-4">
                    <span style="float:left;">推荐：</span>
                        <asp:TextBox style=" width:80%;" ID="txtDirect" placeholder="推荐编号" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>  
                         </div>
                    <span id="info_txtDirect" style="display: none;" class="re_pro"></span>
                    <asp:HiddenField ID="HFTopNumber" runat="server" Value=""  />
                </li>

                <li>
                    
                 
                   
                    <%--<span id="info_txtPlacement" style="display: none;" class="re_pro"></span>--%>
                     
                </li>

                <%--<li>    <div class="changeLt">&nbsp;</div>      <div class="changeRt">
                       <input  type="button" style="float:left; width:80%; line-height:35px; height:35px;  padding:0 5px;text-align:center; background-color:#96c742; " onclick="AutoSet();"  value="<%=GetTran("009130","自动安置")%>"  />
                    </div>
                </li>--%>
                <input type="text"  style="display:none;" id="sraz"/>
                <li style="display:none;"><span class="re_text"><span style="color: Red"></span>区位：</span>
                    <span>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" meta:resourcekey="RadioButtonList1Resource1">
                            <asp:ListItem Selected="True" Value="1">极左</asp:ListItem>
                            <asp:ListItem Value="2">极右</asp:ListItem>
                        </asp:RadioButtonList>
                    </span></li>
                <li>
                        <asp:HiddenField ID="hiddirect" Value="" runat="server" />
                       <asp:HiddenField ID="hidDistrict"  Value="1" runat="server" />
                    <span style="float:left;">安置：</span>
                    <asp:Button ID="jz" CssClass="btn btn-qianse btn-lg" style="font-size:13px;width:35%;float:left;"  Text="自动寻位" OnClientClick="return quwei(1);"
                             runat="server"  />
                   <%-- <asp:Button ID="jy" CssClass="btn btn-qianse btn-lg" style="width:30%;float:left;margin-left:2%; font-size:13px;"  Text="极右滑落安置" OnClientClick="return quwei(2);"
                            runat="server"  />--%>

                    <asp:TextBox ID="txtplacemnet" placeholder="指定安置编号" CssClass="form-control" onblur="shur()" onkeyup="sz(this)" onafterpaste="sz(this)" style="width:35%;float:left;margin-left:2%;  font-size:13px;" MaxLength="10" OnTextChanged="txtplacemnet_TextChanged" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hidplacemnet" Value="" runat="server" />
                </li>
            
                <li style=" margin-top: 30px;">
                    <span class="re_text" style="float:left;">
                       金额：</span> 
                    <asp:HiddenField ID="hidtzmoney" Value="300" runat="server" />
                    <asp:Button ID="tz300" CssClass="btn btn-success btn-lg" style="width:26%;float:left; font-size:14px;"  Text="300" OnClientClick="return je(this.value);"
                             runat="server"  />
                    <asp:Button ID="tz3000" CssClass="btn btn-qianse btn-lg" style="width:26%;float:left;margin-left: 1%; font-size:14px;"  Text="3000" OnClientClick="return je(this.value);"
                            runat="server"  />
                    <asp:Button ID="tz21000" CssClass="btn btn-qianse btn-lg" style="width:26%;float:left;margin-left: 1%; font-size:14px;"  Text="21000" OnClientClick="return je(this.value);"
                            runat="server"  />

                </li>
                <li>
                    <span class="re_text"><span style="color: Red"></span>
                       提货方式：</span> 
                    <span>
                    <asp:RadioButtonList ID="rbltotaltype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" >
                        <asp:ListItem Value="0" Selected="true">邮寄&nbsp;</asp:ListItem>
                        
                    </asp:RadioButtonList>
                    </span>
                </li>

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
         <div  class="" ></div>
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
                $("#myModall").modal({ backdrop: 'static', keyboard: false });
                $('#myModall').modal('show');
                
            }
</script>
         
        <%--     <uc2:bottom runat="server" ID="bottom" />
         --%>    
   <!-- #include file = "comcode.html" -->

<script>
    $(function () {



        $('.p_menu2 h5').click(function () {
            $(this).siblings('ul').stop().slideToggle()
            if ($(this).siblings('ul').height() > 1) {
                $(this).children('div').children('span').html('展开').siblings('em').css({ 'background': 'url(img/zhank.png) no-repeat center center', 'backgroundSize': '17px' })
            } else {

                $(this).children('div').children('span').html('收起').siblings('em').css({ 'background': 'url(img/shouq.png) no-repeat center center', 'backgroundSize': '15px' })
            }
        })

        $('.p_menu2 ul li a').height('115px')
         


    });

  



</script>
     <script type="text/javascript" src="../MemberMobile/js/RegisterXY.js"></script>

    
    </form>
</body>
      
  
      

    
</html>
