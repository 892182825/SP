<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HCRXX.aspx.cs" Inherits="MemberMobile_HCRXX" %>


<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">

    <title></title>
    <script src="jeDate/jquery-1.7.2.js"></script>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script> 
    <link rel="stylesheet" href="css/style.css">
        <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            filterSql_II(btname);
        }
    </script>



    <style>
        .changeBox ul li .changeLt {
            width: 30%;
        }

        .changeBox ul li .changeRt {
            width: 70%;
        }

            .changeBox ul li .changeRt .textBox {
                width: 80%;
            }

        .zcMsg ul li .changeRt .zcSltBox {
            width: 80%;
        }

        .zcMsg ul li .changeRt .zcSltBox2 {
            width: 39%;
        }

        #txtadvpass {
            width: 79%;
            border: 1px solid #ccc;
        }

       

        .moneyInfo3 a {
            float: left;
            width: 25%;
            text-align: center;
            height: 30px;
            font-size: 16px;
            line-height: 30px;
        }

        .moneyInfoSlt {
            background: forestgreen;
            color: #fff;
        }

        #Button1 {
            display: block;
            height: 28px;
            line-height: 30px;
            text-align: center;
            background: #85ac07;
            width: 58px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 51%;
            margin-top: 11%;
        }

        #Button2 {
            display: block;
            height: 28px;
            line-height: 30px;
            text-align: center;
            background: #85ac07;
            width: 58px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 238%;
            margin-top: 11%;
        }

        #Button3 {
            display: block;
            height: 28px;
            line-height: 30px;
            text-align: center;
            background: #85ac07;
            width: 58px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 141%;
            margin-top: 11%;
        }

        #sub {
            display: block;
            height: 35px;
            line-height: 35px;
            text-align: center;
            background: #85ac07;
            width: 100%;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
        }
           #HkTime {
            width: 80%;
            margin-top: 5px;
            float: left;
            height: 24px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }
    </style>


</head>

<body>
    <!--页面内容宽-->
    <form id="form1" runat="server" name="form1" method="post">

        <div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:35%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">账户充值	</span>
            </div>
              </div>
        <div class="moneyInfo3" style="left: 0px; overflow: hidden; zoom: 1; width: 100%; background-color: #fff">
           <a href="OnlinePayment.aspx" ><%=GetTran("8137","充值") %></a>
            <a href="DetailDHK.aspx" ><%=GetTran("8138","待汇出") %></a>
            <a href="DetailDCS.aspx" class="moneyInfoSlt"><%=GetTran("007169","已汇出") %></a>
            <a href="DetailYDZ.aspx"><%=GetTran("007371","已到账") %></a>
        </div>
        <div class="middle" style="width: 95%; margin-left: 10px; margin-top: 10px;">
            <div class="changeBox zcMsg">
                <ul>
                    <li style="font-size: 18px">
                        <%=GetTran("009018", "汇出方信息")%>：
                    </li>


                    <li style="font-size: 18px">
                        <div class="changeLt"><%=GetTran("009016", "实汇金额")%>：</div>
                        <div class="changeRt">
                            <asp:TextBox ID="Money" CssClass="textBox" runat="server" style="width:30%"
                                MaxLength="50"></asp:TextBox>
                        </div>

                    </li>

                    <li style="font-size: 18px">
                        <div class="changeLt"><%=GetTran("007798", "汇款时间")%>：</div>
                        <div class="changeRt">
                             <%-- <asp:TextBox ID="HkTime" placeholder="YYYY-MM-DD hh:mm:ss"   CssClass="jeinput" runat="server" MaxLength="50"></asp:TextBox>--%>
                            <asp:TextBox ID="HkTime" Style="margin-top: 5px;float:left" CssClass="Wdate" runat="server" onfocus="WdatePicker({dateFmt:'yyyyMMdd HH:mm:ss'})"></asp:TextBox>
                          <%--  <asp:Label ID="HkTime" class="hhmmss" runat="server"  Text="Label"></asp:Label>--%>

                                    <%--<input type="datetime-local" name="HkTime" id="HkTime" runat="server" />--%>

                           <%-- <input type="text" class="jeinput" id="test04" placeholder="YYYY-MM-DD hh:mm:ss">--%>
                        </div>
                    </li>

                    <li style="font-size: 18px">
                        <div class="changeLt"><%=GetTran("001243","开户行") %>：</div>
                        <div class="changeRt">
                            <asp:TextBox ID="KHH" CssClass="textBox" runat="server"
                                MaxLength="50"></asp:TextBox>
                        </div>
                    </li>


                    <li style="font-size: 18px">
                        <div class="changeLt"><%=GetTran("002073","账号") %>：</div>
                        <div class="changeRt">
                            <asp:TextBox ID="ZH" CssClass="textBox" runat="server"  style="width:50%"
                                MaxLength="4"></asp:TextBox>（<%=GetTran("008316","后4位") %>）
                        </div>
                    </li>

                    <li style="font-size: 18px">
                        <div class="changeLt"><%=GetTran("000086","开户名") %>：</div>
                        <div class="changeRt">
                            <asp:TextBox ID="KHM" CssClass="textBox" runat="server"
                                MaxLength="50"></asp:TextBox>
                        </div>
                    </li>
                    <li style="height: auto; font-size: 18px">
                        <div class="changeLt"><%=GetTran("009017", "汇款说明")%>：</div>
                        <div class="changeRt">
                            <asp:TextBox ID="txtEnote" CssClass="ctConPgTxt2" TextMode="MultiLine" Style="border: 1px solid #ccc; border-radius: 3px; width: 80%" Height="60px" runat="server" MaxLength="250" />
                        </div>
                    </li>
                </ul>
            </div>


            <div class="changeBtnBox zc">
                <asp:Button ID="sub" runat="server" Text="提交"
                    CssClass="changeBtn" OnClick="btn_Click" />
                <%--   <input type="button" id="sub"  name="sub" value="提 交" onclick="popDiv()" /> --%>

                <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
            </div>
        <!-- #include file = "comcode.html" -->

        </div>
    </form>
</body>
</html>
<script type="text/javascript">

    //function popDiv() {

    //    window.location.href = "/MemberMobile/OnlinePayQD.aspx";
    //}


</script>

<script type="text/jscript">
    $(function () {
        $('#bottomDiv').css('display', 'none');

        $("#Pager1_div2").css('background-color', '#FFF')
    })

</script>


