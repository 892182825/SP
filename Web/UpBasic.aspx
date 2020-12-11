<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpBasic.aspx.cs" Inherits="UpBasic"
    EnableEventValidation="false" %>

<%@ Register Src="UserControl/CountryCityPCode.ascx" TagName="CountryCityPCode" TagPrefix="uc1" %>
<%@ Register Src="UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改注册</title>
    <link href="Company/CSS/Company.css" rel="stylesheet" id="cssid" type="text/css" />

    <script language="javascript" type="text/javascript" src="javascript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" src="js/SqlCheck.js"></script>
    <script language="javascript" type="text/javascript">
        function txtchangedname()
        {

              if(document.getElementById("lblEbankname")!=null)
              {
                document.getElementById("lblEbankname").innerHTML = document.getElementById("txtEname").value;
              }
        }
        
        	//绑定邮编
		function GetCCode_s2(city)
		{
		    var sobj = document.getElementById("<%=txtEcode.ClientID %>");
		    sobj.value=AjaxClass.GetAddressCode(city).value
		}
		
		//身份证类型
		function GetEidtype()
		{  
		    var Eva=document.getElementById("<%=txtEIdtype.ClientID %>").value;

		 if(Eva=="P001")
         {
             document.getElementById("Esex").style.display="none";
             document.getElementById("Ebirthday").style.display='none';
         }else
         {
            document.getElementById("Esex").style.display="";
            document.getElementById("Ebirthday").style.display="";
         }
          if(Eva=="P000")
         {
             document.getElementById("txtEidnumber").value="";
             document.getElementById("trEidnum").style.display='none';
         }else
         {
            document.getElementById("trEidnum").style.display="";
         }
		}
		
    </script>

    <script type="text/javascript">
//        function change()
//        {
//              //   GetEidtype();     
//            var a = '<%=GetCssType() %>';
//            var css=document.getElementById("cssid");
//            if (a==1)
//                css.setAttribute("href","Company/CSS/Company.css");
//            if (a==2)
//                css.setAttribute("href","Store/CSS/Store.css");
//            if (a==3)
//                css.setAttribute("href","Member/CSS/Member.css");
//        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 17px;
        }
    </style>
<script type="text/javascript">
	function showImage(obj)
	{
		document.getElementById("imgE").src=obj.value;
	}
</script>
<script language="javascript" type="text/javascript">
 

</script>
<script type="text/javascript">    
      
	    

	  

	      //判断是否是半角数字
	    function isShuZi(txtStr)
	    {
		    var	oneNum="";
		    for(var i=0;i<txtStr.length;i++)
		    {
			    oneNum=txtStr.substring(i,i+1);
			    if (oneNum<"0" || oneNum>"9")
				    return true;
		    }
		    return false;
	    }
	    
	    //判断输入的手机号格式是否正确
	    function MobTelQuOnblur()
	    {
	   
		    var mobTel = document.getElementById('txtEmobile').value;
		    if(mobTel!='')
		    {
			    var isInt = isShuZi(mobTel);
			    if(isInt)
			    {
			        document.getElementById('spanMobile').innerHTML = '<%=GetTran("006559","移动电话必须是半角数字组成的!") %>';
				    //alert('<%=GetTran("006559","移动电话必须是半角数字组成的!") %>');
				    return;
			    }
			    if(mobTel.length!=11)
			    {
			        document.getElementById('spanMobile').innerHTML = '<%=GetTran("006560","移动电话必须是11位的！") %>';
				    return;
			    }
		    }
		    else
		    {
		        document.getElementById('spanMobile').innerHTML = '<%=GetTran("006889","移动电话不能为空！") %>';
		        return;
		    }
		    document.getElementById('spanMobile').innerHTML = "";
	    }
	    
	     function MobTelQuOnblur2()
	    {
		    var mobTel = document.getElementById('txtEmobile').value;
		    if(mobTel!='')
		    {
			    var isInt = isShuZi(mobTel);
			    if(isInt)
			    {
				    document.getElementById('spanMobile').innerHTML = '<%=GetTran("006559","移动电话必须是半角数字组成的!") %>';
				    return false;
			    }
			    if(mobTel.length!=11)
			    {
				    document.getElementById('spanMobile').innerHTML = '<%=GetTran("006560","移动电话必须是11位的！") %>';
				    return false;
			    }
		    }
		    else
		    {
		        document.getElementById('spanMobile').innerHTML = '<%=GetTran("006889","移动电话不能为空！") %>';
		        return false;
		    }
		    document.getElementById('spanMobile').innerHTML = "";
		    return true;
	    }
	    
	    function GetTxtcolor()
	    {
	        var txtFamQh = document.getElementById('TxtjtdhQh');
	        if(txtFamQh.value=="区号")
	        {    
	            txtFamQh.style.color = "gray";
	        }
	        else
	        {
	            txtFamQh.style.color = "";
	        }
	        
	        var txtFamTel = document.getElementById('Txtjtdh');
	        if(txtFamTel.value=='电话号码')
	        {
	            txtFamTel.style.color = "gray";
	        }
	        else
	        {
	            txtFamTel.style.color = "";
	        }
  
	        var faxValue = document.getElementById('TxtczdhQh');
	        if(faxValue.value=="区号")
	        {    
	            faxValue.style.color = "gray";
	        }
	        else
	        {
	            faxValue.style.color = "";
	        }
	        
	        faxValue = document.getElementById('Txtczdh');
	        if(faxValue.value=="电话号码")
	        {    
	            faxValue.style.color = "gray";
	        }
	        else
	        {
	            faxValue.style.color = "";
	        }
	        
	        faxValue = document.getElementById('TxtczdhFj');
	        if(faxValue.value=="分机号")
	        {    
	            faxValue.style.color = "gray";
	        }
	        else
	        {
	            faxValue.style.color = "";
	        }
	        
	        var officValue = document.getElementById('TxtbgdhQh');
	        if(officValue.value=="区号")
	        {    
	            officValue.style.color = "gray";
	        }
	        else
	        {
	            officValue.style.color = "";
	        }
	        
	        officValue = document.getElementById('Txtbgdh');
	        if(officValue.value=="电话号码")
	        {    
	            officValue.style.color = "gray";
	        }
	        else
	        {
	            officValue.style.color = "";
	        }
	        
	        officValue = document.getElementById('TxtbgdhFj');
	        if(officValue.value=="分机号")
	        {    
	            officValue.style.color = "gray";
	        }
	        else
	        {
	            officValue.style.color = "";
	        }
	   
	    }
        
        function Verify()
	    {
	        var a = czdhQhOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	         
	        a = MobTelQuOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	         
	        a = faxTelOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	        
	        a = faxTelFjOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	        
	        a = jtdhQhOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	       
	        a = famTelOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	        
	        a = bgdhQhOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	        
	        a = offmTelOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	        
	        a = officeTelFjOnblur2();
	        if(a==false)
	        {
	            return false;
	        }
	        
	        return true;
	    }
	    
	    function ShowStore()
	    {
	         var type = '<%=GetLogoutType() %>';
	         if(type=="2")
	         {
	             document.getElementById('trStore').style.display = "none";
	         }
	         else
	         {
	             document.getElementById('trStore').style.display = "";
	         }
	    }
	    
	     window.onload =function()
        {
	          try
	          {
	             GetTxtcolor();
	          }
	         catch(e)
	         {}
	     }
</script>

</head>
<body ><%--onload='change()'--%>
    <form id="form1" runat="server" onsubmit="filterSql_III()">
    <div align="center" width="660px">
        <table align="center" width="660px">
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                        <tr>
                            <td align="left"  width="560px">
                                <img src="images/men01.gif" align="absbottom" />
                                <b>
                                    <%=this.GetTran("002165", "基本信息")%></b><span>｜<font color="Gray"><%=this.GetTran("003159", "关系到您本人的切身利益，请准确填写。")%></font></span><span>
                                        <font color="red">（<%=this.GetTran("003166", "注")%>：<span>*</span>
                                            <%=this.GetTran("003170", "为必填项")%>）</font></span>
                            </td>
                            <td align="right" width="100px" valign="bottom">
                                <asp:Button ID="btnEbasic" runat="server" Text="修改" ValidationGroup="G" OnClick="btnEbasic_Click"
                                    CssClass="anyes" />
                                    
                                    <asp:linkbutton id="lkSubmit" style="DISPLAY: none" Runat="server" Text="保存" onclick="lkSubmit_Click"></asp:linkbutton>
                                    
                                    <input class="anyes" id="bSubmit" onclick="CheckText()" type="button" value="保存" style="DISPLAY: none" ></input>

                                    
                                <asp:Button ID="btnSbasic" runat="server" Text="保存" ValidationGroup="G" Visible="false"
                                    OnClick="btnSbasic_Click" CssClass="anyes" />
                                    
                                    
                                <asp:Button ID="btnCbasic" runat="server" Text="取消" OnClick="btnCbasic_Click" Visible="false"
                                    CssClass="anyes" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table id="tbebasic" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" class="tablett">
                                    <tr>
                                        <td width="80px" align="right">
                                            <%=GetTran("000024", "会员编号")%>：
                                        </td>
                                        <td align="left" width="460px">
                                            <b>
                                                <asp:Label ID="lblEnumber" runat="server" /></b><asp:TextBox ID="txtEnumber" runat="server"
                                                    ValidationGroup="G" MaxLength="50" />
                                        </td>
                                        <td rowspan="5" align="right" valign="top">
                                            <asp:Image ID="imgE" runat="server" width="100px" Height="130px" style="display:none;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <font color="red">*</font><%=GetTran("003176", "真实姓名")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEname" runat="server" /><asp:TextBox ID="txtEname" runat="server"
                                                onblur="txtchangedname()" ValidationGroup="G" MaxLength="50" />
                                            <asp:Label ID="labenc" runat="server">&nbsp;<%=GetTran("000209", "会员姓名长度小于50个字！")%></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("000063", "会员昵称")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEnickname" runat="server" /><asp:TextBox ID="txtEnickname" runat="server"
                                                ValidationGroup="G" MaxLength="50" />
                                        </td>
                                    </tr>
                                    <tr style="display:none" id="trStore">
                                        <td align="right">
                                            <font color="red">*</font><%=GetTran("001770", "购货店铺")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEstore" runat="server" /><asp:TextBox ID="txtEstore" runat="server"
                                                ValidationGroup="G" MaxLength="10" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("000081", "证件类型")%>：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="lblEIdtype" runat="server" /><asp:DropDownList ID="txtEIdtype" runat="server"
                                                onchange="GetEidtype()">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trEidnum">
                                        <td align="right">
                                            <font color="red">*</font> <%=GetTran("000083", "证件号码")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEidnumber" runat="server" /><asp:TextBox ID="txtEidnumber" runat="server"
                                                ValidationGroup="G" MaxLength="20" />
                                        </td>
                                    </tr>
                                    <tr id="Esex">
                                        <td align="right" width="12%">
                                            <%=GetTran("000103", "会员性别")%>：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="lblEsex" runat="server" />
                                            <asp:RadioButtonList ID="txtEsex" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">男</asp:ListItem>
                                                <asp:ListItem Value="0">女</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr id="Ebirthday">
                                        <td align="right">
                                            <%=GetTran("000105", "出生日期")%>：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="lblEbirthday" runat="server" /><asp:TextBox ID="txtEbirthday" runat="server"
                                                CssClass="Wdate" onfocus="WdatePicker()" MaxLength="12" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <font color="red">*</font><%=GetTran("003177", "联系地址")%>：
                                        </td>
                                        <td align="left" colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEaddress" runat="server" /><uc1:CountryCityPCode ID="CountryCityPCode1"
                                                            runat="server" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtEaddress" runat="server" ValidationGroup="G" MaxLength="100" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("000114", "邮政编码")%>：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="lblEcode" runat="server" /><asp:TextBox ID="txtEcode" runat="server"
                                                MaxLength="6" ValidationGroup="G" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ValidationExpression="[0-9---(-)]{0,20}"
                                                ControlToValidate="txtEcode" ValidationGroup="G" ErrorMessage="不能输入字母,请重输！"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td align="right">
                                            <%=GetTran("000526", "运货方式")%>：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="lblEmethod" runat="server" /><asp:TextBox ID="txtEmethod" runat="server"
                                                ValidationGroup="G" MaxLength="30" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td align="right">
                                            <%=GetTran("000185", "支付币种")%>：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="lblEcurrency" runat="server" /><asp:TextBox ID="txtEcurrency" runat="server"
                                                ValidationGroup="G" MaxLength="30" />
                                        </td>
                                    </tr>
                                   
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" width="100%">
                        <tr>
                            <td align="left" valign="bottom">
                                <img src="images/zfu.gif" height="42px" width="35px" align="absbottom">
                                <b>
                                    <%=GetTran("003208", "联系信息")%></b>｜<font color="Gray"><%=GetTran("003210", "请准确填写您的联系方式，以便我们可以及时联系。")%></font>
                            </td>
                            <td align="right" width="100px" valign="bottom">
                                <asp:Button ID="btnEContact" runat="server" Text="修改" OnClick="btnEContact_Click"
                                    CssClass="anyes" />
                                <asp:Button ID="btnSContact" runat="server" Text="保存" ValidationGroup="H" Visible="false"
                                    OnClick="btnSContact_Click" CssClass="anyes"/>
                                <asp:Button ID="btnCContact" runat="server" Text="取消" OnClick="btnCContact_Click"
                                    Visible="false" CssClass="anyes" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablett">
                                    <tr>
                                        <td width="12%" align="right">
                                            <span style="color:Red">*</span><%=GetTran("000069", "移动电话")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEmobile" runat="server" /><asp:TextBox ID="txtEmobile"   onblur="MobTelQuOnblur()"  runat="server"
                                                MaxLength="20" />
                                                <span id="spanMobile" style="color:Red"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("000065", "家庭电话")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEphone" runat="server" />
                                            <div id="divJt" runat="server">
                                                <asp:TextBox ID="TxtjtdhQh" runat="server" MaxLength="4" onblur="jtdhQhOnblur()" onfocus="jtdhQhOnfocus()" Width="27px">区号</asp:TextBox>-<asp:TextBox id="Txtjtdh" onblur="famTelOnblur()" onfocus="famTelOnfocus()"  runat="server" Width="65px" MaxLength="15">电话号码</asp:TextBox>
                                                <span id="spanFamilyTel" style="color:Red"></span>
                                             </div>
                                    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("000071", "传真电话")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEfax" runat="server" />
                                               <div id="divCz" runat="server"> <asp:TextBox MaxLength="4" ID="TxtczdhQh" runat="server" onblur="czdhQhOnblur()" onfocus="czdhQhOnfocus()" Width="27px">区号</asp:TextBox>-<asp:textbox id="Txtczdh" onblur="faxTelOnblur()" onfocus="faxTelOnfocus()"  runat="server" Width="65px" MaxLength="15">电话号码</asp:textbox>-<asp:TextBox ID="TxtczdhFj" onblur="faxTelFjOnblur()" onfocus="faxTelFjOnfocus()" runat="server" MaxLength="4" Width="40px">分机号</asp:TextBox>
                                                <span id="spanFaxTel" style="color:Red"></span></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("000044", "办公电话")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEOffphone" runat="server" />
                                            <div id="divBg" runat="server">
                                                <asp:TextBox ID="TxtbgdhQh" MaxLength="4" runat="server" onblur="bgdhQhOnblur()" onfocus="bgdhQhOnfocus()" Width="27px">区号</asp:TextBox>-<asp:TextBox id="Txtbgdh" onblur="offmTelOnblur()" onfocus="offmTelOnfocus()"  runat="server" Width="65px" MaxLength="15">电话号码</asp:TextBox>-<asp:TextBox ID="TxtbgdhFj" onblur="officeTelFjOnblur()" onfocus="officeTelFjOnfocus()" MaxLength="4" runat="server" Width="40px">分机号</asp:TextBox>
                                   <span id="spanOfficeTel" style="color:Red"></span>
                                            </div>                                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("001532", "Email")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEemail" runat="server" /><asp:TextBox ID="txtEemail" runat="server"
                                                MaxLength="50" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="H"
                                                ControlToValidate="txtEemail" ErrorMessage="抱歉！请输入正确的Email地址" SetFocusOnError="True"
                                                ValidationExpression="^(?!(\.|-|_))(?![a-zA-Z0-9\.\-_]*(\.|-|_)@)[a-zA-Z0-9\.\-_]+@(?!.{64,}\.)(?![\-_])(?![a-zA-Z0-9\-_]*[\-_]\.)[a-zA-Z0-9\-_]+(\.\w+)+$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" align="center" cellpadding="0" cellspacing="0" class="biaozzi"
                        width="100%">
                        <tr>
                            <td align="left" valign="bottom">
                                <img src="images/ddnu01.gif" height="45px" width="35px" align="absbottom">
                                <b>
                                    <%=GetTran("003217", "帐户信息")%></b>｜<font color="Gray"><%=GetTran("003219", "如果没有银行帐号，可以在以后补充。但为方便业务处理，请尽早补充。")%></font>
                            </td>
                            <td align="right" width="100px" valign="bottom">
                                <asp:Button ID="btnEBank" runat="server" Text="修改" OnClick="btnEBank_Click" CssClass="anyes" />
                                <asp:Button ID="btnSBank" runat="server" Text="保存" ValidationGroup="B" Visible="false"
                                    OnClick="btnSBank_Click" CssClass="anyes" />
                                <asp:Button ID="btnCBank" runat="server" Text="取消" OnClick="btnCBank_Click" Visible="false"
                                    CssClass="anyes" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablett">
                                    <tr>
                                        <td width="12%" align="right">
                                            <%=GetTran("000086", "开户名")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEbankname" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("000087", "开户银行")%>：
                                        </td>
                                        <td align="left">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEbank" runat="server" />
                                                        <asp:Panel ID="CcpEbankaddress" runat="server">
                                                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DdlBank" runat="server">
                                                            </asp:DropDownList>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="plEbank" runat="server">
                                                            支行：<asp:TextBox ID="txtEbank" runat="server" MaxLength="100" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("001407", "银行卡号")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEbanknumber" runat="server" /><asp:TextBox ID="txtEbanknumber"
                                                runat="server" MaxLength="25" />
                                                <asp:RegularExpressionValidator ID="REVtxtEbanknumber" runat="server" ValidationGroup="B"
                                                ControlToValidate="txtEbanknumber" ErrorMessage="只能输入25位数字、-" SetFocusOnError="True"
                                                ValidationExpression="^[0-9 -]*"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=GetTran("000089", "银行地址")%>：
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEbankaddress" runat="server" />
                                            <table>
                                                <tr>
                                                    <td>
                                                        <uc2:CountryCity ID="CountryCityPCode2" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEbankaddress" runat="server" MaxLength="100" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                <br />
               <asp:Button ID="btnEfh" runat="server" Text="返回" CssClass="anyes" onclick="btnEfh_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
