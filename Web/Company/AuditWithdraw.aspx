<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditWithdraw.aspx.cs" Inherits="Company_AuditWithdraw" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/PagerTwo.ascx" TagName="Pager1" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%= GetTran("006993","提现审核") %></title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="js/jquery-1.4.3.min.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(
            function() {
                $('#checkAll').click(
                    function() {
                        $(".chb input").each(
                            function() {
                                $(this).attr("checked", !this.checked);
                            }
                        )
                    }
                );
            }
        );
    </script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="js/tianfeng.js" type="text/javascript"></script>

    <script language="javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }
    </script>

    <script>
        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            filterSql_II(btname);
        }
        window.onload = function() {
            down2();
        }
    </script>
</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <br />
    <div width="100%">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top">
                    <div>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi" style="word-break: keep-all;
                            word-wrap: normal">
                            <tr>
                                <td align="left" style="white-space: nowrap">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BtnConfirm" runat="server" Text="查 询" OnClick="BtnConfirm_Click"
                                                    CssClass="anyes" Style="cursor: hand;"></asp:Button>
                                                
                                                &nbsp;
                                            </td>
                                            <td>
                                                <%=GetTran("000058", "请选择国家")%>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropCurrency" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList1" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <%=GetTran("000024", "会员编号")%>：<asp:TextBox ID="txtNumber" runat="server" Width="120"></asp:TextBox>
                                                <%=GetTran("006984", "提现金额")%><asp:DropDownList ID="ddlType" runat="server">
                                                    <asp:ListItem Value="0">大于</asp:ListItem>
                                                    <asp:ListItem Value="1">大于等于</asp:ListItem>
                                                    <asp:ListItem Value="2">等于</asp:ListItem>
                                                    <asp:ListItem Value="3">小于</asp:ListItem>
                                                    <asp:ListItem Value="4">小于等于</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtMoney" runat="server" Width="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                （<%=GetTran("000733", "不填为任意")%>）
                                            </td>
                                            <td>
                                                <span>
                                                    <%=GetTran("000719", "并且")%></span><%=GetTran("006986", "提现时间")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Datepicker1" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                                                    Width="85px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                                <%=GetTran("000068", "至")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Datepicker2" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                                                    Width="85px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <%=GetTran("000734", "的记录")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server"></asp:Label><span> </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br /> 
                        <table>
                            <tr>
                                <td><asp:Button ID="btn_listsubmit" runat="server" 
                                CssClass="anyes" Text="批量处理" onclick="btn_listsubmit_Click" />&nbsp;&nbsp;
                        </td>
                        <td><span style="font-size:12px"> 
                        <asp:RadioButtonList ID="rad_list" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="1" Text="已汇出" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="账号出错"></asp:ListItem>
                            <asp:ListItem Value="3" Text="开始处理"></asp:ListItem>
                            </asp:RadioButtonList>
                            </span>
                        </td>
                            </tr>
                        </table>               
                        
                        <div runat="server" id="div1">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tbColor">
                                <tr>
                                    <td style="word-break: keep-all; word-wrap: normal">
                                        <asp:GridView ID="GridView1" runat="server" ShowFooter="true" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                            OnRowDataBound="GridView1_RowDataBound" Width="100%" CssClass="tablemb">
                                            <AlternatingRowStyle BackColor="#F1F4F8" />
                                            <HeaderStyle CssClass="tablebt" Wrap="false" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <input type="checkbox" id="checkAll" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox  ID="chb" CssClass="chb" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审核" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip='<%#Eval("Number")+","+Eval("isauditing")+","+Eval("Id")+","+Eval("withdrawMoney")+","+Eval("withdrawsxf")+","+Eval("wyj")+","+Eval("IsJL") %>' CommandName="Lbtn"  CommandArgument='<%#Eval("Number")+","+Eval("isauditing")+","+Eval("Id")+","+Eval("withdrawMoney")+","+Eval("withdrawsxf")+","+Eval("wyj")+","+Eval("IsJL") %>'><%#GetTran("007169", "已汇出")%></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                       <asp:LinkButton ID="LinkButton2" runat="server"  CommandArgument='<%#Eval("Number")+","+Eval("isauditing")+","+Eval("Id")+","+Eval("withdrawMoney")+","+Eval("withdrawsxf")+","+Eval("wyj")+","+Eval("IsJL") %>' CommandName="carderror"><%=GetTran("007171", "账号错误")%></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%#Eval("Number")+","+Eval("isauditing")+","+Eval("Id")+","+Eval("withdrawMoney")+","+Eval("withdrawsxf")+","+Eval("wyj")+","+Eval("IsJL") %>' CommandName="kscl"><%=GetTran("007170", "开始处理")%></asp:LinkButton>
                                                        <input id="HidisAuditingr" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.isAuditing")%>'
                                                            name="Hidden3" runat="server" />
                                                        <input id="HidId" type="hidden" value='<%# DataBinder.Eval(Container,"DataItem.Id")%>'
                                                            name="Hidden4" runat="server" />
                                                        <input id="HidNumber" type="hidden" value='<%# DataBinder.Eval(Container,"DataItem.number")%>'
                                                            name="Hidden4" runat="server" />
                                                        <input id="HidMoney" type="hidden" value='<%# DataBinder.Eval(Container,"DataItem.withdrawMoney")%>'
                                                            name="Hidden4" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Number" HeaderText="会员编号" />
                                                <asp:TemplateField HeaderText="会员姓名">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# GetNumberName(DataBinder.Eval(Container.DataItem, "name").ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="withdrawMoney" HeaderText="总金额" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="withdrawMoneys" HeaderText="提现金额" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField HeaderText="审核状态">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# GetAuditState(DataBinder.Eval(Container.DataItem, "isauditing").ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="提现币种">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# Eval("IsJL").ToString()=="1"?"TUSDT":"USDTERC20"%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="手机号">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem,"bankcard").ToString() %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="提现时间">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# GetWithdrawTime(DataBinder.Eval(Container.DataItem, "WithdrawTime").ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审核时间">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# GetAuditTime(DataBinder.Eval(Container.DataItem,"AuditingTime").ToString()) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="提现位置">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# Convert.ToInt32(Eval("wyj"))==1?"钱包":"商城"%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                

                                                <asp:TemplateField HeaderText="查看备注">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%#SetVisible(DataBinder.Eval(Container.DataItem, "Remark").ToString(), DataBinder.Eval(Container.DataItem, "id").ToString())%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkBtnDelete" runat="server" CommandName="Del" CommandArgument='<%#Eval("Number")+","+Eval("isauditing")+","+Eval("Id")+","+Eval("withdrawMoney")+","+Eval("withdrawsxf")+","+Eval("wyj") %>'><%#GetTran("000022", "删除")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <table width="100%" class="tablemb">
                                                    <tr>
                                                        <th><%=GetTran("000761", "审核")%></th>
                                                        <th><%=GetTran("000024", "会员编号") %></th>
                                                        <th><%=GetTran("000025", "会员姓名")%></th>
                                                        <th><%=GetTran("000000", "总金额")%></th>
                                                        <th><%=GetTran("006984", "提现金额")%></th>
                                                        <th><%=GetTran("006983", "审核状态")%></th>
                                                        <th><%=GetTran("000000", "提现币种")%></th>
                                                        <th><%=GetTran("000000", "手机号")%></th>
                                                        <th><%=GetTran("006986", "提现时间")%></th>
                                                        <th><%=GetTran("001155", "审核时间")%></th>
                                                        <th><%=GetTran("000000", "提现位置")%></th>
                                                        <th><%=GetTran("000744", "查看备注")%></th>
                                                        <th><%=GetTran("000024", "000022")%></th>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                <tr>
                                    <td colspan="3">
                                        <uc1:Pager ID="Pager1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <br />
                </td>
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
                            onclick="down2()" /></a>
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
                                        <asp:Button ID="Button1" Text="导出EXECL" runat="server" OnClick="Button1_Click" Style="display: none;">
                                        </asp:Button><a href="#"><img src="images/anextable.gif" width="49" height="47" border="0"
                                            onclick="__doPostBack('Button1','');" style="cursor: hand;" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <%=GetTran("007821", "审核会员提现申请")%>
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
