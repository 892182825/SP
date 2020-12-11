<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocType.aspx.cs" Inherits="Company_SetParams_DocType"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DocType</title>
    <script language="javascript" type="text/javascript" src="../../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../../javascript/ManagementVsExplanation.js"></script>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">
        function ale()
        {
            return confirm('<%=GetTran("001222","确实要清空吗？")%>');
        }
        function ale1()
        {
           return confirm('<%=GetTran("001718","确实要删除吗？")%>');
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
                 <div>             
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td colspan="2" style="white-space:nowrap">
                                <asp:GridView ID="gvDocTypeTable" runat="server" class="tablemb" AllowSorting="true" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="DocTypeID" OnRowDataBound="gvDocTypeTable_RowDataBound" OnSorting="gvDocTypeTable_Sorting" RowStyle-Wrap="false">
                                    <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                    <HeaderStyle Wrap="false" />
                                    <RowStyle HorizontalAlign="Center"  Wrap="false" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidName" runat="server" Value='<%# Eval("DocTypeName") %>' />
                                                <asp:LinkButton ID="lbtEdit" runat="server" OnCommand="lbtEdit_Command"><%#GetTran("000259", "修改")%></asp:LinkButton>
                                                <asp:LinkButton ID="lbtDelete" runat="server" OnClientClick="return ale1()"
                                                    OnCommand="lbtDelete_Command"><%#GetTran("000022", "删除")%></asp:LinkButton>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DocTypeID" SortExpression="DocTypeID" HeaderText="单据编号" ItemStyle-Wrap="false" Visible="false" />
                                        <asp:BoundField DataField="DocTypeName" SortExpression="DocTypeName" HeaderText="单据名称" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="是否红单" ItemStyle-Wrap="false" SortExpression="IsRubric">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblIsRubric" Text='<%#DataBinder.Eval(Container.DataItem,"IsRubric")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DocTypeDescr" SortExpression="DocTypeDescr" HeaderText="单据说明" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />                                                    
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>           
                        <tr>
                            <td style="white-space:nowrap">                              
                                <asp:Button ID="btnAdd" Text="添 加" runat="server" OnClick="btnAdd_Click" CssClass="anyes" />&nbsp;&nbsp;
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
                <div id="divDocTypeTable" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" class="tablemb">
                        <tr>
                            <td align="right"><asp:Label ID="lblDocTypeName" runat="server" Text="单据名称："></asp:Label></td>
                            <td><asp:TextBox ID="txtDocTypeName" runat="server" MaxLength="15"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right"><asp:Label ID="lblIsRubric" runat="server" Text="是否红单："></asp:Label></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlIsRubric">
                                    <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblDocTypeDescr" runat="server" Text="单据描述："></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocTypeDescr" runat="server" TextMode="MultiLine" ></asp:TextBox>
                            </td>
                        </tr>
                                
                        <tr>
                            <td colspan="2">                                
                                <asp:Button ID="btnOK" runat="server" Text="确 定" OnClick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;
                                <asp:Button ID="btnReset" Text="清 空" runat="server" OnClick="btnReset_Click" CssClass="anyes" OnClientClick="return ale()" />
                            </td>
                        </tr>
                    </table>
                </div>                
            </td>
        </tr>               
    </table>
     
    <div id="cssrain" style="width:100%">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
        <tr>
          <td width="150"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
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
                  <td><asp:Button ID="btnExcel" Text="Excel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                  <a href="#"><img src="../images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcel','');"/></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table> </td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none" id="tbody1">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td> 
                  <%=GetTran("004257", "单据管理")%><br />
                    1.<%=GetTran("004256", "设置各种单据")%>    
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
