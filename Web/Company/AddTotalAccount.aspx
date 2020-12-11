<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTotalAccount.aspx.cs"
    Inherits="Company_AddTotalAccount" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script src="../JS/QCDS2010.js" type="text/javascript"></script>

    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

    <script src="js/tianfeng.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function CheckText(btname) {
            //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
            var b = checkedcf('<%= GetTran("007151","确定提交吗") %>');
	    if (b == true) {
	        filterSql_II(btname);
	    } else {
	        return false;
	    }
	}
	function CheckText2(btname) {
	    //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
	    filterSql_II(btname);
	}
	function RadPayFashionChange() {
	    if (document.getElementById("RadPayFashion_1").checked) {
	        document.getElementById("place1").style.display = "block";
	    } else {
	        document.getElementById("place1").style.display = "none";
	    }
	}

    </script>

    <script language="javascript">
        function cut() {
            document.getElementById("span1").title = '<%=GetTran("000032", "管 理") %>';
        }
        function cut1() {
            document.getElementById("span2").title = '<%=GetTran("000033", "说 明") %>';
        }
    </script>

    <style type="text/css">
        .style1 {
            width: 122px;
        }
    </style>


</head>
<body onload="down2()">
    <form id="form1" runat="server">
        <br />
        <table width="99%" border="0" cellpadding="0" align="center" cellspacing="0" class="biaozzi">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" width="900" align="center">
                        <tr>
                            <td align="right" height="18" class="style1">
                                <asp:Literal ID="lit_number" runat="server"></asp:Literal>：
                            </td>
                            <td height="18">
                                <asp:TextBox ID="Number" runat="server" MaxLength="10" Width="133px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                    ID="Btn_Detail" runat="server" CssClass="anyes" Text="查看"
                                    OnClick="Btn_Detail_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="18" class="style1">
                                <%=GetTran("001400","昵称")%>：
                            </td>
                            <td height="18">
                                <asp:Literal ID="lit_petname" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="18" class="style1">
                                <%=GetTran("007745", "地址信息")%>：
                            </td>
                            <td height="18">
                                <asp:Literal ID="lit_add" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <%=GetTran("000322", "金额")%>：&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="Money" runat="server" Width="74px" Font-Bold="True" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <%=GetTran("001044", "汇款用途")%>：&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:RadioButtonList ID="member_type" RepeatLayout="Flow" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="消费账户"></asp:ListItem>
                                  <%--  <asp:ListItem Value="2" Text="复消账户"></asp:ListItem>--%>
                                </asp:RadioButtonList>
                          <%--       <asp:RadioButtonList ID="member_type2" RepeatLayout="Flow" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="2" Text="复消账户"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                                <asp:RadioButtonList ID="store_type" RepeatLayout="Flow" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="10" Text="服务机构订货款"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="服务机构周转款"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <%=GetTran("000591", "收款日期")%>：&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="FKBirthday" runat="server" CssClass="Wdate" Width="85px" onfocus="new WdatePicker()" /><asp:DropDownList
                                    ID="ddlHour" runat="server">
                                </asp:DropDownList>
                                <%=GetTran("007002","时") %><asp:DropDownList ID="ddlMinute" runat="server">
                                </asp:DropDownList>
                                <%=GetTran("007003","分") %>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" rowspan="2">
                                <%=GetTran("007747", "汇款方式")%>：&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadPayFashion" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                    RepeatLayout="Flow" OnSelectedIndexChanged="RadPayFashion_SelectedIndexChanged">
                                    <asp:ListItem Value="1">现金支付</asp:ListItem>
                                    <asp:ListItem Value="2">银行汇款</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="place1" Style="display: none; padding-bottom: 4px; padding-left: 4px; padding-right: 4px; padding-top: 4px"
                                    runat="server">
                                    <table border="0" cellspacing="0" bordercolor="#99caf2" bordercolordark="#ffffff"
                                        cellpadding="0" width="600">
                                        <tr>
                                            <td width="18%" align="right">
                                                <%=GetTran("006980", "汇款凭条或回单号")%>：&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="PayeeNum" runat="server" Width="190px"></asp:TextBox>
                                            </td>
                                            <td width="18%" align="right">
                                                <%=GetTran("000777", "汇款人姓名")%>：&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Remitter" runat="server" Width="104px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="18%" align="right">
                                                <%=GetTran("001051", "汇款人证件号")%>：&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="IdentityCard" runat="server" Width="190px"></asp:TextBox>
                                            </td>
                                            <td width="18%" align="right">
                                                <%=GetTran("001053", "汇出方银行")%>：&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="RemitBank" runat="server" Width="105px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="18%" align="right">
                                                <%=GetTran("001054", "汇出方帐号")%>：&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="RemitNum" runat="server" Width="190px"></asp:TextBox>
                                            </td>
                                            <td width="18%" align="right">
                                                <%=GetTran("001056", "汇入银行及帐号")%>：&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="BankName" runat="server" Width="177px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="18%" align="right">
                                                <%=GetTran("000570", "汇出日期")%>：&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3">
                                                <font face="宋体">
                                                    <asp:TextBox ID="HCBirthday" runat="server" CssClass="Wdate" Width="85px" onfocus="new WdatePicker()"></asp:TextBox></font>
                                                <asp:DropDownList ID="DropDownList1" runat="server">
                                                </asp:DropDownList>
                                                <%=GetTran("007002", "时")%><asp:DropDownList ID="DropDownList2" runat="server">
                                                </asp:DropDownList>
                                                <%=GetTran("007003","分") %>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <%=GetTran("000595", "确认方式")%>：&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:RadioButtonList ID="Way" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">传真</asp:ListItem>
                                    <asp:ListItem Value="0">核实</asp:ListItem>
                                    <asp:ListItem Value="2">透支</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" align="right" class="style1">
                                <%=GetTran("000078", "备 注")%>：&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="Remark" runat="server" Width="288px" Height="70px" TextMode="MultiLine"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" class="style1">&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;
                                        <asp:Button ID="sub" runat="server" Text="提 交" OnClick="sub_Click" CssClass="anyes"
                                            Style="cursor: hand;" OnClientClick="return abc();"></asp:Button>
                                <input class="anyes" id="bSubmit" style="display: none;" onclick="return CheckText('lkSubmit')" type="button" value='<%=GetTran("000321", "提交")%>'></input>
                                <input type="hidden" value="0" id="hid_fangzhi" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
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
                    <tbody id="tbody1">
                        <tr>
                            <td style="padding-left: 20px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <%=GetTran("001062", "1、公司给店铺透支一定数额的钱作为应急使用，或者店铺汇款后传真至公司，财务管理员直接输入汇款金额，并默认为已审。")%>
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
<script type="text/javascript">
    function abc() {
        if (confirm('<%=GetTran("007386","确定转入！") %>')) {
                var hid = document.getElementById("hid_fangzhi").value;
                if (hid == "0") {
                    document.getElementById("hid_fangzhi").value = "1";
                } else {
                    alert('<%=GetTran("007387","不可重复提交！") %>');
                    return false;
                }
            } else { return false; }
        }
</script>
