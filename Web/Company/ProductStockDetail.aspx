<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductStockDetail.aspx.cs"
    Inherits="Company_ProductStockDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=msg%>
    </title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <br />
        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="biaozzi">
            <tr>
                <td align="center"><%=GetTran("000317", "产品各库位明细")%>
                   (<asp:Label ID="lbl_title" runat="server">Label</asp:Label>)
                </td>
            </tr> 
        </table>        
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" class="tablemb">
          
            <tr>
                <td style=" white-space:nowrap">
                    <asp:GridView ID="gvWareHouse" runat="server" AutoGenerateColumns="false" 
                        Width="100%" onrowdatabound="gvWareHouse_RowDataBound" >
                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle CssClass="tablemb" Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />		
                        <Columns>                
                            <asp:BoundField DataField="ProductCode" HeaderText="产品编码" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ProductName" HeaderText="产品名称" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="WareHouseName" HeaderText="仓库名称" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="SeatName" HeaderText="库位名称" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Totalin" HeaderText="入库数量" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalOut" HeaderText="出库数量" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalEnd" HeaderText="实际数量" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="AlertnessCount" HeaderText="预警数量" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />                            
                        </Columns>
                    </asp:GridView>                 
                       
                    <asp:GridView ID="gvStore" runat="server" AutoGenerateColumns="false" 
                        Width="100%" onrowdatabound="gvStore_RowDataBound" >
                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle CssClass="tablemb" Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />		
                        <Columns>                
                            <asp:BoundField DataField="storename" HeaderText="店铺名称" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="name" HeaderText="店长姓名" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ProductCode" HeaderText="产品编码" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Totalin" HeaderText="入库数量" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="TotalOut" HeaderText="出库数量" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="TotalEnd" HeaderText="实际数量" ItemStyle-Wrap="false" />                           
                            <asp:BoundField DataField="AlertnessCount" HeaderText="在途数量" ItemStyle-Wrap="false" />
                        </Columns>
                    </asp:GridView>   
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lbl_message" runat="server">Label</asp:Label>
                </td>
            </tr>
        </table>
    <p>
                    <asp:Button ID="Button3" runat="server" Text="返 回"  
            CssClass="anyes" onclick="Button3_Click" />
                    </p>
    </form>
</body>
</html>
