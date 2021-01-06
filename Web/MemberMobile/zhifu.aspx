<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zhifu.aspx.cs" Inherits="MemberMobile_zhifu" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=0.5,maximum-scale=2.0,user-scalable=yes">
    <%--<meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">--%>
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <%--<script src="js/jquery-1.7.1.min.js"></script>--%>
    <title>账户明细</title>
    <link rel="stylesheet" href="CSS/style.css">
    <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
 
   
    
    
</head>

<body>
    <b id="lang" style="display:none"></b>

    <form id="form2" runat="server">
           <div style="width:100%;height:100%;margin:0 0 0 0;background-color:#fff">
              <div style="height:150px;background-color:#0057c8;color:#fff;background-image: linear-gradient(to bottom right, #0057c8 , #000310);">
                  <div style="height:30px;text-align:center;font-size:20px;padding-top:10px">我的钱包</div>
              </div>
              <div style="position:absolute;margin-top:-75px;background-image:url(img/jb-1.png);width:90%;height:150px;margin-left:5%;color:#fff;background-repeat: no-repeat;background-size: 100% 100%;-moz-background-size: 100% 100%;">
                  <div style="margin-left:25px;float:left;font-size:14px;margin-top:25px;">总资产（FTC）</div>
                  <div style="margin-left: -82px;float:left;font-size:24px;margin-top: 55px;width:70%"><asp:Label ID="lblBonse" runat="server" Text=""></asp:Label></div>
                  <div style="margin-left: -248px;float:left;font-size:14px;margin-top: 108px;">最新价格：  <asp:Label ID="lblPay" runat="server" Text=""></asp:Label>
                      <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
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

                <asp:Button ID="jrsc" runat="server" Text="进入商城" OnClick="jrsc_Click"  />

        <div style="width: 90%;height:70px;margin-top: 96px;margin-left: 5%;">
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/MemberCashXF.aspx">
                      <img src="images/cz.png" width="36" height="36"  />
                      <br />
                      充值
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/MemberCash.aspx">
                      <img src="images/tx.png" width="36" height="36"  />
                      <br />
                      提现
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/MoneyManage.aspx">
                      <img src="images/zhuanzhk.png" width="36" height="36"  />
                      <br />
                      转账
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/ReCast.aspx">
                      <img src="images/bzsz.png" width="36" height="36"  />
                      <br />
                      锁仓
                  </a>
              </div>
         <div style="width: 90%;height:70px;margin-top: 15px;margin-left: 5%;">
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/SST_TJ.aspx">
                      <img src="images/fenxwlt.png" width="36" height="36"  />
                      <br />
                      网络
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/ShopingList.aspx?type=new">
                      <img src="images/sc.png" width="36" height="36"  />
                      <br />
                      商城
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="">
                      <img src="images/yl.png" width="36" height="36"  />
                      <br />
                      娱乐
                      
                  </a>
                  <a style="float:left;width:24%;height:55px;text-align:center;font-size:14px;margin-top:3px;color: #666;" href="../MemberMobile/SHJF.aspx">
                      <img src="images/sh.png" width="36" height="36"  />
                      <br />
                      生活
                  </a>
              </div>

              <div style="width:90%;height:300px;margin-top:20px;margin-left:5%">
                  <%--<div style="width:100%;height:40px;">
                      <div style="float:left;font-size:20px;font-weight:bold;">账户总览</div>
                      <div style="float:right;color:#fe3f10;text-align:right">会员级别：<br /></div>
                  </div>--%>
                  <div style="box-shadow:#e8e5e5 0px 0px 10px;width:100%;height:70px;background-color:#f9f9ff;margin-top:10px">
                      <div style="margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;"><a href="../MemberMobile/AccountDetail.aspx?type=AccountXJ" style="text-decoration: underline;color: #666;" >会员级别</a></div>
                      <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">钱包里可用的FTC</div>--%>
                      <div style="margin-right:10px;float:right;font-size:18px;margin-top:-27px;width:60%;text-align:right;"><asp:Label ID="Label1" style="font-size:18px" runat="server" Text="0.00"></asp:Label><asp:Label ID="Label2" style="font-size:20px" runat="server" Text=""></asp:Label></div>
                      <div style="margin-right:10px;float:right;font-size:12px;margin-top:0px;color:#797979;"><asp:Label ID="mobil" style="color:#797979;font-size:12px" runat="server" Text="0.00"></asp:Label></div>
                  </div>
                  <div style="box-shadow:#e8e5e5 0px 0px 10px;width:100%;height:70px;background-color:#f9f9ff;margin-top:10px">
                      <div style="margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;"><a href="../MemberMobile/AccountDetail.aspx?type=AccountXJ" style="text-decoration: underline;color: #666;" >可用账户</a></div>
                      <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">钱包里可用的FTC</div>--%>
                      <div style="margin-right:10px;float:right;font-size:18px;margin-top:-27px;width:60%;text-align:right;"><asp:Label ID="Jackpot" runat="server" Text="0.00"></asp:Label></div>
                      <div style="margin-right:10px;float:right;font-size:12px;margin-top:0px;color:#797979;">FTC</div>
                  </div>
                  <div style="box-shadow:#e8e5e5 0px 0px 10px;width:100%;height:70px;background-color:#f9f9ff;margin-top:10px">
                      <div style="margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;"><a href="../MemberMobile/AccountDetail.aspx?type=AccountFX" style="text-decoration: underline;color: #666;" >锁仓账户</a></div>
                      <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">锁仓中的FTC</div>--%>
                      <div style="margin-right:10px;float:right;font-size:18px;margin-top:-27px;width:60%;text-align:right;"><asp:Label ID="fuxiaoin" runat="server" Text="0.00"></asp:Label></div>
                      <div style="margin-right:10px;float:right;font-size:12px;margin-top:0px;color:#797979;"><asp:Label ID="sfje" runat="server" Text="0.00"></asp:Label></div>
                  </div>
                  <div style="box-shadow: #e8e5e5 0px 0px 10px;width:100%;height:70px;background-color:#f9f9ff;margin-top:10px">
                      <div style="margin-left: 10px;font-size:18px;padding-top: 23px;font-weight:bold;"><a href="../MemberMobile/AccountDetail.aspx?type=AccountFXth" style="text-decoration: underline;color: #666;" >团队业绩</a></div>
                      <%--<div style="margin-left:10px;float:left;font-size:12px;margin-top:5px;color:#797979;">整个团队的业绩汇总</div>--%>
                      <div style="margin-right:10px;float:right;font-size:18px;margin-top:-27px;width:60%;text-align:right;"><asp:Label ID="labCurrentOneMark" style="color:#dd4814;font-size:18px" runat="server" Text="0.00"></asp:Label> </div>
                      <div style="margin-right:10px;float:right;font-size:12px;margin-top:0px;color:#797979;">USDT</div>
                  </div>
              </div>
           
       </div>
        
      
    </form>
</body>
</html>