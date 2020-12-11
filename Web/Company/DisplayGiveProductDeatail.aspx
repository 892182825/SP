<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayGiveProductDeatail.aspx.cs" Inherits="Company_DisplayGiveProductDeatail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>单据详细</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript">
    function fanyi()
    {
        return '<%=GetTran("000421","有效") %>';
    }
</script>

<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center >
        <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
        <td>
          <asp:GridView  ID="givDoc" runat="server" AutoGenerateColumns="False" Width="100%"
                              
                CssClass="tablemb bordercss" OnRowDataBound="givDoc_RowDataBound"
                                AlternatingRowStyle-Wrap="False" FooterStyle-Wrap="False" HeaderStyle-Wrap="False"
                                PagerStyle-Wrap="False" SelectedRowStyle-Wrap="False">
                                <EmptyDataTemplate>
                                    <table class="tablebt" width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000000", "赠送起始PV")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "赠送结束PV")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "操作编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "操作时间")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <FooterStyle Wrap="False"></FooterStyle>
                                <Columns>
                                   <asp:BoundField DataField="totalpvStart" HeaderText="赠送起始PV"></asp:BoundField>
                                    <asp:BoundField DataField="totalpvEnd" HeaderText="赠送结束PV"></asp:BoundField>
                                    <asp:BoundField DataField="operatenum" HeaderText="操作编号"></asp:BoundField>
                                    <asp:TemplateField HeaderText="操作时间">
                                    <ItemTemplate>
                                        <asp:Label ID="lblregisterdate" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle Wrap="False"></PagerStyle>
                                <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                <HeaderStyle Wrap="False"></HeaderStyle>
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                            </asp:GridView>
                            </td>
        </tr>
    </table>
    <br />
        <table width="100%">
            <tr>
                <td style="border:rgb(147,226,244) solid 1px"><asp:GridView ID="givDocDitals" runat="server" AutoGenerateColumns="False" 
            onrowdatabound="givDocDitals_RowDataBound"
            width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD"  CssClass="tablemb bordercss"
            HeaderStyle-CssClass="tablebt bbb">
            <Columns>
                <asp:BoundField HeaderText="产品编号" DataField="ProductCode" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="产品名称" DataField="ProductName" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="数量" DataField="ProductQuantity" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:BoundField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" DataField="TotalPrice" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                <asp:BoundField HeaderText="总积分" DataField="Pv" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
            </Columns>
            <AlternatingRowStyle BackColor="#F1F4F8" />
        </asp:GridView></td>
            </tr>
        </table>
          <table width="100%">
          <tr>
            <td align="left">
               
            <input type="button" ID="butt_Query" value='<%=GetTran("000421","返回") %>' style="cursor:pointer" Class="anyes" onclick="history.back()"/></td>
          </tr>
          </table>
        
        
        </center>
    </div>
    </form>
</body>
</html>

