<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayMemberInfo.aspx.cs" Inherits="Company_DisplayMemberInfo" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员信息查询</title>
    <script type="text/javascript" language="javascript" src="../JS/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" /> 
    <link href="CSS/stylen.css" rel="stylesheet" />
      <link href="bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="../JS/SqlCheck.js" type="text/javascript"></script>
    <script src="js/companyview.js" type="text/javascript"></script>
</head>
<body onload="down2()">
    <form id="form1" method="post" runat="server">
 
    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
        <tr>
            <td align="left">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="left" style="white-space: nowrap;">
                            <asp:Button ID="btnsearch" runat="server" Text="查 询" OnClick="BtnConfirm_Click" CssClass="btn btn-primary"></asp:Button>
                            <asp:DropDownList ID="DropDownExpectNum" runat="server" Visible="false">
                            </asp:DropDownList>
                            &nbsp; 手机号：<asp:TextBox ID="Number" runat="server" Width="80px"
                                MaxLength="10"></asp:TextBox>
                            &nbsp;<%=GetTran("000025", "会员姓名")%>：<asp:TextBox ID="Name" runat="server" Width="80px" 
                                MaxLength="50"></asp:TextBox>
                            &nbsp;<%=GetTran("0000", "推荐账号")%>：<asp:TextBox ID="Recommended" runat="server"
                                Width="80px" MaxLength="10"></asp:TextBox>
                                 &nbsp;<%=GetTran("000192", "推荐姓名")%>：<asp:TextBox ID="DName" runat="server"
                                Width="80px" MaxLength="10"></asp:TextBox>
                              <br/><br/>
                            &nbsp;<%=GetTran("000031", "注册时间")%>：<asp:TextBox ID="txtBox_OrderDateTimeStart"
                                runat="server" Width="80px" CssClass="Wdate" onfocus="new WdatePicker()"></asp:TextBox>
                            <%=GetTran("000068")%>：<asp:TextBox ID="txtBox_OrderDateTimeEnd" runat="server" Width="80px"
                                onfocus="new WdatePicker()" CssClass="Wdate"></asp:TextBox>
                         
                            &nbsp; &nbsp;<%=GetTran("008025", "会员状态")%>：<asp:DropDownList ID="ddl_zxState" runat="server">
                                <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                                
                                <asp:ListItem Value="2" Text="已注销"></asp:ListItem>
                                <asp:ListItem Value="3" Text="已冻结"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="1000px" class="tablemb">
                    <tr>
                        <td valign="top" align="left">
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="true" AutoGenerateColumns="False"
                                BorderStyle="Solid" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <HeaderStyle CssClass="tablebt" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="详细信息"  Visible="false">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <img src="images/fdj.gif" />
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Bind("number") %>'
                                                CommandName="detl" Text='<%#GetTran("000035", "详细信息") %>'> </asp:LinkButton>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("MemberState") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="mobiletele" HeaderText="会员手机号"></asp:BoundField>
                                      
                                  
                                    <asp:TemplateField HeaderText="会员姓名">
                                        <ItemTemplate>
                                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                   
                                    <asp:TemplateField HeaderText="推荐编号">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDirect" runat="server" Text='<%# Bind("dtele") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="注册金额" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblzcPrive" runat="server" Text='<%# Bind("zcPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="激活时间" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladvtime" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="会员状态">
                                        <ItemTemplate>
                                            <asp:Label ID="MemberState" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table class="biaozzi" width="100%">
                                        <tr>
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
                                                <%=GetTran("000000", "注册金额")%>
                                            </th>
                                            <th>
                                                <%=GetTran("007301", "激活时间")%>
                                            </th>
                                            <th>
                                                <%=GetTran("008025", "会员状态")%>
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
    </table>
    <br />
    <br />
    <br />
    <div id="cssrain" style="width: 100%">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" border="0" height="28" cellpadding="0" cellspacing="0" id="secTable">
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
            <table width="100%" border="0" height="68" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="Button1" runat="server" Text="导出EXECL" OnClick="Button1_Click"
                                            Style="display: none"></asp:LinkButton>
                                        <a href="#">
                                            <img src="images/anextable.gif" width="49" alt="" height="47" border="0" onclick="__doPostBack('Button1','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <%=GetTran("000286", "1、根据条件查询会员的基本信息。")%>
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