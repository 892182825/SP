<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductType.aspx.cs" Inherits="Company_SetParams_ProductType" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ProductType</title>
    <script language="javascript" type="text/javascript" src="../../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()"> 
    <br /> 
    <table width="100%">
        <tr>
            <td>
                <div >                   
        <table border="0" cellpadding="0" cellspacing="0" width="100%">            
            <tr>
                <td >
                     <asp:GridView ID="gvProductType" runat="server" AllowSorting="true" 
                         AutoGenerateColumns="false" class="tablemb" DataKeyNames="ProductTypeID"                         
                         onrowdatabound="gvProductType_RowDataBound" 
                         onsorting="gvProductType_Sorting" Width="100%"    >
                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />
                        <Columns>                        
                            <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                                <ItemTemplate>                             
                                    <asp:LinkButton ID="lbtEdit" runat="server" Text="修改" 
                                        oncommand="lbtEdit_Command"></asp:LinkButton>
                                     <asp:LinkButton  ID="lbtDelete" runat="server" Text="删除" OnClientClick="javascript:return confirm('确实要删除吗？');"
                                        oncommand="lbtDelete_Command"></asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="ProductTypeID" SortExpression="ProductTypeID" HeaderText="产品型号编号" Visible="false" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ProductTypeName" SortExpression="ProductTypeName" HeaderText="产品型号名称" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ProductTypeDescr" SortExpression="ProductTypeDescr" HeaderText="产品型号说明" ItemStyle-Wrap="false" />                      
                                                    
                        </Columns>                        
                    </asp:GridView>
                 </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" Text="添 加" runat="server" onclick="btnAdd_Click" CssClass="anyes"/>&nbsp;&nbsp;    
                    <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_Click" CssClass="anyes" />                           
                </td>
            </tr>
        </table>
            
    </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divProductType" runat="server">
                <br />
        <table border="0" cellpadding="0" cellspacing="0" class="tablemb">
            <tr>
                <td><asp:Label ID="lblProductTypeName" runat="server" Text="型号名称："></asp:Label></td>
                <td><asp:TextBox ID="txtProductTypeName" MaxLength="20" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblProductTypeDescr" runat="server" Text="型号描述："></asp:Label></td>
                <td><asp:TextBox ID="txtProductTypeDescr" runat="server" TextMode="MultiLine" Height="91px"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnOK" runat="server" Text="确 定" onclick="btnOK_Click" CssClass="anyes" /> &nbsp;&nbsp;                   
                    <asp:Button ID="btnReset" Text="清 空" runat="server" onclick="btnReset_Click" CssClass="anyes" OnClientClick="javascript:return confirm('确实要清空吗？');" />
                </td>
            </tr>
        </table>
        
    </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="cssrain">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/DMdp.gif">
        <tr>
          <td width="150"><table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
              <tr>
                <td class="sec2" onclick="secBoard(0)">管 理</td>
                <td class="sec1" onclick="secBoard(1)">说 明</td>
              </tr>
          </table></td>
          <td><a href="#"><img src="../images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down2()"/></a></td>
        </tr>
      </table>
	  <div id="divTab2">
      <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
        <tbody style="DISPLAY: block">
          <tr>
            <td valign="bottom" style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td><asp:Button ID="btnExcel" Text="Excel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                  <a href="#"><img src="../images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcel','');"/></a>&nbsp;&nbsp;&nbsp;&nbsp; 
                  <a href="#"><img src="../images/anprtable.gif" width="49" height="47" border="0" /></a> 
                    
                  </td>
                </tr>
            </table> </td>
          </tr>
        </tbody>
        <tbody style="DISPLAY: none">
          <tr>
            <td style="padding-left:20px"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td>产品型号管理<br /> 
                  1.对产品型号进行添加，修改，删除等操作      
                    </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>
            </td>
        </tr>
    </table>    
    </form>
</body>
</html>
