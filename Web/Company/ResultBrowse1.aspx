<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultBrowse1.aspx.cs" Inherits="Company_ResultBrowse1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script>
        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            filterSql_II(btname);
        }
    </script>
    <script language="javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }
        function down2() {
            if (document.getElementById("divTab2").style.display == "none") {
                document.getElementById("divTab2").style.display = "";
                document.getElementById("imgX").src = "images/dis1.GIF";

            }
            else {
                document.getElementById("divTab2").style.display = "none";
                document.getElementById("imgX").src = "images/dis.GIF";
            }
        }
    </script>
</head>
<body onload="down2();">
    <form id="form1" runat="server">
    <div>
        <br />
        <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="word-break: keep-all;
            word-wrap: normal">
            <tr>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">A币账户明细</asp:ListItem>
                        <asp:ListItem Value="1">B币账户明细</asp:ListItem>
                          <asp:ListItem Value="2">C币账户明细</asp:ListItem>
                        <asp:ListItem Value="3">D币账户明细</asp:ListItem>
                        <asp:ListItem Value="4">E币账户明细</asp:ListItem>
                        
                        
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="word-break: keep-all;
            word-wrap: normal">
            <tr>
                <td>
                    <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClick="BtnConfirm_Click"
                        CssClass="anyes" Style="cursor: hand;"></asp:Button>
                </td>
                <td>
                    &nbsp;<%=GetTran("006581", "发生时间")%>：
                    <asp:TextBox ID="Datepicker1" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                        Width="85px"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;
                    <%=GetTran("000068", "至")%>&nbsp;
                </td>
                <td>
                    &nbsp;
                    <asp:TextBox ID="Datepicker2" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                        Width="85px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;手机号/邮箱：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td><asp:Label ID="zsf" Runat="server" ></asp:Label></td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1">
            <tr>
                <td style="word-break: keep-all; word-wrap: normal">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound"
                        Width="100%" CssClass="tablemb">
                        <AlternatingRowStyle BackColor="#F1F4F8" />
                        <HeaderStyle CssClass="tablebt" />
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="mobiletele" HeaderText="会员账号" />
                            <asp:TemplateField HeaderText="科目">
                                <ItemTemplate>
                                    <%#BLL.Logistics.D_AccountBLL.GetKmtype(Eval("kmtype").ToString()) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发生时间">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("happentime") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("happentime") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="转入">
                                <ItemTemplate>
                                    <%#Eval("Direction").ToString() == "0" ?double.Parse(Eval("happenmoney").ToString()).ToString("0.00") : ""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="转出">
                                <ItemTemplate>
                                    <%#Eval("Direction").ToString()=="1"? Math.Abs(double.Parse(Eval("happenmoney").ToString())).ToString("0.00"):"" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Balancemoney" HeaderText="结余" DataFormatString="{0:f2}"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sftype" HeaderText="类型" Visible="false" />
                            <asp:TemplateField HeaderText="摘要">
                                <ItemTemplate>
                                    <%#getMark(Eval("remark").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table class="tablemb" width="100%">
                                <tr>
                                    <th>
                                        <%=GetTran("001195", "编号")%>
                                    </th>
                                    <th>
                                        <%=GetTran("006581", "发生时间")%>
                                    </th>
                                    <th>
                                        <%= GetTran("000000", "转入FTC")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000000", "转出FTC")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000000", "结余FTC")%>
                                    </th>
                                    <th>
                                        <%=GetTran("000000", "类型")%>
                                    </th>
                                    <th>
                                        <%=GetTran("006615", "科目")%>
                                    </th>
                                    <th>
                                        <%=GetTran("006616", "摘要")%>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
            <tr>
                <td>
                    <uc1:Pager ID="Pager1" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td class="zihz12">
                </td>
            </tr>
            <tr>
                <td style="height: 22px" class="zihz12">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="cssrain" style="width: 100%">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
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
                                onclick="down2()" /></a>
                    </td>
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
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/anextable.gif" OnClick="Download_Click" />
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
                                            <%=GetTran("000000", "1、查询服务机构的报单账户明细。")%>
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
