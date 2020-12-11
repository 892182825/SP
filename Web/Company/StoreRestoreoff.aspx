<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreRestoreoff.aspx.cs" Inherits="Company_StoreRestoreoff" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <title>Company_twQuery</title>
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
     
     <script language="javascript" type="text/javascript" src="js/MemberRestoreoff.js"></script>
 <script language="javascript" type="text/javascript">
    

	 var aa=[
        '<%=GetTran("000032", "管 理") %>',
        '<%=GetTran("000033", "说 明") %>',
        '<%=GetTran("000481", "查询条件里面不能输入特殊字符")%>！',
        '<%=GetTran("000000", "您确定要恢复该服务机构的注销状态吗")%>'
    ];

    </script>
   
</head>
<body  onload="down2()">
    <form id="form1" runat="server">
    <div>
    <br />
        <table width="100%" align="center">
            <tr>
                <td nowrap="nowrap">
                    <table class="biaozzi">
                        <tr>
                            
                            <td>
                                &nbsp;&nbsp;&nbsp;
                                 
                                 <asp:Button ID="btnSeach" runat="server" Text="查 询" OnClick="btnSeach_Click"  CssClass="anyes" />
                            </td>
                            <td >
                               <%=GetTran("000037", "服务机构编号")%>：
&nbsp;</td>
                            <td >
                                <asp:TextBox ID="txtStoreid" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                           
                           
                                <td>
                                <%=GetTran("000613", "日期")%>：<asp:TextBox ID="DatePicker2" runat="server" CssClass="Wdate" 
                                    onfocus="WdatePicker()"></asp:TextBox>
                                
                            </td>
                            <td nowrap="nowrap"><%=GetTran("000045", "期数")%>：
                                <uc2:ExpectNum ID="ExpectNum1" runat="server" />
                            </td>
                             
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!--Gridview显示部分-->
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center" width="100%">
                                <asp:GridView ID="givTWmember" Width="100%" runat="server" AutoGenerateColumns="False"
                                    CssClass="tablemb" 
                                    onrowdatabound="givTWmember_RowDataBound" 
                                    onrowcommand="givTWmember_RowCommand">
                                    <Columns>
                                       <asp:TemplateField HeaderText="恢复注销">
                            
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtnOk" runat="server" Text="" CommandName="OK" 
                                    CommandArgument='<%#Eval("ID")+":"+Eval("storeid")+":"+Eval("iszx") %>'
                                    OnClientClick="javascript:return Cfm()"><%#GetTran("000064", "确认")%></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        <asp:BoundField DataField="Storeid" HeaderText="服务机构编号" />
                                        
                                     <asp:TemplateField  HeaderText="店长姓名">
                                            
                                            <ItemTemplate>
                                                <%#GetbyName(Eval("Number").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="zxqishu" HeaderText="注销期数" />
                                        
                                        <asp:TemplateField HeaderText="注销日期">
                                            
                                            <ItemTemplate>
                                                <%#GetbyDateTime(Eval("zxdate").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="zx" HeaderText="注销状态" />
                                    </Columns>
                                     <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                        <th>
                                                <%=GetTran("001322", "恢复注销")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000037", "服务机构编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "店长姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001289", "注销期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001292", "注销日期")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001277", "注销状态")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblMassage" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="cssrain"  style="width:100%">
                        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                            <tr>
                                <td width="150">
                                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                        <tr>
                                            <td class="sec2" onclick="secBoard(0)">
                    <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                </td>
                <td class="sec1" onclick="secBoard(1)">
                    <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <a href="#">
                                        <img src="images/dis1.GIF" name="imgX" width="18" height="22" border="0" id="imgX"
                                            onclick="down2()" style="vertical-align:middle" /></a>
                                </td>
                            </tr>
                        </table>
                        <div id="divTab2">
                            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                                <tbody style="display: block" id="tbody0">
                                    <tr>
                                        <td valign="bottom" style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="btnDownExcel"  runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif" width="49"
                                                                height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <!-- <a href="#">
                                                            <img src="images/anprtable.gif" width="49" height="47" border="0" /></a>-->
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
                                                        １、<%=GetTran("000000", "恢复已经注销的服务机构。")%>
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
          
    </div>
    </form>
   
</body>
</html>