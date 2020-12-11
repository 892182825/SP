<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaodanDetailReport.aspx.cs" Inherits="Company_BaodanDetailReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员销售明细</title>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="biaozzi">
                        <tr>
                        
                            <td style="white-space:nowrap">
                            <asp:Button  runat="server" ID="btnSearch" CssClass="anyes" Text="查 询" 
                                    onclick="btnSearch_Click"/>
                                <%=GetTran("000735", "订单日期")%>：
                                <%=GetTran("000559", "开始时间")%>：
                                <asp:TextBox ID="txtBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                                <%=GetTran("000740", "结束日期")%>：
                                <asp:TextBox ID="txtEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox>
                                <%=GetTran("000150", "店铺编号")%>：
                                <asp:TextBox runat="server" ID="txtStoreID" MaxLength="10"></asp:TextBox>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
