<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreInfoDetail.aspx.cs" Inherits="Company_StoreInfoDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <br />
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0" class="tablett">
  <tr>
    <td>
      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
        <tr>
          <th colspan="3"><%=GetTran("000609", "店铺详细信息")%></th>
        </tr>
        <tr>
          <td width="120" align="right" bgcolor="#EBF1F1"><%=GetTran("000024", "会员编号")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblNumber" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000150", "店铺编号")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblStoreId" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000039", "店长姓名")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000313", "店铺所在地")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblCountry" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr  style="display:none">
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000617", "所属语言")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblLanager" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000316", "店长联系信息")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblLianxifangshi" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr style="display:none">
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000072", "地址")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lbladdress" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000073", "邮编")%>：</td>
          <td bgcolor="#F8FBFD"> <asp:Label ID="lblPostalCode" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000319", "负责人电话")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblfuzherentel" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000044", "办公电话")%>：</td>
          <td bgcolor="#F8FBFD"> <asp:Label ID="lblofficess" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000069", "移动电话")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblMobiletel" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000071", "传真电话")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblfaxtel" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000087", "开户银行")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblback" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000329", "银行账户")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblbackcard" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000330", "电子邮箱")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000332", "网址")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblNetAddress" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000078", "备注")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblRemark" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000043", "推荐人编号")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblDirect" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000042", "办店期数")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblExpectNum" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000031", "注册时间")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblRegisterDate" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000046", "级别")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblStoreLevelStr" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr style="display:none">
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000856", "店铺可用报单底线")%>：</td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblTotalMaxMoney" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000341", "经营面积（平方米）")%>：
            <label></label></td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblFareArea" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000343", "投资总额（万元）")%>：
            <label></label></td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblTotalInvestMoney" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr style="display:none">
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000041", "总金额（元）")%>：
            <label></label></td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblTotalAccountMoney" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr style="display:none">
          <td align="right" bgcolor="#EBF1F1"><%=GetTran("000041", "总消费（元）")%>：
            <label></label></td>
          <td bgcolor="#F8FBFD"><asp:Label ID="lblzongxiaofei" runat="server" Text=""></asp:Label></td>
        </tr>
      </table>
    </td>
  </tr>
</table>
<br />
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center"><input type="button" value='<%=GetTran("000096","返 回")%>' onclick="javascript:window.history.go(-1);" 
                     class="anyes" /></td>
  </tr>
</table>
<br />
    </form>
</body>
</html>
