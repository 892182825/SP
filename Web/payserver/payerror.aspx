<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payerror.aspx.cs" Inherits="payerror" %>

<%@ Register src="../UserControl/paybottom.ascx" tagname="paybottom" tagprefix="uc1" %>
<%@ Register src="../UserControl/paytop.ascx" tagname="paytop" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>快速--支付</title>
    <link href="../CSS/buy.css" rel="stylesheet" type="text/css" />
    <style>
        .dvTop {
            width: 998px;
            margin:10px auto;
            padding-top:15px;
            
            font-size: 20px;
            color:#000;

            background-color: #f6f9ff;
            border: 1px solid #bcccee;
            height: 46px;
        }
        .dvTop div {
            margin-left:16px;
        }
        .dvTop a {
            font-size: 16px;
            color:#0a8978;/*color:#09c;*/
            padding:0px 10px;
            cursor:pointer;
            text-decoration:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dvTop">
        <div>
            <font>快速导航 >></font> <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="pay">
       <uc2:paytop ID="paytop1" runat="server" />
        <div class="ui-notice ui-state-error">
            <div class="ui-notice-container">
                
                    
                <p class="ui-notice-content">
                   <h3 class="ui-notice-title"> <asp:Label ID="lblinfo" runat="server" Text="未知错误信息，请返回原请求地检查."></asp:Label> </h3> </p>
                <p style="display:none">
                    <%=GetTran("007510", "您可能需要")%>： <a title="" href="../default.aspx"><%=GetTran("007511","登陆系统")%></a>
                </p>
            </div>
        </div>
       
     
       <uc1:paybottom ID="paybottom1" runat="server" />
    </div>
    
    </form>
</body>
</html>
