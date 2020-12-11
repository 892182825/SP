<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GiveProduct.aspx.cs" Inherits="Company_SetParams_GiveProduct" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="../../javascript/cardAndElcCard.js"></script>

    <script type="text/javascript" src="../../javascript/My97DatePicker/WdatePicker.js"></script>

    <script src="../JS/CommonFunction.js" type="text/javascript"></script>

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
		     AjaxClass.EnGiveProductList(parseInt(num),proid.toString());
		    
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
         function AjShopp(proid,proName)
		 {
		      AjaxClass.GetGiveProductList(proid,proName);
		     Bind();
		 }
       function  Bind()
	   {
		    var divPro = document.getElementById('product');
     
             var curr=document.getElementById("ddlCountry").value;
             if(curr==""){
                curr="86";
             }
             divPro.innerHTML=AjaxClass.BindsGiveProductList(curr).value;
	    }
	    function checkPV()
	    {
	         if(document.getElementById("txtPvStart").value=="")
	         {
	             alert('<%=GetTran("000000","请填写赠送起始PV！") %>');
	             return false;
	         }
	         if(document.getElementById("txtPvEnd").value=="")
	         {
	             alert('<%=GetTran("000000","请填写赠结束始PV！") %>');
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

</style>
    <script language="javascript" type="text/javascript" src="../../js/SqlCheck.js"></script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <br />
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
                                        <b><%=GetTran("000000", "赠送产品列表")%></b>
                                        <br />
                                    </td>
                                </tr>
                                <tr class="lineh">
                                    <td colspan="2">
                                        <div id="product" runat="server" style="width: 90%; margin-left: 50px">
                                        </div>
                                    </td>
                                </tr>
                                <tr class="lineh">
                                    <td align="right">
                                        <%=GetTran("000058", "国家")%>：
                                    </td>
                                    <td><asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true"
                                            Width="100px" onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="lineh">
                                    <td align="right"><%=GetTran("000000","赠送起始PV")%>：</td>
                                    <td>
                                        <asp:TextBox ID="txtPvStart" Width="115px" runat="server" MaxLength="20"></asp:TextBox><font color="red">*</font>
                                        
                                    </td>
                                </tr>
                                <tr class="lineh">
                                    <td align="right"><%=GetTran("000000","赠送结束PV")%>：</td>
                                    <td>
                                        <asp:TextBox ID="txtPVEnd" Width="115px" runat="server" MaxLength="20"></asp:TextBox><font color="red">*</font>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSaveOrder" runat="server" Text="确认"
                                            CssClass="another"  OnClick="btnSaveOrder_Click" OnClientClick="return checkPV()"  />&nbsp;&nbsp;
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


