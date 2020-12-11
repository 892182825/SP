<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ddcyxx.aspx.cs" Inherits="MemberMobile_ddcyxx" %>
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
    <title><%=GetTran("004028", "公告查阅")%></title>
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
       

     
        .minMsgBox ol li {
            overflow:hidden
        }
            .minMsgBox ol li span {
                background:none;
            }
            .minMsgBox ol li span {
        color:black;
        } 
    </style>
</head>

<body>
    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">公告查阅</span>
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
                                        <div>
                                          <%=GetTran("000912", "接收对象")%>：	<%# getloginRole(DataBinder.Eval(Container, "DataItem.loginRole").ToString()) %>
                                            <br />
                                            <br />
                                          <%=GetTran("000910", "接收编号")%>： <%#Eval("Receive").ToString()=="*" ? "全体成员" : Eval("Receive")%>
                                            <br />
                                            <br />
                                           <%=GetTran("000908", "发送编号")%>： <%#Eval("sender") %>
                                            <br />
                                            <br />
                                            <%=GetTran("000720", "发布日期")%>： <%# DateTime.Parse(Eval("Senddate").ToString()).AddHours(8)%>
                                             <br />
                                            <br />
                                          <%=GetTran("000825", "信息标题")%>： <%#Eval("infoTitle")%>
                                            <br />
                                            <br />
                                          <%=GetTran("9110", "邮件内容")%>：<div id="content" style="color:#333;overflow:hidden;"> <%#Eval("Content")%></div>
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

            });
            $(function () {
                $(".glyphicon").removeClass("a_cur");
                $("#c5").addClass("a_cur");
            });
        </script>
      <%--  <uc1:ucPagerMb ID="ucPagerMb1" runat="server" />--%>
    </form>
</body>
</html>



