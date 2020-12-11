<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yongxujiang.aspx.cs" Inherits="Company_yongxujiang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>永续奖</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>


    <form id="form1" runat="server">
    <br />
        <div style="margin-left:3%">
                <table width="90%" style="    margin-left: 7%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="90%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                <tr>
                                    <td align="left" style="word-break: keep-all; word-wrap: normal">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    Width="100%" CssClass="tablemb" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="#F1F4F8" />
                                    <HeaderStyle CssClass="tablebt" />
                                    <RowStyle HorizontalAlign="Center" />
                                 
                                            <Columns>
                                               <asp:BoundField HeaderText="会员编号" DataField="number" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="下级编号" DataField="DownNumber" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                               
                                                <asp:BoundField HeaderText="代数" DataField="daishu" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="奖金" DataField="Bonus" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                <%--<asp:BoundField  HeaderText="会员编号" DataField="number"/>
                                                 <asp:BoundField  HeaderText="下级编号" DataField="DownNumber"/>
                                                
                                                 <asp:BoundField  HeaderText="代数" DataField="daishu"/>
                                                 <asp:BoundField  HeaderText="奖金" DataField="Bonus"/>--%>
                                               
                                            </Columns>
                                            <EmptyDataTemplate>
                                            <table width="100%"   class="tablemb">
                                            <tr>
                                            
                                               <th style="text-align: center;">会员编号</th>
                                               <th style="text-align: center;">下级编号</th>
                                               <th style="text-align: center;">代数</th>
                                               <th style="text-align: center;">奖金</th>
                                              
                                            </tr>
                                        </table>
                                                </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
         
                
         <td>
            <asp:button id="BtnConfirm" runat="server" Text="返 回" CssClass="anyes" onclick="BtnConfirm_Click" Visible=false></asp:button></li>
            <input type="button" Height="27px" Width="47px" Style="margin-top:2%; margin-left: 46%; padding: 2px 9px; color: #FFF; border: 1px solid #132022; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);" class="&quot;anyes&quot;" value="返 回"' onclick="window.history.back(-1);" />
        </td>
        </div>
      
    </form>
    
</body>
</html>
