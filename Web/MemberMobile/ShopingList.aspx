<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopingList.aspx.cs" Inherits="Membermobile_ShopingList" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>


<%@ Register Src="~/UserControl/STop.ascx" TagPrefix="uc1" TagName="STop" %>
<%@ Register Src="~/UserControl/SLeft.ascx" TagPrefix="uc1" TagName="SLeft" %>

<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>购物中心</title>
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <%--<script src="js/jquery-1.7.1.min.js"></script>
    <link href="../member/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../member/css/products.css" id="cssid" rel="stylesheet" type="text/css" />
    <link href="../member/css/divbox.css" rel="stylesheet" type="text/css" />

    <script src="../Company/js/jquery-1.4.3.min.js" type="text/javascript"></script>
 
    <link rel="stylesheet" href="css/style.css" />

    <link href="../Member/hycss/serviceOrganiz.css" rel="stylesheet" />--%>

    <%-- 新模板引用--%>

<%--    <script src="js/jquery-1.4.3.min.js" type="text/javascript"></script>--%>
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <script src="js/bootstrap.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-migrate-1.1.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.mCustomScrollbar.js"></script>
    <%-- 新模板引用--%>


    <script type="text/javascript" language="javascript" src="../js/SqlCheck.js"></script>

    <%--<script src="js/jquery-1.7.1.min.js"></script>--%>



    <link rel="stylesheet" href="css/style.css" />
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
          
            document.getElementById("span").style.display="block";
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
        .cl{ clear:both;}
    </style>
    <style>
        body{
            padding:40px 0 60px;
            font-family: '微软雅黑';
            font-size: 14px;
            background: #eee;
            color: #333;
            padding-bottom: 60px;
            padding-top: 40px;
        }
    </style>
    <script type="text/javascript" >
        //选择不同语言是将要改的样式放到此处
        var lang = $("#lang").text();
        // alert(1);
        if (lang != "L001") {
            //alert("1111");
        }
    </script>
    <style>
        .serch {
            background:url(img/search.png) no-repeat;
            background-size:30px;
            border-width:0px;
            width:30px;
            height:30px;
            position:absolute;
            top:6px;
            z-index:10000;
            right:16%;
        }
    </style>

    <style type="text/css">
        .px {
            background: #fff;
            overflow: hidden;
            zoom: 1;
            width: 100%;
            border-bottom: 1px solid #eaeaea;
            margin: 0 0 5px 0;
            position: fixed;
            top: 40px;
            left: 0px;
            z-index: 999;
            }
        .px a {
            float: left;
            width: 25%;
            text-align: center;
            font-size: 14px;
            height: 40px;
            line-height: 44px;
            background: #fff;
            color: #232326;
            position: relative;
        }
        .px a:after {
            content: "";
            display: block;
            position: absolute;
            width: 1px;
            height: 20px;
            right: 0px;
            top: 13px;
            background: #ccc;
        }
        .px a:nth-child(4):after {
            width:0;
        }
        .px .select {
            color: #93b220;
        }
        .dvp1 {
            float:left;
        }
    .dvp1 p i {
        font-size:16px;
        padding-left:5px;
    }
    .dvp1 p i b{
        font-size:17px;
    }
    .dvp2 {
        float:left;
        margin-left:5px;
    }
    .dvp2 p i {
        text-decoration: line-through;
    }

    .searchProBox a {
        display: block;
        color: #333;
    }
    .searchPro {
    width: 100%;
    overflow: hidden;
    zoom: 1;
    margin: 42px 0 0 0;
    background: #dcdbdb;
}

    .searchPro ul li h6 {
        font-size: 15px;
        font-weight: normal;
        line-height: 20px;
        overflow: hidden;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
        margin-bottom: 5px;
        padding: 0 1%;
        height: 40px;
    }
    .searchProBox a p {
        font-size: 13px;
        margin-bottom: 5px;
    }
    .searchProBox a p i {
        color: tomato;
        font-style: normal;
    }

    .b_shoping{
        margin-top:40px;
        padding:5px;

    }

    .b_shoping ul
{
    overflow: hidden;
}

    .b_shoping ul li
    {
        width: 48%;
        float: left;
        margin-left: 2%;
        margin-top: 6px;
        background: #fff;
        border-radius: 5px;
        padding-bottom: 5px;
        overflow: hidden;
    }

        .b_shoping ul li a
        {
            display: block;
        }

.jiag
{
    font-size: 16px;
    color: red;
}

.b_shoping ul li a p
{
    padding: 0 5px;
    line-height: 22px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

    .b_shoping ul li a p:nth-of-type(2)
    {
        font-size: 12px;
    }

    .b_shoping ul li a p:nth-of-type(1)
    {
        color: #333;
    }


    .more
{
    padding-top: 10px;
    padding-bottom: 10px;
    height: 40px;
    line-height: 20px;
    text-align: center;
    width: 100%;
    color: #999;
    background-color: #eee;
    cursor: pointer;
}

.end
{
    padding-top: 10px;
    padding-bottom: 10px;
    height: 40px;
    line-height: 20px;
    text-align: center;
    width: 100%;
    color: #aaa;
    background-color: #eee;
}
.t_top {
    background-color:#8CDAFB;line-height:30px;
            background-color: #1babec;
    border-color: #bf3e11;
    /*background: url(img/bg1.png) repeat-x 0 0;*/
   border-bottom: 1px solid #bfbfbf;
}
    </style>
</head>
<body style="height:auto">
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form1" runat="server" style="height:auto">
        
        <%--<div style="display:none">
            <uc1:STop runat="server" ID="STop1" />
            <uc1:SLeft runat="server" ID="SLeft1" />
        
            <uc1:top runat="server" ID="top" />
        </div>--%>
       
            <div class="px">
                <a href="javascript:sort('PreferentialPrice asc')">价格 ↑</a>
                <a href="javascript:sort('PreferentialPrice desc')">价格 ↓</a>
               <%-- <a href="javascript:sort('PreferentialPV asc')">PV ↑</a>
                <a href="javascript:sort('PreferentialPV desc')">PV ↓</a>--%>
            </div>
            <div>
                <input type="hidden" id="sort" value="0" />
            </div>    

            <div class="t_top">	 <asp:Button CssClass="serch" ID="Button1" runat="server" OnClick="Button1_Click" />
                <a class="fenlei" href="ShopingList1.aspx?type=new"></a>
               <%-- <asp:TextBox ID="txtProName" placeholder="搜索商品" CssClass="shous" runat="server" MaxLength="10"></asp:TextBox>--%>
                <input type="text" name="cname"  id="cname" onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" onkeyup="ValidateValue(this)"    placeholder="输入商品名称" class="shous"  style="color:black;" runat="server" />

                <input type="hidden" id="hcname" value=""  runat="server"/>
            	<%--<div>
                    <a href="ShoppingCartView.aspx" class="tt_r" style="display:block">
                        <span id="sp"><%=GetCount()%></span>
                    </a>
                </div>--%>
            </div>

            <%--<div class="paix">	
            	<ul>
            		<li class="paix1">	
                    	<asp:DropDownList ID="DropDownList1"  runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">默认排序</asp:ListItem>
                                            <asp:ListItem Value="PreferentialPrice asc">价格从低到高</asp:ListItem>
                                            <asp:ListItem Value="PreferentialPrice desc">价格从高到低</asp:ListItem>
                                            <asp:ListItem Value="PreferentialPV asc">PV从低到高</asp:ListItem>
                                            <asp:ListItem Value="PreferentialPV desc">PV从高到低</asp:ListItem>
                                        </asp:DropDownList>
                    </li>
            		<li>	
                    	<span><%=GetTran("002084","价格") %>: </span><input type="text"><span><%=GetTran("000068","至") %></span><input type="text"><button ><%=GetTran("000434","确定") %></button>
                    </li>
            	</ul>            
            </div>--%>
            
            
            
            <div id="div1" class="b_shoping" style="display: block">
                <input type="hidden" id="cpage" value="0" />
                <div>
                    <ul id="mblist">
                    </ul>
                    <%--<div class="cl"></div>--%>
                </div>
            </div>

        <div id="more"  class="more" onclick="loadMore();">加载更多...</div>

            <%--<div class="b_shoping">
                <ul>
                    <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate> 
                        <li>
                            <a href='<%#"ShowProductInfo.aspx?oT=3&rt=3&ty=1&ID="+Eval("ProductID")+"&type="+Request["type"] %>'>
                            <img alt="" width="100%" height="200" src='<%# FormatURL(DataBinder.Eval(Container.DataItem, "ProductID")) %>'></a>
                            <p>
                                <%# DataBinder.Eval(Container.DataItem,"ProductName") %>
                            </p>
                            <p>
                    <%=GetTran("002084")%>：<%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%>
    <%# ( Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PreferentialPrice"))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        ).ToString("f2") %>
                            </p>

                            <p class="jiag">
                                PV：
                                <%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PreferentialPV")).ToString("f2")%>
                            </p>
                        </li>
                    </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="cl"></div>
                <!--行-->
            </div>--%>

         <%--  <div id="AddProduct" style="display: none;" class="PrListImgCart">
            <h4 class="Plic-Title">
                <%=GetTran("007395", "添加产品到我的购物车")%></h4>
            <span onclick='CloseCartPop(); return false;' style="position: absolute; cursor: pointer;
                top: 10px; right: 10px;">
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
                <ul class="btn" style="margin-top:-13px;margin:0;padding:0;">
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
                             onclick='toJiesuan();location.href="ShoppingCartView.aspx?type=<%= Request["type"] %>    ";return false;'
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
                    <li><%=GetTran("000041","总金额") %>：250.00 <%=GetTran("007324","总") %>PV:250.00</li>
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
        </div>--%>

        <%--<div>
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
                                     
                            <input name="" type="submit" 
                                style="background-color:#77c225;background-repeat: repeat-x;color: #FFF;padding-right: 10px;
                                padding-left: 10px;float: left;height: 24px;line-height: 24px;font-weight: normal;border: 0;"
                            value='<%=GetTran("000434","确定") %>' onclick="javascript:document.getElementById('ucPagerMb1_GoTo').click()" />
                                       
                        </div>
                    </td>
                </tr>
            </table>
        </div>--%>

       <%-- <uc1:PageSj ID="Pager1" runat="server" />--%>

     <!-- #include file = "comcode.html" -->

        
        <script type="text/jscript">
            var pagesize=10;
            
            $(function () {
                $("#cpage").val(0);
                if($("#more").html()!="没有更多了..."){
                    loadMore();
                }
            });

            function sort(type) {
               var txtProName=$("#cname").val();
                if (type != "-1") {
                    $("#cpage").val(1);
                }
                var res = AjaxMemShopCart.ShoppingListXF("", $("#cpage").val(),pagesize,txtProName,12,type,"new").value;
                if (res != "") {
                    if(type!="-1"){
                        $("#mblist").html(res);
                    }else{
                        $("#mblist").append(res);
                    }
                    
                    $("#more").html("加载更多...");
                }else{
                    $("#more").html("没有更多了...");
                }
            }
            function loadMore() {
                if($("#more").html()!="没有更多了..."){
                    $("#more").html("正在加载...");
                    var pg = $("#cpage").val();
                    pg = parseInt(pg) + 1;
                    $("#cpage").val(pg);
                    sort("-1");
                }
            }
            $(function () {
                $(".glyphicon").removeClass("a_cur");
                $("#c4").addClass("a_cur");
            });
    </script>

    </form>
</body>
</html>
<style>
    .paix li select {
        padding:0px;
    }
</style>