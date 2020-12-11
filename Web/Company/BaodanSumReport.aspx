<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaodanSumReport.aspx.cs"
    Inherits="Company_BaodanSumReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售汇总</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        TABLE.colortest
        {
            border-right: 0px;
            border-top: 0px;
            border-left: black 1px solid;
            border-bottom: black 1px solid;
            font-size: 10pt;
            color: rgb(0,85,117);
        }
        .colortest TD
        {
            border-right: black 1px solid;
            border-top: black 1px solid;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="0" cellpadding="0" width="1020px" border="0" class="biaozzi">
            <tr>
                <td align="center" height="40">
                    <font style="font-weight: bold; font-size: 18px">
                        <%=GetTran("000650", "销售汇总表")%>(<asp:Label ID="lbl_title" runat="server">Label</asp:Label>)</font>
                </td>
            </tr>
            <tr height="30">
                <td>
                    <table id="Table3" width="1020px">
                        <tr>
                            <td width="30%">
                                <%=GetTran("000559", "开始时间")%>:<%=GetTran("000448", "从")%>
                                <asp:Label ID="lbl_Begin" runat="server">Label</asp:Label><%=GetTran("000205", "到")%>
                                <asp:Label ID="lbl_End" runat="server">Label</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_message" runat="server">Label</asp:Label>
                            </td>
                            <td align="right" width="30%">
                                <%=GetTran("000562", "币种")%>:<%=GetTran("000563", "人民币")%>&nbsp;&nbsp;&nbsp;
                                <%=GetTran("000518", "单位")%>:<%=GetTran("000564", "元")%>&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;<%=GetTran("000565", "制表时间")%>:
                    <asp:Label ID="lbl_maketime" runat="server">Label</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
