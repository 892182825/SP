<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mallchongzhi.aspx.cs" Inherits="MemberMobile_mallchongzhi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title></title>
    <link rel="stylesheet" href="CSS/style.css">
     <script type="text/javascript">
         $(document).ready(
             function () {
                 $('#money').blur(function () {
                     var money = $("#money").val();

                     var res1 = 0.1;//手续费
                     if (money != "" && money != null) {
                         try {
                             $('#txsxf').css('display', 'inline');
                             //var txsxf =res1.value;
                             var djje = parseFloat(money) - parseFloat(money) * res1;
                             $("#SXF").text(djje);
                             //$("#HiddenField1").val(res.value)
                             $("#HiddenField2").val(res1)
                             $('#SXF').css('display', 'inline');

                         } catch (e) {
                             return false;
                         }
                     }



                 })
             })
    </script>
    <script type="text/javascript">
        function abc() {
            //return true;
            if (confirm('您确定要提交申请吗？')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                    return true;
                } else {
                    alert('不可重复提交！');
                    return false;
                }
            } else { return false; }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
          <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">	    FTC充值</span>
            </div>
        </div>
        <div class="middle" style="margin-top: 50px;">
            <div class="changeBox zcMsg" style="position: relative; margin-top: 10px;">
                <div>
                    <div style="text-align: center; margin-bottom: 20px; margin-top: -20px;">
                        <p style=" font-size: 18px;">
                              <%--<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                            <%=GetTran("005873","提现")%>--%>
                        </p>
                    </div>
                </div>
                <ul>
                    <li>
                        <div class="changeLt">会员手机：</div>
                        <div class="changeRt">
                            <asp:Label ID="number" runat="server"></asp:Label>
                        </div>
                    </li>
                    <li>
                        <div class="changeLt" style="font-size: 16px;">兑换FTC数量：</div>
                        <div class="changeRt">
                            <div>
                                <asp:TextBox CssClass="ctConPgTxt" ID="money"  runat="server" MaxLength="15" style="width:150px;height:40px;border-radius: 5px;border: 1px solid #ccc;"></asp:TextBox>
                            </div>
                           <%-- <div style="font-size: 15px;">
                                (<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                <%=GetTran("009102","%作为提现手续费")%>)

                            </div>--%>
                            <%--  <div>(<%=GetTran("009022", "最多")%>：<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>)</div>--%>
                        </div>
                    </li>
                    <li>
                        <div class="changeLt" style="font-size: 16px;">云端钱包余额：</div>
                        <div class="changeRt">
                            <asp:Label ID="rmoney" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </li>
                    <li>
                        <div class="changeLt" style="font-size: 16px;display:none;">钱包余额：</div>
                        <div class="changeRt" style="display:none;">
                            <asp:Label ID="kymoney" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </li>

                    

                    <li style="display: none">
                        <div class="changeLt" style="font-size: 12px">备注：</div>
                        <div class="changeRt">
                            <asp:TextBox CssClass="ctConPgTxt2" ID="remark" runat="server" Height="56px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </li>
                    <li><asp:Button ID="Button1" runat="server" Height="45px" Width="80%" Style="height: 45px;width: 80%;margin-top: 25px;border-radius: 10px;font-size: 20px;margin-left: 10%;color: #FFF;border: 1px solid #9E9E9E;background-image: linear-gradient(#54b4eb, #2fa4e7 60%, #1d9ce5);text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);font-weight: 900;"
                    Text="充值"  OnClick="Button1_Click" />

                    </li>
                    <li>
                        
                            <a style="float: left; width: 90%; height:100px; text-align: center; font-size: 16px; margin-top: 30px;margin-left:5% ;color: #666;" href="http://wap.xphmalls.com/WapShop">
                               不充值点击图标直接进入商城>>> <img src="images/sc.png" width="42" height="42" style="text-align:center;margin-left:45%" />
                                
                      
                            </a>
                        
                    </li>
                </ul>
            </div>
            


            <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />

            <input type="hidden" id="Wyj" runat="server" />
        </div>
        <br />
        <!-- #include file = "comcode.html" -->

        <script type="text/javascript">
            $(function () {
                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();
                })
            });
        </script>
    </form>
</body>
</html>
