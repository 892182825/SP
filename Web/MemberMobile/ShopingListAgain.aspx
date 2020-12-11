<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopingListAgain.aspx.cs" Inherits="Member_ShopingListAgain" EnableEventValidation="false"%>

<%@ Register src="../UserControl/ucPagerMb.ascx" tagname="ucPagerMb" tagprefix="uc1"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>经销商管理系统</title>
    <link href="CSS/shipList.css" rel="stylesheet" type="text/css" />
    <link href="../Company/CSS/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Company/CSS/table.css" rel="stylesheet" type="text/css" />
    <script src="../Company/js/jquery-1.4.3.min.js" type="text/javascript"></script>
    
<script language="javascript" type="text/javascript">

		
		 
		function menu( menu,img,plus )
		{			
			if( menu.style.display == "none")
			{
				menu.style.display="";
				img.src="images/foldopen.gif";
				plus.src="images/MINUS2.GIF";		
			}
			else
			{
				menu.style.display = "none";
				img.src="images/foldclose.gif";
				plus.src="images/PLUS2.GIF";				
			}
			
		}	
                
		function EnShopp(num,proid)
		{
	        num = num.value;
 
		     AjaxClass.EnShopping(parseInt(num),proid.toString());
		    
		    
		    
		     //Bind();
		}
		
		
//		function Bind()//绑定树旁边的表格
//		 {
// 
//            var divPro = document.getElementById('product');
//     
//        
//             
//               divPro.innerHTML=AjaxClass.divhtml().value;
//			
//		 }
//		  window.attachEvent("onload",function(){
//		  
//		 Bind();
// 
//		 
//		 });
		 
		 function AjShopp(proid,proName)
		 {
		    //if(confirm('<%=GetTran("000000", "确认将 ' + proName + ' 产品放入购物车?")%>') )
		    //{
		   document.getElementById("DivCarPop").style.display="";
		        //AjaxClass.Shopping(proid,proName);
		    
		        //Bind();
		    //}
		 }
		 
		 
function ValidateInputValue(control)
{
// 检测输入是否为数字
var myReg=/\d+/;
var obj = myReg.exec(control.value);
if(control.value == "")
{
return;
}
else if (!obj || !(obj == control.value))
{
//alert("您输入的格式不正确！");
//control.value = parseInt(control.title);
control.value ="";
return;
}
// 输入的数字跟上次一样，则不需要继续
if(parseInt(control.value) == parseInt(control.title))
{
return false;
}
control.title = parseFloat(control.value);
} 
		 
		 window.onerror=function()
		 {
		    return true;
		 }
		
    </script>
    <script type="text/javascript"> 
$(document).ready(function(){
     //$(".product_content").height($(window).height()-155)
     
	//Adjust panel height
	
 
    $("#product_list li").hover(function() {
		$(this).addClass("hover"); //Show delete icon on hover
	},function() {
		$(this).removeClass("hover"); //Hide delete icon on hover out
	});
 
    $("#BtnContinue").hover(function() {
		$(this).addClass("Next111"); //Show delete icon on hover
		
	},function() {
		$(this).removeClass("Next111"); //Hide delete icon on hover out
		
	});
	
	$("#Button3").hover(function() {
		$(this).addClass("Next111"); //Show delete icon on hover
		
	},function() {
		$(this).removeClass("Next111"); //Hide delete icon on hover out
		
	});
 
 
	
});
</script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrap">
	
    <div class="main">
   <%-- <div class="product">
    	<span></span>
    </div>--%>
    <div class="shopping_tit">
            <img src="images/cartnav-step1.gif" width="960" height="64" /></div>
    <div class="product_content">
    	<div class="product_sidebar">
        <div class="sidebar_title">产品分类
        
          <a id="qHId" href="RegOrderAgain.aspx">切换</a>
        </div>
       <div class="subnav">
		  <%--<ul>
         <li><a href="#">产品分类</a></li>
         <li><a href="#">露得清</a></li>
         <li><a href="#">参加</a></li>
         <li><a href="#">参洁</a></li>
         <li><a href="#">物料</a></li>
         <li><a href="#">礼品</a></li>
         </ul>--%>
         <ul>
         <li style="height:auto; background-image:none; padding-left:10px; line-height:15px;">
           
                        <asp:Literal ID="menuLabel" runat="server"></asp:Literal>
               
         
                                  
         </li>
         </ul>
         
        </div>
        
        <div class="sidebar_title">最近浏览过的产品</div>
        <div class="subproduct" id="subproduct1">
           <%= GetLiuLanPro()%>
        	<%--<dl>
            	<dd><img src='images/banner_bg.jpg' width='55' height='55' /></dd>
                <dt>洋参胶囊(打装简装)<br />
                	金额：￥138:00<br />
                    PV:68:00
                </dt>
            </dl>
            <dl>
            	<dd><img src="images/banner_bg.jpg" width="55" height="55" /></dd>
                <dt>洋参胶囊(打装简装)<br />
                	金额：￥138:00<br />
                    PV:68:00
                </dt>
            </dl>--%>
        </div>
        </div>
        <div class="product_right" >
            
            <div id="product_search">
    <!--  弹出购物车效果-->
    <div class="te_poplook" id="DivCarPop" style="display:none;">
    <table cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr class="te_top">
                <td class="te_left te_corner"></td>
                <td class="te_middle te_rib"></td>
                <td class="te_right te_corner">
                </td>
            </tr>
            <tr class="te_middle">
                <td class="te_left te_rib"></td>
                <td class="te_middle">
                    <!-- Content -->
                    <div style="border: #bcd7dc 1px solid;" id="cart-pop">
                        <div id="pop-heading">
                            <div class="title">
                                <img alt="帐户激活" src="images/001_47.png">
                                <h2>
                                    产品已成功添加到购物车</h2> 
                            </div> 
                        </div>
                        <div style="padding: 40px 40px 30px; width: auto;" class="pop-content">
                            <p class="notice">
                                购物车共 <span id="ctl00_ContentPlaceHolder1_Product1_PopCart1_LitCount">2</span> 种产品<br>
                                总金额：<span id="ctl00_ContentPlaceHolder1_Product1_PopCart1_LitTotalMoney">1544.00</span>
                                总PV：<span id="ctl00_ContentPlaceHolder1_Product1_PopCart1_LitTotalPV">1544.00</span>
                            </p>
                        </div>
                        <div class="pop-footer">
                            <input type="button" style="cursor: pointer;" class="Next" id="BtnContinue" name="Next" value="继续购物">
                            <input type="button" style="cursor: pointer;" class="Next" id="Button3" name="Next" onclick="window.location='ShoppingCartViewAgain.aspx?tt1=pic'" value="去结算">
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
    <script language="javascript" type="text/javascript">    
$("#BtnContinue").click(function(){ 
    CloseCartPop();
});
function CloseCartPop()
{
    $("#DivCarPop").attr("style","display:none;"); 
}
function ShowCartPop(str)
{ 
    //$(".subproduct").html("");
    
    $(".notice").html(AjaxMemShopCart.GetShopCartStrFx(str,"1","1").value);
    $("#DivCarPop").attr("style","display:block;top:" + (document.documentElement.scrollTop + 200) + "px"); 
    var strLl=AjaxMemShopCart.GetLiuLanProFx("1").value;
     $("#subproduct1").html(strLl);
    //$(".subproduct").html(strLl);
}
</script>
    <!--  弹出购物车效果 结束-->
    <div class="filter">
        <ul class="filter-Search">
            <li>
                <label>产品名搜索：</label>
                   <%-- <input type="text" class="search-text" value="请输入产品关键词" id="SearchKeyword" size="10" >--%>
                <asp:TextBox ID="txtProName" CssClass="search-text" runat="server" Text="请输入产品关键词" MaxLength="10" onmousedown="this.value=''"></asp:TextBox>
              </li>
            <li>
                <asp:DropDownList ID="ddlProList" runat="server">
                    <asp:ListItem Value="PreferentialPrice">价格</asp:ListItem>
                    <asp:ListItem Value="PreferentialPV">PV</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txt1" runat="server" MaxLength="7" CssClass="ipt-t"  Width="40px"></asp:TextBox>
                <%--<input type="text" title="0" size="5" name="sp" class="ipt-t" id="SearchMinValue" maxlength="7">--%>
                <label>
                    至</label>
                    <asp:TextBox ID="txt2" runat="server" MaxLength="7" CssClass="ipt-t"  Width="40px"></asp:TextBox>
               <%-- <input type="text" title="0" size="5" name="ep" class="ipt-t" id="SearchMaxValue" maxlength="7">--%>
            </li>
            <li class="submit-buy">
                <%--<input type="button" class="buy" value="搜索产品" >--%>
                <asp:Button ID="Button1" CssClass="buy" runat="server" Text="搜索产品" 
                    onclick="Button1_Click" />
            </li>
            <li class="cart_but_buy">
                <a href="ShoppingCartViewAgain.aspx?tt1=pic"><img width="151" height="41" alt="去结算" src="images/gouwuche_03.jpg"></a>
            </li>
        </ul>
        <div style="height:10px; _clear:none; _padding-top:10px;" class="clear clearfix">
        </div>        
        
                <ul id="QuicklySearchMemberPrice">
                    <li><label>按价格检索：</label></li>
                    <li>
                     <asp:LinkButton ID="PriceAll" runat="server" onclick="PriceAll_Click">全部</asp:LinkButton>
                     <asp:LinkButton ID="Price0" runat="server" onclick="Price0_Click">0 - 100</asp:LinkButton>
                     <asp:LinkButton ID="Price1" runat="server" onclick="Price1_Click">101 - 500</asp:LinkButton>
                     <asp:LinkButton ID="Price2" runat="server" onclick="Price2_Click">501 - 1000</asp:LinkButton>
                     <asp:LinkButton ID="Price3" runat="server" onclick="Price3_Click">1001 - 2000</asp:LinkButton>
                     <asp:LinkButton ID="Price4" runat="server" onclick="Price4_Click">2001 以上</asp:LinkButton>
                    <%--<a href="ShopingList.aspx?priceStu=-1"></a> 
                <a href="ShopingList.aspx?priceStu=0">0 - 100</a>
            
                <a href="ShopingList.aspx?priceStu=1">101 - 500</a>
            
                <a href="ShopingList.aspx?priceStu=2">501 - 1000</a>
            
                <a href="ShopingList.aspx?priceStu=3">1001 - 2000</a>
            
                <a href="ShopingList.aspx?priceStu=4">2001 以上</a>--%>
            
                </li>
                </ul>
                <ul id="QuicklySearchPV">
                    <li>
                        <label>按PV检索：</label></li>
                    <li style="padding-left:6px;">
                    
                    <asp:LinkButton ID="PvAll"  runat="server" onclick="PvAll_Click">全部</asp:LinkButton>
                     <asp:LinkButton ID="Pv0" runat="server" onclick="Pv0_Click">0 - 100</asp:LinkButton>
                     <asp:LinkButton ID="Pv1" runat="server" onclick="Pv1_Click">101 - 500</asp:LinkButton>
                     <asp:LinkButton ID="Pv2" runat="server" onclick="Pv2_Click">501 - 1000</asp:LinkButton>
                     <asp:LinkButton ID="Pv3" runat="server" onclick="Pv3_Click">1001 - 2000</asp:LinkButton>
                     <asp:LinkButton ID="Pv4" runat="server" onclick="Pv4_Click">2001 以上</asp:LinkButton>
                    
                    <%--<a href="ShopingList.aspx?pvStu=-1">全部</a> 
                <a href="ShopingList.aspx?pvStu=0">0 - 100</a>
            
                <a id="id(MemberPrice &gt;= 101 and MemberPrice &lt;= 500)">101 - 500</a>
            
                <a id="id(MemberPrice &gt;= 501 and MemberPrice &lt;= 1000)">501 - 1000</a>
            
                <a id="id(MemberPrice &gt;= 1001 and MemberPrice &lt;= 2000)">1001 - 2000</a>
            
                <a id="id(MemberPrice &gt;= 2001)">2001 以上</a>--%>
            
                </li>
                </ul>
            
    </div>
    <div class="view_mode">
        <div style="display:none;">
            显示方式：</div>
        <div style="display:none;" class="dd2">
            <div style="padding-top: 5px; padding-left: 10px; height: 22px">
                <a href="#">
                    <img alt="按缩略图方式查看" src="/JrCustomer/App_Themes/Customer2011/images/Product/view_mode_10a1.gif">
                </a>
            </div>
            <div style="padding-top: 5px; padding-left: 10px; height: 22px">
                <a href="#">
                    <img alt="按列表方式查看" src="/JrCustomer/App_Themes/Customer2011/images/Product/view_mode_10a.gif">
                </a>
            </div>
        </div>
        <div>
            排序方式：</div>
        <div class="dd2">
            <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlSort_SelectedIndexChanged">
            <asp:ListItem Value="-1">默认排序</asp:ListItem>
            <asp:ListItem Value="-2">最近热销</asp:ListItem>
            <asp:ListItem Value="PreferentialPrice asc">价格从低到高</asp:ListItem>
            <asp:ListItem Value="PreferentialPrice desc">价格从高到低</asp:ListItem>
            <asp:ListItem Value="PreferentialPV asc">PV从低到高</asp:ListItem>
            <asp:ListItem Value="PreferentialPV desc">PV从高到低</asp:ListItem>
            </asp:DropDownList>
            
        </div>
        <div class="dd2">
            共找到产品<strong class="required"><%= ucPagerMb1.RecordCount %></strong>件</div>
        <div class="dd3">
            <div class="pagination">
                <div class="page-top">
                    <a href="javascript:document.getElementById('ucPagerMb1_lbtnPre').click()" class="page-prev"><span>上一页</span></a>
                    <a href="javascript:document.getElementById('ucPagerMb1_lbtnNext').click()" class="page-next"><span>下一页</span></a>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- end product_search -->
            
            
            <ul id="product_list" class="product_list">
                <asp:Repeater ID="Repeater1" runat="server">
                   <ItemTemplate>
                        <li>
                     <div class="image_wrapper">
                     	<a href='<%#"../ShowProductInfo.aspx?oT=1&rt=3&ty=1&ID="+Eval("ProductID") %>'><img width="140" height="120" src='<%# FormatURL(DataBinder.Eval(Container.DataItem, "ProductID")) %>'></a>
                     </div>
                     <h3><%# DataBinder.Eval(Container.DataItem,"ProductName") %><br><strong><%=GetTran("002084")%>：￥<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PreferentialPrice")).ToString("f2")%>
                                    PV：
                                    <%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PreferentialPV")).ToString("f2")%> </strong>
                     </h3>
                     <div class="Button_wrapper">
                          <strong style="display:none;">￥280.00</strong>
                            <input type="button" class="buy" value=" 加入购物车" style="display:block" id="productid_1" onclick='ShowCartPop("<%#DataBinder.Eval(Container.DataItem, "ProductID")%>")'>
                     </div>
                </li>
                   </ItemTemplate>
                </asp:Repeater>
                
                                                
            </ul>
            
            <!--列表结束-->
			<!--分页-->
			<uc1:ucPagerMb ID="ucPagerMb1" runat="server" />
            
            <!--分页结束-->
            
            </div>
            
                   
    </div>
    </div>
</div>
 

<script>
$("#ucPagerMb1_txtPn").keyup(function(){ValidateInputValue(this)});
	$("#txt1").keyup(function(){ValidateInputValue(this)});
	$("#txt2").keyup(function(){ValidateInputValue(this)});
</script>

    </form>
</body>
</html>


