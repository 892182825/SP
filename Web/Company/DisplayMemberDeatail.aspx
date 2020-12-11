<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayMemberDeatail.aspx.cs" Inherits="Company_DisplayMemberDeatail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员详细资料</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function btnClick() {
            var name, value;
            var str = location.href; //取得整个地址栏
            var num = str.indexOf("?")
            str = str.substr(num + 1); //取得所有参数
            var arr = str.split("&"); //各个参数放到数组里
            if (arr.length > 1) {
                if (arr[1] == "type=AddTotalAccount.aspx") {
                    value = arr[0].split("=");
                    var toPages = arr[1].split("=")[1] + "?type=" + arr[2].split("=")[1];
                    window.location.href = toPages + "&Number=" + value[1];
                } else {
                    value = arr[0].split("=");
                    var toPages = arr[1].split("=");
                    //window.location.href = toPages[1] + ".aspx?Number=" + value[1] + "&type=" + arr[2].split("=")[1];
                    window.location.href = toPages[1] + ".aspx?Number=" + value[1] + "&type=" + arr[1].split("=")[1];
                }
            }
            else {
                window.history.back();
            }
        }
    </script>
</head>
<body>
    <form method="post" id="form1" runat="server">
			<br />
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0" class="tablett">
  <tr>
    <td>
      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
        <tr>
          <th colspan="3"><%=GetTran("000297", "会员资料")%></th>
        </tr>
        <tr>
          <td width="120" align="right" bgcolor="#EBF1F1"><%=GetTran("000024", "会员编号")%>：</td>
          <td bgcolor="#F8FBFD"><asp:label id="Number" runat="server" Width="104px"></asp:label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000027", "安置编号")%>：</td>
          <td bgcolor="#F8FBFD"><asp:label id="Placement" runat="server" Width="104px"></asp:label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000026", "推荐编号")%>：</td>
          <td bgcolor="#F8FBFD"><asp:label id="Recommended" runat="server" Width="104px"></asp:label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000025", "会员姓名")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="Name" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000063", "会员昵称")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="PetName" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000105", "出生日期")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label ID="Birthday" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000085", "性别")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label ID="Sex" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000081", "证件类型")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label ID="PaperType" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000083", "证件号码")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="PaperNumber" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000065", "家庭电话")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="HomeTele" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000044", "办公电话")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="OfficeTele" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000069", "移动电话")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="MoblieTele" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000071", "传真电话")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="FaxTele" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("001532", "Email")%>：</td>
            <td bgcolor="#F8FBFD"><asp:Label ID="labEmail" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000072", "地址")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label ID="Country" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000073", "邮编")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="PostolCode" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000087", "开户银行")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label ID="Bank" runat="server"></asp:Label></td>
        </tr>
        <tr style="display:none;">
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000089", "银行地址")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label ID="BankAddress" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000088", "银行帐号")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="BankNum" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000086", "开户名")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label id="BankBook" Runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000029", "注册期数")%>： </td>
          <td bgcolor="#F8FBFD"><asp:Label  id="ExpectNum" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000078", "备注")%>： </td>
		  <td bgcolor="#F8FBFD"><asp:Label id="Remark" runat="server"></asp:Label></td>
        </tr>
      </table>
    </td>
  </tr>
</table>
<br />
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center"><input type="button" onclick="btnClick()" value='<%=GetTran("000096","返 回")%>' id="Button1" name="Button1" class="anyes" /></td>
  </tr>
</table>
<br />
	</form>
</body>
</html>
