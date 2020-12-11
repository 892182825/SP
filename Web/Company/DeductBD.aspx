<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeductBD.aspx.cs" Inherits="Company_DeductBD" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>扣补款管理</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="js/tianfeng.js" type="text/javascript"></script>
    <script language="javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }
    </script>
    <script type="text/javascript" language="javascript">
        function vali() {
            var tddl = document.getElementById("DropDownList2").value;
            var tvalue = document.getElementById("TxtSearch").value;
            if (tddl == "MemberInfo.Number" || tddl == "MemberInfo.Name") {
                if (tvalue.length <= 0) {
                    alert('<%=GetTran("007749","请输入详细信息") %>' + '！');
                    return false;
                }
            }
        }
    </script>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
        function CheckText() {
            filterSql();
        }
        window.onload = function () {
            down2();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <br />
                <table width="" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lkSubmit" runat="server" Style="display: none" Text="提交" OnClick="lkSubmit_Click"></asp:LinkButton>
                            <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value='<%=GetTran("000340","查询") %>'></input>
                            <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClientClick="return vali()"
                                OnClick="BtnConfirm_Click" CssClass="anyes" Style="display: none"></asp:Button>
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;&nbsp;&nbsp;<%=GetTran("001885","查询期数是") %>&nbsp;
                        </td>
                        <td>
                            <uc1:ExpectNum ID="DropDownQiShu" runat="server" />
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;<%=GetTran("001889","的") %>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="2">扣款</asp:ListItem>
                                <asp:ListItem Value="3">补款</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        &nbsp;&nbsp;<%=GetTran("001889","的")%>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_audit" runat="server">
                                <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                                <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                                <asp:ListItem Value="1" Text="审核"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;<%=GetTran("000719","并且") %>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="84px">
                                <asp:ListItem Value="条件不限">条件不限</asp:ListItem>
                                <asp:ListItem Value="MemberInfo.Number">编号</asp:ListItem>
                                <asp:ListItem Value="MemberInfo.Name">姓名</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;<%=GetTran("000851","包含") %>&nbsp;
                        </td>
                        <td style="width: 128px">
                            <asp:TextBox ID="TxtSearch" runat="server" Width="128px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;<%=GetTran("001895","的会员的扣补款信息") %>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;<table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="border: rgb(147,226,244) solid 1px">
                            <asp:GridView ID="gvdeduct" runat="server" AutoGenerateColumns="false" AllowSorting="false"
                                Width="100%" CssClass="tablemb bordercss" 
                                OnRowDataBound="gvdeduct_RowDataBound" onrowcommand="gvdeduct_RowCommand">
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>操作</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="ok" CommandArgument='<%# Eval("id") %>'><%#GetTran("000761", "审核")%></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="del" CommandArgument='<%# Eval("id") %>'><%#GetTran("000022","删除")%></asp:LinkButton>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("IsAudit") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="编号" DataField="Number" SortExpression="Number" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            姓名</HeaderTemplate>
                                        <ItemTemplate>
                                            <span>
                                                <%#getname(Eval("Name"))%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                        <HeaderTemplate>
                                            <span>钱包类型 </span>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span>
                                                <%#GetActtypestr( Eval("IsDeduct")) %></span> <span>
                                                    
                                                </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                        <HeaderTemplate>
                                            <span>扣补款 </span>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span>
                                                <%#Eval("typeE") %></span> <span>
                                                    <%#Eval("DeductMoney","{0:n2}")%>
                                                </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="期数" DataField="ExpectNum" SortExpression="ExpectNum" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <span>录入日期</span></HeaderTemplate>
                                        <ItemTemplate>
                                            <span>
                                                <%#Getdate(Eval("KeyinDate","{0:d}"))%></span></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="操作编号" DataField="OperateNum" />
                                    <asp:TemplateField HeaderText="审核状态">
                                        <ItemTemplate>
                                            <%#Eval("isaudit").ToString() == "0" ? GetTran("001009", "未审核") : GetTran("001011", "已审核")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <span>
                                                <%=GetTran("000078", "备注")%></span>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <img src="images/fdj.gif" /> <asp:Label ID="labnote" runat="server" Text='<%#GetNote(Eval("ExpectNum"), Eval("Number"),Eval("DeductReason"),Eval("ID"))%>' />
                                            <asp:HiddenField ID="hdfID" runat="server" Value='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table backcolor="#F8FBFD" width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("001195", "编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000107", "姓名")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "扣补款消费积分")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000045", "期数")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001838", "录入日期")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001835", "操作编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000078", "备注")%>
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
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <uc2:Pager ID="Pager1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="cssrain" style="width: 100%">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
            <tr>
                <td width="150">
                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                        <tr>
                            <td class="sec2" onclick="secBoard(0)" style="white-space: nowrap;">
                                <span id="span1" title="" onmouseover="cut()">
                                    <%=BLL.CommonClass.CommonDataBLL.cut(GetTran("000032", "管 理"))%></span>
                            </td>
                            <td class="sec1" onclick="secBoard(1)" style="white-space: nowrap;">
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
            <table width="99%" height="68" border="0" cellspacing="0" class="DMbk" id="mainTable">
                <tbody style="display: block" id="tbody0">
                    <tr>
                        <td valign="bottom" style="padding-left: 20px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Button ID="Button1" runat="server" Text="导出Excel" OnClick="Button1_Click" Style="display: none;" /><a
                                            href="#"><img src="images/anextable.gif" width="49" height="47" border="0" onclick="__doPostBack('Button1','');" /></a>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <td style="font-size: 12px; color: #333333;">
                                        1、<%=GetTran("001833", "查询对会员进行的扣补款记录")%>。
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