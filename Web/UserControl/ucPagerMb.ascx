<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPagerMb.ascx.cs" Inherits="UserControl_ucPagerMb" %>


<div id="divControls" class="pagination" runat="server">
    <div id="GridViewPaging" class="page-bottom">
        <%--总： <span><%= RecordCount %></span>&nbsp;&nbsp;当前<span ><%=PageIndex%></span> 尾页： <span ><%= PageCount %></span>--%>

        <asp:Button ID="lbtnFirst" Visible="false" runat="server" Text="第一页" readonly="readonly" OnClick="lbtnFirst_Click" title="第一页" class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'" Height="20px" Width="44px" />

        <asp:LinkButton ID="lbtnPre" runat="server" Text="上一页" OnClick="lbtnPre_Click" title="上一页" class="input2" />

        <!-- class="biaozzib" 1 2 3 4 5 … -->

        <asp:LinkButton ID="btn1" runat="server" Visible="false"
            OnClick="btn1_Click"></asp:LinkButton>

        <asp:Label ID="lbtn1" runat="server" Visible="false" CssClass="page-cur"></asp:Label>

        <asp:LinkButton ID="btn2" runat="server" Visible="false"
            class="input2" OnClick="btn2_Click"></asp:LinkButton>

        <asp:Label ID="lbtn2" runat="server" Visible="false" CssClass="page-cur"></asp:Label>

        <asp:LinkButton ID="btn3" runat="server" Visible="false"
            class="input2" OnClick="btn3_Click"></asp:LinkButton>

        <asp:Label ID="lbtn3" runat="server" Visible="false" CssClass="page-cur"></asp:Label>

        <asp:LinkButton ID="btn4" runat="server" Visible="false"
            class="input2" OnClick="btn4_Click"></asp:LinkButton>

        <asp:Label ID="lbtn4" runat="server" Visible="false" CssClass="page-cur"></asp:Label>

        <asp:LinkButton ID="btn5" runat="server" Visible="false"
            class="input2" OnClick="btn5_Click"></asp:LinkButton>

        <asp:Label ID="lbtn5" runat="server" Visible="false" CssClass="page-cur"></asp:Label>

        <asp:LinkButton ID="btn6" runat="server" Visible="false"
            class="input2" OnClick="btn6_Click"></asp:LinkButton>

        <asp:Label ID="lbtn6" runat="server" Visible="false" CssClass="page-cur"></asp:Label>


        <span class="page-next">
            <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click" title="下一页" class="page-next"><span>下一页</span></asp:LinkButton>
            <span class="page-skip"><%=tran.GetTran("001045","共")%><%= RecordCount %><%=tran.GetTran("006978","条")%>&nbsp;&nbsp;&nbsp;<%=tran.GetTran("007531", "转到第")%><asp:TextBox ID="txtPn" CssClass="prcSchPgForm" runat="server"></asp:TextBox><%=BLL.Translation.Translate("001055","页")%><img src="../member/images/products-SchBtn-2.png" id="GoTo" style="display: none;" runat="server" onclick="javascript:__doPostBack('ucPagerMb1$LinkButton1','')" />

                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="false">LinkButton</asp:LinkButton>
            </span>
            <asp:Button ID="lbtnLast" runat="server" Text="尾页" Visible="false" Enabled="False" OnClick="lbtnLast_Click" title="尾页" class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'" Height="20px" Width="44px" />
            <asp:DropDownList ID="ddlPages" runat="server" Visible="false"
                AutoPostBack="True" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged">
            </asp:DropDownList>
    </div>
    <input id="hidPageIndex" value="1" type="hidden" name="hidPageIndex"
        runat="server" />
    <input id="hidRecord" value="0" type="hidden" name="hidRecord" runat="server" />
    <input id="hidPageCount" value="0" type="hidden"
        name="hidPageCount" runat="server" />
    <input id="hidQueryStr"
        value=" 1=1 " type="hidden" name="hidPageCount" runat="server" />
    <input id="hidColumns" type="hidden" name="hidColumns"
        runat="server" />
    <input type="hidden" id="hidPageSize" value="10" runat="server" />
</div>
