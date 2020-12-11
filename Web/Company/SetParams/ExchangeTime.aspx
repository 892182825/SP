<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExchangeTime.aspx.cs" Inherits="Company_SetParams_ExchangeTime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>交易时间</title>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../bower_components/jquery/jquery.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <style>
        #kq1 {
        display:none;
        }
        #kq2 {
        display:none;
        }
        #kq3 {
        display:none;
        }
        #gb1 {
        display:none;
        }
        #gb2 {
        display:none;
        }
        #gb3 {
        display:none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function ale() {
            return confirm('确定要更改交易时间吗？');
        }

        function CheckTextOK() {
            filterSql_II('lbtnOK');
        }
         var i=0
        function js() {
            if (i == 0)
            {
                $("#kq1").show();
                $("#gb1").show();
            }
            if (i == 1) {
                $("#kq2").show();
                $("#gb2").show();
            }
            if (i == 2) {
                $("#kq3").show();
                $("#gb3").show();
            }
            i++;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
    <div>
        <table width="300px" align="center" cellpadding="0" cellspacing="3" class="tablemb">
            <tr >
                <td align="right">开启时间1</td>
                <td>
                    <asp:TextBox ID="txtOpen" runat="server" MaxLength="10"></asp:TextBox>
                    <%--<input type="text" id="txtMemBaseLine" maxlength="8" runat="server" value="0" onkeyup="value=value.replace(/[^\d]/g,'')" />--%>
                </td>
            </tr>
            <tr>
               <td align="right">关闭时间1</td>
                <td>
                    <asp:TextBox ID="txtClose" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr id="kq1">
                <td align="right">开启时间2</td>
                <td>
                    <asp:TextBox ID="txtOpen1" runat="server" MaxLength="10"></asp:TextBox>
                    <%--<input type="text" id="txtMemBaseLine" maxlength="8" runat="server" value="0" onkeyup="value=value.replace(/[^\d]/g,'')" />--%>
                </td>
            </tr>
            <tr id="gb1">
               <td align="right">关闭时间2</td>
                <td>
                    <asp:TextBox ID="txtClose1" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr id="kq2">
                <td align="right">开启时间3</td>
                <td>
                    <asp:TextBox ID="txtOpen2" runat="server" MaxLength="10"></asp:TextBox>
                    <%--<input type="text" id="txtMemBaseLine" maxlength="8" runat="server" value="0" onkeyup="value=value.replace(/[^\d]/g,'')" />--%>
                </td>
            </tr>
            <tr id="gb2">
               <td align="right">关闭时间3</td>
                <td>
                    <asp:TextBox ID="txtClose2" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr id="kq3">
                <td align="right">开启时间4</td>
                <td>
                    <asp:TextBox ID="txtOpen3" runat="server" MaxLength="10"></asp:TextBox>
                    <%--<input type="text" id="txtMemBaseLine" maxlength="8" runat="server" value="0" onkeyup="value=value.replace(/[^\d]/g,'')" />--%>
                </td>
            </tr>
            <tr id="gb3">
               <td align="right">关闭时间4</td>
                <td>
                    <asp:TextBox ID="txtClose3" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                <input type="button" value="增加时间段" onclick="js();" /></td>
            </tr>
            <tr style="white-space:nowrap">
                <td colspan="2" align="center"><br />                                               
                    <asp:Button ID="btnOK" runat="server" Text="确 定" OnClientClick="return ale()" onclick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;
                    <asp:Button ID="lbtnReturn" runat="server" Text="返 回" CssClass="anyes" onclick="lbtnReturn_Click" />               
                </td>
            </tr>             
        </table>    
    </div>
    </form>
</body>
</html>