<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagModify.aspx.cs" Inherits="Company_ManagModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改操作员信息</title>
       <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table  width="95%">
    <tr><td>
        <asp:Button ID="btnaddnew" runat="server" Text="添加新操作员" CssClass="anyes" 
            onclick="btnaddnew_Click" /></td></tr>
    <tr>
      <td> 
        <asp:GridView ID="gvcontrolors" Width=100% class="tablemb" 
              RowStyle-HorizontalAlign=Center runat="server" AutoGenerateColumns="False" 
              onrowcommand="gvcontrolors_RowCommand" 
              onrowdatabound="gvcontrolors_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="登录名"  DataField="username"  />
                <asp:BoundField HeaderText="添加时间"  DataField="addtime" />
                <asp:BoundField HeaderText="最后登录时间"  DataField="lastlogintime" />
                      <asp:BoundField HeaderText="最后登录ip"  DataField="lastloginip" />
                <asp:TemplateField HeaderText="操作">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbtnmodify" runat="server" CommandName="mdf" CommandArgument='<%# Eval("id") %>' ><%#GetTran("000259","修改")%></asp:LinkButton>
                        <asp:Label ID="Label1" runat="server" Text="/"></asp:Label>
                     <asp:LinkButton ID="lkbtnsetnouse" runat="server"  CommandName="use" CommandArgument='<%# Eval("username") %>' ><%#GetTran("007906", "禁用")%> </asp:LinkButton>  
                        <asp:Label ID="Label2" runat="server" Text="/"></asp:Label>
                  <asp:LinkButton ID="lkbtndelete" runat="server"  OnClientClick="return check();" CommandName="del" CommandArgument='<%# Eval("username") %>' ><%#GetTran("000022", "删除")%> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </td>
    </tr>
    <tr>
    <td >
        <asp:Panel  Width="300px" ID="panaddmodify" runat="server"  Visible=false>
            <asp:HiddenField ID="hdfid" runat="server" Value=0 />
        <table width=100% class="tablemb">
        <tr><td>
            <asp:Label ID="lblinfo" runat="server" Text=""></asp:Label></td></tr>
        <tr><td align=right><%=GetTran("007904","登录名")%>：</td><td>
            <asp:TextBox ID="txtusername" MaxLength=20 runat="server"   onblur="getvild( )" ></asp:TextBox> </td></tr>
             <tr><td align=right><%=GetTran("007908","登陆密码")%>：</td><td>
            <asp:TextBox ID="txtpass" MaxLength=50 TextMode=Password runat="server"></asp:TextBox> </td></tr>
             <tr><td colspan=2 align=center> 
                 <asp:Button ID="btnadd" runat="server" Text="添加操作员"  Visible=false    OnClientClick="return getvild( ) ;"
                     CssClass="anyes" onclick="btnadd_Click" />    <asp:Button ID="btnmodify" 
                     runat="server" CssClass="anyes" Text="修改操作员" Visible=false   OnClientClick="return getvild( ) ;"
                     onclick="btnmodify_Click" /> </td></tr>
        </table>
          
        </asp:Panel>
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
<script language=javascript>
 function getvild( ) 
 { 
    var rep=/\W+/;
    var ele =document.getElementById("txtusername");
   
    if(rep.test(ele.value))
    {
       alert("登录名只能是英文字母或数字或下划线！");
       return false;
    }
    else return true;
 }

 function check() {
     if (confirm('<%=GetTran("007907","确定要删除该操作员吗？") %>')) {
         return true;
     } else {
     return false;
     }
 }
</script>
