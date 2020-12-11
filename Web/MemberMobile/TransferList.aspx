<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferList.aspx.cs" Inherits="Member_TransferList" %>

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
    <title><%=GetTran("007292","转账浏览") %></title>
    <link rel="stylesheet" href="CSS/style.css">
   <%-- <link href="hycss/serviceOrganiz.css" rel="stylesheet" />--%>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
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
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>

    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:30%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">转账浏览</span>
            </div>
              </div>
        <div style="display: none;overflow:hidden">
            <div id="qq2" class="fiveSquareBox clearfix searchFactor">
                <span id="zr" style="width: 50%;overflow:hidden;line-height:30px;float:left">
                    <span id="zr1" style="float:left"><%=GetTran("007251", "转入账户")%>：</span>
                   <span id="s"> <asp:DropDownList runat="server" ID="ddl_InAccount" Style="width: 50%;float:left;margin-top: 3%;">
                            <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                            <asp:ListItem Value="0" Text="会员现金账户"></asp:ListItem>
                            <asp:ListItem Value="1" Text="会员消费账户"></asp:ListItem>
                            <asp:ListItem Value="2" Text="店铺订货款"></asp:ListItem>
                        </asp:DropDownList> </span>
                </span>
                <span id="out" style="width: 50%;overflow:hidden;line-height:30px;float:left">
                    <span id="sout" style="float:left"><%=GetTran("007250", "转出账户")%>：</span>
                   <span id="id1"><asp:DropDownList runat="server" ID="ddl_OutAccount" Style="width: 50%;float:left;margin-top: 3%;">
                            <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                            <asp:ListItem Value="0" Text="会员现金账户"></asp:ListItem>
                            <asp:ListItem Value="1" Text="会员消费账户"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </span>
                <span style="width: 100%;line-height:30px;overflow:hidden">
                    <span style="float:left"><%=GetTran("002018", "转账金额")%>：</span>
                    <span style="float:left;width:77%;">

                       <asp:DropDownList ID="ddl_TransferMoney" runat="server" Style="width: 33%;float:left;margin-top: 2%;">
                            <asp:ListItem Value=">" Text="大于"></asp:ListItem>
                            <asp:ListItem Value="<" Text="小于"></asp:ListItem>
                            <asp:ListItem Value="=" Text="等于"></asp:ListItem>
                        </asp:DropDownList>
                      <asp:TextBox ID="txt_OutMoney" runat="server" Style="width: 58%;float:left;margin-top: 2%;margin-left: 5%;"></asp:TextBox>
                    </span>
                </span>
                <asp:Button ID="btn_SeachList" runat="server" Height="35px" Width="97%" Style="margin-top: 1px; margin-left: -3px; padding: 4px 9px; background-color: #dd4814; color: #FFF; border-radius: 6px;  text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                    Text="查 询" OnClick="btn_SeachList_Click" CssClass="anyes" />
            </div>
        </div>
        <div class="middle">
            <input type="hidden" id="hdst" value="-1" />
            <ul class="sctt">
                <li class="cur" atr="-1">全部</li>
                <li atr="0">转入</li>
                <li atr="1">转出</li>
            </ul>
            <div class="minMsg minMsg2" style="display: block">

                <div class="minMsgBox">
                    <div class="dianji">
                         <table id="div1" class="table table-striped" style="width:100%;display: block;border-radius: 5px;font-size: 16px;margin-left: -3px;background-color:#fff">
                  <thead id="mbbt">
                    <tr>
                        <th style="width:35%">时间</th>
                        <th style="width:35%">昵称</th>
                        <th style="width:130px">金额</th>
                        
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
                
                var res = AjaxClass.TransferListOrders($("#hdst").val(), cupindex).value;
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

