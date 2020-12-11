<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryOnlinepay.aspx.cs" Inherits="Company_QueryOnline" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script src="../JS/SqlCheck.js" type="text/javascript"></script>

    <script src="js/companyview.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

</head>
<body onload=" down2();">
    <form id="form1" runat="server">
    <div style="margin: auto;">
      <center>
            <div style="width: 1000px; padding-top: 25px;">
             <asp:HiddenField ID="hidtype" runat="server" Value="0" />
                <table class="tablemb" width="100%">
                    <tr>
                        <td style="width: 100px;">
                            <asp:Button ID="btnsearch" runat="server" Text="查询" CssClass="anyes" OnClick="btnsearch_Click" />
                        </td>
                        <td style="text-align: left;">
                       <%--     <asp:RadioButtonList ID="rdolistrmtype" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Text="经销商在线支付汇款" Value="0"></asp:ListItem>
                                <asp:ListItem Text="专卖店在线支付汇款" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>--%>
                             <%=GetTran("000777","汇款人姓名")%> ：<asp:TextBox ID="txtnumber" runat="server"></asp:TextBox>&nbsp;
                             <%=GetTran("007798", "汇款时间")%>：<asp:TextBox ID="txtbstd" runat="server" onfocus="WdatePicker()" ></asp:TextBox> 至<asp:TextBox ID="txtendd" runat="server" onfocus="WdatePicker()"  ></asp:TextBox>&nbsp;
                             <%=GetTran("005854","汇款单号")%>：<asp:TextBox ID="txtRemittancesid" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:GridView CssClass="tablema" Width="100%" ID="gvcdRemttlist" runat="server" AutoGenerateColumns="False"
                    OnRowCommand="gvcdRemttlist_RowCommand" OnRowDataBound="gvcdRemttlist_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="确认收款">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbtngetmoney" CommandArgument='<%#Eval("remittancesid") %>'
                                    CommandName="GT" runat="server" ><%#GetTran("005605", "收款")%></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="汇款金额" ItemStyle-HorizontalAlign="Right" DataField="remitmoney"
                            DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="汇款单编号" DataField="remittancesid" />
                        <asp:TemplateField HeaderText="汇款人姓名">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text= ' <%# Eval("name")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="汇款单时间">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("remittancesdate") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblrmdate" runat="server" Text='<%# Bind("remittancesdate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="汇款人编号" DataField="remitnumber" />
                    
                        <asp:TemplateField HeaderText="汇款用途" Visible=false>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbluseto" runat="server" Text='<%#Eval("use") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr>
                                <th>
                                    <%=GetTran("007799", "确认收款")%>
                                </th>
                                <th>
                                    <%=GetTran("001970", "收款金额")%>
                                </th>
                                <th>
                                    <%=GetTran("000777", "汇款人姓名")%>
                                </th>
                                <th>
                                    <%=GetTran("007806", "汇款单时间")%>
                                </th>
                                <th>
                                    <%=GetTran("001892", "汇款人编号")%>
                                </th>
                                <th>
                                    <%=GetTran("001044", "汇款用途")%>
                                </th>  
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>
                <uc1:Pager ID="Pager1" runat="server" />
            </div>
            <br />
            <table width="950px" class="tablema">
                <tr>
                    <td>
                        <asp:HiddenField ID="hidremid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 200px;">
                        <%=GetTran("007805", "汇款单编号")%>：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblremid" runat="server" Text="未选择"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("007806", "汇款单时间")%>：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblremittancesdate" runat="server" Text="未选择"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("001892", "汇款人编号")%>：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblnumber" runat="server" Text="未选择"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("000777","汇款人姓名")%>：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblname" runat="server" Text="未选择"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("007807", "汇款总金额")%>：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblmoney" runat="server" Text="未选择"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%=GetTran("007798", "汇款时间")%>：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtgettime" runat="server" onfocus="WdatePicker()" Width="80px"></asp:TextBox>&nbsp;日&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlhours" runat="server">
                        </asp:DropDownList>
                        &nbsp;<%=GetTran("007002","时")%>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlmins" runat="server">
                        </asp:DropDownList>
                        &nbsp;<%=GetTran("007503","分")%>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlsecs" runat="server">
                        </asp:DropDownList>
                        &nbsp;<%=GetTran("007629","秒")%>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnsureget" runat="server" CssClass="anyes" Text="确定收款" OnClick="btnsureget_Click" />
                    </td>
                </tr>
            </table>
      </center>
    </div>
    <br /><br /><br /><br /><br /><br />
    <table width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)" style="white-space: nowrap;">
                                <span id="span1" title=""  >
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)" style="white-space: nowrap;">
                                <span id="span2" title="" >
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
        </center>
        <div id="divTab2">
            <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnDownExcel" runat="server" Text="导出Excel" OnClick="btnDownExcel_Click"
                                            Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                                width="49" height="47" border="0" onclick="__doPostBack('btnDownExcel','');" /></a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <!--<a href="#">
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
                                        <%=GetTran("000034", "1、根据条件查询到会员，点击“编辑”可修改会员基本信息，除了当期的会员之外，其它会员安置、推荐不能改。")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            
        </div>
    </form>
</body>
</html>
