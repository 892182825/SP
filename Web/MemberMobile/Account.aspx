<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="MemberMobile_Account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="css/style.css" />
     <%--<link  href="../css/bootstrap-united.min.css" rel="stylesheet">
    <link href="../css/charisma-app.css" rel="stylesheet">--%>
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <title></title>
    <style>
        .h_bottom .xs_xx
        {
            line-height: 16px;
               margin: 0px 0px -1px -1px;
    
    
        height: 90px;
        }
        .xs_xx p:nth-of-type(1) {
        
           margin-left: 10px;
        }
        .xs_xx p:nth-of-type(2) {
        margin-left: 10px;
               font-size: 20px;
        }
        .xs_xx {
        width:50%;
        }
        .ds {
        width:100%;
        height:90px;
        color:#fff;
        }
        .ds1 {
        width:45%;
        margin-top:15px;

        float:left
        }
            .ds1 p {
            
            float:left;
                margin-bottom: 0px;
                    margin-top: 11px;
    margin-left: 10px;
            }
        .ds2 {
            width: 45%;
    margin-top: 14px;
    margin-right: 15px;
    float: right;
    font-size: 24px;
    text-align: right;
        }
        .divRecord {
      width:100%;
       height:40px;
      background: #fff;
    margin-bottom: 10px;

            line-height: 40px; 

        }
 .divRecord span {
            margin-left: 10px;
            font-size:13px;
            color:#000;
            }

        .ds3 {
        width:100%;
        height:40px;
            background: #fff;
    margin-bottom: 10px;
        line-height: 40px;
        }
            .ds3 span {
            margin-left: 10px;
            font-size:13px;
            color:#000;
            }

        .ds4 a {
        color: #000; 
        }
        .ds5 {
        width:47%;
        height:140px;
        margin-left: 10px;
        text-align:center;
        float:left;
            margin-top: 15px;
        }
        .ds55 {
            width:100px;
            height:100px;
        border-radius:50%;
        border:2px solid;
        margin-left:20%;
            border-right-width: 0px;
                border-left-width: 0px;
            color: #359ff4;
        }
        .ds6 {
            float:right;
            height:150px;
        width:47%;
        text-align:center;
            margin-top: 15px;
        }
        .ds7 {
        width: 100%;
    margin-top: 15px;
    border-top-color: #dad6d6;
    border-top-style: solid;
    border-top-width: 1px;
    float: left;
    padding:10px;
    height: 100px;
        }
        .ds71 {
        color:#000;
        font-weight: 600;
        width:45%;
        float:left;
          }
        
        .ds72 {
        font-size: 24px;
    color: #2196F3;
    margin-top: 10px;
    margin-left: 10px;
    width:45%;
        float:left;
        }
        .ds8 {
        width:50%;
        float:right;
        text-align:right;
            border-left-color: #dad6d6;
    border-left-style: solid;
    border-left-width: 1px;
        margin-top: 20px;
        }
        .row {
        width:100%;
        padding:0;
        margin:0;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <div style="background-color:#e6e5e5; height: max-content;">
        <div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:30%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">我的账户	</span>
            </div>  
              </div>
    <div class="h_bottom" style="height:90px;margin-top:0px; padding-top:0px;background: #0b95d4;" >
            <div class="ds"  >
                
                <div class="ds1"><i class="glyphicon glyphicon-user white" style="font-size: 40px;float:left;margin-top: 5px;margin-left: 20px;"></i><p style="margin-top: 0px;"><%=nic %></p><br /><p style="margin-top:7px;"><%=Session["Member"] %></p></div>
                <div class="ds2"><p style="font-size: 12px;color: #dde3e6;">总资产(元)</p><p><asp:Label ID="lblzzc" runat="server" Text="0.00"></asp:Label></p></div>
            </div>
            
        </div> 
        
          <div class="ds3" >
                <span>最新价:</span>
                    <a style="" >
                        <asp:Label ID="NowPrice" style="color:#dd4814;font-size:24px" runat="server" Text="0.0000"></asp:Label> 
                    </a>
                <a onclick="xt();"><span style="font-size:10px;margin-right: 20px;float: right;margin-right: 20px;float:right;"><i class="glyphicon glyphicon-signal" style="color:#dd4814"></i>  增长率 <asp:Label ID="Addrate" style="font-size:18px;color:#dd4814" runat="server" Text=""></asp:Label></span></a>
            </div>
        
         

        <div class="divRecord">
            <span>安置总网业绩:</span>
            <a style="" >
               <asp:Label ID="labTotalNetRecord" style="color:#dd4814;font-size:24px" runat="server" Text="0.00"></asp:Label> 
            </a>
            
        </div>
        
        <div class="divRecord">
            <span>安置新网业绩:</span>
            
             <a style="" >
               <asp:Label ID="labCurrentTotalNetRecord" style="color:#dd4814;font-size:24px" runat="server" Text="0.00"></asp:Label> 
            </a>
        </div>
        <div class="divRecord">
            <span>个人新增业绩:</span>
            <a style="" >
               <asp:Label ID="labCurrentOneMark" style="color:#dd4814;font-size:24px" runat="server" Text="0.00"></asp:Label> 
            </a>
        </div>
      
         <div class="ds4" style=" background-color: #fff;    color: #607D8B;height: 455px;">
             <a  href="../MemberMobile/AccountDetail.aspx?type=AccountXJ" >
        <div class="ds5">
                <p>可用石斛积分账户</p>
                <div class="ds55">
                    
                        <asp:Label style="color: #dd4814;line-height: 95px;font-size: 20px;" ID="Jackpot" runat="server" Text="0.00"></asp:Label>
                    
                </div>
            </div>
            </a>
             <a  href="../MemberMobile/AccountDetail.aspx?type=AccountXF">
        <div class="ds6">
                <p>消费账户</p>
                <div class="ds55">
                    
                        <asp:Label style="color: #dd4814;font-size: 20px;line-height: 95px;" ID="TotalRemittances" runat="server" Text="0.00"></asp:Label>
                    
                </div>单位：元
            </div>
            </a>
             <a  href="../MemberMobile/AccountDetail.aspx?type=AccountFX">
        <div class="ds7" style="height:40px;">
                <div class="ds71" style="margin-top: 5px;"><i class="glyphicon glyphicon-list-alt"></i> 注册积分账户</div>
            
                <div class="ds72" style="margin-top:0;">
                    
                        <asp:Label ID="fuxiaoin" runat="server" Text="0.00"></asp:Label>
                   
                </div>
            
            </div>
                  </a>
             <a  href="../MemberMobile/AccountDetail.aspx?type=AccountFZ">
        <div class="ds7">
                <div class="ds71"><i class="glyphicon glyphicon-list-alt"></i> 投资积分账户(冻结)</div>
            <div class="ds8">
                <p>释放速度</p>
                <p >
                    <div style=" color: #dd4814;font-size: 18px;">
                        <asp:Label ID="IRate" runat="server" Text="0.00"></asp:Label>
                    </div>
                </p>
            </div>
                <div class="ds72">
                    
                        <asp:Label ID="InvestIn" runat="server" Text="0.00"></asp:Label>
                   
                </div>
            
            </div>
                  </a>
        
             <a href="../MemberMobile/AccountDetail.aspx?type=AccountFXth">
        <div class="ds7">
                <div class="ds71"><i class="glyphicon glyphicon-list-alt"></i> 奖励积分账户(冻结)</div>

            <div class="ds8">
                <p>释放速度</p>
                <p>
                    <a style=" color: #dd4814;font-size: 18px;" >
                        <asp:Label ID="ARate" runat="server" Text="0.00"></asp:Label>
                    </a>
                </p>
            </div>
                <div class="ds72">
                    
                        <asp:Label ID="AwardIn" runat="server" Text="0.00"></asp:Label>
                  
                </div>
            </div>
          </a>
        
           
        </div>
        
        <div class="row" style=" margin-bottom: 40px; margin-left: 0;">
             <div class="box col-md-4" style=" padding: 0; background-color: #fff;">
        <div class="box-inner">
            <div class="box-header well" data-original-title="">
                <h2><i class="glyphicon glyphicon-list-alt"></i> 账户金额占比图</h2>

                
            </div>
            <div class="box-content">
                <div id="donutchart" style="height: 300px;">
                </div>
            </div>
        </div>
    </div>
        </div><!--/row-->
         <div class="modal fade" id="myModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
             <div class="row" style="margin-left:0px;">
        <div class="box col-md-12" style="padding: 0; background-color: #fff;width:100%">
        <div class="box-inner">
            <div class="box-header well">
                <h2><i class="glyphicon glyphicon-list-alt"></i> 积分增长曲线</h2>

            </div>
            <div class="box-content">
                <div id="stackchart" class="center" style="height:280px;width:350px"></div>

                <%--<p class="stackControls center">
                    <input class="btn btn-default" type="button" value="With stacking">
                    <input class="btn btn-default" type="button" value="Without stacking">
                </p>

                <p class="graphControls center">
                    <input class="btn btn-primary" type="button" value="Bars">
                    <input class="btn btn-primary" type="button" value="Lines">
                    <input class="btn btn-primary" type="button" value="Lines with steps">
                </p>--%>
            </div>
        </div>
    </div></div><!--/row-->
             </div>
             
          <%=msg %>
        <%=msg1 %>
        <!-- chart libraries start -->
        <script src="../bower_components/flot/excanvas.min.js"></script>
<script src="../bower_components/flot/jquery.flot.js"></script>
<script src="../bower_components/flot/jquery.flot.pie.js"></script>
<script src="../bower_components/flot/jquery.flot.stack.js"></script>
<script src="../bower_components/flot/jquery.flot.resize.js"></script>
        <script src="../bower_components/flot/jquery.flot.time.js"></script>
        <!-- chart libraries end -->
        <script src="../JS/init-chart.js"></script>


        
    </div>
        
        


    
    </form>
     <!-- #include file = "comcode.html" -->

     <script>
         function xt() {
             $('#myModal3').modal('show');
         }
         $(function () {
             $(".glyphicon").removeClass("a_cur");
             $("#c3").addClass("a_cur");
         });
             </script>
</body>
</html>
