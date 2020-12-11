<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageContent.aspx.cs" Inherits="Member_MessageContent" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="x-ua-compatible" content="ie=11" />

<title>会员管理系统</title>
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link href="css/detail.css" rel="stylesheet" type="text/css" />
<link href="css/cash.css" rel="stylesheet" type="text/css" />
</head>

<body>
<div class="MemberPage" id="div_content" runat="server">
<form id="form1" name="form1" runat="server" method="post" action="">
<Uc1:MemberTop ID="Top" runat="server" />
	  <!--表单-->
      <div class="ctConPgCash">
        <table width="100%" style="margin-top:70px" border="0" cellspacing="1" cellpadding="0">
          <tr>
            <th width="120"><%=GetTran("000613", "日期")%>：</th>
            <td width="580"><asp:TextBox id="Text_Date" runat="server" Width="390px" ReadOnly="true" BorderStyle="None"></asp:TextBox></td>
          </tr>
          <tr>
            <th><%=GetTran("000784", "接收者")%>：</th>
            <td><asp:TextBox id="Text_Recive" runat="server" Width="390px" ReadOnly="true" BorderStyle="None"></asp:TextBox></td>
          </tr>
          <tr>
            <th><%=GetTran("000781", "标题")%>：</th>
            <td><asp:TextBox id="Text_Title" runat="server" Width="390px" ReadOnly="true" BorderStyle="None"></asp:TextBox></td>
          </tr>
          <tr>
            <th><%=GetTran("000779", "发送者")%>：</th>
            <td><asp:TextBox id="Text_Send" runat="server" Width="390px" ReadOnly="true" BorderStyle="None"></asp:TextBox></td>
          </tr>
          <tr>
            <th><%=GetTran("000013", "内容")%>：</th>
            <td>
                <div style="WIDTH:580px;height:280px;border:gray solid 1px;overflow:auto;padding-left:5px;padding-top:5px;">
			    <span id="DetailSpan" runat="server" ></span>
			    </div>
            </td>
          </tr>
        </table>
        
        <ul>
        	<li><%--<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="返 回" class="anyes"  />--%>

                 <asp:Button ID="Button2" runat="server" Height="24px" Width="58px" Style="margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);line-height: 18px; margin-top: 2px;margin-right: 10px;"
                        Text="返 回" CssClass="anyes" onclick="Button2_Click" />

        	</li>
        </ul>

<!--页面内容结束-->
</div>
<!--页面内容结束-->
</form>
<!--版权信息-->
<Uc1:MemberBottom ID="Bottom" runat="server" />
<!--结束-->
</div>
</body>
</html>
