<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberCashListXF.aspx.cs" Inherits="MemberMobile_MemberCashList" %>

<%--<%@ Register Src="~/UserControl/MemberPager.ascx" TagName="MemberPager" TagPrefix="Uc1" %>--%>
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
    <title><%=GetTran("000000","申请充值浏览") %></title>
    <link rel="stylesheet" href="CSS/style.css">
   <%-- <link href="hycss/serviceOrganiz.css" rel="stylesheet" />--%>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    
    <style>
        body {
            /*padding: 50px 2% 60px;*/
        }

        
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
        .dianji th {
            text-align: center;
            padding-left:0px;
        }
        .dianji tr {
        height:50px;
        }
         .dianji td {
            text-align: center;
           
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
    </style>
   
</head>

<body>
    <b id="lang" style="display:none"></b>

    <form id="form2" runat="server">
        
        <div style="display: none;overflow:hidden">
            <div id="qq2" class="fiveSquareBox clearfix searchFactor">
                <asp:Button ID="btn_SeachList" runat="server" Height="35px" Width="97%" Style="margin-top: 1px; margin-left: -3px; padding: 4px 9px; background-color: #dd4814; color: #FFF; border-radius: 6px;  text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                    Text="查 询" OnClick="btn_SeachList_Click" CssClass="anyes" />
            </div>
        </div>
        <div class="middle">
            <input type="hidden" id="hdst" value="-1" />
           <%-- <ul class="sctt">
                <li class="cur" atr="-1"><%=GetTran("000633", "全部")%></li>
                <li atr="0"><%=GetTran("010047", "全部转入")%></li>
                <li atr="1"><%=GetTran("010048", "全部转出")%></li>
            </ul>--%>
            <div class="minMsg minMsg2" style="display: block">

                <div class="minMsgBox">
                    <div class="dianji">
                         <table id="div1" class="table table-striped" style="width:100%;display: block;border-radius: 5px;font-size: 16px;margin-left: -3px;background-color:#fff">
                  <thead id="mbbt">
                    <tr>
                        <th style="width:35%"><%=GetTran("001546", "时间")%></th>
                        <th style="width:35%"><%=GetTran("000322", "金额")%></th>
                        <th style="width:130px"><%=GetTran("000000", "状态")%></th>
                        
                    </tr>

                </thead>
                        <tbody id="mblist"></tbody>
                        </table>
                            <asp:Repeater ID="rep_TransferList" runat="server">
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
               
                getNext();
                

            });
            function getNext() {
                
                var res = AjaxClass.XFOrders(1, cupindex).value;
                if (res != "") {
                    if (cupindex == 1) $("#mblist").html(res);
                    else {
                        $("#mom").remove(); $(res).appendTo("#mblist");
                    }
                    $("<div id='mom' class='more' style='position: absolute;' onclick='getNext();' ><%=GetTran("010100", "加载更多..")%></div>").appendTo("#mblist"); cupindex += 1;
                } else {
                    if (cupindex == 1) { $("#mblist").html(res); }
                    $("#mom").remove();
                    $("#end").remove();
                    $("<div id='end' class='end'  style='position: absolute;'><%=GetTran("010101", "没有更多了")%>~</div>").appendTo("#div1");
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

            })
        </script>
    <%--    <Uc1:MemberPager ID="Pager" runat="server" />--%>
<%--         <uc1:ucPagerMb ID="ucPagerMb1" runat="server" />--%>
    </form>
</body>
</html>
 <script type="text/javascript" >
     $(function () {
         //选择不同语言是将要改的样式放到此处
         var lang = $('#lang').text();
         if (lang!= "L001") {
            // alert(1)
             $('#zr').width("100%");
             $('#zr1').width("60%");
             $('#s').css({ "width": "110%" });
             $('#out').css({ "width": "100%" });
             $('#sout').css({ "width": "100%" });
             $('#id1').css({ "width": "110%" });
         }
     })

 </script>  


