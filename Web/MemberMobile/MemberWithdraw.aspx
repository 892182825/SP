<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberWithdraw.aspx.cs" Inherits="Member_MemberWithdraw" %>


<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>
<%--<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>--%>

<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title><%=GetTran("009006", "提现申请浏览")%></title>
    <link rel="stylesheet" href="CSS/style.css">
    <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
    <script src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            a.dianji();
        })
        var a = {
            dianji: function () {
                $(".DD").on("click", function () {
                    location.href = "/MemberDateUpdatePhone/Index?";
                })
            },
        }

    </script>
        <script type="text/jscript">
        $(function () {
            $('#rdbtnType label').css('float', 'left');
            $('#rtbiszf').css({ 'width': '19%', 'margin-left': '5px' });
            $('#rdbtnType input').css({ 'width': '5%', border: 0, 'margin': '5px' });
            $('#Pager1_pageTable').css('margin-right', '0px');
            $('#Pager1_btn_submit').css('Height', '22px');
            $('#Pager1_btn_submit').css('width', '50px');
            $('#Pager1_pageTable').css('color', '#333');
            $('#Pager1_ShowTable').css('color', '#333');
        })
    </script>
    <style>
        

      
        .fiveSquareBox {
            margin-bottom:4px;
        }
        input[type=checkbox] {
            float: right;
    width: auto;
    margin-right: 45px;
        }
        .proLayerLine ul li {
            overflow:hidden;
            width:50%;
            float:left;
            line-height:28px;
        }
        .proLayerLine ul {
            overflow:hidden
        }
            
    </style>
    <script type="text/javascript" >
        $(function () {
            //选择不同语言是将要改的样式放到此处
            var lang = $("#lang").text();
            // alert(1);
            if (lang != "L001") {

                $("#rq").width("100%");
            }

        })
       
 </script> 
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
 
    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">	    申请提现</span>
            </div>
        </div>
        <div style="overflow: hidden">
            <div id="qq2" class="fiveSquareBox clearfix searchFactor">
                <span  style=" overflow: hidden">
                    <span id="rq" style="float: left"><%=GetTran("006986", "提现时间")%>：</span>
                    <asp:TextBox ID="beginTime" CssClass="Wdate"
                        runat="server" onfocus="WdatePicker()" Style="width: 31%; margin-top: 4px"></asp:TextBox>
                    <span style="float: left; width: 9%; text-align: center"><%=GetTran("000205","到") %></span>
                     <asp:TextBox ID="endTime" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                            Style="width: 31%; margin-top: 4px"></asp:TextBox>
                </span>
              <asp:Button ID="Button2" runat="server" Height="35px" Width="93%" style="height: 45px;width:93%;margin-top: 25px;border-radius: 5px;margin-left: 12px;font-size: 20px;font-weight: 600;background-color: #0057c8;color: #FFF;border: 1px solid #5da1fa;background-image: linear-gradient(#54b4eb, #2fa4e7 60%, #1d9ce5);text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="查 询" OnClick="Button2_Click" CssClass="anyes" />
            </div>
        </div>
        <div class="middle">

            <div class="minMsg minMsg2" style="display: block">

                <div class="minMsgBox">
                    <div class="dianji">
                        <ol>
                            <asp:Repeater ID="rep_TransferList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div>
                                            <a >
                                                <%-- <a href='<%# Eval("id","TixianXx.aspx?id={0}") %>'>--%>
                                                <%=GetTran("000000", "提现到")%>：<%# Convert.ToInt32(Eval("wyj"))==1?"钱包":"商城" %>
                                                <label style="color: #e06f00;"><%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%>
                                       <%# Math.Round((Convert.ToDouble(  GetWithdrawMoney(DataBinder.Eval(Container.DataItem, "WithdrawMoney").ToString()))*
                        ( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))
                        )).ToString("#0.00") %>

                                                </label>
                                        </div>
                                        <p>
                                          <%-- <%# GetWithdrawTime(DataBinder.Eval(Container.DataItem, "WithdrawTime").ToString())%>--%>
                                         <%# DateTime.Parse(Eval("WithdrawTime").ToString()).AddHours(8)%>
                                            <label><%# GetWState(DataBinder.Eval(Container.DataItem,"hkid").ToString(), DataBinder.Eval(Container.DataItem,"shenhestate").ToString(), DataBinder.Eval(Container.DataItem,"isAuditing").ToString()) %> </label>
                                        </p>
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
     <!-- #include file = "comcode.html" -->

        <script type="text/javascript">
            $(function () {
                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

                })

            })
        </script>
               <uc1:ucPagerMb ID="ucPagerMb1" runat="server" />
    </form>
</body>
</html>

