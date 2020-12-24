<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanteDH.aspx.cs" Inherits="PlanteDH" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <script src="js/jquery-1.7.1.min.js"></script>
    <title>Ecoin兌換</title>
    <link rel="stylesheet" href="CSS/style.css">
     <script type="text/javascript">
        
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
        function countcsb() {
            var buy = $("#txtneed").val(); 
            if (!isNaN(buy)) { 
                var dj = $("#hiddj").val();
                var cr = parseInt( buy  * dj*10000)/10000;
                $("#uu").html(cr);
            }else 
            $("#uu").val("0.0000");
             
        } 
        function showsuc(remark) {
            $("#shscid").show();
            $("#sbuyinfo1").html(remark);
        }
        function hidediv1() {
            $("#shscid").hide();
        }
    </script>
    <style>
        .timg { width:100%;  height:250px; background-image:url(img/csj.png);  background-size:cover;  background-repeat:no-repeat;      }

        .dq { font-size:16px; background-color:#111622; padding:10px; height:500px; }
        .dq p { color:#808080;   padding-left:20px;  margin-top:5px; line-height:30px; height:30px; }
            .dq p input { border:none; border-bottom:1px solid #999;
           color:#fff; line-height:40px; width:100%; background:#111622; height:30px; }

        #uu {border:none;
           color:#fff; line-height:40px; width:150px; background:#111622; height:30px; font-size:12px;
        }
        .busub { color:#fff; margin:auto; width:80%; border-radius:20px; height:40px;line-height:40px; font-size:18px;	background: -webkit-gradient(linear, left top, left bottom, from(#0E4A93), to(#5F0BC1));
	background: -moz-linear-gradient(top,  #0E4A93,  #5F0BC1);
	filter:  progid: DXImageTransform.Microsoft.gradient(startColorstr='#0E4A93', endColorstr='#5F0BC1');
  }
        .btdiv {  width:100%; margin-top:30px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div id="shscid" class="showhid">
            <div id="sbuyinfo1"  style=""> </div>
            <div> 
                <input type="button" class="qr"  value="確定" onclick="hidediv1()" /> 
            </div>
        </div>

        <div class="timg"><p>當前第一期</p></div> 

        <div class="dq">
            
            <p style="width:45%;float:left;">創世幣</p> <p style="width:45%;float:left;">USDT</p>
            <p style="width:45%;float:left;"> <asp:TextBox   ID="txtneed" onkeyup="countcsb()"   runat="server" MaxLength="2"  ></asp:TextBox></p><p style="width:45%;float:left;  ">
                
                <i     id="uu"   ></i></p> 
             <p  >&nbsp;</p>  <p  >&nbsp;</p>
           <p  >&nbsp;</p>
       <p>USDT餘額：<asp:Label ID="lblusdt" runat="server" Text=""></asp:Label></p> 
        
              <p>匯兌單價：<asp:Label ID="lbldj" runat="server" Text=""></asp:Label></p>

            
            <p >已有創世幣：<asp:Label ID="lblcsb" runat="server" Text=""></asp:Label></p>

              <p  >&nbsp;</p>
           
            <div  style="width:100%">   <asp:Button ID="Button1" runat="server"   CssClass="busub"    Text="兌換"  OnClick="Button1_Click" />
                </div>

            </div>
         
        <asp:HiddenField ID="hidactm"  runat="server" />
         <asp:HiddenField ID="hiddj" runat="server" />
        <asp:HiddenField ID="hidebc" runat="server" />

        <br /> 
       
    </form>
</body>
</html>
