<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ddcy.aspx.cs" Inherits="MemberMobile_ddcy" %>



<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>
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
         .minMsgBox ol li span {
    margin-right: 5px;
    padding: 0px 5px;
    border-radius: 2px;
    color: #171717;
    font-size: 14px;
    text-align: left;
        background: #fff;
        width:45%;
        overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}
        .minMsgBox ol li {
            border-bottom: solid 1px #8e8e8e;
        }
    </style>
    <script type="text/javascript" >
        //选择不同语言是将要改的样式放到此处
        var lang = $("#lang").text();
        // alert(1);
        if (lang != "L001") {
            //alert("1111");

            //alert("MemberWithdraw");

        }

 </script> 
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
 
    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">公告查阅</span>
            </div>
        </div>
        <div style="overflow: hidden;display:none;">
            <div id="qq2" class="fiveSquareBox clearfix searchFactor">
                <span style="height: 38px; height: 30px; overflow: hidden">
                    <span style="float: left"> <%=GetTran("000720", "发布日期")%>：</span>
               <%--     <asp:TextBox ID="beginTime" CssClass="Wdate"
                        runat="server" onfocus="WdatePicker()" Style="width: 35%; margin-top: 4px"></asp:TextBox>--%>
                     <asp:TextBox ID="txtBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker()"  Style="width: 31%; margin-top: 4px"></asp:TextBox>
                    <span style="float: left; width: 9%; text-align: center"><%=GetTran("000205","到") %></span>
                     <asp:TextBox ID="txtEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                            Style="width: 31%; margin-top: 4px"></asp:TextBox>
                </span>
              <asp:Button ID="btnConfirm" runat="server" Height="27px" Width="100%" Style="margin-top: 1px; margin-left: -1px; padding: 4px 9px; background-color: #dd4814; color: #FFF; border: 1px solid #507E0C;  text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                            Text="查 询" OnClick="btnConfirm_Click" CssClass="anyes" />
            </div>
        </div>
        <div class="middle">

            <div class="minMsg minMsg2" style="display: block">

                <div class="minMsgBox">
                    <div class="dianji">
                        <ol>
                           <ul id="mblist">
                         
                            </ul>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <script>
            var cupindex = 1;
            $(function () {

                getNext();

            });
            function getNext() {


                var res = AjaxClass.ddcyDetail(cupindex).value;
                if (res != "") {
                    if (cupindex == 1) $("#mblist").html(res);
                    else {
                        $("#mom").remove(); $(res).appendTo("#mblist");
                    }
                    $("<div id='mom' class='more' style='position: absolute;' onclick='getNext();' >加载更多..</div>").appendTo("#mblist"); cupindex += 1;
                } else {
                    $("#mom").remove();
                    $("#end").remove();
                    $("<div id='end' class='end'  style='position: absolute;'>没有更多了~</div>").appendTo("#mblist");
                    // var tab = $("#mblist").html();

                }


            }
    </script>
      <!-- #include file = "comcode.html" -->

        <script type="text/javascript">
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
          
    </form>
</body>
</html>





