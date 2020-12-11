<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeftMenu_W.aspx.cs" Inherits="Company_LeftMenu_W" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Company.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="Stylesheet" type="text/css"/>
     <link href="css/select.css" rel="Stylesheet" type="text/css"/>
   <script src="../bower_components/jquery/jquery.min.js"></script>
    <style type="text/css">
      
    </style>
    <script type="text/javascript">
      

		function isShowMenu(th)
		{
		    /*var i=1;
		    while(true)
		    {
		        if(document.getElementById("divbody"+i)!=null)
		            createInstance("divbody"+i).style.display="none";
		        else
		            break;
		        
		        i++;
		    }*/
		   
		    var divbodyobj = createInstance(th.id.replace("title", "body"));
		    //var divbodyobj1 = createInstance(th.id.replace("title", "body"));
		    var aa = th.id.substr(th.id.length - 1, 1)

		    if (divbodyobj.style.display == "none") {
		        divbodyobj.style.display = "";
		        $("#table" + aa).show();

		    }

		    else {
		        divbodyobj.style.display = "none";
		        $("#table" + aa).hide();
		    }
			    
			   
		}

        function ShowMessageReceive()
        {
           window.parent.frames["mainframe"].location.href="ManageMessage_Recive.aspx";
        }
		function createInstance(menuId)
		{
			return document.getElementById(menuId);
		}
		
		function setColor(th)
		{
		    var arrA=th.parentNode.parentNode.parentNode.getElementsByTagName("a");
		    var s="";
		    for(var i=0;i<arrA.length;i++)
		    {
		        arrA[i].style.color = "#333333";

		    }
		    
            var aa=   th.href;
            var index = aa.lastIndexOf("\/");
            var str = aa.substring(index + 1, aa.length)
		    th.style.color = "orange";
		    
		    AjaxClass.getsession(str);
		    
		}
		

		function loadM()
		{
		    
			var h=document.documentElement.clientHeight;
			
			//document.getElementById("main").style.top=h-(document.getElementById("main").style.height.replace("px","")-0)-2+"px";
			//document.getElementById("Div3").style.top=h-(document.getElementById("Div3").style.height.replace("px","")-0)-2+"px";
			
			isShowMenu(document.getElementById("divtitle1"));
			document.getElementById("table1").getElementsByTagName("a")[0].style.color="orange";

			if(navigator.userAgent.toLowerCase().indexOf("ie")!=-1)
			    window.frameElement.onresize=gb;
			else
			    window.onresize=gb;

            //alert(navigator.userAgent.toLowerCase())
			
		}
		
		function gb()
		{
		    var h=document.documentElement.clientHeight;

			//document.getElementById("main").style.top=h-(document.getElementById("main").style.height.replace("px","")-0)-2+"px";
			
			//document.getElementById("div3").style.top=h-(document.getElementById("div3").style.height.replace("px","")-0)-2+"px";
		}
		
		function aacc()
		{
		    window.parent.document.getElementById("pp").cols="0px,25px,*";
		}
		
		
	</script>
	
	<%--<script language="JavaScript">
function correctPNG() // correctly handle PNG transparency in Win IE 5.5 & 6.
{
    var arVersion = navigator.appVersion.split("MSIE")
    var version = parseFloat(arVersion[1])
    if ((version >= 5.5) && (document.body.filters))
    {
       for(var j=0; j<document.images.length; j++)
       {
          var img = document.images[j]
          var imgName = img.src.toUpperCase()
          if (imgName.substring(imgName.length-3, imgName.length) == "PNG")
          {
             var imgID = (img.id) ? "id='" + img.id + " ' " : ""
             var imgClass = (img.className) ? "class='" + img.className + "'" : ""
             var imgTitle = (img.title) ? "title='" + img.title + "'" : "title='" + img.alt + "'"
             var imgStyle = "display:inline-block;" + img.style.cssText
             if (img.align == "left") imgStyle = "float:left;" + imgStyle
             if (img.align == "right") imgStyle = "float:right;" + imgStyle
             if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle
             var strNewHTML = "<span " + imgID + imgClass + imgTitle
             + "style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";"
             + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
             + "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>"
             img.outerHTML = strNewHTML
             j = j-1
          }
       }
    }    
}
window.attachEvent("onload", correctPNG);
</script>--%>
</head>
<body onload="loadM()" <%--style="overflow:hidden;"--%> scroll="no" style="overflow:auto">
   
    <form id="form1" runat="server" style="height:100%">
        
      <%--  <div>
            <table width="195" height="460" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td height="8" style=""></td>
                  </tr>
                </table>
                    <table width="95%" height="75" border="0" align="right" cellpadding="0" cellspacing="0" style="font-size:10pt;font-weight:bold;">
                      <tr>
                        <td><marquee onmouseover="this.stop()" onmouseout="this.start()"  direction="up" behavior="scroll" scrollamount="2" height="75">
                          <span style="color:rgb(0,85,117);line-height:22px"><%=GetTran("001066", "管理员编号")%>：</span><span style="color:#333333"><asp:Literal ID="ltlId" runat="server"></asp:Literal></span><br />
                          <span style="color:rgb(0,85,117);line-height:22px"><%=GetTran("002049", "管理员姓名")%>：</span><span style="color:#333333"><asp:Literal ID="ltlNme" runat="server"></asp:Literal></span><br />
                          <span style="color:rgb(0,85,117);line-height:22px"><%=GetTran("002051", "上次登录")%>：</span><span style="color:#333333"><asp:Literal ID="ltlTime" runat="server"></asp:Literal></span><br />
                          <asp:Literal ID="ltlMessage" runat="server" Visible=false></asp:Literal>
                        </marquee></td>
                      </tr>                      
                  </table></td>
              </tr>
            </table>
          </div>--%>
    
        <!--菜单-->
       <%-- style="position:absolute;left:10px;top:0px;width:176px;height:465px;overflow:hidden;background-color:white;filter:alpha(opacity=90);opacity:0.9;z-index:10"--%>
             
             <dl class="leftmenu" style="margin:0px;height:100%">    
                <dd>
			     <%=GetMenu() %>
                 </dd>   			
                 </dl>
<%--		 <dd> <div id="Div1" class="title">
               <%=GetTitle()%> <span onclick="aacc()"><img src="images/menunu15.gif" align="absmiddle"/></span>   
            </div></dd>--%>
		<%--<div id="Div3" style="position:absolute;left:0px;top:0px;width:176px;height:460px;overflow:hidden;z-index:2;background-repeat:no-repeat;background-image:url(images/lmenudp.gif);background-position:0px 250px">
		</div>--%>
    </form>
</body>
</html>
