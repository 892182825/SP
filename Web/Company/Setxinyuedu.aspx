<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Setxinyuedu.aspx.cs" Inherits="Setxinyuedu" %>

<%@ Register src="../UserControl/ucPager.ascx" tagname="ucPager" tagprefix="uc1" %>
<%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head> 
<body>
    <form id="form1" runat="server">
    <div>
    <center>
    <table width="400px"  style="margin:30px;" class=" tablemb" >
    <tr><td colspan=2 align=center> <%=GetTran("007837", "设置会员信誉额")%> </td></tr>
    <tr><td align=right>  <%=GetTran("000024","会员编号")%>：</td><td align=left >  <asp:TextBox ID="txttopnumber"  MaxLength=15    runat="server"></asp:TextBox></td></tr>
    <tr><td align=right ><%=GetTran("007278", "设置信誉额")%>：</td><td  align=left>
        <asp:TextBox ID="txtxinyue" MaxLength=15 onblur="vlidatenaa()"  runat="server"></asp:TextBox>
    </td></tr>
    
          <tr><td align=right ><%=GetTran("007838", "信誉额有效期")%>：</td><td  align=left>
        <asp:TextBox ID="txtlimitdate"    onfocus="WdatePicker();"   runat="server"></asp:TextBox>
    </td></tr><tr><td colspan=2> <asp:Button ID="btnsetcardnumber"  CssClass="anyes"  
            OnClientClick="return check();" runat="server" 
            Text="设置" onclick="btnsetcardnumber_Click"  /></td> </tr></table>
      <table  width=90% class="biaozzi">
      
      <tr>
      <td align=left> &nbsp; &nbsp; &nbsp; &nbsp; <%=GetTran("000024", "会员编号")%>：<asp:TextBox ID="txtnumber" runat="server"  MaxLength=50></asp:TextBox>  &nbsp; &nbsp; &nbsp; &nbsp; <%=GetTran("000025","会员姓名")%>：<asp:TextBox ID="txtname" runat="server"  MaxLength=50></asp:TextBox>  &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button 
              ID="btnsearch" runat="server" Text="查询"  CssClass="anyes" 
              onclick="btnsearch_Click"/></td></tr>
      <tr>
      <td>
          <asp:GridView ID="gvclog" runat="server" AutoGenerateColumns="False"  CssClass=" tablema" Width="100%"   >
              <Columns>
                  <asp:BoundField DataField="number" HeaderText="会员编号" />
                  <asp:BoundField DataField="name" HeaderText="姓名" />
                  <asp:BoundField DataField="douser" HeaderText="操作人" />
                  <asp:BoundField HeaderText="有效期至" DataField="CreditLimitdate" />
                  <asp:BoundField DataField="CreditLimit" DataFormatString="{0:0.00}" 
                      HeaderText="设置额度" />
              </Columns>
              <EmptyDataTemplate>
              <table Width="100%" CssClass=" tablema">
              <tr><th><%=GetTran("000024", "会员编号")%></th> 
               <th><%=GetTran("000107", "姓名")%></th>  
               <th><%=GetTran("007272", "操作人")%></th> 
              <th><%=GetTran("007843", "有效期至")%></th> 
               <th><%=GetTran("007844", "设置额度")%></th></tr>
              </table>
              </EmptyDataTemplate>
          </asp:GridView>
          <uc2:Pager ID="Pager1" runat="server" />
      </td></tr>
      </table>
       </center>
    </div>
    </form>
</body>
<script language=javascript>
   function vlidatenaa(){
         var money=document.getElementById("txtxinyue");
         
         var mon=money.value;
         
         if(isNaN(mon))
         {
             alert('<%=GetTran("007845","信誉额请输入数字") %>！');
          money.value="";
         }
        
      }

      function check() {
          if (confirm('<%=GetTran("007839","确定要设置该网会员的信誉额度吗？") %>')) {
              return true;
          } else {
          return false;
          }
      }
</script>
</html>
