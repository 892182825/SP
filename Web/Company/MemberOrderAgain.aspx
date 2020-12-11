<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberOrderAgain.aspx.cs"
    Inherits="Store_MemberOrderAgain" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/CountryCityPCode.ascx" TagName="CountryCity" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GetTran("001174", "复消报单")%></title>

    <script language="javascript">
    //产品树的影藏和显示
		function menu( menu,img,plus )
		{			
			if( menu.style.display == "none")
			{
				menu.style.display="";
				img.src="images/foldopen.gif";
				plus.src="images/MINUS2.GIF";		
			}
			else
			{
				menu.style.display = "none";
				img.src="images/foldclose.gif";
				plus.src="images/PLUS2.GIF";				
			}
			
		}	
                
		function GetCCode_s2(city)
		{
		    var sobj = document.getElementById("<%=Txtyb.ClientID %>");
		    sobj.value=AjaxClass.GetAddressCode(city).value
		}
		
         function Bind()//绑定树旁边的表格
		 {
		    debugger;
			var divPro = document.getElementById('product');
			var pId="";
			var productid="";
			for(var i=0;i<document.getElementById('menuLabel').getElementsByTagName('input').length;i++)
			{
				if(document.getElementById('menuLabel').getElementsByTagName('input').item(i).getAttribute("type")=="text")
				{
				    var numx=document.getElementById('menuLabel').getElementsByTagName('input').item(i).value;
				    var num=Number(document.getElementById('menuLabel').getElementsByTagName('input').item(i).value);
				    if(numx!=num)//验证输入产品数量是否是数字
				    {
				        alert('<%=GetTran("000239","产品数字只能为数字!") %>');
				        document.getElementById('menuLabel').getElementsByTagName('input').item(i).value=0;
				    }
				    if(num>0)//数量大于0记录产品
				    {
				    	pId+=document.getElementById('menuLabel').getElementsByTagName('input').item(i).value+",";
					    productid+=document.getElementById('menuLabel').getElementsByTagName('input').item(i).name+",";
					}
				}
			}

			
			var storeid= document.getElementById('TxtStore').value;
			
			var curr=document.getElementById("DropCurrency").value;
			
			divPro.innerHTML=AjaxClass.DataBindTxt(pId,storeid,productid,"tablemb","",curr).value;//更新产品表格记录
			
		 }
		  window.onload =function(){
		  try
		  {
		 Bind();
		 }
		 catch(e)
		 {}
		 }		
		//联系电话验证
		 function CheckMobileTele()
		 {
		    var tele=document.getElementById("Txtyddh").value;
		    if(tele=="" || tele==null)
		    {
		        alert('<%=GetTran("000269","收货人电话不能为空!") %>');
		        return;
		    }
		    else
		    {
		        //判断输入的手机号格式是否正确
			    var isInt = isShuZi(tele);
			    if(isInt)
			    {
			        alert('<%=GetTran("006559","移动电话必须是半角数字组成的!") %>');
				    return;
			    }
			    else if(tele.length!=11)
			    {
			        alert('<%=GetTran("006560","移动电话必须是11位的！") %>');
				    return;
			    }
		    }
		 }
		 
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
	    
	    function ShowProductDiv(sender,pid)
        {
            //弹出层
            document.getElementById("divShowProduct").style.display = "block";
            var leftpos = 0,toppos = 0;
            var pObject = sender.offsetParent;
            if (pObject)
            {
                leftpos += pObject.offsetLeft;
                toppos += pObject.offsetTop;
            }
            while(pObject=pObject.offsetParent )
            {
                leftpos += pObject.offsetLeft;
                toppos += pObject.offsetTop;
            };

            document.getElementById("divShowProduct").style.left = (sender.offsetLeft + leftpos) + "px";
            document.getElementById("divShowProduct").style.top = (sender.offsetTop + toppos + sender.offsetHeight - 2) + "px";
            
           //显示树信息
           document.getElementById("divShowProduct").innerHTML="";
		   
           if(pid=="")
           {
                document.getElementById("divShowProduct").style.display="none";
                return;                        
           }
           else
           {
               var result=AjaxClass.GetProductDetail(pid).value;
               document.getElementById("divShowProduct").innerHTML=result;
           }

        }
        function HideProductDiv(sender)
        {
            document.getElementById("divShowProduct").style.display = "none";
        }
    </script>

    <style>
        .style3
        {
            width: 150px;
            background-color: #EBF1F1;
        }
        .bjkk{
	        border: 3px solid #C2DEE8;
        }
        .bjkk2{
	        border: 1px solid #C2DEE8;
	        padding: 3px;
        }
        .thbt {
	        font-size: 12px;
	        font-weight: bold;
	        line-height: 22px;
	        background-color: #EDF5F8;
	        text-indent: 8px;
        }
        .bzbt {
	        font-size: 12px;
	        font-weight: bold;
	        line-height: 22px;
        }
        .biaozzi {
	        font-family: "宋体";
	        font-size: 12px;
	        line-height: 22px;
	        text-decoration: none;
        }
        .smbiaozzi {
	        font-family: "宋体";
	        font-size: 12px;
	        text-decoration: none;
	        line-height: 18px;
    </style>
    <link rel="Stylesheet" href="CSS/Company.css" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <div align="center">
        <table cellspacing="0" cellpadding="10" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" class="tablemb">
                        <tr height="25">
                            <td height="25" align="left">
                                <font face="宋体"><strong></strong></font>
                                <%=GetTran("001176", "可报单金额")%>：
                                <asp:Label ID="lblMoney" runat="server" Text="Label"></asp:Label>
                            </td>
                            <asp:Label ID="messageLabel" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></tr>
                    </table>
                    <table id="Table1" width="100%" style="border-left: 1px solid #88E0F4; border-right: 1px solid #88E0F4;
                        border-bottom: 1px solid #88E0F4;" bgcolor="#FAFAFA">
                        <tr width="30%">
                            <td valign="top" bgcolor="white">
                                <asp:Panel ID="Product1" runat="server" Visible="False">
                                    <div>
                                        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0" class="biaozzi">
                                            <tr>
                                                <td>
                                                    <%=GetTran("001185", "请输入金额")%>：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="GroupMoney" runat="server" ForeColor="#000040"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%=GetTran("000414", "积分")%>：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="GroupPv" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Product2" runat="server">
                                    <div class="biaozzi" align="left">
                                        <asp:Label ID="menuLabel" runat="server"></asp:Label>
                                        <div id="divShowProduct" style="position: absolute; border-right: #a5a5a5 1px solid;
                                            padding-right: 0px; border-top: #a5a5a5 1px solid; padding-left: 0px; padding-top:0px;
                                            border-left: #a5a5a5 1px solid; width: 302px; height:164px;border-bottom: #a5a5a5 1px solid;
                                            background-color: #ffffff; display: none; overflow-y:hidden;overflow:hidden; text-align:center;" onmouseover='this.style.display="block";if(navigator.appName=="Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE6.0"){for(var i=0; i<document.getElementsByTagName("SELECT").length;i++)document.getElementsByTagName("SELECT")[i].style.visibility="hidden";}'
                                            onmouseout='this.style.display="none";if(navigator.appName=="Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE6.0") { for(var i=0; i<document.getElementsByTagName("SELECT").length;i++) document.getElementsByTagName("SELECT")[i].style.visibility="visible"; }'></div>
                                    </div>
                                </asp:Panel>
                            </td>
                            <td valign="top" height="100">
                                <table class="biaozzi" bgcolor="#FAFAFA" cellpadding="0" cellspacing="0" border="0"
                                    style="width: 100%;">
                                    <tr>
                                        <td valign="bottom" colspan="2" align="left">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<img height="40" src="../images/xchuo.gif" width="40">
                                            <span style="font-weight: bold;">购物车</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" colspan="2">
                                            <div id="product" runat="server" style="vertical-align: top; width: 100%; text-align: center;">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table class="biaozzi" bgcolor="#FAFAFA" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td valign="bottom" colspan="2" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="../images/men01.gif" style="height: 34px;
                                                        width: 33px">
                                                    <span style="font-weight: bold;">
                                                        <%=this.GetTran("002165", "基本信息")%></span>
                                                </td>
                                            </tr>
                                            <tr height="25" align="left">
                                                <td align="right" class="style3">
                                                    <%=GetTran("001195", "编号")%>：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txtbh" runat="server" Width="150px" MaxLength="10" OnTextChanged="Txtbh_TextChanged"
                                                        AutoPostBack="True" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="display: none" height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("001188", "复消店铺")%>：
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TxtStore" runat="server" Width="150px" MaxLength="20" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="style3">
                                                    <%=GetTran("001198", "收货姓名")%>：
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="Txtxm" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" class="style3" valign="top">
                                                    <%=GetTran("000112", "收货地址")%>：
                                                </td>
                                                <td align="left">
                                                    <asp:Panel ID="panel1" runat="server">
                                                        <asp:RadioButtonList ID="rbtAddress" runat="server"  Font-Size="12px" 
                                                            AutoPostBack="True" onselectedindexchanged="rbtAddress_SelectedIndexChanged"></asp:RadioButtonList>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panel2" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <uc1:CountryCity ID="CountryCity1" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="Txtdz" runat="server" Width="152px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("000114", "邮政编码")%>：
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="Txtyb" runat="server" Width="150px" MaxLength="6"></asp:TextBox>
                                                    <font face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("001203", "收货电话")%>：
                                                </td>
                                                <td align="left">
                                                    <font face="宋体">
                                                        <asp:TextBox ID="Txtjtdh" runat="server" Width="150px"></asp:TextBox></font>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("000069", "移动电话")%>：
                                                </td>
                                                <td align="left">
                                                    <font>
                                                        <asp:TextBox ID="Txtyddh" runat="server" Width="150px" onblur="CheckMobileTele()"></asp:TextBox></font>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="right" class="style3">
                                                    <%=GetTran("001345","发货方式") %>：
                                               </td> 
                                               <td align="left">
                                                    <asp:DropDownList ID="DDLSendType" runat="server" Width="100px">
                                                        <asp:ListItem Value="0" Selected="True">公司发货到店铺</asp:ListItem>
                                                        <asp:ListItem Value="1">公司直接发给会员</asp:ListItem>
                                                    </asp:DropDownList>
                                               </td>
                                            </tr>
                                            <tr style="display: none" height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("001532", "Email")%>：
                                                </td>
                                                <td align="left">
                                                    </FONT>
                                                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("000132", "运输方式")%>：
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddth" runat="server" Width="100px">
                                                        <asp:ListItem Value="1">自提</asp:ListItem>
                                                        <asp:ListItem Value="2">邮寄</asp:ListItem>
                                                        <asp:ListItem Value="6">其它</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("001535", "提货费用")%>：
                                                </td>
                                                <td align="left">
                                                    0
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("000185", "支付币种")%>：
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="DropCurrency" runat="server" onChange="Bind();" Width="100px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" class="style3">
                                                    <%=GetTran("000186", "支付方式")%>：
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="rdPayType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Ddzf_SelectedIndexChanged"
                                                        RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="dzpanel" runat="server" Visible="false">
                                                <tr height="25">
                                                    <td align="right" class="style3">
                                                        <%=GetTran("001537", "用户编号")%>：
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtdzbh" runat="server" Width="100"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr height="25">
                                                    <td align="right" class="style3">
                                                        <%=GetTran("001538", "电子账户密码")%>：
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtdzmima" runat="server" Width="100" TextMode="Password"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td align="right" class="style3">
                                                    <%=GetTran("000078", "备注")%>：
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="Txtbz" runat="server" Width="352px" TextMode="MultiLine" Columns="20"
                                                        Height="88px"></asp:TextBox><br>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    &nbsp;
                                                    <asp:Button ID="go" runat="server" Width="63px" Text="确 定" OnClick="go_Click" CssClass="anyes">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" style="display: none">
            <tr>
                <td>
                    <img height="2" src="../images/common/spacer.gif" width="1">
                </td>
            </tr>
            <tr>
                <td>
                    <img height="10" src="../images/common/spacer.gif" width="1">
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    </form>
</body>
</html>
