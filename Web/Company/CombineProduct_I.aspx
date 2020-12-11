<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CombineProduct_I.aspx.cs" Inherits="Company_CombineProduct_I" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    
    <style type="text/css">
        .tb
        {
        	text-align:center;
        	border:rgb(157,228,244) solid 1px;
        }
        .tb tr
        {
        	text-align:center;
        	color:rgb(0,61,92)
        }
        .tb td
        {
        	text-align:center;
        	height:25px;
        }
        
    </style>
    
  <script language="javascript" type="text/javascript">
		function menu( menu,img,plus )
		{			
			if( menu.style.display == "none"){
			
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
	    
	    var xmlHttp;
	    var quantp;
	    function createTable(pid)
	    {
	        getData(pid,"3",document.getElementById("<%=ddlCombineProduct.ClientID %>").options[document.getElementById("<%=ddlCombineProduct.ClientID %>").selectedIndex].value)
	    }
	    
	    function createXMLHttpRequest()
	    {
	        if(window.ActiveXObject)
	            xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
	        else if(window.XMLHttpRequest)
	            xmlHttp=new XMLHttpRequest();
	            
	    }
	    
	    function getData(pid,tp,zhpid)
	    {
	        quantp=tp;
	        
	        if(xmlHttp==null)
	            createXMLHttpRequest();
	       
	        xmlHttp.open("post","CombineProduct_Ajax.aspx",true);
	        xmlHttp.onreadystatechange=stateChange;
	        
	        xmlHttp.setRequestHeader("content-type","application/x-www-form-urlencoded"); 
	        
	        xmlHttp.send("pid="+pid+"&tp="+tp+"&zhpid="+zhpid+"&date="+new Date().getTime());
	    }
	    
	    function stateChange()
	    {
	        if(xmlHttp.readyState==4)
	        {
	            if(xmlHttp.status==200)
	            {
	                if(quantp=="1")
	                {
	                    if(document.getElementById("tabid")!=null)
	                    {
	                        document.getElementById("aabb").removeChild(document.getElementById("tabid"));
	                    }
                      
	                    var xmlobj=xmlHttp.responseXML;
    	                
	                    var hangarr=xmlobj.getElementsByTagName("hang");
    	                
	                    var tabobj=document.createElement("table");
	                    tabobj.id="tabid";
	                    tabobj.style.fontSize="10pt";
	                    tabobj.style.width="700px";
	                    tabobj.className="tb";
	                    tabobj.cellSpacing="1px";
	                    tabobj.border="0";
    	                
	                    var tbodyobj=document.createElement("tbody");
    	                
	                    var trthobj=document.createElement("tr");
	                    trthobj.style.backgroundImage="url(images/lmenu02.gif)";
	                    trthobj.style.color="white";
	                    trthobj.style.height="25px";
	                    trthobj.style.fontWeight="bold";
    	                
	                    var thtdobj0=document.createElement("td")
	                    thtdobj0.appendChild(document.createTextNode("操作"));
    	                
	                    var thtdobj1=document.createElement("td")
	                    thtdobj1.appendChild(document.createTextNode("序号"));
    	                
	                    var thtdobj2=document.createElement("td")
	                    thtdobj2.appendChild(document.createTextNode("产品编码"));
    	                
	                    var thtdobj3=document.createElement("td")
	                    thtdobj3.appendChild(document.createTextNode("产品名称"));
    	                
	                    var thtdobj4=document.createElement("td")
	                    thtdobj4.appendChild(document.createTextNode("单价"));
    	                
	                    var thtdobj5=document.createElement("td")
	                    thtdobj5.appendChild(document.createTextNode("积分"));
    	                
	                    var thtdobj6=document.createElement("td")
	                    thtdobj6.appendChild(document.createTextNode("数量"));
    	                
	                    var thtdobj7=document.createElement("td")
	                    thtdobj7.appendChild(document.createTextNode("总金额"));
    	                
	                    var thtdobj8=document.createElement("td")
	                    thtdobj8.appendChild(document.createTextNode("总积分"));
    	                
	                    trthobj.appendChild(thtdobj0);
	                    trthobj.appendChild(thtdobj1);trthobj.appendChild(thtdobj2);
	                    trthobj.appendChild(thtdobj3);trthobj.appendChild(thtdobj4);
	                    trthobj.appendChild(thtdobj5);trthobj.appendChild(thtdobj6);
	                    trthobj.appendChild(thtdobj7);trthobj.appendChild(thtdobj8);
    	                
	                    tbodyobj.appendChild(trthobj);
    	 
	                    for(var i=0;i<hangarr.length;i++)
	                    {
    	                  
	                        var trobj=document.createElement("tr");
	                        if(i%2!=0)
	                            trobj.style.backgroundColor="rgb(241,244,248)";
    	                        
	                        var tdobj0=document.createElement("td");
    	                    
	                        var tdval0;
	                        if(xmlobj.getElementsByTagName("isyw")[0].firstChild.nodeValue=="n")
	                        {
	                           tdval0=document.createTextNode("删除");
	                           tdobj0.style.color="gray";
	                        }
	                        else
	                        {
	                            tdval0=document.createElement("A");
	                            tdval0.id=hangarr[i].getElementsByTagName("productid")[0].firstChild.nodeValue;//
	                            tdval0.innerHTML="删除";
	                            tdval0.onclick=function()
	                            {
	                                if(window.confirm('确定要删除？')) getData(this.id,'2',document.getElementById("<%=ddlCombineProduct.ClientID %>").options[document.getElementById("<%=ddlCombineProduct.ClientID %>").selectedIndex].value);
	                            };
	                            
	                            tdval0.style.cursor="pointer";
	                        }
    	                    
	                        tdobj0.appendChild(tdval0);   
    	                    
	                        //编号
	                        var tdobj1=document.createElement("td");
	                        var tdval1=document.createTextNode((i+1)+"");
	                        tdobj1.appendChild(tdval1);
	                        //产品编码
	                        var tdobj2=document.createElement("td");
	                        var tdval2=document.createTextNode(hangarr[i].getElementsByTagName("productcode")[0].firstChild.nodeValue);
	                        tdobj2.appendChild(tdval2);
	                        //产品名称
	                        var tdobj3=document.createElement("td");
	                        var tdval3=document.createTextNode(hangarr[i].getElementsByTagName("productname")[0].firstChild.nodeValue);
	                        tdobj3.appendChild(tdval3);
	                        //单价
	                        var tdobj4=document.createElement("td");
	                        var tdval4=document.createTextNode(hangarr[i].getElementsByTagName("preferentialprice")[0].firstChild.nodeValue);
	                        tdobj4.appendChild(tdval4);
	                        //积分
	                        var tdobj5=document.createElement("td");
	                        var tdval5=document.createTextNode(hangarr[i].getElementsByTagName("preferentialpv")[0].firstChild.nodeValue);
	                        tdobj5.appendChild(tdval5);
	                        //数量
	                        var tdobj6=document.createElement("td");
	                        var inputobj=document.createElement("input");
	                        inputobj.style.width="50px";
	                        inputobj.maxLength=5;
	                        inputobj.type="text";
	                        inputobj.id=hangarr[i].getElementsByTagName("productid")[0].firstChild.nodeValue+"tx";
	                        inputobj.value=hangarr[i].getElementsByTagName("quantity")[0].firstChild.nodeValue;
	                        
	                        var butobj=document.createElement("input");
	                        butobj.type="button";
	                        butobj.className="anyes";
	                        butobj.id=hangarr[i].getElementsByTagName("productid")[0].firstChild.nodeValue;
	                        butobj.value="更新";
	                        butobj.onclick=function()
	                        {
	                            if(isNaN(document.getElementById(this.id+"tx").value))
	                                alert("只能输入数字");
	                            else if(document.getElementById(this.id+"tx").value.indexOf(".")!=-1)
	                                alert("产品数量只能是整数");
	                            else if(document.getElementById(this.id+"tx").value-0<0)
	                                alert("只能输入正整数");
	                            else
    	                            getData(this.id,"7",document.getElementById(this.id+"tx").value+"*"+document.getElementById("<%=ddlCombineProduct.ClientID %>").options[document.getElementById("<%=ddlCombineProduct.ClientID %>").selectedIndex].value);
	                        }
	                        
	                        tdobj6.appendChild(inputobj);
	                        tdobj6.appendChild(butobj);
	                        //总金额
	                        var tdobj7=document.createElement("td");
	                        var tdval7=document.createTextNode(hangarr[i].getElementsByTagName("zje")[0].firstChild.nodeValue);
	                        tdobj7.appendChild(tdval7);
	                        //总积分
	                        var tdobj8=document.createElement("td");
	                        var tdval8=document.createTextNode(hangarr[i].getElementsByTagName("zjf")[0].firstChild.nodeValue);
	                        tdobj8.appendChild(tdval8);
    	                    
	                        trobj.appendChild(tdobj0);
	                        trobj.appendChild(tdobj1);
	                        trobj.appendChild(tdobj2);
	                        trobj.appendChild(tdobj3);
	                        trobj.appendChild(tdobj4);
	                        trobj.appendChild(tdobj5);
	                        trobj.appendChild(tdobj6);
	                        trobj.appendChild(tdobj7);
	                        trobj.appendChild(tdobj8);
    	                    
	                        tbodyobj.appendChild(trobj);
	                    }
    	                
	                    tabobj.appendChild(tbodyobj);
    	                
	                    document.getElementById("aabb").appendChild(tabobj);
	                }
	                
	                
	                else if(quantp=="2")
	                {
	                    if(xmlHttp.responseText=="c")
	                    { 
	                        alert("删除成功！");
	                        getData(document.getElementById("<%=ddlCombineProduct.ClientID %>").options[document.getElementById("<%=ddlCombineProduct.ClientID %>").selectedIndex].value,"1","");
	                    }
	                    else if(xmlHttp.responseText=="s")
	                        alert("删除失败！");
	                }
	                else if(quantp=="3")
	                {
	                    if(xmlHttp.responseText=="c")
	                    { 
	                        getData(document.getElementById("<%=ddlCombineProduct.ClientID %>").options[document.getElementById("<%=ddlCombineProduct.ClientID %>").selectedIndex].value,"1","");
	                    }
	                    else if(xmlHttp.responseText=="s")
	                        alert("添加失败！");
	                    else if(xmlHttp.responseText=="i")
	                        alert("此产品已经发生业务，不允许添加！");
	                    else if(xmlHttp.responseText=="e")
	                    {
	                        alert("此产品已在组合产品之中，数量加 1 ");
	                        getData(document.getElementById("<%=ddlCombineProduct.ClientID %>").options[document.getElementById("<%=ddlCombineProduct.ClientID %>").selectedIndex].value,"1","");
	                   
	                    }
	                }

	                else if(quantp=="7")
	                {
	                    if(xmlHttp.responseText=="c")
	                    {
	                        getData(document.getElementById("<%=ddlCombineProduct.ClientID %>").options[document.getElementById("<%=ddlCombineProduct.ClientID %>").selectedIndex].value,"1","");
	                        alert("更新成功！");
	                    }
	                    else if(xmlHttp.responseText=="s")
	                        alert("更新失败！");
	                }
	            }
	        }
	    }
	    
	    function test(th)
	    {
	        getData(th.options[th.selectedIndex].value,"1","");
	    }
	</script>
</head>
<body >
    <form id="form1" runat="server">
    <div style='font-size:10pt;color:rgb(0,61,92)'><br/>
        <%=GetTran("000047", "国家")%>：<asp:dropdownlist id="ddlCountry" runat="server" 
            onselectedindexchanged="ddlCountry_SelectedIndexChanged" 
            AutoPostBack="True"></asp:dropdownlist> 
        
        <%=GetTran("004236", "组合产品列表")%>：<asp:dropdownlist id="ddlCombineProduct" runat="server" onchange='test(this)' ></asp:dropdownlist>
        <br>
        <table>
            <tr>
                <td valign="top">
                    <div id="treeid" runat="server"  style='font-size:10pt;color:rgb(0,61,92)'>
                    </div>
                </td>
                <td id="aabb" valign="top" style="padding-left:100px;padding-top:30px;">
                    
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding-left:100px;">
                    
                </td>
            </tr>
        </table>
    </div>
    <div id="ddddd"></div>
    </form>
</body>
</html>
