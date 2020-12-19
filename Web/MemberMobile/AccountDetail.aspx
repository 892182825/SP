<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountDetail.aspx.cs" EnableEventValidation="false" Inherits="AccountDetail_AccountDetail" %>

<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>
<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <%--<script src="js/jquery-1.7.1.min.js"></script>--%>
    <title><%=GetTran("008140","账户明细") %></title>
    <link rel="stylesheet" href="CSS/style.css">
    <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
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
        .searchFactor span select, .searchFactor span input {
            position:initial;
            margin-left:0;
        }
        #ucPagerMb1_txtPn {
            width:30px;margin:0 5px;
        } 

          .middle .sctt
        {
            height: 40px;
            line-height: 40px;
            width: 100%;
                background-color: #fff;
                margin-bottom: -5px;
        }

            .middle .sctt li
            {
                list-style: none;
                float: left;
                width: 33.33%;
                text-align: center;
                border-bottom: 1px solid #ccc;
            }

            .middle .sctt .cur
            {
                border-bottom: 2px solid rgb(255, 106, 0);
            }

        .minMsg ul
        {
            width: 100%;
        }

            .minMsg ul li
            {
                background-color: #fff;
                margin-top: 10px;
                border-top: 1px solid #eee;
                border-bottom: 1px solid #eee;
                height: 50px;
                line-height: 20px;
            }
        .dianji th {
            text-align: center;
            padding-left:0px;
        }
        .dianji tr {
        height:50px;
        }
        .out {
    border-top: 40px #D6D3D6 solid;
    width: 0px;
    height: 0px;
    border-left: 80px #BDBABD solid;
    position: relative;
}
        .auto-style1 {
            width: 3840px;
            height: 2160px;
        }
        .auto-style2 {
            width: 1920px;
            height: 1080px;
        }
        .auto-style3 {
            width: 2560px;
            height: 1440px;
        }
        .auto-style4 {
            width: 960px;
            height: 540px;
        }
    </style>
       <script type="text/javascript" >
           $(function () {
               //选择不同语言是将要改的样式放到此处
               var lang = $("#lang").text();
               // alert(1);
               if (lang != "L001") {
                   //alert("1111");

                   //alert("MemberWithdraw");
                   $('.rq').width('100%')
                   $('.rq_in').width('100%')

                   $('.rq_in input').width('auto').css('margin-left', '9%')
                   $('.rq_in input').eq(0).css('margin-left', '0')
               }
           });

         
           
           


 </script> 
    
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>

    <form id="form2" runat="server">
          
        
        <div class="middle" style="margin-bottom: 100px;">
            <input type="hidden" id="hdst" value="-1" />
            <ul class="sctt">
                <li class="cur" atr="-1">全部</li>
                <li atr="0">收入</li>
                <li atr="1">支出</li>
            </ul>
            <div class="minMsg minMsg2" style="display: block">

                <div class="minMsgBox">
                    <div class="dianji">
                         <table id="div1" class="table table-striped" style="width:100%;display: block;border-radius: 5px;font-size: 15px;margin-left: -3px;background-color:#fff">
                  <thead id="mbbt">
                    <tr>
                        <th style="width:29%;">时间</th>
                        <th style="width:29%;">科目 </th>
                        <th style="width: 180px;">
                            <div> 金额/余额 </div>
                        </th>
                        
                    </tr>

                </thead>
                        <tbody id="mblist"></tbody>
                        </table>
                        <asp:Repeater ID="rep_km" runat="server">
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>

        <script>
            var cupindex = 1;
            $(function () {
                var q1 = "<%=ttt %>";
                
                getNext();
                $(".sctt li").click(function () {
                    var ck = $(this).attr("atr");
                    $("#hdst").val(ck);
                    $(".sctt li").removeClass("cur");
                    $(this).addClass("cur");

                    cupindex = 1;
                    getNext();
                });

            });
            function getNext() {
                var q1 = "<%=ttt %>";

                var res = AjaxClass.AccountDetailOrders($("#hdst").val(), cupindex,q1).value;
                if (res != "") {
                    if (cupindex == 1) $("#mblist").html(res);
                    else {
                        $("#mom").remove(); $(res).appendTo("#mblist");
                    }
                    $("<div id='mom' class='more' style='position: absolute;' onclick='getNext();' >加载更多..</div>").appendTo("#mblist"); cupindex += 1;
                } else {
                    if (cupindex == 1) { $("#mblist").html(res); }
                    $("#mom").remove();
                    $("#end").remove();
                    $("<div id='end' class='end'  style='position: absolute;'>没有更多了~</div>").appendTo("#div1");
                    
                    
                }


            }
    </script>
      <!-- #include file = "comcode.html" -->
        <script type="text/javascript">
            $(function () {
                
                    $(".glyphicon").removeClass("a_cur");
                    $("#c3").addClass("a_cur");
                
          

                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();

                })

            })
        </script>
        
      
    </form>
</body>
</html>
