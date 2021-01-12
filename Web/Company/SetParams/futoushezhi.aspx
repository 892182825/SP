<%@ Page Language="C#" AutoEventWireup="true" CodeFile="futoushezhi.aspx.cs" Inherits="Company_SetParams_futoushezhi" %>

<%@ Register Src="../../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>

    <script type="text/javascript">
     function getname() {
         var number = document.getElementById("txtOpen").value;
            
                        var name = AjaxClass.GetPetName(number, "Member").value;
                    
                        document.getElementById("txtName").innerHTML = name;
              
        }
    </script>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="600px" align="center" cellpadding="0" cellspacing="3" class="tablemb">
            <tr >
                <td align="right">会员手机号：</td>
                <td>
                    <asp:TextBox ID="txtOpen" onblur="getname()" runat="server" MaxLength="40"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
               <td align="right">姓名：</td>
                <td>
                    <asp:Label ID="txtName" runat="server" MaxLength="10"></asp:Label>
                </td>
            </tr>
            <tr id="kq1">
                <td align="right">复投金额：</td>
                <td>
                    <asp:TextBox ID="txtMoney" runat="server" MaxLength="10"></asp:TextBox>
                   
                </td>
            </tr>
            
          
            <tr style="white-space:nowrap">
                <td colspan="2" align="center"><br />                                               
                    <asp:Button ID="btnOK" runat="server" Text="确 定" OnClientClick="return ale()" onclick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;
                    <asp:Button ID="lbtnReturn" runat="server" Text="返 回" CssClass="anyes" onclick="lbtnReturn_Click" />               
                </td>
            </tr>   
        <tr>
               <td align="right">上级奖金：<asp:Button ID="jsjj" runat="server" Text="计算" CssClass="anyes" OnClick="jsjj_Click" /></td>
                
                   <td colspan="2" style="white-space:nowrap">
                       </td>
            </tr>          
        </table>    
                                <table width="70%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                    BorderStyle="Solid"  CssClass="tablemb">
                                    <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                    <RowStyle HorizontalAlign="Center" Wrap="false" />
                                    <Columns>
                                        
                                        <asp:BoundField DataField="MobileTele" HeaderText="手机号"></asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="姓名"></asp:BoundField>
                                        <asp:BoundField DataField="level" HeaderText="等级"></asp:BoundField>
                                        <asp:BoundField DataField="level2" HeaderText="节点"></asp:BoundField>
                                        <asp:BoundField DataField="Sex" HeaderText="帕点"></asp:BoundField>
                                        <asp:BoundField DataField="cw" HeaderText="向上层位"></asp:BoundField>
                                        
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%">
                                            <tr>
                                                <th>手机号
                                                </th>
                                                <th>姓名
                                                </th>
                                                <th>等级
                                                </th>
                                                <th>节点
                                                </th>
                                                <th>帕点
                                                </th>
                                                <th>向上层位
                                                </th>
                                                
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
            <tr>
            <td>
                <table width="70%">
                    <tr>
                        <td>
                            <uc2:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
                    </table>
                            
                
    </div>
    </form>
</body>
</html>
