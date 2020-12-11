<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSContent.aspx.cs" Inherits="Company_SMSContent" %>

<%@ Register TagPrefix="ucl" TagName="uclCountry" Src="~/UserControl/Country.ascx" %>

<html>
<head >   
    <title>无标题页</title>
    <link href="CSS/Company.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript">        
		function menuTree( menu,img,plus )
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
		
			var newWin=0;
			var widthProduct=830;
			var heightProduct=720;
			var widthFold=420;
			var heightFold=320;			
//			var topProduct=(screen.height-heightProduct)/2;   
//			var leftProduct=(screen.width-widthProduct)/2;
//			var topFold=(screen.availHeight/2)-(heightFold/2);
//			var leftFold=(screen.availWidth/2)-(heightFold/2);
    
            var topProduct=100;   
			var leftProduct=100;
			var topFold=100;
			var leftFold=100;
			
			var widthDel=400;
			var heightDel=100;
//			var topDel=(screen.availHeight-heightDel)/2;
//			var leftDel=(screen.availWidth-widthDel)/2;
			var topDel=100;
			var leftDel=100;
			
			function openAddWin(action,id)
			{	
				var param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthProduct+'px;dialogHeight:'+heightProduct+'px;dialogLeft:'+leftProduct+'px;dialogTop:'+topProduct+'px;';
			
				var sure = true;
				if( action == "add" )
				{
				    //param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthProduct+'px;dialogHeight:'+heightProduct+'px;dialogLeft:'+leftProduct+'px;dialogTop:'+topProduct+'px;';
			        param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthFold+'px;dialogHeight:'+heightFold+'px;dialogLeft:'+leftFold+'px; dialogTop:'+topFold+'px;';	
			    }
			    if( action == "addsms" )
				{
				    //param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthProduct+'px;dialogHeight:'+heightProduct+'px;dialogLeft:'+leftProduct+'px;dialogTop:'+topProduct+'px;';
			        param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthFold+'px;dialogHeight:'+heightFold+'px;dialogLeft:'+leftFold+'px; dialogTop:'+topFold+'px;';	
			    }
				
				else if (action=="addFold")
				{
				    param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthFold+'px;dialogHeight:'+heightFold+'px;dialogLeft:'+leftFold+'px; dialogTop:'+topFold+'px;';				    				   		    
				}
				
				else if( action == "deleteItemSMS" )
				{
				    if(window.confirm('<%=GetTran("005831","你确认要删除吗?")%>'))
				    {	
				        sure=true;			       
				        param='status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:'+widthDel+'px;dialogHeight:'+heightDel+'px;dialogLeft:'+leftDel+'px;dialogTop:'+topDel+'px;';
				    }
				    
				    else
				    {
				        sure=false;			        
				    }				
			        
				}
				    
				else if( action == "deleteItem" )
				{
				    if(window.confirm('<%=GetTran("005831","你确认要删除吗?")%>'))
				    {	
				        sure=true;			       
				        param='status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:'+widthDel+'px;dialogHeight:'+heightDel+'px;dialogLeft:'+leftDel+'px;dialogTop:'+topDel+'px;';
				    }
				    
				    else
				    {
				        sure=false;			        
				    }				
			        
				}
				
				else if( action == "deleteFold" )
				{
				    if(window.confirm('<%=GetTran("005831","你确认要删除吗?")%>'))
				    {
				        sure=true;
				        param='status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:'+widthDel+'px;dialogHeight:'+heightDel+'px;dialogLeft:'+leftDel+'px;dialogTop:'+topDel+'px;';
				    }
				    
				    else
				    {
				        sure=false;
				    }
				}
				
				else if( action == "editFold" )
				{
				    param='status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:'+widthFold+'px;dialogHeight:'+heightFold+'px;dialogLeft:'+leftFold+'px;dialogTop:'+topFold+'px;';				   
				}
					
				else if( action == "editItem" )
				{
				   // param='status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:'+widthProduct+'px;dialogHeight:'+heightProduct+'px;dialogLeft:'+leftProduct+'px;dialogTop='+topProduct+'px;';					
				     param='status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:'+widthFold+'px;dialogHeight:'+heightFold+'px;dialogLeft:'+leftFold+'px;dialogTop:'+topFold+'px;';
				}
				
				else if( action == "editItemSMS" )
				{
				   // param='status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:'+widthProduct+'px;dialogHeight:'+heightProduct+'px;dialogLeft:'+leftProduct+'px;dialogTop='+topProduct+'px;';					
				     param='status:no;resizable:no;copyhistory:no;scroll:no;help:no;dialogWidth:'+widthFold+'px;dialogHeight:'+heightFold+'px;dialogLeft:'+leftFold+'px;dialogTop:'+topFold+'px;';
				}
				
				if(sure)				
				{
				    newWin=window.showModalDialog("ADDSMSContent.aspx?action=" + action + "&id=" + id+"&date="+new Date().getTime() ,'nW', param);
				    if(newWin==1)					   
				    {
				        window.location.reload(); 
				    }															
				}				
			}
			
			function openAddWin2(action,id,countryCode)
			{
				var param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthProduct+'px;dialogHeight:'+heightProduct+'px;dialogLeft:'+leftProduct+'px;dialogTop:'+topProduct+'px;';
				
				if( action == "addFold" )
				{
				    param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthFold+'px;dialogHeight:'+heightFold+'px;dialogLeft:'+leftFold+'px; dialogTop:'+topFold+'px;';			    
				}
				
				if( action == "add" )
				{
				    //param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthProduct+'px;dialogHeight:'+heightProduct+'px;dialogLeft:'+leftProduct+'px;dialogTop:'+topProduct+'px;';
				    param='status:no;resizable:no;scroll:no;help:no;dialogWidth:'+widthFold+'px;dialogHeight:'+heightFold+'px;dialogLeft:'+leftFold+'px; dialogTop:'+topFold+'px;';			    
				}	
	
			    newWin=window.showModalDialog("ADDSMSContent.aspx?action=" + action + "&id=" + id +"&countryCode="+countryCode  ,'nW', param);			    
			    if(newWin==1)					   
				{
				    window.location.reload(); 
				}																
			}		
			
			function openSMScontent(action,id)
			{
			    var con = AjaxClass.OpenSmsContent(id).value;
			    
			    document.getElementById("lblcon").innerHTML = con;
			}	
    </script>
 
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upProductTreeList" runat="server">
        <ContentTemplate>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" >
            <tr>
                <td><%=GetTran("000058","请选择国家")%>：</td>
                <td>
                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td></td>                
            </tr>
        </table>
                <br />
         <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" width="90%">
            <tr>
                <td align="left" colspan=2>
                    <a href="javascript:window.location.reload();"><%=GetTran("007023", "刷新列表")%></a>
                 </td>
            </tr>
            <tr align="left">
                <td style="width:30%" valign="top" >
                    <asp:Label ID="lblmenu" runat="server"></asp:Label>            
                </td>
                <td valign="top" >
                    &nbsp; <asp:Label ID="lblcon" runat="server"></asp:Label>   
                    </td>
            </tr>
        </table>     
            </div>   
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
