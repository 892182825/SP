<%@ Control Language="C#" AutoEventWireup="true" CodeFile="paytop.ascx.cs" Inherits="UserControl_paytop" %>
<div id="header" class="sl-col-cnt">
     
         <span class="ge-ce"  ><%= tran.GetTran("007507","收银台")%></span>
         
     
    <ul class="fn-right quick-links">
        <li>
            <asp:Label ID="lblname" runat="server" Text=""></asp:Label>&nbsp; <%=tran.GetTran("007508", "您好，欢迎支付！")%></li>
        <li><a id="J-faq-trigger" href="#"><%=tran.GetTran("007509", "付款/充值遇到问题？")%></a> </li>
    </ul>
</div>
