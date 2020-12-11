<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Joinreceipt.aspx.cs" Inherits="Member_Joinreceipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link type="text/css" rel="Stylesheet" href="../store/Css/store.css" />
    <link href="CSS/member.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //币种变时，钱也要变。（表格列数变化时一定要更改此方法）
        var first=true; 
        var from=0;
        function setHuiLv(th)
        {   
            if(first)
            {
                from=AjaxClass.GetCurrency().value-0;
                first=false;
            }
            
            var to=th.options[th.selectedIndex].value-0;
            
            
            var hl=AjaxClass.GetCurrency_Ajax(from,to).value;
            
            var trarr=document.getElementById("gvOrder").getElementsByTagName("tr");
            for(var i=1;i<trarr.length;i++)
            {
                trarr[i].getElementsByTagName("td")[2].innerHTML=
                (parseFloat(trarr[i].getElementsByTagName("td")[2].firstChild.nodeValue.replace(/,/g,""))/hl).toFixed(2);
            }
            
            from=to;
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server"><br />
    <table width="90%" class="biaozzi" >
        <tr>
            <td>
                <%=GetTran("000000", "会员编号")%>：
                <asp:Label ID="lblStoreId" runat="server" ForeColor="Red"></asp:Label>
                
                &nbsp;&nbsp;<%=GetTran("000562")%>：
            <asp:dropdownlist id="Dropdownlist1" runat="server" Width="100px" 
                                    EnableViewState="False" onchange="setHuiLv(this)"></asp:dropdownlist>    
            </td>
        </tr>
    </table><br /><br /><br />
    <table width="90%" class="biaozzi" style="border:rgb(147,226,244) solid 1px;width:100%" >
        
        <tr>
            <td>
                <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" Width="100%"
                    CssClass="tablemb bordercss" OnRowDataBound="gvOrder_RowDataBound" EmptyDataText='<%#GetTran("002206","没有查询到记录") %>'>
                    <HeaderStyle CssClass="title" />
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckIsChoose" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StoreOrderID" HeaderText="订单号" />
                        <asp:BoundField DataField="TotalMoney" HeaderText="订单消费金额" />
                        <asp:BoundField DataField="TotalPv" HeaderText="订单消费PV" />
                        <asp:TemplateField HeaderText="收货人">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Encryption.Encryption.GetDecipherName(Eval("InceptPerson").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货人地址">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Encryption.Encryption.GetDecipherAddress(Eval("InceptAddress").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
   
                
                        <asp:TemplateField HeaderText="收货人电话">
                            <ItemTemplate>
                                <asp:Label ID="Label2dd" runat="server" Text='<%# Encryption.Encryption.GetDecipherTele(Eval("Telephone").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="是否付款">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# CheckYorN(Eval("IsCheckOut")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Carriage" HeaderText="运费" />
                        <asp:BoundField DataField="Weight" HeaderText="重量" />
                        <asp:BoundField DataField="kuaididh" HeaderText="快递单号" />
                        <asp:TemplateField HeaderText="是否有问题">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbtnQuestion" runat="server" CommandArgument='<%# Eval("StoreOrderID") %>'
                                    OnClick="lkbtnQuestion_Click"><%=GetTran("001831", "填入")%></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table">
                            <tr class="title">
                            <th><%=GetTran("000000", "选择")%></th>
                            <th><%=GetTran("000079", "订单号")%></th>
                            <th><%=GetTran("002102", "订单消费金额")%></th>
                            <th><%=GetTran("002103", "订单消费PV")%></th>
                            <th><%=GetTran("001850", "收货人")%></th>
                            <th><%=GetTran("000393", "收货人地址")%></th>
                            <th><%=GetTran("000403", "收货人电话")%></th>
                            <th><%=GetTran("000352", "是否付款")%></th>
                            <th><%=GetTran("000120", "运费")%></th>
                            <th><%=GetTran("000118", "重量")%></th>
                            <th><%=GetTran("002109", "是否有问题")%></th>
                            </tr>
                            </table>
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="#F1F4F8" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnReceived" runat="server" CssClass="anyes" Text="货已收到" OnClick="btnReceived_Click">
                </asp:Button>
            </td>
            
        </tr>
        <tr>
            <td style="font-size:10pt;color:red; font-size:12px;">
                <br>
                <%=GetTran("000649", "功能说明")%>：<br>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <%=GetTran("006866", "1、公司发货后，会在此处出现一条待收货的订单，打勾点击“货已收到”表示会员已经收到公司发出的货物。")%>
            </td>
        </tr>
    </table>
    <%=msg %>
    </form>
</body>
</html>
