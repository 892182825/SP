<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailCSDCS.aspx.cs" Inherits="MemberMobile_DetailCSDCS" %>



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
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
    <script src="js/jquery-1.7.1.min.js"></script>
    <link rel="stylesheet" href="css/style.css">
    <script language="javascript" type="text/javascript">
        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            filterSql_II(btname);
        }
    </script>
    <script type="text/javascript">

        function tiaozhuan(hkid) {

            window.location.href = "/MemberMobile/HkJS.aspx?hkid=" + hkid;
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
            /* display: block; */
            height: 25px;
            line-height: 25px;
            text-align: center;
            background: #85ac07;
            width: 64px;
            border-radius: 5px;
            color: #fff;
            font-size: 14px;
            margin-left: 34%;
            /* margin-top: 11%; */
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

    </style>


</head>

<body>
    <!--页面内容宽-->
    <form id="form1" runat="server" name="form1" method="post">

        <div class="t_top">
            <a class="backIcon" href="javascript:history.go(-1)"></a>
            <%=GetTran("002272", "账户充值")%>
            <div class="tt_r">
            </div>

        </div>
        <div class="moneyInfo3" style="left: 0px; overflow: hidden; zoom: 1; width: 100%; background-color: #fff">
            <a href="OnlinePayment.aspx" >充值</a>
            <a href="DetailDHK.aspx">待汇出</a>
            <a href="DetailDCS.aspx" class="moneyInfoSlt">待查收</a>
            <a href="DetailYDZ.aspx">已到账</a>
        </div>
         <div class="minMsg minMsg2" style="display: block">
             <div class="minMsgBox">
                 <div>
                    <ol>
                        <asp:Repeater ID="rep_km" runat="server" OnItemDataBound="rep_km_ItemDataBound">
                            <ItemTemplate>
                                <li style="width: 95%; margin-left: 10px; font-size: 18px;line-height: 30px;">
                               
                                    <div>
                                        <div style="color:red;position:relative; ">
                                            <%#double.Parse(Eval("WithdrawMoney").ToString()).ToString("0.00")%>  <%=GetTran("000564","元") %>
                                     
                                            <%-- <input type="button" id="Button1" name="Button1"  onclick='tiaozhuan(<%# Eval("hkid") %>,<%#double.Parse(Eval("WithdrawMoney").ToString()).ToString("0.00")%>)' value="通知查收" />--%>
                                            <asp:Button ID="Button1"  Visible="false" style="display:none; height: 25px; line-height: 25px; text-align: center;  background: #85ac07; width: 64px;  border-radius: 5px; color: #fff; font-size: 14px; margin-left: 34%;" runat="server" Text="通知查收" 
                                                 OnClientClick='tiaozhuan(<%# Eval("hkid") %>,<%#double.Parse(Eval("WithdrawMoney").ToString()).ToString("0.00")%>); return false' />


                                            <asp:HyperLink ID="HyperLink1"  style=" height: 25px; line-height: 25px; text-align: center;  background: #85ac07; width: 64px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 23%;top: 4px;" runat="server">通知查收</asp:HyperLink>

                                              <asp:HyperLink ID="HyperLink2"  style="color:red; font-size: 15px;position: absolute; right: 23%;top: 2px;" runat="server">等待确认...</asp:HyperLink>


                                             <asp:Label style="margin-right: 80px;display:none"  ID="Label1" runat="server" Text="等待确认...."></asp:Label>
                                            <asp:HiddenField ID="HiddenField1" Value='<%# Eval("hkid") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenField2" Value='<%# double.Parse(Eval("WithdrawMoney").ToString()).ToString("0.00")%>' runat="server" />
                                            <input type="button" onclick='tiaozhuan(<%# Eval("hkid") %>)' style=" height: 25px; line-height: 27px; text-align: center;  background: red; width: 50px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 5%;top: 4px;"  id="jieshi" value="解释" />
                                        </div>
                                        <div>
                                            <%=GetTran("000086","开户名") %>：<%#Eval("name")%><br /><%=GetTran("001243","开户行") %>：<%#Eval("bankname")%><br /><%=GetTran("002073","账号") %>：<%#Eval("bankcard")%></div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ol>


                </div>
            </div>
        </div>

        <input type="hidden"  id="Hkid" runat="server"/>
     <!-- #include file = "comcode.html" -->
    </form>
</body>
</html>
<script type="text/jscript">
    $(function () {
        $('#bottomDiv').css('display', 'none');

        $("#Pager1_div2").css('background-color', '#FFF')
    })

    //function popDiv() {

    //    window.location.href = "/MemberMobile/OnlinePayQD.aspx";
    //}
</script>






