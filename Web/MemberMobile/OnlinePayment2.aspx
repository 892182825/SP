<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlinePayment2.aspx.cs" Inherits="Member_OnlinePayment2" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="x-ua-compatible" content="ie=11" />

<title>会员管理系统</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<link href="css/cash.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

<script src="../company/js/tianfeng.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">

function CheckText(btname)
{
	//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
	filterSql_II (btname);
	
}
</script>
    <script type="text/javascript">
        var lang = $("#lang").text();
        if (lang!="L001") {
            //alert("OnlinePayment2");
        }
    </script>
</head>

<body>
<!--页面内容宽-->
<div class="MemberPage">
<!--顶部信息,logo,help-->
<Uc1:MemberTop ID="Top" runat="server" />


<!--内容部分,左下背景-->
<div class="centerCon">
	<!--内容,右下背景-->
<form id="form1" name="form1" runat="server" method="post" action="">
	<div class="centConPage">
	  <!--表单-->
      <div class="ctConPgCash">
      	<h1 class="CashH1">[<%=GetTran("001125", "检验页面")%>]</h1>

        <table width="705" border="0" cellspacing="1" cellpadding="0">
          <tr>
            <th width="220"></th>
            <td width="480">
            <asp:RadioButtonList ID="RadPayFashion" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:RadioButtonList>
            </td>
          </tr>
          <tr>
            <th><%=GetTran("000150", "店铺编号")%>：</th>
            <td><asp:Literal runat="server" ID="Number"></asp:Literal></td>
          </tr>
          <tr>
            <th><%=GetTran("000322", "金 额")%>：</th>
            <td>
                <asp:TextBox CssClass="ctConPgTxt" ID="Money" runat="server" Font-Bold="True" MaxLength="20"></asp:TextBox>
                <asp:Label ID="LabCurrency22" runat="server"></asp:Label>
                <asp:Label ID="LabCurrency"  runat="server" Style="display: none"></asp:Label>
                <asp:Label ID="LabCurrency2" runat="server" Style="display: none;"></asp:Label>
            </td>
          </tr>
          <tr style="display: none;">
            <th><%=GetTran("000562", "币种")%>：</th>
            <td>
                <asp:RadioButtonList ID="Currency" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:RadioButtonList>
            </td>
          </tr>
          <tr style="display: none;">
            <th><%=GetTran("001044", "汇款用途")%>：</th>
            <td>
                 <asp:RadioButtonList ID="DeclarationType" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" Style="margin-right: 7px">
                 </asp:RadioButtonList>
            </td>
          </tr>
          <tr>
            <th><%=GetTran("000786", "付款日期")%>：</th>
            <td><asp:TextBox ID="FKBirthday"  CssClass="ctConPgTxt" runat="server" Enabled="false"></asp:TextBox></td>
          </tr>
          <tr>
            <th> <%=GetTran("001305", "备 注")%>：</th>
            <td>
            <asp:TextBox CssClass="ctConPgTxt2" ID="Remark" runat="server" Height="86px" TextMode="MultiLine" Width="262px"></asp:TextBox>
            </td>
          </tr>
        </table>
        
        <ul>
        	<li><asp:Button ID="back2" runat="server" Text="返回修改" OnClick="back2_Click" CssClass="anyes" /></li>
            <li><asp:Button ID="Button5" runat="server" Text="保 存" OnClick="Button5_Click" OnClientClick="return checkedcf('是否保存？')" CssClass="anyes"></asp:Button></li>
        </ul>

      <div style="clear:both"></div>
      </div>
	</div>
</form>
</div>

<div style="clear:both"></div>
<!--页面内容结束-->
<!--版权信息-->
<Uc1:MemberBottom ID="Bottom" runat="server" />
<!--结束-->
</div>
</body>
</html>

