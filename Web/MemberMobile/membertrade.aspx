<%@ Page Language="C#" AutoEventWireup="true" CodeFile="membertrade.aspx.cs" Inherits="Member_membertrade"
    EnableEventValidation="false" %>


<%@ Register Src="~/UserControl/MemberTop.ascx" TagName="top" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/MemberBottom.ascx" TagName="bottom" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/CountryCityPCode.ascx" TagName="CountryCity" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        </title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/shopcart.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../js/SqlCheck.js"></script>
    <script src="../js/fuxiaojs.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
   
		  window.onload =function(){
		   
		         Bind();
		    
		 }		
		 function menu( menu,img,plus )
	{			
		if( menu.style.display == "none")
		{
			menu.style.display="";
			img.src="../company/images/foldopen.gif";
			plus.src="../company/images/MINUS2.GIF";		
		}
		else
		{
			menu.style.display = "none";
			img.src="../company/images/foldclose.gif";
			plus.src="../company/images/PLUS2.GIF";				
		}		
	}	
		 function getwebpro(){
		   var  pm1='<%=Txtyb.ClientID %>';
		   var pm2='<%=GetTran("000239","产品数字只能为数字!") %>';
		   var pm3='<%=GetStoreId()%>';
		   var pm4='<%=GetTran("006559","移动电话必须是半角数字组成的!") %>';
		   var pm5='<%=GetTran("006560","移动电话必须是11位的！") %>';
		   var pm6='<%=GetTran("006889","移动电话不能为空！") %>';
		   var pm7='<%=GetTran("006819","邮编必须是半角数字组成的！") %>';
		   var pm8='<%=GetTran("006820","邮编必须是6位！") %>';
		   var pm9='<%=this.GetTran("006928","收货电话只能输入*，-，#或数字！") %>';
		   return {txtyb:pm1,pdnum:pm2,stid:pm3,pon:pm4,phle:pm5,phnul:pm6,
		     posn:pm7,posl:pm8,phg:pm9};
		 }

		function CheckStoreId()
		{
		    var storeId = document.getElementById("TxtStore").value;
		    if(storeId=="")
		    {
		        alert('<%=GetTran("006026","店铺编号不能为空！") %>');
		        //alert('<%=GetTran("006026","店铺编号不能为空！") %>');
		        return;
		    }
		    var returnValue=AjaxClass.CheckStoreID(storeId).value;
		    if(returnValue==false)
		    {
		        alert('<%=GetTran("006817","店铺编号不存在，请重新输入！") %>');
		        //alert('<%=GetTran("006817","店铺编号不存在，请重新输入！") %>');
		        return;
		    }
		}
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="MemberPage">
        <uc1:top runat="server" ID="top" />
        <div class="shopCen">
	<!---->
	        <div class="shopCenC">
    	        <h1 class="shopCartTitle"><%=GetTran("006981","购物车")%></h1>
    	         <div id="product" runat="server" style="vertical-align: top; width: 100%; text-align: center;"> </div>
            </div>
            
	        <!--左侧-->
	        <div class="shopCenL">
    	        <h1><%=GetTran("000542", "产品列表")%></h1>
                <ul>
        	        <li>
        	         <asp:HiddenField ID="hidpids" runat="server" />
        	          <asp:Label ID="menuLabel" runat="server"></asp:Label>
        	        </li>
                </ul>
            </div>
            
            <!--右侧-->
            <div class="shopCenR">
   	          <h1 class="shopCenRtlBg"><%=GetTran("007533", "填写订货信息")%></h1>
                 <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <th style=" display: none">
                                <%=GetTran("000024", "会员编号")%>：<asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>
                            </th>
                            <td  style="display: none">
                                <%=GetTran("000025", "会员姓名")%>：<asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                            </td>
                            <asp:Label ID="messageLabel" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label>
                        </tr>
                        
                        <tr>
                            <th >
                                <%=GetTran("001770", "购货店铺")%>：
                            </th>
                            <td align="left">
                                <asp:TextBox ID="TxtStore" runat="server" CssClass="shopCenRform" MaxLength="20"   onblur="CheckStoreId()" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <%=GetTran("000069", "移动电话")%>：
                            </th>
                            <td align="left">
                                <font>
                                    <asp:TextBox ID="Txtyddh" runat="server" CssClass="shopCenRform" onblur="CheckMobileTele()"></asp:TextBox></font>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <%=GetTran("000112", "收货地址")%>：
                            </th>
                            <td align="left">
                                <asp:Panel ID="panel1" runat="server">
                                    <asp:RadioButtonList ID="rbtAddress" runat="server" Font-Size="12px" AutoPostBack="True"
                                        OnSelectedIndexChanged="rbtAddress_SelectedIndexChanged" 
                                        RepeatLayout="Flow">
                                    </asp:RadioButtonList>
                                </asp:Panel>
                                <asp:Panel ID="panel2" runat="server">  
                                </asp:Panel>
                            </td>
                        </tr>
                         <tr>
                            <th >
                                <%=GetTran("000112", "收货地址")%>：
                            </th>
                            <td align="left">
                                <uc1:CountryCity ID="CountryCity1" runat="server" />
                            </td>
                        </tr>
                               <tr>
                            <th >
                                <%=GetTran("000920", "详细地址")%>：
                            </th>
                            <td align="left">
                                  <asp:TextBox ID="Txtdz" CssClass="shopCenRform"  runat="server"  MaxLength="120"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <th >
                                <%=GetTran("001198", "收货姓名")%>：
                            </th>
                            <td align="left">
                                <asp:TextBox ID="Txtxm" CssClass="shopCenRform" runat="server" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style=" display:none;">
                            <th>
                                <%=GetTran("000114", "邮政编码")%>：
                            </th>
                            <td align="left">
                                <asp:TextBox ID="Txtyb" runat="server" Width="150px" MaxLength="6" onblur="VerifyPostCard()"></asp:TextBox>
                                <font face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </font>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <%=GetTran("001203", "收货电话")%>：
                            </th>
                            <td align="left">
                                <font face="宋体">
                                    <asp:TextBox ID="Txtjtdh" runat="server" MaxLength="11" CssClass="shopCenRform"  onblur="famTelOnblur()"></asp:TextBox></font>
                            </td>
                        </tr>
                       
                        <tr>
                           <th>
                                <%=GetTran("007416", "收货途径")%>：
                           </th> 
                           <td>
                                <asp:RadioButtonList ID="DDLSendType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">店铺收货</asp:ListItem>
                                    <asp:ListItem Value="1">会员收货</asp:ListItem>
                                </asp:RadioButtonList>
                           </td>
                        </tr>
                        <tr style="display: none">
                            <th>
                                <%=GetTran("001532", "Email")%>：
                            </th>
                            <td align="left">
                                <asp:TextBox ID="Email" CssClass="shopCenRform" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <%=GetTran("001345", "发货方式")%>：
                            </th>
                            <td align="left">
                                
                                <asp:RadioButtonList ID="ddth" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="2" Selected="True">邮寄</asp:ListItem>
                                    <asp:ListItem Value="1">自提</asp:ListItem>
                                    
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <%=GetTran("001535", "提货费用")%>：
                            </th>
                            <td align="left">
                                0
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <th>
                                <%=GetTran("000185", "支付币种")%>：
                            </th>
                            <td align="left">
                                <asp:DropDownList ID="DropCurrency"  runat="server" onChange="Bind();" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <%=GetTran("000078", "备注")%>：
                            </th>
                            <td align="left">
                                <asp:TextBox ID="Txtbz" CssClass="shopCenRform" runat="server" Width="352px" TextMode="MultiLine" Columns="20"
                                    Height="88px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="shopCenRtd" align="center">
                            <asp:Button ID="go" runat="server" Text="确认支付" Width="78px" Height="31px" style="background-image:url(images/shop-shopButton.png)" OnClick="go_Click" />
                            </td>
                        </tr>
                    </table>
                
            </div>
            
        <!--信息结束-->
        <div style="clear:both"></div>
        </div>

        <uc2:bottom runat="server" ID="bottom" />
        <!--页面内容结束-->
    </div>
    <asp:Label ID="Label1" runat="server"></asp:Label><input type="hidden" id="saveStore" />
    </form>
</body>
</html>
