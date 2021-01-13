<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xiajimingdanxx.aspx.cs" Inherits="Company_SetParams_xiajimingdanxx" %>

<%@ Register Src="../../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>下级业绩名单</title>
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
              <tr id="kq1">
                <td align="right">会员姓名：</td>
                <td>
                    <asp:TextBox ID="txtMoney" runat="server" MaxLength="40"></asp:TextBox>
                   
                </td>
            </tr>      
          
           <tr>
               <td align="right"><asp:Button ID="jsjj" runat="server" Text="查询" CssClass="anyes" OnClick="jsjj_Click" /></td>
                
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
                                        <asp:BoundField DataField="TotalOneMark" HeaderText="个人等级"></asp:BoundField>
                                        <asp:BoundField DataField="DTotalNetRecord" HeaderText="团队业绩"></asp:BoundField>
                                        <asp:BoundField DataField="cw" HeaderText="层位"></asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="姓名"></asp:BoundField>
                                        <asp:BoundField DataField="OrderDate" HeaderText="报单时间"></asp:BoundField>
                                        <asp:BoundField DataField="bd" HeaderText="报单类型"></asp:BoundField>
                                        <asp:BoundField DataField="TotalPv" HeaderText="报单金额"></asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%">
                                            <tr>
                                                <th>手机号
                                                </th>
                                                <th>个人等级
                                                </th>
                                                <th>团队业绩
                                                </th>
                                                <th>层位
                                                </th>
                                                <th>姓名
                                                </th>
                                                <th>报单时间
                                                </th>
                                                <th>报单类型
                                                </th>
                                                <th>报单金额
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
