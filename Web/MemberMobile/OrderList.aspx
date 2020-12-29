<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="OrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link rel="stylesheet" href="css/style.css">
     
    <script src="../bower_components/jquery/jquery.min.js"></script>
    <title>矿机列表</title>
    <style>
                ul li {
       list-style:none; 
       margin-bottom:5%;
        }

        .web_toast {
    position: fixed;
    margin: 150px 10px;
    z-index: 9999;
    display: none;
    display: block;
    padding: 10px;
    color: #FFFFFF;
    background: rgba(0, 0, 0, 0.7);
    font-size: 1.4rem;
    text-align: center;
    border-radius: 4px;
}
        .btn-qianse {
            background-color: #e1e3e6;
            color:#000;
        }
        .dsc2 .actv{
          background-color:#00ff90; width:100px; height:50px;
          font-size:20px; font-weight:bold; font-family:黑体;
        }
    </style>

    <script>
      
        
        ///点击购买
        function showbuy(num) {
            $("#shid").show();
            var blc=  $("#hideye").val();
           var nde= $("#hidpayE").val(); 
            var html = "需支付E币" + nde + "<br/> 余额 " + blc;
            $("#sbuyinfo").html(html);
        }

        function hidediv() {
            $(".showhid").hide();
        }


        function showsuc(remark) {
            $("#shscid").show();
            $("#sbuyinfo1").html(remark);
        } 
       function hidediv1() {
            $("#shscid").hide();
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        
    <div>
 
         <div id="shscid" class="showhid">
            <div id="sbuyinfo1"  style=""> </div>
            <div> 
                <input type="button" class="qr"  value="关闭" onclick="hidediv1()" /> 
            </div>
        </div>

         <div id="shid" class="showhid">
            <div id="sbuyinfo"  style=""> </div>
            <div> <asp:HiddenField ID="hidetp" runat="server"  Value="0"/>
                 <asp:HiddenField ID="hidpayE" runat="server"  Value="0"/>
                <input type="button" class="canc" value="取消" onclick="hidediv()" />
                <asp:Button ID="Button1" CssClass="qr" runat="server" Text="确认激活" OnClick="Button1_Click" /> 
            </div>
        </div>

        <div style="margin-left:10%;margin-top:40px;margin-bottom:40px;"><h2 style="color:#fff">已买矿机</h2></div>

        <div id="getshow" runat="server" class="buylist">
             
        </div>  


    
    </div>
         
    </form>
    

   
</body>
   
</html>
