<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="RegisterStore.aspx.cs" Inherits="Company_RegisterStore" %>

<%@ Register Src="../UserControl/Language.ascx" TagName="Language" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc2" %>
<%@ Register src="../UserControl/CountryCityPCode.ascx" tagname="CountryCityPCode" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<link type="text/css" rel="Stylesheet" href="CSS/Company.css" />
 <script language="javascript" src="../js/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">
        function GetMemberName()
        {        
            var name=document.getElementById("txtNumber").value;
            var result=AjaxClass.GetMumberName(name).value;
            if(result==""||result==null)
            {
                alert('<%=GetTran("000537", "对不起,该会员不存在")%>');
            }else
            {
                document.getElementById("txtName").value=result;
                //家庭电话
                var resultH=AjaxClass.getMemberHTele(name).value;
                if(resultH==""||resultH==null || resultH=="电话号码")
                {
                    document.getElementById("txtHomeTele").value="";
                }
                else
                {
                    document.getElementById("txtHomeTele").value=resultH;
                }
            //手机
                var resultM=AjaxClass.getMemberMTele(name).value;
                if(resultM==""|| resultM==null || resultM=="电话号码")
                {
                    document.getElementById("txtMobileTele").value="";
                }
                else
                {
                    document.getElementById("txtMobileTele").value=resultM;
                }
            //办公室

                var resultO=AjaxClass.getMemberOTele(name).value;
                if(resultO==""||resultO==null || resultO=="电话号码")
                {
                    document.getElementById("txtOfficeTele").value="";
                }
                else
                {
                    document.getElementById("txtOfficeTele").value=resultO;
                }
            //传真
                var resultF=AjaxClass.getMemberFTele(name).value;
                if(resultF==""||resultF==null || resultF=="电话号码")
                {
                    document.getElementById("txtFaxTele").value="";
                }
                else
                {
                    document.getElementById("txtFaxTele").value=resultF;
                }
                
            }
            
         }
         function CheckText()
	     {
		    //这个方法是只有1个lkSubmit按钮时候 可直接用
		   
		    filterSql();
	     }

         
           	//绑定邮编
		function GetCCode_s2(city)
		{
		    var sobj = document.getElementById("<%=txtPostalCode.ClientID %>");
		    sobj.value=AjaxClass.GetAddressCode(city).value
		}       
    </script>
<script type="text/javascript">
	function down2()
	{
		if(document.getElementById("divTab2").style.display=="none")
		{
			document.getElementById("divTab2").style.display="block";
			document.getElementById("imgX").src="images/dis1.GIF";
			
		}
		else
		{
			document.getElementById("divTab2").style.display="none";
			document.getElementById("imgX").src="images/dis.GIF";
		}
	}
 </script>
</head>

<body>
    <form id="form1" runat="server">
        <!--******************************增加页面代码********************************-->
        <table width="650" border="0" align="center" cellpadding="0" cellspacing="0" class="tablett">
            <tr>
                <th colspan="2" style="white-space:nowrap">
                    <asp:Label ID="lbltitel" runat="server" Text=""></asp:Label>
                </th>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("007233", "申请人的会员编号")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtNumber" runat="server" MaxLength="100"></asp:TextBox><font style="color:Red">*</font>
                    <input type="button" id="btnGetmember" value='<%=GetTran("000533", "点击获取信息")%>' onclick="GetMemberName()"
                        style="background-color: #FFFFFF; border: 0 #FFFFFF none; margin: 0; padding: 0;
                        color: Red;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumber"
                        ErrorMessage="请输入会员编号！！！" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ControlToValidate="txtNumber" ErrorMessage="会员编号只能为字母数字下划线，且在6~100位之间" 
                        ValidationExpression="\w{6,100}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000037", "店编号")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtStoreId" runat="server" MaxLength="10"></asp:TextBox><font style="color:Red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStoreId"
                        ErrorMessage="请输入店编号"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                        ControlToValidate="txtStoreId" ErrorMessage="店编号只能为字母数字下划线，且在4~100位之间" 
                        ValidationExpression="\w{4,100}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000307", "推荐店的会员编号")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtDirect" runat="server" MaxLength="100"></asp:TextBox><font style="color:Red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDirect"
                        ErrorMessage="请输入推荐您的会员编号！！！"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                        ControlToValidate="txtDirect" ErrorMessage="会员编号只能是字母数字下划线，且在6~100位之间" 
                        ValidationExpression="\w{6,100}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000039", "店长姓名")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtName" runat="server" MaxLength="50"></asp:TextBox><font style="color:Red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName"
                        ErrorMessage="请输入店长名！！！"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000040", "店铺名称")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtStoreName" runat="server" MaxLength="50"></asp:TextBox><font style="color:Red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStoreName"
                        ErrorMessage="请输入店铺名称！！！"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000313", "店铺所在地")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                 <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
                  <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                    <asp:DropDownList ID="StoreCountry" runat="server" 
                        onselectedindexchanged="StoreCountry_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp;<asp:DropDownList ID="StoreCity" runat="server">
                        </asp:DropDownList><font color="Red">*</font><%=GetTran("000314", "以后无法修改此信息")%>
                           </ContentTemplate>
                </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("007234", "联系人地址")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
               
               
                        <table border="0" cellpadding="0" cellspacing="0"><tr><td><uc3:CountryCityPCode ID="CountryCity1" runat="server" /></td><td>
                       
                       <asp:TextBox ID="txtaddress" runat="server" MaxLength="150"></asp:TextBox><font style="color:Red">*</font></td></tr></table> 
                 
                   
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000073", "邮编")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                        ControlToValidate="txtPostalCode" ErrorMessage="邮编不能有字符" 
                        ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr style="display:none">
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    登陆语言：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:DropDownList ID="ddlLanaguage" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display:none">
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    币种：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:DropDownList ID="Currency" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000319", "负责人电话")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtHomeTele" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                        ControlToValidate="txtHomeTele" ErrorMessage="不能有字符" 
                        ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000044", "办公电话")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtOfficeTele" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                        ControlToValidate="txtOfficeTele" ErrorMessage="不能有字符" 
                        ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000069", "移动电话")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtMobileTele" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" 
                        runat="server" ControlToValidate="txtMobileTele" ErrorMessage="不能有字符" 
                        ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000071", "传真电话")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtFaxTele" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" 
                        runat="server" ControlToValidate="txtFaxTele" ErrorMessage="不能有字符" 
                        ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000087", "开户银行")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                        <asp:UpdatePanel ID="up1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlBank" runat="server" bgcolor="#EBF1F1">
                            </asp:DropDownList>
                        </ContentTemplate>
                            
                            </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000329", "银行账户")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtBankCard" runat="server" MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000330", "电子邮箱")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD"> 
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="邮箱格式不正确" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000332", "网址")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtNetAddress" runat="server" MaxLength="50"></asp:TextBox><%=GetTran("000924", "例")%>：http://www.baidu.com

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNetAddress"
                        ErrorMessage="网址填写不正确" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000078", "备注")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD"> 
                    <asp:TextBox ID="txtRemark" runat="server" Height="73px" TextMode="MultiLine" 
                        Width="564px" MaxLength="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000046", "级别")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:RadioButtonList ID="rdoListLevel" runat="server" RepeatDirection="Horizontal">
                       <%--  <asp:ListItem Selected="True" Value="1">县级</asp:ListItem>
                        <asp:ListItem Value="2">市级</asp:ListItem>
                        <asp:ListItem Value="3">省级</asp:ListItem>--%>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000341", "经营面积(平方米)")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtFareArea" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" 
                        runat="server" ControlToValidate="txtFareArea" ErrorMessage="输入的数据不对" 
                        ValidationExpression="^[-\+]?\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000342", "经营品种")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtFareBreed" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space:nowrap" bgcolor="#EBF1F1">
                    <%=GetTran("000343", "投资总额(万元)")%>：

                </td>
                <td style="white-space:nowrap" bgcolor="#F8FBFD">
                    <asp:TextBox ID="txtTotalAccountMoney" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" 
                        runat="server" ControlToValidate="txtTotalAccountMoney" ErrorMessage="输入数据不对" 
                        ValidationExpression="^[-\+]?\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr style="display:none">
                <td colspan="2" style="white-space:nowrap">
                    <div id="cssrain">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/DMdp.gif">
                            <tr>
                                <td width="80px">
                                    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" id="secTable">
                                        <tr>
                                            <td class="sec2">
                                                <%=GetTran("000033", "说 明")%>
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
                                <tbody style="display: block">
                                    <tr>
                                        <td style="padding-left: 20px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        1、<%=GetTran("000346", "只有本公司的会员才能注册店铺,在注册店铺的时候必须填写, 会员的编号,和推荐开店铺的会员编号.")%>
                                                        <br />
                                                      2 、<%=GetTran("000347", "店铺密码：如果开店的会员有证件号则是证件号的后六位，反之就是自己的会员编号.")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td style="white-space:nowrap" align="center">
                <asp:linkbutton id="lkSubmit" Runat="server" Text="提交" style="DISPLAY: none"  onclick="lkSubmit_Click"></asp:linkbutton>
                            <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value='<%=GetTran("000351", "提 交")%>' />
                <asp:Button ID="btnAdd" runat="server" Text="提 交" OnClick="btnAdd_Click" Visible="false" CssClass="anyes">
                        </asp:Button>
                        
                   &nbsp;&nbsp;<asp:Button ID="btnblock" runat="server" Text="返回" 
                        CssClass="anyes" onclick="btnblock_Click" Visible="false" />
                </td>
            </tr>
        </table>
        <br />
    </form>
    <%=msg %>
        
</body>
</html>
