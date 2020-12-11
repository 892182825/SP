<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDepotSeat.aspx.cs" Inherits="Company_SetParams_AddDepotSeat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加库位</title>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" /><script language="javascript" type="text/javascript" src="../../js/SqlCheck.js"></script>
    <style type="text/css">
        .style1
        {
            width: 142px;
        }
    </style>
            <script>
    	function CheckText(btname)
	{
		//这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		filterSql_II (btname);
		
	}
    </script>
        <script language="javascript" type="text/javascript">
    function confirmvlue()
    {
        return confirm('<%=GetTran("001688", "确定要清空吗？")%>');
    }
       function confirmvlue1()
    {
        return confirm('<%=GetTran("001718", "确实要删除吗？")%>');
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
<tr><td><asp:Button ID="Button1" runat="server" Text="添加库位" CssClass="anyes" 
                        onclick="Button1_Click"/>&nbsp;<asp:Button ID="Button2" runat="server" Text="返 回" CssClass="anyes" 
                        OnClick="btnBack_Click"/></td></tr>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                     <asp:GridView ID="gvDepotSeat" runat="server" AutoGenerateColumns="false" 
                onrowdatabound="gvDepotSeat_RowDataBound" DataKeyNames="ID" Width="100%" 
                  AllowSorting="true"  CssClass="tablemb" EmptyDataText="#">
                <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                <HeaderStyle Wrap="false" />
                <RowStyle HorizontalAlign="Center"  Wrap="false" />
                <Columns>                        
                    <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDepotSeatEdit" runat="server"
                                oncommand="lbtnDepotSeatEdit_Command"><%=GetTran("000259", "修改")%></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDepotSeatDelete" runat="server"
                                OnClientClick="return confirmvlue1()" 
                                oncommand="lbtnDepotSeatDelete_Command"><%=GetTran("000022", "删除")%></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="ID" HeaderText="编号"  ItemStyle-Wrap="false" Visible="false" />
                    <asp:BoundField DataField="WareHouseID" HeaderText="仓库编号" ItemStyle-Wrap="false" Visible="false" />
                    <asp:BoundField DataField="DepotSeatID"  HeaderText="库位编号" ItemStyle-Wrap="false" Visible="false" />
                    <asp:BoundField DataField="SeatName"  HeaderText="库位名称" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Remark" HeaderText="库位备注" ItemStyle-Wrap="false" /> 
                                                
                </Columns>
             <EmptyDataTemplate>
             <table class="tablemb" Width="100%" >
                                <tr>
                                    <th>
                                        <%=GetTran("000015", "操作")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001195", "编号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000877", "库位编号")%>
                                    </th>
                                    
                                    <th>
                                        <%=GetTran("000357", "库位名称")%>
                                    </th>
                                    <th>
                                        <%=GetTran("001738", "库位备注")%>
                                    </th>
                                </tr>                
                            </table>
             </EmptyDataTemplate>
            </asp:GridView>
                </td>
            </tr>                   
        </table>
        <br />
           <table border="0" cellpadding="0" cellspacing="0" class="tablemb" align="center" runat="server" id="tab1">
                        <tr>
                            <td align="right" class="style1"><%=GetTran("000355", "仓库名称")%>：</td>
                            <td>
                            <asp:Label ID="ddlWareHouse" runat="server" Text=""></asp:Label> </td>         
                        </tr>
                        <tr>
                            <td align="right" class="style1"><%=GetTran("000357", "库位名称")%>：</td>
                            <td><asp:TextBox ID="txtSeatName" MaxLength="16" runat="server"></asp:TextBox></td>
                        </tr>                        
                        <tr>
                            <td align="right" class="style1"><%=GetTran("000078", "备注")%>：</td>
                            <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnAdd" style="DISPLAY: none" runat="server" Text="确 定" onclick="btnAdd_Click" CssClass="anyes" />&nbsp;&nbsp;                                    
                                    
                                    <asp:linkbutton id="Linkbutton1"  Runat="server" Text="" style="DISPLAY: none"
                                                onclick="Linkbutton1_Click"></asp:linkbutton>
    <input class="anyes" id="Button4" onclick="CheckText('Linkbutton1')" type="button" value='<%=GetTran("000434", "确 定")%>' runat="server"></input>
                                    
                                    <asp:Button ID="btnReset" runat="server" Text="清 空" CssClass="anyes" 
                                        OnClientClick="return confirmvlue()" 
                                        onclick="btnReset_Click" />                                                                                             
                                </td>
                            </tr>       
                    </table>
                 </td></tr></table>                      
    </form>
</body>
</html>
