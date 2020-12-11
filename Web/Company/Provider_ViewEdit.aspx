<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Provider_ViewEdit.aspx.cs" ValidateRequest="false" EnableEventValidation="false"
    Inherits="Company_Provider_ViewEdit" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../javascript/ManagementVsExplanation.js" type="text/javascript"></script>
    <script type="text/javascript">	
	window.onload=function()
	{
	    down2();
	};

    function cut()
    {
         document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
    }
    function cut1()
    {
         document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
    }
    function conf()
    {
        return confirm('<%=GetTran("001718","确实要删除吗") %>');
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>    
        <table class="biaozzi">
            <tr>
            <td>
                    <input type="button" onclick="javascript: window.location.href='AddProvider.aspx';"
                        value='<%=GetTran("000973","添加供应商")%>'  class="another"/>
                </td>
                 <td nowrap="nowrap">
                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="查 看" OnClick="btnSearch_Click" class="anyes" />
                </td>
                <td nowrap="nowrap">                    
                    <%=GetTran("000927", "供应商编号")%>：<asp:TextBox ID="txtNumber" runat="server" MaxLength="10"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    <%=GetTran("000931", "供应商名称")%>：<asp:TextBox ID="txtName" runat="server" MaxLength="15"></asp:TextBox>
                </td>
               
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td>
                    <asp:GridView ID="givProviderInfo" runat="server" AutoGenerateColumns="False" Width="100%"
                    CssClass="tablemb" OnRowCommand="givProviderInfo_RowCommand" onrowdatabound="givProviderInfo_RowDataBound"
                        AlternatingRowStyle-Wrap="false"
                        HeaderStyle-Wrap="false"
                        FooterStyle-Wrap="false">
                        <Columns>
                            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ShowHeader="False" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                        CommandArgument='<%#Eval("ID") %>' ><%#GetTran("000259", "修改")%></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Del" OnClientClick="javascript:return conf();"
                                        CommandArgument='<%#Eval("ID") %>' ><%#GetTran("000022", "删除")%></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:BoundField HeaderText="供应商编号" DataField="Number"  ItemStyle-Wrap="false" />
                            <asp:BoundField HeaderText="供应商名称" DataField="Name"  ItemStyle-Wrap="false" />
                            <asp:BoundField HeaderText="供应商简称" DataField="ForShort" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField HeaderText="联系人" DataField="LinkMan" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField HeaderText="手机" DataField="Mobile" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField HeaderText="地址" DataField="Address" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField HeaderText="税号" DataField="DutyNumber" NullDisplayText="无" ItemStyle-Wrap="false" />
                            <asp:BoundField HeaderText="备注" DataField="Remark" NullDisplayText="无" ItemStyle-Wrap="false" />
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="100%">
                                <tr>
                                    <th><%=GetTran("000015", "操作")%></th>
                                    <th><%=GetTran("000927", "供应商编号")%></th>
                                    <th><%=GetTran("000931", "供应商名称")%></th>
                                    <th><%=GetTran("000958", "供应商简称")%></th>
                                    <th><%=GetTran("000960", "联系人")%></th>
                                    <th><%=GetTran("000052", "手机")%></th>
                                    <th><%=GetTran("000072", "地址")%></th>
                                    <th><%=GetTran("000962", "税号")%></th>
                                    <th><%=GetTran("000078", "备注")%></th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td><uc1:Pager ID="Pager1" runat="server" /></td>
            </tr>
        </table>
    </div>
<div id="cssrain" style="width: 100%">
                    <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                        <tr>
                            <td width="150">
                                <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                    <tr>
                                        <td class="sec2" onclick="secBoard(0)">
                                            <span id="span1" title="" onmouseover="cut()">
                                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                        </td>
                                        <td class="sec1" onclick="secBoard(1)">
                                            <span id="span2" title="" onmouseover="cut1()">
                                                <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <a href="#">
                                    <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                        onclick="down2()" style="vertical-align: middle" /></a>
                            </td>
                        </tr>
                    </table>
                    <div id="divTab2">
                        <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                            <tbody  style="display: block" id="tbody0">
                                <tr>
                                    <td valign="bottom" style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                        <asp:LinkButton ID="btnDownExcel" runat="server" Text="OutExcel" OnClick="btndropexcil_Click"
                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif" width="49"
                                                height="47px" border="0px" onclick="__doPostBack('btnDownExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;  
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                            <tbody style="display: none" id="tbody1">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    １、<%=GetTran("000989", "表格中可以选择删除和修改供应商的信息。")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
    </form>
    <%=msg %>
</body>
</html>
