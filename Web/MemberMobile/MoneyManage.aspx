<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoneyManage.aspx.cs" Inherits="Member_MoneyManage" ValidateRequest="false" %>
<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
   <script src="../bower_components/jquery/jquery.min.js"></script>
    <title>电子转账</title>
    <link rel="stylesheet" href="CSS/style.css"> 
 
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

       <script type="text/javascript">
        function abc() {
            if (confirm('确定要转入吗？')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                } else {
                alert('不可重复提交！');
                    return false;
                }
            } else { return false; }
        }
        function getname() {
            var number = document.getElementById("txt_InNumber").value;
            var rad_Inzh = document.getElementsByName("rad_Inzh");
            var rad = 0;
            for (i = 0; i < rad_Inzh.length; i++) {
                if (rad_Inzh[i].checked) {
                    if (rad_Inzh[i].value == "2") { var name = AjaxClass.GetPetName(number, "Store").value; } else {
                        var name = AjaxClass.GetPetName(number, "Member").value;
                    }
                    document.getElementById("lab_nicheng").innerHTML ="账号昵称："+ name;
                }
            }
        }
    </script>
  <script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');


        })

    </script>

    <script type="text/jscript">
        $(function () {
            $('#rad_Outzh label').css({ 'float': 'left', 'margin-top': '1px'});
            $('#rad_Outzh').css({ 'width': '19%', 'margin-left': '5px'});
            $('#rad_Outzh input').css({ border: 0, 'margin': '2px', 'float': 'left' });

            $('#cb_check label').css({ 'float': 'left','width':'93%' });
            $('#cb_check input').css({ 'float': 'left' });


            $('#rad_xianjin').css({ 'float': 'left' });
            $('#rad_xiaofei').css({ 'float': 'left' });
            $('#rad_fuwujigou').css({ 'float': 'left' });



            $('#aa').css({ 'float': 'left' });
            $('#bb').css({ 'float': 'left' });
            $('#cc').css({ 'float': 'left' });

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
    <style>
      
      
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
        .changeBox {
            padding:10px 2%
        }
            .changeBox ul li .changeRt {
                width:70%;
            }
            .changeBox ul li .changeLt {
                width:30%
            }
    </style>
    <script type="text/javascript" >
        $(function () {
            //选择不同语言是将要改的样式放到此处
            var lang = $("#lang").text();
            // alert(1);
            if (lang != "L001") {
                $('.changeBox ul li .changeLt').width('100%').css('textAlign','left');
                $('.changeBox ul li .changeRt').width('100%')
            }
        })
       

 </script> 

</head>

<body> 
 
    <form id="form2" runat="server">

    <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">	    转账交易</span>
            </div>
        </div>
        <div class="middle">
          
            <div style="display:none;" class="changeBox zcMsg">
                  
                <ul>

                    <li>
                        <div class="changeLt">转出人编号：</div>
                        <div class="changeRt">
                          <asp:Label ID="lbEnum" runat="server"></asp:Label>
                        </div>
                    </li>
                    <li>
                        <div class="changeLt">转出人昵称：</div>
                        <div class="changeRt">
                            <asp:Label ID="lbUsername" runat="server"></asp:Label>
                        </div>
                    </li>
                    <li>
                        <div class="changeLt">转出账户：</div>
                        <div class="changeRt">
                            <asp:RadioButtonList ID="rad_Outzh" runat="server" AutoPostBack="true" RepeatLayout="Flow"
                                RepeatDirection="Horizontal" OnSelectedIndexChanged="rad_Outzh_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </div>
                    </li>
                   
                    <li>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        <%--<div class="changeLt"><%=GetTran("001768", "现金账户余额")%>：</div>
                        <div class="changeRt">--%>
                         <%--  <asp:Label ID="lbEmoney" runat="server"></asp:Label>--%>
                       <%-- </div>--%>
                    </li>

                    
                   
                </ul>
            </div>

             <div style="text-align:center;font-size:16px;line-height:30px">
         
                 转账到对方账户
            </div> 
            <div class="changeBox zcMsg">
          
                 
                <ul>

                    <li>
                       <%-- <div class="changeLt"><%=GetTran("007683", "转入人编号")%>：</div>--%>
                        <div class="changeRt">
                           <asp:TextBox  onkeypress="return kpyzsz();" onkeyup="szxs(this);" MaxLength="15"   placeholder="转账对方账号"   ID="txt_InNumber" onblur="getname()" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </li>
                    <li >
                        <div class="changeLt" ><asp:Label ID="lab_nicheng"  runat="server"></asp:Label></div>
                        
                    </li>
                     <li>
                        <%--<div class="changeLt"><%=GetTran("001630", "转出金额")%>：</div>--%>
                        <div class="changeRt">
                            <asp:TextBox  onkeypress="return kpyzsz();" onkeyup="   szxs(this);"    placeholder="FTC数量"   CssClass="form-control" ID="txtEmoney" runat="server"  MaxLength="10"  />
                        </div>
                    </li>
                    <li>
                        <div class="changeRt"  style="line-height:30px;padding-left:10px;">
                            <asp:Label ID="shky" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="bdky" runat="server" Text=""></asp:Label>
                            </div>
                    </li>
                     <%--<li>--%>
                       <%-- <div class="changeLt"><%=GetTran("001632", "二级密码")%>：</div>--%>
                      <%--  <div class="changeRt">
                            <asp:TextBox ID="txtEpwd" placeholder="支付密码"  runat="server" CssClass="form-control" TextMode="Password" MaxLength="15"  ></asp:TextBox>
                        </div>--%>
                    <%--</li>--%>
                    <li style="display:none;">
                        <div class="changeLt">转入账户：</div>
                        <div class="changeRt">
                            <input type="radio" onclick="getname()" value="0" checked="true" name="rad_Inzh" id="rad_xianjin" runat="server" />
                            <asp:Label ID="aa" runat="server" Text=""><asp:Literal ID="lit_xianjin" runat="server"></asp:Literal></asp:Label>
                          
                            <input type="radio" onclick="getname()" value="1" id="rad_xiaofei" name="rad_Inzh" runat="server" />
                             <asp:Label ID="bb" runat="server" Text="">
                            <asp:Literal ID="lit_xiaofei" runat="server"></asp:Literal>
                                 </asp:Label>
                            <input type="radio" onclick="getname()" value="2" id="rad_fuwujigou" name="rad_Inzh" visible="false" runat="server" />
                                <asp:Label ID="cc" runat="server" Text="">
                            <asp:Literal ID="lit_fuwujigou" runat="server"></asp:Literal>
                                    </asp:Label>
                        </div>
                    </li>
                      <li>
                 <%--       <div class="changeLt"><%=GetTran("000078", "备注")%>：</div>--%>
                        <div class="changeRt">
                            <asp:TextBox ID="txtEnote"  onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" onkeyup="ValidateValue(this)"   placeholder="添加备注(50字以内)"  CssClass="form-control"  Height="80px" runat="server" MaxLength="50" />
                        </div>
                    </li>
                    <li>
                        <div class="changeRt">
                            <span>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" meta:resourcekey="RadioButtonList1Resource1" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">FTC转保险</asp:ListItem>
                                <asp:ListItem  Value="2">现金转保险</asp:ListItem>
                                 <asp:ListItem  Value="3">消费转保险</asp:ListItem>
                                    <asp:ListItem  Value="4">保险转保险</asp:ListItem>
                                </asp:RadioButtonList>
                            </span>
                        </div>
                    </li>
                    
                    <li>
                      <%--  <div class="changeLt"><%=GetTran("009009","是否收到")%>：</div>--%>
                        <div class="changeRt"  style="line-height:30px;padding-left:10px;">
                          <span style="float:left;margin-top:5px; margin-right:10px; ">  <asp:CheckBox ID="cboyishd"  runat="server"  /> </span>我已收到该会员支付的上数金额
                            <%--<asp:RadioButtonList  ID="cb_check" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical">
                                <asp:ListItem Value="1" Text="我已收到该会员支付的上数金额" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="我未收到该会员支付的上数金额(如选此项将不能执行转帐)"></asp:ListItem>
                            </asp:RadioButtonList>--%>
                        </div>
                    </li>
                  
                </ul>
            </div>

            <div class="zzBox" id="zzBox" style="margin:10px;margin-bottom:70px;">
                <asp:Button ID="btnE" runat="server"  style="width:100%;"
                    Text="确认转账" CssClass="btn btn-primary" onclick="btnE_Click"  />
            </div>
            <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
        </div>
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
    </form>
</body>
</html>

