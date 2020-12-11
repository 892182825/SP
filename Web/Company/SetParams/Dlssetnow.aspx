<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dlssetnow.aspx.cs" Inherits="Company_SetParams_Dlssetnow" EnableEventValidation="false"%>

<%@ Register TagPrefix="ucl" TagName="uclCountry" Src="~/UserControl/Country.ascx"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MemberBank</title>       
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">
    function confirmvalue()
    {
        return confirm('<%=GetTran("001718", "确实要删除吗？")%>');
    }
    
    function cutManagement()
    {
         document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
    }
    
    function cutDescription()
    {
         document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
    }   
            
    window.onload=function()
    {
        down3();               
    };
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
    <table width="100%">        
        <tr>
            <td>
                <div >                   
        <table width="100%" cellpadding="0" cellspacing="0" border="0" >            
            <tr>
                <td>
                     <asp:GridView ID="gvMemberBank" runat="server" AllowSorting="True" 
                         AutoGenerateColumns="False" DataKeyNames="id"                         
                         onrowdatabound="gvMemberBank_RowDataBound" Width="100%" 
                         onsorting="gvMemberBank_Sorting" CssClass="tablemb" EnableModelValidation="True" OnRowCommand="gvMemberBank_RowCommand" >
                         <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />
                        <Columns>
                            <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                                <ItemTemplate>             
                                    
                                    <asp:Button ID="btnclose"  CssClass="anyes"  Visible="false" CommandArgument='<%#Eval("id") %>' CommandName="Cl" runat="server" Text="关闭" />  
                                      <asp:Button ID="btnopen"  CssClass="anyes"  Visible="false" CommandArgument='<%#Eval("id") %>' CommandName="OP" runat="server" Text="开启" /> 
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtEdit" runat="server"
                                        oncommand="lbtEdit_Command" Visible="false"><%=GetTran("000259", "修改")%></asp:LinkButton>
                                     <asp:LinkButton  ID="lbtDelete" runat="server" OnClientClick="return confirmvalue()"
                                        oncommand="lbtDelete_Command"><%=GetTran("000022", "删除")%></asp:LinkButton>                                    
                                </ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="id" SortExpression="id" HeaderText="编号" ItemStyle-Wrap="false" Visible="false" >
<ItemStyle Wrap="False"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="number" SortExpression="number" HeaderText="会员编号" ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="发奖金状态" SortExpression="number">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("opct") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("opct") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="更新时间" SortExpression="lastuptime">
                                 <EditItemTemplate>
                                     <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("lastuptime") %>'></asp:TextBox>
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("lastuptime") %>'></asp:Label>
                                 </ItemTemplate>
                                 <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                                                                                                  
                        </Columns>                        
                    </asp:GridView>
                 </td>                
            </tr>
        </table>
        <br />
        <table>            
            <tr style="white-space:nowrap">
                <td>
                    <asp:Button ID="btnAdd" Text="添 加" runat="server" onclick="btnAdd_Click" CssClass="anyes" />&nbsp;&nbsp;
                    <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_Click" CssClass="anyes" />           
                </td>
            </tr>
        </table>
        <br />            
    </div>                
            </td>
        </tr>
        <tr>
            <td>
                <div id="divMemberBank" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" class="tablemb" >
            <tr style="display:none;">
                <td align="right"><%=GetTran("001026", "国家名称")%>：</td>                        
                <td><ucl:uclCountry ID="ddlCountry" runat="server" /></td>
            </tr>
            
            <tr>
                <td align="right"><%=GetTran("000000", "会员编号")%>：</td>
                <td><asp:TextBox ID="txtBankName" MaxLength="20" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        
        <table>
            <tr>
                <td colspan="2">                    
                    <asp:Button ID="btnOK" runat="server" Text="确 定" onclick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;                    
                    <asp:Button ID="btnReset" Text="清 空" runat="server" onclick="btnReset_Click" CssClass="anyes" OnClientClick="javascript:return confirm('确实要清空吗？');" />
                </td>
            </tr>
        </table>                  
    </div>
            </td>
        </tr>           
    </table>
    <div id="cssrain" style="width:100% ; display:none;">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
        <tr>
          <td width="150px"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
            <tr>
                <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" onmouseover="cutManagement()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cutDescription()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
            </tr>
          </table></td>
          <td><a href="#"><img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down3()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block" id="tbody0">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                    <asp:Button ID="btnExcel" Text="Excel" runat="server" onclick="btnExcel_Click" style="display:none"/>   
                  <a href="#"><img src="../images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcel','');"/></a>&nbsp;&nbsp;&nbsp;&nbsp;            
                  </td>
                </tr>
            </table> </td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><%=GetTran("005385", "会员使用银行管理")%><br /> 
                  <%=GetTran("005386", "1.对会员使用银行进行增删改查")%>      
                    </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
    </form>
</body>
</html>

