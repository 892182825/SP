<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCPermission.ascx.cs" Inherits="UserControl_Permission" %>
 <script language="javascript">
function menu( menu,img,plus )
		{		
			if( document.getElementById(menu.toString()).style.display == "none"){
				document.getElementById(menu.toString()).style.display="";
				document.getElementById(img.toString()).src="images/foldopen.gif";
				plus.src="images/MINUS2.GIF";		
			}
			else
			{
				document.getElementById(menu.toString()).style.display = "none";
				document.getElementById(img.toString()).src="images/foldclose.gif";
				plus.src="images/PLUS3.GIF";		
			}
		}
		function CheckChildren(menu,pchkid,pids)
		{
			var pdiv = document.getElementById(menu);
			var pchk = document.getElementById(pchkid);
			var cchks = pdiv.getElementsByTagName('input')
			for(var i=0;i<cchks.length;i++)
			{
			    if(cchks[i].type="CheckBox")
			        cchks[i].checked=pchk.checked;
			}
			var pid=pids.split(',')
           if(pid.length>0)
           {
               for(var x=0;x<pid.length;x++)
               {
                    var pdiv_ = document.getElementById('menu_'+pid[x]);
                    cchks=pdiv_.getElementsByTagName('input');
                    if(cchks.length<0)
                    {
                        return;
                    }
                    var chk= cchks[0];
                    var ced=chk.checked;
                    pdiv = document.getElementById('menu'+pid[x]);
                    cchks=pdiv.getElementsByTagName('input');
                    for(var i=0;i<cchks.length;i++)
                    {
                        if(cchks[i].checked)
                        {
                           ced=true;
                           break; 
                        }   
                    }
                    chk.checked=ced; 
                }
           }		   
			pid=null;
			cchks=null;
		}
		function checkallbox(obj)
		{

		   var checkall=document.getElementById('checkall');		     
		   var checkqx=document.getElementsByName('qxCheckBox');
		    for(var i=0;i<checkqx.length;i++)
		    {
			    checkqx[i].checked=obj.checked;
		    }
		   
		}
		function getpermission(obj)
		{
		    var checkqx=document.getElementsByName('qxCheckBox');
		     for(var i=0;i<checkqx.length;i++)
		      {
		         if(checkqx[i].value==obj)
		         {
		            checkqx[i].checked=true;
		         }
		      }
		}
		function  checkpid(obj,obj1,pids)
		{

		   var checkpid=document.getElementById(obj);
		   if(obj1.checked==true)
		   {
		     checkpid.checked=true;
		   }
		   var pid=pids.split(',')
	       if(pid.length>0)
	       {
	            for(var x=0;x<pid.length;x++)
	            {
	                var pdiv_ = document.getElementById('menu_'+pid[x]);
                    cchks=pdiv_.getElementsByTagName('input')
                    if(cchks.length<0)
                    {
                        return;
                    }
                    var chk= cchks[0]
                    var ced=chk.checked;
                    pdiv = document.getElementById('menu'+pid[x]);
                    cchks=pdiv.getElementsByTagName('input')
                    for(var i=0;i<cchks.length;i++)
                    {
                        if(cchks[i].checked)
                        {
                           ced=true;
                           break; 
                        }      
                    }
                    chk.checked=ced;    
                }
	       }		   
		}
</script>
<TABLE id="Table1"
	width="100%" align="center" >
	<TR>
		<TD align="center" colSpan="2"><FONT face="宋体"><input id="checkall" onclick="checkallbox(this);" type="checkbox" name="Checkall"><%=BLL.Translation.Translate("004198", "全选")%>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkAllot" runat="server" /><%=BLL.Translation.Translate("004199", "是否可以将自己的权限分配给下级")%>
                </FONT></TD>
	</TR>
	<tr>
		<td colSpan="2">&nbsp;<FONT face="宋体" size="2"><%=BLL.Translation.Translate("004197", "当需要分配权限时，请勾相应的复选框（注：权限后面带刮号的,则是刮号里的子权限)")%></FONT><br></td>
	</tr>
</TABLE>
<table cellSpacing="0">
	<TR  valign="top" >
		<TD >
			<div runat="server" id = "DivPermission">
			
			</div>
			<DIV>
                <br />
            </DIV>
		</TD>
	</TR>
</table>
			<DIV id="DivSetPer" runat="server"></DIV>
			