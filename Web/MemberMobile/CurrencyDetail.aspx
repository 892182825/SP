<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurrencyDetail.aspx.cs" Inherits="Member_CurrencyDetail" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="x-ua-compatible" content="ie=11" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<script src="js/jquery-1.7.1.min.js"></script>

<title></title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<link href="css/cash.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/style.css">
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
 

</style>
    <script type="text/javascript" >
        $(function () {
            var lang = $("#lang").text();
            if (lang != "L001") {
                //$('.changeLt').css('margin-left', '5%');
                //$('.changeRt').css('margin-right', '5%');
                
                $('.zcSltBox').css('width','50%')
            }
        })
 </script> 
</head>
<body>
    <b id="lang" style="display:none"><%=Session["LanguageCode"] %></b>
 
<form id="form1" runat="server" name="form1" method="post" >

       	<div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>
                	<%=GetTran("007240", "货币修改")%>
            </div>
    <div class="middle">
        <div class="changeBox zcMsg">
            <ul> 
                <li>
                    <div class="changeLt"><%=GetTran("002133", "请选择货币")%>:</div>
                    <div class="changeRt">
                          <asp:DropDownList ID="DropCurrency" CssClass="zcSltBox" runat="server">
                          </asp:DropDownList>
                    </div>
                </li>
               </ul>
               <div class="changeBtnBox zc">
               <asp:Button ID="btn_submit" runat="server" 
                            Text="搜 索" CssClass="changeBtn" onclick="btn_submit_Click" style="background:#77c225" />  
                    
        
        </div>
           
        </div>

    </div>
  <!-- #include file = "comcode.html" -->


<!--页面内容宽-->
<div class="MemberPage" style="display:none">
<!--顶部信息,logo,help-->
<Uc1:MemberTop ID="Top" runat="server" />
<!--内容部分,左下背景-->
<div class="centerCon">
	<!--内容,右下背景-->	
	<div class="centConPage">
	<div class="ctConPgCash">
	    <h1 class="CashH1"><%=GetTran("007240", "货币修改")%></h1>
        <table width="705" border="0" cellspacing="1" cellpadding="0">
          <tr>
            <th><%=GetTran("002133", "请选择货币")%>：</th>
            <td>
          
            </td>
          </tr>
        </table>
        
        <ul>
        	<li><%--<asp:Button ID="btn_submit" runat="server" Text="确 定"  CssClass="anyes" 
                    onclick="btn_submit_Click"></asp:Button>--%>
                
        	</li>
        </ul>
      </div>
      </div>
	</div>


      
<!--版权信息-->
<Uc1:MemberBottom ID="Bottom" runat="server" />
<!--结束-->
</div>
</form>
     <script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');


        })

    </script>
</body>
</html>
