<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdZhuChe.aspx.cs" Inherits="Store_UpdZhuChe" EnableEventValidation="false" %>

<%@ Register src="UserControl/CountryCityPCode.ascx" tagname="CountryCityPCode" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <script type="text/javascript" src="../javascript/cardAndElcCard.js"></script>
    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>
    <link href="Company/CSS/Company.css" rel="stylesheet" id="cssid" type="text/css" />
    <script src="../JS/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <style type="text/css">
        a
        {
            text-decoration:none;
            color:rgb(0,85,117);
        }
    </style>

    <script src="JS/Editreginfo.js" type="text/javascript"></script>
    
    <script language="javascript">
        window.onload = function() {
            if (window.location.href.indexOf("CssType=2") != -1)
                document.getElementById("cssid").href = "Store/CSS/Store.css";

            abc();

            Bind();
        }

        function setpids(ele) {
            var hpid = document.getElementById("hidpids");
            hpid.value = hpid.value.replace("," + ele.name, "");
            if (ele.value > 0)
                hpid.value += "," + ele.name;
        }

        function getpro() {
            var param1 = '<%=this.GetTran("000239", "产品数字只能为数字")%>';
            var param2 = '<%=GetStoreID()%>';
            return { pdnum: param1, gstid: param2 };
        }

	</script> 
	<script type="text/javascript" src="js/SqlCheck.js"></script>
</head>
<body>
	<form id="Form1"  runat="server" onsubmit="return filterSql_III()">
    <br />
    <asp:HiddenField ID="hidpids" runat="server" />
	    <div style="width:100%" align="center">
	    <table style="width:100%;"  class="biaozzi">
			<tr>
				<td align="left"  style="width:200px"><%=GetTran("000066", "可报单额")%>：&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMoney" runat="server" Text="Label"></asp:Label></td>
				<td align="left"><asp:Label ID="lblBtLine" runat="server" Text="Label" Visible="False"></asp:Label></td>				
			</tr>
		</table>
        
		<table  class="biaozzi">
			<tr>
				<td ><asp:label id="messageLabel" Runat="server" EnableViewState="False"></asp:label></td>
			</tr>
		</table>
		
		<table id="table1" class="biaozzi"  class="biaozzi" border="0" cellspacing="0" cellpadding="5">
			<tr >
			    <td valign="top"  style="width:100%;text-overflow:ellipsis; word-break:keep-all; overflow:hidden;" >
                    <div align="left"  class="biaozzi">
                        <asp:label id=menuLabel Runat="server"></asp:label>
                        <div id="divShowProduct" style="position: absolute; border-right: #a5a5a5 1px solid;
                                        padding-right: 10px; border-top: #a5a5a5 1px solid; padding-left: 0px; padding-top:0px;
                                        border-left: #a5a5a5 1px solid; width: 302px; height:164px;border-bottom: #a5a5a5 1px solid;
                                        background-color: #ffffff; display: none; overflow-y:hidden;overflow:hidden; text-align:center;" onmouseover="this.style.display='block'"
                                        onmouseout="this.style.display='none'"></div> 
                    </div>
				</td>
				<td valign="top" style="padding-left:10px;">
					<table class="biaozzi"  border="0">
					    <tr>
					        <td colspan="2">
					            <img src="images/xchuo.gif"  align=absmiddle>购物车
					            <div id="product" runat="server" style="width:100%"></div>
					            <img src="images/men01.gif" align=absmiddle>基本信息
					        </td>
					    </tr>
					    <tr>
					        
						    <td  valign="middle" align="right"><font color="red"></font><%=GetTran("000024", "会员编号")%>：</td>
						    <td  align="left">
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </td>
					    </tr>
				 
					    <tr align="left">
							<td  valign="middle" align="right" ><font color="red">*</font><%=GetTran("000112", "收货地址")%>：</td>
							<td >
							    <table border="0" cellspacing="0" cellpadding="0">
									<tr align="left">
										<td ><uc1:countrycitypcode ID="CountryCity2" runat="server" /></td>
										<td ><asp:textbox id="Txtdz" Runat="server" MaxLength="50"></asp:textbox></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
						  <td valign="middle" align="right"><%=GetTran("000114", "邮政编码")%>：</td>
						   <td align="left"><asp:textbox id="Txtyb" Runat="server" Width="150px" MaxLength="6"></asp:textbox></td>
						</tr>															
					    <tr align="left">
						    <td  valign="middle" align="right"><%=GetTran("000526", "运货方式")%>：</td>
						    <td >
						        <asp:dropdownlist id="ddth" Runat="server" Width="100px">
									    <asp:ListItem Value="1">自提</asp:ListItem>
									    <asp:ListItem Value="2">邮寄</asp:ListItem>									    
								    </asp:dropdownlist>
							</td>
					    </tr>
					    <tr align="left" style="display:none;">
						    <td valign="middle" align="right"><%=GetTran("000135", "运输费用")%>：</td>
						    <td>0</td>
					    </tr>
					  
                          <tr align="left" id="dropdown" style="display:none">
						    <td valign="middle" align="right" ><%=GetTran("000185", "支付币种")%>：</td>
						    <td ><asp:dropdownlist id="DropCurrency" runat="server" Width="100px" onchange="Bind()"></asp:dropdownlist></td>
					    </tr>
					  
					    <tr align="left" style=" display:none;">
						    <td  valign="top" align="right" ><%=GetTran("000186", "支付方式")%>：</td>
						    <td  >
                                <asp:dropdownlist id="Ddzf" Runat="server" Width="100px" onchange="abc()" >
							    </asp:dropdownlist></td>
					    </tr>
                        <tr id="DD1" runat="server" style="display:none;">
                            <td valign="middle" align="right"><%=GetTran("000024", "会员编号")%>：</td>
                            <td align="left" >
                                <asp:TextBox id=txtdzbh Runat="server" Width="100"></asp:TextBox></td></tr>
                        <tr id="DD2" runat="server" style="display:none">
                            <td valign="middle" align="right"><%=GetTran("000187", "电子钱包密码")%>：</td>
                            <td align="left"><asp:TextBox id=txtdzmima Runat="server" TextMode="Password"></asp:TextBox></td>
                        </tr>
                        <tr align="left" style=" display:none;">
                            <td align="right"><%=GetTran("001345","发货方式") %>：</td>
                            <td><asp:DropDownList ID="DDLSendType" runat="server" Width="100px">
                                                        <asp:ListItem Value="0" Selected="True">公司发货到店铺</asp:ListItem>
                                                        <asp:ListItem Value="1">公司直接发给会员</asp:ListItem>
                                                    </asp:DropDownList></td>
                        </tr>
					    <tr align="center">
						    <td valign="top"  colspan="2">
                                <asp:Button ID="btnEditProduct" runat="server" Text="修 改" CssClass="anyes"   onclick="btnEditProduct_Click"/>
					        </td>
					    </tr>
					</table>
				</td>
			</tr>
		</table>						
		<div id="Dealdiv" runat="server"></div>					
	    <asp:label ID="txt_jsLable" runat="server"></asp:label>
	  
	    </div>
	</form>
</body>		
</html>