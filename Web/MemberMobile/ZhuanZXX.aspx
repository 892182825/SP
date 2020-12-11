<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZhuanZXX.aspx.cs" Inherits="MemberMobile_ZhuanZXX" %>

<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>
<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title><%=GetTran("007292","转账浏览") %></title>
    <link rel="stylesheet" href="CSS/style.css">
    <script type="text/javascript">
        $(function () {
            a.dianji();
        })
        var a = {
            dianji: function () {
                $("#ucPagerMb1").css('display', 'none');
            },
        }

    </script>
    <style>
        body {
            /*padding: 0px 2% 60px;*/
        }
        .mx {
        
        width:100%;
        }
        .mx1 {
        text-align: center;
        font-size:20px;
        margin-top:5%;
        }
            

        .mx2 {
        float:left;
        width:30%;
        font-size:16px;
        margin-top:5%;
            margin-left: 2%;
        }
        .mx3 {
        float:right;
        width:60%;
        font-size:16px;
        text-align:right;
         margin-top:5%;
             margin-right: 2%;
        }
    </style>
</head>

<body>
    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">转账明细</span>
            </div>
        </div>
        <div class="middle">

            <div class="minMsg minMsg2" style="display: block">
                <div class="minMsgBox"> 
                    <div>
                        <ol>
                            <asp:Repeater ID="rep_km" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="mx">
                                            <div class="mx1">FTC会员互转</div>
                                            <div class="mx1" style="color:red;"><%=jine %>
                                                <br />
                                                <span style="font-size:14px;color:hsl(0, 0%, 61%);line-height: 28px;float: initial;background: #fff;" >交易成功</span>
                                            </div>
                                           <div class="mx2"><%=huikuan%></div><div class="mx3"><%=hkje %></div>
                                            
                                          <%-- <div class="mx2"><%=GetTran("000000", "账户类型")%></div><div class="mx3">可用石斛积分账户</div>--%>
                                            
                                           <div class="mx2"><%=GetTran("000000", "账户类型")%></div><div class="mx3"><%#GetOutAccountType(int.Parse(Eval("OutAccountType").ToString())) %></div>
                                            
                                           <div class="mx2"><%=GetTran("007699", "转账期数")%></div><div class="mx3">第 <%#Eval("ExpectNum")%> 期</div>
                                            
                                          
                                            
                                            <div class="mx2"><%=GetTran("005891", "转账时间")%></div><div class="mx3"><%# DateTime.Parse(Eval("Date").ToString()).AddHours(8)%></div>
                                            
                                           <div class="mx2"><%=GetTran("000078","备注说明")%></div><div class="mx3" ><%#(Eval("remark").ToString())%><div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
   <!-- #include file = "comcode.html" -->

        <script>
            $(function () {
                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

                })

            })
        </script>
      <%--  <uc1:ucPagerMb ID="ucPagerMb1" runat="server" />--%>
    </form>
</body>
</html>


