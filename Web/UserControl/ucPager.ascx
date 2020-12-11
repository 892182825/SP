<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPager.ascx.cs" Inherits="UserControl_ucPager" %>
<style>

.input1{background:#fafafa;border:#777 solid 1px;width:34px; height:20px;cursor:pointer; color:#ff0000}
.input2{background:#fff; border:#999 solid 1px;width:34px; height:20px;cursor:pointer; color:#555; text-align:center;}

</style>
<div id="divControls" runat="server">
<table border="0" cellspacing="0" cellpadding="0" width="100%" style="height:30px">
  <tr>
    <td  align="right">总： <span><%= RecordCount %></span>&nbsp;&nbsp;当前<span ><%=PageIndex%></span> 尾页： <span ><%= PageCount %></span>
    
	 <asp:Button ID="lbtnFirst" runat="server" Text="第一页"  readonly="readonly"  onclick="lbtnFirst_Click" title= "第一页"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="44px"/>
			&nbsp;
        <asp:Button ID="lbtnPre" runat="server" Text="上一页" Enabled="False" onclick="lbtnPre_Click" title="上一页" class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="55px"/>
			&nbsp;
      <!-- class="biaozzib" 1 2 3 4 5 … -->    <asp:Button ID="btn1" runat="server"  Visible="false"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="30px"></asp:Button>
          <asp:Button ID="btn2" runat="server"   Visible="false"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="30px"></asp:Button>
            <asp:Button ID="btn3" runat="server"   Visible="false"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="30px"></asp:Button>
              <asp:Button ID="btn4" runat="server"   Visible="false"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="30px"></asp:Button>
                <asp:Button ID="btn5" runat="server"   Visible="false"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="30px"></asp:Button>
                  <asp:Button ID="btn6" runat="server"  Visible="false"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="30px"></asp:Button>
  
			  <asp:Button ID="lbtnNext" runat="server" Text="下一页" Enabled="False"  onclick="lbtnNext_Click" title="下一页"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="55px"/>
			&nbsp;
				 <asp:Button ID="lbtnLast" runat="server" Text="尾页" Enabled="False"  onclick="lbtnLast_Click" title= "尾页"  class="input2" onmousemove="this.className='input1'" onmouseout="this.className='input2'"  Height="20px" Width="44px"/>
			<span>
					转到：</span> <asp:DropDownList id="ddlPages" runat="server" 
            AutoPostBack="True" onselectedindexchanged="ddlPages_SelectedIndexChanged"></asp:DropDownList></td></tr></table>
            
            <input 
id="hidPageIndex" value="1" type="hidden" name="hidPageIndex" 
runat="server"/> <input id="hidRecord" value="0" type="hidden" name="hidRecord" runat="server"/> <input id="hidPageCount" value="0" type="hidden" 
name="hidPageCount" runat="server"/> <input id="hidQueryStr" 
value=" 1=1 " type="hidden" name="hidPageCount" runat="server"/> 
<input id="hidColumns" type="hidden" name="hidColumns" 
runat="server"/> 
<input type="hidden" id="hidPageSize" value="10"  runat="server"/>
</div>