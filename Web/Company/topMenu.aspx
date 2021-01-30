<%@ Page Language="C#" AutoEventWireup="true" CodeFile="topMenu.aspx.cs" Inherits="Company_topMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> </title>
     <link href="bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
 
    <!-- jQuery -->
    <script src="bower_components/jquery/jquery.min.js"></script>
	<link href="CSS/stylen.css" rel="stylesheet" />
    <link href="CSS/iconfont.css" rel="stylesheet" />
 
 
</head>
<body class="sidebar-expanded">
   
        <div class="skin-default" id="wrapper">
		<header class="navbar-header">
			<div class="brand">
				<a class="navbar-brand" href="index.html" title="AlphaAdmin">后台管理系统</a>
			</div>
			<div class="navbar">
				 
				<div class="navbar-menu pull-right">
					<ul class="nav navbar-nav">
						<li class="dropdown message">
							<a class="dropdown-toggle" data-toggle="dropdown" href="#" title="消息">
								<i class="iconfont ">消息</i>
								<span class="badge">0</span>
							</a>
							<ul class="dropdown-menu">
								<li class="header">
									您有4条消息
								</li>
								<li class="body"> 
								</li>
								<li class="footer">
									<a href="#" title="查看所有消息">
										查看所有消息
									</a>
								</li>
							</ul>
						</li>
						<li class="dropdown notify">
							<a class="dropdown-toggle" data-toggle="dropdown" href="#" title="通知">
								<i class="iconfont">通知</i>
								<span class="badge">0</span>
							</a>
							<ul class="dropdown-menu">
								<li class="header">
									您有7个通知
								</li>
								 
								<li class="footer">
									<a href="#" title="查看所有通知">
										查看所有通知
									</a>
								</li>
							</ul>
						</li>
						<li class="dropdown user">
							 <a href="javascript:change(0);" target="mainframe" id="A1"><%=GetTran("001478", "首页")%></a>  </li>
						<li class="dropdown user">
                            <a href="../Logout.aspx?tp=gongs" target="_parent"><%=GetTran("001652", "退出")%></a>
						 
						</li>
					</ul>
				</div>
			</div>
		</header>
	
	 
	</div>

 <form id="form1" runat="server">

        <div class="top" style="background: url(images/topbg.gif) repeat-x; position: fixed; top: 0; left: 0; width: 100%; display:none;">
            <div class="topleft">
                <div class="user">
                    <marquee onmouseover="this.stop()" onmouseout="this.start()" direction="up" behavior="scroll" scrollamount="2" height="75" style="margin-left: -30px;">
                        <span style="color:white;line-height:22px"><%=GetTran("001066", "管理员编号")%>：</span><span style="color:#ffffff;margin-left: -30px;"><asp:Literal  ID="ltlId" runat="server"></asp:Literal></span><br />
                        <span style="color:white;line-height:22px"><%=GetTran("002049", "管理员姓名")%>：</span><span style="color:#ffffff;margin-left: -30px;"><asp:Literal  ID="ltlNme" runat="server"></asp:Literal></span><br />
                        <span style="color:white;line-height:22px"><%=GetTran("002051", "上次登录")%>：</span><span style="color:#ffffff;margin-left: -30px;"><asp:Literal ID="ltlTime" runat="server"></asp:Literal></span><br />
                        <asp:Literal ID="ltlMessage" runat="server" Visible=false></asp:Literal>
                    </marquee>
                </div>
                <a href="#"></a>
            </div>

            <ul class="nav" id="menu" runat="server" >
               <%-- <li>
                    <a class="selected" href="leftMenu_W.aspx?pid=1" target="leftmenu" id="A2" runat="server" onclick="change(1);">
                        <img src="images/icon01.png" /><h2>客户管理</h2>
                    </a>
                </li>
                <li>
                    <a class="white" href="leftMenu_W.aspx?pid=2" target="leftmenu" id="A3" runat="server" onclick="change(2);">
                        <img src="images/icon02.png" /><h2>库存管理</h2>
                    </a>
                </li>
                <li>
                    <a class="white" href="leftMenu_W.aspx?pid=3" target="leftmenu" id="A4" runat="server" onclick="change(3);">
                        <img src="images/icon03.png" /><h2>销售管理</h2>
                    </a>
                </li>
                <li>
                    <a class="white" href="leftMenu_W.aspx?pid=4" target="leftmenu" id="A5" runat="server" onclick="change(4);">
                        <img src="images/icon04.png" /><h2>财务管理</h2>
                    </a>
                </li>
                <li>
                    <a class="white" href="leftMenu_W.aspx?pid=5" target="leftmenu" id="A6" runat="server" onclick="change(5);">
                        <img src="images/icon05.png" /><h2>信息中心</h2>
                    </a>
                </li>
                <li>
                    <a class="white" href="leftMenu_W.aspx?pid=6" target="leftmenu" id="A7" runat="server" onclick="change(6);">
                        <img src="images/icon06.png" /><h2>系统管理</h2>
                    </a>
                </li>--%>
            </ul>


            <div class="topright">
                <ul>
                    <li></li>
                    <li><span>
                        <img src="images/help.png" title="帮助" class="helpimg" /></span><a href="javascript:change(0);" target="mainframe" id="menu10" > <%=GetTran("001651", "帮助")%></a></li>
                    <li><a href="../Logout.aspx?tp=gongs" target="_parent"><%=GetTran("001652", "退出")%></a></li>
                    <li><a onclick="abc()"><%=GetTran("006017", "隐藏")%></a></li>
                </ul>
                <%-- <a onclick="abc()" style="color:white;font-size:10pt;cursor:pointer;display:none;" ><img src="images/menunu15.gif" id="imgid" align="absmiddle"><span id="tx" ><%=GetTran("006017", "隐藏")%></span></a>--%>
            </div>
            <asp:HiddenField ID="StoreID" runat="server" />
            <div id="DealDiv" runat="server"></div>
        </div>
    </form>
</body>
</html>
