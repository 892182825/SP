<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeductXF.aspx.cs" Inherits="Company_DeductXF" %>

<%@ Register Src="../UserControl/ExpectNum.ascx" TagName="ExpectNum" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>消费积分申请处理</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
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
        
    </script>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">
        function CheckText() {
            filterSql();
        }
        window.onload = function () {
            down2();
        }
        function doSelect(oCheckbox) {
            var GridView1 = document.getElementById('gvdeduct');
            for (i = 1; i < GridView1.rows.length; i++) {
                GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
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
<%--                            <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value='<%=GetTran("000340","查询") %>'></input>--%>
                            <asp:Button ID="BtnConfirm" runat="server" Text="查 询"
                                OnClick="BtnConfirm_Click" CssClass="anyes" Style=""></asp:Button>
                        </td>
                        <%--<td style="white-space: nowrap;">
                            &nbsp;&nbsp;&nbsp;<%=GetTran("001885","查询期数是") %>&nbsp;
                        </td>
                        <td>
                            <uc1:ExpectNum ID="DropDownQiShu" runat="server" />
                        </td>--%>
                        <td style="white-space: nowrap;">
                        &nbsp;&nbsp;   注册日期:
                        </td> <td >
                           <asp:TextBox ID="txtregTimeStart"
                                runat="server" Width="80px" CssClass="Wdate" onfocus="new WdatePicker( )"></asp:TextBox></td>
                        <td>
                            <%=GetTran("000068")%>：</td>
                        <td><asp:TextBox ID="txtregTimeEnd" runat="server" Width="80px"
                                onfocus="new WdatePicker( )" CssClass="Wdate"></asp:TextBox>
                       
                             
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;<%=GetTran("000000","会员编号") %>&nbsp;
                        </td>
                        <td style="width: 128px">
                            <asp:TextBox ID="TxtSearch" runat="server" Width="128px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;<%=GetTran("000000","   消费积分 : ") %>
                        </td>
                        <td style="width: 128px">
                            <asp:TextBox ID="Txtjf1" runat="server" Width="128px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;<%=GetTran("000000"," 至 ") %>
                        </td>
                        <td style="width: 128px">
                            <asp:TextBox ID="Txtjf2" runat="server" Width="128px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;<%=GetTran("000000"," 状态 ") %>
                        </td>
                        <td style="width: 128px">
                            
                        <asp:DropDownList runat="server" ID="strzt">
                            <asp:ListItem Value="-1">全部</asp:ListItem>
                            <asp:ListItem Value="0">已提交</asp:ListItem>
                            <asp:ListItem Value="1">处理中</asp:ListItem>
                            <asp:ListItem Value="2">完成</asp:ListItem>
                            <asp:ListItem Value="3">拒绝</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>

                    </tr>
                    <tr><td style="width:128px;">
                        <asp:LinkButton ID="lkSubmit" runat="server" Width="" CssClass="anyes" Text="批量审核" OnClick="lkSubmit_Click"></asp:LinkButton>

                        </td>
                        <td>修改状态:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="plsh">
                            <asp:ListItem Value="0">已提交</asp:ListItem>
                            <asp:ListItem Value="1">处理中</asp:ListItem>
                            <asp:ListItem Value="2">完成</asp:ListItem>
                            <asp:ListItem Value="3">拒绝</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>备注 :</td>
                        <td><asp:TextBox ID="beizhu" runat="server" Width="128px" MaxLength="50"></asp:TextBox></td>
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
                                >
                                <AlternatingRowStyle BackColor="#F1F4F8" />
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField >
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                               <asp:CheckBox ID="CheckBoxALL" runat="server" onclick="doSelect(this);" />
                            </HeaderTemplate>
                            <HeaderStyle Font-Size="9pt" Width="20px" />
                        </asp:TemplateField>
                                    <asp:BoundField DataField="ID"  />
                                    <asp:BoundField HeaderText="状态" DataField="zt" SortExpression="zt" />
                                    <asp:BoundField HeaderText="编号" DataField="Number" SortExpression="Number" />
                                    <asp:BoundField HeaderText="手机号" DataField="ipon" SortExpression="ipon" />
                                    <asp:BoundField HeaderText="申请时间" DataField="XFTime" SortExpression="XFTime" />
                                    <asp:BoundField HeaderText="兑换金额" DataField="XFON" SortExpression="XFON" />
                                    <asp:BoundField HeaderText="兑换总额" DataField="XFEN" SortExpression="XFEN" />
                                    <asp:BoundField HeaderText="备注" DataField="ps" SortExpression="ps" />
                                    
                                    
                                    
                                    
                                </Columns>
                                <EmptyDataTemplate>
                                    <table backcolor="#F8FBFD" width="100%">
                                        <tr>
                                            <th>
                                                <%=GetTran("000000", "状态")%>
                                            </th>
                                            <th>
                                                <%=GetTran("001195", "编号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "手机号")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "申请时间")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "兑换金额")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "兑换总额")%>
                                            </th>
                                            <th>
                                                <%=GetTran("000000", "备注")%>
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
                                        1、<%=GetTran("000000", "消费积分导出清除")%>。
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