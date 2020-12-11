<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopingList1.aspx.cs" Inherits="Member_ShopingList" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>

<%@ Register Src="~/UserControl/STop.ascx" TagPrefix="uc1" TagName="STop" %>
<%@ Register Src="~/UserControl/SLeft.ascx" TagPrefix="uc1" TagName="SLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="x-ua-compatible" content="ie=11" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--    <title>复消报单</title>--%>



    <%--------------------------------------------------------------------------------------------------------------------------------%>

    <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title><%#GetTran("009077","分类") %></title>
    <link rel="stylesheet" href="css/style.css">
    <style>
        /*.xs_footer li a{display:block;padding-top:40px;background:url(img/shouy1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li:nth-of-type(2) a{background:url(img/jiangj1.png) no-repeat center 10px;background-size:32px;}
.xs_footer li:nth-of-type(3) a{background:url(img/xiaoxi1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li:nth-of-type(4) a{background:url(img/anquan1.png) no-repeat center 8px;background-size:27px;}*/
        .fenlei_in {
            overflow: hidden;
            background: #fff;
            height: 100%;
            margin-top: 2px;
            padding:2px;
        }

        .f_left {
            float: left;
            width: 30%;
            border-right: 1px solid #ccc;
            height: 100%;
        }

            .f_left li {
                padding: 12px 0;
                border-bottom: 1px solid #ccc;
                text-align: center;
            }

        html, body {
            height: 100%;
        }

        .f_right {
            height: 100%;
            width: 70%;
            float: left;
        }

            .f_right ul li a {
                float: left;
                width: 33.33%;
                padding: 5px;
            }

                .f_right ul li a p {
                    text-align: center;
                    font-size: 14px;

                    display:block;
                }

            .f_right ul li {
                padding:2px;
                
                display: none;
            }
    </style>
    <%--------------------------------------------------------------------------------------------------------------------------------%>
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            var logintype = '<%=Session["UserType"].ToString() %>';
            if (logintype == "1") {
                $("#cssid").attr("href", "../member/css/products-co.css")
            } else if (logintype == "2") {
                $("#cssid").attr("href", "../member/css/products-use.css")
            } else {
                $("#cssid").attr("href", "../member/css/products.css")
            }
        });

        function menu(menu, img, plus) {
            if (menu.style.display == "none") {
                menu.style.display = "";
                img.src = "images/foldopen.gif";
                plus.src = "images/MINUS2.GIF";
            }
            else {
                menu.style.display = "none";
                img.src = "images/foldclose.gif";
                plus.src = "images/PLUS2.GIF";
            }

        }

        function EnShopp(num, proid) {
            num = num.value;

            AjaxClass.EnShopping(parseInt(num), proid.toString());
        }

        function AjShopp(proid, proName) {
            document.getElementById("DivCarPop").style.display = "";
        }

        function ValidateInputValue(control) {
            // 检测输入是否为数字
            var myReg = /\d+/;
            var obj = myReg.exec(control.value);
            if (control.value == "") {
                return;
            }
            else if (!obj || !(obj == control.value)) {
                //alert("您输入的格式不正确！");
                //control.value = parseInt(control.title);
                control.value = "";
                return;
            }
            // 输入的数字跟上次一样，则不需要继续
            if (parseInt(control.value) == parseInt(control.title)) {
                return false;
            }
            control.title = parseFloat(control.value);
        }

        window.onerror = function() {
            return true;
        }
		
    </script>

    <script type="text/javascript"> 
        var showCount=1;
        var unit=5;
        $(document).ready(function() {
            showCount = 1;
            var lists = $("#productlist ul");
            var count = lists.length;
            if (count <= unit) {
                $("#open").hide();
            }
            $("#close").hide();
            for (var i = 0; i < count; i++) {
                if (i < showCount * unit) {
                    $(lists[i]).show();
                } else {
                    $(lists[i]).hide();
                }
            }
        });

        function openMore() {
            showCount++;
            var lists = $("#productlist ul");
            var count = lists.length;
            if (count <= showCount * unit) {
                $("#open").hide();
            }
            $("#close").show();
            for (var i = 0; i < count; i++) {
                if (i < showCount * unit) {
                    $(lists[i]).show();
                } else {
                    $(lists[i]).hide();
                }
            }
        }
        function closeMore() {
            showCount--;
            if (showCount == 0) {
                showCount = 1;
            }
            if (showCount == 1) {
                $("#close").hide();
            }
            var lists = $("#productlist ul");
            var count = lists.length;
            if (count >= showCount * unit) {
                $("#open").show();
            }
            for (var i = 0; i < count; i++) {
                if (i < showCount * unit) {
                    $(lists[i]).show();
                } else {
                    $(lists[i]).hide();
                }
            }
        }
    </script>

    <script language="javascript" type="text/javascript">

        function CloseCartPop() {
            $("#DivCarPop").hide();
            $("#AddProduct").hide();
            document.getElementById("dvbg").style.display = "none";
        }
        var proid=0;
        function ShowCartPop(str) {
            proid = str;
            changeCount("1");
            var left = (document.body.clientWidth) / 2 - 100;
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
    <style type="text/css">
        .cl {
            clear: both;
        }

        .jiag
{
    height:33.3%;    
    font-size: 14px;
    color: red;
}
        /*.productImg {
        height: 63px;
        width: 80px;
        padding-top: 2px;
        }*/
    </style>

</head>
<body style="height: auto">
    <form id="form1" runat="server" style="height: auto">
        <div style="display: none">
            <uc1:STop runat="server" ID="STop1" />
            <uc1:SLeft runat="server" ID="SLeft1" />

            <uc1:top runat="server" ID="top" />
        </div>
         <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 35%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%); font-weight: 600;">分类</span>
            </div>
        </div>
       <%-- <div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>
            <%=GetTran("009077","分类") %>
            <div class="tt_r">
            </div>

        </div>--%>

        <div class="fenlei_in">
            <div class="f_left">
                <ul id="Item">

                    <asp:Repeater ID="rptFold" runat="server" OnItemDataBound="rptFold_ItemDataBound">
                        <ItemTemplate>

                            <asp:HiddenField ID="HFPid" Value='<%# Eval("productid") %>' runat="server" />
                            

                            <div style="">
                                <%--<asp:Repeater ID="childfold" runat="server">
                                    <ItemTemplate>--%>

                                        <li>
                                            <a href='<%# "shopinglist1.aspx?pid="+ Eval("productid") %>'>
                                                <%# Eval("productname") %></a>
                                        </li>
                                    <%--</ItemTemplate>
                                </asp:Repeater>--%>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>

            <div class="f_right">
                <ul>

                    <asp:Repeater ID="rep" runat="server">
                        <ItemTemplate>
                            <li style="display: block">
                                <a href='<%#"ShowProductInfo.aspx?oT=3&rt=3&ty=1&ID="+Eval("ProductID")+"&type="+Request["type"] %>'>
                                    <img  width="63px" height="80px;" src='<%# FormatURL(DataBinder.Eval(Container.DataItem, "ProductID")) %>'>
                                </a>

                                <p style="height:33px;line-height:50px;">
                                    <%# DataBinder.Eval(Container.DataItem,"ProductName") %>
                                </p>

                                <p style="height:33px;line-height:50px;">
                                    <%=GetTran("002084")%>：
                             <%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(defc) ))==1?"￥":"$"%>
                                    <%--<%#
                                        Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PreferentialPrice")).ToString("f2")
                                    %>--%>
                                    <%# (( Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PreferentialPrice")))* 7).ToString("f2")%>
                                </p>

                                <%--<p class="jiag">
                                    PV：
                                    <%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PreferentialPV")).ToString("f2")%>
                                </p>--%>

                               <%-- <div class="btn" style="display: none">
                                    <div class="btnLeft">
                                    </div>
                                    <input id="productid_1" class="btnC" style="width: auto; height: auto;"
                                        value='<%=GetTran("007377","加入购物车") %>'
                                        onclick='return ShowCartPop("<%#DataBinder.Eval(Container.DataItem, "ProductID")%>    ")' type="submit" />
                                    <div class="btnRight">
                                    </div>
                                </div>--%>

                            </li>

                        </ItemTemplate>
                    </asp:Repeater>

                </ul>
                <div class="cl"></div>
                <!--行-->
            </div>
        </div>
        <%----%>

        <!-- #include file = "comcode.html" -->
        <div class="rightArea clearfix" style="display: none">
            <div class="MemberPage" style="padding-top: 40px; margin-left: 200px; overflow: hidden">

                <!--内容部分,产品-->
                <div class="productCent" style="width: 1080px">
                    <!--产品搜索,展示,中间-->
                    <div class="prdListC">
                        <div class="prcSchList">
                            <div runat="server" class="foldList" id="list">
                            </div>

                            <div id="productlist" class="prcSearch" style="display: none">
                                <asp:Repeater ID="rptFold2" runat="server" OnItemDataBound="rptFold_ItemDataBound">
                                    <ItemTemplate>
                                        <ul id="item" style="width: 771px; border-bottom: dashed 1px #ccc;">
                                            <li class="fold"><a href='<%# "shopinglist.aspx?pid="+ Eval("productid") %>'>
                                                <%# Eval("productname") %>：</a></li>
                                            <li style="width: 600px; float: left;">
                                                <asp:HiddenField ID="HFPid" Value='<%# Eval("productid") %>' runat="server" />
                                                <asp:Repeater ID="childfold" runat="server">
                                                    <ItemTemplate>
                                                        <div class="folds">
                                                            <a href='<%# "shopinglist.aspx?pid="+ Eval("productid") %>'>
                                                                <%# Eval("productname") %></a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </li>
                                            <li style="clear: both"></li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="prcSchList2">
                                    <img onclick="openMore();" id="open" src="../member/images/products-ListButtonDown.png"
                                        alt='<%=GetTran("007378","展开") %>' width="19" height="17" />
                                    <img onclick="closeMore();" id="close" src="../member/images/products-ListButtonDown2.png"
                                        alt='<%=GetTran("007379","收缩") %>' width="19" height="17" />
                                </div>
                            </div>

                            <!--搜索结果信息,版头-->
                            <div class="prcSchArray" style="display: none">
                                <!--详细搜索-->
                                <div class="prcSchList">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr class="prcScTabTr">
                                            <td width="8%">
                                                <%=GetTran("007380","排序方式")%>：
                                            </td>
                                            <td width="13%">
                                                <asp:DropDownList ID="ddlSort" CssClass="proTabForm" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">默认排序</asp:ListItem>
                                                    <asp:ListItem Value="PreferentialPrice asc">价格从低到高</asp:ListItem>
                                                    <asp:ListItem Value="PreferentialPrice desc">价格从高到低</asp:ListItem>
                                                    <asp:ListItem Value="PreferentialPV asc">PV从低到高</asp:ListItem>
                                                    <asp:ListItem Value="PreferentialPV desc">PV从高到低</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="12%">
                                                <asp:DropDownList ID="ddlProList" Visible="false" runat="server">
                                                    <asp:ListItem Selected="True" Value="PreferentialPrice">价格</asp:ListItem>
                                                    <asp:ListItem Value="PreferentialPV">PV</asp:ListItem>
                                                </asp:DropDownList>
                                                <%=GetTran("002084","价格")%>：
                                        <asp:TextBox ID="txt1" runat="server" MaxLength="7" CssClass="proTabForm" Width="40px"></asp:TextBox>
                                            </td>
                                            <td width="3%">
                                                <%=GetTran("000068","至")%>
                                            </td>
                                            <td width="6%">
                                                <asp:TextBox ID="txt2" runat="server" MaxLength="7" CssClass="proTabForm" Width="40px"></asp:TextBox>
                                            </td>
                                            <td width="12%" class="prcScTabTrTd">
                                                <asp:Button ID="ImageButton1" runat="server" Width="52" Height="20" Style="background-image: url(../member/images/loginButton1-fb.png);"
                                                    OnClick="Button1_Click" Text="确认" />
                                            </td>
                                            <td width="16%">
                                                <asp:TextBox ID="txtProName" CssClass="proTabForm" runat="server" MaxLength="10"
                                                    onmousedown="this.value=''"></asp:TextBox>
                                            </td>
                                            <td width="14%">
                                                <div id="rr" class="btn">
                                                    <div class="btnLeft">
                                                    </div>
                                                    <asp:Button CssClass="btnC" ID="Button1" runat="server" Text="搜索" OnClick="Button1_Click" />
                                                    <div class="btnRight">
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <!--搜索结果,产品信息-->
                            </div>



                            <div class="prcSchPage">
                                <table>
                                    <tr>
                                        <td>
                                            <%=GetTran("007390", "共找到产品")%><strong class="required"><%= ucPagerMb1.RecordCount %></strong><%=GetTran("007392","件")%>
                                        </td>
                                        <td>
                                            <uc1:ucPagerMb ID="ucPagerMb1" runat="server" />
                                        </td>
                                        <td>
                                            <div class="btn">
                                                <div class="btnLeft">
                                                </div>
                                                <input name="" type="submit" class="btnC" value='<%=GetTran("000434","确定") %>' onclick="javascript:document.getElementById('ucPagerMb1_GoTo').click()" />
                                                <div class="btnRight">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="cl"></div>
                        </div>
                        <!--右侧栏结束-->
                    </div>
                    <!--产品搜索,右侧-->
                    <div class="prdListR" style="display: none">
                        <div style="margin-bottom: 20px; cursor: pointer;">
                            <div class="prcClass" onclick="javascript:location.href='ShoppingCartView.aspx?type=<%= Request["type"] %>';">
                                <h1 class="prcCsTitle"><%=GetTran("007393","去结算") %></h1>
                            </div>
                            <br />
                            <!--浏览历史-->
                            <div class="prcClass">
                                <h1 class="prcCsTitle">
                                    <a href="ShoppingCartView.aspx?type=<%= Request["type"]%>">
                                        <%=GetTran("007394","我的购物车")%></a></h1>
                                <!--详细产品信息-->
                                <div id="MyCar">
                                    <%= GetLiuLanPro()%>
                                </div>
                                <!--详细产品信息-->
                            </div>
                        </div>
                        <div class="cl"></div>
                    </div>
                    <div class="cl"></div>
                    <!--页面内容结束-->
                </div>

                <!--注册结束-->
            </div>
        </div>

        <div id="product_search" style="display: none">
            <div id="AddProduct" style="display: none;" class="PrListImgCart">
                <h4 class="Plic-Title">
                    <%=GetTran("007395", "添加产品到我的购物车")%></h4>
                <span onclick='CloseCartPop(); return false;' style="position: absolute; cursor: pointer; top: 10px; right: 10px;">
                    <%=GetTran("000019","关闭")%></span>
                <div class="plic-1">
                    <ul id="Ul1">
                        <li class="plicImg-1">
                            <%=GetTran("000515", "产品数量")%>：<input type="text" id="count" value="1" maxlength="6"
                                style="width: 60px;" onchange="changeCount(this.value);" class="proTabForm" /></li>
                        <li>
                            <%=GetTran("000041","总金额")%>：<label id="money">0.00</label>
                            <%=GetTran("007324","总")%>PV:<label id="pv">0.00</label></li>
                    </ul>
                </div>
                <div class="plic-2">
                    <ul class="btn" style="margin-top: -13px; margin: 0; padding: 0;">
                        <li>
                            <div class="btnLeft">
                            </div>
                            <input id="productid_1" class="btnC" value='<%=GetTran("007377","加入购物车") %>' onclick='AddProductToCar(); return false;'
                                type="submit" />
                            <div class="btnRight">
                            </div>
                        </li>
                        <li>
                            <div class="btnLeft">
                            </div>
                            <input id="Submit1" class="btnC" value='<%=GetTran("007393","去结算") %>'
                                onclick='toJiesuan();location.href="ShoppingCartView.aspx?type=<%= Request["type"] %>";return false;'
                                type="submit" />
                            <div class="btnRight">
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div id="DivCarPop" style="display: none;" class="PrListImgCart">
                <h4 class="Plic-Title">
                    <%=GetTran("007396", "产品已经成功添加到购物车")%></h4>
                <div class="plic-1">
                    <ul id="notice">
                        <li class="plicImg-1">购物车中共1种产品</li>
                        <li>总金额：250.00 总PV:250.00</li>
                    </ul>
                </div>
                <div class="plic-2">
                    <ul>
                        <li>
                            <img style="cursor: pointer;" onclick="CloseCartPop();" src="../member/images/proCartList-Button1.png"
                                width="92" height="30" /></li>
                        <li>
                            <img style="cursor: pointer;" onclick="window.location='ShoppingCartView.aspx?tt1=pic'"
                                src="../member/images/proCartList-Button.png" width="92" height="30" /></li>
                    </ul>
                </div>
            </div>
            <!-- end product_search -->

            <script type="text/javascript">
            $("#ucPagerMb1_txtPn").keyup(function(){ValidateInputValue(this)});
            $("#txt1").keyup(function(){ValidateInputValue(this)});
            $("#txt2").keyup(function(){ValidateInputValue(this)});
            $(function () {
                $(".glyphicon").removeClass("a_cur");
                $("#c4").addClass("a_cur");
            });
            </script>
            <div id="dvbg" style="position: fixed; left: 0px; top: 0px; width: 100%; height: 100%; filter: alpha(opacity=50); opacity: 0.5; background-color: black; z-index: 10; display: none;"></div>
            <uc2:bottom runat="server" ID="bottom" />
    </form>
</body>
</html>
