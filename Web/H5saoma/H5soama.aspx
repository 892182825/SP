<%@ Page Language="C#" AutoEventWireup="true" CodeFile="H5soama.aspx.cs" Inherits="H5saoma_H5soama" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>确认订单­</title>
        <meta content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
        <meta name="Keywords" content="">
        <meta name="Description" content="">
        <!-- Mobile Devices Support @begin -->
        
        <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
        <meta content="no-cache" http-equiv="pragma">
        <meta content="0" http-equiv="expires">
        <meta content="telephone=no, address=no" name="format-detection">
        <meta name="apple-mobile-web-app-capable" content="yes">
        <!-- apple devices fullscreen -->
        <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
        <!-- Mobile Devices Support @end -->
        <link rel="stylesheet" href="css/style.min.css" type="text/css">
        <link rel="stylesheet" href="css/base.min.css">
     
    </head>
	
    <body onselectstart="return true;" ondragstart="return false;" style="user-select: none;">
        <form runat="server">
    <link rel="stylesheet" href="css/payOrder.min.css">

    <style>
        .layui-layer-content {
            max-width: 1.48rem!important;
        }
        .pay-gray{
            background: #e5e5e5!important;
        }
         </style>
 
		      <section class="pay-container-box bg-white pay-simplified">
            <!-- 门店名称展示 -->
            <div class="shop-name-box">
                <i class="default-shop-icon"></i>
                <span class="shop-name-display single-overflow" id="shop-name-display">输入订单信息</span>
            </div>
            <!-- 金额输入（账单金额等） -->
            <div class="input-money-box">
                <div class="js-amount-input pay-amount-box display-flex flex-between-lr flex-horizontal-center s-open-keyboard" data-id="mainMoney">
                    <label class="pay-money-desc" for="">金额</label>
                    <span class="js-input-hint no-pay-amount hide">￥0.00</span>
                    <span class="js-input-amount has-pay-amount">
                        <em>￥</em>
                        <span id="mainMoney" runat="server"  style="font-size:.36rem" ></span>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <em class="pay-money-cursor"></em>
                    </span>
                </div>
            </div>
                  
            <!-- 支付方式 -->
            <div class="pay-way-box">
                <div class="active pay-way-item display-flex flex-between-lr flex-horizontal-center" id="wechatRadio" data-code="Dbank" data-id="907">
                        <span>
                            <i class=""></i>
                            <em class="pay-way-desc">H5支付</em>
                        </span>
                    <i class="select-icon"></i>
                </div>                

            </div>

                  <div class="pay-way-box">
                <div class="active pay-way-item display-flex flex-between-lr flex-horizontal-center" id="Div1" data-code="Dbank" data-id="907">
                        <span>
                            <i class=""></i>
                            <em class="pay-way-desc">账户： <asp:Label ID="MobileTele" runat="server" style=""></asp:Label></em>
                        </span>
                    <i class="select-icon"></i>
                </div>                

            </div>
                  <div class="pay-way-box">
                <div class="active pay-way-item display-flex flex-between-lr flex-horizontal-center" id="Div2" data-code="Dbank" data-id="907">
                        <span>
                            <em class="pay-way-desc">账户余额： <asp:Label ID="Label1" runat="server" style=""></asp:Label></em>
                        </span>
                    <i class="select-icon"></i>
                </div>                

            </div>

            
            <!--备注 部分 start-->
            <div class="s-remark">
                <div class="s-remark-port">
                    <span class="s-remark-title">备注:</span>
                    <input class="s-remark-info" id="remark" type="text" placeholder="请添加备注信息" maxlength="16">
                </div>
            </div>
            <!--备注 部分 end-->

            <!--按钮 部分 start-->
            <%--<div class="s-pay-btn" id="s-pay-btn">确认支付</div>--%>
            <asp:Button ID="spaybtn" runat="server" class="s-pay-btn"
                    Text="确认订单" OnClick="spaybtn_Click" OnClientClick="document.getElementById('HiddenField1').value=document.getElementById('mainMoney').innerText;" />
            <!--按钮 部分 end-->
        </section>

        <!-- 自定义键盘 start 加上x-mask-show显示-->
        <div id="keyBoard" class="x-mask-box x-mask-show" data-id="mainMoney" style="z-index:9;background-color: rgba(0,0,0,0);height:auto;" v-cloak="">
            <div class="x-slide-box pop-up-show">
                <div class="x-key-board">
                    <div class="row">
                        <div class="item js-key" data-number="1">1</div>
                        <div class="item js-key" data-number="4">4</div>
                        <div class="item js-key" data-number="7">7</div>
                        <div class="item js-key" data-number=".">.</div>
                    </div>
                    <div class="row" style="width: 50%">
                        <div class="display-flex">
                            <div class="item js-key" style="width: 50%" data-number="2">2</div>
                            <div class="item js-key" style="width: 50%" data-number="3">3</div>
                        </div>
                        <div class="display-flex">
                            <div class="item js-key" style="width: 50%" data-number="5">5</div>
                            <div class="item js-key" style="width: 50%" data-number="6">6</div>
                        </div>
                        <div class="display-flex">
                            <div class="item js-key" style="width: 50%" data-number="8">8</div>
                            <div class="item js-key" style="width: 50%" data-number="9">9</div>
                        </div>
                        <div class="display-flex">
                            <div class="item js-key" data-number="0">0</div>
                            <div class="item js-key s-pack-key" data-number="down"><i class="keyboard-icon"></i></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="item no-border-right js-key x-key-del" data-number="×">
                            <i class="back-icon"></i>
                        </div>
                        <div class="item no-border-bottom no-border-right x-key-ok" data-number="ok" id="confirm_pay">
                            <span style="line-height: 1.2; font-size: .2rem;" onclick="return KeyDown('spaybtn');" >确<br>认<br>支<br>付</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 自定义键盘 end -->

   
        <!-- form表单 -->
        <form action="" style="display: none;" method="post" id="payForm">
            <input type="text" name="remarks" id="remarks">
            <input type="text" name="amount" id="amount">
            <input type="text" name="mchid" id="mchid" value="191143040">
            <input type="text" name="bankcode" id="bankcode">
    
        </form>
    
  
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="js/layer.js"></script>
    <script type="text/javascript" src="js/payOrderSimpfilied.min.js"></script>
    <script type="text/javascript" src="js/jweixin-1.0.0.js"></script>
    <script>
        var auto_wiping_zero = 1
        $('#s-pay-btn, #confirm_pay').click(function () {

        })

        function KeyDown(btn) {
           
                try {
                    __doPostBack(btn, '');
                    return false;
                }
                catch (e) {
                    alert(e);
                    return;
                }
            
        }

       
    </script>
        </form>
	</body>
 
</html>
