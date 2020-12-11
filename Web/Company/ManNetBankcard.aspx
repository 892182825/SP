<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManNetBankcard.aspx.cs" Inherits="Company_ManNetBankcard" %>

<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head> 
<body>
    <form id="form1" runat="server">
    <div>
    <center>
    <table width="700px"  style="margin:30px;" class=" tablemb" >
    
       <tr><td colspan=2 align=center> <%=GetTran("007769", "设置会员网络汇入银行卡号")%> </td></tr>
      
       <tr><td align=right><%=GetTran("007770", "设置类型")%> ： </td><td align=left>
           <asp:RadioButtonList ID="rbtltype" runat="server" RepeatDirection=Horizontal>
           <asp:ListItem Selected="True" Text="设置网络" Value="0"></asp:ListItem>
              <asp:ListItem  Value=1 Text="设置个人"></asp:ListItem>
           </asp:RadioButtonList>
       </td></tr>
    <tr><td align=right>  <%=GetTran("007771","设置网络顶点编号")%>：</td><td align=left >  <asp:TextBox ID="txttopnumber"  MaxLength=15  Width=200 runat="server"></asp:TextBox></td></tr>
       <tr><td align=right>  <%=GetTran("007772", "排除网络顶点编号")%>：</td><td align=left >  <asp:TextBox ID="txtnosetnumber"    TextMode=MultiLine Height=60px MaxLength=4000 Width=200px runat="server"></asp:TextBox></td></tr>
    
    <tr><td align=right ><%=GetTran("007773", "汇入银行卡号")%>：</td><td  align=left>
        <asp:DropDownList ID="ddlbanklist" runat="server" Width=250px>
        </asp:DropDownList>
    </td></tr>
    <tr><td colspan=2> <asp:Button ID="btnsetcardnumber"  CssClass="anyes"  
            OnClientClick="return check();" runat="server" 
            Text="设置" onclick="btnsetcardnumber_Click"  /></td>  
            </tr>
            <tr><td colspan=2 align=left><%=GetTran("007775","提示： 如有多个网络不需要本次设置，则将需要排除的网络顶点编号用 / 隔开即可；<br />(例如：A网络下有B、C、D三个网络 ，当设置A网络，且排除B、C网络，则在排除网络输入框中输入  B/C 即可)")%><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetTran("007776", "如不填写，则默认设置该网络下所有编号的汇入卡号。")%></td></tr>
            </table>
      
      <table  class="biaozzi" width=90%>
      <tr><td align=left><%=GetTran("000024","会员编号")%>： 
          <asp:TextBox ID="txtnumber"  MaxLength=15 runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
              ID="btnsearch" CssClass="anyes" runat="server" Text="查询" 
              onclick="btnsearch_Click" /> </td></tr>
      <tr><td>
          <asp:GridView ID="gvsetbanklog" runat="server" CssClass="tablemb" Width=100% 
              AutoGenerateColumns="False" onrowdatabound="gvsetbanklog_RowDataBound">
              <Columns>
                  <asp:BoundField HeaderText="会员编号"  DataField="number" />
                  <asp:TemplateField HeaderText="会员姓名"  >
                  <ItemTemplate>
                 <%# getName( Eval("name"))%>
                  </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="设置类型">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="lbltype" runat="server"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="设置账号">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="lblbankinfo" runat="server"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="排除网络编号" >
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate   >
                          <asp:Label ID="lblnosetnumbers" runat="server"  title='<%#Eval("numberstr") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField HeaderText="操作人"  DataField="operatenum"/>
                  <asp:BoundField HeaderText="操作时间" DataField="modifytime" />
              </Columns>
              <EmptyDataTemplate>
              <table  width=100%>
               <tr><th><%=GetTran("000024","会员编号")%></th>
                 <th><%=GetTran("000025", "会员姓名")%></th>
                    <th><%=GetTran("007770", "设置类型")%></th>
                       <th><%=GetTran("007790", "设置账号")%></th>
                        <th><%=GetTran("007791", "排除网络编号")%></th>
                            <th><%=GetTran("007272", "操作人")%></th>
                              <th><%=GetTran("003259", "操作时间")%></th></tr>
              </table>
              
              </EmptyDataTemplate>
          </asp:GridView>
          <uc1:Pager ID="Pager1" runat="server" />
      </td></tr>
      
      </table>
       </center>
    </div>
    </form>
</body>
</html>
<script language="javascript">
    function check() {
        if (confirm('<%=GetTran("007797","确定要重新设置该会员网络的汇入银行卡号吗？") %>')) {
            return true;
        } else {
            return false;
        }
    }
</script>