<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagerStore.ascx.cs" Inherits="PagerStore" %>
<table id="ShowTable" runat="server" height="30px;" width="100%" style="font-family: 宋体;font-size: 12px;line-height: 24px;color: #fff;text-decoration: none;background:#444">
    <tr>
        <td align="left" width="60%" nowrap="nowrap" style="color: #fff;padding-left:10px">
            <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
              <%=new BLL.TranslationBase().GetTran("007031", "每页显示")%>
            <asp:TextBox style="width: 26px;color:#333;height:auto"  ID="txtTS" runat="server" onblur="__doPostBack('Pager1$lkbtn_Login','')" ></asp:TextBox>
            <%=new BLL.TranslationBase().GetTran("006978", "条")%><asp:linkbutton id="lkbtn_Login" style="DISPLAY: none;"  Runat="server" onclick="lkbtn_Login_Click" >条数</asp:linkbutton>
            
        </td>
        <td nowrap="nowrap" align="right" >
            <table id="pageTable" runat="server" style="font-family: 宋体;font-size: 12px;line-height: 24px;	color: #0A5D3F;	text-decoration: none;background:#444">
                <tr >
                    <td nowrap="nowrap" style="background:url('../fyan/biank.png') no-repeat 0 center;width: 284px;background-size: 100%;padding-left: 2px;">
                        <%--<asp:Button ID="btnFirst" class="pageBtn btn-disable" onclick="btnFirst_Click"  runat="server" Text="上一页" />
                        <%--<span  ID="btnFirst" class="pageBtn btn-disable" onclick="btnFirst_Click">上一页</span>--%>
                        <%--<a href="#"><asp:Label ID="btnFirst" class="pageBtn btn-disable" onclick="btnFirst_Click" runat="server" Text="Label">上一页</asp:Label></a>--%>
                        <asp:Button  ID="btnFirst" runat="server" 
                            ImageUrl="../fyan/shouy.png" width="70" height="21" border="0" 
                            align="absmiddle" onclick="btnFirst_Click" 
                            style="float:left;padding-left:0;background:url('../fyan/bianxian.png') no-repeat right 0;padding-right:1px;color: #fff;"/>
                         <asp:Button ID="btnPrevious" runat="server" 
                            ImageUrl="../fyan/syy.png" width="70" height="21" border="0"
                            align="absmiddle" onclick="btnPrevious_Click"  
                             style="float:left;padding-left:0;background:url('../fyan/bianxian.png') no-repeat right 0;padding-right:1px;color: #fff;"/>
                         <asp:Button ID="btnNext" runat="server" 
                            ImageUrl="../fyan/xyy.png" width="70" height="21" border="0" 
                            align="absmiddle" onclick="btnNext_Click"  
                             style="float:left;padding-left:0;background:url('../fyan/bianxian.png') no-repeat right 0;padding-right:1px;color: #fff;"/>
                         <asp:Button ID="btnGo" runat="server" ImageUrl="../fyan/weiy.png" 
                            width="70" height="21" border="0" align="absmiddle" onclick="btnGo_Click" 
                            style="float:left;padding-left:0; background:url('../fyan/bianxian.png') no-repeat right 0;color: #fff;" />
                    </td>
                  
                    <td nowrap="nowrap" valign="middle"><label style="color:#fff;padding:0 10px"><%=new BLL.TranslationBase().GetTran("001022", "跳转到")%></label>
                        <%--<asp:DropDownList ID="ddlPageList" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlPageList_SelectedIndexChanged" >
                        </asp:DropDownList>--%>
                        <asp:TextBox ID="txt_page" runat="server" Width="20px" CssClass="ctConPgTxt" style="margin-right:10px;height:auto"></asp:TextBox>
                        <asp:Button ID="btn_submit" value=""  style="background:url('../fyan/qued.png') center center no-repeat;" runat="server" Text="" CssClass="anyes" 
                    onclick="btn_submit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%=msg %>