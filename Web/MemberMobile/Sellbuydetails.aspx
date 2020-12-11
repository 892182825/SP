<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sellbuydetails.aspx.cs" Inherits="Sellbuydetails" %>

<%@ Register Src="../UserControl/ucPagerMb.ascx" TagName="ucPagerMb" TagPrefix="uc1" %>
<!doctype html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title>交易记录明细</title>
    <link rel="stylesheet" href="CSS/style.css">
    <script type="text/javascript">
        $(function () {
            a.dianji();
        })
        var a = {
            dianji: function () {
                $("#ucPagerMb1").css('display', 'none');
            },
        }

    </script>
    <style>
        .mx
        {
            width: 100%;
        }

        .mx1
        {
            text-align: center;
            font-size: 20px;
            margin-top: 5%;
        }

            .changeRt
        {width:100%; float:left;
        }
          .changeLt
        {width:40%; font-size:14px;float:left;
        }
        .mx2
        {
            float: left;
            width: 25%;
            font-size: 16px;
            margin-top: 5%;
            margin-left: 2%;
        }

        .mx3
        {
            float: right;
            width: 70%;
            font-size: 16px;
            text-align: right;
            margin-top: 5%;
            margin-right: 2%;
        }
    </style>
</head>

<body>
    <form id="form2" runat="server">
        <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="javascript:history.go(-1)"><i class="glyphicon glyphicon-chevron-left glyphicon-white"></i></a>

                <span style="color: #fff; font-size: 18px; margin-left: 30%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">交易记录明细</span>
            </div>
        </div>
        <div class="midls">
            <div class="topshow">
                <p><a class="btn btn-primary">买</a></p>
                <p>
                    <asp:Literal ID="lblttbuy" Text="0.00" runat="server"></asp:Literal>
                </p>
                <p style="font-size: 16px; font-weight: normal;">
                    <asp:Literal ID="litstate" Text="" runat="server"></asp:Literal>
                </p>
                <div class="jindubg">
                    <asp:Literal ID="litdjdut" Text="0.00" runat="server"></asp:Literal>
                </div>
            </div>
            <ul class="buyif">
                <asp:Literal ID="litbuyinfo" Text="" runat="server"></asp:Literal>
                <%--     <li><div class="fdiv">买方账号</div><div class="sdiv">8888888888</div></li>
                 <li><div class="fdiv">买方姓名</div><div class="sdiv">李*文</div></li>
                  <li><div class="fdiv">挂单时间</div><div class="sdiv"> </div></li>
                <li><div class="fdiv">买入石斛积分</div><div class="sdiv">200</div></li>
                <li><div class="fdiv">买入价格</div><div class="sdiv">1.05</div></li>
                <li><div class="fdiv">买入市值</div><div class="sdiv">&yen;210</div></li> --%>
                <%--   <li><input   type="button" runat="server" value="上传转账凭证" style="width: 100%; text-align: center;" class="btn btn-primary btn-lg"      /> </li>--%>
            </ul>

            <asp:Literal ID="lblwdrlist" Text="" runat="server"></asp:Literal>
            <%--   <ul class="sellif">
               <li class="title"><div class="fdiv">第一笔</div><div class="sdiv">交易对方信息 </div> </li>
                  <li><div  >张波  中国银行 泗凤路支行  6555555555555555555 </div> </li>
                  <li><div  >张波  支付宝 1232222@ddd.com </div> </li>
                   <li><div  >张波 微信号 1524444444 </div> </li>--%>
            <%--<li><div class="fdiv">卖方信息</div><div class="sdiv"> </div></li>
                <li><div class="fdiv">卖方账号</div><div class="sdiv">8888888888</div></li>
                <li><div class="fdiv">卖方姓名</div><div class="sdiv">张波</div></li>
                  <li><div class="fdiv">收款类型</div><div class="sdiv">银行卡</div></li>
                <li><div class="fdiv">收款银行</div><div class="sdiv">中国银行</div></li>
                   <li><div class="fdiv">支行名称</div><div class="sdiv">泗凤路支行</div></li>
                     <li><div class="fdiv">收款卡号</div><div class="sdiv">6335444545454445555</div></li>
                   <li><div class="fdiv">支付宝</div><div class="sdiv">aaaa@dd.com</div></li>
                   <li><div class="fdiv">微信</div><div class="sdiv">1525555555</div></li>--%>

            <%--  </ul>--%>
        </div>



 
        <div class="confirmRemit" id="chosbank">
            <asp:HiddenField ID="hidid" Value="0" runat="server" />
            <ul>
                <li>
                    <div onclick="closeremit()" style="text-align: center; width: 100%; font-size: 20px;" >关闭</div>
                </li>
                <li>   <asp:TextBox ID="txthkdesc"  onkeydown="ValidateValue(this)" onblur="ValidateValue(this)" placeholder="添加汇款说明(100字以内)"  CssClass="form-control" TextMode="MultiLine" Height="80px" runat="server"    MaxLength="100"  ></asp:TextBox> 
                </li>
                 <li> <asp:FileUpload ID="FileUpload1" runat="server" />
       
                </li>

                 <li> <asp:Button ID="Button1" style="width: 100%; text-align: center;" CssClass="btn btn-primary btn-lg" runat="server"  Text="提交汇款说明" OnClick="Button1_Click" />
                        
                </li>

            </ul>
        </div>
        <div class="confirmRemit" id="imgback" onclick="hideimg()">
            <img id="imgsrc" style="width:80%;margin-left:10%;"/>
        </div>
         <div id="bakg"></div>
        <!-- #include file = "comcode.html" -->

        <script>
          


            function addhksm(wid) {
                $("#hidid").val(wid);
                $("#bakg").show();
 $("#chosbank").show();
              
            }

            function closeremit() {
                $("#chosbank").hide();
                $("#bakg").hide();
            }
            function showimg(srcc) {
                $("#imgback").show();
                $("#imgsrc").attr('src', srcc);
                $("#bakg").show();
            }

            function hideimg() {
                $("#bakg").hide();
                $("#imgback").hide();
            }

        </script>
    </form>
</body>
</html>

