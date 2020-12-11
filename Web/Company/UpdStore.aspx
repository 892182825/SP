<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdStore.aspx.cs" Inherits="Company_UpdStore"
    EnableEventValidation="false" %>

<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountryCityPCode.ascx" TagName="CountryCityPCode"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UpdStore</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <script language="javascript">
    function CheckText()
	{
		//这个方法是只有1个lkSubmit按钮时候 可直接用

		filterSql();
	}

	//绑定邮编
		function GetCCode_s2(city)
		{
		    var sobj = document.getElementById("<%=PostolCode.ClientID %>");
		    sobj.value=AjaxClass.GetAddressCode(city).value
		}
    </script>

</head>
<body>
    <form id="form1" method="post" runat="server">
    <br />
    <table width="650" border="0" align="center" cellpadding="0" cellspacing="0" class="tablett">
        <tr>
            <td>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
                    <tr>
                        <th colspan="3">
                             <%=GetTran("000883", "店铺信息编辑")%>
                        </th>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000024", "会员编号")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="Number" runat="server"  Width="184px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000150", "店铺编号")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="StoreID" runat="server" Enabled="false"  Width="184px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000043", "推荐人编号")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="txtDirect" runat="server" Enabled="false"  Width="184px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000039", "店长姓名")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="Name" runat="server"  Width="184px" MaxLength="25" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000040", "店铺名称")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="StoreName" runat="server"  Width="184px" MaxLength="25" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000617", "所属语言")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="Language" runat="server"  Width="184px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000313", "店铺所在地")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:DropDownList ID="StoreCountry" runat="server" Enabled="false">
                            </asp:DropDownList>
                            &nbsp;<asp:DropDownList ID="StoreCity" runat="server" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000316", "店长联系信息")%>：
                        </td>
                        <td bgcolor="#F8FBFD" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <uc2:CountryCityPCode ID="CountryCity1" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Address" runat="server" TextMode="SingleLine" MaxLength="200"></asp:TextBox><font style="color:Red">*</font>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000073", "邮编")%>：
                        </td>
                        <td bgcolor="#F8FBFD" >
                            <asp:TextBox ID="PostolCode" runat="server" Width="88px" MaxLength="6"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator2" runat="server" ValidationExpression="[0-9---(-)]{0,20}"
                                ControlToValidate="PostolCode" ErrorMessage="不能输入字母,请重输！"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000319", "负责人电话")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="HomeTele" runat="server"  Width="184px" MaxLength="20"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator1" runat="server" ValidationExpression="[0-9---(-)]{0,20}"
                                ControlToValidate="HomeTele" ErrorMessage="不能输入字母,请重输！"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000044", "办公电话")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="OfficeTele" runat="server"  Width="184px" MaxLength="20"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator3" runat="server" ValidationExpression="[0-9---(-)]{0,20}"
                                ControlToValidate="OfficeTele" ErrorMessage="不能输入字母,请重输！"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000069", "移动电话")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="MobileTele" runat="server"  Width="184px" MaxLength="20"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator4" runat="server" ValidationExpression="[0-9---(-)]{0,20}"
                                ControlToValidate="MobileTele" ErrorMessage="不能输入字母,请重输！"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000071", "传真电话")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="FaxTele" runat="server"  Width="184px" MaxLength="20"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator5" runat="server" ValidationExpression="[0-9---(-)]{0,20}"
                                ControlToValidate="FaxTele" ErrorMessage="不能输入字母,请重输！"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000087", "开户银行")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                        <asp:ScriptManager runat="server" ID="sc1"></asp:ScriptManager>
                        <asp:UpdatePanel ID="up1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:DropDownList ID="MemberBank1" runat="server">
                            </asp:DropDownList>
                            <%=GetTran("006046", "支行名称")%>：<asp:TextBox ID="txtEbank" runat="server" MaxLength="50"/>
                        </ContentTemplate>
                            
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000329", "银行账户")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="BankCard" runat="server" Width="184px" MaxLength="50"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator6" runat="server" ValidationExpression="[0-9---(-)]{0,20}"
                                ControlToValidate="BankCard" ErrorMessage="不能输入字母,请重输！"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                           <%=GetTran("000330", "电子邮箱")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="Email" runat="server" Width="184px" MaxLength="50"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="Email"
                                ErrorMessage="邮箱格式不正确" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000332", "网址")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="NetAddress" runat="server"  Width="184px" MaxLength="50"></asp:TextBox>（<%=GetTran("000924", "例")%>：http://www.baidu.com）
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="NetAddress"
                                ErrorMessage="网址填写不正确" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000078", "备注")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="Remark" runat="server" Width="295px" TextMode="MultiLine" Height="58px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000043", "推荐人编号")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="Label1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000042", "办店期数")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="ExpectNum" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000031", "注册时间")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="RegisterDate" runat="server" CssClass="Wdate" onfocus="WdatePicker()" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000046", "级别")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:RadioButtonList ID="StoreLevelInt" runat="server" Enabled="false" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                <%-- <asp:ListItem Value="1">县级</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">市级</asp:ListItem>
                                <asp:ListItem Value="3">省级</asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000856", "店铺可用报单底线")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="StorageScalar" runat="server" Width="80px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                   
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000341", "经营面积（平方米）")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="FareArea" runat="server" Width="112px" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000343", "投资总额（万元）")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="TotalInvestMoney" runat="server" Width="112px" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000041", "总金额（元）")%>：
                        </td>
                        <td bgcolor="#F8FBFD">
                            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td style="height: 27px" align="right" bgcolor="#EBF1F1">
                            <%=GetTran("000041", "总消费（元）")%>：
                        </td>
                        <td style="height: 27px" bgcolor="#F8FBFD">
                            <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:LinkButton ID="lkSubmit" Style="display: none" runat="server" Text="提交" OnClick="lkSubmit_Click"></asp:LinkButton>
                <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value="<%=GetTran("000092", "修 改")%>" />
                <asp:Button runat="server" ID="Button1" Text="修 改" CssClass="anyes" OnClick="Button1_Click1"
                    Visible="false" />
                &nbsp;&nbsp;
                <asp:Button runat="server" ID="Button2" Text="返 回" CssClass="anyes" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>
    <br />
    <%= msg%>
    </form>
</body>
</html>
