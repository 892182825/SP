<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendEmail.aspx.cs" Inherits="Member_SendEmail" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberPager.ascx" TagName="MemberPager" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="x-ua-compatible" content="ie=11" />

<title></title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
<!--
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}
//-->
</script>
<style type="text/css">
    ul li {
	    float: left;
	    line-height: 24px;
	    padding-right: 5px;
	    padding-left: 5px;
    }
</style>
</head>

<body>
<br />
<form id="form1" runat="server" name="form1" method="post" action="">
      	<ul>
        	<li><asp:button id="BtnConfirm" runat="server" Text="查 询" onclick="BtnConfirm_Click" CssClass="anyes" /></li>
            <li><%=GetTran("001285", "发件日期")%>  <%=GetTran("000448", "从")%>：</li>
            <li><asp:TextBox ID="txtBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox></li>
            <li><%=GetTran("000205", "到")%>：</li>
            <li><asp:TextBox ID="txtEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker()"></asp:TextBox></li>
        </ul>
        <br /><br /><br />
      <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True" DataKeyField="id" Width="100%" onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" border="0" cellspacing="1" cellpadding="0">
            <HeaderStyle cssClass="ctConPgTab" />
            <RowStyle HorizontalAlign="Center" Wrap="false" />
            <Columns>
				<asp:TemplateField HeaderText="接收对象">
					<ItemTemplate>
						<%# getloginRole(DataBinder.Eval(Container, "DataItem.loginRole").ToString()) %>
						<input id="Hidden1" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.SenderRole")%>' name="Hidden1" runat="server" />
						
                        <asp:Label ID="HidID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id")%>' Visible="false"></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField DataField="Receive" HeaderText="接收编号" />
				<asp:BoundField DataField="sender" HeaderText="发送编号" />
				<asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="../Member/MessageContent.aspx?id={0}&amp;T=MessageSend&amp;source=SendEmail.aspx&amp;type=11"
					DataTextField="infoTitle" HeaderText="信息标题"></asp:HyperLinkField>
                <asp:TemplateField HeaderText="处理状态">
									<ItemTemplate>
										<%# gethandlestatus(DataBinder.Eval(Container, "DataItem.ReadFlag").ToString()) %>
									</ItemTemplate>
								</asp:TemplateField>					
			<asp:TemplateField HeaderText="发布日期" ItemStyle-Wrap="false" >
                    <ItemTemplate>
                        <asp:Label ID="fbsj" runat="server" Text='<%#GetBiaoZhunTime(Eval("Senddate").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:TemplateField HeaderText="删除">
					<ItemTemplate>
					    <asp:ImageButton ID="Linkbutton2" ImageUrl="images/view-button3.png" runat="server" CommandName="del" CausesValidation="false" />
						<%--<asp:LinkButton runat="server"  CommandName="del" CausesValidation="false" ID="Linkbutton2"><%=GetTran("000022","删除")%></asp:LinkButton>--%>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
			<EmptyDataTemplate>
			    <table width="100%" cellspacing="0">
			        <tr class="ctConPgTab">
			          <th><%=GetTran("000912", "接收对象")%></th>
							<th><%=GetTran("000910", "接收编号")%></th>
							<th><%=GetTran("000908", "发送编号")%></th>
							<th><%=GetTran("000825", "信息标题")%></th>
							<th><%=GetTran("007405", "处理状态")%></th>
							<th><%=GetTran("000720", "发布日期")%></th>
							<th><%=GetTran("000022","删除")%></th>			
			        </tr>
			    </table>
			</EmptyDataTemplate>
      </asp:GridView>
      <br />
    <Uc1:MemberPager ID="Pager1" runat="server" />
      <div><asp:button id="Button1" Text="导出EXECL" Runat="server" Visible="False" onclick="Button1_Click"></asp:button></div>
      <div class="ctConPgList-3">
      	<h1><%=GetTran("000649", "操作说明")%>：</h1>
        <p>	1、<%=GetTran("001046", "查看发出去的的邮件")%>。</p>
      </div>
</form>
<%= msg %>
</body>
</html>
