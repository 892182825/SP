<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager.ascx.cs" Inherits="Pager" %>

<table id="ShowTable" runat="server" height="30px;" width="100%" style="font-family: 宋体;	font-size: 12px;	line-height: 24px;	color: #005575;	text-decoration: none;">
     <tr>
        <td align="left" width="60%" nowrap="nowrap" >
            <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
             <%=new BLL.TranslationBase().GetTran("007031", "每页显示")%>
            <asp:TextBox style="width: 26px"  ID="txtTS" runat="server" onblur="__doPostBack('Pager1$lkbtn_Login','')" ></asp:TextBox>
           <%=new BLL.TranslationBase().GetTran("006978", "条")%> <asp:linkbutton id="lkbtn_Login" style="DISPLAY: none"  Runat="server" onclick="lkbtn_Login_Click">条数</asp:linkbutton>
        </td>
        <td nowrap="nowrap" align="right" >
            <table id="pageS" runat="server"  style="font-family: 宋体;	font-size: 12px;	line-height: 24px;	color: #005575;	text-decoration: none;">
                <tr>
                    <td nowrap="nowrap">
                        <asp:ImageButton  ID="btnFirst" runat="server" ImageUrl="~/Company/images/one.gif" width="70" height="17" border="0" align="absmiddle" OnClick="btnFirst_Click" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:ImageButton ID="btnPrevious" runat="server" ImageUrl="~/Company/images/up.gif" width="70" height="17" border="0" align="absmiddle" OnClick="btnPrevious_Click" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Company/images/down.gif" width="70" height="17" border="0" align="absmiddle" OnClick="btnNext_Click" />
                    </td>
                    <td nowrap="nowrap">
                        <asp:ImageButton ID="btnOmega" runat="server" ImageUrl="~/Company/images/last.gif" width="70" height="17" border="0" align="absmiddle" OnClick="btnOmega_Click" />
                    </td>
                    <td nowrap="nowrap" valign="middle"><label><%=new BLL.TranslationBase().GetTran("001022", "跳转到")%></label>
<%--                        <asp:DropDownList ID="dropPageList" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="dropPageList_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                        <asp:TextBox ID="txt_page" runat="server" Width="20px" CssClass="ctConPgTxt"></asp:TextBox>
                        <asp:Button ID="btn_submit" runat="server" Text="确认" CssClass="anyes" 
                    onclick="btn_submit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%=msg %>
