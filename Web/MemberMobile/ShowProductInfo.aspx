<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowProductInfo.aspx.cs" Inherits="ShowProductInfo" %>

<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>


<%@ Register Src="~/UserControl/STop.ascx" TagPrefix="uc1" TagName="STop" %>
<%@ Register Src="~/UserControl/SLeft.ascx" TagPrefix="uc1" TagName="SLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品详情</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />

    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
   <%-- <script src="js/jquery-1.7.1.min.js"></script>--%>

    <%--<link rel="stylesheet" href="css/style.css">--%>

    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <%--<link href="../member/css/item.css" id="cssid" rel="stylesheet" type="text/css" />--%>

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
   <%-- <script src="../Company/js/jquery-1.4.3.min.js" type="text/javascript"></script>--%>
    <script src="../bower_components/jquery/jquery.min.js"></script>


    <script language="javascript" type="text/javascript">
        window.alert = alert;

        $(document).ready(function () {
            var logintype = '<%=Session["UserType"].ToString() %>';
             if (logintype == "1") {
                 $("#cssid").attr("href", "../member/css/item-co.css")
             } else if (logintype == "2") {
                 $("#cssid").attr("href", "../member/css/item-use.css")
             } else {
                 $("#cssid").attr("href", "../member/css/item.css")
             }
         });


         function CloseCartPop() {
             $("#DivCarPop").hide();
             $("#AddProduct").hide();
             document.getElementById("dvbg").style.display = "none";
         }
         var proid = 0;
         function ShowCartPop(str) {
             proid = str;
             changeCount("1");
             var left = (document.body.clientWidth) / 2 - 100;
             //$("#AddProduct").style.display="block";
             $("#AddProduct").attr("style", "display:block; position:fixed; z-index:100;top:200px;left:" + left + "px;");

             document.getElementById("dvbg").style.height = document.body.scrollHeight + "px";
             document.getElementById("dvbg").style.display = "";

             return false;
         }
         function AddProductToCar() {
             AjaxMemShopCart.GetShopCartStr(proid, $("#count").val(), "21").value;
             var strLl = AjaxMemShopCart.GetLiuLanPro("21").value;
             $("#MyCar").html(strLl);
             $("#AddProduct").hide();

             document.getElementById("dvbg").style.display = "none";

             return false;
         }
         function toJiesuan() {
             AjaxMemShopCart.GetShopCartStr(proid, $("#count").val(), "21").value;
             return false;
         }
         function changeCount(val) {
             if (isNaN(val)) {
                 $("#count").val("1");
                 val = 1;
             }
             if (val < 1) {
                 $("#count").val("1");
                 val = 1;
             }
             var result = (AjaxClass.getProductMoneyPv(proid).value).split(",");
             $("#money").html((result[0] * val).toFixed(2));
             $("#pv").html((result[1] * val).toFixed(2));
             return false;
         }





    </script>


    <style>
        body {
            padding-bottom: 100px;
        }

        .changeBox ul li .changeLt {
            width: 30%;
        }

        .changeBox ul li .changeRt {
            width: 70%;
        }

            .changeBox ul li .changeRt .textBox {
                width: 80%;
            }

        .zcMsg ul li .changeRt .zcSltBox {
            width: 80%;
        }

        .zcMsg ul li .changeRt .zcSltBox2 {
            width: 39%;
        }

        #txtadvpass {
            width: 79%;
            border: 1px solid #ccc;
        }

        .changeBox p:nth-of-type(1) {
            font-size: 16px;
            color: red;
            padding-top: 10px;
        }

        .changeBox p:nth-of-type(3) {
            font-size: 16px;
            color: #666666;
            padding-top: 10px;
        }
        /*.xs_footer li{float:left;width:25%;text-align:center;}
        .xs_footer li a{display:block;padding-top:32px;background:url(img/shouy1.png) no-repeat center 4px;background-size:28px;}
        .xs_footer li .a_cur{color:#77c225}
        .xs_footer li:nth-of-type(2) a{background:url(img/jiangj1.png) no-repeat center 6px;background-size:29px;}
        .xs_footer li:nth-of-type(3) a{background:url(img/xiaoxi1.png) no-repeat center 3px;background-size:32px;}
        .xs_footer li:nth-of-type(4) a{background:url(img/anquan1.png) no-repeat center 5px;background-size:26px;}*/
        #div_PDetails {
            min-height: 200px;
            height: auto;
        }

        .t_top { /*background-color:#8CDAFB;*/
            line-height: 30px;
            background-color: #dd4814;
            border-color: #bf3e11;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white" style="float: left;"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 5%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">商品详情</span>
            </div>
        </div>


        <%--<div class="t_top">	
            <a class="backIcon" href="javascript:history.go(-1)"></a>
               <%=GetTran("009078","商品详情")%> 
            <div >
             <a href="ShoppingCartView.aspx?type=<%= Request["type"]%>" class="tt_r" style="display:block">
                        <span id="sp"><%=GetCount()%></span>
                        </a>  
            </div>
        
        </div>--%>
        <div class="middle">
            <div class="changeBox" style="padding-top: 0">
                <asp:Image ID="ProductImage" runat="server" alt="" Width="100%" />
                <p><%=GetTran("002084","价格")%>：<asp:Label ID="lblPrice" runat="server"></asp:Label></p>
                <%--<p>  PV：<asp:Label ID="lblPPV" runat="server"></asp:Label></p>--%>
                <p><%=GetTran("000558","产品编号")%>：<asp:Label ID="lblPCode" runat="server"></asp:Label></p>
                <p>
                    <%=GetTran("007190","产品类别")%>：
                  <asp:Label ID="lblPType" runat="server"></asp:Label>
                </p>
                <p><%=GetTran("000880","产品规格")%>：<asp:Label ID="lblPSpec" runat="server"></asp:Label></p>
                <%--<p>[翡翠宝石手表]女士时尚精钢金色珐琅，圆形简单大...</p>
            <p>简单大方的设计感，很适合职场拼杀的风范，很有品位,金与蓝
的结合，经典传承，手表是职场女性的身份象征,装饰大过它的
实用，有机结合，带来全新...</p>--%>
            </div>


            <div class="changeBox2">
                <h3><span></span><%=GetTran("009079","产品详情") %></h3>

                <div id="div_PDetails" runat="server">
                    <div></div>
                </div>
                <%-- 
               <ul>
        	        <li>PV：<asp:Label ID="lblPPV" runat="server"></asp:Label></li>
                </ul>
                <ul>
        	        <li><%=GetTran("000558","产品编号")%>：<asp:Label ID="lblPCode" runat="server"></asp:Label></li>
                </ul>
                <ul>
        	        <li><%=GetTran("007190","产品类别")%>：
                  <asp:Label ID="lblPType" runat="server"></asp:Label></li>
                </ul>
                <ul>
        	        <li><%=GetTran("000880","产品规格")%>：<asp:Label ID="lblPSpec" runat="server"></asp:Label></li>
                </ul>--%>
            </div>
        </div>


        <div class="bd_pay">

            <span style="width: 50%">
                <asp:Button ID="LinkButton1" Text="继续浏览" runat="server" OnClick="LinkButton1_Click" Style="width: 100%; height: 100%; background: #aea79f;"></asp:Button>
            </span>
            <span style="width: 50%">

                <%--             
                                                    <input id="Submit3" class="btnC" style="width: auto; height: auto;" 
                                                        value='<%=GetTran("007377","加入购物车") %>' 
                                                        onclick='return ShowCartPop("<%#Request.QueryString["ID"]%>    ")'type="submit" />
                --%>

                <%--<input id="Submit2" class="btnC" style="width: auto; height: auto;" 
                                                value='<%=GetTran("007377","加入购物车") %>' 
                                                onclick='return ShowCartPop("<%#Request.QueryString["ID"]%>")'type="submit" />     --%>
                <asp:Button ID="LinkButton2" Text="加入购物车" runat="server" OnClick="LinkButton2_Click" Style="width: 100%; height: 100%; background: #dd4814; color: #fff;"></asp:Button>
            </span>

        </div>
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
                    <a href="#" class="btn btn-default" id="gb"  data-dismiss="modal">关闭</a>
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


        <div style="display: none">
            <uc1:STop runat="server" ID="STop1" />
            <uc1:SLeft runat="server" ID="SLeft1" />
            <uc1:top runat="server" ID="top" />
        </div>

        <div class="rightArea clearfix" style="display: none">
            <div class="MemberPage">


                <div class="proItem">
                    <div class="pItem-title">
                        <h1>
                            <asp:Label ID="lblPName" runat="server" Style="margin-left: 60px;"></asp:Label></h1>
                    </div>
                    <div class="pItem-img"></div>
                    <div class="pItem-list">
                        <ul>
                            <li><%=GetTran("002084","价格")%>：</li>
                            <li></li>
                        </ul>

                        <ul>
                            <div class="btn" style="margin-top: 19px;">
                            </div>
                            <div class="btn1">
                                <div class="btnLeft1"></div>

                                <div class="btnRight1"></div>
                            </div>
                        </ul>
                    </div>
                    <div style="clear: both"></div>
                </div>




            </div>
        </div>


        <%-- <script src="../JS/alertPopShow.js"></script>--%>

       <%-- <uc2:bottom runat="server" ID="bottom" />
        <%=msg %>--%>
    </form>
</body>
</html>
