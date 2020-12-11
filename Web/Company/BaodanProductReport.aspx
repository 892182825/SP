<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaodanProductreport.aspx.cs" Inherits="Company_BaodanProductReport" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>产品销售表</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <style type="text/css">TABLE.colortest { BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid;font-size:10pt;color:rgb(0,85,117); }
	.colortest TD { BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid }
	</style>
</head>
<body>
   <form id="form1" runat="server">
    <div>
        <table id="table1" cellspacing="0px" cellpadding="0px" width="1020px" border="0px" class="biaozzi">
            <tr>
                <td align="center" height="40px">
                    <font style="font-weight: bold; font-size: 18px"><%=GetTran("000557", "产品销售表")%></font>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="table3" width="1020px">
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
                                <%=GetTran("000562", "币种")%>:<%=GetTran("000563", "人民币")%>&nbsp;&nbsp;&nbsp; <%=GetTran("000518", "单位")%>:<%=GetTran("000564", "元")%>&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
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
