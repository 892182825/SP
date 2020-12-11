<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryMemberInfo.aspx.cs" Inherits="Company_QueryMemberInfo" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员信息编辑</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../JS/SqlCheck.js" type="text/javascript"></script>
    <script src="js/companyview.js" type="text/javascript"></script>
    <style type="text/css">
        #secTable
        {
            width: 150px;
        }
    </style>
</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <br />
    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="left" style="white-space: nowrap">
                            <asp:Button ID="btnsearch" CssClass="anyes" runat="server" Text= "查 询"  OnClick="btnsearch_Click" />
                            &nbsp;<%=GetTran("000029", "注册期数")%>：<asp:DropDownList ID="DropDownExpectNum" runat="server">
                            </asp:DropDownList>
                            &nbsp;<%=GetTran("000024", "会员编号")%>：<asp:TextBox ID="Number" runat="server" Width="80px"
                                MaxLength="10"></asp:TextBox>
                            &nbsp;<%=GetTran("000025", "会员姓名")%>：<asp:TextBox ID="Name" runat="server" Width="80px"
                                MaxLength="50"></asp:TextBox>
                            &nbsp;<%=GetTran("000026", "推荐编号")%>：<asp:TextBox ID="Recommended" runat="server"
                                Width="80px" MaxLength="10"></asp:TextBox>
                                &nbsp;<%=GetTran("000192", "推荐姓名")%>：<asp:TextBox ID="DName" runat="server"
                                Width="80px" MaxLength="10"></asp:TextBox>
                            &nbsp;<%=GetTran("000027", "安置编号")%>：<asp:TextBox ID="Placement" runat="server" Width="80px"
                                MaxLength="10"></asp:TextBox>
                                &nbsp;<%=GetTran("000097", "安置姓名")%>：<asp:TextBox ID="PName" runat="server" Width="80px"
                                MaxLength="10"></asp:TextBox>
                                 &nbsp;<%=GetTran("007301", "激活时间")%>：<asp:TextBox ID="txtBox_OrderDateTimeStart"
                                runat="server" Width="80px" CssClass="Wdate" onfocus="new WdatePicker()"></asp:TextBox>
                            <%=GetTran("000068")%> <asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server" Width="80px"
                                onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tablemb">
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                BorderStyle="Solid" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" CommandArgument='<%#Eval ("Number") %>' CommandName="edit"
                                                Text='<%#GetTran("000036", "编辑")%>' runat="server"> </asp:LinkButton>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("MemberState") %>' /> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Number" HeaderText="会员编号"></asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="会员昵称">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPetname" runat="server" Text='<%# Bind("PetName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="会员姓名">
                                        <ItemTemplate>
                                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="安置编号">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlacement" runat="server" Text='<%# Bind("Placement") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="安置姓名">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlacementName" runat="server" Text='<%# Bind("PlacementName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="推荐编号">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDirect" runat="server" Text='<%# Bind("Direct") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="推荐姓名">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDirectName" runat="server" Text='<%# Bind("DirectName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ExpectNum" HeaderText="注册期数"></asp:BoundField>
                                    <asp:TemplateField HeaderText="注册时间">
                                        <ItemTemplate>
                                            <asp:Label ID="lblregisterdate" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="激活时间">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladvtime" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000015", "操作")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000024", "会员编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000025", "会员姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000027", "安置编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000097", "安置姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000026", "推荐编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000192", "推荐姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000029", "注册期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000031", "注册时间")%>
                                            </th>
                                            <th>
                                                <%=GetTran("007301", "激活时间")%>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
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
                                        <!--<a href="#"><img src="images/anprtable.gif" width="49" height="47" border="0" /></a>-->
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
    </div>
    </form>
</body>
</html>