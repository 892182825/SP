<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JLCanshu.aspx.cs" Inherits="Company_JLCanshu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>

</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
    <div>
        <table width="371px" align="center" cellpadding="0" cellspacing="3" class="tablemb">
            <tr  style="display:none">
                <td>

                    <span><%=GetTran("009059","待汇款时间")%>：</span>
                    <asp:TextBox ID="Dhktime" runat="server" Width="100px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                    <span><%=GetTran("007628","小时")%></span>
                </td>
            </tr>
               <tr   style="display:none">
                <td>
                    <span><%=GetTran("009060","待查收时间")%>：</span>
                    <asp:TextBox ID="Dcstime" runat="server" Width="100px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                     <span><%=GetTran("007628","小时")%></span>
                </td>
            </tr>


               <tr >
                <td>
                    <span><%=GetTran("009061","收方过错：冻结的百分之")%>：</span>
                    <asp:TextBox ID="sfgc" runat="server" Width="100px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                    <span><%=GetTran("009062","被扣除")%></span>
                </td>
            </tr>


               <tr >
                <td>
                    <span><%=GetTran("009063","汇方过错：现金账户被扣除百分之")%>：</span>
                    <asp:TextBox ID="hfgc" runat="server" Width="100px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                     <span>(<%=GetTran("0090624","以负数充入")%>)</span>
                </td>
            </tr>


               <tr >
                <td>
                    <span><%=GetTran("006984","提现金额")%>：</span>
                    <asp:TextBox ID="tkje" runat="server" Width="100px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                     <span><%=GetTran("009053","的倍数")%></span>
                </td>
            </tr>

               <tr >
                <td>
                    <span><%=GetTran("001970","汇款金额")%>：</span>
                    <asp:TextBox ID="hkje" runat="server" Width="100px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                    <span><%=GetTran("009053","的倍数")%></span>
                </td>
            </tr>
                <tr >
                <td>
                    <span><%=GetTran("009017","汇款说明")%>：</span>
                    <asp:TextBox ID="txtEnote" CssClass="ctConPgTxt2" TextMode="MultiLine" Style="border: 1px solid #ccc; margin-left: 12px; border-radius: 3px; width: 80%" Height="100px" runat="server" MaxLength="250" />
                </td>
            </tr>


            <tr style="white-space:nowrap">
                <td colspan="2" align="center"><br />                                               
                    <asp:Button ID="btnOK" runat="server" Text="确 定" CssClass="anyes" onclick="btnOK_Click" />&nbsp;&nbsp;
                     <input type="button" id="butt_Query"value='<%=GetTran("000421","返回") %>' style="cursor:pointer" class="anyes" onclick="history.back()"/>              
                </td>
            </tr>             
        </table>    
    </div>
    </form>
    <%=msg %>
</body>
</html>
