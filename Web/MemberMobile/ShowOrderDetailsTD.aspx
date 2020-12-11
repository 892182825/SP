<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowOrderDetailsTD.aspx.cs" Inherits="Member_ShowOrderDetailsTD" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="x-ua-compatible" content="ie=11" />
<head id="Head1" runat="server">
    <title>开户浏览</title>
    <link href="../Company/CSS/Company.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
                           
    function getArgs(parm) //parm: 地址栏上参数名称
    {
      var pars=location.search;//获取当前url
      var pos=pars.indexOf('?');//查找第一个?
      pars=pars.substring(pos+1);//获取参数部分
      var ps=pars.split("&");
      var temp;
      var name,value,index;
      for(var i=0;i<ps.length;i++)
      {
        temp=ps[i]; 
        index=temp.indexOf("=");
        if(index==-1) continue;//如果参数中未包含=则继续
        name=temp.substring(0,index);//参数名称
        if(name==parm)
        {
            value=temp.substring(index+1);//参数的值
            document.location.href=value+".aspx";
        }     
      }
    }
    </script>

</head>
<body leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
    <form id="Form1" method="post" runat="server">
    <br />
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="biaozzi">
        <tr>
            <td height="48" align="center">
                <font size="3"><b><%=GetTran("000884", "订单明细")%></b></font>
            </td>
        </tr>
    </table>
     <table style="width: 100%;" id="talbe1" runat="server" cellpadding="0" cellspacing="1"
        border="0" class="tablemb" align="center">
        <tr>
            <td >
                 <asp:GridView ID="gvorder" runat="server" Width="100%" CssClass="tablemb bordercss" AutoGenerateColumns="False"
                                OnRowDataBound="gvorder_RowDataBound" BackColor="white">
                                <HeaderStyle HorizontalAlign="Center" CssClass="menuhome"></HeaderStyle>
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <Columns>
                                    
                                    <asp:BoundField DataField="OrderExpectNum" HeaderText="期数"></asp:BoundField>
                                    <asp:BoundField DataField="zhifu" HeaderText="支付状态"></asp:BoundField>
                                    <asp:BoundField DataField="OrderID" HeaderText="订单号"></asp:BoundField>
                                    <asp:BoundField  DataField="Number" HeaderText="会员编号"></asp:BoundField>
                                    <asp:BoundField  DataField="Name" HeaderText="会员姓名"></asp:BoundField>
                                    <asp:BoundField DataField="TotalMoney" ItemStyle-HorizontalAlign="Right" HeaderText="金额"
                                        DataFormatString="{0:n2}">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Totalpv" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                                        HeaderText="积分">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="报单日期">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# GetOrderDate(Eval("orderdate")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StoreID" HeaderText="确认店铺"></asp:BoundField>
                                    <asp:BoundField DataField="fuxiaoname" HeaderText="报单途径"></asp:BoundField>
                                    <asp:BoundField DataField="dpqueren" HeaderText="店铺确认"></asp:BoundField>
                                    <asp:BoundField DataField="gsqueren" HeaderText="公司确认"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="1">
                                        <tr>
                                            
                                            <th>
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000775", "支付状态")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000079", "订单号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000322", "金额")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000414", "积分")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001429", "报单日期")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000793", "确认店铺")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000774", "报单途径")%>
                                            </th>     
                                            <th>
                                                <%=GetTran("006049", "店铺确认")%>
                                            </th>         
                                            <th>
                                                <%=GetTran("006048", "公司确认")%>
                                            </th>                                                    
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
            </td>
        </tr>
     
       
    </table>
    <br />
    <table cellspacing="0" cellpadding="0" border="0" width="100%" align="center">
        <tr>
            <td valign="top" align="center">
                <asp:GridView ID="myDatGrid" runat="server" AutoGenerateColumns="False" AllowSorting="false"
                    Width="100%" CssClass="tablemb" onrowdatabound="myDatGrid_RowDataBound">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <Columns>
                        <asp:BoundField DataField="OrderID" ItemStyle-HorizontalAlign="Center" HeaderText="订单号">
                        </asp:BoundField>
                        <asp:BoundField DataField="StoreID" HeaderText="店铺编号" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="ProductName" HeaderText="产品名称" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="ProductTypeName" HeaderText="产品型号" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="数量" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="单价" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right">
                        </asp:BoundField>
                        <asp:BoundField DataField="pv" HeaderText="积分" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:f2}"></asp:BoundField>
                        <asp:BoundField DataField="totalmoney" HeaderText="总金额" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:f2}"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table cellspacing="0" cellpadding="0" border="1" width="100%">
                            <tr>
                                <th>
                                    <%=GetTran("000079", "订单号")%>
                                </th>
                                <th>
                                    <%=GetTran("000150", "店铺编号")%>
                                </th>
                                <th>
                                    <%=GetTran("000501", "产品名称")%>
                                </th>
                                <th>
                                    <%=GetTran("000882", "产品型号")%>
                                </th>
                                <th>
                                    <%=GetTran("000505", "数量")%>
                                </th>
                                <th>
                                    <%=GetTran("000503", "单价")%>
                                </th>
                                <th>
                                    <%=GetTran("000414", "积分")%>
                                </th>
                                <th>
                                    <%=GetTran("000041", "总金额")%>
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="left" class="biaozzi"><br />
                <!--  <a visible="false" onclick="getArgs('form')" style="cursor: hand">返回</a>>
                <a onclick="history.go(-1)" style="cursor: hand">返回</a -->
                <asp:Button ID="btnE" runat="server" Text="返 回" CssClass="anyes" 
                    onclick="btnE_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
