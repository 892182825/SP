<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankRemit.aspx.cs" Inherits="Member_BankRemit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>银行汇款</title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <link href="CSS/Member.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript" type="text/javascript">

	    function CheckText(btname)
	    {
		    //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
		    filterSql_II (btname);
    		
	    }
        function currencychange()
        {
            document.getElementById("LabCurrency2").value=document.getElementById("LabCurrency1").value;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="800" border="0" align="center" cellpadding="0" cellspacing="0" class="tablemb">
        <tr>
            <td>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
                    <tr>
                        <th align="center" colspan="2">
                            <b>
                                <%=GetTran("005865", "汇款申报——充值")%></b>
                        </th>
                    </tr>
                    <tr>
                        <td align="right" width="129" height="34" bgcolor="#EBF1F1">
                            <%=GetTran("000024", "会员编号")%>：
                        </td>
                        <td height="34">
                            <asp:Literal ID="Number" runat="server"></asp:Literal>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000322", "金额")%>：&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="Money" runat="server" Width="74px" ForeColor="Black" Font-Bold="True"
                                MaxLength="20"></asp:TextBox>&nbsp;<asp:DropDownList ID="LabCurrency1" runat="server"
                                    onchange="currencychange()">
                                </asp:DropDownList>
                            <asp:TextBox ID="LabCurrency" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="LabCurrency2" runat="server" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="biZhong" style="display: none" runat="server">
                        <td align="right" width="129" bgcolor="#F8FBFD">
                            <%=GetTran("000562", "币种")%>：
                        </td>
                        <td>
                            <asp:RadioButtonList ID="Currency" runat="server" Width="264px" Height="13px" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td valign="top" align="right" width="129" bgcolor="#EBF1F1">
                            <%=GetTran("001044", "汇款用途")%>：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="DeclarationType" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" Style="margin-right: 7px">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="129" bgcolor="#EBF1F1">
                            <%=GetTran("000786", "付款日期")%>：
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="FKBirthday" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                                Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("001048", "汇单号码")%>：
                        </td>
                        <td>
                            <asp:TextBox ID="PayeeNum" runat="server" Width="190px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style3" bgcolor="#EBF1F1">
                            <%=GetTran("000777", "汇款人姓名")%>：
                        </td>
                        <td width="140px">
                            <asp:TextBox ID="Remitter" runat="server" Width="104px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("001051", "汇款人证件号")%>：
                        </td>
                        <td>
                            <asp:TextBox ID="IdentityCard" runat="server" Width="190px" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style3" bgcolor="#EBF1F1">
                            <%=GetTran("001053", "汇出方银行")%>：
                        </td>
                        <td>
                            <asp:TextBox ID="RemitBank" runat="server" Width="105px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="10" bgcolor="#EBF1F1">
                            <%=GetTran("001054", "汇出方帐号")%>：
                        </td>
                        <td height="10">
                            <asp:TextBox ID="RemitNum" runat="server" Width="190px" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style3" bgcolor="#EBF1F1">
                            <%=GetTran("000570", "汇出日期")%>：
                        </td>
                        <td>
                            <font face="宋体">
                                <asp:TextBox ID="HCBirthday" runat="server" CssClass="Wdate" onfocus="WdatePicker()"
                                    Width="85px"></asp:TextBox></font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="10" bgcolor="#EBF1F1">
                            <%=GetTran("001056", "汇入银行及帐号")%>：
                        </td>
                        <td height="10">
                            <asp:DropDownList ID="BankName" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right" width="129" bgcolor="#EBF1F1">
                            <%=GetTran("001305", "备 注")%>：
                        </td>
                        <td>
                            <asp:TextBox ID="Remark" runat="server" Width="319px" Height="86px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="800" border="0" align="center" cellpadding="0" cellspacing="1">
        <tr>
            <td align="center">
                <asp:Button ID="sub" runat="server" Text="提 交" OnClick="sub_Click" CssClass="anyes"></asp:Button>
                <!--如果输入有错,请输入一笔负金额进行冲销,并在备注中详细说明.-->
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
        <tr>
            <td class="zihz12">
                <%=this.GetTran("000649", "功能说明")%>：
            </td>
        </tr>
        <tr>
            <td style="height: 22px" class="zihz12">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <%=this.GetTran("006883", "1、会员在不愿采用网上支付的情况下，会员通过银行等途径向公司汇款后，在此填写汇款信息。")%>
            </td>
        </tr>
    </table>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
