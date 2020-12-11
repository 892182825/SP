﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BMyx.aspx.cs" Inherits="MemberMobile_BMyx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="css/style.css">

    <link  href="../css/bootstrap-cerulean.min.css" rel="stylesheet">

    <link href="../css/charisma-app.css" rel="stylesheet">
    <link href='../bower_components/fullcalendar/dist/fullcalendar.css' rel='stylesheet'>
    <link href='../bower_components/fullcalendar/dist/fullcalendar.print.css' rel='stylesheet' media='print'>
    <link href='../bower_components/chosen/chosen.min.css' rel='stylesheet'>
    <link href='../bower_components/colorbox/example3/colorbox.css' rel='stylesheet'>
    <link href='../bower_components/responsive-tables/responsive-tables.css' rel='stylesheet'>
    <link href='../bower_components/bootstrap-tour/build/css/bootstrap-tour.min.css' rel='stylesheet'>
    <link href='../css/jquery.noty.css' rel='stylesheet'>
    <link href='../css/noty_theme_default.css' rel='stylesheet'>
    <link href='../css/elfinder.min.css' rel='stylesheet'>
    <link href='../css/elfinder.theme.css' rel='stylesheet'>
    <link href='../css/jquery.iphone.toggle.css' rel='stylesheet'>
    <link href='../css/uploadify.css' rel='stylesheet'>
    <link href='../css/animate.min.css' rel='stylesheet'>
    <title></title>
        <script src="../bower_components/jquery/jquery.min.js"></script>
    <script>
        window.alert = alert;


    </script>
</head>
<body style="padding-top:50px" >
    <form id="form1" runat="server">
        <div class="middle">
        <input type="hidden" id="hdst" value="-1" />
            <ul class="sctt">
                <li class="cur" atr="-1" style="width:50%;"><%=GetTran("010471", "充值")%></li>
                <li atr="0" style="width:50%;"><%=GetTran("000340", "查询")%></li>
            </ul>
         <div class="minMsg minMsg2" style="display: none" id="div1">

                <div class="minMsgBox">
                    <div class="dianji">
                         <table  class="table table-striped" style="width:100%;display: block;border-radius: 5px;font-size: 15px;margin-left: -3px;background-color:#fff">
                  <thead id="mbbt">
                    <tr>
                        <th style="width:33%;">充值账号</th>
                        <th style="width:33%;">充值金额</th>
                        <th style="width: 125px;">
                            <div> 实际支付FTC </div>
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
    <div class="cont_p" id="div2">
        <div id="yc2"><asp:Button ID="cyx" runat="server" Text="查询游戏列表" OnClick="cyx_Click" /></div>
        <div id="yc1" style="display:none;">
            <div>
            <%=GetTran("010510", "游戏选择:")%></div><asp:DropDownList  CssClass="form-control" ID="mc" runat="server">
        </asp:DropDownList><asp:Button ID="cyxlb" runat="server" Text="选择" OnClick="cyxlb_Click" /> </div>
        <br />
        <div id="yc3" style="display:none;">
       <div> <%=GetTran("010511", "游戏可充值商品:")%></div><asp:DropDownList  CssClass="form-control" ID="yxkcz" runat="server" >
        </asp:DropDownList>
            <asp:Button ID="yxkczxz" runat="server" Text="选择" OnClick="yxkczxz_Click" />
            </div>
            <div id="yc4" style="display:none;">
        <div> <%=GetTran("010512", "选择商品:")%></div><asp:DropDownList  CssClass="form-control" ID="xzsp" runat="server" >
        </asp:DropDownList>
        <%=GetTran("010513", "游戏账号:")%><asp:TextBox ID="jykxm" runat="server"></asp:TextBox>
       <%=GetTran("002029", "数量:")%> <asp:TextBox ID="sjh" runat="server"></asp:TextBox>
        <%=GetTran("010382", "金额:")%><asp:Label runat="server" ID="lab" Text="0.00"></asp:Label>
        <asp:Button ID="cz" runat="server" Text="充值" OnClick="cz_Click" />
        
        
            </div>
    </div>
            </div>
         <div class="modal fade" id="myModall" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h3><%=GetTran("010021", "系统提示")%></h3>
                </div>
                <div class="modal-body">
                    <p id="p">Here settings can be configured...</p>
                </div>
                <div class="modal-footer">
                    <a href="#" class="btn btn-default"  data-dismiss="modal"><%=GetTran("000019", "关闭")%></a>
                    <a href="#" class="btn btn-primary" style="display:none;" id="tiaoz" ><%=GetTran("000434", "确定")%></a>
                </div>
            </div>
        </div>
    </div>
        <script>
            function alertt(data) {
                var x = document.getElementById("p");
                x.innerHTML = data;
                $('#myModall').modal({ backdrop: 'static', keyboard: false });
                $('#myModall').modal('show');

            }
        </script>
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
                     if ($("#hdst").val() == 0) {
                         $("#div1").show();
                         $("#div2").hide();
                         getNext();
                     }
                     else {
                         $("#div1").hide();
                         $("#div2").show();
                     }
                 });

             });
             function getNext() {


                 var res = AjaxClass.EshenghuoCZ(cupindex, 4).value;
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
    </form>
</body>
</html>