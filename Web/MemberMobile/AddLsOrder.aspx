<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddLsOrder.aspx.cs" Inherits="Member_AddLsOrder" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>



<%@ Register Src="~/UserControl/STop.ascx" TagPrefix="uc1" TagName="STop" %>
<%@ Register Src="~/UserControl/SLeft.ascx" TagPrefix="uc1" TagName="SLeft" %>

<%@ Register Src="../UserControl/CountryCityPCode1.ascx" TagName="CountryCityPCode" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
      <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<script src="js/jquery-1.7.1.min.js"></script>
<title><%=GetTran("009071","报单确定") %></title>
<link rel="stylesheet" href="css/style.css">

    <link href="../member/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../member/css/submit.css" id="cssid" rel="stylesheet" type="text/css" />
    <script src="../member/js/jquery-1.4.3.min.js" type="text/javascript"></script>

        <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />
    <%--<script src="../JS/slide.js" type="text/javascript"></script>--%>
  
<style>

        /*.xs_footer li{float:left;width:25%;text-align:center;}
        .xs_footer li a{display:block;padding-top:32px;background:url(img/shouy1.png) no-repeat center 4px;background-size:28px;}*/
        

    .bdqd_in li #lblMemBh
    {
        text-align:left
    }
     .bdqd_in li input[type='text']
    {
        text-align:left;
        width:65%;
        float:left
    }
    .bdqd_in li
    {
        padding:5px 0;
    }
        .bdqd_in li select
        {
            width:50%;
            float:left
        }
    body form
    {
        font-size:14px;
       
    }
    body
    {
        height:auto
    }
    .bdqd_in li span
    {
        margin-right:3%
    }

    .changeBox2 h3 span, .bdqd_in h3 span {
    float: left;
    background: #dd4814;
    width: 3px;
    height: 16px;
    margin: 13px 5px 0 10px;
}
    #p_content2 h3 {
    border-bottom: 1px solid #ccc;
    height: 30px;
    line-height: 30px;
    font-size: 15px;
}
</style>

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            var logintype = '<%=Session["UserType"].ToString() %>';
            if (logintype == "1") {
                $("#cssid").attr("href", "../member/css/submit-co.css")
            } else if (logintype == "2") {
                $("#cssid").attr("href", "../member/css/submit-use.css")
            } else {
                $("#cssid").attr("href", "../member/css/submit.css")
            }
        });

        function GetCCode_s2(city) {
            var sobj = document.getElementById("<%=txtPostCode.ClientID %>");
            sobj.value = AjaxClass.GetAddressCode(city).value
        }
        function isTel(txtStr) {
            var validSTR = "1234567890-#*";

            for (var i = 1; i < txtStr.length + 1; i++) {
                if (validSTR.indexOf(txtStr.substring(i - 1, i)) == -1) {
                    return false;
                }
            }
            return true;
        }

        $("#CountryCity2_ddlP").change(function() {

            $("#txtYunfei").text(AjaxMemShopCart.GetCarryMoney('<%= returnTotalMoney() %>').value);

            //运费
            $("#ltYunfei").html(AjaxMemShopCart.GetCarryMoney('<%= returnTotalMoney() %>').value);

            //应付总金额
            $("#ltPayMoney").text((Number($("#ltYunfei").html()) + Number($("#ltPrice").html())).toFixed(2));
        });

        function checkAddress() {
            if ($("#rbtAddress input[type=radio]:checked").val() == "新地址") {
                $("#panel2").css("display", "");
            }
            else {
                $("#panel2").css("display", "none");
                var arr = new Array();
                arr = $(this).val().split(' ');

                $("#txtYunfei").text(AjaxMemShopCart.GetCarryMoney('<%= returnTotalMoney() %>').value);

                //运费
                $("#ltYunfei").html(AjaxMemShopCart.GetCarryMoney('<%= returnTotalMoney() %>').value);

                //应付总金额
                $("#ltPayMoney").text((Number($("#ltYunfei").html()) + Number($("#ltPrice").html())).toFixed(2));
            }
        }


        $("#ddth").change(function() {
            if ($("#ddth").val() == 1) {
                //运费
                $("#ltYunfei").html(Number(0.00).toFixed(2));
                $("#txtYunfei").html(Number(0.00).toFixed(2));

                //应付总金额
                $("#ltPayMoney").text((Number($("#ltYunfei").html()) + Number($("#ltPrice").html())).toFixed(2));

                document.getElementById("shouhuodizhi").style.display = "none";
                document.getElementById("fahuofangshi").style.display = "none";
            }
            else {

                var arr = new Array();
                arr = $("#rbtAddress input[type=radio]:checked").val().split(' ');

                $("#txtYunfei").text(AjaxMemShopCart.GetCarryMoney(<%= returnTotalMoney() %>).value);
                
                //运费
                $("#ltYunfei").html(AjaxMemShopCart.GetCarryMoney(<%= returnTotalMoney() %>).value);

                //应付总金额
                $("#ltPayMoney").text((Number($("#ltYunfei").html()) + Number($("#ltPrice").html())).toFixed(2));

                document.getElementById("shouhuodizhi").style.display = "";
                document.getElementById("fahuofangshi").style.display = "";
            }
        });
	    
        /*发货方式改变，其新地址要改变*/
        $("#DDLSendType").change(function() {
            var sendtypeV = $("#DDLSendType").val();
            if (sendtypeV == 0) {
                $("#shouhuodizhi").attr("style", "display:none;");
            }
            else {
                $("#shouhuodizhi").attr("style", "display:block;");
            }
        });
	    
	    
        // 赋予表格样式
        var strFlag=true;
        var reem=/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        var renum=/\D+/;
        var postReg=/^[0-9][0-9]{5}$/;
        var tdcolor = $("#cart_table").find("tr");
        for (var i = 0; i < tdcolor.length; i++) {
            tdcolor[i].className = (i % 2) ? "" : "alt";
        }

        function checkNumber(obj) {
            if ($(obj).val() == "") {
                alert("会员编号不能为空！");
                return false;
            } else {

            }
        }

        function checkdate() {
            if ($("#AgainTime").val() == "1") {
                alert("已提交，不能重复操作！");
                return false;
            }
            if ($("#txtMemBh").val() == "") {
                alert("会员编号不能为空！");
                return false;
            }
            if ($("#txtConName").val() == "") {
                alert("收货姓名不能为空！");
                return false;

            }

            if ($("#txtOtherPhone").val() == "") {
                alert("收货电话不能为空。");
                return false;
            }
            if (renum.test($("#txtOtherPhone").val()) || $("#txtOtherPhone").val().length != 11) {
                alert("收货电话格式错误。");
                return false;
            }

            if ($("#CountryCity2_ddlCountry").val() == "请选择" || $("#CountryCity2_ddlP").val() == "请选择" || $("#CountryCity2_ddlCiTy").val() == "请选择" || $("#CountryCity2_ddlX").val() == "请选择") {
                alert("请选择收货地址！");
                return false;
            }
            if ($("#Txtdz").val() == "") {
                alert("详细地址不能为空！");
                return false;
            }
            return true;
            $("#AgainTime").val("1");
        }
    </script>
    <script type="text/javascript">
        var btnValue;
        function BtnTijiao(obj) {
            btnValue = obj;
            document.getElementById("p_content").style.display = "none";
            document.getElementById("p_content2").style.display = "";

            document.getElementById("lab_confirm_number").innerHTML = document.getElementById("lblMemBh").innerHTML;
            document.getElementById("lab_confirm_name").innerHTML = document.getElementById("labName").innerHTML;
            document.getElementById("lab_confirm_tel").innerHTML = document.getElementById("labyddh").innerHTML;

            //document.getElementById("lab_confirm_addr").innerHTML='<%=getShouHuoAddr() %>';

        }
        function BtnQueren() {
            var v = true;
            if ($("#txtMemBh").val() == "") {
                alert("会员编号不能为空！");
                v = false;
                return;
            }
            if ($("#txtOtherPhone").val() == "") {
                alert("收货电话不能为空。");
                v = false;
                return;
            }
            if (renum.test($("#txtOtherPhone").val()) || $("#txtOtherPhone").val().length != 11) {
                alert("收货电话格式错误。");
                v = false;
                return;
            }
            if (v == true) {
                document.getElementById("p_content").style.display = "none";
                document.getElementById("p_content2").style.display = "";


                var sendtypeV = $("#ddth").val();

                document.getElementById("lab_confirm_number").innerHTML = document.getElementById("txtMemBh").value;
                document.getElementById("lab_confirm_name").innerHTML = $("#txtConName").val();
                document.getElementById("lab_confirm_tel").innerHTML = $("#txtOtherPhone").val();
                document.getElementById("lbl_Name").innerHTML = $("#labName").text();

                var country = document.getElementById("CountryCity2_ddlCountry").value;
                var province = document.getElementById("CountryCity2_ddlP").value;
                var city = document.getElementById("CountryCity2_ddlCity").value;
                var xian = document.getElementById("CountryCity2_ddlX").value;
                var xx = document.getElementById("Txtdz").value;
                document.getElementById("lab_confirm_addr").innerHTML = country + province + city + xian + xx;

            }
        }
        function Cancel() {
            document.getElementById("p_content").style.display = "";
            document.getElementById("p_content2").style.display = "none";
            return false;
        }
    </script>

    <script type="text/javascript" language="javascript">
        <!--
    function setTab(name, cursel, n, clsname) {
        for (i = 1; i <= n; i++) {
            var menu = document.getElementById(name + i);
            var con = document.getElementById("con_" + name + "_" + i);
            menu.className = i == cursel ? clsname : "";
            con.style.display = i == cursel ? "block" : "none";
        }
    }
    //-->
    </script>
     <script type="text/javascript">
       
         //选择不同语言是将要改的样式放到此处

         $(function () {
             var lang = $("#lang").text();
             
             if (lang != "L001") {
                 $('.bdqd_in ul li span').css({ 'width': '60%', 'text-align': 'left' })
                 //alert(11111);
                 //$('.changeBox ul li .changeRt').css({ 'width': '100%', 'margin-left': '10%' })
                 //$('.changeBox ul li ').css('height', 'auto')
                // $('element.style').css('width',''); 
             }

         })
      
    </script>
</head>
<body>
     <b id="lang" style="display: none;"><%=Session["LanguageCode"] %></b>
    <form id="form1" runat="server">
        <%-- <div class="t_top">	
        <a class="backIcon" href="javascript:history.go(-1)"></a>
          <%=GetTran("009071","报单确定") %>
    </div>--%>
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 2px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 35%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">报单确定</span>
            </div>
        </div>

        <div id="p_content" runat="server">
          <div class="bdqd_in">	
    	<h3><span></span>  <%=GetTran("007409","订货人信息") %></h3>
        <ul>
        	<li><span><%=GetTran("000106", "订单类型")%>：</span> <asp:Label ID="lblOdType" runat="server" Text="" style="text-align:left;"></asp:Label></li>
        	<li><span><%=GetTran("000024","会员编号")%>：</span> 
                  <asp:Label ID="lblMemBh" runat="server" Text="" style="text-align:left;"></asp:Label>
                   <asp:TextBox ID="txtMemBh" onblur="return checkNumber(this);"
                               runat="server"  AutoPostBack="true" OnTextChanged="txtMemBh_TextChanged"></asp:TextBox>     
        	</li>
        	<li><span><%=GetTran("007410","订货人姓名")%>：</span><asp:Label ID="labName" runat="server" Text="" style="text-align:left;"></asp:Label></li>
        	<li><span><%=GetTran("000069","移动电话")%>：</span><asp:Label ID="labyddh" runat="server" Text="" style="text-align:left;"></asp:Label></li>
        </ul>
    </div>

          <div class="bdqd_in bdqd_in2">	
    	<h3><span></span><%=GetTran("007412","收货人信息")%></h3>
        <ul>
        	<li>	
            	<span><%=GetTran("007413","收货选择")%>：</span>
                <div class="dzxz">
                	<div>
                      <asp:Panel ID="panel1" runat="server">
                                            <asp:RadioButtonList ID="rbtAddress" onclick="checkAddress();" runat="server" 
                                                AutoPostBack="true" RepeatLayout="Flow"
                                                OnSelectedIndexChanged="rbtAddress_SelectedIndexChanged" style="width:100%;text-align:left;overflow:hidden">

                                            </asp:RadioButtonList>
                                        </asp:Panel>
                       
                                        <asp:Panel ID="panel3" runat="server">

                                            <table id="panel2" cellpadding="0" cellspacing="0" style="display: none;" runat="server">
                                            </table>
                                        </asp:Panel>
                        
                    </div>
                </div>	
            </li>
            <li>
                <span><%=GetTran("000383","收货人姓名")%>：</span>
                 <asp:TextBox ID="txtConName"  runat="server" MaxLength="20"></asp:TextBox>
            </li>
               <li>
                <span><%=GetTran("000403","收货人电话")%>：</span>
                    <asp:TextBox ID="txtOtherPhone"  runat="server" MaxLength="11"></asp:TextBox>
            </li>
             <li>
                <span style="float:left;"><%=GetTran("000112","收货地址")%>：</span>
                  <div style="float:left; width:65%;"><uc3:CountryCityPCode  ID="CountryCity2" runat="server" /></div>
            </li>
        <li>
            <span><%=GetTran("000920","详细地址")%>：</span>
             <asp:TextBox ID="Txtdz" runat="server"  MaxLength="120" ></asp:TextBox>
          </li>
        	<li>	
            	<span><%=GetTran("000526","运货方式")%>：</span>
                <div class="dzxz">
                	<div>
                         <asp:RadioButtonList ID="ddth" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                <asp:ListItem Value="2" Selected="True">邮寄</asp:ListItem>
                                <asp:ListItem Value="1">自提</asp:ListItem>
                         </asp:RadioButtonList>
                    </div>
                </div>	
            </li>
        	<li>	
            	<span><%=GetTran("007416","收货途径")%>：</span>
                <div class="dzxz">
                	<div>
                        <asp:RadioButtonList ID="DDLSendType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                            <asp:ListItem Value="0" >服务机构收货</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">会员收货</asp:ListItem>
                                        </asp:RadioButtonList>
                    </div>
                </div>	
            </li>
        	<li>	
            	<span><%=GetTran("007417", "快递费用")%>：</span>
                 <asp:Label ID="txtYunfei" runat="server" Text="0.00" style="text-align:left"></asp:Label>
            </li>
            <li id="tr1" style="display: none;">	
            	<span><%=GetTran("000114","邮政编码")%>：</span>
                   <asp:TextBox ID="txtPostCode"  runat="server"></asp:TextBox>
                   <asp:Label ID="labPostCode" runat="server" Text="" Style="font-size: 18px; font-weight: bold"></asp:Label>
            </li>
        </ul>
    </div>

           <div class="bdqd_in bdqd_in2" style="margin-bottom:100px;">	
    	<h3><span></span><%=GetTran("000560","产品信息")%></h3>
                 <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
           <ul>
        	<li>	
            	<span>  <%=GetTran("000558","产品编号")%>：</span>	
           		   <%# DataBinder.Eval(Container.DataItem, "ProductCode")%>
            </li>
        	<li><span><%=GetTran("000501", "产品名称")%>：</span> <%# DataBinder.Eval(Container.DataItem,"ProductName") %></li>
        	<li><span><%=GetTran("000503","单价")%>：</span> <%# (Convert.ToDouble( DataBinder.Eval(Container.DataItem, "PreferentialPrice"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2") %></li>
        	<%--<li><span>PV：</span><%# DataBinder.Eval(Container.DataItem, "PreferentialPV")%></li>--%>
        	<li><span><%=GetTran("000505","数量")%>：</span><%# DataBinder.Eval(Container.DataItem, "proNum")%></li>
        	<li><span><%=GetTran("000041","总金额")%>：</span><%#  (Convert.ToDouble(  DataBinder.Eval(Container.DataItem, "TotalPrice"))*i
                        ).ToString("f2") %></li>
        	<%--<li><span><%=GetTran("007324","总")%>PV：</span><%# DataBinder.Eval(Container.DataItem, "TotalPv")%></li>--%>
        </ul>
                                             </ItemTemplate>
                                </asp:Repeater>

        <ul>
           <li><span><%=GetTran("000120","运费") %>：</span><asp:Label ID="ltYunfei" runat="server" Text="0.00" style="text-align:left"></asp:Label></li>
        	<%--<li><span><%=GetTran("000630","合计") %>：</span></li>--%>
        	<li><span><%=GetTran("000505","数量")%>：</span><asp:Label ID="labNum" runat="server" Text="" style="text-align:left"></asp:Label> </li>
        	<li><span><%=GetTran("000041","总金额")%>：</span><asp:Label ID="ltPayMoney" runat="server" Text="0.00" style="text-align:left"></asp:Label></li>
        	<%--<li><span><%=GetTran("007324","总")%>PV：</span><asp:Literal ID="ltPv" runat="server" ></asp:Literal></span></li>--%>
        </ul>
    </div>

    <div class="pay_qd" style="position:fixed; bottom:50px; border:1px solid #ccc; width:100%; " >
        <input id="back" style="width: 50%; float: left; background: #aea79f; border: 0;height:100%;"
    type="submit" onclick="location.href='ShoppingCartView.aspx?type=<%=Request["type"] %>';return false;" value='<%=GetTran("004075","上一步") %>'/> 
    
        <input style=" width: 50%; float: left; background: #dd4814;color: #fff; border: 0;height:100%;"
        type="button" onclick="BtnQueren();return false;" value='<%=GetTran("000419","确认提交") %>' />

   </div>

            </div>

       
          <div class="bdqd_in" id="p_content2" runat="server">	
    	<h3><span></span><%=GetTran("007420","请确认您的收货信息")%></h3>
        <ul>
        	<li><span style="display:block;white-space:nowrap"><%=GetTran("007421","订货人编号")%>：</span><asp:Label ID="lab_confirm_number" runat="server" Text="" style="text-align:left"></asp:Label></li>
        	<li><span style="display:block;white-space:nowrap"><%=GetTran("007410","订货人姓名")%>：</span><asp:Label ID="lbl_Name" runat="server" Text="" style="text-align:left"></asp:Label></li>
        	<li><span style="display:block;white-space:nowrap"><%=GetTran("000383","收货人姓名")%>：</span><asp:Label ID="lab_confirm_name" runat="server" Text="" style="text-align:left"></asp:Label></li>
        	<li><span style="display:block;white-space:nowrap"><%=GetTran("000403","收货人电话")%>：</span><asp:Label ID="lab_confirm_tel" runat="server" Text="" style="text-align:left"></asp:Label></li>
        	<li><span style="display:block;white-space:nowrap"><%=GetTran("000393", "收货人地址")%>：</span><asp:Label ID="lab_confirm_addr" runat="server" Text="" style="text-align:left"></asp:Label></li>
        </ul>

        <div class="pay_qd" style="position:absolute;bottom:53px;left:0;width:100%">
            <input name="" style="width: 50%; float: left;background: #fff; border: 0;height:100%;"  type="submit"  onclick="return Cancel();" value='<%=GetTran("004075","上一步") %>' />
    	    <asp:Button style=" width: 50%; float: left;background: #77c225;color: #fff; border: 0;height:100%;" ID="Button1" runat="server" OnClientClick="return checkdate();" OnClick="Button1_Click" />
            <%-- 判断重复提交--%>
            <asp:HiddenField ID="AgainTime" Value="0" runat="server" />
        </div>
    </div>
	

     <!-- #include file = "comcode.html" -->

        <div style="display:none">
        <uc1:STop runat="server" ID="STop1" />
        <uc1:SLeft runat="server" ID="SLeft1" />
        <uc1:top runat="server" ID="top" />
            </div>
        <div class="rightArea clearfix" style="display:none">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="MemberPage">
                <%--<uc1:top runat="server" ID="top" />--%>
                <div id="p_content4" runat="server">
                    <div class="submitCent" style="margin-left:200px">
                        <div class="subOrder">
                            <h1 class="subTitle"><%=GetTran("007409", "订货人信息")%>：</h1>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <th><%=GetTran("000106", "订单类型")%>：</th>
                                    <td>
                                       

                                    </td>
                                </tr>
                                <tr>
                                    <th><%=GetTran("000024","会员编号")%>：</th>
                                    <td>

                                    
                                    </td>

                                </tr>
                                <tr>
                                    <th><%=GetTran("007410","订货人姓名")%>：</th>
                                    <td><span>
                                        <asp:TextBox ID="txtName" CssClass="ctConPgFor" runat="server"></asp:TextBox>
                                        
                                    </span>
                                    </td>
                                </tr>
                                <tr>
                                    <th><%=GetTran("000069","移动电话")%>：</th>
                                    <td>
                                        <span>
                                            <asp:TextBox ID="Txtyddh" CssClass="ctConPgFor" runat="server" MaxLength="11"></asp:TextBox>
                                            
                                        </span>
                                    </td>
                                </tr>
                            </table>
                            <h1 class="subTitle"><%=GetTran("007412","收货人信息")%>：</h1>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <th class="subOdrAddr" rowspan="1"><%=GetTran("007413","收货选择")%>：</th>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <th><%=GetTran("000383","收货人姓名")%>：</th>
                                    <td>
                                        <span>
                                           
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <th><%=GetTran("000403","收货人电话")%>：</th>
                                    <td>
                                        <span>
                                           
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <th><%=GetTran("000112","收货地址")%>：</th>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <th><%=GetTran("000920","详细地址")%>：</th>
                                    <td>
                                       
                                    </td>
                                </tr>

                                <tr>
                                    <th><%=GetTran("000526","运货方式")%>：</th>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <th><%=GetTran("007416","收货途径")%>：</th>
                                    <td>
                                        <%--<asp:DropDownList ID="DDLSendType" runat="server" Width="225px" CssClass="ctConPgFor">
                        <asp:ListItem Value="0" Selected="True">公司发货到服务机构</asp:ListItem>
                        <asp:ListItem Value="1">公司直接发给会员</asp:ListItem>
                    </asp:DropDownList>--%>
                                       
                                    </td>
                                </tr>

                                <tr>
                                    <th><%=GetTran("007417", "快递费用")%>：</th>
                                    <td><span>
                                      
                                    </span></td>
                                </tr>
                                <tr>
                                    <th><%=GetTran("000114","邮政编码")%>：</th>
                                    <td>
                                      
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="subDis">
                            <h1 class="subTitle"><%=GetTran("000560","产品信息")%>：</h1>
                            <table width="98%" border="0" cellspacing="1" cellpadding="0">
                                <tr class="subDsTabTr">
                                    <th>
                                        <%=GetTran("000558","产品编号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000501", "产品名称")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000503","单价")%>
                                    </th>
                                    <th>PV
                                    </th>
                                    <th>
                                        <%=GetTran("000505","数量")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000041","总金额")%>
                                    </th>
                                    <th>
                                        <%=GetTran("007324","总")%>PV
                                    </th>
                                </tr>
                              <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span>
                                                  
                                                </span>
                                            </td>
                                            <td>
                                                <span>
                                                   </span>
                                            </td>
                                            <td>
                                                <span>
                                                   </span>
                                            </td>
                                            <td>
                                                <span>
                                                    </span>
                                            </td>
                                            <td>
                                                <span>
                                                    </span>
                                            </td>
                                            <td>
                                                <span>
                                                    </span>
                                            </td>
                                            <td>
                                                <span>
                                                    </span>
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr class="alt" style="display: none;">
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <%=GetTran("000630","合计")%>：
                                    </td>
                                    <td>
                                        <span>
                                            <asp:Literal ID="ltNum" runat="server"></asp:Literal>
                                          </span>
                                    </td>
                                    <td>
                                        <span>
                                            <asp:Label ID="ltPrice" runat="server" Text="0.00"></asp:Label>
                                        </span>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <%=GetTran("000120","运费")%>：
                                    </td>
                                    &nbsp;
                    <td></td>
                                    <td>
                                        <span>
                                            </span>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <%=GetTran("000630","合计")%>：
                                    </td>
                                    <td>
                                        <span>
                                            </span>
                                    </td>
                                    <td>
                                        <span>

                                            </span>
                                    </td>
                                    <td>
                                        <span>
                                            
                                    </td>
                                </tr>
                            </table>
                        </div>
                <%--        <div class="subProduct" style="margin-right: 1124px;margin-left:0px">
                            <ul>
                                <li>
                                    <div class="btn">
                                        <div class="btnLeft"></div>
                                        
                                        <div class="btnRight"></div>
                                    </div>
                                </li>
                                <li>
                                    <div class="btn2" id="ff">
                                        <div class="btnLeft2"></div>
                      
                                        <div class="btnRight2"></div>
                                    </div>
                                </li>
                            </ul>
                        </div>--%>

                        <!--信息结束-->
                        <div style="clear: both"></div>
                    </div>
                </div>

                <div id="p_content3" style="height: 500px;" runat="server">
                    <br />
                    <h3 style="font-size: 18px; margin-left: 30px; color: Red;"><%=GetTran("007420","请确认您的收货信息")%>：</h3>
                    <br />
                    <h3 style="font-size: 16px; margin-left: 200px;"><%=GetTran("007421","订货人编号")%>：</h3>
                    <br />
                    <h3 style="font-size: 16px; margin-left: 200px;"><%=GetTran("007410","订货人姓名")%>：</h3>
                    <br />
                    <h3 style="font-size: 16px; margin-left: 200px;"><%=GetTran("000383","收货人姓名")%>：</h3>
                    <br />
                    <h3 style="font-size: 16px; margin-left: 200px;"><%=GetTran("007423","收货人联系电话")%>：</h3>
                    <br />
                    <h3 style="font-size: 16px; margin-left: 200px;"><%=GetTran("000393", "收货人地址")%>：</h3>
                    <br />
                    <ul style="padding-left: 200px;">
                        <li style="float: left; margin-right: 50px;">
                            <div class="btn">
                                <div class="btnLeft"></div>
                             
                                <div class="btnRight"></div>
                            </div>
                        </li>
                        <li style="float: left;">
                            <div class="btn2">
                                <div class="btnLeft2"></div>
                             
                                <div class="btnRight2"></div>
                            </div>
                        </li>
                    </ul>
                   
                </div>
                <div style="display: none;" class="te_poplook" id="DivCarPop">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr class="te_top">
                                <td class="te_left te_corner"></td>
                                <td class="te_middle te_rib"></td>
                                <td class="te_right te_corner"></td>
                            </tr>
                            <tr class="te_middle">
                                <td class="te_left te_rib"></td>
                                <td class="te_middle">
                                    <div id="cart-pop">
                                        <div id="pop-heading">
                                            <div class="title">
                                                <h2 style="padding-left: 15px;">
                                                    <asp:Literal ID="ltError" runat="server"></asp:Literal>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 10px 40px; background-color: #FFF; width: 120px;" class="pop-content">
                                        <p id="msgValue" class="error">
                                            <asp:Literal ID="ltMess" runat="server"></asp:Literal>
                                        </p>
                                    </div>
                                    <div style="background-color: #F0F0F0; height: 30px; width: auto; padding: 5px; text-align: center;">
                                        <div style="float: right; margin: 0px auto;">
                                            <div class="cssbutton sample25 blue" style="height: 25px;">
                                                <input type="button" value='<%=GetTran("000064","确认") %>' id="btnClose">
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="te_right te_rib"></td>
                            </tr>
                            <tr class="te_bottom">
                                <td class="te_left te_corner"></td>
                                <td class="te_middle te_rib"></td>
                                <td class="te_right te_corner"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
               
            </div>
        </div>
         <uc2:bottom runat="server" ID="bottom" />
    </form>

    <script type="text/jscript">
        $(function () {
            $('label').css({ 'lineHeight': '20px', 'fontSize': '13px','padding':'5px 0','float':'left','width':'94%' });
            $('input[type="radio"]').css('float', 'left');
            $('#rbtAddress input').css({'width':'6%','marginTop':'2px'})
            $('#ddth label').css({ 'lineHeight': '20px', 'margin': '0 5px','float': 'left','width': 'auto' });
            $('#ddth input[type="radio"]').css({'float': 'left'});



            $('#DDLSendType label').css({ 'lineHeight': '20px', 'margin': '0 5px','float': 'left','width': 'auto' });
            $('#DDLSendType input[type="radio"]').css({'float': 'left'});

       
        })
    </script>
</body>
</html>
