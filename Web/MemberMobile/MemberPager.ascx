﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberPager.ascx.cs" Inherits="UserControl_MemberPager" %>
<style>

      #Pager1_div2 {
            background-color: #019c87;
           

        }
         #Pager1_div2 ul {
            color: #fff;

        }
            #Pager1_div2 ul li {
                width:100%;
            }
    #Pager1_btn_submit {
        background:#507d0c
    }

</style>

<%--<script>

        
    $('.ctConPgList-2').css('background-color', '#019c87');
       

      

  
</script>--%>

<div class="ctConPgList-2" id="div2" runat="server" style="background:none">
    <div style="background-color:none; color: #fff;padding:10px 0">
      	<ul>

          
              <li style="overflow:hidden">
                <%--  <asp:Button ID="btn_submit" style="width:auto" runat="server" Text="确认" CssClass="anyes1" 
                    onclick="btn_submit_Click" />--%>

              
                
                <asp:TextBox ID="txt_page" runat="server" Width="20px" CssClass="ctConPgTxt" style="color:#666;display:none"></asp:TextBox>
            <%-- <asp:DropDownList ID="dropPageList" runat="server"  CssClass="ctConPgFor" AutoPostBack="True" 
                            onselectedindexchanged="dropPageList_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                        
            
          
                <asp:linkbutton id="lkbtn_Login" style="DISPLAY: none"  Runat="server" onclick="lkbtn_Login_Click">条数</asp:linkbutton>
                 <label style="display:none"><%=new BLL.TranslationBase().GetTran("001022", "跳转到")%></label>

               
              <div style="padding-top:2px;height:25px;margin:0 auto;margin-top:2px;width:100%">
            <asp:ImageButton  ID="btnFirst" runat="server" ImageUrl="../fyan/shouye.png"  style="float:left;padding-left:0;padding-right:2%;height:23px;width:22%;margin-left:6%"  border="0" align="absmiddle" OnClick="btnFirst_Click" />
             <asp:ImageButton ID="btnPrevious" runat="server" ImageUrl="../fyan/shangyy.png" style="float:left;padding-left:0;padding-right:2%;height:23px;width:22%"  border="0" align="absmiddle" OnClick="btnPrevious_Click" />
             
            <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../fyan/xiayy.png" style="float:left;padding-left:0;padding-right:2%;height:23px;width:22%"  border="0" align="absmiddle" OnClick="btnNext_Click" />
               
           <asp:ImageButton ID="btnOmega" runat="server" ImageUrl="../fyan/weiye.png" style="float:left;padding-left:0;padding-right:2%;height:23px;width:22%"  border="0" align="absmiddle" OnClick="btnOmega_Click" />
              </div>
                                     <asp:Button ID="btn_submit" runat="server" Height="24px" Width="28px" Style="margin-left: 5px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C;background: #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);line-height: 18px; margin-top: 2px;margin-right: 10px;display:none"
                        Text="确 认" CssClass="anyes" onclick="btn_submit_Click" />

    </li>
                
              <li id="pageS" runat="server" style="float:right">
            
            </li>
                      	<li id="lbl" runat="server" style="text-align:center;color:#666"> <%--<asp:Label ID="lbl" runat="server" Text=""></asp:Label>--%></li>
            <li style="float:left;display:none">    
                 <%=new BLL.TranslationBase().GetTran("007031", "每页显示")%>
                <asp:TextBox Width="26" ID="txtTS" CssClass="ctConPgTxt" runat="server" onblur="__doPostBack('Pager1$lkbtn_Login','')" Style="color:#666" ></asp:TextBox><%=new BLL.TranslationBase().GetTran("006978", "条")%>
            </li>

        </ul>
        <div style="clear:both"></div>
</div>
</div>
<%=msg %>