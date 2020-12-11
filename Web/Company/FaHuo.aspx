<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuo.aspx.cs" Inherits="Company_FaHuo" %>

<%@ Register src="../UserControl/WareHouseDepotSeat.ascx" tagname="WareHouseDepotSeat" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <style type="text/css">
        .ddtable td
        {
            white-space:nowrap;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server"><br>
        <table width="80%" border="0" cellpadding="0" cellspacing="1" bgcolor="#F8FBFD" class="tablemb ddtable"  id="tbColor">
            <tr>
                <th colspan="4" align="center" background="images/tabledp.gif" class="tablebt">
                    <asp:Label ID="Label1" runat="server" Text='<%#GetTran("001728","填写发货单") %>'></asp:Label>
                </th>
            </tr>
             <tr style="background-color:white">
                <td align="right">
                    <%=GetTran("001216","单据日期")%>：</td>
                 <td>
                     <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
                 <td align="right">
                    <%=GetTran("000079","订单号")%>：</td>
                 <td>
                     <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
               
            </tr>
             <tr style="background-color:#F1F4F8">
                
                 <td align="right">
                    <%=GetTran("001271","出库类别")%>：
                </td>
                 <td>
                     <asp:TextBox ID="TextBox3" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
                 <td align="right">
                    <%=GetTran("000519","经办人")%>：
                </td>
                 <td>
                     <asp:TextBox ID="TextBox4" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
            </tr>
             <tr style="background-color:white">
                
                 <td align="right">
                    <%=GetTran("001273","原始单据号")%>：
                </td>
                 <td>
                     <asp:TextBox ID="TextBox5" runat="server" ReadOnly="true" style="color:gray"></asp:TextBox>
                </td>
                 <td align="right">
                    <asp:Label ID="fhck" Visible="false" runat="server" Text='<%#GetTran("001279","发货仓库") %>'/> <%=GetTran("007206", "快递单号")%>：
                     </td>
                 <td>
                    
                     <asp:DropDownList ID="DropDownList1" runat="server" 
                         DataTextField="WareHouseName" DataValueField="WareHouseID" AutoPostBack="True" 
                         onselectedindexchanged="DropDownList1_SelectedIndexChanged" Visible="false">
                         
                     </asp:DropDownList>
                    
                     <%--<%=GetTran("000390","库位")%>：--%><asp:DropDownList ID="DropDownList2" runat="server"  Visible="false" DataTextField="SeatName" DataValueField='DepotSeatID'>
                         
                     </asp:DropDownList>
                    <asp:TextBox ID="txtCarry" runat="server"></asp:TextBox>

                </td>
            </tr>
             <tr  style="background-color:#F1F4F8">
                 <td align="right">
                    <%=GetTran("001276","附加信息")%>：</td>
                    
                 <td>
                     <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
                
                 <td align="right">
                    <%=GetTran("000121","物流公司")%>：  
                </td>
                <td>
                    
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text='<%#GetTran("001707","确定发货") %>' 
                                            style="cursor:pointer"  CssClass="another"
                                                onclick="Button1_Click" align="absmiddle" 
                         Height="22px" />
      
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                
                     <asp:Button ID="Button3" runat="server" Text='<%#GetTran("000096","返 回") %>' style="cursor:pointer"  
                         CssClass="anyes" align="absmiddle" 
                         Height="22px" onclick="Button3_Click"/>                   
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
