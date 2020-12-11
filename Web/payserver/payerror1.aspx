<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payerror1.aspx.cs" Inherits="payerror" %>

<%@ Register src="../UserControl/paybottom.ascx" tagname="paybottom" tagprefix="uc1" %>
<%@ Register src="../UserControl/paytop.ascx" tagname="paytop" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<script src="../membermobile/js/jquery-1.7.1.min.js"></script>
    <title>  <%=GetTran("000938","支付") %></title>
    <link href="../CSS/buy.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../membermobile/css/style.css">
<style>
body{padding-bottom:100px;}
 
.pay_qd{position:fixed;bottom:60px;left:0;width:100%;}
.cg_in img{margin:20px auto;display:block;}
.cg_in{text-align:center;font-size:16px;}
.cg ul{border-top:1px solid #ccc;margin-top:40px;padding-top:20px;padding-bottom:20px;}
.cg ul li{padding-left:20px;line-height:30px;overflow: hidden;font-size:15px;color:#666}
.cg ul li span{float:left;}
.cg ul li .right{margin-left:50px;}
.cg{padding:0 5%;}
.cg a{font-size:13px;text-decoration:underline;color:#1db06e;padding-top:10px;text-align:center;display: block; bottom:100px; }
</style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="navbar navbar-default" role="navigation">
            <div class="navbar-inner">
                <a class="btn btn-primary btn-lg" style="float: left; padding: 6px; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);" href="first.aspx"></a>

                <span style="color: #fff; font-size: 18px; margin-left: 35%; text-shadow: 2px 2px 5px hsl(0, 0%, 61%);">支付结果	</span>
            </div>
        </div>
    <div class="cg">
    	<div class="cg_in">
          
             <span  runat="server" id="cg" ><i style="font-size:30px; margin-top:30px;" class=" glyphicon glyphicon-ok green"> </i></span>
             
            <span runat="server" id="sb" visible="false"><i style="font-size:30px; margin-top:30px;" class=" glyphicon glyphicon-warning-sign red"> </i></span>
            <asp:Label ID="lblinfo" runat="server" Text="未知错误信息，请返回原请求地检查."></asp:Label>
           <div></div> 
          
        </div>
        <ul>
        	<li><span class="left"></span></li>
        	<%--<li><span class="left">支付单号</span><span  class="right">171022104254</span></li>--%>
        </ul>   <a href="../membermobile/first.aspx"> 回到首页>> </a> 
                   <%--<a href="question.aspx"> 支付遇到问题？>> </a> --%>

    </div>
    <div class="pay" style="display:none">
       <uc2:paytop ID="paytop1" runat="server" />
        <div class="ui-notice ui-state-error">
            <div class="ui-notice-container">
                
                    
                <p class="ui-notice-content">
                   <h3 class="ui-notice-title">  </h3> </p>
                <p style="display:none">
                    <%=GetTran("007510", "您可能需要")%>： <a title="" href="../default.aspx"><%=GetTran("007511","登陆系统")%></a>
                </p>
            </div>
        </div>
        
    </div>
       <!-- #include file = "../MemberMobile/comcode.html" -->

        	
    </form>
</body>
</html>