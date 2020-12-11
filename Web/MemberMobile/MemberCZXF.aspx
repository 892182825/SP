<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberCZXF.aspx.cs" Inherits="Member_MemberCash" %>

<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <title>民生充值</title>
    <link rel="stylesheet" href="CSS/style.css">
    <link href="hycss/serviceOrganiz.css" rel="stylesheet" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(
            function () {
                $('#money').blur(function () {
                    var money = $("#money").val();
                   
                    var res1 =0.1;//手续费
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

    <script type="text/javascript">
        function abc() {
            //return true;
            if (confirm('<%=GetTran("000000","您确定要提交申请吗？") %>')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                    return true;
                } else {
                    alert('<%=GetTran("007387","不可重复提交！") %>');
                    return false;
                }
            } else { return false; }
        }
    </script>

    <style>
        .fiveSquareBox {
            margin-bottom: 4px;
        }

        input[type=checkbox] {
            float: right;
            width: auto;
            margin-right: 45px;
        }

        .proLayerLine ul li {
            overflow: hidden;
            width: 50%;
            float: left;
            line-height: 28px;
        }

        .proLayerLine ul {
            overflow: hidden;
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
            background: #2d323e;
            color: #fff;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //选择不同语言是将要改的样式放到此处
            var lang = $("#lang").text();
            // alert(1);
            if (lang != "L001") {
                //alert("1111");
                $('.zcMsg p').css({ 'font-size': '14px', 'white-space': 'normal', 'textAlign': 'left' })
                //alert("MemberCash");
                $('.changeBox ul li .changeLt').width('100%').css('textAlign', 'left')
                $('.changeBox ul li .changeRt').width('100%')
                $('.changeBox ul li').css('padding-left', '2%')
            }
        });
    </script>
</head>

<body>
    <b id="lang" style="display: none"><%=Session["LanguageCode"] %></b>

    <form id="form2" runat="server">


        <%--          <div class="moneyInfo3" style="margin-top:50px;left:0px;overflow:hidden;zoom:1;width:100%;background-color:#fff">
            <a href="MemberCash.aspx" class="moneyInfoSlt"><%=GetTran("005873","提现") %></a>
            <a href="TxDetailDHK.aspx"><%=GetTran("8143  ","待汇入") %></a>
            <a href="TxDetailDCS.aspx"><%=GetTran("8139  ","待查收") %></a>
            <a href="TXDetailYDZ.aspx"><%=GetTran("007371","已到账") %></a>
        </div>--%>
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">	    民生充值</span>
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
                    <li style="display: none;">
                        <div class="changeLt">会员编号：</div>
                        <div class="changeRt">
                            <asp:Label ID="number" runat="server"></asp:Label>
                        </div>
                    </li>
                    <li>
                        <div class="changeLt" style="font-size: 16px;">兑换FTC数量：</div>
                        <div class="changeRt">
                            <div>
                                <asp:TextBox CssClass="ctConPgTxt" ID="money" runat="server" Width="132px"  Text=""></asp:TextBox>
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
                        <div class="changeLt" style="font-size: 16px;">消费钱包余额：</div>
                        <div class="changeRt">
                            <asp:Label ID="kymoney" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </li>
                    

                    <li style="display: none">
                        <div class="changeLt" style="font-size: 12px"><%=GetTran("000078", "备注")%>：</div>
                        <div class="changeRt">
                            <asp:TextBox CssClass="ctConPgTxt2" ID="remark" runat="server" Height="56px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="zzBox" id="zzBox" style="margin-top: -3%;">
                <asp:Button ID="Button1" runat="server" Height="35px" Width="93%" Style="margin-top: 25px; border-radius: 5px; margin-left: 12px; padding: -1px 9px; background-color: #96c742; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#2d323e,#2d323e); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                    Text="提交申请" CssClass="anyes" OnClick="Button1_Click" />
            </div>

            
                       <div style="color:red;font-size:16px;margin-left:10%;">注意，这是民生充值，主要用于民生支付及以后用于商城支付，充值进来后如果没有消费完可以转到保险钱包中。</div>
                    
            <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />

            <input type="hidden" id="Wyj" runat="server" />
        </div>
        <br />
        <!-- #include file = "comcode.html" -->
        <div class="modal fade" id="myModall" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h3>系统提示</h3>
                </div>
                <div class="modal-body">
                    <p id="p">Here settings can be configured...</p>
                </div>
                <div class="modal-footer">
                    <a href="#" class="btn btn-default"  data-dismiss="modal">关闭</a>
                    <a href="#" class="btn btn-primary" style="display:none;" id="tiaoz" >确定</a>
                </div>
            </div>
        </div>
    </div>
        <script type="text/javascript">
            $(function () {
                $('.mailbtn').on('click', function () {
                    $(this).addClass('mailSlt').siblings('.mailbtn').removeClass('mailSlt');
                    var Mindex = $(this).index();
                    $('.minMsg').eq(Mindex).show().siblings('.minMsg').hide();
                })
            });
        </script>
        <script>
            function alertt(data) {
                var x = document.getElementById("p");
                x.innerHTML = data;
                $('#myModall').modal({ backdrop: 'static', keyboard: false });
                $('#myModall').modal('show');

            }
</script>
    </form>
</body>
</html>

