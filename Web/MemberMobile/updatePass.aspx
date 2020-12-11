<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updatePass.aspx.cs" Inherits="Member_updatePass" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="x-ua-compatible" content="ie=11" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<script src="js/jquery-1.7.1.min.js"></script>


<title>修改密码</title>
    <link rel="stylesheet" href="css/style.css">
  
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<link href="css/cash.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function abc() {
            return confirm('<%=GetTran("001653","确定要修改吗？") %>');
        }
    </script>
      <style>
        .changeBox ul li .changeLt {
            width: 30%;
        }

        .changeBox ul li .changeRt {
            width: 70%;
        }

            .changeBox ul li .changeRt .textBox {
                width: 80%;
            }

        .zcMsg ul li .changeRt .zcSltBox {
            width: 80%;
        }

        .zcMsg ul li .changeRt .zcSltBox2 {
            width: 39%;
        }

        #txtadvpass {
            width: 79%;
            border: 1px solid #ccc;
        }
		.xs_footer li a{display:block;padding-top:40px;background:url(img/shouy1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li:nth-of-type(2) a{background:url(img/jiangj1.png) no-repeat center 10px;background-size:32px;}
.xs_footer li:nth-of-type(3) a{background:url(img/xiaoxi1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li:nth-of-type(4) a{background:url(img/anquan1.png) no-repeat center 8px;background-size:27px;}

</style>
    <script>
        $(function () {
            var lang = $('#lang').text();
            if (lang!="L001") {
                $('.changeBox ul li .changeRt').width('55%')
                $('.changeBox ul li .changeLt').width('45%').css('font-size', '12px')
            }
        })
    </script>
</head>

<body>
  <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
<form id="form1" runat="server" name="form1" method="post" >
      
     	<div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>
                	<%=GetTran("001342","修改密码") %>
            </div>
    <div class="middle">
        <div class="changeBox zcMsg">
            <ul>
                   <li id="tr1">
                    <div class="changeLt">
                    
                   <%=GetTran("001612", "请选择修改类型")%>：</div>
                     <div class="changeRt">
                             <asp:RadioButtonList ID="passtype"  Width="240px" RepeatLayout="Flow" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True">登陆密码</asp:ListItem>
                    <asp:ListItem Value="1">电子账户密码</asp:ListItem>
                </asp:RadioButtonList>  
                    </div>
                   </li>
                 <li>
                    <div class="changeLt"><%=GetTran("001344", "原密码")%>：</div>
                    <div class="changeRt">
                         <asp:TextBox CssClass="textBox" ID="oldPassword" MaxLength="10" runat="server" TextMode="Password"></asp:TextBox>
                    
                    </div>
                    <span id="info_name" style="display: none;" class="error"></span>
                </li>
                 <li>
                    <div class="changeLt"><%=GetTran("001348", "新密码")%>：</div>
                    <div class="changeRt">
                        <asp:TextBox ID="newPassword"  CssClass="textBox" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                     
                    </div>
                </li>
                 <li>
                    <div class="changeLt"><%=GetTran("001349", "确认新密码")%>：</div>
                    <div class="changeRt">
                       <asp:TextBox ID="newPassword2" CssClass="textBox" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                     
                    </div>
                </li>
                </ul>
            <div class="bd_pay" >	
    	<span> 
            <asp:Button ID="btn_submit" runat="server" Height="27px" Width="52px" style="width: 100%;height: 100%; background: #77c225; color:#fff;"
                        Text="确 定" CssClass="anyes" OnClientClick="return abc()" OnClick="btn_submit_Click" />
    	</span>
    	<span>
              <asp:Button ID="Button1" runat="server" Height="27px" Width="52px" style="width: 100%;height: 100%;   background: #fff;"
                        Text="确 定" CssClass="anyes" OnClick="Button1_Click" />
            </span>
                </div>
            </div>
        </div>
<!--页面内容宽-->
<div class="MemberPage" style="display:none">
<!--顶部信息,logo,help-->
<Uc1:MemberTop ID="Top" runat="server" />
<!--内容部分,左下背景-->
<div class="centerCon">
	<!--内容,右下背景-->
	<div class="centConPage" visible="false" id="table2" runat="server">
	<div class="ctConPgCash">
	    <h1 class="CashH1"><%=GetTran("001342", "修改密码")%></b></h1>
        <table width="705" border="0" cellspacing="1" cellpadding="0">
          <tr>
            <th><%=GetTran("001612", "请选择修改类型")%>：</th>
            <td>
             

            </td>
          </tr>
          <tr>
            <th><%=GetTran("001344", "原密码")%>：</th>
            <td>
           

            </td>
          </tr>
          <tr>
            <th><%=GetTran("001348", "新密码")%>：</th>
            <td></td>
          </tr>
          <tr>
            <th><%=GetTran("001349", "再输一次新密码")%>：</th>
            <td></td>
          </tr>
        </table>
        
        <ul>
        	<li><%--<asp:Button ID="btn_submit" runat="server" Text="确 定" OnClientClick="return abc()" OnClick="btn_submit_Click" CssClass="anyes"></asp:Button>--%>
                

        	</li>
            <li><%--<asp:Button ID="Button1" runat="server" Text="重 置" CssClass="anyes" OnClick="Button1_Click"></asp:Button>--%>

              

            </li>
        </ul>
      </div>
      </div>
	</div> 
<!--版权信息-->
<Uc1:MemberBottom ID="Bottom" runat="server" />
<!--结束-->
</div>
   <!-- #include file = "comcode.html" -->

</form>
</body>
</html>
