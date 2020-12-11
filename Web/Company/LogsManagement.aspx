<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogsManagement.aspx.cs" Inherits="Company_LogsManagement"
    EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register TagPrefix="ucl" TagName="uclExpectNum" Src="~/UserControl/ExpectNum.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>日志管理</title>
    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/QCDS2010.js"></script>
    <script language="javascript" type="text/javascript" src="../javascript/ManagementVsExplanation.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/SqlCheck.js"></script>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
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
            down2();               
        };     
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server" onsubmit="filterSql_III()">
    <br />
        <table width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" width="800" align="center">
                            <tr>
                                <td align="right">
                                    <%=GetTran("003160", "操作的时间")%>：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" onfocus="WdatePicker()" CssClass="Wdate"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <%=GetTran("000045", "期数")%>：
                                </td>
                                <td>
                                    <ucl:uclExpectNum ID="ddlExpectNum" runat="server" Visible="false" />
                                    <asp:DropDownList ID="DropDownListqishu" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <%=GetTran("003163", "操作类型")%>：
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlChangeType" runat="server">
                                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="0">删除</asp:ListItem>
                                        <asp:ListItem Value="1">修改</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <%=GetTran("003181", "操作对象类别")%>：
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlToType" runat="server">
                                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="Company">公司(管理员)</asp:ListItem>
                                        <%--<asp:ListItem Value="Store">店铺</asp:ListItem>--%>
                                        <asp:ListItem Value="Member">会员</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <%=GetTran("003182", "操作类别")%>：
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlChangeCategory" runat="server">
                                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="0">会员</asp:ListItem>
                                       <%-- <asp:ListItem Value="1">店铺</asp:ListItem>--%>
                                        <asp:ListItem Value="2">公司</asp:ListItem>
                                        <asp:ListItem Value="3">报单</asp:ListItem>
                                        <asp:ListItem Value="4">权限(角色)</asp:ListItem>
                                        <asp:ListItem Value="5">密码</asp:ListItem>
                                        <asp:ListItem Value="6">产品</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <%=GetTran("003183", "操作者类别")%>：
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlOperatorType" runat="server">
                                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="Company">公司(管理员)</asp:ListItem>
                                        <%--<asp:ListItem Value="Store">店铺</asp:ListItem>--%>
                                        <asp:ListItem Value="Member">会员</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <%=GetTran("003185", "操作者帐号")%>：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLoginID" runat="server" MaxLength="20"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <%=GetTran("003187", "操作对象帐号")%>：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToID" runat="server" MaxLength="20"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <%=GetTran("003189", "操作者IP地址")%>：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtOperatorIP" runat="server" MaxLength="20"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <br />
                                    <asp:Button ID="btnQuery" runat="server" Text="查 询" OnClick="btnQuery_Click" 
                                        CssClass="anyes" Height="22px" />&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="清 除" OnClick="btnReset_Click" CssClass="anyes" />&nbsp;&nbsp;
                                    <asp:Button ID="btnBack" runat="server" Text="备份日志" OnClick="btnBack_Click" CssClass="another" />&nbsp;&nbsp;
                                    <asp:Button ID="btnDel" runat="server" Text="删除日志" ToolTip="进入系统日志删除页面" OnClick="btnDel_Click"
                                        CssClass="another" />&nbsp;&nbsp;
                                        <input type="button" value="登录日志查询" class="another" onclick="window.location.href='LoginLogs.aspx'" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvDetailsByDay" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                        OnSorting="gvDetailsByDay_Sorting" Width="100%" OnRowDataBound="gvDetailsByDay_RowDataBound"
                                        CssClass="tablemb">
                                        <AlternatingRowStyle BackColor="#F1F4F8" Wrap="false" />
                                        <HeaderStyle Wrap="false" />
                                        <RowStyle HorizontalAlign="Center" Wrap="false" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="序号" 
                                                ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Type" SortExpression="type" HeaderText="操作类型" 
                                                ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="时间" SortExpression="time">
                                            <ItemTemplate>
                                            <asp:Label ID="lab" runat="server" Text='<%#GetbyDateTime(Eval("Time").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="SIP" SortExpression="sip" HeaderText="IP地址" 
                                                ItemStyle-Wrap="false" >
                                            
<ItemStyle Wrap="False"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField HeaderText="操作部位" SortExpression="Category" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#GetCategory(Eval("Category").ToString().Trim())%>
                                                </ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="From" SortExpression="[from]" HeaderText="操作者" 
                                                ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="操作者类型" SortExpression="FromUserType" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <%#GetType(DataBinder.Eval(Container.DataItem,"FromUserType").ToString().Trim())%>
                                                </ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="To" SortExpression="[to]" HeaderText="操作的对象" 
                                                ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="操作对象类型" SortExpression="ToUserType">
                                                <ItemTemplate>
                                                    <%#GetToUserType(DataBinder.Eval(Container.DataItem, "ToUserType").ToString().Trim())%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ExpectNum" SortExpression="ExpectNum" HeaderText="期数"
                                                ItemStyle-Wrap="false" >
<ItemStyle Wrap="False"></ItemStyle>
                                            </asp:BoundField>
                                                <asp:TemplateField HeaderText="详细信息" SortExpression="ToUserType">
                                                <ItemTemplate>
                                                   <img src="images/fdj.gif" /> <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("ID","~/Company/ViewChangeLog.aspx?ID={0}") %>'><%#GetTran("000399", "查看详细")%></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <th>
                                                        <%=GetTran("000012", "序号")%>
                                                    </th>
                                                    <th>
                                                        <%=GetTran("003163", "操作类型")%>
                                                    </th>
                                                    <th>
                                                        <%=GetTran("001546", "时间")%>
                                                    </th>
                                                    <th>
                                                       <%=GetTran("000072", "地址")%> 
                                                    </th>
                                                    <th>
                                                       <%=GetTran("003182", "操作类别")%>  
                                                    </th>
                                                    <th>
                                                        <%=GetTran("003197", "操作者")%>  
                                                    </th>
                                                    <th>
                                                        <%=GetTran("003199", "操作者类型")%>  
                                                    </th>
                                                    <th>
                                                        <%=GetTran("003201", "操作对象")%>  
                                                    </th>
                                                    <th>
                                                        <%=GetTran("003203", "操作对象类型")%>  
                                                    </th>
                                                    <th>
                                                        <%=GetTran("000045", "期数")%>  
                                                    </th>
                                                    <th>
                                                        <%=GetTran("000035", "详细信息")%>  
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <uc2:Pager ID="Pager1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="HiddenDG" runat="server" Visible="False">
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                        <br />
                        <div id="cssrain" style="width:100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                                <tr>
                                    <td width="150">
                                        <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                            <tr>
                                                <td class="sec2" onclick="secBoard(0)">
                                                    <span id="span1" title="" onmouseover="cutManagement()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                                </td>
                                                <td class="sec1" onclick="secBoard(1)">
                                                    <span id="span2" title="" onmouseover="cutDescription()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td><a href="#"><img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX" onclick="down3()" /></a></td>
                                </tr>
                            </table>
                            <div id="divTab2">
                                <table width="100%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                                    <tbody style="display: block" id="tbody0">
                                        <tr>
                                            <td valign="bottom" style="padding-left: 20px">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnExcel" Text="Excel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                                                            <a href="#">
                                                                <img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('btnExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;                                                            
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
                                                            <%=GetTran("003116", "日志管理")%>  <br />
                                                            <%=GetTran("003206", "1.查看系统的操作日志，主要修改和删除操作的日志")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
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