<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlinePayQD.aspx.cs" Inherits="MemberMobile_OnlinePayQD" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

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
        function popDiv() {
            var hkid = $("#hkid").val();
            var money = $("#rmoney1").val();
            window.location.href = "OnlineYZSM.aspx?hkid=" + hkid + "&money=" + money;
        }
    </script>
     <script type="text/javascript">
         function abc() {
             if (confirm('<%=GetTran("009054","您确定要取消吗") %>？')) {
                 var hid = document.getElementById("fanhuiz").value;
                 if (hid == "1") {
                     document.getElementById("fanhuiz").value = "0";
                     return true;
                 }
                 else {
                     alert('<%=GetTran("009056","取消失败") %>！');
                     return false;
                 }

             } else { return false; }
         }
    </script>

    <script type="text/javascript">
        $(function () {
            //说明
            $("#Button3").click(function () {
                modalToggle("containal");
            });
        });
        function modalToggle(contentCls, e) {
            e = e || window.event;
            e.stopPropagation();
            var _containal = document.querySelector('.' + contentCls)
            if (e.target == _containal) {
                return
            }
            var modal_id = document.getElementById('modal');
            var computedStyle = document.defaultView.getComputedStyle(modal_id, null);
            computedStyle.display === 'none' ? modal_id.style.display = 'block' : modal_id.style.display = 'none';
        }
    </script>
    
    <style type="text/css">
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
            /*margin-left: 51%;*/
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
            /*margin-left: 238%;*/
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
            /*margin-left: 141%;*/
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
        .modal .containal .modal_btn1 {
            left: 5%;
            float: left;
            display: block;
            height: 40px;
            line-height: 40px;
            text-align: center;
            border: 0px;
            background: #ccc;
            color: #666;
            margin-left: 5px;
            margin-top: 415px;
            width: 40%;
        }
        .modal .containal .modal_btn2 {
            float: left;
            right: 5%;
            display: block;
            height: 40px;
            line-height: 40px;
            text-align: center;
            border: 0px;
            background: #4d4342;
            color: #fff;
            margin-top: 415px;
            margin-left: 50px;
            width: 40%;
        }
        .modal .containal {
            position: absolute;
            width: 80%;
            height: 70%;
            background: #fff;
            top: 11%;
            left: 10%;
            padding: 5%;
            border-radius: 10px;
            z-index: 999;
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
            <a href="DetailDHK.aspx" class="moneyInfoSlt"><%=GetTran("8138","待汇出") %></a>
            <a href="DetailDCS.aspx"><%=GetTran("007169","已汇出") %></a>
            <a href="DetailYDZ.aspx"><%=GetTran("007371","已到账") %></a>
        </div>
        <div class="minMsg minMsg2" style="display: block">
            <div class="minMsgBox">
                <div>
                    <ol>
                        <div style="font-size: 18px;">
                           <%=GetTran("009083","您的") %> <%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%>

                            <asp:Label ID="rmoney" runat="server" ForeColor="Red"></asp:Label><%=GetTran("009084","需分") %><asp:Label ID="bishucount" runat="server" ForeColor="Red"></asp:Label><%=GetTran("009085","笔汇入以下账户") %>  
                        </div>
                        <br />
                        <asp:Repeater ID="rep_km" runat="server">
                            <ItemTemplate>
                                <li style="width:95%;margin-left:6px;font-size: 18px;">
                                    <div>
                                        <%=GetTran("009086","第1笔") %>：
                                      <%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%> 
                                         <%#Math.Round((double.Parse(Eval("cdmoney").ToString())*huilv)).ToString("#0.00")  %> 

   <%--( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) )) ).ToString("f2")--%>
                    
                                       <%-- <%# double.Parse(Eval("cdmoney").ToString()).ToString("0.00")%>--%>
                                        <br />
                                        <%# Eval("bankname")%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="rep_km1" Visible="false" runat="server" OnItemDataBound="rep_km1_ItemDataBound">
                            <ItemTemplate>
                                <li style="width: 95%; margin-left: 6px; font-size: 18px;">
                                    <div>
                         <%=GetTran("009086","第1笔") %>：<%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%> 
                                        <asp:Label ID="Label1" style="float: none;margin-right: -6px;background: #fff;padding: 0px 5px; border-radius: 2px;color: black;font-size: 18px;" runat="server" Text="Label"></asp:Label>
                                      <%--  <%=GetTran("000564","元") %>--%>
                                        <br />
                                        <%# Eval("Bank")%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ol>
                </div>
            </div>
        </div>

        <div class="changeBox zcMsg" style="margin-left:7px;width:95%" id="div1">
            <ul>
                <li style="height: 95px; text-align: center">
                    <div style="font-size: 20px;">
                        <%=GetTran("009010","您确定能在2小时内完成转账吗") %>？
                    </div>
                    <div style="float: left;margin-left:15%">
                        <input type="button" id="Button1" name="Button1" onclick="popDiv()" value='<%=GetTran("000434","确定") %>' />
                    </div>
                    <div style="float: left;margin-left:8%">
                        <input type="button" id="Button3" name="Button3" value=' <%=GetTran("000628","说明") %>' />
                    </div>
                    <div style="float: left;margin-left:9%">
                      
                    <%--<input type="button" id="Button2" name="Button2" value="取消" />--%>
                        <asp:Button ID="Button2" runat="server" 
                                CssClass="changeBtn" Text="取消" OnClick="Btn_click" OnClientClick="return abc();" />
                    
                        <input type="hidden" value="0" id="hkid" runat="server" />
                        <input type="hidden" value="0" id="rmoney1" runat="server" />
                        <input type="hidden" value="1" id="fanhuiz" runat="server" />

                    </div>
                    <div class="modal" id="modal" style=" display:none" onclick='modalToggle("containal")'>
                        <div class="containal">
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            <a class="modal_btn1" onclick='modalToggle("containal")'><%=GetTran("001614","取消") %></a>
                            <a class="modal_btn2" onclick='modalToggle("containal")'><%=GetTran("000434","确定") %></a>
                        </div>
                    </div>

                </li>
            </ul>
        </div>
     <!-- #include file = "comcode.html" -->
    </form>
</body>
</html>


