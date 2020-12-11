<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BrowseMemberOrders.aspx.cs" Inherits="MemberMobile_BrowseMemberOrders" %>

<%@ Register Src="../MemberMobile/PageSj.ascx" TagName="PageSj" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <title></title>
    <%-- 新模板引用--%>

    <script src="js/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-migrate-1.1.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.mCustomScrollbar.js"></script>
    <%-- 新模板引用--%>


    <script type="text/javascript" language="javascript" src="../js/SqlCheck.js"></script>

    <script src="js/jquery-1.7.1.min.js"></script>



    <link rel="stylesheet" href="css/style.css" />
    <%--<link href="hycss/serviceOrganiz.css" rel="stylesheet" />--%>
    <style>
      
        .minMsg
        {
            background-color:#eee;
        }
        .minMsg ul
        {
            width: 100%;
        }

            .minMsg ul li
            {
                background-color: #fff;
                margin-top: 10px;
                padding-top:5px;
                padding-bottom:5px;
                border-top: 1px solid #eee;
                border-bottom: 1px solid #eee;
                height: 60px;
                line-height: 20px;
            }

                .minMsg ul li span
                {
                    padding-left: 2px;
                    height: 40px;
                    margin: 5px;
                    font-size: 30px; 
                    width: 10%;
                    float: left;
                }

                .minMsg ul li .ctinfo
                {font-size: 16px;
                   height: 50px; 
                   line-height:50px;
                        width:15%;
                        margin: 2px;
                    float: left;
                }
                    .minMsg ul li .ctinfo2
                {font-size: 14px;  
                   height: 50px; 
                   line-height:22px;
                        width:30%;
                        margin: 2px;
                    float: left;
                }
                        .minMsg ul li .ctinfo2 p
                        {
                             margin:0px;
                             line-height:22px;
                        }
  .minMsg ul li .ctinfo2 .p1
                        {font-size: 16px; font-weight:bold;
                             margin:0px;
                        }
    .minMsg ul li .ctinfo2 .p2
                        {
                             margin:0px;
                        }

                        .minMsg ul li .ctinfo3
                {font-size: 16px;
                   height: 40px; 
                   line-height:40px;
                        width:15%;
                        margin: 2px;
                         color: #ff6a00;
                    float: left;
                } 


                  

                .minMsg ul li a
                {
                    float: right;
                    height: 40px;
                    line-height:30px;
                    margin-left: 10px;
                    width:15%;
                }

                .minMsg ul li .active
                {
                    float: right;
                    height: 40px;
                    margin: 5px;
                    line-height: 40px;
                    color: #0094ff;
                }


    </style>

    <script type="text/javascript" language="javascript">
        function CheckText() {
            //防SQL注入
            filterSql();
        }

        var defaultcur;

        //    window.onload=function (){
        //       defaultcur=document.getElementById("ddlCurrency").value;
        //    }

    </script>

    <script type="text/javascript">
        //选择不同语言是将要改的样式放到此处
        var lang = $("#lang").text();
        //alert(1);
        if (lang != "L001") {
            //alert("1111");

            // alert("BrowseMemberOrders");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">激活会员</span>
            </div>
        </div>


        <div class="middle">
            <input type="hidden" id="hdst" value="-1" />
            <ul class="sctt" style="display:none">
                <li  atr="-1">全部</li>
                <li class="cur" atr="0">未激活</li>
                <li  atr="1">已激活</li>
            </ul>


            <div id="div1" class="minMsg minMsg2" style="display: block">

                <div>
                    <ul id="mblist">
                         
                    </ul>

                </div>




            </div>
        </div>
          
 <div class="modal fade" id="myModall" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >         <div class="modal-dialog">
            <div class="modal-content">
               
                <div class="modal-body">
                     <ul><li><input  placeholder="安置位" class="form-control"   /></li></ul>
                </div>
                <div class="modal-footer">
               <a href="#" class="btn btn-primary" style="width:100%;" data-dismiss="modal">支付</a> 
                </div>
            </div>
        </div>
    </div>
        <!-- #include file = "comcode.html" -->





    </form>
    <script type="text/jscript">
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


            var res = AjaxClass.BrowseMemberOrders(0, cupindex).value;
            if (res != "") {
                if (cupindex == 1) $("#mblist").html(res);
                else {

                    $("#mom").remove(); $(res).appendTo("#mblist");
                }
                $("<div id='mom' class='more' onclick='getNext();' >加载更多..</div>").appendTo("#mblist"); cupindex += 1;
            } else {
                if (cupindex == 1) $("#mblist").html(res);
                $("#mom").remove();
                $("#end").remove();
                $("<div id='end' class='end'>没有更多了~</div>").appendTo("#mblist");

            }


        }


        function gotoact(eld) {
            alert(eld);
            $('#myModall').modal('show');

        }


    </script>
</body>
</html>
