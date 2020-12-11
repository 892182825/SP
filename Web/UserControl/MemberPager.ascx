<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberPager.ascx.cs" Inherits="UserControl_MemberPager" %>
<style>

      #Pager1_div2 {
            background-color: #019c87;
           

        }
         #Pager1_div2 ul {
            color: #fff;

        }
    #Pager1_btn_submit {
        background:#507d0c
    }
    * {
        list-style:none
    }
    input {
        line-height:none;
    }
</style>

<%--<script>
    $('.ctConPgList-2').css('background-color', '#019c87');
</script>--%>

<div class="ctConPgList-2" id="div2" runat="server" >
    <div style="background-color:#019c87; color: #fff;">
      	<ul>
        	<li id="lbl" runat="server" style="margin-left:6px;"> <%--<asp:Label ID="lbl" runat="server" Text=""></asp:Label>--%></li>
            <li>
                <%=new BLL.TranslationBase().GetTran("007031", "每页显示")%>
                <asp:TextBox Width="26" ID="txtTS" CssClass="ctConPgTxt" runat="server" onblur="__doPostBack('Pager1$lkbtn_Login','')" Style="color:#666" ></asp:TextBox><%=new BLL.TranslationBase().GetTran("006978", "条")%>
            </li>
            <li style="float:right">
                <%--  <asp:Button ID="btn_submit" style="width:auto" runat="server" Text="确认" CssClass="anyes1" 
                    onclick="btn_submit_Click" />--%>
                   <asp:Button ID="btn_submit" runat="server" Height="26" Width="60" Style="margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #fff;background:rgb(125, 191, 63);/*background: #507E0C; background-image: linear-gradient(#addf58,#96c742);*/ text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);/*line-height: 26px;*/ margin-top: 8px;margin-right: 10px;"
                        Text="确 认" CssClass="anyes" onclick="btn_submit_Click" />
            </li>
            <li style="float:right;padding-top: 2px;padding-left:10px">
                <asp:TextBox ID="txt_page" runat="server" Width="20px" CssClass="ctConPgTxt" style="color:#666"></asp:TextBox>
            <%--<asp:DropDownList ID="dropPageList" runat="server"  CssClass="ctConPgFor" AutoPostBack="True" 
                    onselectedindexchanged="dropPageList_SelectedIndexChanged">
                </asp:DropDownList>--%>
            </li>
            <li id="pageS" runat="server" style="float:right;">
            
            </li>
            <li><asp:linkbutton id="lkbtn_Login" style="DISPLAY: none"  Runat="server" onclick="lkbtn_Login_Click">条数</asp:linkbutton></li>
            <li style="float:right;width:auto;"><label><%=new BLL.TranslationBase().GetTran("001022", "跳转到")%></label></li>
            <li style="/*background:url('../fyan/biank.png') no-repeat 0 center;*/background-size: 100% 100%;padding-left:2px;padding-top:6px;height:36px;padding-right:1px;margin:0 10px;float:right;margin-top:2px;width:22%">
                <asp:Button  ID="btnFirst" runat="server" style="/*background-image: url(../fyan/feny.png);*/background:rgb(125, 191, 63);/*color: wheat;*/color:#fff;border:1px solid #fff;float:left;margin-left:2px;height:26px;line-height:26px;width:20%"  border="0" align="absmiddle" OnClick="btnFirst_Click" />
                <asp:Button ID="btnPrevious" runat="server" style="/*background-image: url(../fyan/feny.png);*/background:rgb(125, 191, 63);/*color: wheat;*/color:#fff;border:1px solid #fff;float:left;margin-left:2px;height:26px;line-height:26px;width:20%"  border="0" align="absmiddle" OnClick="btnPrevious_Click" />
                <asp:Button ID="btnNext" runat="server" style="/*background-image: url(../fyan/feny.png);*/background:rgb(125, 191, 63);/*color: wheat;*/color:#fff;border:1px solid #fff;float:left;margin-left:2px;height:26px;line-height:26px;width:20%"  border="0" align="absmiddle" OnClick="btnNext_Click" />
                <asp:Button ID="btnOmega" runat="server" style="/*background-image: url(../fyan/feny.png);*/background:rgb(125, 191, 63);/*color: wheat;*/color:#fff;border:1px solid #fff;float:left;margin-left:2px;height:26px;line-height:26px;width:20%"  border="0" align="absmiddle" OnClick="btnOmega_Click" />
            </li>
        </ul>
        <div style="clear:both"></div>
    </div>
</div>
<%=msg %>