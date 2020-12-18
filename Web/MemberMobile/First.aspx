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
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <style>
        .dv1 {box-shadow:#e8e5e5 0px 0px 10px;width:100%;height:70px;background-color:#f9f9ff;margin-top:10px
        }
        .dv2 {margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;
        }
        .dv3 {
            margin-right:10px;float:right;font-size:18px;margin-top:-27px;width:60%;text-align:right;
        }

    </style>
 
  
   
</head>
<body style="height:100%">
    <form id="form2" runat="server">
    <div style="width:100%;height:100%;margin:0 0 0 0;background-color:#fff">
              <div style="height:150px;background-color:#0057c8;color:#fff;background-image: linear-gradient(to bottom right, #0057c8 , #000310);">
                  <div style="height:30px;text-align:center;font-size:20px;padding-top:10px">我的钱包</div>
              </div>
              <div style="position:absolute;margin-top:-75px;background-image:url(img/jb-1.png);width:90%;height:150px;margin-left:5%;color:#fff;background-repeat: no-repeat;background-size: 100% 100%;-moz-background-size: 100% 100%;">
                  <div style="margin-left:25px;float:left;font-size:14px;margin-top:25px;">总资产（FTC）</div>
                  <div style="margin-left: -82px;float:left;font-size:24px;margin-top: 55px;width:70%"><asp:Label ID="lblBonse" runat="server" Text=""></asp:Label></div>
                  <div style="margin-left: -248px;float:left;font-size:14px;margin-top: 108px;">最新价格：  <asp:Label ID="lblPay" runat="server" Text=""></asp:Label>
                    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
  <!--GridView控件在后台进行绑定--->
 
  <!--定时器每5秒钟刷新一次UpdatePanel中的数据-->
  <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick" >
  </asp:Timer>
  </ContentTemplate>
  <Triggers>
  <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"></asp:AsyncPostBackTrigger>
  </Triggers>
  </asp:UpdatePanel>--%>

                  </div>
                  
                  <a style="margin-right:10px;float:right;font-size:14px;margin-top:55px;color:#fff" href=""><asp:Label ID="IRate" runat="server" Text="0.00" ></asp:Label><br />
                      释放</a>
                  <div style="margin-right: 10px;float:right;font-size:12px;margin-top: 22px;width:50%;text-align: right;display:none;" >奖金账户： <asp:Label ID="pointAIn" runat="server" Text="0.00"></asp:Label></div>
              </div>

        <div style="width: 90%;height:70px;margin-top: 96px;margin-left: 5%;">
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/MemberCashXF.aspx">
                      <img src="images/cz.png" width="32" height="32"  /><br />充值
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/MemberCash.aspx">
                      <img src="images/tx.png" width="32" height="32"  /><br />提现
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/MoneyManage.aspx">
                      <img src="images/zhuanzhk.png" width="32" height="32"  /><br />转账
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/ReCast.aspx">
                      <img src="images/bzsz.png" width="32" height="32"  /><br />锁仓
                  </a>
              </div>
         <div style="width: 90%;height:70px;margin-top: 15px;margin-left: 5%;">
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/SST_TJ.aspx">
                      <img src="images/fenxwlt.png" width="32" height="32"  /><br />网络
                  </a>
                  <%--<a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/ShopingList.aspx?type=new">--%>
             <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="http://wap.xphmalls.com/WapShop">
                      <img src="images/sc.png" width="32" height="32"  /><br />商城
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="">
                      <img src="images/yl.png" width="32" height="32"  /><br />娱乐
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/SHJF.aspx">
                      <img src="images/sh.png" width="32" height="32"  /><br />生活
                  </a>
              </div>

              <div style="width:90%;height:300px;margin-top:20px;margin-left:5%">
                  <%--<div style="width:100%;height:40px;">
                      <div style="float:left;font-size:20px;font-weight:bold;">账户总览</div>
                      <div style="float:right;color:#fe3f10;text-align:right">会员级别：<br /></div>
                  </div>--%>
                  <div  class="dv1">
                      <div  class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=AccountXJ" style="text-decoration: underline;color: #666;" >会员级别</a></div>
                      <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">钱包里可用的FTC</div>--%>
                      <div  class="dv3"><asp:Label ID="Label1" style="font-size:18px" runat="server" Text="0.00"></asp:Label><asp:Label ID="Label2" style="font-size:20px" runat="server" Text=""></asp:Label></div>
                      <div style="margin-right:10px;float:right;font-size:12px;margin-top:0px;color:#797979;"><asp:Label ID="mobil" style="color:#797979;font-size:12px" runat="server" Text="0.00"></asp:Label></div>
                  </div>
                  <div style="box-shadow:#e8e5e5 0px 0px 10px;width:100%;height:70px;background-color:#f9f9ff;margin-top:10px">
                      <div style="margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;"><a href="../MemberMobile/AccountDetail.aspx?type=AccountXJ" style="text-decoration: underline;color: #666;" >USDT账户</a></div>
                      <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">钱包里可用的FTC</div>--%>
                      <div style="margin-right:10px;float:right;font-size:18px;margin-top:-27px;width:60%;text-align:right;"><asp:Label ID="Jackpot" runat="server" Text="0.00"></asp:Label></div>
                      <div style="margin-right:10px;float:right;font-size:12px;margin-top:0px;color:#797979;"> </div>
                  </div>
       <div  class="dv1">   <div  class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=A" style="text-decoration: underline;color: #666;" >A币账户</a></div> 
                      <div  class="dv3"><asp:Label ID="lblPointA" runat="server" Text="0.00"></asp:Label></div> 
        </div>

                   <div  class="dv1">   <div  class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=B" style="text-decoration: underline;color: #666;" >B币账户</a></div> 
                      <div  class="dv3"><asp:Label ID="lblPointB" runat="server" Text="0.00"></asp:Label></div> 
        </div>

                   <div  class="dv1">   <div  class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=C" style="text-decoration: underline;color: #666;" >C币账户</a></div> 
                      <div  class="dv3"><asp:Label ID="lblPointC" runat="server" Text="0.00"></asp:Label></div> 
        </div>

                   <div  class="dv1">   <div  class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=D" style="text-decoration: underline;color: #666;" >D币账户</a></div> 
                      <div  class="dv3"><asp:Label ID="lblPointD" runat="server" Text="0.00"></asp:Label></div> 
        </div>

                   <div  class="dv1">   <div  class="dv2"><a href="../MemberMobile/AccountDetail.aspx?type=E" style="text-decoration: underline;color: #666;" >E币账户</a></div> 
                      <div  class="dv3"><asp:Label ID="lblPointE" runat="server" Text="0.00"></asp:Label></div> 
        </div>

                   
                   
                  <div style="box-shadow: #e8e5e5 0px 0px 10px;width:100%;height:70px;background-color:#f9f9ff;margin-top:10px">
                      <div style="margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;"><a href="../MemberMobile/AccountDetail.aspx?type=AccountFXth" style="text-decoration: underline;color: #666;" >团队业绩</a></div>
                      <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">整个团队的业绩汇总</div>--%>
                      <div style="margin-right:10px;float:right;font-size:18px;margin-top:-27px;width:60%;text-align:right;"><asp:Label ID="labCurrentOneMark" style="color:#dd4814;font-size:18px" runat="server" Text="0.00"></asp:Label> </div>
                      <div style="margin-right:10px;float:right;font-size:12px;margin-top:0px;color:#797979;">USDT</div>
                  </div>
              </div>
              <div style="width:100%;height:50px;background-color:#5b9ffa;margin-bottom:0px;display:none">
                  <a style="float:left;width:24%;height:55px;text-align:center;color:#ffffff;font-size:14px;margin-top:3px" href="">
                      <img src="images/sc.png" width="25" height="25"  />
                      <br />
                      商城
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;color:#ffffff;font-size:14px;margin-top:3px" href="">
                      <img src="images/yl.png" width="25" height="25"  />
                      <br />
                      娱乐
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;color:#ffffff;font-size:14px;margin-top:3px" href="../MemberMobile/SHJF.aspx">
                      <img src="img/sc.png" width="25" height="25"  />
                      <br />
                      生活
                      
                  </a>
                  <div style="float:left;width:24%;height:55px;text-align:center;color:#ffffff;font-size:14px;margin-top:13px">
                      
                      <br />
                      释放
                  </div>
              </div>
        


          </div>
        
           
       
        
      
    

    <script>
        function alert(data, id) {
            
                var x = document.getElementById("p");
                x.innerHTML = data;
                $('#myModall').modal('show');
                if (id == "0") {
                    document.getElementById("gb").style.display = "block";
            }
        }
        var bd = '<%=b6%>';
        if( bd=="1")
        {
            setTimeout(function () { alert('<%=b5%>','<%=b6%>');},500);
        }
        
        
    </script>
    <!-- #include file = "comcode.html" -->
   

    
   
<%-- <div id="gl" style="opacity:0.5; background-color:#666; width:110%; height:600px; z-index:100000; text-align:center; line-height:650px; color:#fff; font-size:30px;position:absolute;top:0px; left:0px; "> 敬请期待... </div> --%>

</form>
    
</body>
</html>



