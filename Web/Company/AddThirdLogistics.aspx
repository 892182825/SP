<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddThirdLogistics.aspx.cs" Inherits="Company_AddThirdLogistics" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/CountryCity.ascx" TagPrefix="uc1" TagName="CountryCity" %>
<%@ Register src="../UserControl/CountryCityPCode.ascx" tagname="CountryCityPCode" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加物流公司</title>
    <script language="javascript" type="text/javascript"  src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript"  src="javascript/js.js"></script>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
<script language="javascript">
	//绑定邮编
		function GetCCode_s2(city)
		{
		    var sobj = document.getElementById("<%=txtPostalCode.ClientID %>");
		    sobj.value=AjaxClass.GetAddressCode(city).value
		}
		
		function yz()
		{
		    if(isNaN(document.getElementById("txtPostalCode").value))
		    {
		        alert('<%=GetTran("006951","邮编只能输入数字") %>');
		        return false;
		    }
		    else if(isNaN(document.getElementById("txtBankCard").value))
		    {
		        alert('<%=GetTran("006950","银行账号只能输入数字") %>');
		        return false;
		    }
		    else
		        true;
		}
		
</script>
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
    <script type="text/javascript" src="../js/SqlCheck.js"></script>
</head>
<body ><%--onload="setZF('txtRemark',200)"--%>
    <form id="form1" runat="server" onsubmit="return filterSql_III()">
    <br/>    
    <div>
    <table width="600px" border="0" align="center" cellpadding="0" cellspacing="1" class="tablett">
        <tr>
            <th colspan="2" >
                <asp:Label ID="messagebox" runat="server" ></asp:Label>
            </th>
        </tr>
        <tr >
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("001897","公司编号") %>：</td>
            <td bgcolor="#F8FBFD">
                <asp:TextBox ID="txtnumber" runat="server" MaxLength="10" /><font color="red">*</font>
                <asp:Label ID="lblNumber" runat="server" Text="" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage='<%#GetTran("002132","不能输入字母,请重输") %>'
                    ControlToValidate="txtnumber" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                
                <asp:Label ID="lblMsg" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("001900","公司名称") %>：</td>
            <td bgcolor="#F8FBFD"><asp:TextBox ID="txtName" runat="server" MaxLength="20" />
                <span class="style1">*</span><%=GetTran("002125", "必填")%></td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("000044","办公电话") %>：</td>
            <td bgcolor="#F8FBFD"><asp:TextBox ID="txtTelephone1" runat="server" MaxLength="20" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage='<%#GetTran("002132","不能输入字母,请重输") %>'
                    ControlToValidate="txtTelephone1" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("002111", "所在地域")%>：</td>
            <td bgcolor="#F8FBFD"><table border="0" cellpadding="0" cellspacing="0"><tr><td><uc2:CountryCityPCode ID="CountryCity1" runat="server" /></td><td>
                
                <asp:TextBox ID="txtStoreAddress" runat="server" MaxLength="100"></asp:TextBox></td></tr></table> </td>
        </tr>
        
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("000073", "邮编")%>：</td>
            <td bgcolor="#F8FBFD">
                <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("001910","负责人姓名") %>：</td>
            <td bgcolor="#F8FBFD"><asp:TextBox ID="txtPrincipal" runat="server" MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr style="display:none">
            <td align="right" bgcolor="#EBF1F1">负责人电话：</td>
            <td bgcolor="#F8FBFD">
                <asp:TextBox ID="txtTelephone2" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="不能输入字母,请重输"
                    ControlToValidate="txtTelephone2" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr style="display:none">
            <td align="right" bgcolor="#EBF1F1">负责人手机：</td>
            <td bgcolor="#F8FBFD">
                <asp:TextBox ID="txtTelephone3" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="不能输入字母,请重输"
                    ControlToValidate="txtTelephone3" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("000071","传真电话") %>：</td>
            <td bgcolor="#F8FBFD">
                <asp:TextBox ID="txtTelephone4" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="不能输入字母,请重输"
                    ControlToValidate="txtTelephone4" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("000087", "开户银行")%> ：</td>
            <td bgcolor="#F8FBFD">
                <asp:DropDownList ID="ddlBank" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("000088","银行帐号") %>：</td>
            <td bgcolor="#F8FBFD"><asp:TextBox ID="txtBankCard" runat="server" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("000962", "税号")%>：</td>
            <td bgcolor="#F8FBFD"><asp:TextBox ID="txtTax" runat="server" MaxLength="50"> </asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("001981", "批准日期")%>：</td>
            <td bgcolor="#F8FBFD"><asp:TextBox ID="txtDate"  style="WIDTH:120px" onfocus="WdatePicker()"  CssClass="Wdate" runat="server"/></td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("001983", "营业执照号码")%>：</td>
            <td bgcolor="#F8FBFD"><asp:TextBox ID="txtLicenceCode" runat="server" MaxLength="50"> </asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" bgcolor="#EBF1F1"><%=GetTran("000078", "备注")%>：</td>
            <td bgcolor="#F8FBFD"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="68px" Width="280px" MaxLength="20"></asp:TextBox></td>
        </tr>
        
    </table>
    <br />
    <table width="600px" align="center">
       <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="保 存" OnClientClick="return yz()" OnClick="btnSave_Click"  CssClass="anyes" /> 
                &nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" Text="返 回"  CssClass="anyes" onclick="btnClear_Click"/> 
            </td>   
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
