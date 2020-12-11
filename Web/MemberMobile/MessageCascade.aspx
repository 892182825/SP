<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageCascade.aspx.cs" Inherits="Member_MessageCascade" %>


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
    <title>   <%=GetTran("009076", "邮件查询")%></title>
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

    <script type="text/jscript">
        $(function () {
            $('#DetailSpan span').css('float', 'left');
        })

    </script>
    <style>
        body {
            padding: 50px 2% 60px;
        }

       
       
    </style>
    <script type="text/javascript">
        $(function () {
            var lang = $('#lang').text();
            if (lang!="L001") {
                $('.content').css({"margin-left": "110px","margin-top": "-20px"})
            }
        })
    </script>
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form2" runat="server">
        <div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>
<%=GetTran("009076", "邮件查询")%>
 
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
                                            <%=GetTran("9110", "邮件内容")%>：
                                          <div class="content" style="overflow: auto; padding-left: 5px; margin-left: 67px; margin-top: -18px;">
                                              <span id="DetailSpan" style="float: left; margin-right: 5px; padding: 0; border-radius: 2px; font-size: 14px; background: #fff;color:#000000">
                                                  <%#Eval("Content")%>
                                              </span>
                                          </div>
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





