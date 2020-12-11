<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowNetworkView.aspx.cs"
    Inherits="Company_ShowNetworkView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>newWLT</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE"/>
    <meta content="JavaScript" name="vs_defaultClientScript"/>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link rel="Stylesheet" href="CSS/Company.css" type="text/css" />
        
    <link href="CSS/ShowNetworkView.css" rel="stylesheet" type="text/css" />
    <script src="js/ShowNetworkView.js" type="text/javascript"></script>
</head>
<body onload="javascript:DataBind()">
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
                                        <%=GetTran("000421", "返回")%><%=GetTran("000879", "以下图中红色代表新增人员")%></u></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCY" runat="server" Text="常用" CssClass="anyes" OnClick="btnCY_Click" />
                            <asp:Button ID="Button3" runat="server" Text="表格" CssClass="anyes" OnClick="Button3_Click" />
                            <asp:Button ID="Button5" runat="server" Text="展开" Enabled="false" CssClass="anyes" />
                            <asp:Button ID="Button4" runat="server" Text="伸缩" CssClass="anyes" OnClick="Button4_Click" />
                            
                        </td>
                    </tr>
                   
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablema">
                    <tr>
                        <td>
                            
                            <table class="tablema">
                                <tr>
                                    <td><asp:Button ID="Button6" runat="server" Text="转到安置" CssClass="anyes" 
                                            onclick="Button6_Click" />
                            <asp:Button ID="Button7" runat="server" Text="转到推荐" CssClass="anyes" 
                                            onclick="Button7_Click" /></td>
                                    <td>
                                        可查看网络
                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <ItemTemplate>
                                                <b>
                                                    <%#DataBinder.Eval(Container.DataItem, "wlt")%></b>&nbsp;<span style="color:black"> / </span>&nbsp;
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                     <tr>
                        <td>
                           <div style="float:left;text-align:left;margin-left:20px" id="divDH" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" colspan="3" >
                            
                            <%=strRes[0]%><%=strRes[1]%><%=strRes[2]%><%=strRes[3]%><%=strRes[4]%><%=strRes[5]%><%=strRes[6]%><%=strRes[7]%>
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
