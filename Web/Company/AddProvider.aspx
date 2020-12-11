<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProvider.aspx.cs" Inherits="Company_AddProvider" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
    <script language="javascript" type="text/javascript" src="../JS/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">        
        function ResetValue()
        {
            return confirm('<%=GetTran("001222","确实要清空吗?")%>');
        }
        function checkProviderID(obj)
        {
            var rusult=AjaxClass.checkProviderID(obj).value;
            if(rusult!="")
            {
               alert(rusult);
            }
        }
        function checkProviderName(obj)
        {
            var type='<%=num %>';
            if(type=="")
            {
                var rusult=AjaxClass.checkProviderName(obj,"").value;
                if(rusult!="")
                {
                   alert(rusult);
                }
            }
            else
            {
               var rusult=AjaxClass.checkProviderName(obj,type).value;
                if(rusult!="")
                {
                   alert(rusult);
                } 
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return filterSql_III()">    
    <br />
    <table width="650px" border="0px" align="center" cellpadding="0px" cellspacing="0px" class="tablett">
      <tr>
        <td>
          <table width="100%" border="0px" align="center" cellpadding="0px" cellspacing="1px">
            <tr>
              <th colspan="2"><asp:Label runat="server" ID="lab" ></asp:Label> </th>
            </tr>
            <tr>
              <td width="120px" align="right" bgcolor="#EBF1F1"><font color="red">*</font><%=GetTran("000927", "供应商编号")%>：</td>
              <td bgcolor="#F8FBFD">
                  <div style="float:left;">
                  <asp:TextBox ID="txtnumber" runat="server" MaxLength="15" onblur="checkProviderID(this.value);"></asp:TextBox>
              <asp:Label runat="server" ID="lblNumber" Visible="false"></asp:Label>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnumber"
                        ErrorMessage="编号格式错误" ValidationExpression="\w{6,15}"></asp:RegularExpressionValidator>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnumber"
                        ErrorMessage="必填"></asp:RequiredFieldValidator>
                  </div>
                  <div style="float:left;"><asp:Label runat="server" ID="lblInfo">
                  <font color="red"><%=GetTran("001275", "供应商编号只能是数字字母下划线组成，且在6~15位之间")%></font>
                  </asp:Label> 
                    
                    </div>
                </td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><font color="red">*</font><%=GetTran("000931", "供应商名称")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtName" runat="server" MaxLength="30" onblur="checkProviderName(this.value);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                            ErrorMessage="必填"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000958", "供应商简称")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtForShort" runat="server" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" bgcolor="#EBF1F1"><%=GetTran("001232", "供应商地址")%>：</td>
                <td bgcolor="#F8FBFD"><asp:TextBox ID="txtAddress" runat="server" Width="301px" MaxLength="40"></asp:TextBox></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000960", "联系人")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtLinkMan" runat="server" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000052", "手机")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtMobile" runat="server" MaxLength="15"></asp:TextBox>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMobile"
                            ErrorMessage="手机格式不对" ValidationExpression="^0{0,1}(13[0-9]|15[7-9]|153|156|18[7-9])[0-9]{8}$"></asp:RegularExpressionValidator>
              </td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000646", "电话")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtTelephone" runat="server" MaxLength="15"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtTelephone"
                            ErrorMessage="电话格式不对(如：(021-3836545))" ValidationExpression="^((0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$"></asp:RegularExpressionValidator>
              </td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000643", "传真")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtFax" runat="server" MaxLength="15"></asp:TextBox>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage='不能输入字母,请重输'
                        ControlToValidate="txtFax" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000330", "电子邮箱")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtEmail" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="电子邮箱格式不对" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000332", "网址")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtUrl" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUrl"
                            ErrorMessage="你输入的网址格式不对，请输入格式为http://1.com" ValidationExpression="^(http(s)?:\/\/)?(www\.)?[\w-]+\.\w{2,4}(\/)?$"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("001243", "开户行")%>：</td>
              <td bgcolor="#F8FBFD">                                   
                <asp:DropDownList ID="ddlCountry" runat="server" 
                    onselectedindexchanged="ddlCountry_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:DropDownList runat="server" ID="ddlBankName" ></asp:DropDownList>                                  
              </td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("001239", "开户行地址")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtBankAddress" runat="server" Width="321px" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000111", "银行账号")%>：</td>
              <td bgcolor="#F8FBFD">
                <asp:TextBox ID="txtBankNumber" runat="server" MaxLength="25"></asp:TextBox>
                <asp:RegularExpressionValidator ID="REVtxtBankNumber" runat="server"
                    ControlToValidate="txtBankNumber" ErrorMessage="只能输入25位数字" SetFocusOnError="True"
                    ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
              </td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000962", "税号")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtDutyNumber" runat="server" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
              <td align="right" bgcolor="#EBF1F1"><%=GetTran("000078", "备注")%>：</td>
              <td bgcolor="#F8FBFD"><asp:TextBox ID="txtRemark" runat="server" Height="65px"  MaxLength="500"
                      TextMode="MultiLine" Width="324px"></asp:TextBox></td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
    <br />
    <table width="600px" border="0px" align="center" cellpadding="0px" cellspacing="0px">
      <tr>
       <td align="center">
            <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click" CssClass="anyes" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
            <asp:Button ID="btnReset" runat="server" OnClientClick="return ResetValue()" 
                CssClass="anyes" Text="重置" onclick="btnReset_Click" />   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
            <input type="button" class="anyes" onclick="location.href='Provider_ViewEdit.aspx'" value='<%=GetTran("000421","返回") %>' />
        </td>
      </tr>
    </table>
    <br />
    </form>
    <%=msg %>
</body>
</html>
