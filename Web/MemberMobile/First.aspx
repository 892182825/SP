<%@ Page Language="C#" AutoEventWireup="true" CodeFile="First.aspx.cs" Inherits="Member_First" %>

<%@ Register Src="~/UserControl/MemberPerformance.ascx" TagName="performance" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("000370", "会员首页")%></title>
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=yes" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="css/style.css" />
    <script type="text/javascript" src="../bower_components/jquery/jquery.min.js"></script>
    <style type="text/css">
        .dv1 {width:80%;height:160px;margin-top:10px;position: absolute;    border-top: 2px transparent solid;
    border-image: linear-gradient(to right,#0E4A93,#5F0BC1) 1 10;
    background-color: #1A202E;
    margin-left:5%;
        }
        .dv2 {margin-left: 25px;font-size:15px;padding-top: 25px;font-weight:bold;width:100%
        }
        .dv3 {
            margin-left:25px;font-size:18px;margin-top:50px;width:60%;width:100%;color:#fff;
        }
        .content{transform:scale(0.5);opacity:0;transition:all 1s;}
        .active{transform:scale(1);opacity:1;transition:all 1s;}

        .signin { z-index:1000; position:fixed; width:60%; top:150px;  left : 20%;margin:auto; height:150px; background:#ff0000; border-radius:30px; display:none;
        }
        .gai { width:100%; height:50%; background:#ff6a00;
               border-bottom-left-radius:50%; border-bottom-right-radius:50%; border-top-left-radius:30px; border-top-right-radius:30px; 
        }
         
        .gaiqi { width:60px; height:60px; border-radius:50px;
                 background:#ffd800; font-size:40px; color:#f00;
                 font-family:YouYuan; margin:auto; margin-top:-30px;
                line-height:60px; text-align:center;
                cursor:pointer;  
        }
          
        
        .gaiqi1 { width:100px; height:100px; border-radius:5px;
                 background:#ffd800; font-size:20px; color:#2dd60c;
                 font-family:YouYuan; margin:auto;  margin-top:-50px;
                line-height:40px; text-align:center;
                cursor:pointer;  
        }
    </style>

    

</head>
<body style="height:100%">
    <form id="form2" runat="server">
        <div class="signin">
            <div class="gai">
                <div class="getcoin"></div>
            </div>
            <div id="kaiqi" class="gaiqi" onclick="showcu()">领</div>
        </div>

        <div style="width: 100%; height: 100%; margin: 0 0 0 0;">
            <div style="height: 100px; color: #fff;">
                <div style="height: 30px; text-align: center; font-size: 20px; padding-top: 10px">Super Planet</div>
            </div>
            <div style="margin-left: 10%;">
                <h4 style="color: #797979">总资产</h4>
            </div>
            <div style="margin-top: 10px; width: 90%; height: 70px; margin-left: 5%; color: #fff; border-bottom: 1px solid #484848!important;">

                <div style="margin-left: 5%; float: left; font-size: 24px; margin-top: 5%; width: 70%">
                    $
                    <asp:Label ID="lblBonse" runat="server" Text="0.0000"></asp:Label>
                </div>
                <div></div>
            </div>




            <div style="margin-left: 10%; margin-top: 40px;">
                <h4 style="color: #797979">账户总览</h4>
            </div>
            <div style="width: 90%; height: 170px; margin-top: 10px; margin-left: 5%" id="box">
                <%--<div style="width:100%;height:40px;">
                      <div style="float:left;font-size:20px;font-weight:bold;">账户总览</div>
                      <div style="float:right;color:#fe3f10;text-align:right">会员级别：<br /></div>
                  </div>--%>
                <div class="dv1 content active">
                    <div class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=AccountXJ" style="text-decoration: underline; color: #fff;">会员等级</a></div>
                    <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">钱包里可用的FTC</div>--%>
                    <div class="dv3">
                        <asp:Label ID="Label1" Style="font-size: 18px" runat="server" Text="0.00"></asp:Label><asp:Label ID="Label2" Style="font-size: 20px" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="margin-left: 25px; font-size: 12px; margin-top: 0px; color: #797979;">
                        <asp:Label ID="mobil" Style="color: #797979; font-size: 12px" runat="server" Text="0.00"></asp:Label>
                    </div>
                    <div style="float: right; margin-top: -50px; color: #fff; margin-right: 15px">→滑动切换</div>
                </div>

                <div class="dv1 content">
                    <div class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=A" style="text-decoration: underline; color: #fff;">土星账户</a></div>
                    <div class="dv3">
                        <asp:Label ID="lblPointA" runat="server" Text="0.00"></asp:Label>
                    </div>
                    <div style="margin-left: 25px; font-size: 12px; margin-top: 0px; color: #797979;">
                        <asp:Label ID="Label3" Style="color: #797979; font-size: 12px" runat="server" Text="0.00"></asp:Label>
                    </div>
                </div>

                <div class="dv1 content">
                    <div class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=B" style="text-decoration: underline; color: #fff;">金星账户</a></div>
                    <div class="dv3">
                        <asp:Label ID="lblPointB" runat="server" Text="0.00"></asp:Label>
                    </div>
                    <div style="margin-left: 25px; font-size: 12px; margin-top: 0px; color: #797979;">
                        <asp:Label ID="Label4" Style="color: #797979; font-size: 12px" runat="server" Text="0.00"></asp:Label>
                    </div>
                </div>

                <div class="dv1 content">
                    <div class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=C" style="text-decoration: underline; color: #fff;">水星账户</a></div>
                    <div class="dv3">
                        <asp:Label ID="lblPointC" runat="server" Text="0.00"></asp:Label>
                    </div>
                    <div style="margin-left: 25px; font-size: 12px; margin-top: 0px; color: #797979;">
                        <asp:Label ID="Label5" Style="color: #797979; font-size: 12px" runat="server" Text="0.00"></asp:Label>
                    </div>
                </div>

                <div class="dv1 content">
                    <div class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=D" style="text-decoration: underline; color: #fff;">木星账户</a></div>
                    <div class="dv3">
                        <asp:Label ID="lblPointD" runat="server" Text="0.00"></asp:Label>
                    </div>
                    <div style="margin-left: 25px; font-size: 12px; margin-top: 0px; color: #797979;">
                        <asp:Label ID="Label6" Style="color: #797979; font-size: 12px" runat="server" Text="0.00"></asp:Label>
                    </div>
                </div>

                <div class="dv1 content">
                    <div class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=E" style="text-decoration: underline; color: #fff;">火星账户</a></div>
                    <div class="dv3">
                        <asp:Label ID="lblPointE" runat="server" Text="0.00"></asp:Label>
                    </div>
                    <div style="margin-left: 25px; font-size: 12px; margin-top: 0px; color: #797979;">
                        <asp:Label ID="Label7" Style="color: #797979; font-size: 12px" runat="server" Text="0.00"></asp:Label>
                    </div>
                </div>


            </div>
            <div style="margin-left: 10%; margin-top: 40px;">
                <h4 style="color: #797979">功能</h4>
            </div>
            <div style="width: 90%; height: 70px; margin-top: 20px; margin-left: 5%;">
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #fff;" href="../MemberMobile/ReCast.aspx">
                    <img src="img/矿业.png" width="32" height="32" /><br />
                    矿机
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #fff;" href="">
                    <img src="img/星球.png" width="32" height="32" /><br />
                    矿池
                      
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #fff;" href="../MemberMobile/SST_TJ.aspx">
                    <img src="img/团队.png" width="32" height="32" /><br />
                    团队
                      
                </a>
                <a style="float: left; width: 24%; height: 55px; text-align: center; font-size: 14px; margin-top: 3px; color: #fff;" href="PlanteDH.aspx">
                    <img src="img/交易.png" width="32" height="32" /><br />
                    抢购
                </a>
            </div>
            <div style="box-shadow: #e8e5e5 0px 0px 10px; width: 100%; height: 70px; background-color: #f9f9ff; margin-top: 10px; display: none">
                <div style="margin-left: 10px; font-size: 18px; padding-top: 23px; font-weight: bold;"><a href="../MemberMobile/AccountDetail.aspx?type=AccountXJ" style="text-decoration: underline; color: #666;">USDT账户</a></div>
                <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">钱包里可用的FTC</div>--%>
                <div style="margin-right: 10px; float: right; font-size: 18px; margin-top: -27px; width: 60%; text-align: right;">
                    <asp:Label ID="Jackpot" runat="server" Text="0.00"></asp:Label>
                </div>
                <div style="margin-right: 10px; float: right; font-size: 12px; margin-top: 0px; color: #797979;"></div>
            </div>

            <div style="box-shadow: #e8e5e5 0px 0px 10px; width: 100%; height: 70px; background-color: #07090e; margin-top: 10px; display: none">
                <div style="margin-left: 10px; font-size: 18px; padding-top: 23px; font-weight: bold;"><a href="../MemberMobile/AccountDetail.aspx?type=AccountFXth" style="text-decoration: underline; color: #fff;">團隊業績</a></div>
                <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">整个团队的业绩汇总</div>--%>
                <div style="margin-right: 10px; float: right; font-size: 18px; margin-top: -27px; width: 60%; text-align: right;">
                    <asp:Label ID="labCurrentOneMark" Style="color: #dd4814; font-size: 18px" runat="server" Text="0.00"></asp:Label>
                </div>
                <div style="margin-right: 10px; float: right; font-size: 12px; margin-top: 0px; color: #fff;">USDT</div>
            </div>

            <div style="margin-left: 10%; margin-top: 40px;">
                <h4 style="color: #797979">最新价格</h4>
            </div>
            <div id="getshow" runat="server" class="pricelist">
                <ul>
                    <li>
                        <div class='ltimg'>
                            <img src='img/tub.png' alt='XA' />
                        </div>
                        <div class='dsc1'>
                            <p class='p1'>Saturn(土星)</p>
                            <p class='p3'>0.5%</p>
                        </div>
                        <div class='dsc2'>
                            <p class='p1'>&nbsp;</p>
                            <asp:Label ID="Label8" Style="color: #797979; font-size: 12px" runat="server" Text="0.10"></asp:Label>
                        </div>
                    </li>
                    <li>
                        <div class='ltimg'>
                            <img src='img/jb.png' alt='XB' />
                        </div>
                        <div class='dsc1'>
                            <p class='p1'>Venus(金星)</p>
                            <p class='p3'>0.5%</p>
                        </div>
                        <div class='dsc2'>
                            <p class='p1'>&nbsp;</p>
                            <p class='p2'>$0.1</p>
                        </div>
                    </li>
                    <li>
                        <div class='ltimg'>
                            <img src='img/shuib.png' alt='XC' />
                        </div>
                        <div class='dsc1'>
                            <p class='p1'>Marcury(水星)</p>
                            <p class='p3'>0.5%</p>
                        </div>
                        <div class='dsc2'>
                            <p class='p1'>&nbsp;</p>
                            <p class='p2'>$0.1</p>
                        </div>
                    </li>
                    <li>
                        <div class='ltimg'>
                            <img src='img/mb.png' alt='XD' />
                        </div>
                        <div class='dsc1'>
                            <p class='p1'>Jupiter(木星)</p>
                            <p class='p3'>0.5%</p>
                        </div>
                        <div class='dsc2'>
                            <p class='p1'>&nbsp;</p>
                            <p class='p2'>$0.1</p>
                        </div>
                    </li>
                    <li>
                        <div class='ltimg'>
                            <img src='img/huob.png' alt='XE' />
                        </div>
                        <div class='dsc1'>
                            <p class='p1'>Mars(火星)</p>
                            <p class='p3'>0.5%</p>
                        </div>
                        <div class='dsc2'>
                            <p class='p1'>&nbsp;</p>
                            <p class='p2'>$1</p>
                        </div>
                    </li>

                </ul>
            </div>

        </div>




        <script type="text/javascript">
            (function (select) {
                //小左边滑动
                var startX, moveX, movebox = document.querySelector(select);
                //触摸开始
                function boxTouchStart(e) {
                    var touch = e.touches[0]; //获取触摸对象
                    startX = touch.pageX; //获取触摸坐标
                }
                //触摸移动
                function boxTouchMove(e) {
                    var touch = e.touches[0];
                    moveX = touch.pageX - startX; //手指水平方向移动的距离
                }
                //触摸结束
                function boxTouchEnd(e) {
                    moveDir = moveX > 0 ? true : false; //滑动方向大于0表示向左滑动，小于0表示向右滑动
                    //手指向左滑动
                    if (moveDir) {
                        var index = $(this).find("div.active").index();
                        //第一个是1的时候
                        if (index == 0) {
                            $(this).find("div.active").removeClass("active");
                            $(this).children(":last").addClass("active");
                        } else {
                            var last = $(this).find("div.active");
                            last.removeClass("active").prev().addClass("active");
                        }

                        //手指向右滑动
                    } else {
                        var index = $(this).find("div.active").index();
                        //第一个是4的时候
                        if (index == 5) {
                            $(this).find("div.active").removeClass("active");
                            $(this).children(":first").addClass("active");
                        } else {
                            var last = $(this).find("div.active");
                            var _this = $(this);
                            last.removeClass("active").next().addClass("active");

                        }
                    }
                }

                //滑动对象事件绑定
                movebox.addEventListener("touchstart", boxTouchStart, false);
                movebox.addEventListener("touchmove", boxTouchMove, false);
                movebox.addEventListener("touchend", boxTouchEnd, false);

            })("#box")

        </script>


        <script type="text/javascript">
        function alert(data, id) {
            
                var x = document.getElementById("p");
                x.innerHTML = data;
                $('#myModall').modal('show');
                if (id == "0") {
                    document.getElementById("gb").style.display = "block";
            }
        }
      
        
var  bs=0;
    $(function(){
     var bd = '<%=b6%>';
        if( bd=="1")
        {
            setTimeout(function () { alert('<%=b5%>','<%=b6%>');},500);
        }


              bs=AjaxClass.ChekisSignIn().value;
 
        if(bs>0)
        {
        $(".signin").show();
         } 



        });



 function showcu() {

        var  m=AjaxClass.GetSignIn().value;
if(m>0){
          $("#kaiqi").html("<p>领取矿币</p><p>"+bs+" </p> ");

          $("#kaiqi").removeClass("gaiqi"); $("#kaiqi").addClass("gaiqi1");
           

          $(".signin").fadeOut(3000);
 }          
      }



        
        </script>


        <!-- #include file = "comcode.html" -->




        <%-- <div id="gl" style="opacity:0.5; background-color:#666; width:110%; height:600px; z-index:100000; text-align:center; line-height:650px; color:#fff; font-size:30px;position:absolute;top:0px; left:0px; "> 敬请期待... </div> --%>
    </form>

</body>
</html>



