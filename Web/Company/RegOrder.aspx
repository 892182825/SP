<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegOrder.aspx.cs" Inherits="Company_RegOrder" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <%--<script src="js/jquery-1.4.3.min.js" type="text/javascript"></script>--%>
<script language="javascript" type="text/javascript">
        function  Bind()
		{
		   var divPro = document.getElementById('product');
			var pId="";
			var productid="";
			var hasChose=0;
			for(var i=0;i<document.getElementById('menuLabel').getElementsByTagName('input').length;i++)
			{
				if(document.getElementById('menuLabel').getElementsByTagName('input').item(i).getAttribute("type")=="text")
				{
				    var numx=document.getElementById('menuLabel').getElementsByTagName('input').item(i).value;
				    var num=Number(document.getElementById('menuLabel').getElementsByTagName('input').item(i).value);
				    if(numx!=num)//验证输入产品数量是否是数字

				    {
				        alert('<%=this.GetTran("000239", "产品数字只能为数字")%>');
				        document.getElementById('menuLabel').getElementsByTagName('input').item(i).value=0;
				    }
				    if(num>0)//数量大于0记录产品
				    {
				    	pId+=document.getElementById('menuLabel').getElementsByTagName('input').item(i).value+",";
					    productid+=document.getElementById('menuLabel').getElementsByTagName('input').item(i).name+",";
					    ++hasChose;
					}
				}
			}

			var storeid= '<%=GetStoreId()%>';
   
            var curr=document.getElementById("DropCurrency").value;
                        
			divPro.innerHTML=AjaxMemShopCart.DataBindTxt(pId,storeid,productid,"tablemb","",curr).value;//更新产品表格记录

//		    if(hasChose>0)
//		    {
//		      document.getElementById("lblErr").style.display="none";
//		    }
	    }
          
         function    setpids(ele)
	    { 
	      var  hpid= document.getElementById("hidpids");
	      hpid.value= hpid.value.replace(","+ele.name ,"");	
	      if(ele.value>0)   
	      hpid.value+=","+ele.name; 
	     
	   
	    }  
	    
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
                   var result=AjaxMemShopCart.GetProductDetail(pid).value;
                   document.getElementById("divShowProduct").innerHTML=result;
                   
                   if(navigator.appName=="Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE6.0")
                   { 
                      for(var i=0; i<document.getElementsByTagName("SELECT").length;i++)
                        document.getElementsByTagName("SELECT")[i].style.visibility="hidden";
                   }
               }

            }
            function HideProductDiv(sender)
            {
                document.getElementById("divShowProduct").style.display = "none";
                if(navigator.appName=="Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g,"")=="MSIE6.0")
                { 
                    for(var i=0; i<document.getElementsByTagName("SELECT").length;i++)
                        document.getElementsByTagName("SELECT")[i].style.visibility="visible";
                }
            } 
            
            window.onload=function()
            {
                Bind();
            } 
            
            function GetCart(sender,pid)
            {
                //alert(sender.value+":"+pid);
                var myReg=/\d+/;
                var obj = myReg.exec(sender.value);  
                if (obj)
                { 
                    if(sender.value==0)
                    {
                        var flag=AjaxMemShopCart.DelOne(pid,"0").value;
                    }
                    else
                    {
                         flagStr=AjaxMemShopCart.UpdShopCart(pid,sender.value,"0").value;
                    }
                }
            } 
    </script>
    

    <style type="text/css">
        <!
        -- .bjkk
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
        -- ></style>
    <style type="text/css">
        #Form1
        {
            margin-left: 40px;
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
    </style>
    
    <link href="../Store/CSS/Store.css" rel="stylesheet" type="text/css" id="cssid"/>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

</head>
<body>
    <form id="Form1" onsubmit="filterSql_III()" method="post" runat="server">
    <br />
     <asp:HiddenField ID="hidpids" runat="server" />
    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tablemb">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="biaozzi">
                    <tr>
                        <td>
                            <asp:Label ID="messageLabel" runat="server" Font-Bold="true" ForeColor="Red" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#D6EDE6"
                    class="biaozzi">
                    <tr>
                        <td align="left">
                            <a href="ShopingList.aspx" style="text-decoration:underline; padding-left:10px;">切换</a>
                            <span id="sp1" runat="server">
                                <%=GetTran("000066", "现金可报单额")%>：&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblMoney" runat="server" Text="Label"></asp:Label>
                            </span>
                        </td>
                        <td align="left">
                            &nbsp;
                            <asp:Label ID="lblBtLine" runat="server" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp
                        </td>
                    </tr>
                </table>
                <table id="table1" cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
                    <tr style="width: 30%">
                        <td align="left" valign="top" nowrap="NOWRAP">
                            &nbsp;&nbsp;
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                <tr style="display:none;">
                                    <td align="left">
                                        <%=GetTran("000526", "运货方式")%>：
                                        <asp:DropDownList ID="ddth" runat="server" Width="100px">
                                            <asp:ListItem Value="1">自提</asp:ListItem>
                                            <asp:ListItem Value="2">邮寄</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td align="left">
                                        <%=GetTran("000185", "支付币种")%>：
                                        <asp:DropDownList ID="DropCurrency" onChange="Bind()" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td align="left">
                                        <%=GetTran("000186", "支付方式")%>：
                                        <asp:DropDownList ID="Ddzf" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td align="left">
                                        支付期数：
                                        <asp:DropDownList ID="ddlQishu" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="DD1" runat="server" style="display:none;">
                                    <td align="left">
                                        <%=GetTran("000024", "会员编号")%>： <span>
                                            <asp:TextBox ID="txtdzbh" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                                        </span>
                                    </td>
                                </tr>
                                <tr id="DD2" runat="server" style="display:none;">
                                    <td align="left">
                                        <%=GetTran("006056", "二级密码")%>： <span>
                                            <asp:TextBox ID="txtdzmima" runat="server" Width="100px" MaxLength="10" TextMode="Password"></asp:TextBox>
                                        </span>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td align="left">
                                        <%=GetTran("001345","发货方式") %>：
                                    
                                    <asp:DropDownList ID="DDLSendType" runat="server" Width="100px">
                                        <asp:ListItem Value="0" Selected="True">公司发货到店铺</asp:ListItem>
                                        <asp:ListItem Value="1">公司直接发给会员</asp:ListItem>
                                    </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 100%; text-overflow: ellipsis; word-break: keep-all;
                                        overflow: hidden;">
                                        <div class="biaozzi" align="left">
                                            <asp:Label ID="menuLabel" runat="server"></asp:Label>
                                            <div id="divShowProduct" style="position: absolute; border-right: #a5a5a5 1px solid;
                                                padding-right: 10px; border-top: #a5a5a5 1px solid; padding-left: 0px; padding-top: 0px;
                                                border-left: #a5a5a5 1px solid; width: 302px; height: 164px; border-bottom: #a5a5a5 1px solid;
                                                background-color: #ffffff; display: none; overflow-y: hidden; overflow: hidden;
                                                text-align: center;" onmouseover="this.style.display='block'" onmouseout="this.style.display='none'">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="background-color:#ffffff;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff"
                                class="biaozzi">
                                <tr>
                                    <td valign="bottom" colspan="2" align="left">
                                        <%--<br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;<img height="40" src="../images/xchuo.gif" width="40">
                                        <b>购物车</b>
                                        <br />--%>
                                         <div class="shopping_tit"><span><img src="images/cartnav-cart.gif" width="199" height="64" /></span>
            <img src="images/cartnav-step1.gif" width="566" height="64" /></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="product" runat="server" style="width: 90%; margin-left: 50px">
                                        </div>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" align="center">
                                         <br />
                                         
                                        <asp:Button ID="Button1" runat="server" Text="去结算中心" CssClass="anyes" 
                                             onclick="Button1_Click" />
                                    </td>
                                </tr>
                               
                                
                                
                                
                               
                                
                               
                                
                               
                               
                                
                               
                                
                               
                                
                               
                               
                                
                            </table>
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
    </form>
</body>
</html>




