<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageIn.aspx.cs" Inherits="Company_StorageIn"  %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="../javascript/cardAndElcCard.js"></script>

    <script type="text/javascript" src="../javascript/My97DatePicker/WdatePicker.js"></script>

    <script src="../JS/CommonFunction.js" type="text/javascript"></script>
    <script src="../JS/sryz.js"></script>
    <script language="javascript" type="text/javascript">
    function UpdateTree()
		{
           var returnValue=AjaxClass.GetProductMenu(document.getElementById("ddlCountry").value).value;
           document.getElementById("menuLabel").innerHTML=returnValue;   
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
                   var result=AjaxClass.GetProductDetailIsCount(pid).value;
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
        function checkbig(obj,proid){
            if(obj.checked){
                 AjaxClass.EnstorageListBig("1",proid.toString());
            }else{
                 AjaxClass.EnstorageListBig("0",proid.toString());
            }
            Bind();
        }   
        function EnShopp(num,proid)
		{
	        num = num.value;
             if(num=="")
                num="0";
		     AjaxClass.EnstorageList(parseInt(num),proid.toString());
		    
		     Bind();
		}
		function EnShoppPrice(num,proid)
		{
	        num = num.value;
            if(num=="")
                num="0";
		     AjaxClass.EnstorageListPrice(num,proid.toString());
		    
		     Bind();
		}
		 function EnShoppPi(num,proid)
		{
	         num = num.value;
             if(num==""){
                num="0";
             }
		     AjaxClass.EnstorageListPi(num,proid.toString());
		    
		     Bind();
		 }
		 function EnMFD_DT(dateObj, proid) {
		     var dateString = dateObj.value;
		     if (dateString != "") {
		         AjaxClass.EnstorageListMFD_DT(dateString, proid.toString());
		         Bind();
		     }
		 }

		 function EnExp_DT(dateObj, proid) {
		     var dateString = dateObj.value;
		     if (dateString != "") {
		         AjaxClass.EnstorageListExp_DT(dateString, proid.toString());
		         Bind();
		     }
		 }
		 
         function AjShopp(proid,proName)
		 {
		      AjaxClass.GetstorageList(proid,proName);
		     Bind();
		 }
       function  Bind()
	   {
		    var divPro = document.getElementById('product');
     
             var curr=document.getElementById("ddlCountry").value;
             if(curr==""){
                curr="86";
             }
             divPro.innerHTML=AjaxClass.BindstorageList(curr).value;
	    }
	    function checkDate()
	    {
	         var  hpid= document.getElementById("hidpids");
	         if(document.getElementById("txtpici").value=="")
	         {
	             alert('<%=GetTran("007678","填写入库批次！") %>');
	             return false;
	         }
	    }

    </script>
    
    <link rel="Stylesheet" href="../company/CSS/company.css" type="text/css" id="cssid" />
<style type="text/css">
    .lineh
    {
    	line-height:30px;
    }
    .hiddenObj{display:none;}

</style>
    <script language="javascript" type="text/javascript" src="../js/SqlCheck.js"></script>

</head>
<body>
    <form id="Form1" onsubmit="filterSql_III()" method="post" runat="server">
    <br />
         <div id="visdiv" runat="server"> 
     <asp:HiddenField ID="hidpids" runat="server" />
        
    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="tablemb">
        <tr>
            <td>
                <table id="table1" cellspacing="0" cellpadding="0" border="0" width="100%" class="biaozzi">
                    <tr style="width: 30%">
                        <td align="left" valign="top" nowrap="NOWRAP">
                            &nbsp;&nbsp;
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="biaozzi">
                                
                    
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
                        <td valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff"
                                class="biaozzi">
                                <tr class="lineh">
                                    <td valign="bottom" colspan="2" align="left">
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;<img height="40" src="../images/xchuo.gif" width="40">
                                        <b><%=GetTran("007152", "入库产品列表")%></b>
                                        <br />
                                    </td>
                                </tr>
                                <tr class="lineh">
                                    <td colspan="2">
                                        <div id="product" runat="server" style="width: 90%; margin-left: 50px">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width:200px;">
                                        <%=GetTran("000058", "国家")%>：
                                    </td>
                                    <td><asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true"
                                            Width="100px" onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr class="lineh">
                                    <td align="right"><asp:Label runat="server" ID="lblProvider"><%=GetTran("002020","供应商")%>：</asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlProvider" runat="server" AutoPostBack="true" 
                                            onselectedindexchanged="ddlProvider_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                               <tr class="lineh">
                                    <td align="right"><%=GetTran("000386","仓库")%>：</td>
                                    <td align="left">
                                        <asp:DropDownList runat="server" ID="ddlWareHouse" 
                                            Width="120px" onselectedindexchanged="ddlWareHouse_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="lineh">
                                    <td align="right">
                                        <%=GetTran("000390","库位")%>：</td>
                                    <td align="left" style="white-space: nowrap">
                                     <asp:DropDownList runat="server" ID="ddlDepotSeat" Width="120px" DataTextField="SeatName" DataValueField="DepotSeatID"></asp:DropDownList>
                                        
                                    </td>
                                </tr>
                                <tr class="lineh">
                                    <td align="right"><%=GetTran("000514","原始单据编号")%>：</td>
                                    <td><asp:TextBox ID="txtOriginalDocID" runat="server" Width="115px" MaxLength="20" onkeyup = "ValidateValue(this)"></asp:TextBox></td>
                                </tr>
                                
                                <tr class="lineh hiddenObj">
                                    <td align="right"> <%=GetTran("002021","业务员")%>：</td>
                                    <td>  <asp:TextBox ID="txtOperationPerson" runat="server" Width="115px" MaxLength="20"></asp:TextBox></td>
                                </tr>
                                <tr class="lineh">
                                    <td align="right"><%=GetTran("007679", "入库批次")%>：</td>
                                    <td>
                                        <asp:TextBox ID="txtpici" Width="115px" runat="server" MaxLength="20" onkeyup = "ValidateValue(this)"></asp:TextBox><font color="red"><%=GetTran("001272", "*必填")%></font>
                                        
                                    </td>
                                </tr>
                                <tr class="lineh hiddenObj" >
                                    <td align="right" style="white-space: nowrap"><%=GetTran("002040","购货地址")%>：</td>
                                    <td><asp:TextBox ID="txtAddress" runat="server" Width="500px" MaxLength="100"></asp:TextBox></td>
                                </tr>
                                <tr class="lineh">
                                    <td align="right"><%=GetTran("000078","备注")%>：</td>
                                    <td>
                                        <asp:TextBox Rows="4" ID="txtMemo" runat="server" TextMode="MultiLine" Height="100px"
                                            Width="500px" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSaveOrder" runat="server" Text="确认入库单"
                                            CssClass="another" OnClientClick="return checkDate();"  OnClick="btnSaveOrder_Click"  />&nbsp;&nbsp;
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
              </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">
       window.onload =function()
        {
	          try
	          {
	             Bind();

	          }
	         catch(e)
	         {}
	     }
	        
	    
</script>


