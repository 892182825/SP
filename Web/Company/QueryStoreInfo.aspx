<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryStoreInfo.aspx.cs" Inherits="Company_QueryStoreInfo" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td nowrap="nowrap">
                <div>
                    <br />
                    <table class="biaozzi">
                        <tr>
                            <td nowrap="nowrap">
                                <asp:Button ID="BtnSeach" runat="server" Text="查 询" OnClick="BtnSeach_Click" CssClass="anyes" />
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000150", "店铺编号")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtstoreid" Style="width: 80px;" runat="server" MaxLength="10" />
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000039", "店长姓名")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtname" Style="width: 80px;" runat="server" MaxLength="100" />
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000040", "店铺名称")%>：
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtstorename" Style="width: 80px;" runat="server" MaxLength="100" />
                            </td>
                            <td nowrap="nowrap">
                                 <%=GetTran("000046", "级别")%>： 
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList runat="server" ID="dplLevel">
                            </asp:DropDownList>&nbsp;
                            </td>
                            <td nowrap="nowrap">
                                <%=GetTran("000045", "期数")%>：
                                <uc2:ExpectNum ID="ExpectNum1" runat="server" />
                            </td>
                            <td>
                                &nbsp;<%=GetTran("000031", "注册时间")%>：<asp:TextBox ID="txtBox_OrderDateTimeStart"
                                runat="server" Width="80px" CssClass="Wdate" onfocus="new WdatePicker()"></asp:TextBox>
                            <%=GetTran("000068")%>：<asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server" Width="80px"
                                onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
                
            </td>
        </tr>
        <tr>
            <td width="100%" nowrap="nowrap">
                <div>
                    <asp:GridView ID="givSearchStoreInfo" runat="server" AutoGenerateColumns="False"
                        OnRowCommand="givSearchStoreInfo_RowCommand" CssClass="tablemb" Width="100%"
                        OnRowDataBound="givSearchStoreInfo_RowDataBound" AlternatingRowStyle-Wrap="False"
                        FooterStyle-Wrap="False" HeaderStyle-Wrap="False" PagerStyle-Wrap="False" RowStyle-Wrap="False"
                        SelectedRowStyle-Wrap="False">
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                         <asp:TemplateField HeaderText="详细信息">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                          <img src="images/fdj.gif" /> <asp:LinkButton ID="Button1" runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                        CommandName="DetailsStoreInfo" Text='详细信息' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:BoundField DataField="StoreID" HeaderText="店编号" HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="Number" HeaderText="会员编号" HeaderStyle-Wrap="false" />
                            <asp:BoundField HeaderText="店长姓名" DataField="Name" HeaderStyle-Wrap="false" />
                            <asp:BoundField HeaderText="店铺名称" DataField="StoreName" HeaderStyle-Wrap="false" />
                            <asp:BoundField HeaderText="办店期数" DataField="ExpectNum" HeaderStyle-Wrap="false" />
                            <asp:BoundField HeaderText="推荐人编号" DataField="Direct" HeaderStyle-Wrap="false" />
                            <asp:TemplateField HeaderText="注册时间">
                                        <ItemTemplate>
                                                <%# GetRDate(DataBinder.Eval(Container.DataItem,"RegisterDate")) %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RegisterDate") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="店铺所属国家" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="country" Text='<%# DataBinder.Eval(Container.DataItem, "SCPCCode")%>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="级别" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%--<asp:Label runat="server" ID="StoreLevelInt" Text='<%# DataBinder.Eval(Container.DataItem, "StoreLevelInt")%>'> </asp:Label>--%>
                                    <asp:Label runat="server" ID="StoreLevelInt" Text='<%# getStoLevelStr(DataBinder.Eval(Container.DataItem, "storeid").ToString()) %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="100%" style="white-space: nowrap">
                                <tr style="white-space: nowrap">
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000037", "店编号")%>
                                    </th>
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000024", "会员编号")%>
                                    </th>
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000039", "店长姓名")%>
                                    </th>
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000040", "店铺名称")%>
                                    </th>
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000042", "办店期数")%>
                                    </th>
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000043", "推荐人编号")%>
                                    </th>
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000454", "店铺所属国家")%>
                                    </th>
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000057", "注册日期")%>
                                    </th>
                                    <th style="white-space: nowrap">
                                        <%#GetTran("000046", "级别")%>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <table height="20px" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>                <div id="cssrain" style="width:100%">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                                    <tr>
                                        <td width="150">
                                            <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
                                                <tr>
                                                    
                                                    <td class="sec2" onclick="secBoard(0)"  style="white-space:nowrap;">
                                            <span id="span1" title="" onmouseover="cut()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                                        </td>
                                        <td class="sec1" onclick="secBoard(1)"  style="white-space:nowrap;">
                                            <span id="span2" title="" onmouseover="cut1()"><%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000033", "说 明"))%></span>
                                        </td>
                                                    
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <a href="#">
                                                <img src="images/dis.GIF" name="imgX" width="18" height="22" border="0" 
                                                id="imgX" onclick="down2()" style="vertical-align:middle"/></a></td>
                                    </tr>
                                </table>
                                <div id="divTab2" style="display:none;">
                                    <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="mainTable">
                                        <tbody style="display: block" id="tbody0">
                                            <tr>
                                                <td valign="bottom" style="padding-left: 20px">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="导出Excel" OnClick="btndownExcel_Click"
                                                        Style="display: none;"></asp:LinkButton><a href="#"><img src="images/anextable.gif"
                                                            width="49" height="47" border="0" onclick="__doPostBack('LinkButton1','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                    1、
                                                    <%=GetTran("000494", "根据条件查询店铺的基本信息。")%>
                                                 
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
</body>
<script src="js/companyview.js" type="text/javascript"></script>
</html>