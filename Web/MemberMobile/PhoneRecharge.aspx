<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhoneRecharge.aspx.cs" Inherits="Member_PhoneRecharge" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>话费充值</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/detail.css" rel="stylesheet" type="text/css" />
    <link href="css/cash.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
        function CheckText(btname)
        {
	        //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
	        filterSql_II (btname);
    		
        }
        
        function abc() {
            if (confirm('<%=GetTran("007587","您确定要充值吗") %>？')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                } else {
                alert('<%=GetTran("007387","不可重复提交!") %>');
                    return false;
                }
            } else { return false; }
        }
    </script>

</head>
<body>
    <!--页面内容宽-->
    <form id="form1" runat="server" name="form1" method="post" action="">
    <div class="MemberPage">
        <!--顶部信息,logo,help-->
        <Uc1:MemberTop ID="Top" runat="server" />
        <!--内容部分,左下背景-->
        <div class="centerCon">
            <!--内容,右下背景-->
            <div class="centConPage">
                <!--表单-->
                <div class="ctConPgCash">
                    <h1 class="CashH1">
                        <%=GetTran("008028", "手机充值")%>
                    </h1>
                    <table width="705" border="0" cellspacing="1" cellpadding="0">
                        <tr>
                            <th width="220">
                                <%=GetTran("005623", "手机号码")%>：
                            </th>
                            <td width="480">
                                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="ctConPgTxt" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <%=GetTran("000322", "金额")%>：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlMoney" runat="server" Width="60px" CssClass="ctConPgTxt" >
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <ul>
                        <li>
                            <%--<asp:Button ID="sub" runat="server" Text="提 交" OnClientClick="return abc();" OnClick="sub_Click"
                                CssClass="anyes"></asp:Button></li>--%>
                                <asp:Button ID="sub" runat="server" Height="27px" Width="47px" Style="margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                        Text="提 交" CssClass="anyes" OnClientClick="return abc();" OnClick="sub_Click"/>
                    </ul>
                    <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
                    <div style="clear: both">
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both">
        </div>
        <!--页面内容结束-->
        <!--版权信息-->
        <Uc1:MemberBottom ID="Bottom" runat="server" />
        <!--结束-->
    </div>
    </div>
        
    </form>
      <script type="text/jscript">
        $(function () {
            $('#bottomDiv').css('display', 'none');

            $("#Pager1_div2").css('background-color','#FFF')
        })
        
    </script>
</body>
</html>
