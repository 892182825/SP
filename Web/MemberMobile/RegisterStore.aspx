<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterStore.aspx.cs" Inherits="Member_RegisterStore"
    EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/Language.ascx" TagName="Language" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CountryCityPCode1.ascx" TagName="CountryCityPCode1"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<script src="js/jquery-1.7.1.min.js"></script>
   
<link rel="stylesheet" href="css/style.css">
    <link href="css/detail.css" rel="stylesheet" type="text/css" />
    <link href="css/cash.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../js/SqlCheck.js"></script>

    <title>申请服务机构</title>

    <script language="javascript" type="text/javascript">
        function CheckText() {
            if (labkahaoOnfocus() == false) {
                return false;
            }
            filterSql();
            return true;
		    
		}

		function isIntTel(txtStr) {
		    var validSTR = "1234567890";

		    for (var i = 1; i < txtStr.length + 1; i++) {
		        if (validSTR.indexOf(txtStr.substring(i - 1, i)) == -1) {
		            return false;
		        }
		    }
		    return true;
		}
		function labkahaoOnfocus() {
		    var labkahao = document.getElementById('txtBankCard');
		    var isInt = isIntTel(labkahao.value);
		    if (!isInt) {
		        document.getElementById('span_kahao').innerHTML = '<%=GetTran("007543","卡号只能输入数字") %>！';
		        return false;
		    }
		    document.getElementById('span_kahao').innerHTML = '';
		    return true;
		}
	    
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
                if(resultH==""||resultH==null)
                {
                    document.getElementById("txtHomeTele").value="";
                }
                else
                {
                    document.getElementById("txtHomeTele").value=resultH;
                }
            //手机
                var resultM=AjaxClass.getMemberMTele(name).value;
                if(resultM==""||resultM==null)
                {   
                    document.getElementById("txtMobileTele").value="";
                }
                else
                {
                    document.getElementById("txtMobileTele").value=resultM;
                }
            //办公室
                var resultO=AjaxClass.getMemberOTele(name).value;
                if(resultO==""||resultO==null)
                {
                    document.getElementById("txtOfficeTele").value="";
                }
                else
                {
                    document.getElementById("txtOfficeTele").value=resultO;
                }
            //传真
                var resultF=AjaxClass.getMemberFTele(name).value;
                if(resultF==""||resultF==null)
                {
                    document.getElementById("txtFaxTele").value="";
                }
                else
                {
                    document.getElementById("txtFaxTele").value=resultF;
                }
            }
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
	    function GetCCode_s2(xian)
	    {
	        var sobj = document.getElementById("<%=txtPostalCode.ClientID %>");
	        sobj.value=AjaxClass.GetAddressCode(xian).value
	    }
    </script>

    <style type="text/css">
        .xs_footer li a{display:block;padding-top:40px;background:url(../membermobile/img/shouy1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li .a_cur{color:#77c225}
.xs_footer li:nth-of-type(2) a{background:url(../membermobile/img/jiangj1.png) no-repeat center 10px;background-size:32px;}
.xs_footer li:nth-of-type(3) a{background:url(../membermobile/img/xiaoxi1.png) no-repeat center 8px;background-size:32px;}
.xs_footer li:nth-of-type(4) a{background:url(../membermobile/img/anquan1.png) no-repeat center 8px;background-size:27px;}

        .ctConPgFor
        {
            border: 1px solid #ccc;
            width: auto;
            height: 20px;
        }
        input[type='text']
        {
            width:100%;
            height:26px;
            border:1px solid #ccc;
            border-radius:3px;
            padding-left:5px
        }
        .minMsgBox ol li div div
        {
            white-space:nowrap;
            line-height:24px;
            margin-bottom:5px

        }
        #txtNumber
        {
            border:0;
        }
        select
        {
            height: 26px;
            border: 1px solid #ccc;
            border-radius: 3px;

        }
        .minMsgBox ol li span 
        {
            background:none;
           
        }
        #CountryCity1_dv_cpc span
        {
         width:25%;
            overflow:hidden

        }
        textarea
        {
            border:1px solid #ccc;
            border-radius:3px;
        }
        input[type='radio']
        {
            float:left;
            margin-top:4px;
            margin-right:3px
        }
        .minMsgBox ol li label
        {
            float:left;
        }
    </style>


</head>
<body onload="GetMemberName()">
    <form id="form1" runat="server">
         <div class="t_top">	
                    <a class="backIcon" href="javascript:history.go(-1)"></a>
                	<%=GetTran("006614","申请服务机构") %>
            </div>
                 <div class="minMsg minMsg2" style="display: block">
                <div class="minMsgBox">
                    <div>
                        <ol>
                         
                    <li>
                        <div>
                               <div>
                             <%=GetTran("007233", "申请人的会员编号")%>：
                                   </div>
                            <asp:TextBox ID="txtNumber" runat="server" CssClass="textBox" MaxLength="10" onblur="GetMemberName()"></asp:TextBox>
                                <input type="button" id="btnGetmember" value='<%=GetTran("000533", "点击获取信息")%>' onclick="GetMemberName()"
                                    style="background-color: #FFFFFF; border: 0 #FFFFFF none; margin: 0; padding: 0;
                                    display: none; color: Red;" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumber"
                                    ErrorMessage="请输入会员编号！！！" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNumber"
                                    ErrorMessage="会员编号只能为字母数字下划线，且在6~10位之间" ValidationExpression="\w{6,10}"></asp:RegularExpressionValidator>
                              </div></li>
                            <li><div>      <div>   
                           <%=GetTran("000037", "店编号")%>：   </div>
                                <asp:TextBox ID="txtStoreId" CssClass="textBox" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStoreId"
                                    ErrorMessage="请输入店编号！！！"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtStoreId"
                                    ErrorMessage="店编号只能为字母数字下划线，且在4~10位之间" ValidationExpression="\w{4,10}"></asp:RegularExpressionValidator>
                                  </div>
                                </li>
                            <li><div>     <div>     
                             <%=GetTran("000307", "推荐店的会员编号")%>： </div>
                                <asp:TextBox ID="txtDirect" CssClass="textBox" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDirect"
                                    ErrorMessage="请输入推荐您的会员编号！！！"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDirect"
                                    ErrorMessage="会员编号只能是字母数字下划线，且在6~10位之间" ValidationExpression="\w{6,10}"></asp:RegularExpressionValidator>
                                </div>
                                </li>
                            <li><div> <div>            
                       <%=GetTran("000039", "店长姓名")%>：</div>
                                <asp:TextBox ID="txtName" CssClass="textBox" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="请输入店长名！！！"></asp:RequiredFieldValidator>
                                   </div>
                                </li>
                            <li><div>  <div>      
                              <%=GetTran("000040", "店铺名称")%>：</div>
                                <asp:TextBox ID="txtStoreName" CssClass="textBox" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStoreName"
                                    ErrorMessage="请输入如店铺名称！！！"></asp:RequiredFieldValidator>
                                </div>
                                </li>
                            <li><div>   <div>            
                       <%=GetTran("000313", "店铺所在地")%></div>
                                <asp:ScriptManager runat="server" ID="ScriptManager1">
                                </asp:ScriptManager>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="StoreCountry" CssClass="zcSltBox" runat="server" OnSelectedIndexChanged="StoreCountry_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        &nbsp;<asp:DropDownList ID="StoreCity" CssClass="zcSltBox" runat="server">
                                        </asp:DropDownList>
                                        <font color="Red">*</font><%=GetTran("000314", "以后无法修改此信息")%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    </div>
                                </li>
                            <li><div style="overflow:hidden">    <div>      
                             <%=GetTran("007234", "联系人地址")%>：</div>
                                <span>
                                    <uc3:CountryCityPCode1 ID="CountryCity1" runat="server" />
                                </span><span style="width: 92.5%;float: left;margin-left: 1%;margin-top:5px">
                                    <asp:TextBox ID="txtaddress" CssClass="textBox" runat="server" MaxLength="150"></asp:TextBox></span>
                                 </div>
                                </li>
                            <li><div>    <div>        
                      <%=GetTran("000073", "邮编")%></div>
                                <asp:TextBox ID="txtPostalCode" CssClass="textBox" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPostalCode"
                                    ErrorMessage="邮编不能有字符" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                                 </div>
                                </li>
                            <li><div> <div>            
                     <%=GetTran("000319", "负责人电话")%></div>
                                <asp:TextBox ID="txtHomeTele" CssClass="textBox" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtHomeTele"
                                    ErrorMessage="电话不能有字符" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                                   </div>
                                </li>
                            <li><div> <div>            
                     <%=GetTran("000044", "办公电话")%>：</div>
                                <asp:TextBox ID="txtOfficeTele" CssClass="textBox" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtOfficeTele"
                                    ErrorMessage="电话不能有字符" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                                  </div>
                                </li>
                            <li><div> <div>           
                          <%=GetTran("000069", "移动电话")%></div>
                                <asp:TextBox ID="txtMobileTele" CssClass="textBox" runat="server" MaxLength="11"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                    ControlToValidate="txtMobileTele" ErrorMessage="电话不能有字符" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                                   </div>
                                </li>
                            <li><div> <div>         
                           <%=GetTran("000071", "传真电话")%>：</div>
                                <asp:TextBox ID="txtFaxTele" CssClass="textBox" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                    ControlToValidate="txtFaxTele" ErrorMessage="不能有字符" ValidationExpression="[0-9---(-)]{0,20}"></asp:RegularExpressionValidator>
                                     </div>
                                </li>
                            <li><div> <div>     
                               <%=GetTran("000087", "开户银行")%>：</div>
                                <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownList2" CssClass="zcSltBox" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlBank" CssClass="zcSltBox" runat="server" bgcolor="#EBF1F1">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                   </div>
                                </li>
                            <li><div> <div>       
                         <%=GetTran("000329", "银行账户")%></div>
                                <asp:TextBox ID="txtBankCard" CssClass="textBox" runat="server" MaxLength="30"></asp:TextBox>
                                 <span id="span_kahao" style="color:Red;" ></span>
                                     </div>
                                </li>
                            <li><div> <div>        
                            <%=GetTran("000330", "电子邮箱")%></div>
                                <asp:TextBox ID="txtEmail" CssClass="textBox" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="邮箱格式不正确" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                                     </div>
                                </li>
                            <li><div> <div>           
                                <%=GetTran("000332", "网址")%> ： </div>
                             <asp:TextBox ID="txtNetAddress" CssClass="textBox" runat="server" MaxLength="50"></asp:TextBox><%=GetTran("000924", "例")%>：http://www.baidu.com
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNetAddress"
                                    ErrorMessage="网址填写不正确" ValidationExpression="^(http(s)?:\/\/)?(www\.)?[\w-]+\.\w{2,4}(\/)?$"></asp:RegularExpressionValidator>
                                             
                             </div>
                                </li>
                            <li><div>   
                                        <div>    
                                 <%=GetTran("000078", "备注")%></div>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="textBox" Height="73px" TextMode="MultiLine"
                                    Width="100%" MaxLength="500"></asp:TextBox>
                                   </div>
                                </li>
                            <li><div>  <%--<div>          
                            <%=GetTran("000046", "级别")%>：</div>
                                <asp:RadioButtonList ID="rdoListLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                </asp:RadioButtonList>
                                  </div>
                                </li>
                            <li><div> --%> <div> 
                             <%=GetTran("000341", "经营面积(平方米)")%></div>
                                <asp:TextBox ID="txtFareArea" CssClass="textBox" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                    ControlToValidate="txtFareArea" ErrorMessage="输入的数据不对" ValidationExpression="^[-\+]?\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                  </div>
                                </li>
                            <li><div>   <div>
                              <%=GetTran("000342", "经营品种")%>：</div>
                                <asp:TextBox ID="txtFareBreed" CssClass="textBox" runat="server" MaxLength="150"></asp:TextBox>
                                  </div>
                                </li>
                            <li><div> <div>  
                             <%=GetTran("000343", "投资总额(万元)")%></div>
                                <asp:TextBox ID="txtTotalAccountMoney" CssClass="textBox" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                    ControlToValidate="txtTotalAccountMoney" ErrorMessage="输入数据不对" ValidationExpression="^[-\+]?\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                        
                        </div>
                    </li>
                             
                        </ol>
                        <ul>
                <li style="text-align: center; margin-top: 20px;">
                            <asp:LinkButton ID="lkSubmit" Style="display: none" runat="server" OnClientClick="return Check();"
                                Text="提交" OnClick="lkSubmit_Click"></asp:LinkButton>&nbsp;
                            <input class="anyes" id="bSubmit" realvalue="0" onclick="CheckText()" type="button"
                                value="<%=GetTran("000351", "提 交")%>" style="width:98%;height:30px;background:#77c225;color:#fff;border-radius:3px;margin-left:1%" />
                            <asp:Button ID="btnAdd" runat="server" Text="提交" OnClick="btnAdd_Click" CssClass="anyes"
                                Style="display: none"></asp:Button>
                            &nbsp;&nbsp;<asp:Button ID="btnblock" runat="server" Text="返回" CssClass="anyes" OnClick="btnblock_Click"
                                Visible="false" /></li></ul>  
                    </div>
                </div>
            </div>
 <!-- #include file = "comcode.html" -->


    <div class="MemberPage" style="display:none" >
        <uc1:top runat="server" ID="top" />
         
        <div class="centerCon">
            <!--内容,右下背景-->
            <div class="centConPage">
                <!--表单-->
                <div class="ctConPgCash">
                    <div class="CashH2"><h1><span class="CashTitle" style="width:755px;"><%=GetTran("007544","注册服务机构")%></span></h1><span class="CashTitleR"></span></div>
                    <table width="705" border="0" cellspacing="1" cellpadding="0">
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("007233", "申请人的会员编号")%>：
                            </th>
                            <td style="white-space: nowrap">
                                
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000037", "店编号")%>：
                            </th>
                            <td style="white-space: nowrap">
                            
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000307", "推荐店的会员编号")%>：
                            </th>
                            <td style="white-space: nowrap">
                                 
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000039", "店长姓名")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                                
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000040", "店铺名称")%>：
                            </th>
                            <td style="white-space: nowrap">
                                  
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000313", "店铺所在地")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                             
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("007234", "联系人地址")%>：
                            </th>
                            <td style="white-space: nowrap">
                               
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000073", "邮编")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                                
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                登陆语言：
                            </th>
                            <td style="white-space: nowrap">
                                 登陆语言：
                                <asp:DropDownList ID="ddlLanaguage" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                币种：
                            </th>
                            <td style="white-space: nowrap">
                                <asp:DropDownList ID="Currency" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000319", "负责人电话")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                                   
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000044", "办公电话")%>：
                            </th>
                            <td style="white-space: nowrap">
                               
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000069", "移动电话")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                                
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000071", "传真电话")%>：
                            </th>
                            <td style="white-space: nowrap">
                              
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000087", "开户银行")%>：
                            </th>
                            <td style="white-space: nowrap">
                            
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000329", "银行账户")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                               
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000330", "电子邮箱")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                              
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000332", "网址")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                               
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <th align="right" bgcolor="#EBF1F1">
                                <p align="right">
                                    <%=GetTran("000042", "办店期数")%></p>
                            </th>
                            <td>
                                   <asp:DropDownList ID="DropDownList1" runat="server" Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000078", "备注")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                               
                            </td>
                        </tr>
                      <%--  <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000046", "级别")%>：
                            </th>
                            <td style="white-space: nowrap">
                                 
                            </td>
                        </tr>--%>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000341", "经营面积(平方米)")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                                  
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" class="style1" bgcolor="#EBF1F1">
                                <%=GetTran("000342", "经营品种")%>：
                            </th>
                            <td style="white-space: nowrap" class="style1">
                               
                            </td>
                        </tr>
                        <tr>
                            <th align="right" style="white-space: nowrap" bgcolor="#EBF1F1">
                                <%=GetTran("000343", "投资总额(万元)")%>
                                ：
                            </th>
                            <td style="white-space: nowrap">
                                 
                            </td>
                        </tr>
                    </table>
                    <ul>
                        
                    </ul>
                    <div style="clear: both">
                    </div>
                </div>
                <div class="ctConPgList-3">
                    <h1>
                        <%=GetTran("000033", "说 明")%>：
                    </h1>
                    <p>
                        <%=GetTran("000347", "店铺密码：如果开店的会员有证件号则是证件号的后六位，反之就是自己的会员编号.")%></p>
                </div>
            </div>
        </div>
        <div style="clear: both">
        </div>
        <uc2:bottom runat="server" ID="bottom" />
        <!--页面内容结束-->
    </div>
    </form>
    <%=msg%>
</body>
</html>
