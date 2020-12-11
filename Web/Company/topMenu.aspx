<%@ Page Language="C#" AutoEventWireup="true" CodeFile="topMenu.aspx.cs" Inherits="Company_topMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DS2014管理系统-头部</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="js/jquery.js"></script>
    <style type="text/css">
        .white {
            FONT-SIZE: 12px;
            COLOR: white;
            TEXT-DECORATION: none;
            font-family: Arial;
        }

        .yellow {
            FONT-WEIGHT: bold;
            FONT-SIZE: 12px;
            COLOR: yellow;
            TEXT-DECORATION: none;
            font-family: Arial;
        }

        .yellow2 {
            FONT-WEIGHT: bold;
            FONT-SIZE: 13px;
            COLOR: yellow;
            TEXT-DECORATION: none;
            font-family: Arial;
        }
        li
        {
            margin-bottom:20px;
        }
        .topleft {
            width:25%;
        }
        .nav {
            width:60%;
        }
        .topright {
            width:15%
        }
        .nav li {
            width:15%;
        }
        .topright ul li a {
            font-size:12px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function change(id) {
            //var menu0 = document.getElementById("menu0");
            //var menu1 = document.getElementById("menu1");
            //var menu2 = document.getElementById("menu2");
            //var menu3 = document.getElementById("menu3");
            //var menu4 = document.getElementById("menu4");
            //var menu5 = document.getElementById("menu5");
            //var menu6 = document.getElementById("menu6");


            ////menu0.className = "white";
            //menu1.className = "white";
            //menu2.className = "white";
            //menu3.className = "white";
            //menu4.className = "white";
            //menu5.className = "white";
            //menu6.className = "white";

            //alert(id);

            //switch (id) {
            //    case 0:
            //       // menu0.className = "yellow";
            //        	                window.open("First.aspx?flag=1", "mainframe","channelmode"); 
            //        break;
            //    case 1:
            //      //  menu1.className = "yellow";
            //         window.open("QueryMemberInfo.aspx?flag=1", "mainframe","channelmode"); 
            //        break;
            //    case 2:
            //      //  menu2.className = "yellow";
            //           window.open("Provider_ViewEdit.aspx?flag=1", "mainframe","channelmode"); 
            //        break;
            //    case 3:
            //      //  menu3.className = "yellow";
            //          window.open("BrowseStoreOrders.aspx?flag=1", "mainframe","channelmode"); 
            //        break;
            //    case 4:
            //       // menu4.className = "yellow";
            //         window.open("AuditingStoreAccount.aspx?flag=1", "mainframe","channelmode"); 
            //        break;
            //    case 5:
            //      //  menu5.className = "yellow";
            //         window.open("ManageResource.aspx?flag=1", "mainframe","channelmode"); 
            //        break;
            //    case 6:
            //      //  menu6.className = "yellow";
            //          window.open("DeptRolesManage.aspx?flag=1", "mainframe","channelmode"); 
            //        break;

            //    case 10:
            //        menu10.className = "yellow";
            //        	                                           left.aspx?pid=10   leftmenu
            //        break;

           // }
            if (id == 0||id==10) {
                //alert(id)
                AjaxClass.getsession("first.aspx");
                window.open("First.aspx?flag=1", "mainframe", "channelmode");
            }
        }

        function abc() {
            if (window.parent.document.getElementById("pp").cols == "195px,18px,*") {
                window.parent.document.getElementById("pp").cols = "0px,18px,*";
                document.getElementById("imgid").src = "images/menunu16.gif";

                document.getElementById("tx").innerHTML = '<%=GetTran("006011", "显示")%>';
            }
            else {
                window.parent.document.getElementById("pp").cols = "195px,18px,*";
                document.getElementById("imgid").src = "images/menunu15.gif";

                document.getElementById("tx").innerHTML = '<%=GetTran("006017", "隐藏")%>';
            }
        }
        $(function () {
            $('.nav  li a').click(function () {

                $(this).css('background', 'url(images/navbg.png) no-repeat').parent('li').siblings('li').children('a').css('background', 'none')
                var aa = this.href;
                var index = aa.lastIndexOf("\/");
                var str = aa.substring(index + 1, aa.length)
                AjaxClass.gettopsession(str);
            })
        });
        $(function () {
            $('.topright  li a').click(function () {
                var aa = this.href;
                var index = aa.lastIndexOf("\/");
                var str = aa.substring(index + 1, aa.length)
                AjaxClass.getsession(str);
            });





        });

        function loadmenu(id) {
            window.open("leftMenu_W.aspx?pid=" + id, "leftmenu", "channelmode");
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="top" style="background: url(images/topbg.gif) repeat-x; position: fixed; top: 0; left: 0; width: 100%">
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
                    <li><a href="javascript:change(0);" target="mainframe" id="A1"><%=GetTran("001478", "首页")%></a></li>
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
