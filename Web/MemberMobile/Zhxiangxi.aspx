<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Zhxiangxi.aspx.cs" Inherits="MemberMobile_Zhxiangxi" %>
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
    <title><%=GetTran("008140","账户明细") %></title>
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
            /*padding: 50px 2% 60px;*/
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
        width:25%;
        font-size:16px;
        margin-top:5%;
            margin-left: 2%;
        }
        .mx3 {
        float:right;
        width:70%;
        font-size:16px;
        text-align:right;
         margin-top:5%;
             margin-right: 2%;
        }
       
    </style>
</head>

<body>
    <form id="form2" runat="server">
        
        <div class="middle" >

            <div class="minMsg minMsg2" style="display: block">
                <div class="minMsgBox"> 
                    <div>
                        <ol>
                            <asp:Repeater ID="rep_km" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="mx">
                                          <div class="mx1"><i class="glyphicon glyphicon-user blue"></i><%=nic %></div>
                                            <div class="mx1" style="color:red;"><%=jine %>
                                                <br />
                                                <span style="font-size:14px;color:hsl(0, 0%, 61%);line-height: 28px;float: initial;background: #fff;" >交易成功</span>
                                            </div>
                                            <div class="mx2"> <%=GetTran("007276","结余金额")%></div><div class="mx3"><%# (Math.Abs((decimal.Parse(Eval("BalanceMoney").ToString())))).ToString("0.00") %></div>
                                            <div class="mx2">科目</div><div class="mx3"><%#BLL.Logistics.D_AccountBLL.GetKmtype(Eval("kmtype").ToString()) %></div>
                                            
                                           <div class="mx2"><%=GetTran("006581","发生时间")%></div><div class="mx3"><%# DateTime.Parse(Eval("happentime").ToString()).AddHours(8)%></div>
                                            
                                           <div class="mx2"><%=GetTran("006616","摘要")%></div><div class="mx3" ><%#getMark(Eval("remark").ToString())%></div> 
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

