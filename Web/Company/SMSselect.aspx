<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSselect.aspx.cs" Inherits="Company_SMSselect" %>


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
			var heightFold=220;			
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
			
			function trim(s)
            { 
                return  s.replace(/(^\s*)|(\s*$)/g,"");     
             }
			
			function openXZ(action,id)
			{	
		
			
				if( action == "SMS" )
				{
				    var productName = AjaxClass.ReadSmsProductName(id).value;
				    
				   
				    
				    var msg=trim(productName);
				    
				    
	                if(msg=="")
	                {
	                     alert ('<%=GetTran("006853", "请选择或填写要发送的短信内容")%>');
	                      return false;
	                }
	                else
	                {
	                    msg=encodeURI (msg);
	                    
	                  
	                    window.returnValue=msg;
	                    window .close();	  
	                }
			    }
			   			
			}
			
				
			
			function openSMScontent(action,id)
			{
			    var con = AjaxClass.OpenSmsContentXZ(id).value;
			    
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
                 <div id="Layer1" style="width: 900px; height: 500px;overflow:auto;">
         <table border="0" cellpadding="0" cellspacing="0" class="biaozzi" width="90%">
           
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

