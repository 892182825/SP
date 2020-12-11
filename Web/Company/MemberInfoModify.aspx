<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberInfoModify.aspx.cs" Inherits="Company_MemberInfoModify"  EnableEventValidation="false"%>

<%@ Register Src="../UserControl/CountryCityPCode.ascx" TagName="CountryCityPCode" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript" src="../JS/jquery-1.2.6.js"></script>
    <script src="js/companyview.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function getwp() {
            var parm1 = '<%=PostolCode.ClientID %>';
            var parm2 = '<%=GetTran("000229", "对不起，您没有修改特殊信息的权限！")%>';
            var parm3 = '<%=GetTran("006819","邮编必须是半角数字组成的！") %>';
            var parm4 = '<%=GetTran("006820","邮编必须是6位！") %>';

            return { pocid: parm1, nmdf: parm2, postf: parm3, postl: parm4 }
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 257px;
        }
        .tablett tr td {
            white-space:nowrap
        }
        .midhong1 {
	    font-family: Arial;
	    font-size: 20px;
	    line-height: 22px;
	    font-weight: bold;
	    text-decoration: none;
	    color: #FF0000;
        }
    </style>

</head>
<body>
    <form method="post" id="form1" runat="server">
    <br />
    <table width="600" border="0" align="center" cellpadding="0" cellspacing="0" class="tablett">
        <tr>
            <td>
                <div runat="server" id="p1">
                    <table width="600" border="0" align="center" cellpadding="0" cellspacing="1">
                        <tr>
                            <th colspan="3">
                                <%=GetTran("000062", "会员资料修改")%>
                            </th>
                        </tr>
                        <tr>
                            <td width="274" align="right" bgcolor="EBF1F1">
                                <%=GetTran("000024", "会员编号")%>：
                            </td>
                            <td bgcolor="#F8FBFD" class="style1">
                                <asp:Label ID="Number" runat="server" Width="104px"></asp:Label>
                            </td>
                            <td rowspan="8" width="274" bgcolor="#F8FBFD" align="center"><asp:Image ID="img1" style="display:none;" runat="server" Width="100" Height="130"/></td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000027", "安置编号")%>：
                            </td>
                            <td bgcolor="#F8FBFD" class="style1">
                                <asp:Label ID="Placement" runat="server" Width="104px"></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000026", "推荐编号")%>：
                            </td>
                            <td bgcolor="#F8FBFD" class="style1" >
                                <asp:Label ID="Recommended" runat="server" Width="104px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <font color="red">*</font><%=GetTran("000063", "会员昵称")%>：
                            </td>
                            <td bgcolor="#F8FBFD" class="style1" >
                                <asp:TextBox ID="PetName" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000065", "家庭电话")%>：
                            </td>
                            <td bgcolor="#F8FBFD" class="style1" >
                            <asp:TextBox id="Txtjtdh" onblur="famTelOnblur()" onfocus="famTelOnfocus()"  runat="server" Width="65px" MaxLength="15">电话号码</asp:TextBox>
                                   <span id="spanFamilyTel" style="color:Red"></span>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000044", "办公电话")%>：
                            </td>
                            <td bgcolor="#F8FBFD" class="style1" >
                                <asp:TextBox id="Txtbgdh" onblur="offmTelOnblur()" onfocus="offmTelOnfocus()"  runat="server" Width="65px" MaxLength="15">电话号码</asp:TextBox>
                                <span id="spanOfficeTel" style="color:Red"></span>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000071", "传真电话")%>：
                            </td>
                            <td bgcolor="#F8FBFD" class="style1" >
                                <asp:textbox id="Txtczdh" onblur="faxTelOnblur()" onfocus="faxTelOnfocus()"  runat="server" Width="65px" MaxLength="15">电话号码</asp:textbox>
                                <span id="spanFaxTel" style="color:Red"></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000069", "移动电话")%>：
                            </td>
                            <td bgcolor="#F8FBFD" class="style1" >
                                <asp:TextBox ID="MoblieTele" runat="server" MaxLength="11"></asp:TextBox>
                                <asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator3" runat="server" ValidationExpression="[0-9]{0,15}"
                                    ControlToValidate="MoblieTele" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <font color="red">*</font><%=GetTran("000072", "地址")%>：
                            </td>
                            <td bgcolor="#F8FBFD" colspan="2">
                                <table class="biaozzi">
                                    <tr>
                                        <td>
                                            <uc1:CountryCityPCode ID="CountryCity1" runat="server" />
                                        </td>
                                        <td>
                                            <%--<asp:TextBox ID="Address" runat="server" Width="192px" MaxLength="50"></asp:TextBox>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>


                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                               
                            </td>
                            <td bgcolor="#F8FBFD" colspan="2">
                                
                                    
                                            <asp:TextBox ID="Address" runat="server" Width="192px" MaxLength="50"></asp:TextBox>
                                      
                            </td>
                        </tr>



                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <font color="red">*</font><%=GetTran("000073", "邮编")%>：
                            </td>
                            <td bgcolor="#F8FBFD" colspan="2">
                                <asp:TextBox ID="PostolCode" runat="server" MaxLength="6" onblur="VerifyPostCard()"></asp:TextBox>
                                <span id="spanTb" style="color:Red"></span>                             
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000078", "备注")%>：
                            </td>
                            <td bgcolor="#F8FBFD" colspan="2">
                                <asp:TextBox ID="Remark" runat="server" Width="272px" TextMode="MultiLine" Height="88px"></asp:TextBox><font color="Silver"><%=GetTran("006821", "不能多于500个字。")%></font>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000079", "订单号")%>：
                            </td>
                            <td bgcolor="#F8FBFD" colspan="2">
                                <asp:TextBox ID="OrderID" runat="server" MaxLength="10" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="32" colspan="3">
                                <span class='midhong1' onclick="Permissions()" style="cursor:hand;"><img runat="server" style="border:0px" id="i1" alt="" src="images/dis2.GIF" width="22" />
                                <%=GetTran("000080", "修改下面隐藏特殊信息")%></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="p2" style="display:none;">
                    <table width="100%" border="0" cellspacing="1" cellpadding="0">
                        <tr>
                            <td width="120" align="right" bgcolor="EBF1F1">
                                <font color="red">*</font><%=GetTran("000025", "会员姓名")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:TextBox ID="Name" runat="server" onKeyUp="keypress()"  MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000081", "证件类型")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:DropDownList ID="PaperType" runat="server" onchange="paperchange()">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="t1">
                            <td align="right" bgcolor="EBF1F1">
                                <font color="red">*</font><%=GetTran("000083", "证件号码")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:TextBox ID="PaperNumber" runat="server" Width="200px" MaxLength="18"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="t2">
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000084", "出生日")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:TextBox ID="Birthday" runat="server" CssClass="Wdate" Width="85px" onfocus="new WdatePicker()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="t3">
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000085", "性别")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:RadioButtonList ID="Sex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="1" Selected="true">男</asp:ListItem>
                                    <asp:ListItem Value="0">女</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000086", "开户名")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Label runat="server" ID="BankBook"></asp:Label>                            
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000087", "开户银行")%>：
                            </td>
                            <td bgcolor="#F8FBFD" >
                                <asp:DropDownList ID="DropDownList1" runat="server" onchange="bankchange()"></asp:DropDownList>
                                <asp:DropDownList ID="MemberBank" runat="server">
                                </asp:DropDownList>
                                &nbsp;
                                <%=GetTran("006046", "支行名称")%>：<asp:TextBox ID="txtEbank" runat="server" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000088", "银行账号")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:TextBox ID="BankNum" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator5" runat="server" ValidationExpression="[0-9]{0,20}"
                                    ControlToValidate="BankNum" ErrorMessage="只能输入数字！"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                                      <tr style="display:none;">
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000089", "银行地址")%> ：
                            </td>
                            <td bgcolor="#F8FBFD" >
                                <table class="biaozzi">
                                    <tr>
                                        <td>
                                        <uc1:CountryCityPCode ID="CountryCity2" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="BankAdderss" runat="server" Width="192px" MaxLength="50"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000090", "个人注册期数")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Literal ID="ExpectNum" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="right" bgcolor="EBF1F1">
                                <%=GetTran("000091", "所属店编号")%>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:Literal ID="StoreID" runat="server"></asp:Literal>
                            </td>
                        </tr>--%>
                        <tr style="display:none">
                            <td align="right" bgcolor="#EBF1F1">
                                <%=GetTran("006997","奖金发放是否需要申请") %>：
                            </td>
                            <td bgcolor="#F8FBFD">
                                <asp:RadioButtonList ID="rbtJj" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                       </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <table width="400" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="BtnUpdate"  runat="server" Text="修 改" OnClick="BtnUpdate_Click1" CssClass="anyes" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button value="返回" ID="Button1" name="Button1" runat="server" Text="返 回" OnClick="Button1_Click"
                    CssClass="anyes" CausesValidation="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <%= msg %>
    </form>
</body>
</html>
