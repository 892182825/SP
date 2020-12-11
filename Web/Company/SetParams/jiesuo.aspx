<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jiesuo.aspx.cs" Inherits="Company_SetParams_ExchangeTime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>提前解锁</title>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../bower_components/jquery/jquery.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
   
    <script type="text/javascript">
        function getname() {
            var number = document.getElementById("txtOpen").value;

            var name = AjaxClass.GetPetName(number, "Member").value;

            document.getElementById("txtName").innerHTML = name;

        }
        function getnamee() {
            var number = document.getElementById("txtClose").value;

            var name = AjaxClass.GetPetName(number, "Member").value;

            document.getElementById("xiajiname").innerHTML = name;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
    <div>
        <table width="300px" align="center" cellpadding="0" cellspacing="3" class="tablemb">
            <tr >
                <td align="right">手机号</td>
                <td>
                    <asp:TextBox ID="txtOpen" onblur="getname()" runat="server" MaxLength="11"></asp:TextBox>
                    <%--<input type="text" id="txtMemBaseLine" maxlength="8" runat="server" value="0" onkeyup="value=value.replace(/[^\d]/g,'')" />--%>
                </td>
            </tr>
            <tr>
               <td align="right">姓名：</td>
                <td>
                    <asp:Label ID="txtName" runat="server" MaxLength="10"></asp:Label>
                </td>
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