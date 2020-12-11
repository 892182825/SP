<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Backaward.aspx.cs" Inherits="Company_Backaward" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>回本奖</title>
     <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>
<body>

<form id="form1" runat="server">
    <br />
        <div>
                <table width="90%" style="    margin-left: 11%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="90%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                <tr>
                                    <td align="left" style="word-break: keep-all; word-wrap: normal">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    Width="90%" CssClass="tablemb" ShowFooter="True">
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
                                                <asp:BoundField HeaderText="报单编号" DataField="OrderId" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="层数" DataField="cengshu" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="业绩" DataField="yj" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="回本业绩" DataField="cpyj" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="回本比例" DataField="cpbl" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="奖金" DataField="Bonus" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundField>
                                                
                                               
                                            </Columns>
                                            <EmptyDataTemplate>
                                            <table width="100%"   class="tablemb">
                                            <tr>
                                              
                                                <th>会员编号</th>
                                                <th>下级编号</th>
                                                <th>报单编号</th>
                                              
                                                <th>层数</th>
                                                <th>业绩</th>
                                                <th>回本业绩</th>
                                                <th>回本比例</th>
                                                <th>奖金</th>
                                              
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
