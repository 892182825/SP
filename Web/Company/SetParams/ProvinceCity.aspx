<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProvinceCity.aspx.cs" Inherits="Company_SetParams_ProvinceCity" EnableEventValidation="false" %>

<%@ Register TagPrefix="ucl" TagName="uclCountry" Src="~/UserControl/Country.ascx" %>
<%@ Register TagPrefix="ucl" TagName="uclPager" Src="~/UserControl/Pager.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script language="javascript" type="text/javascript" src="../../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../../javascript/ManagementVsExplanation.js"></script>
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
    <link href="../CSS/Company.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        input[type="text"]{ border:solid 1px #838282;}
    </style>
</head>
<body>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <br />
        <div>      
        <asp:Label ID="lblCityManage" runat="server" Text=""></asp:Label>                     
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style=" font-size:13px; color:#003d5c;">
                    <asp:Button ID="Button1" runat="server" Text="查 询" onclick="Button1_Click" CssClass="anyes" />&nbsp;&nbsp;
                    国家：<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>&nbsp;&nbsp;
                    省份：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>&nbsp;&nbsp;
                    城市：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>&nbsp;&nbsp;
                    区县：<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:GridView ID="gvCity" runat="server" AllowSorting="true" 
                         AutoGenerateColumns="false" DataKeyNames="ID" CssClass="tablemb"                        
                         onrowdatabound="gvCity_RowDataBound" Width="100%" 
                         onsorting="gvCity_Sorting">
                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                        <HeaderStyle  Wrap="false" />
                        <RowStyle HorizontalAlign="Center"  Wrap="false" />
                        <Columns>                        
                            <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false">
                                <ItemTemplate>                              
                                    <asp:LinkButton ID="lbtEdit" runat="server"
                                        oncommand="lbtEdit_Command"><%#GetTran("000259", "修改")%></asp:LinkButton>
                                     <asp:LinkButton  ID="lbtDelete" runat="server" OnClientClick="return ale1()"
                                        oncommand="lbtDelete_Command"><%#GetTran("000022", "删除")%></asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="ID" SortExpression="ID" HeaderText="编号" ItemStyle-Wrap="false" Visible="false" />
                            <asp:BoundField DataField="Country" SortExpression="Country" HeaderText="国家" ItemStyle-Wrap="false" />               
                            <asp:BoundField DataField="Province" SortExpression="Province" HeaderText="省份" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="City" SortExpression="City" HeaderText="城市" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Xian" SortExpression="Xian" HeaderText="区县" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="PostCode" SortExpression="PostCode" HeaderText="邮编" ItemStyle-Wrap="false" />               
                            <asp:BoundField DataField="CPCCode" SortExpression="CPCCode" HeaderText="地区编码" ItemStyle-Wrap="false" /> 
                        </Columns>                        
                    </asp:GridView>
                 </td>
            </tr>
        </table>
        <ucl:uclPager ID="uclPager" runat="server" />               
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
    </div>

    <div id="divCity" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" class="tablemb" >
                    <tr>
                        <td align="right"><asp:Label ID="lblCountryName" runat="server" Text="国家名称："></asp:Label></td>
                        <td><ucl:uclCountry ID="ddlCountry" runat="server" /></td>
                    </tr>
                    <tr>
                       <td align="right"><asp:Label ID="Label1" runat="server" Text="地区编码："></asp:Label></td>
                       <td><asp:TextBox ID="txt_cpccode" runat="server" MaxLength="6"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator2" runat="server" ValidationExpression="[A-Za-z0-9]{0,6}"
                                ControlToValidate="txt_cpccode" ErrorMessage="6位数字"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblProvice" runat="server" Text="省份："></asp:Label></td>
                        <td><asp:TextBox ID="txtProvice" runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblCity" runat="server" Text="城市："></asp:Label></td>
                        <td><asp:TextBox ID="txtCity" runat="server" MaxLength="20" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="Label2" runat="server" Text="区县："></asp:Label></td>
                        <td><asp:TextBox ID="TextBox1" runat="server" MaxLength="20" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><asp:Label ID="lblPostCode" runat="server" Text="邮编："></asp:Label></td>                       
                        <td><input type="text" id="txtPostCode" runat="server" value="" maxlength="6" onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))" /></td>
                    </tr>
                </table>
                <br />
                <table>            
                    <tr>
                        <td>
                            <asp:Button ID="btnOK" runat="server" Text="确 定" onclick="btnOK_Click" CssClass="anyes" />&nbsp;&nbsp;                    
                            <asp:Button ID="btnReset" Text="清 空" runat="server" onclick="btnReset_Click" CssClass="anyes" OnClientClick="return ale()" />
                        </td>
                    </tr>
                </table>        
        </div>

    <br /><br />
            
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
                  <td>  
                   <%=GetTran("003179", "省份城市管理")%><br />
                        1.<%=GetTran("003178", "对省份城市（包括邮编）进行添加，修改，删除等操作")%>
                    </td>
                </tr>
            </table></td>
          </tr>
        </tbody>
      </table>
	  </div>
    </div>           
    </table>    
    </form>
</body>
</html>
