<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowNotGoodView.aspx.cs" Inherits="Company_ShowNotGoodView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>newWLT</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link rel="Stylesheet" href="CSS/Company.css" type="text/css" />
    <style>
        A:link
        {
            font-size: 12px;
        }
        A:visited
        {
            font-size: 12px;
        }
        A:active
        {
            font-size: 12px;
        }
        A:hover
        {
            font-size: 12px;
        }
        BODY
        {
            font-size: 12px;
        }
        TD
        {
            font-size: 12px;
        }
        .ls
        {
            font-size: 12px;
        }
        .ls2
        {
            font-size: 16px;
        }
    </style>

    <script type="text/javascript">
		    function GetBind(number)
		    {
		        var asg = AjaxClass.GetViewBind(number).values;
		        document.getElementById("divDh").innerHTML = document.getElementById("divDh").innerHTML +" \ "+ asg;
		    }
		    
		    function ShowView(abc)
		    {
		        document.getElementById("txtBh").value = abc.firstChild.nodeValue;
		        document.getElementById("lkn_submit").click();
		    }
    </script>

</head>
<body>
    <form id="Form2" method="post" runat="server">
    <br />
    <table id="Table2" width="100%" border="0" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="1" class="tablemb">
                    <tr>
                        <td style="height: 25px" colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="显示" CssClass=" anyes" OnClick="Button1_Click">
                            </asp:Button>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lbl_msg" runat="server"><%=GetTran("000045", "期数")%></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <%=GetTran("000024", "会员编号")%><asp:TextBox ID="txtBh" runat="server" Width="85" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="TextBox1" runat="server" Width="24px"></asp:TextBox><%=GetTran("000845", "层")%>
                            <asp:LinkButton ID="lkn_submit" runat="server" Text="提交" Style="display: none" OnClick="lkn_submit_Click"></asp:LinkButton>
                            <asp:Button ID="Button2" runat="server" Text="回到顶部" Visible="false" CssClass=" anyes"
                                OnClick="Button2_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="javascript:window.history.back()"
                                    style="display: none"><u>
                                        <%=GetTran("000421", "返回")%><%=GetTran("000879", "以下图中红色代表新增人员")%></u></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                   
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablema">
                    <tr>
                        <td>
                            <table class="tablema">
                                <tr>
                                    <td>
                                        可查看网络
                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <ItemTemplate>
                                                <b>
                                                    <%#DataBinder.Eval(Container.DataItem, "wlt")%></b>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <div id="divDH" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" colspan="3">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <p style="margin-bottom: -19px">
                                        <%#DataBinder.Eval(Container.DataItem, "xinxi")%></p>
                                </ItemTemplate>
                            </asp:Repeater>
                            <p>
                                &nbsp;</p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!--<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td bgcolor="#1a71b9"><img src="../images/common/spacer.gif" width="1" height="2"></td>
				</tr>
			</table>-->
    </form>
    <%=msg %>
</body>
</html>
