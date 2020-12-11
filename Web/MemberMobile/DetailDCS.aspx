<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailDCS.aspx.cs" enableEventValidation="false" Inherits="MemberMobile_DetailDCS" %>


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
  <%--  <script type="text/javascript">

        function tiaozhuan(hkid) {

            window.location.href = "/MemberMobile/HkJS.aspx?hkid=" + hkid;
        }

    </script>--%>

    <script type="text/javascript">
        function abc(v) {
            var id = v.title;
            if (confirm('<%=GetTran("009066","您确定要进行解释吗？") %>')) {
                window.location.href = "HkJS.aspx?id=" + id;
                return false;
                
              } else { return false; }
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
    <script type="text/javascript">
        $(function () {
            var lang = $('#lang').text();
            if (lang!="L001") {
                $('.minMsgBox ol li a').css('font-size', '12px');
                $('.je').css('font-size', '16px')
            }
        })
    </script>

</head>

<body>
    <!--页面内容宽-->
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form1" runat="server" name="form1" method="post">

        <div class="navbar navbar-default" role="navigation">
       <div class="navbar-inner">	
            	<a class="btn btn-primary btn-lg" style="float: left;padding:6px;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>
            
                <span style="color:#fff;font-size:18px;margin-left:35%;text-shadow: 2px 2px 5px hsl(0, 0%, 61%);font-weight: 600;">账户充值	</span>
            </div>
              </div>
        <div class="moneyInfo3" style="left: 0px; overflow: hidden; zoom: 1; width: 100%; background-color: #fff">
            <a href="OnlinePayment.aspx" ><%=GetTran("8137","充值") %></a>
            <a href="DetailDHK.aspx"><%=GetTran("8138","待汇出") %></a>
            <a href="DetailDCS.aspx" class="moneyInfoSlt"><%=GetTran("007169","已汇出") %></a>
            <a href="DetailYDZ.aspx"><%=GetTran("007371","已到账") %></a>
        </div>
         <div class="minMsg minMsg2" style="display: block">
             <div class="minMsgBox">
                 <div>
                    <ol>
                        <asp:Repeater ID="rep_km" runat="server" OnItemDataBound="rep_km_ItemDataBound" OnItemCommand="rep_km_ItemCommand">
                            <ItemTemplate>
                                <li style="width: 95%; margin-left: 10px; font-size: 18px;line-height: 30px;">
                               
                                    <div>
                                        <div style="color:red;position:relative;" class="je">
<%=( AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) ))==1?"$":"￥"%>
  <%#Math.Round((double.Parse(Eval("WithdrawMoney").ToString())*huilv)).ToString("#0.00") %> 
                                           <%-- <%#double.Parse(Eval("WithdrawMoney").ToString()).ToString("0.00")%>--%>
                                            <%-- <input type="button" id="Button1" name="Button1"  onclick='tiaozhuan(<%# Eval("hkid") %>,<%#double.Parse(Eval("WithdrawMoney").ToString()).ToString("0.00")%>)' value="通知查收" />--%>
                                            <asp:Button ID="Button1"  Visible="false" style="display:none; height: 25px; line-height: 25px; text-align: center;  background: #85ac07; width: 64px;  border-radius: 5px; color: #fff; font-size: 14px; margin-left: 34%;" runat="server" Text="通知查收" 
                                                 OnClientClick='tiaozhuan(<%# Eval("hkid") %>,<%#double.Parse(Eval("WithdrawMoney").ToString()).ToString("0.00")%>); return false' />


                                            <asp:HyperLink ID="HyperLink1"  style=" height: 25px; line-height: 25px; text-align: center;  background: #85ac07; width: 64px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 23%;top: 4px;" runat="server"><%=GetTran("009088","通知查收") %></asp:HyperLink>

                                         <asp:HyperLink ID="HyperLink2"  style="color:red; font-size: 15px;position: absolute; right: 23%;top: 2px;" runat="server"><%=GetTran("009087","等待确认") %>...</asp:HyperLink>
                                            <asp:HiddenField ID="HiddenField3" Value='<%# Eval("id") %>' runat="server" />

                                             <asp:Label style="margin-right: 80px;display:none"  ID="Label1" runat="server" Text="等待确认...."></asp:Label>
                                            <asp:HiddenField ID="HiddenField1" Value='<%# Eval("hkid") %>' runat="server" />
                                            <asp:HiddenField ID="HiddenField2" Value='<%# double.Parse(Eval("WithdrawMoney").ToString()).ToString("0.00")%>' runat="server" />
                                         <%--   <input type="button" style=" height: 25px; line-height: 27px; text-align: center; background: #ccc; width: 50px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 5%;top: 4px;"  id="jieshi" value="解释" />--%>
                                            <asp:Button ID="jieshi" runat="server" Visible="false" Text="解释" style=" height: 25px; line-height: 27px; text-align: center; background: #ccc; width: 50px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 5%;top: 4px;"  />
                                            <asp:Button ID="jieshi1" runat="server" Text="解释" ToolTip='<%# Eval("id")%>'  OnClientClick="return abc(this)" style=" height: 25px; line-height: 27px; text-align: center;  background: red; width: 50px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 5%;top: 4px;"  />
                                                  <asp:Button ID="jieshi2" runat="server" Visible="false" Text="解释成功" style=" height: 25px; line-height: 27px; text-align: center; background: #ccc; width: 62px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 2%;top: 4px;"  />
                                             <%--<input type="button" onclick='tiaozhuan(<%# Eval("hkid") %>)' style=" height: 25px; line-height: 27px; text-align: center;  background: red; width: 50px;  border-radius: 5px; color: #fff; font-size: 14px;position: absolute; right: 5%;top: 4px;"  id="jieshi" value="解释" />--%>
                                        </div>
                                        <div>
                                            <%=GetTran("000086","开户名") %>：<%#Eval("name")%><br /><%=GetTran("001243","开户行") %>：<%#Eval("bankname")%><br /><%=GetTran("002073","账号") %>：<%#Eval("bankcard")%></div>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Button ID="Button2" runat="server"  Text="上传汇款凭证" style=" height: 25px; line-height: 27px; text-align: center; background: #85ac07; width: 100px;  border-radius: 5px; color: #fff; font-size: 14px;" CommandName="sc" CommandArgument='<%# Eval("id")%>'  />
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ol>
                </div>
            </div>
        </div>

        <input type="hidden"  id="Hkid" runat="server" value=""/>
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





