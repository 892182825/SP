<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberInfo.aspx.cs" ValidateRequest="false" Inherits="Member_MemberInfo" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="MemberBottom" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="MemberTop" TagPrefix="Uc1" %>
<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="Uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%= GetTran("004057", "资料修改") %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=11" />
     <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/detail.css" rel="stylesheet" type="text/css" />
    <link href="css/cash.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>
</head>

<body>
    <form id="form1" name="form1" runat="server" method="post" action="">
        <!--页面内容宽-->
     <div class="t_top">	
            	<a class="backIcon" href="javascript:history.go(-1)"></a>
       <%=GetTran("007290","提现申请")%> 
            </div>
                <!--内容,右下背景-->
                <div class="centConPage">
                    <div class="ctConPgCash" visible="false" id="table2" runat="server">
                        <h1 class="CashH1"><%=this.GetTran("002165", "个人基本信息")%></h1>

                        <table width="705px" border="0" cellspacing="1" cellpadding="0">
                            <tr>
                                <th><%=GetTran("000024", "会员编号")%>：</th>
                                <td>
                                    <asp:Label ID="LblBh" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000025", "会员姓名")%>：</th>
                                <td>
                                    <asp:Label ID="labName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000063", "会员昵称")%>：</th>
                                <td>
                                    <asp:TextBox ID="Txtlm" CssClass="ctConPgTxt" runat="server" MaxLength="20"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000103", "会员性别")%>：</th>
                                <td>
                                    <asp:Label ID="labSex" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000105", "出生日期")%>：</th>
                                <td>
                                    <asp:Label ID="lblBirthday" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000081", "证件类型")%>：</th>
                                <td>
                                    <asp:Label ID="lblzhengjian" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000083", "证件号码")%>：</th>
                                <td>
                                    <asp:Label ID="labzhengjianhaoma" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000065", "家庭电话")%>：</th>
                                <td>
                                    <asp:TextBox ID="Txtjtdh" CssClass="ctConPgTxt" onblur="famTelOnblur()" onfocus="famTelOnfocus()" runat="server" MaxLength="15">电话号码</asp:TextBox>
                                    <span id="spanFamilyTel" style="color: Red"></span>
                                </td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000071", "传真电话")%>：</th>
                                <td>
                                    <asp:TextBox ID="Txtczdh" CssClass="ctConPgTxt" onblur="faxTelOnblur()" onfocus="faxTelOnfocus()" runat="server" MaxLength="15">电话号码</asp:TextBox>
                                    <span id="spanFaxTel" style="color: Red"></span>
                                </td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000044", "办公电话")%>：</th>
                                <td>
                                    <asp:TextBox ID="Txtbgdh" CssClass="ctConPgTxt" onblur="offmTelOnblur()" onfocus="offmTelOnfocus()" runat="server" MaxLength="15">电话号码</asp:TextBox><span id="spanOfficeTel" style="color: Red"></span></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000069", "移动电话")%>：</th>
                                <td>
                                    <asp:TextBox ID="Txtyddh" CssClass="ctConPgTxt" runat="server" MaxLength="11"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="[0-9]{0,15}" ControlToValidate="Txtyddh" ErrorMessage="格式不正确！"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000072", "地址")%>：</font></th>
                                <td>
                                    <Uc1:CountryCity ID="CountryCity1" runat="server" />

                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <%=GetTran("000920","详细地址")%>：
                                </th>
                                <td>
                                    <asp:TextBox ID="txtdizhi" runat="server" MaxLength="30" CssClass="ctConPgTxt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000073", "邮编")%>：</th>
                                <td>
                                    <asp:TextBox ID="Txtyb1" CssClass="ctConPgTxt" runat="server" MaxLength="6"></asp:TextBox>
                                    <span id="span_txtyb" style="color: red;"></span>
                                </td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000087", "开户银行")%>：</th>
                                <td>
                                    <asp:DropDownList CssClass="ctConPgFor" ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList CssClass="ctConPgFor" ID="DdlBank" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th><%=GetTran("001239", "开户行地址")%>：</th>
                                <td>
                                    <div>
                                        <Uc1:CountryCity ID="CountryCity2" runat="server" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th><%= GetTran("007702", "分/支行名称")%>：</th>
                                <td>
                                    <asp:TextBox CssClass="ctConPgTxt" ID="txtbankbrachname" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000086", "开户名")%>：</th>
                                <td>
                                    <asp:Label ID="labkaihuming" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000111", "银行账号")%>：</th>
                                <td>
                                    <asp:TextBox CssClass="ctConPgTxt" MaxLength="30" ID="labkahao" runat="server"></asp:TextBox>
                                    <span id="span_kahao" style="color: Red;"></span>
                                </td>
                            </tr>
                            <tr>
                                <th><%=GetTran("000078", "备注")%>：</th>
                                <td>
                                    <asp:TextBox ID="Txtbz" CssClass="ctConPgTxt2" runat="server" Height="86px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                    <font color="Silver"><%=GetTran("006821", "不能多于500个字。")%></font></td>
                            </tr>
                        </table>

                        <ul>
                            <li><%--<asp:Button ID="go" Text="确 定" runat="server" CssClass="anyes" OnClientClick="return Verify();" OnClick="go_Click"></asp:Button>--%>

                                <asp:Button ID="go" runat="server" Height="27px" Width="52px" Style="margin-left: 17px; padding: 2px 9px; color: #FFF; border: 1px solid #507E0C; background-image: linear-gradient(#addf58,#96c742); text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);"
                                    Text="确 定" CssClass="anyes" OnClientClick="return Verify();" OnClick="go_Click" />

                            </li>
                        </ul>

                    </div>
                </div>
            </div>
            <!--版权信息-->
            <Uc1:MemberBottom ID="Bottom" runat="server" />
            <!--结束-->
        </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript" src="js/jquery-1.4.3.min.js"></script>
<%--<script type="text/javascript">
    function CheckText(btname) {
        //这个方法是页面有多个按钮要提交时，多次使用这个方法,传入按钮的ID
        var a = Verify();
        if (a == false) {
            return;
        }
        filterSql_II(btname);

    }
    </script>--%>
<script type="text/javascript">
    $(document).ready(
    function () {
        $('#CountryCity1_ddlX').change(
        function () {
            var sobj = document.getElementById("Txtyb1");
            sobj.value = AjaxClass.GetAddressCode($(this).val()).value;
        }
    );
    }
)
    function GetCCode_s2(xian) { };
</script>
<script type="text/javascript">

    function isIntTel(txtStr) {
        var validSTR = "1234567890";

        for (var i = 1; i < txtStr.length + 1; i++) {
            if (validSTR.indexOf(txtStr.substring(i - 1, i)) == -1) {
                return false;
            }
        }
        return true;
    }

    function Txtyb1Onfocus() {
        var Txtyb1 = document.getElementById('Txtyb1');
        var isInt = isIntTel(Txtyb1.value);
        if (!isInt) {
            document.getElementById('span_txtyb').innerHTML = '<%=GetTran("006951","邮编只能输入数字") %>!';
                return false;
            }
            document.getElementById('span_txtyb').innerHTML = '';
            return true;
        }
        function labkahaoOnfocus() {
            var labkahao = document.getElementById('labkahao');
            var isInt = isIntTel(labkahao.value);
            if (!isInt) {
                document.getElementById('span_kahao').innerHTML = '<%=GetTran("007543","卡号只能输入数字") %>！';
                return false;
            }
            document.getElementById('span_kahao').innerHTML = '';
            return true;
        }

        function faxTelOnfocus() {
            var faxTel = document.getElementById('Txtczdh');
            if (faxTel.value == '<%=GetTran("000028","电话号码") %>') {
                faxTel.style.color = "";
                faxTel.value = "";
            }
        }

        function faxTelOnblur() {
            var faxTel = document.getElementById('Txtczdh');
            if (faxTel.value != '') {
                if (faxTel.value != '<%=GetTran("000028","电话号码") %>') {
                    var isInt = isIntTel(faxTel.value);
                    if (!isInt) {
                        document.getElementById('spanFaxTel').innerHTML = '<%=GetTran("008102","传真电话只能输入数字") %>！';
                        return;
                    }
                }
            }
            else {
                faxTel.style.color = "gray";
                faxTel.value = '<%=GetTran("000028","电话号码") %>';
            }
            document.getElementById('spanFaxTel').innerHTML = "";
        }

        function faxTelOnblur2() {
            var faxTel = document.getElementById('Txtczdh');
            if (faxTel.value != '' && faxTel.value != '<%=GetTran("000028","电话号码") %>') {
                var isInt = isIntTel(faxTel.value);
                if (!isInt) {
                    document.getElementById('spanFaxTel').innerHTML = '<%=GetTran("008102","传真电话只能输入数字") %>！';
                    return false;
                }
            }
            document.getElementById('spanFaxTel').innerHTML = "";
            return true;
        }

        function famTelOnfocus() {
            var famTel = document.getElementById('Txtjtdh');
            if (famTel.value == '<%=GetTran("000028","电话号码") %>') {
                famTel.style.color = "";
                famTel.value = "";
            }
        }

        function famTelOnblur() {
            var famTel = document.getElementById('Txtjtdh');
            if (famTel.value != '') {
                if (famTel.value != '<%=GetTran("000028","电话号码") %>') {
                    var isInt = isIntTel(famTel.value);
                    if (!isInt) {
                        document.getElementById('spanFamilyTel').innerHTML = '<%=GetTran("007706","家庭电话只能输入数字") %>！';
                        return;
                    }
                }
            }
            else {
                famTel.style.color = "gray";
                famTel.value = '<%=GetTran("000028","电话号码") %>';
            }
            document.getElementById('spanFamilyTel').innerHTML = "";
        }

        function famTelOnblur2() {
            var famTel = document.getElementById('Txtjtdh');
            if (famTel.value != '' && famTel.value != '<%=GetTran("000028","电话号码") %>') {
                var isInt = isIntTel(famTel.value);
                if (!isInt) {
                    document.getElementById('spanFamilyTel').innerHTML = '<%=GetTran("007706","家庭电话只能输入数字") %>！';
                    return false;
                }
            }
            document.getElementById('spanFamilyTel').innerHTML = "";
            return true;
        }



        function offmTelOnfocus() {
            var faxTel = document.getElementById('Txtbgdh');
            if (faxTel.value == '<%=GetTran("000028","电话号码") %>') {
                faxTel.style.color = "";
                faxTel.value = "";
            }
        }


        function offmTelOnblur() {
            var faxTel = document.getElementById('Txtbgdh');
            if (faxTel.value != '') {
                if (faxTel.value != '<%=GetTran("000028","电话号码") %>') {
                    var isInt = isIntTel(faxTel.value);
                    if (!isInt) {
                        document.getElementById('spanOfficeTel').innerHTML = '<%=GetTran("007707","办公电话只能输入数字") %>！';
                        return;
                    }
                }
            }
            else {
                faxTel.style.color = "gray";
                faxTel.value = '<%=GetTran("000028","电话号码") %>';
            }
            document.getElementById('spanOfficeTel').innerHTML = "";
        }

        function offmTelOnblur2() {
            var faxTel = document.getElementById('Txtbgdh');
            if (faxTel.value != '' && faxTel.value != '<%=GetTran("000028","电话号码") %>') {
                var isInt = isIntTel(faxTel.value);
                if (!isInt) {
                    document.getElementById('spanOfficeTel').innerHTML = '<%=GetTran("007707","办公电话只能输入数字") %>！';
                    return false;
                }
            }
            document.getElementById('spanOfficeTel').innerHTML = "";
            return true;
        }

        function GetTxtcolor() {

            var txtFamTel = document.getElementById('Txtjtdh');
            if (txtFamTel.value == '<%=GetTran("000028","电话号码") %>') {
                txtFamTel.style.color = "gray";
            }
            else {
                txtFamTel.style.color = "";
            }

            faxValue = document.getElementById('Txtczdh');
            if (faxValue.value == '<%=GetTran("000028","电话号码") %>') {
                faxValue.style.color = "gray";
            }
            else {
                faxValue.style.color = "";
            }

            officValue = document.getElementById('Txtbgdh');
            if (officValue.value == '<%=GetTran("000028","电话号码") %>') {
                officValue.style.color = "gray";
            }
            else {
                officValue.style.color = "";
            }
        }

        function Verify() {

            a = faxTelOnblur2();
            if (a == false) {
                return false;
            }

            a = famTelOnblur2();
            if (a == false) {
                return false;
            }

            a = offmTelOnblur2();
            if (a == false) {
                return false;
            }
            a = labkahaoOnfocus();
            if (a == false) {
                return false;
            }
            a = Txtyb1Onfocus();
            if (a == false) {
                return false;
            }

            return true;
        }

        window.onload = function () {
            try {
                GetTxtcolor();
            }
            catch (e)
            { }
        }

</script>
