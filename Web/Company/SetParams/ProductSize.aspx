<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductSize.aspx.cs" Inherits="Company_SetParams_ProductSize" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ProductSize</title>
    <script language="javascript" type="text/javascript" src="../../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JS/SqlCheck.js"></script>
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
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
            <td><div >                   
        <table border="0" cellpadding="0" cellspacing="0" width="100%">           
            <tr>
                <td>
                     <asp:GridView ID="gvProductSize" runat="server" AllowSorting="true" 
                         AutoGenerateColumns="false" DataKeyNames="ProductSizeID"                         
                         onrowdatabound="gvProductSize_RowDataBound" CssClass="tablemb"
                         onsorting="gvProductSize_Sorting" Width="100%" RowStyle-Wrap="false"  >
                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />
                        <Columns>                        
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="操作">
                                <ItemTemplate>                            
                                    <asp:LinkButton ID="lbtEdit" runat="server"
                                        oncommand="lbtEdit_Command"><%#GetTran("000259", "修改")%></asp:LinkButton>
                                     <asp:LinkButton  ID="lbtDelete" runat="server" OnClientClick="return ale1()"
                                        oncommand="lbtDelete_Command"><%#GetTran("000022", "删除")%></asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="ProductSizeID" SortExpression="productSizeID" HeaderText="产品尺寸编号" ItemStyle-Wrap="false" Visible="false" />
                            <asp:BoundField DataField="ProductSizeName" SortExpression="productSizeName" HeaderText="产品尺寸名称" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ProductSizeDescr" SortExpression="productSizeDescr" HeaderText="产品尺寸说明" ItemStyle-Wrap="false" />                                              
                            
                        </Columns>
                        
                    </asp:GridView>
                 </td>
            </tr>
        </table> 
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" Text="添 加" runat="server" onclick="btnAdd_Click" style="cursor:pointer" CssClass="anyes" />&nbsp;&nbsp;  
                    <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_Click" style="cursor:pointer" CssClass="anyes" />                                 
                </td>
            </tr>
        </table>
        <br />
           
    </div></td>
        </tr>
        <tr>
            <td><div id="divProductSize" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" class="tablemb">
            <tr>
                <td  align="right"><asp:Label ID="lblProductSizeName" runat="server" Text="产品尺寸名称："></asp:Label></td>
                <td style="white-space:nowrap"><asp:TextBox ID="txtProductSizeName" MaxLength="6" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtLength" runat="server" Width="60px" MaxLength="6"></asp:TextBox>(mm)*
                    <asp:RegularExpressionValidator ID="REVtxtLength" runat="server"
                    ControlToValidate="txtLength" ErrorMessage="只能输入正整数" SetFocusOnError="True"
                    ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtWidth" runat="server" Width="60px" MaxLength="6"></asp:TextBox>(mm)*
                    <asp:RegularExpressionValidator ID="REVtxtWidth" runat="server"
                    ControlToValidate="txtWidth" ErrorMessage="只能输入正整数" SetFocusOnError="True"
                    ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtHigh" runat="server" Width="60px" MaxLength="6"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="REVtxtHigh" runat="server"
                    ControlToValidate="txtHigh" ErrorMessage="只能输入正整数" SetFocusOnError="True"
                    ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>                       
                </td>
            </tr>
            <tr>
                <td  align="center"><asp:Label ID="lblProductSizeDescr" runat="server" Text="产品尺寸说明："></asp:Label></td>
                <td><asp:TextBox ID="txtProductSizeDescr" runat="server" TextMode="MultiLine" Height="91px" MaxLength="100"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnOK" runat="server" Text="确 定" onclick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;                    
                    <asp:Button ID="btnReset" Text="清 空" runat="server" onclick="btnReset_Click" OnClientClick="return ale()" CssClass="anyes" />
                </td>
            </tr>
        </table>
       
    </div></td>
        </tr>
        <tr>
            <td><div id="cssrain" style="width:100%">
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
                  <td><%=GetTran("005840", "产品尺寸管理")%><br /> 
                  <%=GetTran("005841", "对产品尺寸进行添加，修改，删除等操作")%>                  
                    </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div></td>
        </tr>
    </table>   
    </form>
</body>
</html>
