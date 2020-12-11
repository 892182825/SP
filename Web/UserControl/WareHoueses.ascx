<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WareHoueses.ascx.cs" Inherits="WareHouses" %>
<script language="javascript" type="text/javascript">
function showNext(sid)
{
    document.getElementById("<%=txtwarehouse.ClientID%>").value=sid;
    var tb=WareHouses.getDepotseat(sid).value;
    if (tb!=null&&typeof(tb)=="object")
    {
        var slt=document.getElementById("<%=drpdepotseat.ClientID%>");
        slt.options.length=0; 
        slt.options.add(new Option("请选择",0));
        for(var i=0;i<tb.Rows.length;i++)
        {
            var Name=tb.Rows[i].Name;
            var ID=tb.Rows[i].ID;
            slt.options.add(new Option(Name,ID));
        }
    }
}
function getdepotseat()
{
      var obj=document.getElementById("<%=drpdepotseat.ClientID%>");
      var sid=obj.options[obj.selectedIndex].value;
     document.getElementById("<%=txtdepotseat.ClientID%>").value=sid;
}
</script>
<table>
<tr>
<td>仓库:</td>
<td>
    <asp:DropDownList ID="drpwarehouse" runat="server" >
    </asp:DropDownList>
</td>
<td>库位：</td>
<td>
    <asp:DropDownList ID="drpdepotseat" runat="server">
    </asp:DropDownList>
</td>
</tr>
<tr style="display:none;">
<td colspan="2">
    <asp:TextBox ID="txtwarehouse" runat="server"></asp:TextBox></td>
<td colspan="2">
    <asp:TextBox ID="txtdepotseat" runat="server"></asp:TextBox></td>
</tr>
</table>
    
