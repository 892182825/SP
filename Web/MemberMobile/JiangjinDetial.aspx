<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JiangjinDetial.aspx.cs" Inherits="Member_JiangjinDetial" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="x-ua-compatible" content="ie=11" />

<title></title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<link href="css/cash.css" rel="stylesheet" type="text/css" />
</head>

<body>
<form id="form1" name="form1" runat="server" method="post" action="">
<!--页面内容宽-->
<div class="MemberPage">
<!--顶部信息,logo,help-->
<Uc1:MemberTop ID="Top" runat="server" />


<!--内容部分,左下背景-->
<div class="centerCon">
	<!--内容,右下背景-->
	<div class="centConPage">
	  <!--表单-->
      <div class="ctConPgCash">
      	<h1 class="CashH1"><%=GetTran("000360", "奖金明细")%></h1>
        <table width="705" id="talbe1" runat="server" border="0" cellspacing="1" cellpadding="0">
          <tr>
            <td><%=GetTran("000360", "奖金明细")%>1</td>
            <td><%=GetTran("000360", "奖金明细")%>2</td>
            <td><%=GetTran("000360", "奖金明细")%>3</td>
          </tr>
         <tr>
            
            <td>
               &nbsp;
            </td>
            
            <td>
                &nbsp;
            </td>
            
            <td>
               &nbsp;
            </td>
        </tr>
        <tr>
            
            <td>
               &nbsp;
            </td>
            
            <td>
                &nbsp;
            </td>
            
            <td>
               &nbsp;
            </td>
        </tr>
        </table>
        
        <ul>
        	<li><asp:button id="BtnConfirm" runat="server" Text="返 回" CssClass="anyes" onclick="BtnConfirm_Click" Visible=false></asp:button></li>
            <li><input type="button" Height="27px" Width="47px" Style="margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);" class="&quot;anyes&quot;" value='<%=GetTran("000096", "返 回") %>' onclick="window.history.back(-1);" /></li>

        </ul>
      <div style="clear:both"></div>
      </div>
      
      <div class="ctConPgList-3" style="margin-left:50%">
      	<h1><%=GetTran("000649","功能说明")%>：</h1>
        <p>	1.<%=GetTran("000360", "奖金明细")%>；</p>
      </div>
      
	</div>
</div>
<!--页面内容结束-->
</div>

<!--版权信息-->
<Uc1:MemberBottom ID="Bottom" runat="server" />
<!--结束-->
</form>
        <script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');

            $("#Pager1_div2").css('background-color','#FFF')
        })
        
    </script>   
</body>
</html>

