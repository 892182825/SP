<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuiryInvest.aspx.cs" Inherits="MemberMobile_QuiryInvest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <title></title>
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <%--<script src="js/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.js" type="text/javascript" charset="utf-8"></script>--%>
    <script src="js/serviceOrganiz.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-migrate-1.1.0.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.mCustomScrollbar.js"></script>
    <%-- 新模板引用--%>


    <script type="text/javascript" language="javascript" src="../js/SqlCheck.js"></script>

   



    <link rel="stylesheet" href="css/style.css" />
    <style>
    .middle .sctt
        {
            height: 30px;
            line-height: 30px;
            width: 100%;
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

      

                .sp
                {
                    padding-left: 20px;
                    height: 40px;
                    margin: 5px;
                    font-size: 30px;
                    color: #ccc;
                    width: 60px;
                    float: left;
                }

             
        .ss {
        
                color: #ff6a00;
                    font-size: 17px;
   
        }
        .pp {
        
        
        }
        #div1 th {
         text-align:center;
         height: 40px;
    line-height: 40px;
        }
        #div1 td {
            line-height: 25px;
                text-align: center;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">我的投资</span>
            </div>
        </div>
        <table id="div1" class="table table-striped" style="display: block;font-size: 16px;margin-bottom: 50px;">

                <thead>
                    <tr>
                        <th style="width:30%" >时间</th>
                        <th style="width:44%">金额/积分</th>
                        
                        <th style="width:120px">状态</th>
                            

                    </tr>

                </thead>
                <tbody id="mblist">

                </tbody>



            </table>

    <div>
    <!-- #include file = "comcode.html" -->
    </div>
        <script>
            $(function ()
            {
                var cupindex = 1;
                var res = AjaxClass.MemberOrdersTZ().value;
                if (res != "") {
                    if (cupindex == 1) $("#mblist").html(res);
                    else {
                        $("#mom").remove(); $(res).appendTo("#mblist");
                    }
                   //$("<div id='mom' class='more' onclick ='getNext();' >加载更多..</div>").appendTo("#mblist"); cupindex += 1;
                } else {
                    $("#mom").remove();
                    $("#end").remove();
                    //$("<div id='end' class='end'>没有更多了~</div>").appendTo("#mblist");

                }
            })
       
            </script>
    </form>
</body>
</html>
