<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrantReward1.aspx.cs" Inherits="Company_GrantReward1"
    EnableEventValidation="false" %>
    <%@ Register src="../UserControl/Pager.ascx" tagname="Pager" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>
    <script src="js/tianfeng.js" type="text/javascript"></script>
	
    <script language="javascript">
        function cut()
        {
             document.getElementById("span1").title='<%=GetTran("000032", "管 理") %>';
        }
        function cut1()
        {
             document.getElementById("span2").title='<%=GetTran("000033", "说 明") %>';
        }
    </script>

    <style type="text/css">
        .style1
        {
            width: 122px;
        }
    </style>

    <style>
        .anliunu03
        {
            font-family: "宋体";
            font-size: 12px;
            color: #FFFFFF;
            text-decoration: none;
            background-image: url(images/anextable.gif);
            background-repeat: no-repeat;
            height: 47px;
            width: 49px;
            text-align: center;
            border: 0px;
        }
    </style>

</head>
<body onload="down2()">
    <form id="form1" runat="server">
    <br />
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top">
                    <div>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                            <tr>
                                <td>
                                    <asp:Button ID="BtnRelease" runat="server" Text="确认汇兑" OnClick="BtnRelease_Click"
                                        CssClass="another" Style="cursor: hand;"></asp:Button>&nbsp;&nbsp;
                                    <asp:Button ID="btnShow" runat="server" Text="显 示" OnClick="btnShow_Click" CssClass="anyes"
                                        Style="cursor: hand;"></asp:Button>&nbsp;<%=GetTran("001431", "电子帐户中奖金大于等于")%>
                                    &nbsp;
                                    <asp:TextBox ID="jine" runat="server" Width="56px">500</asp:TextBox><%=GetTran("001434", "并且有银行账号的会员的奖金")%>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tbColor">
                            <tr>
                                <td style="word-break: keep-all; word-wrap: normal">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CssClass="tablemb">
                                        <AlternatingRowStyle BackColor="#F1F4F8" />
                                        <HeaderStyle CssClass="tablebt" />
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="日期">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# "&nbsp;"+DataBinder.Eval(Container, "DataItem.riqi") %>'
                                                        ID="Label1" NAME="Label1">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.zongMoney") %>'
                                                        ID="Label2" NAME="Label2">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="总比数" DataField="zongBishu" />
                                            <asp:BoundField HeaderText="支付宝账号" DataField="paymentaccount" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1">
                            <tr>
                                <td style="word-break: keep-all; word-wrap: normal">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CssClass="tablemb" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true">
                                        <AlternatingRowStyle BackColor="#F1F4F8" />
                                        <HeaderStyle CssClass="tablebt" Wrap="false" />
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField HeaderText="商户流水号" DataField="id" />
                                            <asp:TemplateField HeaderText="收款银行户名">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# "&nbsp;"+DataBinder.Eval(Container, "DataItem.bankbook") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="收款银行账号">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# "&nbsp;"+DataBinder.Eval(Container, "DataItem.strbankcard") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="收款开户银行" DataField="bank" />
                                            <asp:BoundField HeaderText="收款银行所在省份" DataField="bankprovince" />
                                            <asp:BoundField HeaderText="收款银行所在城市" DataField="bankcity" />
                                            <asp:BoundField HeaderText="收款支行名称" />
                                            <asp:TemplateField HeaderText="金额">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# "&nbsp;"+Convert.ToDouble (DataBinder.Eval(Container, "DataItem.zongji")).ToString ("0.00") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="对公对私标志" DataField="biaozhi" />
                                            <asp:TemplateField HeaderText="备注">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="Textbox6" NAME="Textbox6"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label6" NAME="Label6">1&nbsp;</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="False" HeaderText="信息">
                                                <ItemTemplate>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.zongji") %>'
                                                        ID="lblMoney">
                                                    </asp:Label>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.mobileTele") %>'
                                                        ID="lblMobile">
                                                    </asp:Label>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Number") %>'
                                                        ID="lblBianhao">
                                                    </asp:Label>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>'
                                                        ID="lblName">
                                                    </asp:Label>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.bankcard") %>'
                                                        ID="lblBankAccount">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Jackpot") %>'
                                                        ID="Textbox7" NAME="Textbox7">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width=100% class="biaozzi">
                                        <tr>
                                            <td>
                                                <uc2:Pager ID="Pager1" runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table2">
                            <tr>
                                <td style="word-break: keep-all; word-wrap: normal">
                                    <asp:GridView ID="dgTongji" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CssClass="tablemb">
                                        <AlternatingRowStyle BackColor="#F1F4F8" />
                                        <HeaderStyle CssClass="tablebt" />
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="日期">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.riqi") %>'
                                                        ID="Textbox8" NAME="Textbox8">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.riqi") %>'
                                                        ID="Label7" NAME="Label7">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="总金额">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.zongMoney") %>'
                                                        ID="Textbox9" NAME="Textbox9">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.zongMoney") %>'
                                                        ID="Label8" NAME="Label8">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="总比数" DataField="zongBishu" />
                                            <asp:BoundField HeaderText="支付宝账号" DataField="paymentaccount" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table3">
                            <tr>
                                <td style="word-break: keep-all; word-wrap: normal">
                                    <asp:GridView ID="dgDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CssClass="tablemb" OnRowDataBound="dgDetails_RowDataBound">
                                        <AlternatingRowStyle BackColor="#F1F4F8" />
                                        <HeaderStyle CssClass="tablebt" />
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField HeaderText="商户流水号" />
                                            <asp:TemplateField HeaderText="收款银行户名">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.bankbook") %>'
                                                        ID="Textbox10" NAME="Textbox10">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# "&nbsp;"+DataBinder.Eval(Container, "DataItem.bankbook") %>'
                                                        ID="Label9" NAME="Label9">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="收款银行账号">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# "&nbsp;"+DataBinder.Eval(Container, "DataItem.strbankcard") %>'
                                                        ID="Textbox11" NAME="Textbox11">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# "&nbsp;"+DataBinder.Eval(Container, "DataItem.strbankcard") %>'
                                                        ID="Label10" NAME="Label10">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="收款开户银行" DataField="bank" />
                                            <asp:BoundField HeaderText="收款银行所在省份" DataField="bankprovince" />
                                            <asp:BoundField HeaderText="收款银行所在城市" DataField="bankcity" />
                                            <asp:BoundField HeaderText="收款支行名称" />
                                            <asp:TemplateField HeaderText="金额">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# Convert.ToDouble (DataBinder.Eval(Container, "DataItem.zongji")).ToString ("0.00") %>'
                                                        ID="Textbox12" NAME="Textbox12">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# "&nbsp;"+Convert.ToDouble (DataBinder.Eval(Container, "DataItem.zongji")).ToString ("0.00") %>'
                                                        ID="Label11" NAME="Label11">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="对公对私标志" DataField="biaozhi" />
                                            <asp:TemplateField HeaderText="备注">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="Textbox13" NAME="Textbox13"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label12" NAME="Label12">1</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="False" HeaderText="信息">
                                                <ItemTemplate>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.zongji") %>'
                                                        ID="Label13">
                                                    </asp:Label>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.mobileTele") %>'
                                                        ID="Label14">
                                                    </asp:Label>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Number") %>'
                                                        ID="Label15">
                                                    </asp:Label>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>'
                                                        ID="Label16">
                                                    </asp:Label>
                                                    <asp:Label Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.bankcard") %>'
                                                        ID="Label17">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.zongji") %>'
                                                        ID="Textbox14" NAME="Textbox14">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
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
                                        <asp:Button ID="Button2" runat="server" Text="" OnClick="Button1_Click" CssClass="anliunu03"
                                            Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <%=GetTran("006879", "1、将添加到会员的现金帐户中的奖金以现金或银行汇款的形式汇兑，汇兑后会员的现金帐户中的奖金相应减少。")%>
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
