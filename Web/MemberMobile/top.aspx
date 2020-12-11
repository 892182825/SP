<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="Member_top" %>

<!DOCTYPE htm PUBLIC "-//W3C//DTD Xhtm 1.0 Transitional//EN" "http://www.w3.org/TR/xhtm1/DTD/xhtm1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtm">
<head runat="server">
    <title>无标题页</title>

    <script src="../Store/Javascript/JScript.js" type="text/javascript"></script>
		    <link href="CSS/Member.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css" >
        .white { FONT-SIZE: 12px; COLOR: white; TEXT-DECORATION: none ;font-family: Arial; }
		.yellow { FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: yellow; TEXT-DECORATION: none; font-family: Arial;}
		.yellow2 { FONT-WEIGHT: bold; FONT-SIZE: 13px; COLOR: yellow; TEXT-DECORATION: none; font-family: Arial;}
    </style>
    <script language="javascript" type="text/javascript" src="../JS/ZXKF.js" defer=defer></script>
    
    <script language="javascript" type="text/javascript" >
        function change(id)
        {
            menu0.className="white";
	        menu1.className="white";
            menu2.className="white";
	        menu3.className="white";
	        menu4.className="white";
	        menu5.className="white";
	        menu6.className="white";
	        
 	        switch(id)
	        {
	            case 0: 
	                menu0.className="yellow"; 
//	                window.open("First.aspx?flag=1", "mainframe","channelmode"); 
	                break;
		        case 1: 
		            menu1.className="yellow"; 
		            window.open("First.aspx", "mainframe","channelmode"); 
	                break;
		        case 2:  
		            menu2.className="yellow"; 
		            window.open("OrderAgainBegin.aspx", "mainframe","channelmode"); 
	                break;
	            case 3:  
	                menu3.className="yellow"; 
			        window.open("BasicSearch.aspx?flag=1", "mainframe","channelmode"); 
	                break;
	            case 4:  
	                menu4.className="yellow"; 
			        window.open("updatePass.aspx?flag=1", "mainframe","channelmode"); 
	                break;
	            case 5:  
	                menu5.className="yellow"; 
			        window.open("DownLoadFiles.aspx?flag=1", "mainframe","channelmode"); 
	                break;
	            case 6:  
	                menu6.className="yellow"; 
//			        window.open("", "mainframe","channelmode"); left.aspx?pid=160
	                break;
	               
	        }
        }
       
       
       function abc()
       {
            if(window.parent.document.getElementById("pp").cols=="195px,18px,*")
            {
                window.parent.document.getElementById("pp").cols="0px,18px,*";
                document.getElementById("imgid").src="../Company/images/menunu16.gif";
                
                document.getElementById("tx").innerHTML='<%=GetTran("006011", "显示")%>';
            }
            else
            {
                window.parent.document.getElementById("pp").cols="195px,18px,*";
                document.getElementById("imgid").src="../Company/images/menunu15.gif";
                
                document.getElementById("tx").innerHTML='<%=GetTran("006017", "隐藏")%>';
            }
       }
    </script>
</head>
<body >
    <form id="form1" runat="server">

    
    <table width="100%" height="28" border="0" cellpadding="0" cellspacing="0" background="images/menudp.gif" >
              <tr>
				<td width="20%" nowrap="nowrap" style="padding-left:10px">	<A href="First.aspx" target="mainframe" class="white" id="menu0" onclick="change(0);"><img src="images/menunu01.gif" width="20" height="26" border="0" align="absmiddle" /> <%=GetTran("001478", "首页")%></a>
	&nbsp; <a href="First.aspx" target="mainframe" class="white" id="menu6" onclick="change(6);"><img src="images/menunu02.gif" width="20" height="26" border="0" align="absmiddle" /> <%=GetTran("001651", "帮助")%></span></a>
	&nbsp; <a href="../logout.aspx" class="white" target="_parent"><img src="images/menunu03.gif" width="20" height="26" border="0" align="absmiddle" /> <%=GetTran("001652", "退出")%></span></a>
            	&nbsp; <%--<a onclick="abc()" style="color:white;font-size:10pt;cursor:pointer"><img src="../Company/images/menunu15.gif" id="imgid" align="absmiddle"><span id="tx" ><%=GetTran("006017", "隐藏")%></span></a>--%>
            	</td>
			    <td>
			        <table width="100%" border="0" cellspacing="0" cellpadding="0">
			            <tr>
			                <td width="11%" nowrap="nowrap" ><a class="white" href="left.aspx?pid=160" target="leftmenu" id="menu1" onclick="change(1);" ><img src="images/menunu04.gif" width="20" height="26" border="0" align="absmiddle"  /> <%=GetTran("001670", "组织结构")%></a></td>
				            <td width="11%" nowrap="nowrap" ><A class="white" href="left.aspx?pid=161" target="leftmenu" id="menu2" onclick="change(2);"><img src="images/menunu05.gif" width="26" height="26" border="0" align="absmiddle" /> <%=GetTran("007049", "报单管理")%></A></td>
				            <td width="11%" nowrap="nowrap" ><A class="white" href="left.aspx?pid=162" target="leftmenu" id="menu3" onclick="change(3);"><img src="images/menunu06.gif" width="25" height="26" border="0" align="absmiddle" /> <%=GetTran("001673", "帐户管理")%></a></td>
				            <td width="11%" nowrap="nowrap" ><A class="white" href="left.aspx?pid=163" target="leftmenu" id="menu4" onclick="change(4);"><img src="images/menunu07.gif" width="24" height="26" border="0" align="absmiddle" /> <%=GetTran("001668", "个性修改")%></A></td>
				            <td width="11%" nowrap="nowrap" > <A class="white" href="left.aspx?pid=164" target="leftmenu" id="menu5" onclick="change(5);"><img src="images/menunu08.gif" width="24" height="26" border="0" align="absmiddle" /> <%=GetTran("001658", "信息中心")%></A></td>
				            <td width="11%" nowrap="nowrap" style="display:none"> <A class="white" href="#" onclick="createDiv()"><img src="images/menunu08.gif" width="24" height="26" border="0" align="absmiddle" /> 高级设置</A></td>
				        </tr>
				    </table>
				</td>
			</tr>
	</table>
		<div id="DealDiv" runat="server"></div>
    </form>
</body>
</html>
