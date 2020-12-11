<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SLeft.ascx.cs" Inherits="UserControl_SLeft" %>
<style>
    .navIn ul li ol li a {
        padding: 11px 0 11px 85px;
    }
    .navIn a:hover {color:#428bca}
    /*.navIn a:link {color:red}*/
    .navIn a:active {color:red}
</style>
<script type="text/javascript">

</script>
<div class="sideBar">
	<div class="userBox clearfix">
		<%--<div class="portrait pull-left">
			<img src="../images/img/dc.jpg" />
		</div>--%>
		<div class="pull-left userInfo">
			<p><%=GetTran("000037","服务机构编号")%>：<asp:Literal ID="ltlId" runat="server"></asp:Literal></p>
			<p><%=GetTran("000040","服务机构名称")%>：<asp:Literal ID="ltlNme" runat="server"></asp:Literal></p>
			<p><%=GetTran("009001","上次登录时间")%>：<asp:Literal ID="ltlTime" runat="server"></asp:Literal></p>
		</div>
	</div>
	<div class="navIn">
		<ul class="list-unstyled"><%--从数据库加载菜单信息--%>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
		</ul>
	</div>
</div>

<%--<script>
    if (!$.browser.webkit) {

        $.mCustomScrollbar.defaults.scrollButtons.enable = true; //enable scrolling buttons by default
        $.mCustomScrollbar.defaults.axis = "yx"; //enable 2 axis scrollbars by default

        $(".sideBar").mCustomScrollbar({ theme: "dark" });
    }
</script>--%>
<link rel="stylesheet" type="text/css" href="../CSS/bootstrap.css" />
<script>
    /*$('.navIn ul li a').click(function () {
        $(this).siblings().stop().slideToggle('ol').parent('li').siblings('li').children('ol').slideUp(20);
    });*/
    $(function () {
        $('.navIn ul li ol').css('display', 'none');
        //56、57、58、59、60
        var list = $('.navIn ul li ol');
        for (var i = 0; i < list.length; i++) {
            //list.eq(i).css("display", "");
            if ('<%= menuParentid %>' == "56") {
                list.eq(0).css("display", "");
            }
            else if ('<%= menuParentid %>' == "57") {
                list.eq(1).css("display", "");
            }
            else if ('<%= menuParentid %>' == "58") {
                list.eq(2).css("display", "");
            }
            else if ('<%= menuParentid %>' == "59") {
                list.eq(3).css("display", "");
            }
            else if ('<%= menuParentid %>' == "60") {
                list.eq(4).css("display", "");
            }
            else {
                if ('<%= menuUrl %>' == "storenet.aspx") {
                    list.eq(4).css("display", "");
                }
            }
        }
    });
</script>