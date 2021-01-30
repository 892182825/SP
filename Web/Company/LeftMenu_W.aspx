<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeftMenu_W.aspx.cs" Inherits="Company_LeftMenu_W" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>


    <link href="bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/charisma-app.css" rel="stylesheet" />
    <!-- jQuery -->
    <script src="bower_components/jquery/jquery.min.js"></script>
    <link href="CSS/stylen.css" rel="stylesheet" />
    <link href="CSS/iconfont.css" rel="stylesheet" />

    <style type="text/css">
      
    </style>
    <script type="text/javascript">


        function isShowMenu(th) {
            /*var i=1;
            while(true)
            {
                if(document.getElementById("divbody"+i)!=null)
                    createInstance("divbody"+i).style.display="none";
                else
                    break;
                
                i++;
            }*/

            var divbodyobj = createInstance(th.id.replace("title", "body"));
            //var divbodyobj1 = createInstance(th.id.replace("title", "body"));
            var aa = th.id.substr(th.id.length - 1, 1)

            if (divbodyobj.style.display == "none") {
                divbodyobj.style.display = "";
                $("#table" + aa).show();

            }

            else {
                divbodyobj.style.display = "none";
                $("#table" + aa).hide();
            }


        }

        function ShowMessageReceive() {
            window.parent.frames["mainframe"].location.href = "ManageMessage_Recive.aspx";
        }
        function createInstance(menuId) {
            return document.getElementById(menuId);
        }

        function setColor(th) {
            var arrA = th.parentNode.parentNode.parentNode.getElementsByTagName("a");
            var s = "";
            for (var i = 0; i < arrA.length; i++) {
                arrA[i].style.color = "#333333";

            }

            var aa = th.href;
            var index = aa.lastIndexOf("\/");
            var str = aa.substring(index + 1, aa.length)
            th.style.color = "orange";

            AjaxClass.getsession(str);

        }


        function loadM() {

            var h = document.documentElement.clientHeight;

            //document.getElementById("main").style.top=h-(document.getElementById("main").style.height.replace("px","")-0)-2+"px";
            //document.getElementById("Div3").style.top=h-(document.getElementById("Div3").style.height.replace("px","")-0)-2+"px";

            isShowMenu(document.getElementById("divtitle1"));
            document.getElementById("table1").getElementsByTagName("a")[0].style.color = "orange";

            if (navigator.userAgent.toLowerCase().indexOf("ie") != -1)
                window.frameElement.onresize = gb;
            else
                window.onresize = gb;

            //alert(navigator.userAgent.toLowerCase())

        }

        function gb() {
            var h = document.documentElement.clientHeight;

            //document.getElementById("main").style.top=h-(document.getElementById("main").style.height.replace("px","")-0)-2+"px";

            //document.getElementById("div3").style.top=h-(document.getElementById("div3").style.height.replace("px","")-0)-2+"px";
        }

        function aacc() {
            window.parent.document.getElementById("pp").cols = "0px,25px,*";
        }


    </script>
    <style type="text/css">
        .nav {
            width: 200px;
            background-color: #283643;
            min-height: 500px;
            height: 100%;
        }

            .nav .pt {
                height: 100px;
                padding: 10px;
                width: 100%;
            }

                .nav .pt img {
                    width: 50px;
                    height: 50px;
                    border-radius: 5px;
                    margin: 10px;
                    float: left;
                }

                .nav .pt p {
                    line-height: 25px;
                    float: right;
                    width: 100px;
                    color: #a2b4ba;
                }

            .nav ul { margin-left:10px;
                width: 100%;
            }

                .nav ul li { cursor:pointer;   padding-left:10px;
                    list-style: none;
                    color: #a2b4ba;
                    position: relative;
                    line-height: 40px;
                    background-image:url("images/fj.png");
                     background-size:20px;
                 background-repeat:no-repeat;
                   background-position:0px 10px; 
                      font-size: 13px;
                }

                    .nav ul li a {
                        padding: 12px 5px 12px 15px;
                        font-size: 15px;
                        color: #a2b4ba;
                    }

                        .nav ul li a:hover {
                            color: #fff;
                        }

                    .nav ul li .ee { display:none;
                        padding-left: 30px; margin-left:-20px;
                        background-color: #222a31; width:200px;
                    }
                        .nav ul li .ee li {
                         background-image:url("images/zj.png");
                 background-repeat:no-repeat;
                   background-size:20px;
                        }
                            .nav ul li .ee li a {
                             font-size: 13px;
                            }

    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $(".nav ul .top").click(function () { 
                if ($(this).next("ul").css("display") == "none") {

                  //  $(".ee").slideUp();
                     $(this).next("ul").slideDown();
                }
                else if ($(this).next("ul").css("display") == "block") { 
                    $(this).next("ul").slideUp();
                  }
            });
        });

    </script>
</head>
<body style="background-color: #283643;">
    <form id="form1" runat="server" class="sidebar-expanded">

        <div class="nav">
            <div class="pt">
                <img src="images/avatars.png"><p>Admin</p>
                <p>超级管理员</p>
            </div>

              <%=GetMenu() %>
        </div>



        <!--菜单-->
        <%-- style="position:absolute;left:10px;top:0px;width:176px;height:465px;overflow:hidden;background-color:white;filter:alpha(opacity=90);opacity:0.9;z-index:10"--%>

        <dl class="leftmenu" style="margin: 0px; height: 100%; display: none;">
            <dd>
              
            </dd>
        </dl>
        <%--		 <dd> <div id="Div1" class="title">
               <%=GetTitle()%> <span onclick="aacc()"><img src="images/menunu15.gif" align="absmiddle"/></span>   
            </div></dd>--%>
        <%--<div id="Div3" style="position:absolute;left:0px;top:0px;width:176px;height:460px;overflow:hidden;z-index:2;background-repeat:no-repeat;background-image:url(images/lmenudp.gif);background-position:0px 250px">
		</div>--%>
    </form>
</body>
</html>
