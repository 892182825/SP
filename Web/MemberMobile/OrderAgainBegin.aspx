<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderAgainBegin.aspx.cs" Inherits="Member_OrderAgainBegin" EnableEventValidation="false"%>

<%@ Register Src="../UserControl/CountryCityPCode.ascx" TagName="CountryCityPCode"
    TagPrefix="uc1" %>
    <%@ Register Src="../UserControl/CountryCity.ascx" TagName="CountryCity" TagPrefix="uc1" %>
<%@ Register src="../UserControl/SearchPlacement_ThreeLines.ascx" tagname="SearchPlacement_ThreeLines" tagprefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员复消</title>
 
    <script src="../javascript/cardAndElcCard.js"></script>

    <script src="../javascript/My97DatePicker/WdatePicker.js"></script>

   

    <script src="../JS/jquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function isTel(txtStr)
	    {
	        var validSTR="1234567890-#*";

		    for(var i=1;i<txtStr.length+1;i++)
		    {
		        //alert(validSTR.indexOf(txtStr.substring(i-1,i)));
			    if (validSTR.indexOf(txtStr.substring(i-1,i))==-1)
			    {				    
				    return false;
			    }
		    }
		    return true;
	    }  
                    var dlpr;
            function vld(eletxt,elein,info,err,fmer)
       {
               //姓名失去光标
$(eletxt).blur(function(){ 
         var restr=/\W+/;
         var renum=/\D+/;
         var reem=/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
   
 $(elein).css("display","block"); 
if($(this).attr("value") !="")
{ 
 
 // 姓名 昵称 店铺 证件
  if(eletxt=="#Txtxm"||eletxt=="#Txtlm"||eletxt=="#txtStore")
  { 
      if(restr.test($(this).attr("value")))
      {
          $(elein).attr("class","bgimger");
         $(elein).html(fmer);
        
         return ;
      }
  }else
  if( eletxt=="#Txtzj")
   {
 
    dlpr=document.getElementById("dplCardType");
    
   if(dlpr.selectedIndex!=0){
    if($(this).attr("value").length<6|| restr.test($(this).attr("value")))
      {
          $(elein).attr("class","bgimger");
         $(elein).html(fmer);
        
         return ;
      }
      if(dlpr.selectedIndex==1){
       var result = AjaxClass.VerifyPaperNumber($(this).attr("value")).value;
          if(result.indexOf(",") <= 0)
        {
          $(elein).attr("class","bgimger");
         $(elein).html("身份证号非法");
         return  ;
         }     
        }
      }
   }
  else 
  if(eletxt=="#Txtyddh") // 电话
  { 
      if(renum.test($(this).attr("value"))||$(this).attr("value").length!=11)
      {
           $(elein).attr("class","bgimger");
        $(elein).html(fmer);
         return ;
      }
  }else 
  if(eletxt=="#txtOtherPhone") // 电话
  { 
      if(isTel($(this).attr("value"))==false || $(this).attr("value").length>12)
      {
           $(elein).attr("class","bgimger");
        $(elein).html(fmer);
         return ;
      }
  }else 
  if(eletxt=="#Email")// 邮箱
  {

      if(!reem.test($(this).attr("value")))
      {
           $(elein).attr("class","bgimger");
         $(elein).html(fmer);
         return ;
      }
  }  
  
    if(eletxt=="#Txtyb")// 邮箱
  {

      if(renum.test($(this).attr("value"))||$(this).attr("value").length!=6)
      {
           $(elein).attr("class","bgimger");
         $(elein).html(fmer);
         return ;
      }
  } 
  if(eletxt=="#Txttj")
  {alert(2);
     $("#SearchPlacement_ThreeLines1_Txttj").attr("value",$(this).attr("value"));
  }
  
 $(elein).attr("class","bgimgok");
  $(elein).html("完成");
   
 }
else
{ 
  if(eletxt=="#Txtyb"){
    $(elein).attr("class","bgimgin");
     $(elein).css("display","block");
     $(elein).html(info);
  }else{
  
   $(elein).attr("class","bgimger");
   $(elein).html(err);
   
}

 }

});
//姓名获取光标
$(eletxt).focus(function(){
  $(elein).attr("class","bgimgin");
 $(elein).css("display","block");
 $(elein).html(info);
 if(eletxt=="#txtBirthDate") WdatePicker();
});
       }
       function  subform(){
       var restr=/\W+/;
       var renum=/\D+/;
       var reem=/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
   
    $("#Txtbh").blur();
         if($("#Txtbh").attr("value")==""||restr.test($("#Txtbh").attr("value"))||$("#Txtbh").attr("value").length<6)return false;
     
          
              $("#Txtyddh").blur();
               if($("#Txtyddh").attr("value")==""||renum.test($("#Txtyddh").attr("value")))return false;
             
             $("#txtOtherPhone").blur();
                    if($("#txtOtherPhone").attr("value")!="")
                    {
                        if(!isTel($("#txtOtherPhone").attr("value")) || $("#txtOtherPhone").attr("value").length>12)
                        {
                            return false;
                        }
                    }
//                 $("#Txtsb").blur();
//                      if($("#Txtsb").attr("value")==""||restr.test($("#Txtsb").attr("value")))return false;
                    //alert($("#rbtAddress input[type=radio]:checked").val());
                 if($("#rbtAddress input[type=radio]:checked").val()=="新地址")
                   {
                        if($("#CountryCity2_ddlCountry").val()=="请选择" || $("#CountryCity2_ddlP").val()=="请选择" || $("#CountryCity2_ddlCiTy").val()=="请选择")
                    {
                        alert("请选择收货地址！");
                        return false;
                    }
                        
                        if($("#Txtdz").val()=="")
                        {
                           $("#Txtdz").blur();
                            alert("详细地址必填！");
                            return false;
                        }
                   }
                   
                    if(!$("#panel2").is(":hidden"))
                    {
                        if($("#CountryCity2_ddlCountry").val()=="请选择" || $("#CountryCity2_ddlP").val()=="请选择" || $("#CountryCity2_ddlCiTy").val()=="请选择")
                        {
                            alert("请选择收货地址！");
                            return false;
                        }
                        
                        if($("#Txtdz").val()=="")
                        {
                           $("#Txtdz").blur();
                            alert("详细地址必填！");
                            return false;
                        }
                    }
                    
                 return true;
                 
        }
        
         function SetAZText()
            {
                if(document.getElementById('Txttj').value!="")
                                
//                    
                    document.getElementById('Txtsb').readOnly=false;                        
                
                document.getElementById('spanTjtxt').innerHTML = "";
            }
            
             function GetAzNumber()
              {
            var number = document.getElementById('Txtsb').value;
            if(number != "")
            {
                document.getElementById('HidAz').value = number;
            }
              }
           //判断电话号码
	    
 
       window.onload =function()
        {
//        var dlpr=document.getElementById("dplCardType");
//       // alert(document.getElementById("dplCardType").options);
//        alert(dlpr.options[dlpr.selectedIndex].value);
       vld("#Txtbh","#number_error","请输入购货服务机构","购货服务机构不可以为空","购货服务机构格式输入错误"); //姓名验证
         vld("#Txtxm","#name_error","请输入姓名","姓名不可以为空","姓名只允许输入中文字母数字下划线"); //姓名验证
          vld("#Txtlm","#nick_error","请输入昵称","昵称不可以为空","昵称只允许输入中文字母数字下划线");  //昵称验证
          
           vld("#txtStore","#store_error","请输入购物店铺","购物店铺不可以为空","购货店铺只允许输入中文字母数字下划线");  //店铺验证
          
            vld("#Txtzj","#paper_error","请输入证件号","证件号不可以为空","证件号只允许输入字母数字，且必须大于6位");  //证件号验证
            vld("#txtBirthDate","#birthday_error","请输入生日","生日不可以为空","");  //生日验证
            
            vld("#Txtyddh","#phone_error","请输入移动电话","移动电话不可以为空","移动电话只允许输入数字,且必须为11位");  //电话验证
            
             vld("#Email","#email_error","请输入邮箱地址","邮箱地址不可以为空","邮箱格式不正确");  //电话验证
             
             vld("#Txtyb","#post_error","请输入邮政编码","邮政编码不可以为空","邮件格式不正确，必须为6位数字");  //电话验证
             vld("#Txtdz","#address_error","请输入详细地址","详细地址不可以为空","详细地址格式不正确");  //推荐验证
             
              vld("#Txttj","#direct_error","请输入推荐编号","推荐编号不可以为空","推荐编号格式不正确");  //推荐验证
              
              vld("#txtOtherPhone","#otherPhone_error","请输入其他电话","其他电话选填","其他电话只能输入*，-，#或数字并且不能超过12位");
             
//              vld("#Txtsb","#plment_error","请输入安置编号","安置编号不可以为空","安置编号格式不正确");  //安置验证
    
   
                $("#rbtAddress").click(function()
                {
                    //alert($("#rbtAddress input[type=radio]:last").val());
                    //alert($("#rbtAddress input[type=radio]:checked").val());
                   if($("#rbtAddress input[type=radio]:checked").val()=="新地址")
                   {
                        
                        $("#panel2").css("display","");
                   }
                   else
                   {
                    $("#panel2").hide();
                   }
                
                })

//	          try
//	          {
//	        
//                 ShowStore();
//	             GetTxtColor();
//	           // Bind();
//	             change();
//	             GetBankUser();
//	             elcCardConsume();
//	            // Check();
//	          }
//	         catch(e)
//	         {}
	     }
        
           
        
        	function GetCCode_s2(city)
		{
		    var sobj = document.getElementById("Txtyb");
		    sobj.value=AjaxClass.GetAddressCode(city).value
		}
		
        window.onerror=function()
        {
            return true;
        }
    </script>

    <style type="text/css">
        .anyes {
	font-size: 12px;
	color: #FFFFFF;
	text-decoration: none;
	background-image: url(images/anliudp000.GIF);
	background-repeat: repeat-x;
	height: 22px;
	border: 1px solid #132022;
	cursor:pointer;
	font-family: Arial, Helvetica, sans-serif;
}
        <!
        --
     
         .bjkk
        {
            border: 3px solid #C2DEE8;
        }
        .bjkk2
        {
            border: 1px solid #C2DEE8;
            padding: 3px;
        }
        .thbt
        {
            font-size: 12px;
            font-weight: bold;
            line-height: 22px;
            background-color: #EDF5F8;
            text-indent: 8px;
        }
        .bzbt
        {
            font-size: 12px;
            font-weight: bold;
            line-height: 22px;
        }
        .biaozzi
        {
            font-family: "宋体";
            font-size: 12px;
            line-height: 22px;
            text-decoration: none;
        }
        .smbiaozzi
        {
            font-family: "宋体";
            font-size: 12px;
            text-decoration: none;
            line-height: 18px;
        }
      
       
        .style5
        {
            width: 100px;
        }
        .style6
        {
            width: 96px;
        }
        .bk2010
        {
            font-size: 12px;
            background-image: url(images/dp2010.gif);
            background-repeat: no-repeat;
            height: 100px;
            width: 100px;
        }
        .main_div
        {
        	 margin:auto; width:950px; font-size:14px;
        	}
        	.inpst
        	{ float:left;
        	   border:1px solid #ccc;   height:26px; margin:4px;  width:180px; padding-top:4px; padding-left:2px;
        		}
        .bgimgok{   float:left;     background: url("../images/ok.png") no-repeat scroll 0 11px transparent;
    color: #48A309;  line-height:30px; padding-top:3px;
    padding-left: 14px;}
        .bgimger{   float:left; background: url("../images/error.png") no-repeat scroll 0 11px transparent;
    color: #FF6666;  line-height:30px; padding-top:3px;
    padding-left: 14px;}
        .bgimgin{  float:left;  background: url("../images/infm.png") no-repeat scroll 0 11px transparent;
    color: #999;  line-height:30px; padding-top:3px;
    padding-left: 14px;}
    </style>
    <link rel="Stylesheet" href="CSS/company.css" type="text/css" id="cssid" />

    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

</head>
<body>
    <form id="Form1" onsubmit="" method="post" runat="server">
   
   <center>
   <div  class="main_div" >
 
     <div style=" text-align:center;  color: #555555; font-weight:bold; font-family:华文宋体; font-size: 20px; margin-top:20px;  "> 复消报单 </div>
  
               
                <table id="table1" cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;<img  src="../images/cartnav-step01.gif"  />
                        </td>
                    </tr>
                    <tr style="width: 30%">
                      
                        <td valign="top">
                            <table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="#ffffff"
                                class="biaozzi">
                              
                                    
                                <tr>
                                    <td valign="bottom" colspan="2" align="left">
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;<img src="../images/men01.gif" style="height: 34px; width: 33px">
                                        <b>
                                            <%=this.GetTran("002165", "基本信息")%></b> <span><font color="Gray">｜为使电脑识别及公司规范管理，请准确填写。</font></span>
                                        <span style="color: Red">（<%=this.GetTran("003166", "注")%>：*<%=this.GetTran("003170", "为必填项")%>）</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="15px">
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td align="right" style="width: 25%; text-overflow: ellipsis; word-break: keep-all;
                                        overflow: hidden;">
                                        <span style="color: Red">*</span>购货服务机构：
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txtbh"  CssClass="inpst"  MaxLength="10" runat="server" ></asp:TextBox>
                                            <span  style="display:none;" class="bgimgin"   id="number_error">
                                           </span>
                                        &nbsp;&nbsp;<span id="divBtn" runat="server" visible="false">
                                            <input name="button" type="button" class="anyes" onclick="ChangeNumber()" value='换一个' /></span><input
                                                type="hidden" runat="server" id="HidBh" />
                                        <span style="display:none;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Txtbh"
                                                ErrorMessage="会员编号不能为空" ValidationGroup="2"></asp:RequiredFieldValidator><span id="labNumber"
                                                    style="color: Red"></span> </span>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">
                                        <span style="color: Red">*</span><%=GetTran("000069", "移动电话")%>：
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="Txtyddh" CssClass="inpst" runat="server"   MaxLength="11"></asp:TextBox>
                                        <span  style="display:none;" class="bgimgin"   id="phone_error">
                                           </span>
                                         
                                    </td>
                                </tr>
                               <tr>
                                    <td align="right">
                                        <span style="color: Red">&nbsp;</span>其他电话：
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtOtherPhone" CssClass="inpst" runat="server"   MaxLength="30"></asp:TextBox>
                                        <span  style="display:none;" class="bgimgin"   id="otherPhone_error">
                                           </span>
                                         
                                    </td>
                                </tr>
                                 
                               <tr>
                                    <td align="right">
                                        <span style="color: Red">&nbsp;</span><%=GetTran("000526", "运货方式")%>：
                                    </td>
                                    <td align="left">
                                         <asp:DropDownList ID="ddth" CssClass="inpst" runat="server" Width="180px">
                                        <asp:ListItem Value="2">邮寄</asp:ListItem>
                                            <asp:ListItem Value="1">自提</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                         
                                    </td>
                                </tr>
                              
                               <tr>
                                    <td align="right">
                                        <span style="color: Red">&nbsp;</span><%=GetTran("001345","发货方式") %>：
                                    </td>
                                    <td align="left">
                                         <asp:DropDownList ID="DDLSendType" runat="server" Width="180px" style="margin-left:3px;">
                                        <asp:ListItem Value="0" Selected="True">公司发货到店铺</asp:ListItem>
                                        <asp:ListItem Value="1">公司直接发给会员</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                </tr>
                                 
                               <tr style="display:none;">
                                    <td align="right">
                                        <span style="color: Red">&nbsp;</span>运费：
                                    </td>
                                    <td align="left">
                                         0
                                    </td>
                                </tr>
                            <tr height="25">
                                                <td align="right">
                                                    <%=GetTran("000112", "收货地址")%>：
                                                </td>
                                                <td align="left">
                                                
                                               
                                                
                                                    <asp:Panel ID="panel1" runat="server">
                                                        <asp:RadioButtonList ID="rbtAddress" runat="server" Font-Size="12px" AutoPostBack="false"
                                                            OnSelectedIndexChanged="rbtAddress_SelectedIndexChanged">
                                                        </asp:RadioButtonList>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panel3" runat="server">
                                                        <table id="panel2" style="display:none;" runat="server">
                                                            <tr>
                                                                <td>
                                                                   <uc1:CountryCityPCode style="width:250px" ID="CountryCity2" runat="server" /> 
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txtdz" runat="server" Width="152px" MaxLength="120"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    
                                                </td>
                                            </tr>
                                
                             
                                
                                <tr>
                                    <td id="plc" align="left" valign="middle" colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <div id="Divt1" style="margin-left: 50px; display: none; overflow-y: hidden; overflow: auto;
                                                        width: 650px; height: 280px;" align="center">
                                                    </div>
                                                    <div id="divShowView" style="position: absolute; border-right: #a5a5a5 1px solid;
                                                        padding-right: 10px; border-top: #a5a5a5 1px solid; padding-left: 15px; padding-bottom: 12px;
                                                        border-left: #a5a5a5 1px solid; width: 500px; height: 280px; padding-top: 12px;
                                                        border-bottom: #a5a5a5 1px solid; background-color: #ffffff; display: none; overflow-y: hidden;
                                                        overflow: auto; text-align: center;" onmouseover='this.style.display="block";if(navigator.appName=="Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE6.0"){for(var i=0; i<document.getElementsByTagName("SELECT").length;i++)document.getElementsByTagName("SELECT")[i].style.visibility="hidden";}'
                                                        onmouseout='this.style.display="none";if(navigator.appName=="Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE6.0") { for(var i=0; i<document.getElementsByTagName("SELECT").length;i++) document.getElementsByTagName("SELECT")[i].style.visibility="visible"; }'>
                                                    </div>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                               
                               
                               
                                <tr align="center">
                                    <td align="center" colspan="2"   style="height:40px;">
                                       
                                        <asp:Button ID="btnsubmitreg" runat="server" Text="下一步" CssClass="anyes" OnClick="Button1_Click" OnClientClick="return subform();"     />
                                        
                                    
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
     
    <div id="Dealdiv" runat="server">
        <font face="宋体"></font>
    </div>
    <asp:Label ID="txt_jsLable" runat="server"></asp:Label><input type="hidden" id="saveStore" />
    </div>
    </center>
    </form>
</body>
</html>

