<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditBank.aspx.cs" Inherits="EditBank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <link rel="stylesheet" href="css/style.css" />

    <title> </title>
    <link href="css/uselogin.css" rel="stylesheet" type="text/css" />
    <script src="../Company/js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-1.7.1.min.js"></script>
    <style type="text/css" >
        * {
            padding: 0;
            border: 0;
            margin: 0;
            list-style: none;
            box-sizing: border-box;
        }

        body, html {
            height: 100%;
            overflow: hidden;
            font-family: "微软雅黑";
        }

        .scht_bg {
            height: 45%;
        }

      .scht_login {
            height: 420px;
            overflow: hidden;
            padding-top: 3%;
            width:350px;
            padding: 1%;
            background: #fff;
            margin: 10% 0 0 65%;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .scht_l {
            width: 100%;
            height: 20%;
            text-align: center;
        }

        .scht_r {
            width: 100%;
            height: 80%;
            position: relative;
        }

        .scht_l h5 {
            font-size: 30px;
            font-weight: normal;
            color: #90bdd0;
            padding-right: 0.8%;
        }

        .scht_l p {
            font-size: 20px;
            margin-top: 5%;
            color: #333;
        }

        .scht_r span {
            float: left;
            width: 15%;
            height: 100%;
            background: #e7f0f4;
        }

        .scht_r input {
            float: left;
            width: 85%;
            height: 100%;
            background: #f2f8fb;
            /*padding-left: 5px;*/
        }

        .scht_r div {
            overflow: hidden;
            margin-top: 7%;
            border-radius: 3px;
            height: 12%;
            border: 1px solid #ddd;
        }

        .scht_user span {
            background: #e7f0f4 url(images/scht_user.png) no-repeat center center;
            background-size: 63%;
        }

        .scht_password span {
            background: #e7f0f4 url(images/scht_password.png) no-repeat center center;
            background-size: 46%;
        }

        .scht_yzm span {
            background: #e7f0f4 url(images/scht_yzm1.png) no-repeat center center;
            background-size: 58%;
        }

        .scht_user1 span {
            background: #e7f0f4 url(../img/yyan.png) no-repeat center center;
            background-size: 63%;
        }
          .scht_r .scht_user1 {
            margin-top: 1%;
        }
           .scht_r div {
            overflow: hidden;
            margin-top: 7%;
            border-radius: 3px;
            height: 10%;
            border: 1px solid #ddd;
        }

        .scht_r input, select {
            float: left;
            width: 85%;
            height: 100%;
            background: #f2f8fb;
            /*padding-left: 5px;*/
        }

        .scht_login butten {
            background: #24aef1;
            border-radius: 3px;
            width: 100%;
            margin-top: 7%;
            color: #fff;
            height: 12%;
        }

        .scht_yzm input {
            width: 60%;
        }

        .scht_r a {
            text-decoration: none;
            color: #666;
            position: absolute;
            bottom: 8%;
            right: 5%;
            font-size: 12px;
        }

        .scht_yzm img {
            margin-left: 1%;
            display: inline-block;
        }    

        body {
            background: url(images/2login_bg.png) no-repeat 0 center;
            background-size: 100%;
        }

        .hearer_h5 h5 {
            font-size: 40px;
            height: 100px;
            line-height: 100px;
            width: 1220px;
            margin: 0 auto;
            color: #333;
            font-weight: normal;
        }
    </style>

    <script type="text/javascript">
        window.onload = function () {
            document.getElementById("txtName").setAttribute("placeholder", "<%=GetTran("004171", "用户名")%>");
            document.getElementById("txtPwd").setAttribute("placeholder", "<%=GetTran("003231", "密  码")%>");
            document.getElementById("txtValidate").setAttribute("placeholder", "<%=GetTran("004175", "验证码")%>");
        }
    </script>
        <script type="text/javascript">
        $(function () {
            var Dheight = $(' .scht_r div').height()
            $('.scht_r input').css({ 'lineHeight': Dheight + 'px', height: Dheight });
            //$('select').css({ height: Dheight });
        })

    </script>
    <style>
        body{background:#fff;}
        .login_in{padding:10px 5%;}
        .login_in li{height:50px;line-height:50px;padding:5px 0;margin:10px 0;}
            .login_in li span
            {  float:left; width:20%; text-align:right;
            }
        .login_in li input,select{height:100%;width:78%;padding-left:5px;background:#eee;display: block;border-radius:3px ; float:right; }
        button{width:100%;background:#77c225;color:#fff;height:40px;border-radius:3px;font-size:16px}
        .yzm{overflow: hidden;}
        .yzm img{float:left}
        .login_in .yzm input{float:left;margin-right:5%;width:60%;}
        select{color:#757575;padding-left:0px;}
    </style>
</head>
<body>
   <%-- <p><%=Session["LanguageCode"] %></p>--%>
    <form id="form1" runat="server" style="height: 100%">
         <div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>
       <%=GetTran("001407","001407")%>
            	
                
            </div>
        <ul class="login_in">	
           
 <li class="user">	
  <span>    持卡人 </span>  <asp:Label ID="lblname" style="float:left; padding-left:10px;" runat="server" Text=""></asp:Label>
        </li>

            <li class="user">	
              <span>开户行: </span> <asp:DropDownList ID="ddlscbank"  runat="server"></asp:DropDownList>
        </li>
              <li class="user">	
              <span>支行名: </span>  <asp:TextBox ID="txtbankbanchname" runat="server"  MaxLength="30"></asp:TextBox>
        </li>
    	<li class="user">	
        	    <span>银行卡: </span>  <asp:TextBox ID="txtbankcard" runat="server"  MaxLength="30"></asp:TextBox>
        </li>
    	<li class="password">	
        	<span>二级密码: </span> <asp:TextBox ID="txtPwd" runat="server"  MaxLength="10" TextMode="Password"></asp:TextBox>
         
        </li>
    	 
        <li style="margin-top:30px">
             <asp:Button ID="btnSubmit" runat="server" Text="修改银行卡" OnClick="btnSubmit_Click" style="background:#77c225;color:#fff;font-size:16px; width:100%; letter-spacing:4px;" />
        </li>
          

    </ul>
    
        <%=msg %>
    </form>
</body>

</html>
 