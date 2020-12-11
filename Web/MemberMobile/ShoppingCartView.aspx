<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShoppingCartView.aspx.cs" Inherits="Member_ShoppingCartView" %>

 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
     <meta http-equiv="x-ua-compatible" content="ie=11" />
 
    <link rel="stylesheet" href="css/style.css">
    <title>购物车</title>
    <link href="../member/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../member/CSS/detail.css" rel="stylesheet" type="text/css" />
   <%-- <link href="../member/css/submit.css" id="cssid" rel="stylesheet" type="text/css" />--%>
  
    
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function(){
            var logintype = '<%=Session["UserType"].ToString() %>';
              if(logintype=="1"){
                  $("#cssid").attr("href","../member/css/submit-co.css")
              }else if(logintype=="2"){
                  $("#cssid").attr("href","../member/css/submit-use.css")
              } else{
                  $("#cssid").attr("href","../member/css/submit.css")
              }
          });
    </script>
    <style type="text/css">
        .subTitle {
            line-height: 30px;
            height: 30px;
            border-bottom-width: 1px;
            border-bottom-style: dashed;
            border-bottom-color: #CCC;
            text-indent: 24px;
            font-size: 14px;
            margin-bottom: 10px;
        }
    </style>


    <style>
        body {
            padding-bottom: 100px;
        }

        
        /*.xs_footer li{float:left;width:25%;text-align:center;}
        .xs_footer li a{display:block;padding-top:32px;background:url(img/shouy1.png) no-repeat center 4px;background-size:28px;}
        .xs_footer li .a_cur{color:#77c225}
        .xs_footer li:nth-of-type(2) a{background:url(img/jiangj1.png) no-repeat center 6px;background-size:29px;}
        .xs_footer li:nth-of-type(3) a{background:url(img/xiaoxi1.png) no-repeat center 3px;background-size:32px;}
        .xs_footer li:nth-of-type(4) a{background:url(img/anquan1.png) no-repeat center 5px;background-size:26px;}*/

        .pay_qd {
            position: fixed;
            bottom: 53px;
            left: 0;
            width: 100%;
            height:44px;
        }

        .proLiCnt .checkbox1 {
            width: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%--<div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>
            <%=GetTran("006981","购物车") %>
        </div>--%>
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 2px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 35%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">购物车</span>
            </div>
        </div>
        <div class="proLi" id="cart_table" style="margin-bottom:100px;">
            <ul>

            




                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <li id='tr<%# DataBinder.Eval(Container.DataItem, "ProductId")%>'>
                            <div class="proLiCnt">
                                <%--   <input class="checkbox1" checked="checked" type="checkbox">--%>
                                <a href='<%#"ShowProductInfo.aspx?oT=0&rt=3&ty=2&ID="+Eval("ProductID") %>'>
                                    <img src='<%# FormatURL(DataBinder.Eval(Container.DataItem, "ProductID")) %>' height="100%" /></a>
                                <div class="proLiCntSon proLiCntSon2">

                                    <a href='<%#"ShowProductInfo.aspx?oT=0&rt=3&ty=2&ID="+Eval("ProductID") %>'>
                                        <p>
                                            <%# DataBinder.Eval(Container.DataItem,"ProductName") %>
                                        </p>
                                    </a>

                                    <p>
                <b class="TranToValue" tranid="000080" style="font-weight: normal;" id='sinp<%# DataBinder.Eval(Container.DataItem, "ProductId")%>'><%=GetTran("002084","价格") %></b>：
                    $<i>
                            <asp:Label ID="Label1" runat="server" Text='<%#(Convert.ToDouble( DataBinder.Eval(Container.DataItem, "PreferentialPrice"))).ToString("f2")%>'></asp:Label>
                                    </p>
                                    <p>
                                        <b class="TranToValue" tranid="000255" style="font-weight: normal;"><%=GetTran("000505","数量") %> </b>：
                                        <input name="textfield" style="width: 40px;border: 1px #ccc double;" onkeypress="return kpyzsz();" onkeyup="   szxs(this);" onafterpaste="szxs(this)" class="ctConPgFor" type="text" id='textfield<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' maxlength="4" title='<%# DataBinder.Eval(Container.DataItem, "proNum")%>' value='<%# DataBinder.Eval(Container.DataItem, "proNum")%>' />
                                        <span id='snp<%# DataBinder.Eval(Container.DataItem, "ProductId")%>'></span>
                                        <span id='snv<%# DataBinder.Eval(Container.DataItem, "ProductId")%>'></span>

                                    </p>
                                </div>

                                <input id="Button1" class="totalDel TranToValue" style="font-size:12px;border: 0; background: none; width: 30px" type="button" value='<%=GetTran("000022","删除")%>' onclick='DeleteCartItem("<%#DataBinder.Eval(Container.DataItem, "ProductId")%>")' />
                            </div>
                            <div class="proLiFot">
                                <%=GetTran("000534","小计") %> ：
                                $
                                <span style="float: none" id='tdp<%# DataBinder.Eval(Container.DataItem, "ProductId")%>'>
                                    <%# (Convert.ToDouble( DataBinder.Eval(Container.DataItem, "TotalPrice"))).ToString("f2") %></span>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>

        <div style="position:fixed;bottom:100px;">
            <span><%=GetTran("000630","合计")%></span>：$
        <span id="ltPrice1" style="color: #ee394a;"> 
            <asp:Literal ID="ltPrice" runat="server"></asp:Literal>
        </span>

        </div>
        

        <script type="text/javascript">
            $(function () {
                $(".glyphicon").removeClass("a_cur");
                $("#c4").addClass("a_cur");
            });
             // 赋予表格样式
             var tdcolor = $("#cart_table").find("tr");
             for (var i = 0; i < tdcolor.length; i++) {
                 if (i != 0) {
                     tdcolor[i].className = (i % 2) ? "" : "alt";
                 }
             }

             //购物车删除产品
             function DeleteCartItem(productID) {
                 var flag = AjaxMemShopCart.DelOne(productID, "0").value;
                     
                 if (flag != "-1") {
                     $("#ltNum1").html(flag.split("|")[2]);
                     $("#ltPrice1").html(flag.split("|")[0]);
                     $("#ltPv1").html(flag.split("|")[1]);
                     $("#tr" + productID).remove();
                     $("#cart_table1").html(AjaxMemShopCart.GetZpStr(flag.split("|")[1]).value);
                     $('#hid_privce').val(flag.split("|")[0]);
                 }
             }
             //修改购物车产品数量
             function UpdateCartItem(control) {
                 //var productID = control.id.replace("qty_","");
                 // 数据效验
                 if (ValidateUpdateCartItem(control) == false)
                     return;
                 control = $(control).parent().find("input:text");
                 var productID = control.attr("id").replace("textfield", "");
                 var flagStr = "0";

                 flagStr = AjaxMemShopCart.UpdShopCart(productID, control.val(), "0").value;

                 if (flagStr == "1") {
                     if (parseInt(control.val()) == 0) {
                         $("#tr" + productID).remove();
                     }

                    control.attr("title", control.val());
                    $("#tdp" + productID).html((Number($("#sinp" + productID).text().replace("￥", "")) * control.val()).toFixed());//2
                    $("#tdv" + productID).html((Number($("#sinv" + productID).text()) * control.val()).toFixed());
                    var totalPrice = 0.00;
                    var totalPv = 0.00;
                    var tNum = 0.00;
                    $(".proLiCnt").each(function() {
                        var price = $(this).find("span").eq(0).html();
                        var num = $(this).find("input").eq(0).val();
                        var productID = $(this).find("input").eq(0).attr("id").replace("textfield", "");
                        var zj = parseFloat(price)*parseFloat(num);
                        totalPrice=parseFloat(totalPrice)+parseFloat(zj);
                        $("#tdp"+productID).html(zj.toFixed(2));
                    });
                    $("#ltPrice1").text(totalPrice.toFixed(2));
                }
                else {
                    alert('<%=GetTran("007402","修改购物车产品数量失败") %>' + "！");
                }
             }

             //验证产品数量修改
             function ValidateUpdateCartItem(control) {
                 // 检测输入是否为数字    
                 var myReg = /\d+/;
                 var obj = myReg.exec(control.value);
                 if (!obj || !(obj == control.value)) {
                     alert("您输入的格式不正确！");
                     control.value = parseInt(control.title);
                     return false;
                 }
                 // 输入的数字跟上次一样，则不需要继续
                 if (parseInt(control.value) == parseInt(control.title)) {
                     return false;
                 }
                 control.title = parseInt(control.value);
                 return true;
             }


             $("#cart_table input:text").blur(function() { UpdateCartItem(this) });
        </script>


        <div class="pay_qd" style="overflow: hidden">
            <input type="submit" onclick="__doPostBack('LinkButton2','')" value='<%=GetTran("007404","继续购物") %>' style="width:50%; float: left; background: #aea79f; border: 0;height:100%;" />
            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Style="display: none;">LinkButton</asp:LinkButton>
            <asp:Button ID="Button2" runat="server" Text="立刻购买" OnClick="Button2_Click" Style="width: 50%; float: left; background: #dd4814; color: #fff; border: 0;height:100%;" />
        </div>

        <div class="modal fade" id="myModall" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    
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

         

        <div class="rightArea clearfix" id="rightArea1" style="display: none">
            <div class="MemberPage" style="margin-top: 80px;">

                <h1 class="subTitle"><%=GetTran("007397", "已加入到购物车的产品")%>：</h1>
                <table id="cart_table1" width="100%" border="0" cellpadding="1" cellspacing="1">
                    <tr class="ctConPgTab">
                        <th><%=GetTran("004183","产品图片")%></th>
                        <th><%=GetTran("000558","产品编号")%></th>
                        <th><%=GetTran("000501", "产品名称")%></th>
                        <th><%=GetTran("000503","单价")%></th>
                        <th>PV</th>
                        <th><%=GetTran("000505","数量")%></th>
                        <th><%=GetTran("000041","总金额")%></th>
                        <th><%=GetTran("007324","总")%>PV</th>
                        <th><%=GetTran("000022","删除")%></th>
                    </tr>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <tr id='tr<%# DataBinder.Eval(Container.DataItem, "ProductId")%>'>
                                <td></td>

                                <td><%# DataBinder.Eval(Container.DataItem, "ProductCode")%> </td>
                                <td></td>

                                <td>￥</td>

                                <td id='sinv<%# DataBinder.Eval(Container.DataItem, "ProductId")%>'><%# DataBinder.Eval(Container.DataItem, "PreferentialPV")%></td>
                                <td>
                                    <div>
                                    </div>
                                </td>
                                <td></td>
                                <td id='tdv<%# DataBinder.Eval(Container.DataItem, "ProductId")%>'><%# DataBinder.Eval(Container.DataItem, "TotalPv")%></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>


                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td><%=GetTran("000630","合计") %></td>
                        <td id="ltNum1">
                            <asp:Literal ID="ltNum" runat="server"></asp:Literal>
                            <input id="input" type="hidden" value="<%#number %>" />
                        </td>

                        <td id="ltPv1">
                            <asp:Literal ID="ltPv" runat="server"></asp:Literal></td>
                        <td>&nbsp;</td>
                    </tr>
                </table>


                <div style="/* [disabled]top: -50px; */">
                    <input type="hidden" runat="server" id="hid_privce" />
                    <div style="float: left; margin: 0 10px;">
                        <div class="btn">
                            <div class="btnLeft"></div>

                            <div class="btnRight"></div>
                        </div>
                    </div>
                    <div style="float: left; margin: 0 10px; display: none;">
                        <div class="btn2" id="cc">
                            <div class="btnLeft2"></div>
                            <%--<input type="submit" class="btnC2" value='<%=GetTran("007406","删除所有产品") %>' id="Button1" name="ctl00$ContentPlaceHolder1$ShoppingCart1$btnDeleteAll" onclick="if(!confirm('<%=GetTran("007407","您确认是否删除购物车当中的所有产品") %>？')) return false;__doPostBack('LinkButton1','')" />--%>
                            <div class="btnRight2"></div>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="display: none;">LinkButton</asp:LinkButton>
                        </div>
                    </div>

                    <div style="float: left; margin: 0 10px;">
                        <div class="btn">
                            <div class="btnLeft"></div>

                            <div class="btnRight"></div>
                        </div>


                    </div>

                </div>

                <!--注册结束-->
            </div>
        </div>
             
    </form>
</body>
</html>
