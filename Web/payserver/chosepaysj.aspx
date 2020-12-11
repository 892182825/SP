<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chosepaysj.aspx.cs" Inherits="chosepay" %>

<%@ Register Src="../UserControl/paytop.ascx" TagName="paytop" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/paybottom.ascx" TagName="paybottom" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=GetTran("000907","订单支付") %></title>
    <link href="../css/buy.css" rel="stylesheet" type="text/css" />
    <link href="../Member/CSS/style.css" rel="stylesheet" type="text/css" />

    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <script src="..membermobile/js/jquery-1.7.1.min.js"></script>

    <link rel="stylesheet" href="../MemberMobile/css/style.css" />

    <style type="text/css">
        .xs_footer li a{display:block;padding-top:40px;background:url(img/shouy1.png) no-repeat center 8px;background-size:32px;}
        .xs_footer li:nth-of-type(2) a{background:url(img/jiangj1.png) no-repeat center 10px;background-size:32px;}
        .xs_footer li:nth-of-type(3) a{background:url(img/xiaoxi1.png) no-repeat center 8px;background-size:32px;}
        .xs_footer li:nth-of-type(4) a{background:url(img/anquan1.png) no-repeat center 8px;background-size:27px;}

        .buyfont
        {
            line-height:1
        }
        #div_sure input[type='radio']
        {
            float: right;
            position:inherit;
            margin-top: 8px;
            width:5%
        }
        #div_sure label
        {
            float: left;
            width:95%;
            padding-left:40px;
            line-height:24px
        }
        #rdoaccounttype label
        {
            float:left;
            margin-left:3px
        }
        #rdoaccounttype input
        {
            margin-left:5px
        }
        .zfInfo ul li input[type='radio'] {
            right:0
        }
        #rdoectpay,#rdostorepay,#rdorempay {
            right:22px
        }
        .inpss {
            padding:0 0 0 5px
        }
        .zfInfo ul li p {
            float:none;
            overflow:hidden;
            padding-left:60px;
            width:100%
        }
    </style>
    <script src="../JS/jquery.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function checkRad(id) {
            for (var i = 1; i <= 4; i++) {
                if (id == i) {
                    $("#div" + i).addClass("checkStyle");
                } else {
                    $("#div" + i).removeClass("checkStyle");
                }
            }
        }

        function checkThis(obj, os) {
            var ptype = $("#paytype");

            for (var i = 1; i <= 6; i++) {
                if ($(obj).attr("id") == "div_" + i) {
                    $("#div" + i).addClass("checkStyle");
                    switch (i) {
                        case 1: ptype.html('<%=GetTran("007444","电子货币支付") %>');
                            break;

                        case 2: ptype.html('<%=GetTran("007446","去服务机构支付") %>');
                            break;

                        case 3: ptype.html('<%=GetTran("005963","在线支付") %>');
                            break;

                        case 4: ptype.html('<%=GetTran("007447","离线支付") %>');
                            break;
                        case 5: ptype.html('<%=GetTran("007448","服务机构支付会员订单") %>');
                            break;
                        case 6: ptype.html('<%=GetTran("007449","服务机构支付订货单") %>');
                            break;
                        default: ptype.html('<%=GetTran("000303","请选择") %>');
                    }
                    $("#menupt_" + i).show("normal");
                } else {
                    $("#div" + i).removeClass("checkStyle");
                    $("#menupt_" + i).hide("fast");

                }
            }
            $(obj).find("input").first().attr("checked", "checked");
        }

        function remberBankname(bkname) {
            $("#hidbankName").val(bkname);
        }
    </script>
</head>
<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
    <form id="form1" runat="server">
         <div class="t_top">	
        <a class="backIcon" href="../MemberMobile/AuditingMemberOrder.aspx"></a>
            <%=GetTran("000907","订单支付") %>
    </div>
    <div class="ddInfo">
        <ul>
            <li style="background:none;min-height: 28px;"><p style="line-height:100%;"><%=GetTran("000456","支付类型") %>：<asp:Label ID="lbltype" CssClass="buyfont" runat="server" Text="报单支付"></asp:Label></p></li>
            <li style="background:none;min-height: 28px;"><p style="line-height:100%;"><%=GetTran("005611","订单编号") %>：<asp:Label CssClass="buyfont" ID="lblorderid" runat="server" Text="000000"></asp:Label></p></li>
            <li class="paymInfo"><p><b class="TranToValue" style="font-weight: normal;font-weight: 700;"><%=GetTran("000789","支付金额") %></b>：<asp:Label CssClass="buyfont" ID="lbltotalmoney" runat="server" Text="0.00"></asp:Label> <font class="hF"></font> </p></li>
        </ul>
    </div>    
    <div class="zfInfo">
        <ul>
            <li class="zfInfo_3" style="height:auto">
                <div class="buyChoice" onclick="checkThis(this,1);" id="div_1" runat="server" style="position:relative;overflow:hidden">
                    <p><font style="font-weight:700;color:#333;font-size:16px;"><%=GetTran("007444", "电子货币支付")%></font></p>
                    <p style="padding-top:0px;padding-bottom:4px;"><i><%=GetTran("009093","推荐有电子币的用户使用") %></i></p>
                    <asp:RadioButton ID="rdoectpay" GroupName="p" runat="server" />
                </div>
                <div id="menupt_1" class="choselist" style="overflow:hidden" >
                    <p>
                        <span style="width:auto;height:auto"><%=GetTran("007470", "支付编号")%>：</span><asp:Label ID="lblordernumber" runat="server" Text="XXXX" style="width:auto;height:auto"></asp:Label>
                    </p>
                    <p id="p1" runat="server">
                        <span style="width:auto;height:auto;float:left"> <%=GetTran("007471", "账户类型")%>：</span>
                                           
                        <asp:RadioButtonList RepeatLayout="Flow" ID="rdoaccounttype" runat="server" RepeatDirection="Horizontal" style="width:auto;height:auto;float:left">
                            <asp:ListItem Selected="True" Text="现金账户" Value="1"></asp:ListItem>
                            <asp:ListItem Text="消费账户" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                                           
                    </p>
                                         
                    <p id="p2" style="float: left; display: none" runat="server">
                        <span style="width:auto;height:auto">
                            <%=GetTran("007471", "账户类型")%>：
                        </span>
                        <asp:RadioButtonList RepeatLayout="Flow" style="display: none ;width:auto;height:auto;"  ID="rdoaccounttype2"  runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Text="复消账户" Value="2" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </p>

                    <p id="p3" style="float: left; display: none" runat="server">
                        <span style="width:auto;height:auto">
                        <%=GetTran("007471", "账户类型")%>： </span>
                        <asp:RadioButtonList RepeatLayout="Flow" style="display: none;width:auto;height:auto;float:left" ID="rdoaccounttype3"  runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Text="复消提货账户" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </p>

                    <div style="margin-left:60px;height:50px">
                        <span class="sp1" style="width:70px;"><%=GetTran("001538","二级密码")%>：</span>
                        <asp:TextBox ID="txtadvpass" TextMode="Password" CssClass="inpss"
                            MaxLength="50" runat="server" style="float:left;width:65%"></asp:TextBox>
                    </div>
                    <p runat="server" id="div_sure" style="width:100%">
                        <asp:RadioButtonList ID="rdombsuregetmoney" RepeatLayout="Flow" runat="server" RepeatDirection="Horizontal" Width="100%" style="display:block;width:100%; padding: 0 5%;line-height: 24px;">
                            <asp:ListItem Text="我确认已收到该会员支付的上数金额" Value="1"></asp:ListItem>
                            <asp:ListItem Text="我暂未收到该会员支付的上数金额" Selected="True" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </p>
                </div>
            </li>
            <li class="zfInfo_4">
                <div class="buyChoice" onclick="checkThis(this,2);" id="div_2" runat="server">
                    <p><font style="font-weight:700;color:#333;font-size:16px;"><%=GetTran("007446","去服务机构支付")%></font></p>
                    <p style="padding-top:0px;padding-bottom:4px;"><i><%=GetTran("009094","将现金交给即将为您支付的服务机构即可") %></i></p>
                    <asp:RadioButton ID="rdostorepay" GroupName="p" runat="server"/>
                    <div id="menupt_2" class="choselist" style="overflow:hidden;width:100%">
                        <p>
                            <%=GetTran("007472","请将现金")%><%=huilv==1?"$":"￥"%> <asp:Label ID="lblordertmoney" CssClass="bt" Font-Size="20px" runat="server"
                                Text="0.00" style="width:auto;height:auto;color:red;float:none;display:inline-block"></asp:Label><%=GetTran("007473", "交给即将为您支付的服务机构")%>。
                        </p>
                    </div>
                </div>
            </li>
   		 </ul>
	</div>	
	<div class="zfInfo">
    	<ul>
            <li class="zfInfo_5" style="">
                <div class="buyChoice" onclick="checkThis(this,4);" id="div_4" runat="server">
                    <p><font style="font-weight:700;color:#333;font-size:16px;"><%=GetTran("9111","转账汇款支付")%></font></p>
                    <p style="padding-top:0px;padding-bottom:4px;"><i><%=GetTran("009096","向指定账户汇款") %></i></p>
                <asp:RadioButton ID="rdorempay" GroupName="p" runat="server" />                  
                <div id="menupt_4" class="choselist">
                    <div id="Div5" style="font-size: 12px" runat="server">
                        <p>
                            <%=GetTran("007498", "请向以下帐户汇款")%>：<b style="font-size: 20px; color: #000;">
                                <asp:Label ID="lblrmb" ForeColor="red" Font-Size="20px" runat="server" Text="0.00"></asp:Label></b><%--<%=GetTran("000564","元")%>--%>。<br />
                        </p>
                        <p  style="display:none">
                            <%=GetTran("007500","您必须重视")%><asp:Label ID="lblmoneyre" runat="server" ForeColor="red" Font-Size="18px" Font-Bold="true"
                                Text="0.00"></asp:Label>
                            <%=GetTran("007501", "中的尾数")%>
                            <asp:Label ID="lblchat" ForeColor="red" Font-Size="18px" Font-Bold="true" runat="server"
                                Text="0.00"></asp:Label>，<%=GetTran("007502", "只有当公司收到")%><asp:Label ID="lblpartmoney" ForeColor="red" Font-Bold="true"
                                    Font-Size="20px" runat="server" Text="0"></asp:Label><%=GetTran("000564","元")%><asp:Label ID="lbljiao" Font-Bold="true"
                                        ForeColor="red" Font-Size="20px" runat="server" Text="0"></asp:Label><%=GetTran("007499","角")%><asp:Label
                                            ID="lblfen" runat="server" Font-Bold="true" ForeColor="red" Font-Size="20px"
                                            Text="0"></asp:Label><%=GetTran("007503","分")%><%=GetTran("007504", "的汇款时")%>， <%=GetTran("007505", "您本次的支付才能完成。无论是多汇一分还是少汇一分都将对确认带来不必要的麻烦")%>。<br />
                        </p>
                        <%--<p>详情请阅<a name="regMsg" href="InternationalNotes.aspx">《国际汇兑须知》</a></p>--%>
                        <br />
                        <div id="cardlist" runat="server" style="font-size: 16px; font-weight: bold; overflow: hidden; width: 100%;">
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            </li>
        </ul>
    </div>

    <div class="myInfo" style="display:none">
    <ul>
        <li>
            <span><b class="TranToValue" style="font-weight: normal;"><%=GetTran("009095","支付密码") %></b>：</span>
            <span><input id="pwd" style="height:70%;width:90%;border: 1px solid #ccc;font-size:15px;background: #f8f8f8;" type="password" /></span>
        </li>
    </ul>
</div>
<div class="zfTag">
    <asp:Button ID="btnsure"  runat="server" Text="支  付" OnClick="btnsure_Click" style="width:90%;margin-left:5%;height:40px;border-radius:3px;background:#77c225;color:#fff;font-size:16px;font-family:'微软雅黑'" />
</div>
<div class="xs_footer">
    <ul>
    	<li>	
            <a href="../Membermobile/first.aspx">	
            	<span><%=GetTran("001478","首页")%></span>
            </a>	
        </li>
    	<li>	
            <a href="../Membermobile/BasicSearch.aspx">	
            	<span><%=GetTran("000243","奖金")%></span>
            </a>	
        </li>
    	<li>	
            <a href="../Membermobile/ReceiveEmail.aspx">	
            	<span><%=GetTran("009028","消息")%></span>
            </a>	
        </li>
    	<li>	
            <a href="../PassWordManage/CheckAdv1.aspx?type=member&url=updatePass">	
            	<span><%=GetTran("009029","安全")%></span>
            </a>	
        </li>
    </ul>
</div>
        	
        <div class="MemberPage" style="display:none">
            <!--顶部信息,logo,help-->
            <div class="pay">
                <uc1:paytop ID="paytop1" runat="server" />
                <div class="payTitle">
                    <div style="display:none ">
                    <asp:HiddenField ID="hdcheck" runat="server" Value="1" />
                    <asp:HiddenField ID="hdbank" runat="server" Value="1" />
                        </div>
                    <ul>
                        <li><%=GetTran("007450","本次支付类型")%>：
                      
                        </li>
                        <li><%=GetTran("007459", "当前需要支付")%><asp:Label ID="lblot" CssClass="buyfont" runat="server" Text="订单号"></asp:Label><%=GetTran("007460","为")%>
                           </li>
                        <li><%=GetTran("000041","总金额")%>：
                            <span id="tpv" style="" visible="false" runat="server"><%=GetTran("007463","获取总PV为")%>
                                <asp:Label ID="lbltotalpv" CssClass="buyfont" runat="server" Text="0"></asp:Label></span>
                        </li>
                    </ul>
                   
                </div>
                <h1 class="butTitle">
                    <%=GetTran("000186","支付方式")%>：<span id="paytype"><%=GetTran("000303","请选择")%></span></h1>
                <div class="payBuy">
                    <!--支付选择-->
                    <div class="buyChoice" onclick="checkThis(this,5);" id="div_5" runat="server">
                        <table>
                            <tr>
                                <th style="cursor: pointer; text-align: left;">
                                    <h3>
                                        <asp:RadioButton ID="rdostpaymb" GroupName="p" runat="server" />
                                        <%=GetTran("007464","支付会员报单")%>：</h3>
                                </th>
                                <%--  <td style="cursor: pointer;">
                                目前银行卡支付平台支持的银行卡有：中国银行、中国工商银行、中国招商银行、中国建设银行、上海浦东发展银行、中国农业银行、中国交通银行、华夏银行、商业银行、深圳发展银行、民生银行、广东发展银行、中国邮政、福建兴业银行、中信银行、农村信用合作社，但每种银行卡使用的范围和到帐时间有所不同，具体使用情况和申请步骤参看首都电子支付平台的说明。
                            </td>--%>
                            </tr>
                            <tr>
                                <td>
                                    <div id="menupt_5" class="choselist">
                                        <p>
                                            <%=GetTran("007465", "支付服务机构编号")%>：&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblstoreID2" runat="server" Text="XXXX"></asp:Label>
                                        </p>
                                        <p style="float: left; display: none;">
                                            <%=GetTran("007466", "支付账户类型")%>：&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButtonList ID="rdostactypepaymb" runat="server" RepeatDirection="Horizontal"
                                            Width="150px" Visible="false">
                                            <asp:ListItem Text="周转款" Value="11"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="订货款" Value="10"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </p>
                                        <p>
                                            <%=GetTran("001538","二级密码")%>：&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtpayadbpass" TextMode="Password"
                                                CssClass="inpss" MaxLength="50" runat="server"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:RadioButtonList ID="rdoisagree" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="我确认已收到该会员支付的上数金额" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="我暂未收到该会员支付的上数金额" Selected="True" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--支付选择-->
                    <div class="buyChoice" onclick="checkThis(this,6);" id="div_6" runat="server">
                        <table>
                            <tr>
                                <th style="cursor: pointer; text-align: left;">
                                    <h3>
                                        <asp:RadioButton ID="rdostopayorder" GroupName="p" runat="server" />
                                        <%=GetTran("007469", "支付订货单")%>：</h3>
                                </th>
                                <%--  <td style="cursor: pointer;">
                                目前银行卡支付平台支持的银行卡有：中国银行、中国工商银行、中国招商银行、中国建设银行、上海浦东发展银行、中国农业银行、中国交通银行、华夏银行、商业银行、深圳发展银行、民生银行、广东发展银行、中国邮政、福建兴业银行、中信银行、农村信用合作社，但每种银行卡使用的范围和到帐时间有所不同，具体使用情况和申请步骤参看首都电子支付平台的说明。
                            </td>--%>
                            </tr>
                            <tr>
                                <td>
                                    <div id="menupt_6" class="choselist">
                                        <p>
                                            <%=GetTran("007465", "支付服务机构编号")%>：&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblstoreid1" runat="server" Text="XXXX"></asp:Label>
                                        </p>
                                        <p style="float: left;">
                                            <%=GetTran("007466", "支付账户类型")%>：&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButtonList ID="rdostaccount" runat="server" RepeatDirection="Horizontal"
                                            Width="265px">
                                            <asp:ListItem Selected="True" Text="周转款" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="订货款" Value="10"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </p>
                                        <p>
                                            <%=GetTran("001538","二级密码")%>：&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtstadvpass" TextMode="Password" CssClass="inpss"
                                                MaxLength="50" runat="server"></asp:TextBox>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--支付选择--------------------------------------电子货币支付-------------------->
            
                           <div class="buyChoice" onclick="checkThis(this,1);" id="div1" runat="server" style="display:none">
                        <table>
                            <tr>
                                <th style="cursor: pointer; text-align: left;">
                                    <h3>
                                       </h3>
                                </th>
                                <%--<td style="cursor: pointer;">
                                快速结账按钮为买家提供多一个付款途径，为现有的付款方案带来更多选择。网上买家喜欢既方便又具安全性的PayPal，从此，他们可通过他们PayPal账户的余额、银行账户或信用卡付款。
                            </td>--%>
                            </tr>
                            <tr>
                                <td>
                                  
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--支付选择--------------------------------------去服务机构支付--------------------->
                  
                          <div class="buyChoice" onclick="checkThis(this,2);" id="div2" runat="server">
                        <table>
                            <tr>
                                <th style="cursor: pointer; text-align: left;">
                                    <h3>
                                     </h3>
                                </th>
                                <%--  <td style="cursor: pointer;">
                                目前银行卡支付平台支持的银行卡有：中国银行、中国工商银行、中国招商银行、中国建设银行、上海浦东发展银行、中国农业银行、中国交通银行、华夏银行、商业银行、深圳发展银行、民生银行、广东发展银行、中国邮政、福建兴业银行、中信银行、农村信用合作社，但每种银行卡使用的范围和到帐时间有所不同，具体使用情况和申请步骤参看首都电子支付平台的说明。
                            </td>--%>
                            </tr>
                            <tr>
                                <td>
                                
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--支付选择-->
                    <div class="buyChoice" onclick="checkThis(this,3);" id="div_3" runat="server" style="display: none;">
                        <table>
                            <tr>
                                <th style="cursor: pointer; text-align: left;">
                                    <h3>
                                        <asp:RadioButton ID="rdoonlinepay" GroupName="p" runat="server" />&nbsp;<%=GetTran("005963","在线支付") %>：</h3>
                                    <%--这种支付方式，是通过专用支付平台（您必须开通网银或无卡支付功能），在线所完成的支付。--%>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <div id="menupt_3" class="choselist" style="display:none">
                                        <!-- 支付宝合作银行列表 -->
                                        <div class="fn-mb-30 kj-area">
                                            <span class="rdobank" id="alipayspan" runat="server" style="display: none;">
                                                <asp:RadioButton ID="rdoaliypay" GroupName="alipaybank" Text="" runat="server" /></span>
                                            <img src="../images/alipay.gif" id="appic" runat="server" style="display: none;"
                                                class="pticon" alt='<%=GetTran("000968","支付宝") %>' />
                                            <span class="rdobank" id="sftspan" runat="server" style="display: none;">
                                                <asp:RadioButton ID="rdosft" Text="" GroupName="alipaybank" runat="server" /></span>
                                            <img src="../images/shengfutong.png" id="sftpic" runat="server" style="display: none;"
                                                class="pticon" alt='<%=GetTran("007474","盛付通支付") %>' />
                                            <span class="rdobank" id="hxpayspan" runat="server" style="display: none;">
                                                <asp:RadioButton ID="rdohxpay" Text="" GroupName="alipaybank" runat="server" /></span>
                                            <img src="../images/hxpaylogo.gif" id="hxpic" runat="server" style="display: none;"
                                                class="pticon" alt='<%=GetTran("007475","环讯支付") %>' />
                                            <span class="rdobank" id="qkpayspan" runat="server" style="display: none;">
                                                <asp:RadioButton ID="rdoquickpay" Text="" GroupName="alipaybank" runat="server" /></span>
                                            <img src="../images/qucpay.jpg" id="qkpic" runat="server" style="display: none;"
                                                class="pticon" alt='<%=GetTran("001414","快钱支付") %>' />
                                        </div>
                                        <div class="ajax-DEbank" id="banklist" style="display: none;" runat="server">
                                            <h5>
                                                <%=GetTran("007476","网上银行")%>： <span class="ft-12"><%=GetTran("007477","需要开通网上银行")%>。</span>
                                                <asp:HiddenField ID="hidbankName" Value="ICBCB2C" runat="server" />
                                            </h5>
                                            <div class=" ">
                                                <ul class="ui-list-icons">
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="rdoapbankicbc" runat="server" GroupName="alipaybank" onclick="remberBankname('ICBCB2C')" /></span>
                                                        <span class="icon ICBC" title='<%=GetTran("007478","中国工商银行")%>'><%=GetTran("007478","中国工商银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="alipaybank" onclick="remberBankname('CCB')" /></span>
                                                        <span class="icon CCB" title='<%=GetTran("007479","中国建设银行")%>'><%=GetTran("007479","中国建设银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="alipaybank" onclick="remberBankname('ABC')" /></span>
                                                        <span class="icon ABC" title='<%=GetTran("007480","中国农业银行")%>'><%=GetTran("007480","中国农业银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton4" runat="server" GroupName="alipaybank" onclick="remberBankname('COMM')" /></span>
                                                        <span class="icon COMM" title='<%=GetTran("007481","交通银行")%>'><%=GetTran("007481","交通银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton5" runat="server" GroupName="alipaybank" onclick="remberBankname('CMB')" /></span>
                                                        <span class="icon CMB" title='<%=GetTran("007482","招商银行")%>'><%=GetTran("007482","招商银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton6" runat="server" GroupName="alipaybank" onclick="remberBankname('BOCB2C')" /></span>
                                                        <span class="icon BOC" title='<%=GetTran("007483","中国银行")%>'><%=GetTran("007483","中国银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton7" runat="server" GroupName="alipaybank" onclick="remberBankname('CEBBANK')" /></span>
                                                        <span class="icon CEB" title='<%=GetTran("007484","中国光大银行")%>'><%=GetTran("007484","中国光大银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton8" runat="server" GroupName="alipaybank" onclick="remberBankname('CITIC')" /></span>
                                                        <span class="icon CITIC" title='<%=GetTran("007485","中心银行")%>'><%=GetTran("007485","中心银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton10" runat="server" GroupName="alipaybank" onclick="remberBankname('SPDB')" /></span>
                                                        <span class="icon SPDB" title='<%=GetTran("007486", "上海浦东发展银行")%>'><%=GetTran("007486", "上海浦东发展银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton11" runat="server" GroupName="alipaybank" onclick="remberBankname('CMBC')" /></span>
                                                        <span class="icon CMBC" title='<%=GetTran("007487","中国民生银行")%>'><%=GetTran("007487","中国民生银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton12" runat="server" GroupName="alipaybank" onclick="remberBankname('CIB')" /></span>
                                                        <span class="icon CIB" title='<%=GetTran("007488","兴业银行")%>'><%=GetTran("007488","兴业银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton13" runat="server" GroupName="alipaybank" onclick="remberBankname('SPABANK')" /></span>
                                                        <span class="icon SPABANK" title='<%=GetTran("007489","平安银行")%>'><%=GetTran("007489","平安银行")%></span> </li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton14" runat="server" GroupName="alipaybank" onclick="remberBankname('GDB')" /></span>
                                                        <span class="icon GDB" title='<%=GetTran("007490","广发银行")%> '><%=GetTran("007490","广发银行")%> </span></li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton15" runat="server" GroupName="alipaybank" onclick="remberBankname('NBBANK')" /></span>
                                                        <span class="icon NBBANK" title='<%=GetTran("007491","宁波银行")%>'><%=GetTran("007491","宁波银行")%> </span></li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton16" runat="server" GroupName="alipaybank" onclick="remberBankname('BJRCB')" /></span>
                                                        <span class="icon BJRCB" title='<%=GetTran("007492", "北京农商银行")%>'><%=GetTran("007492", "北京农商银行")%> </span></li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton17" runat="server" GroupName="alipaybank" onclick="remberBankname('HZCBB2C')" /></span>
                                                        <span class="icon HZCB" title='<%=GetTran("007493","杭州银行")%>'><%=GetTran("007493","杭州银行")%> </span></li>
                                                    <li><span class="rdobank">
                                                        <asp:RadioButton ID="RadioButton19" runat="server" GroupName="alipaybank" onclick="remberBankname('SHRCB')" /></span>
                                                        <span class="icon SHRCB" title='<%=GetTran("007494", "上海农商银行")%>'><%=GetTran("007494", "上海农商银行")%></span> </li>
                                                    <!-- 各支付方式特有银行 隐藏-->
                                                    <!-- 各支付方式特有银行 支付宝-->
                                                    <li id="RadioBJBANK1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioBJBANK" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('BJBANK')" /></span> <span class="icon BJBANK" title="北京银行">
                                                                <%=GetTran("007495","北京银行")%> </span></li>
                                                    <li id="RadioSDB1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioSDB" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('SDB')" /></span> <span class="icon SDB" title="深圳发展银行"><%=GetTran("007496", "深圳发展银行")%></span>
                                                    </li>
                                                    <li id="RadioPSBC1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioPSBC" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('PSBC-DEBIT')" /></span> <span class="icon PSBC" title="中国邮政储蓄银行">
                                                                <%=GetTran("007497", "中国邮政储蓄银行")%></span> </li>
                                                    <li id="RadioFDB1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioFDB" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('FDB')" /></span> <span class="icon FDB" title="富滇银行">富滇银行</span>
                                                    </li>
                                                    <!-- 各支付方式特有银行 盛付通-->
                                                    <li id="RadioHXB1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioHXB" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('HXB')" /></span> <span class="icon HXB" title="华夏银行">华夏银行
                                                            </span></li>
                                                    <li id="RadioBOS1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioBOS" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('BOS')" /></span> <span class="icon BOS" title="上海银行">上海银行
                                                            </span></li>
                                                    <li id="RadioCBHB1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioCBHB" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('CBHB')" /></span> <span class="icon CBHB" title="渤海银行">渤海银行
                                                            </span></li>
                                                    <li id="RadioHKBEA1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioHKBEA" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('HKBEA')" /></span> <span class="icon HKBEA" title="东亚银行">东亚银行
                                                            </span></li>
                                                    <li id="RadioGZCB1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioGZCB" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('GZCB')" /></span> <span class="icon GZCB" title="广州银行">广州银行
                                                            </span></li>
                                                    <li id="RadioHKBCHINA1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioHKBCHINA" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('HKBCHINA')" /></span> <span class="icon HKBCHINA" title="汉口银行">汉口银行 </span></li>
                                                    <li id="RadioNJCB1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioNJCB" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('NJCB')" /></span> <span class="icon NJCB" title="南京银行">南京银行
                                                            </span></li>
                                                    <li id="RadioWZCB1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioWZCB" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('WZCB')" /></span> <span class="icon WZCB" title="温州银行">温州银行
                                                            </span></li>
                                                    <%--                                                <li id="RadioSDE1" runat="server" visible="false"><span class="rdobank">
                                                    <asp:RadioButton ID="RadioSDE" runat="server" Visible="false" GroupName="alipaybank"
                                                        onclick="remberBankname('SDE')" /></span> <span class="icon SDE" title="顺德农村信用社">顺德农村信用社
                                                        </span></li>
                                                <li id="RadioZHNX1" runat="server" visible="false"><span class="rdobank">
                                                    <asp:RadioButton ID="RadioZHNX" runat="server" Visible="false" GroupName="alipaybank"
                                                        onclick="remberBankname('ZHNX')" /></span> <span class="icon ZHNX" title="珠海市农村信用合作社联社">
                                                            珠海市农村信用合作社联社 </span></li>
                                                <li id="RadioYDXH1" runat="server" visible="false"><span class="rdobank">
                                                    <asp:RadioButton ID="RadioYDXH" runat="server" Visible="false" GroupName="alipaybank"
                                                        onclick="remberBankname('YDXH')" /></span> <span class="icon YDXH" title="尧都信用合作社联社">
                                                            尧都信用合作社联社 </span></li>--%>
                                                    <li id="RadioCZCB1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioCZCB" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('CZCB')" /></span> <span class="icon CZCB" title="浙江稠州商业银行">浙江稠州商业银行 </span></li>
                                                    <li id="RadioGNXS1" runat="server" visible="false"><span class="rdobank">
                                                        <asp:RadioButton ID="RadioGNXS" runat="server" Visible="false" GroupName="alipaybank"
                                                            onclick="remberBankname('GNXS')" /></span> <span class="icon GNXS" title="广州市农村信用合作社">广州市农村信用合作社 </span></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div id="quickpaybank">
                                            <!-- 快钱合作银行列表 -->
                                        </div>
                                        <div id="huanxunpaybank">
                                            <!-- 环迅合作银行列表 -->
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--支付选择-->
                  
                         <div class="buyChoice" onclick="checkThis(this,4);" id="div3" runat="server">
                        <table>
                            <tr>
                                <th style="cursor: pointer; text-align: left;">
                                    <h3></h3>
                                        
                                </th>
                                <%--<td style="cursor: pointer;">
                                已开通“快钱”、“支付宝”、“一网通支付”等多家支付网关，支持包括招商银行在内的所有已开通网银功能的银行卡，可实时到帐。一网通网上支付是招商银行提供的网上即时付款服务。支持中、农、工、建、交行、中信、深发、光大、广发等22家银行借记卡、贷记卡、信用卡网上在线支付功能，可实时到帐。
                            </td>--%>
                            </tr>
                            <tr>
                                <td>
                                   
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <uc2:paybottom ID="paybottom1" runat="server" />
            </div>
        </div>
</form>
<script>
    $(function () {
         
        $('#rdoaccounttype_0').css({ 'position': 'relative', 'float': 'left', 'margin-top': '3px' })
        $('#rdoaccounttype_1').css({ 'position': 'relative', 'float': 'left', 'margin-top': '3px' })
        $('#rdoaccounttype2_0').css({ 'position': 'inherit', 'float': 'left', 'margin-top': '4px','margin-right': '6px' })
        $('#rdostorepay').css({ 'top': '28px' })
        $('#rdorempay').css({ 'top': '28px' })
            
        $('#menupt_4 span').css({ 'float': 'none', 'display': 'inline' })
        //$('#menupt_4 p').css({ 'marginLeft': '60px' })
        $('#menupt_4 #bank1').css({ 'marginLeft': '60px' })

    })
        
</script>
<script type="text/javascript">
    $(function () {
        var lang = $("#lang").text();
        if (lang != "L001") {
            $(".sp1").css('width', '80px');
            $('#bank1 div span').css({ 'display': 'block','width':'100%','height':'auto','margin':'0' })
            $('#bank1 div').css('overflow','hidden')
        }
    })
</script>
</body>
</html>
<style>
    .buyfont {
        font-size: 16px;
    }
    #lbltotalmoney {
        font-size: 16px;
        color:red;
    }
</style>