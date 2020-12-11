<%@ Page Language="C#" AutoEventWireup="true" CodeFile="memberorder.aspx.cs" Inherits="MemberMobile_memberorder" %>

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
    <title><%=GetTran("000000", "订单浏览")%></title>
    <link rel="stylesheet" href="CSS/style.css">
    <link href="CSS/core.css" rel="stylesheet" />
    <link rel="stylesheet" href="CSS/home.css"
    <link href="CSS/icon.css" rel="stylesheet" />
    <%--<link href="hycss/serviceOrganiz.css" rel="stylesheet" />--%>
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


        function shouhuo(ordid) {
            var rr = AjaxClass.shuohsuo(ordid).value;
            if (rr == 'True') { alert('确认收货成功'); } else { alert('确认收货失败'); }
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
         .middle {
              margin-bottom:50px;
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
                width: 25%;
                text-align: center;
                border-bottom: 1px solid #ccc;
            }

            .middle .sctt .cur
            {
                border-bottom: 2px solid rgb(255, 106, 0);
            }

        /*.minMsg ul
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
            }*/
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
            
    </style>
    <script type="text/javascript" >
        $(function () {
            //选择不同语言是将要改的样式放到此处
            var lang = $("#lang").text();
            // alert(1);
            if (lang != "L001") {
                $('.rq').width("100%");
            }
        });
 </script>  
</head>

<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>

    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">订单浏览</span>
            </div>
        </div>
        
        <div class="middle" style="margin-bottom: 100px;">
             <input type="hidden" id="hdst" value="-1" />
            <ul class="sctt">
                <li class="cur" atr="-1">全部</li>
                <li atr="0">待付款</li>
                <li atr="N">待发货</li>
                <li atr="Y">待收货</li>
            </ul>
            <div class="minMsg minMsg2" style="display: block">

                <div class="minMsgBox">
                    <div class="dianji">
                   <div id="div1" class="minMsg minMsg2" style="display: block">

                <div class="tab-panel">
				<div class="tab-panel-item tab-active" >
                    <ul id="mblist" style="margin-bottom:40px;">
                         
                    </ul>

                </div>
                    </div>
            </div>
                    </div>
                </div>
            </div>
        </div>
        
          <script>
              var cupindex = 1;
              $(function () {
                 
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
                

                var res = AjaxClass.MemberDetailOrders($("#hdst").val(), cupindex).value;
                if (res != "") {
                    if (cupindex == 1) $("#mblist").html(res);
                    else {
                        $("#mom").remove(); $(res).appendTo("#mblist");
                    }
                    $("<div id='mom' class='more' style='position: absolute;' onclick='getNext();' >加载更多..</div>").appendTo("#mblist"); cupindex += 1;
                } else {
                    if (cupindex == 1) $("#mblist").html(res);
                    $("#mom").remove();
                    $("#end").remove();
                    $("<div id='end' class='end'  style='position: absolute;'>没有更多了~</div>").appendTo("#mblist");
                    
                }


            }
    </script>
      <!-- #include file = "comcode.html" -->

        
            
    </form>
</body>
</html>