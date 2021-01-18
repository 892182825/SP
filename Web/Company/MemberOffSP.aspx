<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberOffSP.aspx.cs" Inherits="Company_MemberOffSP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="CSS/level.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
    function CheckText()
	{
		//这个方法是只有1个lkSubmit按钮时候 可直接用
		filterSql();
	}
        function getname(){
            var number = document.getElementById("txtNumber").value;
            var name = AjaxClass.GetName(number).value;
            document.getElementById("Label2").innerHTML=name;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div align="center">
        <table class="tablemb" width="500px">
            <tr>
                <th colspan="2">
                    <b>
                        <asp:Label ID="Label1" runat="server"   ></asp:Label>
                    </b>
                </th>
            </tr>
            <tr>
                <td align="right">
                    <%=GetTran("000000", "会员手机号")%>：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtNumber" runat="server" MaxLength="50" onblur="getname()"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Select" Visible="false" runat="server" Text="查看" CssClass="another" OnClick="btn_Select_Click" />
                </td>
            </tr>
            <tr>
                <td align="right"  style="display:none;">
                    <%=GetTran("000025", "会员姓名")%>：
                </td>
                <td align="left">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="display:none;">
                <td align="right">
                    <%=GetTran("004134","操作人编号")%>：
                </td>
                <td align="left">
                    <%--<asp:TextBox ID="txtOperatorNo" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>--%>
                    <asp:Label ID="txtOperatorNo" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr style="display: none;">
                <td align="right">
                    <%=GetTran("007191", "操作人姓名")%>：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtOperatorName" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <%=GetTran("000078", "备注")%>：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtMemberOffreason" runat="server" TextMode="MultiLine" Height="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:LinkButton ID="lkSubmit" runat="server" Text="提交" OnClick="lkSubmit_Click" Style="display: none"></asp:LinkButton>
                    <input class="another" id="bSubmit" onclick="CheckText()" type="button" value="<%=GetTran("000064", "确认")%>" />
                    <asp:Button ID="btnquery" runat="server" Text="确认冻结" OnClick="btnquery_Click" Visible="false"
                        CssClass="anyes"></asp:Button>
                    <asp:Button ID="Button1" runat="server" Text="返回" CssClass="another" OnClick="Button1_Click">
                    </asp:Button>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <%=msg %>
    </div>
    </form>
</body>
</html>
