<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReCast.aspx.cs" Inherits="ReCast" %>

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
    <title>购买矿机</title>
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
    </style>

    <script>
      
        
        ///点击购买
        function showbuy(num) {
            $("#shid").show();
            $("#hidetp").val(num);
            var html = "";
            if (num == 1) html += "抢到免费体验矿机！"; 
            if (num == 2) html += "支付50 USDT"; 
            if (num == 3) html += "支付100 USDT";
            if (num == 4) html += "支付500 USDT";
            if (num == 5) html += "支付1000 USDT";
            if (num == 6) html += "支付1500 USDT";
            if (num == 7) html += "支付3000 USDT";
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
            <div id="sbuyinfo"  class="rawz"> </div>
             <div class="radb"><asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" meta:resourcekey="RadioButtonList1Resource1">
                                <asp:ListItem Selected="True" Value="USDT">USDT</asp:ListItem>
                                <asp:ListItem  Value="USDTERC20">USDTERC20</asp:ListItem>
                                 <asp:ListItem  Value="TUSDT">TUSDT</asp:ListItem>
                                    
                                </asp:RadioButtonList></div>
            <div class="radgm"> <asp:HiddenField ID="hidetp" runat="server"  Value="0"/>
                <input type="button" class="canc" value="取消" onclick="hidediv()" />
                <asp:Button ID="Button1" CssClass="qr" runat="server" Text="确认购买" OnClick="Button1_Click" /> 
            </div>
        </div>

        <div style="margin-left:10%;margin-top:40px;margin-bottom:40px;"><h2 style="color:#fff">购买矿机</h2></div>

        <div id="getshow" runat="server" class="buylist">
             
        </div>  


    
    </div>

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
